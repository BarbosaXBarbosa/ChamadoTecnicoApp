using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChamadoTecnicoWebApp.Models;

namespace ChamadoTecnicoWebApp.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            //Instancia o model com os dados 
            LoginViewModel loginVM = new LoginViewModel();
            //Envia o model para a view(tela)
            return View(loginVM);
        }

        [HttpGet]
        public IActionResult Cadastro()
        {
            //Instancia o model com os dados
            UsuarioViewModel usuarioVm = new UsuarioViewModel();
            return View(usuarioVm);
        }



    }
}
