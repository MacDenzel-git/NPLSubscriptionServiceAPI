using System;
using System.Collections.Generic;

namespace NPLDataAccessLayer.Models
{
    public partial class Region
    {
        public Region()
        {
            Clients = new HashSet<Client>();
            SelfSubscriptionApplications = new HashSet<SelfSubscriptionApplication>();
        }

        public int RegionId { get; set; }
        public string RegionName { get; set; } = null!;
        public int CountryId { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }

        public virtual Country Country { get; set; } = null!;
        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<SelfSubscriptionApplication> SelfSubscriptionApplications { get; set; }
    }
}
