using FeesTrackingApplication.Data;
using FeesTrackingApplication.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FeesTrackingApplication.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Login()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                // Optional: Log error ex.Message
                ViewBag.error = "Something went wrong while loading the login page.";
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(loginModel);
                }
                var user = await _context.Students
                     .AsNoTracking().FirstOrDefaultAsync(u => u.UserName == loginModel.UserName && u.Password == loginModel.Password && u.IsActive);

                if (user == null)
                {
                    ViewBag.error = "Invalid Username or Password..!";
                    return View();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("PRN", user.PRN_Number)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                // Optional: Log error ex.Message
                ViewBag.error = "An error occurred during login. Please try again later.";
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                // Optional: Log error ex.Message
                ViewBag.error = "Error occurred while logging out.";
                return RedirectToAction("Login");
            }
        }

    }
}
