using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPLDataAccessLayer.DataTransferObjects
{
    public class PublicationDTO
    {
        public int PublicationId { get; set; }
        public string PublicationTitle { get; set; } 
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; } 
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; } 
        public DateTime? ModifiedDate { get; set; }

    }
}
