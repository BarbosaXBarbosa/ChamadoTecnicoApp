using Microsoft.AspNetCore.Mvc;

namespace ChamadoTecnicoWebApp.Areas.App.Controllers
{

    public class HomeController : AppController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
