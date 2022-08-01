using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPLDataAccessLayer.DataTransferObjects
{
    public class PaymentDTO
    {
        public long PaymentId { get; set; }
        public int PaymentTypeId { get; set; } 
        public string? PaymentTypeDescription { get; set; }
        public string? TransactionId { get; set; } 
        public int ClientId { get; set; }
        public string? ClientName { get; set; }
        public bool IsUsed { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int AccountNumber { get; set; }
    }
}
