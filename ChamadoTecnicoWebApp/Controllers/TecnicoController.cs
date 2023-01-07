using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChamadoTecnicoWebApp.Models;

namespace ChamadoTecnicoWebApp.Controllers
{
    public class TecnicoController : Controller
    {
        [HttpGet]
        public IActionResult Cadastro()
        {
            //Instancia o model com os dados 
            TecnicoViewModel tecnicoVM = new TecnicoViewModel();
            //Envia o model para a view(tela)
            return View(tecnicoVM);
        }

        [HttpPost]
        public IActionResult tecnico(TecnicoViewModel tecnicoVM)
        {
            return View();
        }
    }
}
