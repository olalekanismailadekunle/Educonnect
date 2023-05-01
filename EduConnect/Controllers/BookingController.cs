using EduConnect.DTOs;
using EduConnect.Entities;
using EduConnect.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EduConnect.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _BookingService;
        private readonly ITutorService _tutorService;
        private readonly IParentService _parentService;
        private readonly IEmailSender _emailSender;

        public BookingController(IBookingService BookingService , ITutorService tutorService, IParentService parentService, IEmailSender emailSender)
        {
            _BookingService = BookingService;
            _tutorService = tutorService;
            _parentService = parentService;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> Index()
        {
            var Booking = await _BookingService.GetAllBooking();
            if(Booking.Status == false)
            {
                return Content("There is no record");
            }
            return View(Booking);
        }
        [Authorize]
        public async Task<IActionResult> IndexForParent()
        {
            var parent = await _parentService.GetparentById(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            var Booking = await _BookingService.GetBookingByParent(parent.Data.Id);
            if (Booking.Status == false)
            {
                return Content("There is no record");
            }
            return View(Booking);
        }
        [Authorize]
        public async Task<IActionResult> GetNotApprovedBookingForParent()
        {
            var parent = await _parentService.GetparentById(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            var Booking = await _BookingService.GetNotApprovedBookingByParent(parent.Data.Id);
            if (Booking.Status == false)
            {
                return Content("There is no record");
            }
            return View(Booking);
        }

        [Authorize]
        public async Task<IActionResult> GetNotApprovedBooking()
        {
            var Booking = await _BookingService.GetNotApprovedBooking();
            if (Booking.Status == false)
            {
                return Content("There is no record");
            }
            return View(Booking);
        }
        [Authorize]
        public async Task<IActionResult> IndexForTutor()
        {
            var tutor = await _tutorService.GetTutorById(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            var Booking = await _BookingService.GetBookingByParent(tutor.Data.Id);
            if (Booking.Status == false)
            {
                return Content("There is no record");
            }
            return View(Booking);
        }



        [HttpGet]
        public async Task<IActionResult> Create(string id)
        {
            var studentId = int.Parse(id.Split(',')[1]);
            var tutorId = int.Parse(id.Split(',')[0]);
            var tutor = await _tutorService.GetTutorByIdForAdmin(tutorId);
            if(tutor.Data.Subjects.Count != 0)
            {
                ViewData["Subjects"] = new SelectList(tutor.Data.Subjects, "Id", "Name");
            }
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(BookingRequestModel model )
        {
            var parent = await _parentService.GetparentById( int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            model.ParentId = parent.Data.Id;

            var admin = await _BookingService.CreateBooking(model);
            ViewBag.Message = admin.Message;
            return RedirectToAction("Dashboard" , "Parent");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var Booking = await _BookingService.GetBookingById(id);
            if (Booking == null)
            {
                return NotFound($"Booking does not Exist");
            }
            return View(Booking);
        }

        
        [HttpGet]
        public async Task<IActionResult> DetailsTutor(int id)
        {
            var Booking = await _BookingService.GetBookingById(id);
            if (Booking == null)
            {
                return NotFound($"Booking does not Exist");
            }
            return View(Booking);
        }

        [HttpGet]
        public async Task<IActionResult> DetailsParent(int id)
        {
            var Booking = await _BookingService.GetBookingById(id);
            if (Booking == null)
            {
                return NotFound($"Booking does not Exist");
            }
            return View(Booking);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var Booking = await _BookingService.UpdateBooking(id);

            if (Booking.Data == null)
            {
                return NotFound($"Booking does not Exist");
            }
            var tutor = await _tutorService.GetTutorByIdForAdmin(Booking.Data.TutorId);
            var parent = await _parentService.GetparentByIdForAdmin(Booking.Data.ParentId);
            var email = new EmailDto
            {
                Message = "testing",
                RecieverEmail = parent.Data.MailAddress,
                RecieverName = $"{parent.Data.FirstName} {parent.Data.LastName}",
                Subject = "registration"

            };
            var email2 = new EmailDto
            {
                Message = "testing",
                RecieverEmail = tutor.Data.MailAddress,
                RecieverName = $"{tutor.Data.FirstName} {tutor.Data.LastName}",
                Subject = "registration"

            };
            await _emailSender.SendEmail(email);
            await _emailSender.SendEmail(email2);
            return RedirectToAction("Index");
        }


        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id, Booking Booking)
        {
            var isDeleted = await _BookingService.DeleteBooking(id);
            if (isDeleted.Status == false)
            {
                return RedirectToAction("Delete");
            }
            return RedirectToAction("Index");
        }
    }
}
