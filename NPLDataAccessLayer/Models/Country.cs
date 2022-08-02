using System;
using System.Collections.Generic;

namespace NPLDataAccessLayer.Models
{
    public partial class Country
    {
        public Country()
        {
            Branches = new HashSet<Branch>();
            Districts = new HashSet<District>();
            Regions = new HashSet<Region>();
            SelfSubscriptionApplications = new HashSet<SelfSubscriptionApplication>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; } = null!;
        public DateTime? DateCreated { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }

        public virtual ICollection<Branch> Branches { get; set; }
        public virtual ICollection<District> Districts { get; set; }
        public virtual ICollection<Region> Regions { get; set; }
        public virtual ICollection<SelfSubscriptionApplication> SelfSubscriptionApplications { get; set; }
    }
}
