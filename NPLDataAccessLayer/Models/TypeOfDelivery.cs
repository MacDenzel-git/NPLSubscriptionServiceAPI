using System;
using System.Collections.Generic;

namespace NPLDataAccessLayer.Models
{
    public partial class TypeOfDelivery
    {
        public TypeOfDelivery()
        {
            SelfSubscriptionApplications = new HashSet<SelfSubscriptionApplication>();
            Subscriptions = new HashSet<Subscription>();
        }

        public int TypeOfDeliveryId { get; set; }
        public decimal? DeliveryFee { get; set; }
        public string TypeOfDeliveryDescription { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime? DateModified { get; set; }
        public string? ModifiedBy { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<SelfSubscriptionApplication> SelfSubscriptionApplications { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
