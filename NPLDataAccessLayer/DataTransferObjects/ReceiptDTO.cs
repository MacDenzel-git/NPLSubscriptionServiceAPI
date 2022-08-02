using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPLDataAccessLayer.DataTransferObjects
{
    public class ReceiptDTO
    {
        public string ClientName { get; set; }
        public string TransactionId { get; set; }
        public string PublicationTitle { get; set; }
        public string PaymentAccountDetails { get; set; }
        public decimal Amount { get; set; }

        public int Duration { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime StartDate { get; set; }
        

    }
}
