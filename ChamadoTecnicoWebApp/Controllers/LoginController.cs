using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChamadoTecnicoWebApp.Models;

namespace ChamadoTecnicoWebApp.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            //Instancia o model com os dados 
            LoginViewModel loginVM = new LoginViewModel();
            //Envia o model para a view(tela)
            return View(loginVM);
        }


    }
}
