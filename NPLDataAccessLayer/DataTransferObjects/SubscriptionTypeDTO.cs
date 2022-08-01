using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPLDataAccessLayer.DataTransferObjects
{
    public class SubscriptionTypeDTO
    {
        public int SubscriptionTypeId { get; set; }
        public bool IsCalculationTypeMonths { get; set; }
        public string SubscriptionName { get; set; }
        public decimal SubscriptionFee { get; set; }
        public int Duration { get; set; }
        public string CreatedBy { get; set; } 
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
