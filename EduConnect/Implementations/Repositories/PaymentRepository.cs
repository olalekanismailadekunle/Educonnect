using EduConnect.Context;
using EduConnect.Entities;
using EduConnect.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EduConnect.Implementations.Repositories
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ApplicationContext context)
        {
            _context = context;
        }
    
public Task<IList<Payment>> GetAllPayment()
        {
            throw new NotImplementedException();
        }

        public Task<Payment> GetPayment(Expression<Func<Payment, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Payment>> GetPaymentByDate()
        {
            throw new NotImplementedException();
        }

        public Payment GetPaymentById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Payment>> GetPaymentByLGA()
        {
            throw new NotImplementedException();
        }
    }
}
