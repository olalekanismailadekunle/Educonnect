using EduConnect.DTOs;
using EduConnect.Entities;
using EduConnect.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace EduConnect.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ISubjectService _SubjectService;

        public SubjectController(ISubjectService SubjectService)
        {
            _SubjectService = SubjectService;
        }

        public async Task<IActionResult> Index()
        {
            var Subject = await _SubjectService.GetAllSubject();
            if(Subject.Status == false)
            {
                return Content("There is no record");
            }
            return View(Subject.Data);
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
        public async Task<IActionResult> Create(SubjectRequestModel model)
        {
            var subject = await _SubjectService.CreateSubject(model);
          
            if (subject.Status == false)
            {
                ViewBag.Message = subject.Message;
                return View();
            }
            return RedirectToAction("DashBoard", "Administrator");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var Subject = await _SubjectService.GetSubjectById(id);
            if (Subject == null)
            {
                return NotFound($"Subject does not Exist");
            }
            return View(Subject);
        }
       
        
        public async Task<IActionResult> Update(int id)
        {
            var subject = await _SubjectService.GetSubjectById(id);
            if (subject == null)
            {
                ViewBag.Message("Subject not found");
                return View();
            }
          
            return View(subject.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, SubjectUpdateModel model)
        {
            var subject = await _SubjectService.UpdateSubject(model, id);
            if (subject.Status == false)
            {
                ViewBag.Message = subject.Message;
                return View();
            }
            return RedirectToAction("Index", "Subject");
        }
        public async Task<IActionResult> Delete(int id)
        {

            var get =await _SubjectService.GetSubjectById(id);

            if(get.Status == false)
            {

            }
            return View(get.Data);
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var isDeleted = await _SubjectService.DeleteSubject(id);
            if (isDeleted.Status == false)
            {
                return RedirectToAction("Delete");
            }
            return RedirectToAction("Index");
        }
    }
}
