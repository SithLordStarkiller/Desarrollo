using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Universidad.Controlador.GestionCatalogos;
using Universidad.Controlador.Login;
using Universidad.Controlador.MenuSistema;
using Universidad.Entidades;
using Universidad.Entidades.ControlUsuario;

namespace Universidad.WebAdministrativa.Controllers
{
    public class HomeController : AsyncController
    {
        public void DefaultAsync()
        {
            var sesion = (Sesion)Session["Sesion"];
            var usuario = (US_USUARIOS)Session["Usuario"];
            var servicioLogin = new SVC_LoginAdministrativos(sesion);
            var serviciosCatalogos = new SVC_GestionCatalogos(sesion);

            AsyncManager.Parameters["sesion"] = sesion;
            AsyncManager.Parameters["usuario"] = usuario;

            servicioLogin.ObtenNombreCompletoFinalizado += delegate(PER_PERSONAS persona)
            {
                AsyncManager.Parameters["persona"] = persona;
                Session["Persona"] = persona;
                AsyncManager.OutstandingOperations.Decrement();
            };

            AsyncManager.OutstandingOperations.Increment();
            servicioLogin.ObtenNombreCompleto(usuario);


            serviciosCatalogos.ObtenTipoUsuarioFinalizado += delegate(US_CAT_TIPO_USUARIO tipoUsuario)
            {
                AsyncManager.Parameters["tipoUsuario"] = tipoUsuario;
                AsyncManager.OutstandingOperations.Decrement();
            };

            AsyncManager.OutstandingOperations.Increment();
            serviciosCatalogos.ObtenTipoUsuario(usuario.ID_USUARIO);
        }

        public ActionResult DefaultCompleted(Sesion sesion, US_USUARIOS usuario, PER_PERSONAS persona,
            US_CAT_TIPO_USUARIO tipoUsuario)
        {
            ViewBag.Nombre = persona.NOMBRE_COMPLETO;
            ViewBag.TipoUsuario = tipoUsuario.TIPO_USUARIO;
            Session["Sesion"] = sesion;
            Session["Usuario"] = usuario;
            Session["Persona"] = persona;
            return View();
        }

       public List<MenuSisWadmE> ObtenArbol(List<SIS_WADM_ARBOLMENU_MVC> listaArbol, int? parentid)
        {
            return (from men in listaArbol
                    where men.IDMENUPADRE == parentid
                    select new MenuSisWadmE
                    {
                        IdMenuHijo = men.IDMENU,
                        IdMenuPadre = men.IDMENUPADRE,
                        IdTipoUsurio = men.ID_TIPO_USUARIO,
                        IdNivelUsuario = men.ID_NIVEL_USUARIO,
                        Nombre = men.NOMBRE,
                        Controller = men.CONTROLLER,
                        Method = men.ACTION,
                        Url = men.URL,
                        Hijos = ObtenArbol(listaArbol, men.IDMENU)
                    }).ToList();
        }

       public PartialViewResult ObtenArbolMenuWadm()
        {
            var sesion = (Sesion)Session["Sesion"];
            var usuario = (US_USUARIOS)Session["Usuario"];

            var serviciosSistema = new SvcMenuSistemaC(sesion);

            var listaArbol = serviciosSistema.TraeArbolMenuSyncrono(usuario);
            
            var lista = ObtenArbol(listaArbol, null);
            ViewBag.listaArbol = lista;

            return PartialView("ObtenArbolMenuWadm");
        }
    }
}