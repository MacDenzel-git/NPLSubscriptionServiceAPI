using System;
using System.Collections.Generic;

namespace NPLDataAccessLayer.Models
{
    public partial class SubscriptionStatus
    {
        public SubscriptionStatus()
        {
            Subscriptions = new HashSet<Subscription>();
        }

        public int SubscriptionStatusId { get; set; }
        public string Description { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
