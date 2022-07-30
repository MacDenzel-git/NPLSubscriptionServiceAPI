using System;
using System.Collections.Generic;

namespace NPLDataAccessLayer.Models
{
    public partial class Branch
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; } = null!;
        public int CountryId { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }

        public virtual Country Country { get; set; } = null!;
    }
}
