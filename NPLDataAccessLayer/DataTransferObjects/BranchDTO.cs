using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPLDataAccessLayer.DataTransferObjects
{
    public class BranchDTO
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; } = null!;
        public int CountryId { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
