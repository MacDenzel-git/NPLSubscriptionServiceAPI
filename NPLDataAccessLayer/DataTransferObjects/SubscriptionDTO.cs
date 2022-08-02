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
        public int SubscriptionStatusId { get; set; }
        public decimal ChargeInMwk { get; set; }
        public int PromotionId { get; set; }
        public DateTime DateOfSubscription { get; set; }
        public DateTime ExpiryDate { get; set; }
        public long PaymentId { get; set; }
        public string? CreatedBy { get; set; } 
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? NumberOfCopies { get; set; }
        public int PublicationId { get; set; }
        public int TypeOfDeliveryId { get; set; }

        public string? TypeOfDeliveryDescription { get; set; }
        public string? PublicationTitle { get; set; }
        public string? SubscriptionTypeDescription { get; set; }
        public string? SubscriptionStatusDescription { get; set; }
        public string? ClientName { get; set; }
        public string? PromotionCode { get; set; }
        public int? TotalSubscriptions { get; set; }
        public IEnumerable<PublicationStats>? PublicationStats { get; set; }
    }
}
