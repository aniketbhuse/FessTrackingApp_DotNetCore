using FeesTrackingApplication.Data;
using FeesTrackingApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FeesTrackingApplication.Controllers
{
    public class CreateAccountController : Controller
    {
        private readonly AppDbContext _context;

        public CreateAccountController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View("CreateAccount");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(Students students)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.error = "Please Enter all Field..!";
                    return View("CreateAccount", students);
                }

                var existingUser = await _context.Students.
                    FirstOrDefaultAsync(u => u.UserName == students.UserName  && u.PRN_Number == students.PRN_Number);

                if (existingUser != null)
                {
                    ViewBag.error = "Username already exists, please try another.";
                    return View("CreateAccount", students);

                }

                _context.Students.Add(students);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Account Created Successfully! Click OK to Login.";

                return RedirectToAction("Login", "Login");

            }
            catch (Exception ex) 
            {
                // Log ex.Message in real project
                ViewBag.error = "Something went wrong while creating the account. Try again!";
                return View("CreateAccount", students);

            }
        }

    }
}
