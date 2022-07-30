using System;
using System.Collections.Generic;

namespace NPLDataAccessLayer.Models
{
    public partial class Client
    {
        public Client()
        {
            Subscriptions = new HashSet<Subscription>();
        }

        public int ClientId { get; set; }
        public int ClientTypeId { get; set; }
        public string Location { get; set; } = null!;
        public string ClientName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } 
        public int RegionId { get; set; }
        public long DistrictId { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ClientType ClientType { get; set; } = null!;
        public virtual District District { get; set; } = null!;
        public virtual Region Region { get; set; } = null!;
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
