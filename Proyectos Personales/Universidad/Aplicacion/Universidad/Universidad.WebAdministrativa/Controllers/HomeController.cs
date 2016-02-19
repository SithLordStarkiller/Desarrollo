﻿namespace Universidad.WebAdministrativa.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Controlador.GestionCatalogos;
    using Controlador.Login;
    using Controlador.MenuSistema;
    using Entidades;
    using Entidades.ControlUsuario;
    public class HomeController : AsyncController
    {

        [SessionExpireFilter]
        public ViewResult Default()
        {
            var sesion = (Sesion)Session["Sesion"];
            var usuario = (US_USUARIOS)Session["Usuario"];
            
            var servicioLogin = new SvcLogin(sesion);
            var serviciosCatalogos = new SvcGestionCatalogos(sesion);

            var persona = servicioLogin.ObtenNombreCompleto(usuario);
            
            if (usuario.ID_TIPO_USUARIO == null) return View();
            var tipoUsuario = serviciosCatalogos.ObtenTipoUsuario((int) usuario.ID_TIPO_USUARIO);

            var listTask = new List<Task>
            {
                persona,
                tipoUsuario
            };

            Task.WaitAll(listTask.ToArray());
            
            ViewBag.Nombre = persona.Result.NOMBRE + " " + persona.Result.A_PATERNO + " " + persona.Result.A_MATERNO;
            ViewBag.TipoUsuario = tipoUsuario.Result.TIPO_USUARIO;
            
            Session["Sesion"] = sesion;
            Session["Usuario"] = usuario;
            Session["Persona"] = persona.Result;
            Session["TipoPersona"] = tipoUsuario.Result;

            return View();
        }

        [SessionExpireFilter]
        public List<MenuSisWadmE> ObtenArbol(List<SIS_WADM_ARBOLMENU_MVC> listaArbol, int? parentid)
        {
            return (from men in listaArbol
                    where men.IDMENUPADRE == parentid
                    select new MenuSisWadmE
                    {
                        IdMenuHijo = men.IDMENU,
                        IdMenuPadre = men.IDMENUPADRE,
                        Nombre = men.NOMBRE,
                        Controller = men.CONTROLLER,
                        Method = men.ACTION,
                        Url = men.URL,
                        Hijos = ObtenArbol(listaArbol, men.IDMENU)
                    }).ToList();
        }
        
        [SessionExpireFilter]
        public PartialViewResult ObtenArbolMenuWadm()
        {
            var sesion = (Sesion)Session["Sesion"];
            var usuario = (US_USUARIOS)Session["Usuario"];

            var serviciosSistema = new SvcMenuSistema(sesion);

            var listaArbol = serviciosSistema.TraeArbolMenuMvc(usuario).Result;

            var lista = ObtenArbol(listaArbol, null);
            ViewBag.listaArbol = lista;

            return PartialView("ObtenArbolMenuWadm");
        }


    }
}