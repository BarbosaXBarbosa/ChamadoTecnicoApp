using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChamadoTecnicoWebApp.Models;
using ChamadoTecnicoAppData.Dto;
using ChamadoTecnicoAppData.Dao;


namespace ChamadoTecnicoWebApp.Controllers
{
    public class UsuarioController : Controller
    {
        // Atributos
        // Objetos do banco de dados: Usuario
        Usuario usuarioDto;
        UsuarioDao usuarioDao;

        [HttpGet]
        public IActionResult Login()
        {
            //Instancia o model com os dados 
            LoginViewModel loginVM = new LoginViewModel();
            //Envia o model para a view(tela)
            return View(loginVM);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginVM)
        {
            //Validação dos dados informados
            if (ModelState.IsValid)
            {
                //Instancia DTO
                usuarioDto = new Usuario();
                //Instancia DAO
                usuarioDao = new UsuarioDao();

                //1 - Verifica se o cliente já está cadastro
                usuarioDto = usuarioDao.ObtemUsuario(loginVM.Email, loginVM.Senha);
                if (usuarioDto == null)
                {
                    //Adiociona uma mensagem de erro
                    ModelState.AddModelError("Email", "Email não encontrado");
                    ModelState.AddModelError("Senha", "Senha inválida");
                    return View();
                }
                //Redireciona o usuario para a página inicial
                return RedirectToAction("Index", "Home");
            }
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

        [HttpPost]
        public IActionResult Cadastro(UsuarioViewModel usuarioVM)
        {
            //Validação dos dados informados
            if (ModelState.IsValid)
            {
                //Instancia DTO
                usuarioDto = new Usuario();
                //Instancia DAO
                usuarioDao = new UsuarioDao();

                //1 - Verifica se o usuario já esta cadastrado
                usuarioDto = usuarioDao.ObtemUsuario(usuarioVM.Email);
                if (usuarioDto != null)
                {
                    //Adiociona uma mensagem de erro
                    ModelState.AddModelError("Email", "Usuário com esse e-mail já cadastrado");
                    return View(usuarioVM);
                }

                //2 - Faz o preenchimento os dados do usuario DTO
                usuarioDto.CodigoUsuario = usuarioVM.CodigoUsuario;
                usuarioDto.Email = usuarioVM.Email;
                usuarioDto.Senha = usuarioVM.Senha;
                usuarioDto.Perfil = Perfis.Cliente.ToString();

                //3 - Realiza o cadastro do usuario cliente no banco de dados
                usuarioDao.IncluiUsuario(usuarioDto);

                //4 - Envia o usuario cadastrado para fazer o Login
                return RedirectToAction("Login");
            }
            //Caso tenha algum erro retorna para tela de cadastro preenchendo com os dados
            return View(usuarioVM);
        }

    }
}
