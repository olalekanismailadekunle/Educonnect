using EduConnect.DTOs;
using EduConnect.Entities;
using EduConnect.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EduConnect.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly IAdministratorService _administratorService;
        private readonly IUserService _userService;
        private readonly IEmailSender _emailSender;

        public AdministratorController(IAdministratorService administratorService , IUserService userService,IEmailSender emailService)
        {
            _administratorService = administratorService;
            _userService = userService;
            _emailSender = emailService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _administratorService.GetAllAdministrator();
            if(user.Status == false)
            {
                return Content("There is no record");
            }
            return View(user);
        }
        
        public IActionResult DashBoard()
        {
            return View();
        }
        public IActionResult DashBoardIndividual()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdministratorRequestModel model)
        {
            var admin = await _administratorService.CreateAdministrator(model);

            var email = new EmailDto
            {
                Message = "Your have successfully registered on Educonnect platform",
                RecieverEmail = model.Email,
                RecieverName = $"{model.FirstName} {model.LastName}",
                Subject = "Registration"

            };
            _emailSender.SendEmail(email);
            ViewBag.Message = admin.Message;
            return RedirectToAction("DashBoard", "Administrator");
        }

        [HttpGet]
        public IActionResult CreateSuperAdmin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSuperAdmin(AdministratorRequestModel model)
        {
            var admin = await _administratorService.CreateSuperAdmin(model);

            var email = new EmailDto
            {
                Message = "Your have successfully registered on Educonnect platform",
                RecieverEmail = model.Email,
                RecieverName = $"{model.FirstName} {model.LastName}",
                Subject = "Registration"

            };
            _emailSender.SendEmail(email);
            ViewBag.Message = admin.Message;
            return RedirectToAction("SignIn", "User");


        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {

            var user = await _administratorService.GetAdministratorByIdByAdmin(id);
            if (user.Status == false)
            {
                return NotFound($"Administrator does not Exist");
            }
            return View(user);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> DetailsByIndividual(int id)
        {
            
            var admin = await _administratorService.GetAdministratorById(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            if (admin == null)
            {
                return NotFound($"Administrator does not Exist");
            }
            return View(admin);
        }
        [Authorize]

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            
            var admin = await _administratorService.GetAdministratorById(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            if (admin == null)
            {
                return ViewBag.Message("Administrator not found");
            }
            return View(admin.Data);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Update( AdministratorUpdateModel model)
        {
            
            var admin = await _administratorService.GetAdministratorById(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            var role = await _administratorService.UpdateAdministrator( model , admin.Data.Id);
            ViewBag.Message = role.Message;
            return RedirectToAction("DashboardIndividual", "Administrator");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var get = await _administratorService.GetAdministratorByIdByAdmin(id);

            if (get.Status == false)
            {
                return NotFound();
            }
            return View(get.Data);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete1(int id)
        {
            var isDeleted = await _administratorService.DeleteAdministrator(id);
            if (isDeleted.Status == false)
            {
                return RedirectToAction("Delete");
            }
            return RedirectToAction("Index"); 
        }
    }
}
 