using System;
using System.Collections.Generic;

namespace NPLDataAccessLayer.Models
{
    public partial class NewsLetter
    {
        public int NewsLetterId { get; set; }
        public int PublicationId { get; set; }
        public string? FileLocation { get; set; } 
        public bool IsSubmittedSuccessfully { get; set; }
        public string? CreatedBy { get; set; } 
        public DateTime CreatedDate { get; set; }
        public DateTime PaperDated { get; set; }
        public string? SubmittedBy { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int SubmissionCount { get; set; }
        public string? Status { get; set; }
        public virtual Publication Publication { get; set; } = null!;
    }
}
