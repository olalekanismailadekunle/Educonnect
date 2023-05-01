using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
