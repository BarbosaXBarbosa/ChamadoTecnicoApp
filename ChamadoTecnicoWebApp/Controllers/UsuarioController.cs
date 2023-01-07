using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChamadoTecnicoWebApp.Models;
using ChamadoTecnicoAppData.Dto;
using ChamadoTecnicoAppData.Dao;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace ChamadoTecnicoWebApp.Controllers
{
    public class UsuarioController : Controller
    {
        //Atributos
        //Objetos do banco de dados: Usuario
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
                //Obtem o usuario pelo email e senha
                usuarioDto = usuarioDao.ObtemUsuario(loginVM.Email, loginVM.Senha);
                //Verifica se o usuario é valido
                if (usuarioDto == null)
                {
                    ModelState.AddModelError("Email", "E-mail informado inválido!");
                    ModelState.AddModelError("Senha", "Senha informada inválida!");
                    return View();
                }
                //Autenticacao do usuario no servidor
                //Credencial
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuarioDto.CodigoUsuario.ToString()),
                    new Claim(ClaimTypes.Email, usuarioDto.Email),
                    new Claim(ClaimTypes.Role, usuarioDto.Perfil)
                };

                //Identidade da credencial
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                //Autentica a identidade do usuario
                AuthenticationProperties authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true, //sempre que fazer o login gera um cookie novo
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(30), //tempo de vida do cookie
                    IssuedUtc = DateTime.UtcNow, //horario de criacao do cookie
                    RedirectUri = @"~/home" //apos desabilitar vai para essa rota
                };
                ClaimsPrincipal contaPrincipal = new ClaimsPrincipal(claimsIdentity);
                //Realiza o acesso do usuario autenticando pelo cookie
                HttpContext.SignInAsync(contaPrincipal, authProperties);

                //Redireciona o usuario para pagina inicial
                return RedirectToAction("Index", "Home", new { area = "App" });
            }

            //Envia o model para a view(tela)
            return View(loginVM);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
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
                //Instancia DTO
                usuarioDto = new Usuario();
                usuarioDto.CodigoUsuario = usuarioVM.CodigoUsuario;
                usuarioDto.Email = usuarioVM.Email;
                usuarioDto.Senha = usuarioVM.Senha;
                usuarioDto.Perfil = Perfis.Cliente.ToString();

                //3 - Realiza o cadastro do usuario cliente no banco de dados
                var codigoUsuario = usuarioDao.IncluiUsuario(usuarioDto);

                //3.1 - Realiza o cadastro do cliente
                if (codigoUsuario > 0) //Usuario foi cadastrado
                {
                    //Faz o cadastro do cliente
                    Cliente clienteDto = new Cliente();
                    clienteDto.CodigoUsuario = codigoUsuario;
                    clienteDto.Nome = usuarioVM.Nome;
                    clienteDto.Profissao = "";
                    clienteDto.Setor = "";

                    //Realiza a inclusão do cliente no banco de dados
                    ClienteDao clienteDao = new ClienteDao();
                    clienteDao.IncluiCliente(clienteDto);
                }

                //4 - Envia o usuario cadastrado para fazer o Login
                return RedirectToAction("Login");
            }
            //Caso tenha algum erro retorna para tela de cadastro preenchendo com os dados
            return View(usuarioVM);
        }
    }
}
