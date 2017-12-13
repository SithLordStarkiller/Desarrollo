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
        // GET: Usuarios
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

                return View(resultado < 0 ? "Error" : "CrearUsuario");
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }
    }
}