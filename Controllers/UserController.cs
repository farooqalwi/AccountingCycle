using AccountingCycle.DataBaseContext;
using AccountingCycle.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AccountingCycle.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly AccountingCycleContext _context;

        public UserController(ILogger<UserController> logger, AccountingCycleContext context)
        {
            _logger = logger;
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.Name != null)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View();
        }

        [AllowAnonymous]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.Where(x => x.Email == model.Email && x.Password == model.Password).FirstOrDefault();

                if (user != null)
                {
                    var claims = new List<Claim>()
                    {
                         new Claim(ClaimTypes.Name, user.Email),
                         new Claim("UserName", user.UserName),
                         new Claim("UserID", user.Id.ToString())
                    };

                    var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                    {
                        IsPersistent = true
                    });

                    return RedirectToAction("Index", "Dashboard");
                }

            }


            return View();
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
               var userCount = _context.Users.Where(x => x.Email == model.Email).Count();
                if (userCount != 0)
                {
                    ViewBag.Message = "User already registered. Try with a new one.";
                    return View();
                }

                var user = new Users();
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.Password = model.ConfirmPassword;

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                ModelState.Clear();

                return RedirectToAction("Login", "User");

            }
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();

                user.Password = model.ConfirmNewPassword;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Dashboard");

            }

            return View();

        }

        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "User");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
