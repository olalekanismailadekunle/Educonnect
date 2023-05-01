using EduConnect.DTOs;
using EduConnect.Identity;
using EduConnect.Interfaces.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EduConnect.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetAll();
            if(user.Status == false)
            {
                return Content("There is no record");
            }
            return View(user);
        }

        public IActionResult Dashboard()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            var user = _userService.GetUser(id);
            if (user == null)
            {
                return NotFound($"User does not Exist");
            }
            return View(user);
        }

        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id, User user)
        {
            var isDeleted = await _userService.DeleteUser(id);
            if (isDeleted.Status == false)
            {
                return RedirectToAction("Delete");
            }
            return RedirectToAction("Index");
        }
        public IActionResult SignIn()
        {
            return View();
        }

      
        [HttpPost]
        public async Task<IActionResult> SignIn(LoginRequest model)
        {
            var ro = "";
            var user = await _userService.Login(model);
            if (user.Status == true)
            {
                var claims = new List<Claim>

                {
                   new Claim(ClaimTypes.NameIdentifier, user.Data.Id.ToString()),
                   new Claim(ClaimTypes.Name, $"{user.Data.UserName}"),
                   new Claim(ClaimTypes.Email,$"{user.Data.Email}"),
                   //new Claim("photo", user.Data.Customer.CustomerPhoto),
                };
                foreach (var role in user.Data.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    ro = role.Name;
                }
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authenticationProperties = new AuthenticationProperties();
                var principal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);

                if (ro == "Administrator")
                {
                    TempData["success"] = user.Message;
                    return RedirectToAction("DashBoardIndividual", "Administrator");
                }

                else if (ro == "SuperAdmin")
                {
                    TempData["success"] = user.Message;
                    return RedirectToAction("DashBoard", "Administrator");
                }
                else if (ro == "Parent")
                {
                    TempData["success"] = user.Message;
                    return RedirectToAction("DashBoard", "Parent");
                }
                else if (ro == "Tutor")
                {
                    TempData["success"] = user.Message;
                    return RedirectToAction("DashBoard", "Tutor");
                }
            }

            // ViewBag.error = "Invalid username or password";

            ViewData["success"] = user.Message;
            return View();
        }

        public async Task<IActionResult> SigningOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("SignIn");
        }
        [Authorize]
        public async Task<IActionResult> RedirectToDashBoard()
        {
           var loggedInUserRole = User.FindFirst(ClaimTypes.Role).Value;
          
       
                if (loggedInUserRole == "Administrator")
                {
                   
                    return RedirectToAction("DashBoardIndividual", "Administrator");
                }

                else if (loggedInUserRole == "SuperAdmin")
                {

                    return RedirectToAction("DashBoard", "Administrator");
                }
                else if (loggedInUserRole == "Parent")
                {
                    
                    return RedirectToAction("DashBoard", "Parent");
                }
                else if (loggedInUserRole == "Tutor")
                {
                    
                    return RedirectToAction("DashBoard", "Tutor");
                }
            return NotFound();
            
       
        }
    }
}

