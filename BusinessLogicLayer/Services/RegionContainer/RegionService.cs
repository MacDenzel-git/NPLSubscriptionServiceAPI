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

namespace BusinessLogicLayer.Services.RegionContainer
{
    public class RegionService : IRegionService 
    {

        private readonly GenericRepository<Region> _service;
        public RegionService(GenericRepository<Region> service)
        {
            _service = service;
        }
        public async Task<OutputHandler> Create(RegionDTO region)
        {
            try
            {
                //check if record with same name already exist to avoid duplicates
                bool isExist = await _service.AnyAsync(x => x.RegionName == region.RegionName);
                if (isExist)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = StandardMessages.GetDuplicateMessage(region.RegionName) 

                    };
                }
 
                var mapped = new AutoMapper<RegionDTO, Region>().MapToObject(region);
               var result = await _service.Create(mapped);
                //await _service.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }

        }

        public async Task<OutputHandler> Delete(int regionId)
        {

            try
            {
                await _service.Delete(x => x.RegionId == regionId);
                //await _service.SaveChanges();
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

        public async Task<RegionDTO> GetRegion(int regionId)
        {
            return new AutoMapper<Region, RegionDTO>().MapToObject(await _service.GetSingleItem(x => x.RegionId == regionId));
        }

        public async Task<IEnumerable<RegionDTO>> GetAllRegions()
        {
            var output = await _service.GetAll( );
            return new AutoMapper<Region, RegionDTO>().MapToList(output);
        }

        public async Task<OutputHandler> Update(RegionDTO region)
        {
            try
            {
                //check if record with same name already exist to avoid duplicates
                bool isExist = await _service.AnyAsync(x => x.RegionName == region.RegionName);
                if (isExist)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = StandardMessages.GetDuplicateMessage(region.RegionName)

                    };
                }
                var mapped = new AutoMapper<RegionDTO, Region>().MapToObject(region);
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
