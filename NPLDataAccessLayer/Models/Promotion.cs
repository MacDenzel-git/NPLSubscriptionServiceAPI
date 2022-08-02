using System;
using System.Collections.Generic;

namespace NPLDataAccessLayer.Models
{
    public partial class Promotion
    {
        public Promotion()
        {
            Subscriptions = new HashSet<Subscription>();
        }

        public int PromotionId { get; set; }
        public string PromotionCode { get; set; } = null!;
        public int DiscountPercentage { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }

        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
