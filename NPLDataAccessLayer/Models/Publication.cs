using System;
using System.Collections.Generic;

namespace NPLDataAccessLayer.Models
{
    public partial class Publication
    {
        public Publication()
        {
            NewsLetters = new HashSet<NewsLetter>();
            SelfSubscriptionApplications = new HashSet<SelfSubscriptionApplication>();
            Subscriptions = new HashSet<Subscription>();
        }

        public int PublicationId { get; set; }
        public string PublicationTitle { get; set; } = null!;
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<NewsLetter> NewsLetters { get; set; }
        public virtual ICollection<SelfSubscriptionApplication> SelfSubscriptionApplications { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
