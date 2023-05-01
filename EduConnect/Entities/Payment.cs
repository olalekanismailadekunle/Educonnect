using EduConnect.Contract;
using EduConnect.Enums;
using System;

namespace EduConnect.Entities
{
    public class Payment : AuditableEntity
    {
        public decimal Amount { get; set; }
        public Guid ReferenceNumber { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
