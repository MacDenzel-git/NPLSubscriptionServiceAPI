using System;
using System.Collections.Generic;

namespace NPLDataAccessLayer.Models
{
    public partial class Payment
    {
        public Payment()
        {
            Subscriptions = new HashSet<Subscription>();
        }

        public long PaymentId { get; set; }
        public int PaymentTypeId { get; set; }
        public string TransactionId { get; set; } = null!;
        public string? Comment { get; set; }  
        public int ClientId { get; set; }
        public bool IsUsed { get; set; }
        public decimal Amount { get;set; }
        public virtual PaymentType PaymentType { get; set; } = null!;
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
