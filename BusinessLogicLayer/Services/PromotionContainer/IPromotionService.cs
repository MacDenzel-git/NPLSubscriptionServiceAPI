using NPLDataAccessLayer.DataTransferObjects;
using NPLReusableResourcesPackage.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.PromotionServiceContainer
{
    public interface IPromotionService
    {
        Task<OutputHandler> Create(PromotionDTO promotion);
        Task<OutputHandler> Update(PromotionDTO promotion);
        Task<OutputHandler> Delete(int promotionId);
        Task<IEnumerable<PromotionDTO>> GetAllPromotions();
        Task<PromotionDTO> GetPromotion(int promotionId);
    }
}
