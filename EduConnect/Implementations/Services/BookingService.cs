using EduConnect.DTOs;
using EduConnect.Entities;
using EduConnect.Implementations.Repositories;
using EduConnect.Interfaces.IRepositories;
using EduConnect.Interfaces.IServices;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EduConnect.Implementations.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ITutorRepository _tutorRepository;

        public BookingService(IBookingRepository bookingRepository, ITutorRepository tutorRepository)
        {
            _bookingRepository = bookingRepository;
            _tutorRepository = tutorRepository;
        }

        public async Task<BookingResponseModel> CreateBooking(BookingRequestModel requestModel)
        {
        
            var booking = new Booking
            {
                BookingDate = DateTime.UtcNow,
                OrderReference = Guid.NewGuid(),
                BookingHour = requestModel.BookingHour,
                IsDeleted = false,
                ParentId = requestModel.ParentId,
                StudentId = requestModel.StudentId,
                TutorId = requestModel.TutorId,
                SubjectId = requestModel.SubjectId,
                BookingStatus = Enums.BookingStatus.Processing,
               
                PaymentCategory = requestModel.PaymentCategory
            };
            await _bookingRepository.Create(booking);
            return new BookingResponseModel
            {
                Data = new BookingDto
                {
                    BookingDate = booking.BookingDate,
                   // ParentName = booking.Parent.FirstName,
                   // StudentName = booking.Student.FirstName,
                  //  TutorName = booking.Tutor.FirstName,
                    OrderReference = booking.OrderReference,
                    
                    
                },
                Status = true,
                Message = "Booking Successfully Created"
            };

        }

        public async Task<BookingResponseModel> DeleteBooking(int Id)
        {
            var booking = await _bookingRepository.Get(x => x.Id == Id);
            if (booking == null)
            {
                return new BookingResponseModel
                {
                    Message = "Tutor not found",
                    Status = false
                };
            }
            booking.IsDeleted = true;
            await _bookingRepository.Update(booking);
            return new BookingResponseModel
            {
                Status = true,
                Message = "Tutor Successfully deleted",
            };
        }

        public async Task<BookingsResponseModel> GetAllBooking()
        {
            var getbooking = await _bookingRepository.GetAllBooking();
            if (getbooking.Count == 0)
            {
                return new BookingsResponseModel
                {
                    Status = false,
                    Message = "Booking List is empty"
                };
            }
            return new BookingsResponseModel
            { 
                Data = getbooking.Select(booking => new BookingDto
                {
                    Id = booking.Id,
                    BookingDate = booking.BookingDate,
                    ParentName = booking.Parent.FirstName,
                    StudentName = booking.Student.FirstName,
                    TutorName = booking.Tutor.FirstName,
                    OrderReference = booking.OrderReference,
                    //Payments = booking.Payments.Select(book => new PaymentDto
                    //{
                    //    Amount = booking.Payment.Amount,
                    //    PaymentStatus = booking.Payment.PaymentStatus,
                    //    ReferenceNumber = booking.Payment.ReferenceNumber
                    //}).ToList(),
                    
                }).ToList(),
                Status = true,
                Message = "Bookings Successfully Retrieved"
            };
        }

        public async Task<BookingResponseModel> GetBookingById(int id)
        {
            var booking = await _bookingRepository.GetBookingById(id);
            if (booking == null)
            {
                return new BookingResponseModel
                {
                    Status = false,
                    Message = "Tutor Not Found"
                };
            }
            return new BookingResponseModel
            {
                Data = new BookingDto
                {
                    Id = booking.Id,
                    BookingDate = booking.BookingDate,
                    ParentName = booking.Parent.FirstName,
                    StudentName = booking.Student.FirstName,
                    TutorName = booking.Tutor.FirstName,
                    OrderReference = booking.OrderReference,
                    ParentId = booking.ParentId,
                    TutorId = booking.TutorId
                    //Payments = booking.Payments.Select(book => new PaymentDto
                    //{
                    //    Amount = booking.Payment.Amount,
                    //    PaymentStatus = booking.Payment.PaymentStatus,
                    //    ReferenceNumber = booking.Payment.ReferenceNumber
                    //}).ToList(),
         
                },
                Status = true,
                Message = "Booking Successfully Retrieved"
            };
        }

        public async Task<BookingsResponseModel> GetBookingByParent(int id)
        {
            var booking = await _bookingRepository.GetBookingByParent(id);
            if (booking.Count == 0)
            {
                return new BookingsResponseModel
                {
                    Status = false,
                    Message = "Tutor Not Found"
                };
            }
            return new BookingsResponseModel
            {
                Data = booking.Select(a => new BookingDto
                {
                    Id = a.Id,
                    BookingDate = a.BookingDate,
                    ParentName = a.Parent.FirstName,
                    StudentName = a.Student.FirstName,
                    TutorName = a.Tutor.FirstName,
                    OrderReference = a.OrderReference,
                    Payments = a.Payments.Select(book => new PaymentDto
                    {
                        Amount = a.Payment.Amount,
                        PaymentStatus = a.Payment.PaymentStatus,
                        ReferenceNumber = a.Payment.ReferenceNumber
                    }).ToList(),
                   
                }).ToList(),
               
                Status = true,
                Message = "Booking Successfully Retrieved"
            };
        }

        public async Task<BookingsResponseModel> GetBookingByTutor(int id)
        {
            var booking = await _bookingRepository.GetBookingByTutor(id);
            if (booking.Count == 0)
            {
                return new BookingsResponseModel
                {
                    Status = false,
                    Message = "Tutor Not Found"
                };
            }
            return new BookingsResponseModel
            {
                Data = booking.Select(a => new BookingDto
                {
                    BookingDate = a.BookingDate,
                    ParentName = a.Parent.FirstName,
                    StudentName = a.Student.FirstName,
                    TutorName = a.Tutor.FirstName,
                    OrderReference = a.OrderReference,
                    Payments = a.Payments.Select(book => new PaymentDto
                    {
                        Amount = a.Payment.Amount,
                        PaymentStatus = a.Payment.PaymentStatus,
                        ReferenceNumber = a.Payment.ReferenceNumber
                    }).ToList(),

                }).ToList(),

                Status = true,
                Message = "Booking Successfully Retrieved"
            };
        }

        public async Task<BookingsResponseModel> GetNotApprovedBooking()
        {
            var booking = await _bookingRepository.GetNotApprovedBooking();
            if (booking.Count == 0)
            {
                return new BookingsResponseModel
                {
                    Status = false,
                    Message = "Tutor Not Found"
                };
            }
            return new BookingsResponseModel
            {
                Data = booking.Select(a => new BookingDto
                {
                    Id = a.Id,
                    BookingDate = a.BookingDate,
                    ParentName = a.Parent.FirstName,
                    StudentName = a.Student.FirstName,
                    TutorName = a.Tutor.FirstName,
                    OrderReference = a.OrderReference,
                    Payments = a.Payments.Select(book => new PaymentDto
                    {
                        Amount = a.Payment.Amount,
                        PaymentStatus = a.Payment.PaymentStatus,
                        ReferenceNumber = a.Payment.ReferenceNumber
                    }).ToList(),
                 
                }).ToList(),

                Status = true,
                Message = "Booking Successfully Retrieved"
            };
        }

        public async Task<BookingsResponseModel> GetNotApprovedBookingByParent(int id)
        {
            var booking = await _bookingRepository.GetNotApprovedBookingByParent(id);
            if (booking.Count == 0)
            {
                return new BookingsResponseModel
                {
                    Status = false,
                    Message = "Tutor Not Found"
                };
            }
            return new BookingsResponseModel
            {
                Data = booking.Select(a => new BookingDto
                {
                    Id = a.Id,
                    BookingDate = a.BookingDate,
                    ParentName = a.Parent.FirstName,
                    StudentName = a.Student.FirstName,
                    TutorName = a.Tutor.FirstName,
                    OrderReference = a.OrderReference,
                    //Payments = a.Payments.Select(book => new PaymentDto
                    //{
                    //    Amount = a.Payment.Amount,
                    //    PaymentStatus = a.Payment.PaymentStatus,
                    //    ReferenceNumber = a.Payment.ReferenceNumber
                    //}).ToList(),
        
                }).ToList(),

                Status = true,
                Message = "Booking Successfully Retrieved"
            };
        }

        public async Task<BookingResponseModel> UpdateBooking(int id)
        {
            var booking = await _bookingRepository.GetBookingById(id);
            if(booking == null)
            {
                return new BookingResponseModel
                {
                    Message = "Booking not found",
                    Status = false
                };
            }
            booking.BookingStatus = Enums.BookingStatus.Approved;
            await _bookingRepository.Update(booking);
            return new BookingResponseModel
            {
                Data = new BookingDto
                {
                    Id = booking.Id,
                    BookingDate = booking.BookingDate,
                    ParentName = booking.Parent.FirstName,
                    StudentName = booking.Student.FirstName,
                    TutorName = booking.Tutor.FirstName,
                    OrderReference = booking.OrderReference,
                    ParentId = booking.ParentId,
                    TutorId = booking.TutorId,
                    Payments = booking.Payments.Select(book => new PaymentDto
                    {
                        Amount = booking.Payment.Amount,
                        PaymentStatus = booking.Payment.PaymentStatus,
                        ReferenceNumber = booking.Payment.ReferenceNumber
                    }).ToList(),
                    
                },
                Status = true,
                Message = "Booking Successfully Retrieved"
            };
        }
    }
}
