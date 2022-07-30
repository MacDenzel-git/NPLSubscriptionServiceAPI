using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPLDataAccessLayer.DataTransferObjects
{
    public class PromotionDTO
    {
        public int PromotionId { get; set; }
        public string? PromotionCode { get; set; }
        public int? DiscountPercentage { get; set; }
    }
}
