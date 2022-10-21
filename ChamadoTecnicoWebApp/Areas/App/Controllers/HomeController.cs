using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
