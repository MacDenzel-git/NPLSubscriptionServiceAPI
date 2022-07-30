using BusinessLogicLayer.Services.DistrictServiceContainer;
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

namespace BusinessLogicLayer.Services.DistrictServiceContainer
{
    public class DistrictService : IDistrictService 
    {

        private readonly GenericRepository<District> _service;
        public DistrictService(GenericRepository<District> service)
        {
            _service = service;
        }
        public async Task<OutputHandler> Create(DistrictDTO district)
        {
            try
            {
                //check if record with same name already exist to avoid duplicates
                bool isExist = await _service.AnyAsync(x => x.DistrictName == district.DistrictName);
                if (isExist)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = StandardMessages.GetDuplicateMessage(district.DistrictName) 

                    };
                }
   
               var mapped = new AutoMapper<DistrictDTO, District>().MapToObject(district);
               var result = await _service.Create(mapped);
               return result;
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }

        }

        public async Task<OutputHandler> Delete(int districtId)
        {

            try
            {
                await _service.Delete(x => x.DistrictId == districtId);
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

        public async Task<DistrictDTO> GetDistrict(int districtId)
        {
            var output = await _service.GetSingleItem(x => x.DistrictId == districtId);
            return new AutoMapper<District, DistrictDTO>().MapToObject(output);
        }

        public async Task<IEnumerable<DistrictDTO>> GetAllDistricts()
        {
            var output = await _service.GetAll( );
            return new AutoMapper<District, DistrictDTO>().MapToList(output);
        }

        public async Task<OutputHandler> Update(DistrictDTO district)
        {
            try
            {
                //check if record with same name already exist to avoid duplicates
                bool isExist = await _service.AnyAsync(x => x.DistrictName == district.DistrictName);
                if (isExist)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = StandardMessages.GetDuplicateMessage(district.DistrictName)

                    };
                }
                var mapped = new AutoMapper<DistrictDTO, District>().MapToObject(district);
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
