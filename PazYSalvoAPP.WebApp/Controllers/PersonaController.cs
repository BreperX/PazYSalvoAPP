using Microsoft.AspNetCore.Mvc;

namespace PazYSalvoAPP.WebApp.Controllers
{
    public class PersonaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
