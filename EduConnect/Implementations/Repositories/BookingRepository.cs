using EduConnect.Context;
using EduConnect.Entities;
using EduConnect.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EduConnect.Implementations.Repositories
{
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IList<Booking>> GetAllBooking()
        {
            return await _context.Bookings.Include(a => a.Parent).Include(a => a.Student).Include(a => a.Subject).Include(a => a.Tutor).Include(a => a.Payment).Where(a => a.BookingStatus == Enums.BookingStatus.Approved).ToListAsync();
        }

        public async Task<IList<Booking>> GetAllbookingByQualification()
        {
            return await _context.Bookings.Include(x => x.Tutor.Qualification).ToListAsync();
        }

        public async Task<Booking> GetBooking(Expression<Func<Booking, bool>> expression)
        {
            return await _context.Bookings.Include(x => x.Parent).SingleOrDefaultAsync(expression);
        }

        public async Task<Booking> GetBookingById(int id)
        {
            return await _context.Bookings.Include(a => a.Parent).Include(a => a.Student).Include(a => a.Subject).Include(a => a.Tutor).Include(a => a.Payment).SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IList<Booking>> GetBookingByParent(int id)
        {
            return await _context.Bookings.Include(a => a.Parent).Include(a => a.Student).Include(a => a.Subject).Include(a => a.Tutor).Include(a => a.Payment).Where(a => a.BookingStatus == Enums.BookingStatus.Approved && a.IsDeleted == false && a.ParentId == id).ToListAsync();
        }

        public async Task<IList<Booking>> GetBookingByQualification(Expression<Func<Booking, bool>> expression)
        {
            return await _context.Bookings.Include(x => x.Tutor.Qualification).ToListAsync();
        }

        public async Task<IList<Booking>> GetBookingByTutor(int id)
        {
            return await _context.Bookings.Include(a => a.Parent).Include(a => a.Student).Include(a => a.Subject).Include(a => a.Tutor).Include(a => a.Payment).Where(a => a.BookingStatus == Enums.BookingStatus.Approved && a.IsDeleted == false && a.TutorId == id).ToListAsync();
        }

        public async Task<IList<Booking>> GetNotApprovedBooking()
        {
            return await _context.Bookings.Include(a => a.Parent).Include(a => a.Student).Include(a => a.Subject).Include(a => a.Tutor).Include(a => a.Payment).Where(a => a.BookingStatus == Enums.BookingStatus.Processing && a.IsDeleted == false).ToListAsync();
        }

        public async Task<IList<Booking>> GetNotApprovedBookingByParent(int id)
        {
            return await _context.Bookings.Include(a => a.Parent).Include(a => a.Student).Include(a => a.Subject).Include(a => a.Tutor).Include(a => a.Payment).Where(a => a.BookingStatus == Enums.BookingStatus.Processing && a.IsDeleted == false && a.ParentId == id).ToListAsync();
        }

        
    }
}
