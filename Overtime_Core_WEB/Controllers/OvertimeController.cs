using Microsoft.AspNetCore.Mvc;

namespace Overtime_Core_WEB.Controllers
{
    public class OvertimeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
