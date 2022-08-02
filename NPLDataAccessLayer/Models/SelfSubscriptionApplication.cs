using System;
using System.Collections.Generic;

namespace NPLDataAccessLayer.Models
{
    public partial class SelfSubscriptionApplication
    {
        public int SubscriptionApplicationId { get; set; }
        public string FullName { get; set; } = null!;
        public string? Email { get; set; }
        public int PublicationId { get; set; }
        public int CountryId { get; set; }
        public int ClientTypeId { get; set; }
        public int SubscriptionTypeId { get; set; }
        public int PaymentTypeId { get; set; }
        public int TypeOfDeliveryId { get; set; }
        public string TransactionId { get; set; } = null!;
        public bool IsConfirmed { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? RegionId { get; set; }
        public string? Location { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? SubscriptionStartDate { get; set; }
        public int? NumberOfCopies { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Country Country { get; set; } = null!;
        public virtual PaymentType PaymentType { get; set; } = null!;
        public virtual Publication Publication { get; set; } = null!;
        public virtual Region? Region { get; set; }
        public virtual TypeOfDelivery TypeOfDelivery { get; set; } = null!;
    }
}
