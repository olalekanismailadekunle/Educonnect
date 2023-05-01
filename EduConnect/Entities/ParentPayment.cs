using EduConnect.Contract;

namespace EduConnect.Entities
{
    public class ParentPayment : BaseEntity
    {
        public int ParentId { get; set; }
        public Parent Parent { get; set; }
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }
    }
}
