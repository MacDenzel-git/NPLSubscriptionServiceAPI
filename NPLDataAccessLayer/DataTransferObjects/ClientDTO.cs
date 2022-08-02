using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPLDataAccessLayer.DataTransferObjects
{
    public class ClientDTO
    {
        public int ClientId { get; set; }
        public int ClientTypeId { get; set; }

        public string Location { get; set; } 
        public string ClientName { get; set; } 
        public string PhoneNumber { get; set; } 
        public int RegionId { get; set; }
        public long DistrictId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public string? OldEmail { get; set; } //to hold previous email to be used for validation
        public string? ClientTypeName { get; set; }
        public string? Email { get; set; }
        public string? RegionName { get; set; }
        public string? DistrictName { get; set; }

    }
}
