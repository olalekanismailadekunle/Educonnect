using EduConnect.DTOs;
using System.Threading.Tasks;

namespace EduConnect.Interfaces.IServices
{
    public interface IBookingService
    {
        Task<BookingResponseModel> CreateBooking(BookingRequestModel requestModel);
        Task<BookingResponseModel> GetBookingById(int id);
       Task<BookingsResponseModel> GetBookingByParent(int id);
        Task<BookingsResponseModel> GetNotApprovedBookingByParent(int id);
        Task<BookingsResponseModel> GetBookingByTutor(int id);
        Task<BookingResponseModel> DeleteBooking(int Id);
        Task<BookingResponseModel> UpdateBooking(int Id);
        Task<BookingsResponseModel> GetAllBooking();
        Task<BookingsResponseModel> GetNotApprovedBooking();
    }
}
