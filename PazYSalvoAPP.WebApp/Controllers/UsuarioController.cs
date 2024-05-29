using Microsoft.AspNetCore.Mvc;

namespace PazYSalvoAPP.WebApp.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
