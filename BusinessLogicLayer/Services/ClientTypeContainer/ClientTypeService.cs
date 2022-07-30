using BusinessLogicLayer.Services.ClientTypeContainer;
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

namespace BusinessLogicLayer.Services.SubscriptionServiceContainer
{
    public class ClientTypeService : IClientTypeService
    {

        private readonly GenericRepository<ClientType> _service;
        public ClientTypeService(GenericRepository<ClientType> service)
        {
            _service = service;
        }
        public async Task<OutputHandler> Create(ClientTypeDTO clientType)
        {
            try
            {
                //check if record with same name already exist to avoid duplicates
                    bool isExist = await _service.AnyAsync(x => x.Description == clientType.Description);
                if (isExist)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = StandardMessages.GetDuplicateMessage(clientType.Description)

                    };
                }

                var mapped = new AutoMapper<ClientTypeDTO, ClientType>().MapToObject(clientType);
                var result = await _service.Create(mapped);
                return result;
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }

        }

        public async Task<OutputHandler> Delete(int paymentTypeId)
        {

            try
            {
                await _service.Delete(x => x.ClientTypeId == paymentTypeId);
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

        public async Task<ClientTypeDTO> GetClientType(int clientTypeId)
        {
            var output = await _service.GetSingleItem(x => x.ClientTypeId == clientTypeId);
            return new AutoMapper<ClientType, ClientTypeDTO>().MapToObject(output);
        }

        public async Task<IEnumerable<ClientTypeDTO>> GetAllClientTypes()
        {
            var output = await _service.GetAll();
            return new AutoMapper<ClientType, ClientTypeDTO>().MapToList(output);
        }

        public async Task<OutputHandler> Update(ClientTypeDTO clientType)
        {
            try
            {
                //check record already exist to avoid duplicates
                bool isExist = await _service.AnyAsync(x => x.Description == clientType.Description);
                if (isExist)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = StandardMessages.GetDuplicateMessage(clientType.Description)

                    };
                }
                var mapped = new AutoMapper<ClientTypeDTO, ClientType>().MapToObject(clientType);
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
