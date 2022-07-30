using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPLDataAccessLayer.DataTransferObjects
{
    public class SubscriptionDTO
    {
        public long SubscriptionId { get; set; }
        public int ClientId { get; set; }
        public int SubscriptionTypeId { get; set; }
        public decimal ChargeInMwk { get; set; }
        public int PromotionId { get; set; }
        public DateTime DateOfSubscription { get; set; }
        public DateTime ExpiryDate { get; set; }
        public long PaymentId { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
