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

namespace BusinessLogicLayer.Services.PromotionServiceContainer
{
    public class PromotionService : IPromotionService 
    {

        private readonly GenericRepository<Promotion> _service;
        public PromotionService(GenericRepository<Promotion> service)
        {
            _service = service;
        }
        public async Task<OutputHandler> Create(PromotionDTO promotion)
        {
            try
            {
                //Promotion can only be used ones, this code restricts duplicate
                bool isExist = await _service.AnyAsync(x => x.PromotionCode == promotion.PromotionCode);
                if (isExist)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = StandardMessages.GetDuplicateMessage(promotion.PromotionCode) 

                    };
                }
 
                var mapped = new AutoMapper<PromotionDTO, Promotion>().MapToObject(promotion);
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
                await _service.Delete(x => x.PromotionId == id);
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

        public async Task<PromotionDTO> GetPromotion(int id)
        {
            var output = await _service.GetSingleItem(x => x.PromotionId == id);
            return new AutoMapper<Promotion, PromotionDTO>().MapToObject(output);
        }

        public async Task<IEnumerable<PromotionDTO>> GetAllPromotions()
        {
            var output = await _service.GetAll( );
            return new AutoMapper<Promotion, PromotionDTO>().MapToList(output);
        }

        public async Task<OutputHandler> Update(PromotionDTO promotion)
        {
            try
            {
                bool isExist = await _service.AnyAsync(x => x.PromotionCode == promotion.PromotionCode);
                if (isExist)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = StandardMessages.GetDuplicateMessage(promotion.PromotionCode)

                    };
                }

                var mapped = new AutoMapper<PromotionDTO, Promotion>().MapToObject(promotion);
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
