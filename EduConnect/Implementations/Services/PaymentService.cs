using EduConnect.Interfaces.IRepositories;
using EduConnect.Interfaces.IServices;

namespace EduConnect.Implementations.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
    

    }
}
