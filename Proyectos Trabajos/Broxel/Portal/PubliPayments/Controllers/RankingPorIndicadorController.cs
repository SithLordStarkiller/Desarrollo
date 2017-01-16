using System;
using System.Linq;
using System.Web.Mvc;
using PubliPayments.App_Code;
using PubliPayments.Entidades;
using PubliPayments.Utiles;

namespace PubliPayments.Controllers
{
    public class RankingPorIndicadorController : Controller
    {
        filtroDashBoard _tipoDashBoard = filtroDashBoard.Sin_Asignacion;
        private string _delegacionUsuario = "";

        public void Inicializar()
        {
            _delegacionUsuario = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol)) == 5
                ? new EntRankingIndicadores().ObtenerDelegacionUsuario(
                    SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario))[0]
                : "";

            switch (Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol)))
            {
                case 0: _tipoDashBoard = filtroDashBoard.Administrador; break;
                case 1: _tipoDashBoard = filtroDashBoard.Administrador; break;
                case 2: _tipoDashBoard = filtroDashBoard.Despacho; break;
                case 3: _tipoDashBoard = filtroDashBoard.Supervisor; break;
                case 4: _tipoDashBoard = filtroDashBoard.Gestor; break;
                case 5: _tipoDashBoard = filtroDashBoard.Delegacion; break;
                case 6: _tipoDashBoard = filtroDashBoard.Direccion; break;
            }
            ViewData["tpFrm"] = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta;
            ViewData["tipoFormulario"] = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Descripcion;
        }

        public ActionResult TablaSupervisor(string tipoDashboard, string indicador, string despacho, int valor, string delegacion = "0")
        {
            var guidTiempos = Tiempos.Iniciar();
            var tipoFormulario = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta;
            var lista = new EntRankingIndicadores().ObtenerTablaSupervisoresDelegacion(tipoDashboard, indicador, despacho, delegacion, valor, tipoFormulario);
            ViewData["tipoDashboard"] = tipoDashboard;
            ViewData["indicador"] = indicador;
            ViewData["despacho"] = despacho;
            ViewData["valor"] = valor;
            ViewData["delegacion"] = delegacion;
            Tiempos.Terminar(guidTiempos);
            return PartialView("TablaSupervisor", lista);
        }

        public ActionResult TablaSupervisorPropio(string tipoDashboard, string indicador, string despacho, string supervisor)
        {
            var guidTiempos = Tiempos.Iniciar();
            var tipoFormulario = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta;
            var lista = new EntRankingIndicadores().ObtenerTablaSupervisorValor(tipoDashboard, indicador, despacho, supervisor, 100,tipoFormulario);

            ViewData["tipoDashboard"] = tipoDashboard;
            ViewData["indicador"] = indicador;
            ViewData["despacho"] = despacho;
            ViewData["supervisor"] = supervisor;
            Tiempos.Terminar(guidTiempos);
            return PartialView("TablaSupervisorPropio", lista);
        }

        public ActionResult TablaDelegaciones(string tipoDashboard, string indicador, string despacho, int valor)
        {
            var guidTiempos = Tiempos.Iniciar();
            var tipoFormulario = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta;
            var lista = new EntRankingIndicadores().ObtenerTablaDelegaciones(tipoDashboard, indicador, despacho, valor, tipoFormulario).Where(e => e.Valor != 0);

            ViewData["tipoDashboard"] = tipoDashboard;
            ViewData["indicador"] = indicador;
            ViewData["despacho"] = despacho;
            ViewData["valor"] = valor;
            Tiempos.Terminar(guidTiempos);
            return PartialView("TablaDelegaciones", lista);
        }

        public ActionResult TablaDelegacionMaster(string tipoDashboard, string indicador)
        {
            var guidTiempos = Tiempos.Iniciar();
            var tipoFormulario = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta;
            var lista = new EntRankingIndicadores().ObtenerDelegaciones(tipoDashboard, indicador, tipoFormulario);
            ViewData["tipoDashboard"] = tipoDashboard;
            ViewData["indicador"] = indicador;
            Tiempos.Terminar(guidTiempos);
            return PartialView("TablaDelegacionMaster", lista);
        }

        public ActionResult TablaDespacho(string tipoDashboard, string indicador)
        {
            var guidTiempos = Tiempos.Iniciar();
            var tipoFormulario = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta;
            var lista = new EntRankingIndicadores().ObtenerTablaDespachos(tipoDashboard, indicador, tipoFormulario);
            ViewData["tipoDashboard"] = tipoDashboard;
            ViewData["indicador"] = indicador;
            Tiempos.Terminar(guidTiempos);
            return PartialView("TablaDespacho", lista);
        }

        public ActionResult TablaDespachoDelegacion(string tipoDashboard, string indicador, string delegacion,int valor=0)
        {
            var guidTiempos = Tiempos.Iniciar();
            var tipoFormulario = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta;
            var lista = new EntRankingIndicadores().ObtenerTablaDespachosDelegacion(tipoDashboard, indicador, delegacion, valor, tipoFormulario).Where(e => e.Valor != 0);
            ViewData["tipoDashboard"] = tipoDashboard;
            ViewData["indicador"] = indicador;
            ViewData["delegacion"] = delegacion;
            ViewData["valor"] = valor;
            Tiempos.Terminar(guidTiempos);
            return PartialView("TablaDespachoDelegacion", lista);
        }

        public ActionResult TablaGestor(string tipoDashboard, string indicador, string despacho, string supervisor, int valor, string delegacion = "0")
        {
            var guidTiempos = Tiempos.Iniciar();
            var tipoFormulario = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta;
            var lista = delegacion == "0"
                ? new EntRankingIndicadores().ObtenerTablaGestores(tipoDashboard, indicador, despacho, supervisor, valor, tipoFormulario)
                : new EntRankingIndicadores().ObtenerTablaGestoresDelegacion(tipoDashboard, indicador, despacho,
                    supervisor, delegacion, valor, tipoFormulario);

            ViewData["tipoDashboard"] = tipoDashboard;
            ViewData["indicador"] = indicador;
            ViewData["despacho"] = despacho;
            ViewData["supervisor"] = supervisor;
            ViewData["valor"] = valor;
            ViewData["delegacion"] = delegacion;
            Tiempos.Terminar(guidTiempos);
            return PartialView("TablaGestor",lista);
        }

        
        public ActionResult Index()
        {
            var guidTiempos = Tiempos.Iniciar();
            Inicializar();
            ViewBag.tipoDashBoard = _tipoDashBoard.ToString();
            ViewData["tipoDashBoard"] = _tipoDashBoard.ToString();
            ViewBag.delegacion = _delegacionUsuario;
            ViewData["delegacion"] = _delegacionUsuario;
            ViewBag.despacho = SessionUsuario.ObtenerDato(SessionUsuarioDato.NomCorto);
            ViewData["despacho"] = SessionUsuario.ObtenerDato(SessionUsuarioDato.NomCorto);
            ViewBag.usuario = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            ViewData["usuario"] = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            ViewData["AplicacionLogos"] = "/imagenes/Logos" + Config.AplicacionActual().Nombre + "/";
            var ruta = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta;
            var lista = new EntRankingIndicadores().ObtenerIndicadores(_tipoDashBoard.ToString(), ruta);
            Tiempos.Terminar(guidTiempos);
            return View("Index",lista);
        }

        public ActionResult RenderTabla(string tipoDashboard,string indicador,string despacho,string supervisor)
        {
            var guidTiempos = Tiempos.Iniciar();
            var tipoFormulario = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta;
            var lista = new EntRankingIndicadores().ObtenerTablaSupervisorValor(tipoDashboard, indicador, despacho, supervisor, 100,tipoFormulario);
            //var lista = new EntRankingIndicadores().ObtenerTablaGestores(tipoDashboard, indicador, despacho, supervisor);
            
            ViewData["tipoDashboard"] = tipoDashboard;
            ViewData["indicador"] = indicador;
            ViewData["despacho"] = despacho;
            ViewData["supervisor"] = supervisor;
            ViewData["tipoFormulario"] = tipoFormulario;
            Tiempos.Terminar(guidTiempos);
            return PartialView("TablaSupervisorPropio", lista);
        }

        public ActionResult RenderTablaSupervisor(string tipoDashboard, string indicador, string despacho, string delegacion)
        {
            var guidTiempos = Tiempos.Iniciar();
            var tipoFormulario = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta;
            var lista = new EntRankingIndicadores().ObtenerTablaDelegaciones(tipoDashboard, indicador, despacho, 100,tipoFormulario).Where(e => e.Valor != 0);
            ViewData["tipoDashboard"] = tipoDashboard;
            ViewData["indicador"] = indicador;
            ViewData["despacho"] = despacho;
            ViewData["valor"] = 100;
            ViewData["tipoFormulario"] = tipoFormulario;
            Tiempos.Terminar(guidTiempos);
            return PartialView("TablaDelegaciones", lista);
        }

        public ActionResult RenderTablaDespacho(string tipoDashboard, string indicador)
        {
            var guidTiempos = Tiempos.Iniciar();
            var tipoFormulario= CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta;
            var lista = new EntRankingIndicadores().ObtenerTablaDespachos(tipoDashboard, indicador, tipoFormulario);
            ViewData["tipoDashboard"] = tipoDashboard;
            ViewData["indicador"] = indicador;
            ViewData["tipoFormulario"] = tipoFormulario;
            Tiempos.Terminar(guidTiempos);
            return PartialView("renderTablaDespacho", lista);
        }

        public ActionResult TabPartial(string tipoDashboard)
        {
            var guidTiempos = Tiempos.Iniciar();
            var ruta = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta;
            var lista = new EntRankingIndicadores().ObtenerIndicadores(tipoDashboard, ruta);
            Tiempos.Terminar(guidTiempos);
            return PartialView("tabPartial",lista);
        }

        public ActionResult RenderTablaDelegacionMaster(string tipoDashboard, string indicador)
        {
            var guidTiempos = Tiempos.Iniciar();
            var tipoFormulario = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta;
            var lista = new EntRankingIndicadores().ObtenerDelegaciones(tipoDashboard, indicador, tipoFormulario);
            ViewData["tipoDashboard"] = tipoDashboard;
            ViewData["indicador"] = indicador;
            ViewData["tipoFormulario"] = tipoFormulario;
            Tiempos.Terminar(guidTiempos);
            return PartialView("RenderTablaDelegacionMaster", lista);
        }

        public ActionResult RenderTablaDelegacion_Despacho(string tipoDashboard, string indicador, string delegacion)
        {
            var guidTiempos = Tiempos.Iniciar();
            var tipoFormulario = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta;
            var lista = new EntRankingIndicadores().ObtenerTablaDespachosDelegacion(tipoDashboard, indicador, delegacion,100, tipoFormulario).Where(e => e.Valor != 0);
            ViewData["tipoDashboard"] = tipoDashboard;
            ViewData["indicador"] = indicador;
            ViewData["delegacion"] = delegacion;
            ViewData["tipoFormulario"] = tipoFormulario;
            Tiempos.Terminar(guidTiempos);
            return PartialView("RenderTablaDelegacion_Despacho", lista);
        }

    }
}
