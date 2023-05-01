using EduConnect.DTOs;
using EduConnect.Entities;
using EduConnect.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EduConnect.Controllers
{
    public class ParentController : Controller
    {
        private readonly IParentService _ParentService;
        private readonly IEmailSender _emailSender;

        public ParentController(IParentService ParentService, IEmailSender emailSender)
        {
            _ParentService = ParentService;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> Index()
        {
            var Parent = await _ParentService.GetAllParent();
            if(Parent.Status == false)
            {
                return Content("There is no record");
            }
            return View(Parent);
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ParentRequestModel model)
        {
            
                var admin = await _ParentService.CreateParent(model);

                var email = new EmailDto
                {
                    Message = "Your have successfully registered on Educonnect platform",
                    RecieverEmail = model.MailAddress,
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
            var Parent = await _ParentService.GetparentByIdForAdmin(id);
            if (Parent == null)
            {
                return NotFound($"Parent does not Exist");
            }
            return View(Parent);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> DetailsByIndividual()
        {
            var Parent = await _ParentService.GetparentById(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            if (Parent == null)
            {
                return NotFound($"Parent does not Exist");
            }
            return View(Parent);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var role = await _ParentService.GetparentById(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            if (role == null)
            {
                return ViewBag.Message("Parent not found");
            }
            return View(role.Data);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Update(int id, ParentUpdateModel model)
        {
            var role = await _ParentService.UpdateParent(model, int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            ViewBag.Message = role.Message;
            return RedirectToAction("SignIn", "User");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var parent = await _ParentService.GetparentByIdForAdmin(id);
            if (parent == null)
            {
                return ViewBag.Message("Parent not found");
            }
           
            return View(parent.Data);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isDeleted = await _ParentService.DeleteParent(id);
            if (isDeleted.Status == false)
            {
                return RedirectToAction("Delete");
            }
            return RedirectToAction("Index");
        }
    }
}
