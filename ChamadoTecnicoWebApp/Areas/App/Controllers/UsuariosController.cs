using ChamadoTecnicoAppData.Dao;
using ChamadoTecnicoAppData.Dto;
using ChamadoTecnicoWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChamadoTecnicoWebApp.Areas.App.Controllers
{
    [Area("App")]
    public class UsuariosController : Controller
    {
        private UsuarioDao _usuarioDao;
        private ClienteDao _clienteDao;        
        
        [HttpGet]
        [Authorize(Roles = "Administrador,Tecnico")]
        public ActionResult Index()
        {
            //Instancia o acesso ao banco de dados
            _usuarioDao = new UsuarioDao();
            //Obtem a lista de usuarios do banco de dados
            var listaUsuarios = _usuarioDao.ListaUsuario();
            //Envia a lista de dados para View
            return View(listaUsuarios);
        }
        
        [HttpPost]
        [Authorize(Roles = "Administrador, Tecnico")]
        public ActionResult Index(string pesquisa)
        {
            //Instancia o acesso ao banco de dados
            _usuarioDao = new UsuarioDao();
            //Obtem a lista de usuarios do banco de dados
            var listaUsuarios = _usuarioDao.ListaUsuario(pesquisa);
            //Envia a lista de dados para View
            return View(listaUsuarios);
        }

        [Authorize(Roles = "Administrador,Tecnico")]
        public ActionResult Inclui()
        {
            UsuarioViewModel usuarioVm = new UsuarioViewModel();
            return View(usuarioVm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Tecnico")]
        public ActionResult Inclui(UsuarioViewModel usuarioVm)
        {
            //Tratamento de erros
            try
            {
                //1 passo: cadastrar o usuario
                //Validacao dos dados
                if (ModelState.IsValid)
                {
                    //Preenche o Dto com ViewModel
                    Usuario usuarioDto = new Usuario();
                    usuarioDto.CodigoUsuario = 0;
                    usuarioDto.Email = usuarioVm.Email;
                    usuarioDto.Senha = usuarioVm.Senha;
                    usuarioDto.Perfil = usuarioVm.Perfil.ToString();
                                        
                    //Instancia o acesso ao banco de dados
                    _usuarioDao = new UsuarioDao();
                    //Realiza a inclusão no banco de dados
                    var codigoUsuario = _usuarioDao.IncluiUsuario(usuarioDto);

                    //2 passo: Cadastrar o cliente ou tecnico conforme o perfil
                    //Verifica se cadastrou o usuario
                    if (codigoUsuario > 0)
                    {
                        if (usuarioVm.Perfil == Perfis.Cliente)
                        {
                            //Cria o cadastro de cliente
                            Cliente clienteDto = new Cliente();
                            clienteDto.CodigoUsuario = codigoUsuario;
                            clienteDto.Nome = usuarioVm.Nome;
                            //Preenche com valor nulo string vazia
                            clienteDto.Profissao = ""; 
                            clienteDto.Setor = "";
                            //Instancia ao acesso ao banco de dados
                            _clienteDao = new ClienteDao();
                            //Inclui o cliente no banco de dados
                            _clienteDao.IncluiCliente(clienteDto);
                        } 
                        //else {} //para o administrador e tecnico
                    }

                    //Retorna
                    return RedirectToAction(nameof(Index));
                }

                //Caso tenha erro volta para view de inclusão
                return View(usuarioVm);
                
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Tecnico,Cliente")]
        public ActionResult Altera(int id)
        {
            //1 passo: obtem o usuario
            //Instancia o acesso ao banco de dados
            _usuarioDao = new UsuarioDao();
            //Obtem o usuario no banco de dados
            var usuarioDto = _usuarioDao.ObtemUsuario(id);
            //Cria o usuario ViewModel
            UsuarioViewModel usuarioVm = new UsuarioViewModel();
            //Preenche ViewModel com o Dto
            usuarioVm.CodigoUsuario = usuarioDto.CodigoUsuario;
            usuarioVm.Email = usuarioDto.Email;
            //usuarioVm.Senha = usuarioDto.Senha; //Seta senha em branco
            //Verifica o perfil do usuario
            switch (usuarioDto.Perfil)
            {
                case "Cliente":
                    usuarioVm.Perfil = Perfis.Cliente;
                    //2 passo: obtem o cliente pelo codigo do usuario
                    //Instancia ao acesso ao banco de dados
                    _clienteDao = new ClienteDao();
                    var clienteDto = new Cliente();
                    clienteDto = _clienteDao.ObtemClientePorUsuario(usuarioDto.CodigoUsuario);
                    //Preenche o usuario ViewModel com os dados do cliente                    
                    usuarioVm.Nome = clienteDto.Nome;
                    break;
                case "Tecnico":
                    usuarioVm.Perfil = Perfis.Tecnico;
                    break;
                case "Administrador":
                    usuarioVm.Perfil = Perfis.Administrador;
                    break;
                default:
                    break;
            }

            return View(usuarioVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Tecnico,Cliente")]
        public ActionResult Altera(UsuarioViewModel usuarioVm)
        {
            //Tratamento de erros
            try
            {
                //Validacao dos dados
                if (ModelState.IsValid)
                {
                    //Preenche o Dto com ViewModel
                    Usuario usuarioDto = new Usuario();
                    usuarioDto.CodigoUsuario = usuarioVm.CodigoUsuario;
                    usuarioDto.Email = usuarioVm.Email;
                    usuarioDto.Senha = usuarioVm.Senha;
                    usuarioDto.Perfil = usuarioVm.Perfil.ToString();

                    //Instancia o acesso ao banco de dados
                    _usuarioDao = new UsuarioDao();
                    //Realiza a alteracao no banco de dados
                    var resultado = _usuarioDao.AlteraUsuario(usuarioDto);

                    //2 passo: Atualizar o cliente ou tecnico conforme o perfil
                    //Verifica se cadastrou o usuario
                    if (resultado)
                    {
                        if (usuarioVm.Perfil == Perfis.Cliente)
                        {
                            //Instancia ao acesso ao banco de dados
                            _clienteDao = new ClienteDao();
                            //Obtem o cliente do usuario                             
                            Cliente clienteDto = new Cliente();
                            clienteDto = _clienteDao.ObtemClientePorUsuario(usuarioDto.CodigoUsuario);
                            //Altera os dados do cliente
                            clienteDto.Nome = usuarioVm.Nome;
                            //Altera o cliente no banco de dados
                            _clienteDao.AlteraCliente(clienteDto);
                        }
                        //else {} //para o administrador e tecnico
                    }

                    return RedirectToAction(nameof(Index));
                }

                //Caso tenha erro volta para view de inclusão
                return View(usuarioVm);

            }
            catch
            {
                return View();
            }
        }

        // GET: Exclui
        [Authorize(Roles = "Administrador,Tecnico")]
        public ActionResult Exclui(int id)
        {            
            //1 passo: obtem o usuario
            //Instancia o acesso ao banco de dados
            _usuarioDao = new UsuarioDao();
            //Obtem o usuario no banco de dados
            var usuarioDto = _usuarioDao.ObtemUsuario(id);
            //Inicia o UsuarioViewModel
            UsuarioViewModel usuarioVm = new UsuarioViewModel();
            usuarioVm.CodigoUsuario = usuarioDto.CodigoUsuario;

            return View(usuarioVm);
        }

        // POST: Exclui
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Tecnico")]
        public ActionResult Exclui(UsuarioViewModel usuarioVm)
        {
            try
            {
                //1 passo: obtem o usuario
                //Instancia o acesso ao banco de dados
                _usuarioDao = new UsuarioDao();
                //Obtem o usuario no banco de dados
                var usuarioDto = _usuarioDao.ObtemUsuario(usuarioVm.CodigoUsuario);
                //Verifica se existe usuario é cliente
                if (usuarioDto.Perfil == Perfis.Cliente.ToString())
                {
                    //Instancia o acesso ao banco de dados
                    _clienteDao = new ClienteDao();
                    //Exclui o cliente pelo codigo do usuario
                    _clienteDao.ExcluiClientePorCodigoUsuario(usuarioDto.CodigoUsuario);             
                }
                //Exclui o usuario
                _usuarioDao.ExcluiUsuario(usuarioDto.CodigoUsuario);
                //Volta para index
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Exibe
        [Authorize(Roles = "Administrador,Tecnico,Cliente")]
        public ActionResult Exibe(int id)
        {
            //1 passo: obtem o usuario
            //Instancia o acesso ao banco de dados
            _usuarioDao = new UsuarioDao();
            //Obtem o usuario no banco de dados
            var usuarioDto = _usuarioDao.ObtemUsuario(id);
            //Cria o usuario ViewModel
            UsuarioViewModel usuarioVm = new UsuarioViewModel();
            //Preenche ViewModel com o Dto
            usuarioVm.CodigoUsuario = usuarioDto.CodigoUsuario;
            usuarioVm.Email = usuarioDto.Email;
            //usuarioVm.Senha = usuarioDto.Senha; //Seta senha em branco
            //Verifica o perfil do usuario
            switch (usuarioDto.Perfil)
            {
                case "Cliente":
                    usuarioVm.Perfil = Perfis.Cliente;
                    //2 passo: obtem o cliente pelo codigo do usuario
                    //Instancia ao acesso ao banco de dados
                    _clienteDao = new ClienteDao();
                    var clienteDto = new Cliente();
                    clienteDto = _clienteDao.ObtemClientePorUsuario(usuarioDto.CodigoUsuario);
                    //Preenche o usuario ViewModel com os dados do cliente                    
                    usuarioVm.Nome = clienteDto.Nome;
                    break;
                case "Tecnico":
                    usuarioVm.Perfil = Perfis.Tecnico;
                    break;
                case "Administrador":
                    usuarioVm.Perfil = Perfis.Administrador;
                    break;
                default:
                    break;
            }

            return View(usuarioVm);
        }

    }
}
