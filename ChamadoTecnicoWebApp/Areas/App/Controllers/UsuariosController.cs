using ChamadoTecnicoAppData.Dao;
using ChamadoTecnicoAppData.Dto;
using ChamadoTecnicoWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChamadoTecnicoWebApp.Areas.App.Controllers
{
    [Area("App")]
    public class UsuariosController : Controller
    {
        private UsuarioDao _usuarioDao;
        private ClienteDao _clienteDao;

        // GET: UsuariosController
        public ActionResult Index()
        {
            //Instanciar UsuarioDao
            _usuarioDao = new UsuarioDao();
            //Obter a de usuario do banco de dados
            var listaUsuarios =_usuarioDao.ListaUsuario();

            //Enviar a lista de dados para view
            return View(listaUsuarios);
        }

        // GET: UsuariosController/Create
        public ActionResult Inclui()
        {
            UsuarioViewModel usuarioVm = new UsuarioViewModel();   
        
            return View(usuarioVm);
        }

        // POST: UsuariosController/Inclui
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Inclui(UsuarioViewModel usuarioVm)
        {
            try
            {
                //Primeiro Passo
                if (ModelState.IsValid)
                {
                    //Preencher o Dto com ViewModel
                    Usuario usuarioDto = new Usuario();
                    usuarioDto.CodigoUsuario = 0;
                    usuarioDto.Email = usuarioVm.Email ;
                    usuarioDto.Senha = usuarioVm.Senha ;
                    usuarioDto.Perfil = usuarioVm.Perfil.ToString() ;

                    //Fazer a inclusão no banco de dados
                    _usuarioDao = new UsuarioDao();
                    var codigoUsuario = _usuarioDao.IncluiUsuario(usuarioDto);

                    //Segundo Passo, Cadastrar o cliente ou técnico conforme o perfil

                    //verifica se cadastrou o usuario
                    if (codigoUsuario > 0)
                    {
                        if(usuarioVm.Perfil == Perfis.Cliente)
                        {
                            //cria o cadastro do cliente
                            Cliente clienteDto = new Cliente();
                            //obtem o cliente do usuario
                            clienteDto = _clienteDao.ObtemClientePor(usuarioDto.CodigoUsuario);

                            //Altera somente com o novo nome do cliente 
                            clienteDto.Nome = usuarioVm.Nome ;

                            //Preenche com valor nulo string vazia
                            clienteDto.Profissao = ""; 
                            clienteDto.Setor = "";
                            //instancia ao acesso ao banco de dados
                            _clienteDao = new ClienteDao();
                            //inclui o cliente no banco de daods
                            _clienteDao.IncluiCliente(clienteDto);
                        }
                    }

                    return RedirectToAction(nameof(Index));
                }

                //casO tenha erro volta para view de inclusão
                return View(usuarioVm);

            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        // GET: UsuariosController/Edit/
        public ActionResult Altera(int id)
        {
            //1 - Obtém o usuario
            //inicia o acesso ao banco de dados
            _usuarioDao = new UsuarioDao();
            //Obtem o usuario no banco de dados
            var usuarioDto = _usuarioDao.ObtemUsuario(id);
            //Cria o usuario ViewModel
            UsuarioViewModel usuarioVm = new UsuarioViewModel();
            //Preenche ViewModel com o Dto
            usuarioVm.CodigoUsuario = usuarioDto.CodigoUsuario;
            usuarioVm.Email = usuarioDto.Email;
            //usuarioVm.Senha = usuarioDto.Senha;//Vir com senha em branco
            switch (usuarioDto.Perfil)
            {
                case "Cliente":
                    usuarioVm.Perfil = Perfis.Cliente;
                    //2 - Obtém Cliente pelo código do usuário
                    var clienteDto = new Cliente();
                    clienteDto = _clienteDao.ObtemCliente(usuarioDto.CodigoUsuario);
                    //Preencher o Usuario View Model com os dados do cliente
                    usuarioVm = new UsuarioViewModel();

                    break;
                case "Tecnico":
                    usuarioVm.Perfil = Perfis.Tecnico;
                    break;
                case "Administrador":
                    usuarioVm.Perfil= Perfis.Administrador;
                    break;
            }
           
            return View(usuarioVm);
        }

        // POST: UsuariosController/Altera
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Altera(UsuarioViewModel usuarioVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Preencher o Dto com ViewModel
                    Usuario usuarioDto = new Usuario();
                    usuarioDto.CodigoUsuario = 0;
                    usuarioDto.Email = usuarioVm.Email;
                    usuarioDto.Senha = usuarioVm.Senha;
                    usuarioDto.Perfil = usuarioVm.Perfil.ToString();

                    //Fazer a alteração no banco de dados
                    _usuarioDao = new UsuarioDao();
                    var resultado = _usuarioDao.AlteraUsuario(usuarioDto);

                    // Atualizar o cliente
                    if (resultado > 0)
                    {
                        if (usuarioVm.Perfil == Perfis.Cliente)
                        {
                            //cria o cadastro do cliente
                            Cliente clienteDto = new Cliente();
                            clienteDto.Nome = usuarioVm.Nome;
                            //instancia ao acesso ao banco de dados
                            _clienteDao = new ClienteDao();
                            //inclui o cliente no banco de daods
                            _clienteDao.AlteraCliente(clienteDto);
                        }
                    }

                    return RedirectToAction(nameof(Index));
                }


                //caso tenha erro volta para view de inclusão
                return View(usuarioVm);

            }
            catch
            {
                return View();
            }
        }

        // GET: UsuariosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsuariosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
