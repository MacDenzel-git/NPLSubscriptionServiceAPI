using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPLDataAccessLayer.DataTransferObjects
{
    public class SelfSubscriptionApplicationDTO
    {
        public int SubscriptionApplicationId { get; set; }
        public string FullName { get; set; } = null!;
        public string? Email { get; set; }
        public int PublicationId { get; set; }
        public int? CountryId { get; set; }
        public int? ClientTypeId { get; set; }
        public int SubscriptionTypeId { get; set; }
        public int PaymentTypeId { get; set; }
        public int TypeOfDeliveryId { get; set; }
        public string? TransactionId { get; set; }
        public bool IsConfirmed { get; set; }
         public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? RegionId { get; set; }
        public string? Location { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? SubscriptionStartDate { get; set; }
        public int? NumberOfCopies { get; set; }
        public bool IsDeleted { get; set; }

        public string? PublicationTitle { get; set; }
        public string? PaymentTypeDescription { get; set; }
        public string? TypeOfDeliveryDescription { get; set; }
        public string SubscriptionTypeDescription { get; set; }
    }
}
