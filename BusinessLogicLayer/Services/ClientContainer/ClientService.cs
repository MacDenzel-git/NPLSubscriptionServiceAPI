using Microsoft.EntityFrameworkCore;
using NPLDataAccessLayer.DataTransferObjects;
using NPLDataAccessLayer.GenericRepositoryContainer;
using NPLDataAccessLayer.Models;
using NPLReusableResourcesPackage.AutoMapperContainer;
using NPLReusableResourcesPackage.ErrorHandlingContainer;
using NPLReusableResourcesPackage.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.ClientServiceContainer
{
    public class ClientService : IClientService 
    {

        private readonly GenericRepository<Client> _service;
        private readonly NPLSubsctiptionServiceDBContext _context;
        public ClientService(GenericRepository<Client> service,
            NPLSubsctiptionServiceDBContext context)
        {
            _context = context;
            _service = service;
        }
        public async Task<OutputHandler> Create(ClientDTO client)
        {
            try
            {
                //names can be the same but emails can only be assigned to one client
                bool isExist = await _service.AnyAsync(x => x.Email == client.Email);
                if (isExist)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = StandardMessages.GetDuplicateMessage(client.Email) 

                    };
                }
 
                var mapped = new AutoMapper<ClientDTO, Client>().MapToObject(client);
               var result = await _service.Create(mapped);
                 return result;
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }

        }

        public async Task<OutputHandler> Delete(int id)
        {

            try
            {
                await _service.Delete(x => x.ClientId == id);
                 return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = StandardMessages.GetSuccessfulMessage()
                };
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }

        public async Task<ClientDTO> GetClient(int id)
        {
            var output = await _service.GetSingleItem(x => x.ClientId == id);
            return new AutoMapper<Client, ClientDTO>().MapToObject(output);
        }

        public async Task<IEnumerable<ClientDTO>> GetAllClients()
        {
            var output = await (from c in _context.Clients 
                                join ct in _context.ClientTypes on c.ClientTypeId equals ct.ClientTypeId
                                join r in _context.Regions on c.RegionId equals r.RegionId
                                join d in _context.Districts on c.DistrictId equals d.DistrictId
                                select new ClientDTO
                                {
                                     
                                    ClientName = c.ClientName,
                                    ClientTypeName = ct.Description,
                                    Location = c.Location,
                                    PhoneNumber = c.PhoneNumber,
                                    RegionName = r.RegionName,
                                    DistrictName = d.DistrictName,
                                    ClientId = c.ClientId
                                }).ToListAsync();

            return output;
        }

        public async Task<IEnumerable<ClientDTO>> ClientsByRegion(int regionId)
        {
            var output = await (from c in _context.Clients.Where(x => x.RegionId == regionId)
                         join ct in _context.ClientTypes on c.ClientTypeId equals ct.ClientTypeId
                         join r in _context.Regions on c.RegionId equals r.RegionId
                         join d in _context.Districts on c.DistrictId equals d.DistrictId
                         select new ClientDTO
                         {
                             RegionId = regionId,
                             ClientName = c.ClientName,
                             ClientTypeName = ct.Description,
                             Location = c.Location,
                             PhoneNumber = c.PhoneNumber,
                             RegionName = r.RegionName,
                             DistrictName = d.DistrictName,
                         }).ToListAsync();

             return output;
        }
        public async Task<OutputHandler> Update(ClientDTO client)
        {
            try
            {
                if (!client.OldEmail.Equals(client.Email))//this means email was was updated because the email doesn't match with old email
                {
                    //check new email if it's already registered
                    //names can be the duplicated but emails can only be assigned to one client
                    bool isExist = await _service.AnyAsync(x => x.Email == client.Email);
                    if (isExist)
                    {
                        return new OutputHandler
                        {
                            IsErrorOccured = true,
                            Message = StandardMessages.GetDuplicateMessage(client.Email)

                        };
                    }
                }


                var mapped = new AutoMapper<ClientDTO, Client>().MapToObject(client);
                var result = await _service.Update(mapped);
                return result;
                  
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }

        
    }
}
