using System;
using System.Collections.Generic;

namespace NPLDataAccessLayer.Models
{
    public partial class SubscriptionType
    {
        public SubscriptionType()
        {
            Subscriptions = new HashSet<Subscription>();
        }

        public int SubscriptionTypeId { get; set; }
        public bool IsCalculationTypeMonths { get; set; }
        public string SubscriptionName { get; set; } = null!;
        public decimal SubscriptionFee { get; set; }
        public int Duration { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
