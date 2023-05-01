using EduConnect.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EduConnect.Interfaces.IRepositories
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        Task<Payment> GetPayment(Expression<Func<Payment, bool>> expression);
        Task<IList<Payment>> GetAllPayment();
        Task<IList<Payment>> GetPaymentByLGA();
        Task<IList<Payment>> GetPaymentByDate();
        Payment GetPaymentById(int id);
    }
}
