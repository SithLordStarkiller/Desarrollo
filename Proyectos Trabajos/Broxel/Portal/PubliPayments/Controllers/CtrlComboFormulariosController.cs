using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PubliPayments.Entidades;
using PubliPayments.Negocios;

namespace PubliPayments.Controllers
{
    public class CtrlComboFormulariosController : Controller
    {
        //
        // GET: /CtrlComboFormularios/
        private static List<ComboFormularioModel> _comboFormularios; 
        private static readonly object LockObject = new object();

        /// <summary>
        /// Vista parcial del control de selección de formularios
        /// </summary>
        /// <returns>Regresa la vista parcial del control de selección de formularios</returns>
        public ActionResult Index()
        {
            lock (LockObject)
            {
                if (_comboFormularios == null || _comboFormularios.Count == 0)
                {

                    var entFormularios =
                        new EntFormulario().ObtenerListaFormularios("").FindAll(x => x.Captura == 1).ToList();
                    var lista = entFormularios.Select(ef => new ComboFormularioModel
                    {
                        IdAplicacion = ef.IdAplicacion,
                        IdFormulario = ef.IdFormulario,
                        Estatus = ef.Estatus,
                        Captura = ef.Captura,
                        Descripcion = ef.Descripcion,
                        FechaAlta = ef.FechaAlta,
                        Nombre = ef.Nombre,
                        Ruta = ef.Ruta,
                        Version = ef.Version
                    }).ToList();

                    if (lista.Count > 0)
                    {
                        var filtro=new Filtros().ObtenerFiltros(
                            Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario)), "formulario");
                        Session["idFormulario"] = filtro != null ? lista.FirstOrDefault(x => x.Ruta == filtro).IdFormulario : lista[0].IdFormulario;
                    }

                    _comboFormularios = lista;
                }
            }
            if (Session["idFormulario"] == null)
            {
                var filtro = new Filtros().ObtenerFiltros(
                           Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario)), "formulario");
                Session["idFormulario"] = filtro != null ? _comboFormularios.FirstOrDefault(x => x.Ruta == filtro).IdFormulario : _comboFormularios[0].IdFormulario;
            }
            ViewBag.ComboSeleccionado = ObtenerFormularioActivo(); 

            return PartialView(_comboFormularios);
        }

        /// <summary>
        /// Cambia el formulario activo
        /// </summary>
        /// <param name="idFormulario">Identificador del formulario</param>
        /// <returns>Regresa "OK" si el cambio fue correcto</returns>
        public ActionResult CambiarFormulario(int idFormulario)
        {
            var formulario = _comboFormularios.FirstOrDefault(x => x.IdFormulario == idFormulario);

            if (formulario != null)
            {
                new EntFiltros().InsertaFiltro(Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario)), "formulario", formulario.Ruta);
                Session["idFormulario"] = idFormulario;
            }
            else
            {
                return Content("El formulario no existe");
            }

            return Content("OK");
        }

        /// <summary>
        /// Obtiene el formulario activo
        /// </summary>
        /// <returns>Regresa el id del formulario activo</returns>
        public static int ObtenerFormularioActivo()
        {
            var idFormulario = -1;
            if (System.Web.HttpContext.Current.Session["idFormulario"] != null)
            {
                idFormulario = Convert.ToInt32(System.Web.HttpContext.Current.Session["idFormulario"]);
            }

            return idFormulario;
        }

        /// <summary>
        /// Obtiene el formulario activo
        /// </summary>
        /// <returns>Regresa el Objeto del formulario activo</returns>
        public static ComboFormularioModel ObtenerModeloFormularioActivo(HttpSessionStateBase session)
        {
            var formulario = new ComboFormularioModel();
            if (session != null && session["idFormulario"] != null)
            {
                var idFormulario = Convert.ToInt32(session["idFormulario"]);

                formulario = _comboFormularios.FirstOrDefault(x => x.IdFormulario == idFormulario);
            }
            else
            {
                formulario.IdFormulario = -1;
            }
         
            return formulario;
        }
    }
}
