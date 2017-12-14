using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ExamenCsi.ControllerServer;
using ExamenCsi.Entities;
using ExamenCsi.Site.Models;

namespace ExamenCsi.Site.Controllers
{
    public class UsuariosController : Controller
    {
        public async Task<ActionResult> ListaUsuarios()
        {
            var resultado = await new UsuariosC().UsuarioObtenerTodos();
            return View(resultado);
        }

        public ActionResult CrearUsuario()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Insertar(UsUsuarioModel model)
        {
            try
            {
                var inserta = new UsUsuario
                {
                    Usuario = model.Usuario,
                    Contrasena = model.Contrasena,
                    IdTipoUsuario = 1
                };

                var resultado = await new UsuariosC().InsertaUsuario(inserta);

                ViewBag.Mensage = resultado > 0 ? "El usuario se guardo correctamente" : "No fue posible guardar el registro";

                var lista = await new UsuariosC().UsuarioObtenerTodos();

                return View(resultado < 0 ? "Error" : "ListaUsuarios", lista);
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }
    }
}