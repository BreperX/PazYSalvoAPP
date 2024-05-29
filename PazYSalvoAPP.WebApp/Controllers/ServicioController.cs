using Microsoft.AspNetCore.Mvc;

namespace PazYSalvoAPP.WebApp.Controllers
{
    public class ServicioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
