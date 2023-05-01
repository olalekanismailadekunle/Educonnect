using EduConnect.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EduConnect.Interfaces.IRepositories
{
    public interface IBookingRepository : IBaseRepository<Booking>
    {
        Task<Booking> GetBooking(Expression<Func<Booking, bool>> expression);
        Task<IList<Booking>> GetAllBooking();
        Task<IList<Booking>> GetNotApprovedBooking();
        Task<IList<Booking>> GetBookingByParent(int id);
        Task<IList<Booking>> GetBookingByTutor(int id);
        Task<IList<Booking>> GetNotApprovedBookingByParent(int id);
       
        Task<IList<Booking>> GetBookingByQualification(Expression<Func<Booking, bool>> expression);
        Task<IList<Booking>> GetAllbookingByQualification();
        Task<Booking> GetBookingById(int id);
    }
}
