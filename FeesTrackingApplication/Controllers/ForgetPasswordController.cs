using Microsoft.AspNetCore.Mvc;

namespace FeesTrackingApplication.Controllers
{
    public class ForgetPasswordController : Controller
    {
        public IActionResult Index()
        {
            return View("ForgetPassword");
        }
    }
}
