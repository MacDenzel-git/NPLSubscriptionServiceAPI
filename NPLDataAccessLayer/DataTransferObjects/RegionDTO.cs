using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPLDataAccessLayer.DataTransferObjects
{
    public class RegionDTO
    {
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public int CountryId { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
