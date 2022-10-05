using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChamadoTecnicoWebApp.Models;

namespace ChamadoTecnicoWebApp.Controllers
{
    public class ClienteController : Controller
    {
        [HttpGet]
        public IActionResult Cadastro()
        {
            //Instancia o model com os dados 
            ClienteViewModel clienteVM = new ClienteViewModel();
            //Envia o model para a view(tela)
            return View(clienteVM);
        }

        [HttpPost]
        public IActionResult cliente(ClienteViewModel clienteVM)
        {
            return View();
        }
    }
}
