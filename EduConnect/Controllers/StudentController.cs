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
    public class StudentController : Controller
    {
        private readonly IStudentService _StudentService;
        private readonly ISubjectService _subjectService;
        private readonly IParentService _parentService;

        public StudentController(IStudentService studentService, ISubjectService subjectService , IParentService parentService)
        {
            _StudentService = studentService;
            _subjectService = subjectService;
            _parentService = parentService;
        }

        public async Task<IActionResult> Index()
        {
            var Student = await _StudentService.GetAllStudent();
            if(Student.Status == false)
            {
                return Content("There is no record");
            }
            return View(Student.Data);
        }
        [Authorize]
        public async Task<IActionResult> IndexForParent()
        {
            var parent = await _parentService.GetparentById(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            var Student = await _StudentService.GetAllStudentForParent(parent.Data.Id);
            if (Student.Status == false)
            {
                return Content("There is no record");
            }
            return View(Student.Data);
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var subjects = await _subjectService.GetAllSubject();
            ViewData["Subjects"] = new SelectList(subjects.Data, "Id", "Name");
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(StudentRequestModel model)
        {
            var parent = await _parentService.GetparentById(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            model.ParentId = parent.Data.Id;
            var admin = await _StudentService.CreateStudent(model);
            ViewBag.Message = admin.Message;
            return RedirectToAction("Dashboard", "Parent");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var Student = await _StudentService.GetStudentById(id);
            if (Student == null)
            {
                return NotFound($"Student does not Exist");
            }
            return View(Student);
        }



        [HttpGet]
        public async Task<IActionResult> GetToBeDeletedStudent()
        {
            var role = await _StudentService.GetAllToBeApprovedDeletedStudent();
            if (role.Status == false)
            {
                return Content("There is no record");
            }
            return View(role.Data);
        }

        
        public async Task<IActionResult> Approve(int id)
        {
            var isDeleted = await _StudentService.DeleteStudent(id);

            return RedirectToAction("Index");
        }



        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var subjects = await _subjectService.GetAllSubject();
            ViewData["Subjects"] = new SelectList(subjects.Data, "Id", "Name");
            var role = await _StudentService.GetStudentById(id);
            if (role == null)
            {
                return ViewBag.Message("Student not found");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, StudentUpdateModel model)
        {
            var role = await _StudentService.UpdateStudent(model, id);
            ViewBag.Message = role.Message;
            return RedirectToAction("IndexForParent", "Student");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _StudentService.GetStudentById(id);
            if (role == null)
            {
                return ViewBag.Message("Tutor not found");
            }
            return View(role.Data);
          
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isDeleted = await _StudentService.DeleteStudent(id);
            if (isDeleted.Status == false)
            {
                return RedirectToAction("Delete");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteForParent(int id)
        {
            var role = await _StudentService.GetStudentById(id);
            if (role == null)
            {
                return ViewBag.Message("Tutor not found");
            }
            return View(role.Data);

        }
        [HttpPost, ActionName("DeleteForParent")]
        public async Task<IActionResult> DeleteConfirmedForParent(int id)
        {
            var isDeleted = await _StudentService.DeleteStudentForParent(id);
            if (isDeleted.Status == false)
            {
                return RedirectToAction("Delete");
            }
            return RedirectToAction("IndexForParent");
        }
    }
}
