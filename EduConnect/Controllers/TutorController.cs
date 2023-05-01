using EduConnect.DTOs;
using EduConnect.Entities;
using EduConnect.Enums;
using EduConnect.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EduConnect.Controllers
{
    public class TutorController : Controller
    {
        private readonly ITutorService _TutorService;
        private readonly ISubjectService _subjectService;
        private readonly IUserService _userService;
        private readonly IEmailSender _emailSender;

        public TutorController(ITutorService tutorService, ISubjectService subjectService , IUserService userService, IEmailSender emailSender)
        {
            _TutorService = tutorService;
            _subjectService = subjectService;
            _userService = userService;
            _emailSender = emailSender;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var Tutor = await _TutorService.GetAllTutor();
            if(Tutor.Status == false)
            {
                return Content("There is no record");
            }
            return View(Tutor.Data);
        }

        public IActionResult Dashboard()
        {
           
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            //var states = await _TutorService.GetAllState();
            //ViewData["States"] = new SelectList(states.Data, "Name", "Name");
            //var localGovrnment = await _TutorService.GetAllLocalGovernment();
            //ViewData["LocalGovernmennts"] = new SelectList(localGovrnment.Data, "Name", "Name");
            var subjects = await _subjectService.GetAllSubject();
            ViewData["Subjects"] = new SelectList(subjects.Data, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TutorRequestModel model)
        {
            var admin = await _TutorService.CreateTutor(model);

            var email = new EmailDto
            {
                Message = "testing",
                RecieverEmail = model.MailAddress,
                RecieverName = $"{model.FirstName} {model.LastName}",
                Subject = "registration"

            };
            _emailSender.SendEmail(email);
            ViewBag.Message = admin.Message;
            return RedirectToAction("SignIn", "User");
        }
        
            
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Details()
        {

            var Tutor = await _TutorService.GetTutorById(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            if (Tutor == null)
            {
                return NotFound($"Tutor does not Exist");
            }
            return View(Tutor.Data);
        }
        [HttpGet]
        public async Task<IActionResult> DetailsByAdmin(int id)
        {

            var Tutor = await _TutorService.GetTutorByIdForAdmin(id);
            if (Tutor == null)
            {
                return NotFound($"Tutor does not Exist");
            }
            return View(Tutor.Data);
        }


        [HttpGet]
        public async Task<IActionResult> GetTutorByQualification(Qualification text)
        {
            var Tutor = await _TutorService.GetTutorByQualification(text);
            if (Tutor.Status == false)
            {
                return Content("There is no record");
            }
            return View(Tutor.Data);
        }
        [HttpGet]
        public async Task<IActionResult> GetTutorBySpecialization(Specialization text)
        {
            var Tutor =  await _TutorService.GetTutorBySpecialization(text);
          
             if (Tutor.Status == false)
            {
                return Content("There is no record");
            }
            return View(Tutor.Data);
        }
        [HttpGet]
        public async Task<IActionResult> GetTutorByStatus(Status text)
        {
            var Tutor =  await _TutorService.GetTutorByStatus(text);
            if (Tutor.Status == false)
            {
                return Content("There is no record");
            }
            return View(Tutor.Data);
        }
        [HttpGet]
        public async Task<IActionResult> GetTutorAccordingToStudent(int id)
        {
            var Tutor = await _TutorService.GetTutorsAccordingToStudent(id);
        
            return View(Tutor.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetTutorByLocalGovernment( string name)
        {
            var localGovernments = await _TutorService.GetTutorsByLocalGovernment(name);
            if (localGovernments.Status == false)
            {
                return Content("There is no record");
            }
            return View(localGovernments.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetTutorByState(string name)
        {
            var localGovernments = await _TutorService.GetTutorsByState(name);
            if (localGovernments.Status == false)
            {
                return Content("There is no record");
            }
            return View(localGovernments.Data);
        }



        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var subjects = await _subjectService.GetAllSubject();
            ViewData["Subjects"] = new SelectList(subjects.Data, "Id", "Name");
            var role = await _TutorService.GetTutorById(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            if (role == null)
            {
                return ViewBag.Message("Tutor not found");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, TutorUpdateModel model)
        {
            var role = await _TutorService.UpdateTutor(model, int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            ViewBag.Message = role.Message;
            return RedirectToAction("DashBoard", "Tutor");
        }
    
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _TutorService.GetTutorByIdForAdmin(id);
            if (role == null)
            {
                return ViewBag.Message("Tutor not found");
            }
            return View(role.Data);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var isDeleted = await _TutorService.DeleteTutor(id);
            if (isDeleted.Status == false)
            {
                return RedirectToAction("Delete");
            }
            return RedirectToAction("Index");
        }
    }
}
