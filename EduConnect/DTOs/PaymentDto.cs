using EduConnect.Enums;
using System;

namespace EduConnect.DTOs
{
    public class PaymentDto
    {
        public decimal Amount { get; set; }
        public Guid ReferenceNumber { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
