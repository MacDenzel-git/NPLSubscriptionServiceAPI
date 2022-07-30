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
        public string PaymentType { get; set; } = null!;
        public string TransactionId { get; set; } = null!;
        public int ClientId { get; set; }
        public bool IsUsed { get; set; }

        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
