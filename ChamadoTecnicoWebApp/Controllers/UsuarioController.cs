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

                //Autenticação do Usuário no servidor
                //Credencial
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, usuarioDto.CodigoUsuario.ToString()),
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
                    IssuedUtc = DateTime.UtcNow, //Horario de criação de cookie
                    RedirectUri = @"~/home" //após desabilitar vai para essa rota
                };
                ClaimsPrincipal contaPrincipal = new ClaimsPrincipal(claimsIdentity);
                //Realiza o acesso do usuário autencicando pelo cookie
                HttpContext.SignInAsync(contaPrincipal, authProperties);

                //Redireciona o usuario para a página inicial
                return RedirectToAction("Index", "Home", new { area = "App"});
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
                usuarioDto = new Usuario();
                usuarioDto.CodigoUsuario = usuarioVM.CodigoUsuario;
                usuarioDto.Email = usuarioVM.Email;
                usuarioDto.Senha = usuarioVM.Senha;
                usuarioDto.Perfil = Perfis.Cliente.ToString();

                //3 - Realiza o cadastro do usuario cliente no banco de dados
                var codigoUsuario = usuarioDao.IncluiUsuario(usuarioDto);

                //4 - Realiza o cadastro do cliente
                if(codigoUsuario > 0) //Usuário foi cadastro
                {
                    //Cria o cliente
                    Cliente clienteDto = new Cliente();
                    clienteDto.CodigoUsuario = codigoUsuario;
                    clienteDto.Nome = usuarioVM.Nome;
                    clienteDto.Profissao = "";
                    clienteDto.Setor = "";

                    //Realoza a inclusão do cliente no banco de dados
                    ClienteDao clienteDao = new ClienteDao();   
                    clienteDao.IncluiCliente(clienteDto);
                    
                }

                //5 - Envia o usuario cadastrado para fazer o Login
                return RedirectToAction("Login");
            }
            //Caso tenha algum erro retorna para tela de cadastro preenchendo com os dados
            return View(usuarioVM);
        }

    }
}
