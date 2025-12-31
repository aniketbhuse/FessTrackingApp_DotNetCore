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
        public async Task<IActionResult> CretaeAccount(Students students)
        {
            try
            {
                if (!ModelState.IsValid)
                { 
                   return View(students);
                }

                var existingUser = await _context.Students.FirstOrDefaultAsync(u => u.UserName == students.UserName);

                if (existingUser != null)
                {
                    ViewBag.error = "Username already exists, please try another.";
                    return View(students);

                }

                _context.Students.Add(students);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login", "Login");

            }
            catch (Exception ex) 
            {
                // Log ex.Message in real project
                ViewBag.error = "Something went wrong while creating the account. Try again!";
                return View(students);

            }
        }

    }
}
