using EduConnect.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EduConnect.Controllers
{
    public class SearchController : Controller
    {
        private readonly ITutorService _tutorService;
     
        public SearchController(ITutorService tutorService)
        {
            _tutorService = tutorService;
        }
        [HttpGet]
        public IActionResult SearchByLocalGovernment()
        {
            return View();
        }
        [HttpGet]

        [HttpGet]
        public IActionResult SearchBySubject()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SearchByState()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SearchByQualification()
        {
            if(_tutorService.GetEnumQualification().Count == 0)
            {
                return Content("There is no record");
            }
            
            return View(_tutorService.GetEnumQualification());
        }
        [HttpGet]
        public IActionResult SearchByStatus()
        {
            if (_tutorService.GetEnumByStatus().Count == 0)
            {
                return Content("There is no record");
            }

            return View(_tutorService.GetEnumByStatus());
        }

        [HttpGet]
        public IActionResult SearchBySpecialization()
        {
            if (_tutorService.GetEnumBySpecialization().Count == 0)
            {
                return Content("There is no record");
            }

            return View(_tutorService.GetEnumBySpecialization());
        }
    }
}
