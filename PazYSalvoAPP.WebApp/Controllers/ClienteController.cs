using Microsoft.AspNetCore.Mvc;

namespace PazYSalvoAPP.WebApp.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
