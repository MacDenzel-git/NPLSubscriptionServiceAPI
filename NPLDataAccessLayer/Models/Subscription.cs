using System;
using System.Collections.Generic;

namespace NPLDataAccessLayer.Models
{
    public partial class Subscription
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

        public virtual Client Client { get; set; } = null!;
        public virtual Payment Payment { get; set; } = null!;
        public virtual Promotion Promotion { get; set; } = null!;
    }
}
