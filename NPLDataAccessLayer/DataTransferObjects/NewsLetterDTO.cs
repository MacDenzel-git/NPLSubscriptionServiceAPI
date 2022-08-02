using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPLDataAccessLayer.DataTransferObjects
{
    public class NewsLetterDTO
    {
        public int NewsLetterId { get; set; }
        public int PublicationId { get; set; }
        public int SubmissionCount { get; set; }
        public string? Status { get; set; }
        public string? FileLocation { get; set; }
        public string? FileName{ get; set; }

        public bool IsSubmittedSuccessfully { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? SubmittedBy { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime PaperDated { get; set; }
        public Byte[]? File { get; set; }

        public string? PublicationTitle { get; set; }   
    }
}
