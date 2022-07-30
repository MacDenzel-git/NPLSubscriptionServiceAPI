﻿using System;
using System.Collections.Generic;

namespace NPLDataAccessLayer.Models
{
    public partial class PaymentType
    {
        public int PaymentTypeId { get; set; }
        public string Description { get; set; } = null!;
        public int AccountNumber { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
