using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NewsApp.Models;
using NewsApp.Repository;

namespace NewsApp.Controllers
{
    [Route("login")]
    public class LoginController : Controller
    {
        private IUserRepository _userRepository;

        public LoginController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                ClaimsIdentity identity = null;
                bool isAuthenticate = false;
                User user = _userRepository.Login(model);
                if (user != null)
                {
                    identity = new ClaimsIdentity(new[]
                     {
                    new Claim(ClaimTypes.Name,user.Name),
                    new Claim(ClaimTypes.Surname,user.Surname),
                    new Claim(ClaimTypes.Role,user.Role),
                    new Claim(ClaimTypes.Email , user.Email),
                    new Claim(ClaimTypes.MobilePhone, user.Mobil),
                }, "myCookie");
                    isAuthenticate = true;
                }
                if (isAuthenticate)
                {
                    var principal = new ClaimsPrincipal(identity);
                    var signIn = HttpContext.SignInAsync("myCookie", principal);
                    return RedirectToAction("Index", "Admin");
                }
                TempData["error"] = "Email or Password is wrong";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Email or Password dont be valid";
                return RedirectToAction("Index");
            }
        }
    }
}
