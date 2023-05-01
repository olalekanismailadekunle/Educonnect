using EduConnect.DTOs;
using EduConnect.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EduConnect.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;

        }
        public async Task<IActionResult> Index()
        {
            var role = await _roleService.GetRoles();
            if(role.Status == false)
            {
                return Content("There is no record");
            }
            return View(role.Data);
        }
        //public async Task<IActionResult> Details(int id)
        //{
        //    var role = await _roleService.GetRoleById(id);
        //    if (role.Status == false)
        //    {
        //        return Content("There is no record");
        //    }
        //    return View(role.Data);
        // }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleRequest model)
        {
            var role = await _roleService.AddRole(model);
            ViewBag.Message = role.Message;
            return RedirectToAction("Dashboard", "Administrator");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var role = await _roleService.GetRoleById(id);
            return View(role.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _roleService.GetRoleById(id);
            if (role == null)
            {
                return ViewBag.Message("Role not found");
            }
            return View(role.Data);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var role = await _roleService.Delete(id);
            ViewBag.Message = role.Message;
            return RedirectToAction("Index", "Role");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var role = await _roleService.GetRoleById(id);
            if (role == null)
            {
                return ViewBag.Message("Role not found");
            }
            return View(role.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateRoleRequest model)
        {
            var role = await _roleService.UpdateRole(model, id);
            ViewBag.Message = role.Message;
            return RedirectToAction("GetAllRole", "Role");
        }
    }
}
    

