using ChamadoTecnicoAppData.Dao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChamadoTecnicoWebApp.Areas.App.Controllers
{
    [Area("App")]
    public class UsuariosController : Controller
    {
        private UsuarioDao _usuarioDao;

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

        // GET: UsuariosController/Details/5
        public ActionResult Details(int id)
        {
        
            return View();
        }

        // GET: UsuariosController/Create
        public ActionResult Inclui()
        {

            return View();
        }

        // POST: UsuariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: UsuariosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsuariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
