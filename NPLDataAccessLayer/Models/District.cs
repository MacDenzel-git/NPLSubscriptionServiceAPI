using System;
using System.Collections.Generic;

namespace NPLDataAccessLayer.Models
{
    public partial class District
    {
        public District()
        {
            Clients = new HashSet<Client>();
        }

        public long DistrictId { get; set; }
        public string DistrictName { get; set; } = null!;
        public int CountryId { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }

        public virtual Country Country { get; set; } = null!;
        public virtual ICollection<Client> Clients { get; set; }
    }
}
