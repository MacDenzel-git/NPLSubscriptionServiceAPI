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
        public string PaymentType { get; set; } = null!;
        public string TransactionId { get; set; } = null!;
        public int ClientId { get; set; }
        public bool IsUsed { get; set; }
    }
}
