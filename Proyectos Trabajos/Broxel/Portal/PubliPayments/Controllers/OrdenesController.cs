using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using PubliPayments.Entidades;
using PubliPayments.Negocios;
using PubliPayments.Negocios.Originacion;
using PubliPayments.Utiles;

//Autorizar,,cancelar
namespace PubliPayments.Controllers
{
    public class OrdenesController : Controller
    {
        const string ErrorHref = "<a target='_blank' href='Ordenes/DescargarError?idError={EO}'>{EO}</a>";
        private bool SIRAProgramada = true;

        /// <summary>
        /// Se encarga de asignar créditos mediante la carga personalizada
        /// </summary>
        /// <param name="creditosList">Array con la información del crédito [0]- "numCred||usuarioAsignar||Auxiliar"... </param>
        /// <param name="paginaActual">Información de la pagina donde se está realizando la acción [0]- "titulo página origen", [1]- "Identificador página origen", [2] - "Ruta que se esta procesando" </param>
        /// <returns>respuesta de la asignación</returns>
        public ActionResult AsignarGestorCreditos(string[] creditosList, string[] paginaActual)
        {
            return AsignarCreditos(creditosList, paginaActual, false);
        }
        
        /// <summary>
        /// Se encarga de asignar créditos mediante la carga personalizada
        /// </summary>
        /// <param name="creditosList">Array con la información del crédito [0]- "numCred||usuarioAsignar||Auxiliar"... </param>
        /// <param name="paginaActual">Información de la página donde se está realizando la acción [0]- "titulo página origen", [1]- "Identificador página origen", [2] - "Ruta que se esta procesando" </param>
        /// <returns>respuesta de la asignación</returns>
        public ActionResult AsignarDifGestorCreditos(string[] creditosList, string[] paginaActual)
        {
            return AsignarCreditos(creditosList, paginaActual, true);
        }

        /// <summary>
        /// Se encarga de asignar ordenes 
        /// </summary>
        /// <param name="ordenesList">Array con la información de las ordenes a asignar [0]- [idorden,estatusOrden] </param>
        /// <param name="idNuevoGestor">id del usuario al cual se va a asignar</param>
        /// <param name="paginaActual">Información de la página donde se está realizando la acción [0]- "titulo página origen", [1]- "Identificador página origen", [2] - "Ruta que se esta procesando" </param>
        /// <returns>resultado de la asignación</returns>
        public ActionResult AsignarGestorOrdenes(string[][] ordenesList, string idNuevoGestor, string[] paginaActual)
        {
            return AsignarOrdenes(ordenesList, idNuevoGestor, false, paginaActual);
        }

        /// <summary>
        /// Se encarga de asignar ordenes 
        /// </summary>
        /// <param name="ordenesList">Array de info de la Orden/Credito [0]- [idorden,estatusOrden] </param>
        /// <param name="idNuevoGestor">id del usuario al cual se va a asignar</param>
        /// <param name="paginaActual">Información de la página donde se está realizando la acción [0]- "titulo página origen", [1]- "Identificador página origen", [2] - "Ruta que se esta procesando" </param>
        /// <returns>resultado de la asignación</returns>
       
        public ActionResult AsignarDifGestorOrdenes(string[][] ordenesList, string idNuevoGestor, string[] paginaActual)
        {
            return AsignarOrdenes(ordenesList,idNuevoGestor ,true,paginaActual);
        }

        /* Métodos para procesar asignación SIRA */
        /// <summary>
        /// Asignación de ordenes programadas 
        /// </summary>
        /// <param name="ordenesList">Areglo contenido de la información de las ordenes [idorden][estatus]</param>
        /// <param name="idNuevoGestor">id del usuario al cual se va a asignar la orden</param>
        /// <param name="paginaActual">Pagina de la cual se realiza el llamado del método</param>
        /// <returns>AsignarOrdenes:mensaje resultado de la asignación</returns>
        /// JARO
        public ActionResult AsignarGestorOrdenesSiraProg(string[][] ordenesList, string idNuevoGestor, string[] paginaActual)
        {
            SIRAProgramada = true;
            return AsignarOrdenes(ordenesList, idNuevoGestor, false, paginaActual);
        }

        /* Métodos para procesar asignación SIRA */
        /// <summary>
        /// Asignación de ordenes no programadas 
        /// </summary>
        /// <param name="ordenesList">Areglo contenido de la información de las ordenes [idorden][estatus]</param>
        /// <param name="idNuevoGestor">id del usuario al cual se va a asignar la orden</param>
        /// <param name="paginaActual">Pagina de la cual se realiza el llamado del método</param>
        /// <returns>AsignarOrdenes:mensaje resultado de la asignación</returns>
        /// JARO
        public ActionResult AsignarGestorOrdenesSiraNoProg(string[][] ordenesList, string idNuevoGestor, string[] paginaActual)
        {
            SIRAProgramada = false;
            return AsignarOrdenes(ordenesList, idNuevoGestor, false, paginaActual);
        }

        /* Métodos para procesar asignación SIRA */

        #region CargaPersonalizada

        /// <summary>
        /// se encarga de asignar créditos mediante la carga personalizada
        /// </summary>
        /// <param name="creditosList">Array con la información del crédito [0]- "numCred||usuarioAsignar||Auxiliar"... </param>
        /// <param name="paginaActual">Información de la pagina donde se está realizando la acción [0]- "titulo página origen", [1]- "Identificador página origen", [2] - "Ruta que se esta procesando" </param>
        /// <param name="ca">Indica si es cambio de asignación</param>
        /// <returns>respuesta de la asignación</returns>
        public ActionResult AsignarCreditos(string[] creditosList, string[] paginaActual, bool ca)
        {
            var guidTiempos = Tiempos.Iniciar();

            ConexionSql sql = ConexionSql.Instance;
            var paginaOrigen = "";
            string respuesta = "";
            var paginaContenedora = -1;
            const int tipo = 1;
            int totOrdenesAsignadas = 0;
            int limiteGestor = Config.LimiteOrdenGestor;
            Ordenes ordenAsociada = null;
            var entUsuario = new EntUsuario();
            var entOrden = new EntOrdenes();
            var errorList = new List<string>();
            var cuentaUsuario = new Dictionary<int, int>();
            int idUsuario = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            if (paginaActual != null)
            {
                paginaOrigen = paginaActual[0];
                paginaContenedora = Convert.ToInt32(paginaActual[1]);
            }
            Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuario, Request.FilePath, "AsignarCreditos, Origen: " + paginaOrigen);

            var usuarioPadre = entUsuario.ObtenerUsuarioPorId(idUsuario);
            var ordenesAsociadas = entOrden.ObtenerOrdenesPorIdPadre(usuarioPadre.idUsuario);
            var usuariosDominio = entUsuario.ObtenerUsuarios(usuarioPadre.idDominio,usuarioPadre.idUsuario,usuarioPadre.idRol==2? 3:4,-1,"%");
            
            List<string[]> ordenesList = creditosList.Select(x => x.Split(new[] { "||" }, StringSplitOptions.None)).ToList();

            try
            {
                var listaTexto = String.Join(",", (ordenesList.Select(x => (x[0] + "|" + x[1])).ToList()).ToArray());
                Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuario, Request.FilePath, "AsignarCreditos, Origen: " + paginaOrigen + "  créditos: " + listaTexto);
            }
            catch {}
            
            for (int i = 0; i < ordenesList.Count; i += 40)
            {
                var usuariosOrden = new Dictionary<int, string>();

                var sbAuxiliares = new StringBuilder();
                var sbAuxiliaresOrden = new StringBuilder();

                int restantes = ordenesList.Count - i;
                var valoresAProcesar = (restantes <= 40)
                    ? ordenesList.GetRange(i, restantes)
                    : ordenesList.GetRange(i, 40);
                foreach (string[] creditosInfo in valoresAProcesar)
                {
                    string numCred = creditosInfo[0].TrimStart('0');
                    string usuarioHijo = creditosInfo[1].Trim();
                    string auxiliar = creditosInfo[2].Trim();
                    var usuarioHijoInfo = usuariosDominio.Find(x => String.Equals(x.Usuario, usuarioHijo, StringComparison.CurrentCultureIgnoreCase) && (x.Estatus == 1 || x.Estatus == 3) && x.IdRol == 4);

                    switch (paginaContenedora)
                    {
                        case 1:
                            ordenAsociada = (ordenesAsociadas.Find(x => x.num_Cred == creditosInfo[0] && x.idUsuario == 0 && x.idUsuarioPadre == usuarioPadre.idUsuario && (x.Estatus == 1 || x.Estatus == 12 )));
                            break;
                       case 2:
                            ordenAsociada = (ordenesAsociadas.Find(x => x.num_Cred == creditosInfo[0] && x.idUsuarioPadre == usuarioPadre.idUsuario && (x.Estatus == 1 || x.Estatus == 11 || x.Estatus == 15 )));
                            break;
                       case 3:
                            ordenAsociada = (ordenesAsociadas.Find(x => x.num_Cred == creditosInfo[0] && x.idUsuarioPadre == usuarioPadre.idUsuario && x.Estatus == 3 ));
                            break;
                    }

                  if (ordenAsociada != null && auxiliar != "")
                    {
                        sbAuxiliaresOrden.Append("," + ordenAsociada.idOrden);
                        sbAuxiliares.Append("," + auxiliar);
                    }
                    if (usuarioHijoInfo == null || ordenAsociada == null)
                    {
                        if (usuarioHijoInfo == null)
                        {
                            errorList.Add(usuarioHijo +" <-- Error el gestor no cuenta con las condiciones para asignarle el crédito " + numCred + ".");
                        }
                        else
                        {
                            var ordenError = ordenesAsociadas.Find(x => x.num_Cred == creditosInfo[0]);
                            var errEspecifico = "";
                            if (ordenError == null) { errEspecifico = "Usted no tiene asignado este crédito"; }
                            else if (ordenError.Estatus == 4) { errEspecifico = "Crédito se encuentra autorizado"; }
                           // else if (ordenError.idVisita > 3) { errEspecifico = "Crédito alcanzó el número máximo de visitas"; }
                            else 
                            {
                                switch (paginaContenedora)
                                {
                                    case 1:
                                        if (ordenError.idUsuario != 0) { errEspecifico = "Crédito ya se encuentra asignado"; }                                        
                                        break;
                                    case 2:
                                        if (ordenError.Estatus == 3 ) { errEspecifico = "Crédito ya se encuentra gestionado"; }
                                        break;
                                    //case 3:
                                    //    if (ordenError.idVisita >= 3) { errEspecifico = "Crédito alcanzó el número máximo de visitas"; }
                                    //    break;
                                }    
                            }
                            errorList.Add(numCred + " <-- Error el crédito no se puede asignar. ---" + errEspecifico + "---");
                        }
                    }
                    else if (usuarioHijoInfo.IdUsuario == ordenAsociada.idUsuario && ca)
                    {
                        errorList.Add(numCred + " <-- Error el crédito ya se encuentra asignado a este gestor.");

                    }
                    else
                    {
                        try
                        {
                            //Valida el limite por usuario
                            if (cuentaUsuario.ContainsKey(usuarioHijoInfo.IdUsuario))
                            {
                                if (usuariosOrden.ContainsKey(usuarioHijoInfo.IdUsuario))
                                {
                                    var cuentaActual = cuentaUsuario[usuarioHijoInfo.IdUsuario]++;

                                    if (cuentaActual > limiteGestor)
                                    {
                                        errorList.Add(ordenAsociada.num_Cred + " <- No se pudo asignar el crédito " +
                                                      "porque el usuario superó el limite de asignación permitido.");
                                    }
                                    else
                                    {
                                        usuariosOrden[usuarioHijoInfo.IdUsuario] += "," + ordenAsociada.idOrden;
                                    }
                                }
                                else
                                {
                                    cuentaUsuario[usuarioHijoInfo.IdUsuario] = cuentaUsuario[usuarioHijoInfo.IdUsuario] + 1;
                                    var cuentaActual = cuentaUsuario[usuarioHijoInfo.IdUsuario];
                                    if (!(cuentaActual > limiteGestor))
                                    {
                                        usuariosOrden.Add(usuarioHijoInfo.IdUsuario,
                                            ordenAsociada.idOrden.ToString(CultureInfo.InvariantCulture));
                                    }
                                    else
                                    {
                                        errorList.Add(ordenAsociada.num_Cred + " <- No se pudo asignar el crédito " +
                                                      "porque el usuario superó el limite de asignación permitido.");
                                    }
                                }
                            }
                            else
                            {
                                int cuentaActual = sql.ObtenerNumeroAsignaciones(usuarioHijoInfo.IdUsuario) + 1;
                                if (!(cuentaActual > limiteGestor))
                                {
                                    cuentaUsuario.Add(usuarioHijoInfo.IdUsuario, cuentaActual);
                                    usuariosOrden.Add(usuarioHijoInfo.IdUsuario,
                                        ordenAsociada.idOrden.ToString(CultureInfo.InvariantCulture));
                                }
                                else
                                {
                                    cuentaUsuario.Add(usuarioHijoInfo.IdUsuario, cuentaActual);
                                    errorList.Add(ordenAsociada.num_Cred + " <- No se pudo asignar el crédito " +
                                                  "porque el usuario superó el limite de asignación permitido.");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteLine(Logger.TipoTraceLog.Error, usuarioPadre.idUsuario,
                                Request.FilePath,
                                "Error: al asignar intentar asignar créditos - Usuario: " +
                                usuarioHijoInfo.IdUsuario + " - UsuarioPadre: " + usuarioPadre.idPadre + " - Error: " +
                                ex.Message);

                            errorList.Add(ordenAsociada.num_Cred + " <-- Error el crédito no pudo ser asignado.");
                        }
                    }
                }
                if (sbAuxiliaresOrden.Length > 1 && sbAuxiliares.Length > 1)
                {
                    new EntOrdenes().ActualizarAuxiliar(sbAuxiliaresOrden.ToString().Substring(1), sbAuxiliares.ToString().Substring(1), idUsuario);
                }

                foreach (var usuarioOrden in usuariosOrden)
                {
                    totOrdenesAsignadas += entOrden.ActualizarEstatusUsuarioOrdenes(usuarioOrden.Value, ca ? 15 : 11, usuarioOrden.Key, true, true, usuarioPadre.idUsuario);
                }

            }
            var idErrorOrden = "-1";
            if (errorList.Count > 0)
            {
                var sbErrores = new StringBuilder();
                foreach (var l in errorList)
                {
                    sbErrores.Append(l);
                    sbErrores.Append(Environment.NewLine);
                }
                ConexionSql conn = ConexionSql.Instance;
                DataSet dsOrdenError = conn.InsertaOrdenesError(idUsuario, ca ? "Cambio asignación" : "Asignación", paginaOrigen, sbErrores.ToString());
                if (dsOrdenError.Tables.Count > 0 && dsOrdenError.Tables[0].Rows.Count > 0)
                {
                    idErrorOrden = dsOrdenError.Tables[0].Rows[0]["idErrorOrden"].ToString();
                }
            }
            if (respuesta == "")
            {
                respuesta = "Asignación completa, se asignaron " + totOrdenesAsignadas + " créditos " + ((errorList.Count > 0) ? ", se presentaron " + errorList.Count + " errores, identificador de error: " + ErrorHref.Replace("{EO}", idErrorOrden) : "") + "|1";
            }
            Session["Respuesta"] = respuesta;
            Tiempos.Terminar(guidTiempos);
            return Json(new { Respuesta = respuesta, Tipo = tipo }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReasignarCreditos(string[] creditosList, string[] paginaActual)
        {
            var guidTiempos = Tiempos.Iniciar();
            ConexionSql sql = ConexionSql.Instance;
            var entUsuario = new EntUsuario();
            var entOrden = new EntOrdenes();
            var cuentaUsuario = new Dictionary<int, int>();
            string paginaOrigen = "";
            int idUsuario = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            if (paginaActual != null)
            {
                paginaOrigen = paginaActual[0];
            }
            Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuario, Request.FilePath, "ReasignarCreditos, Origen: " + paginaOrigen);
            List<string[]> ordenesList = creditosList.Select(x => x.Split(new[] { "||" }, StringSplitOptions.None)).ToList();
            var usuarioPadre = entUsuario.ObtenerUsuarioPorId(idUsuario);
            var ordenesAsociadas = entOrden.ObtenerOrdenesPorIdPadre(usuarioPadre.idUsuario);
            string respuesta = "";
            var totalAutorizadas = 0;
            var totalNoCumpleCondicion = 0;
            var totalReasignadas = 0;
            var limiteGestor = Config.LimiteOrdenGestor;
            var creditosProcesados = new List<string>();
            var errorList = new List<string>();


            try
            {
                var listaTexto = String.Join(",", (ordenesList.Select(x => (x[0])).ToList()).ToArray());
                Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuario, Request.FilePath, "ReasignarCreditos, Origen: " + paginaOrigen + "  creditos: " + listaTexto);
            }
            catch {}
            
            for (int i = 0; i < ordenesList.Count; i += 40)
            {
                var ususariosOrden = new Dictionary<int, string>();
                var sbAuxiliaresOrden = new StringBuilder();
                var sbAuxiliares = new StringBuilder();
                int restantes = ordenesList.Count - i;
                var valoresAProcesar = (restantes <= 40)
                    ? ordenesList.GetRange(i, restantes)
                    : ordenesList.GetRange(i, 40);
                // ReSharper disable once CoVariantArrayConversion
                var creditosCancelar = valoresAProcesar.Select(x => ((object[])x)[0].ToString().Substring(0, 10)).ToList();

                foreach (var creditoOk in creditosCancelar)
                {
                    var creditoInfo = valoresAProcesar.Find(x => (x[0].ToString(CultureInfo.InvariantCulture).Substring(0, 10)) == creditoOk);
                    string auxiliar = creditoInfo[2].Trim();

                    var ordenAsociada = ordenesAsociadas.Find(x => x.num_Cred == creditoInfo[0].Substring(0, 10) && x.idUsuarioPadre == usuarioPadre.idUsuario && (x.Estatus == 3 || x.Estatus == 4) && x.idUsuario>=0 );

                    if (ordenAsociada != null && auxiliar != "")
                    {
                        sbAuxiliaresOrden.Append("," + ordenAsociada.idOrden);
                        sbAuxiliares.Append("," + auxiliar);
                    }
                    if (ordenAsociada != null && ordenAsociada.Estatus == 3)
                    {
                        try
                        {
                            if (cuentaUsuario.ContainsKey(ordenAsociada.idUsuario))
                            {
                                if (ususariosOrden.ContainsKey(ordenAsociada.idUsuario))
                                {
                                    var cuentaActual = cuentaUsuario[ordenAsociada.idUsuario]++;

                                    if (cuentaActual > limiteGestor)
                                    {
                                        errorList.Add(ordenAsociada.num_Cred + " <- No se pudo asignar el crédito " +
                                                      "porque el usuario superó el limite de asignación permitido.");
                                    }
                                    else
                                    {
                                        ususariosOrden[ordenAsociada.idUsuario] += "," + ordenAsociada.idOrden;
                                    }
                                }
                                else
                                {
                                    cuentaUsuario[ordenAsociada.idUsuario] = cuentaUsuario[ordenAsociada.idUsuario] + 1;
                                    var cuentaActual = cuentaUsuario[ordenAsociada.idUsuario];
                                    if (!(cuentaActual > limiteGestor))
                                    {
                                        ususariosOrden.Add(ordenAsociada.idUsuario,
                                            ordenAsociada.idOrden.ToString(CultureInfo.InvariantCulture));
                                    }
                                    else
                                    {
                                        errorList.Add(ordenAsociada.num_Cred + " <- No se pudo reasignar el crédito " +
                                                      "porque el usuario superó el limite de asignación permitido.");
                                    }
                                }
                            }
                            else
                            {
                                int cuentaActual = sql.ObtenerNumeroAsignaciones(ordenAsociada.idUsuario) + 1;
                                if (!(cuentaActual > limiteGestor))
                                {
                                    cuentaUsuario.Add(ordenAsociada.idUsuario, cuentaActual);
                                    ususariosOrden.Add(ordenAsociada.idUsuario,
                                        ordenAsociada.idOrden.ToString(CultureInfo.InvariantCulture));
                                }
                                else
                                {
                                    cuentaUsuario.Add(ordenAsociada.idUsuario, cuentaActual);
                                    errorList.Add(ordenAsociada.num_Cred + " <- No se pudo reasignar el crédito " +
                                                "porque el usuario superó el limite de asignación permitido.");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteLine(Logger.TipoTraceLog.Error, usuarioPadre.idUsuario,
                                Request.FilePath,
                                "Error: al asignar intentar asignar créditos - Usuario: " +
                                ordenAsociada.idUsuario + " - UsuarioPadre: " + usuarioPadre.idPadre + " - Error: " +
                                ex.Message);
                        }
                    }
                    else if (ordenAsociada != null && ordenAsociada.Estatus == 4 )
                        totalAutorizadas++;
                    else
                    {
                        totalNoCumpleCondicion++;
                        var ordenError = ordenesAsociadas.Find(x => x.num_Cred == creditoInfo[0].Substring(0, 10));
                        string errEspecifico;
                        if (ordenError == null) { errEspecifico = "Usted no tiene asignado este crédito."; }
                        else if (ordenError.idUsuario<0) { errEspecifico = "Crédito gestionado por Call Center, no puede ser reasignado."; }
                        else
                        {
                            errEspecifico = "No cuenta con las condiciones para realizar esta acción";
                        }
                        errorList.Add(creditoInfo[0] + " <-- Error el crédito no puede ser reasignado. ---" + errEspecifico + "---");
                    }
                    if (ordenAsociada != null) creditosProcesados.Add(ordenAsociada.num_Cred);
                }
                if (sbAuxiliaresOrden.Length > 1 && sbAuxiliares.Length > 1)
                {
                    new EntOrdenes().ActualizarAuxiliar(sbAuxiliaresOrden.ToString().Substring(1), sbAuxiliares.ToString().Substring(1), idUsuario);
                }
                foreach (var usuarioOrden in ususariosOrden)
                {
                    totalReasignadas += entOrden.ActualizarEstatusUsuarioOrdenes(usuarioOrden.Value, 15, -1, true,
                        true, usuarioPadre.idUsuario);
                }
            }
            var idErrorOrden = "-1";
            if (errorList.Count > 0)
            {
                var sbErrores = new StringBuilder();
                foreach (var l in errorList)
                {
                    sbErrores.Append(l);
                    sbErrores.Append(Environment.NewLine);
                }
                DataSet dsOrdenError = sql.InsertaOrdenesError(idUsuario, "Reasignacion", paginaOrigen, sbErrores.ToString());
                if (dsOrdenError.Tables.Count > 0 && dsOrdenError.Tables[0].Rows.Count > 0)
                {
                    idErrorOrden = dsOrdenError.Tables[0].Rows[0]["idErrorOrden"].ToString();
                }
            }
            if (respuesta == "")
            {
                respuesta = string.Format(
                    "Se reasignaron {0} órdenes y {1} no cumplen con la condiciones para ser reasignadas, {2} estaban autorizadas {3}.|-1 ",
                    totalReasignadas, totalNoCumpleCondicion, totalAutorizadas, (errorList.Count > 0 ? (", se presentaron " + errorList.Count + " errores, identificador de error: " + ErrorHref.Replace("{EO}", idErrorOrden)) : ""));
            }
            Session["Respuesta"] = respuesta;
            Tiempos.Terminar(guidTiempos);
            return Json(new { Respuesta = respuesta, Tipo = 1 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutorizarCreditos(string[] creditosList, string[] paginaActual)
        {
            var guidTiempos = Tiempos.Iniciar();
            ConexionSql sql = ConexionSql.Instance;
            var entUsuario = new EntUsuario();
            var entOrden = new EntOrdenes();
            string paginaOrigen = "";
            if (paginaActual != null)
            {
                paginaOrigen = paginaActual[0];
            }
            int idUsuario = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));

            Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuario, Request.FilePath, "AutorizarCreditos, Origen: " + paginaOrigen);
            List<string[]> ordenesList = creditosList.Select(x => x.Split(new[] { "||" }, StringSplitOptions.None)).ToList();
            var usuarioPadre = entUsuario.ObtenerUsuarioPorId(idUsuario);
            var ordenesAsociadas = entOrden.ObtenerOrdenesPorIdPadre(usuarioPadre.idUsuario);
            string respuesta = "";
            var totalAutorizadasPrev = 0;
            var totalAutorizadas = 0;
            var creditosProcesados = new List<string>();
            var errorList = new List<string>();

            try
            {
                var listaTexto = String.Join(",", (ordenesList.Select(x => (x[0])).ToList()).ToArray());
                Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuario, Request.FilePath, "AutorizarCreditos, Origen: " + paginaOrigen + "  creditos: " + listaTexto);
            }catch {}

            for (int i = 0; i < ordenesList.Count; i += 100)
            {
                var sb = new StringBuilder();
                var sbAuxiliaresOrden = new StringBuilder();
                var sbAuxiliares = new StringBuilder();
                int restantes = ordenesList.Count - i;
                var valoresAProcesar = (restantes <= 100)
                    ? ordenesList.GetRange(i, restantes)
                    : ordenesList.GetRange(i, 100);

                foreach (string[] creditosInfo in valoresAProcesar)
                {
                    string numCred = creditosInfo[0].TrimStart('0');
                    string auxiliar = creditosInfo[2].Trim();
                    var ordenAsociada = ordenesAsociadas.Find(x => x.num_Cred == creditosInfo[0] && x.idUsuarioPadre == usuarioPadre.idUsuario && (x.Estatus == 3 || x.Estatus == 4));

                    if (ordenAsociada != null)
                    {
                        if (numCred != "" && auxiliar != "")
                        {
                            sbAuxiliaresOrden.Append("," + ordenAsociada.idOrden);
                            sbAuxiliares.Append("," + auxiliar);
                        }
                        if (ordenAsociada.Estatus == 3)
                        {
                            sb.Append(",");
                            sb.Append(ordenAsociada.idOrden);
                        }
                        else
                            totalAutorizadasPrev++;

                        creditosProcesados.Add(ordenAsociada.num_Cred);
                    }
                    else
                    {
                        var ordenError = ordenesAsociadas.Find(x => x.num_Cred == creditosInfo[0]);
                        var errEspecifico = "";
                        if (ordenError == null) { errEspecifico = "Usted no tiene asignado este crédito"; }
                        else if (ordenError.Estatus == 4) { errEspecifico = "Crédito se encuentra autorizado"; }
                        else if (ordenError.Estatus < 3) { errEspecifico = "Crédito no ha sido gestionado"; }
                        errorList.Add("El crédito " + numCred + " no cuenta con las condiciones para ser autorizado. ---" + errEspecifico + "---");
                    }
                }
                if (sbAuxiliaresOrden.Length > 1 && sbAuxiliares.Length > 1)
                {
                    new EntOrdenes().ActualizarAuxiliar(sbAuxiliaresOrden.ToString().Substring(1), sbAuxiliares.ToString().Substring(1), idUsuario);
                }
                if (sb.Length > 0)
                {
                    totalAutorizadas += entOrden.ActualizarEstatusUsuarioOrdenes(sb.ToString().Substring(1), 4, -1, true, false, idUsuario);
                    if (totalAutorizadas < 0)
                        totalAutorizadas = 0;
                    if (Config.AplicacionActual().Productivo && Config.AplicacionActual().idAplicacion == 1) //Solo London
                    {
                        var list = sb.ToString().Substring(1).Split(',').ToList();
                        var objectList = new List<Object>(list);
                        Gestiones.EnviarGestionBatch(objectList, "V","4");
                    }
                }
            }
            var idErrorOrden = "-1";
            if (errorList.Count > 0)
            {
                var sbErrores = new StringBuilder();
                foreach (var l in errorList)
                {
                    sbErrores.Append(l);
                    sbErrores.Append(Environment.NewLine);
                }

                DataSet dsOrdenError = sql.InsertaOrdenesError(idUsuario, "Autorización", paginaOrigen, sbErrores.ToString());
                if (dsOrdenError.Tables.Count > 0 && dsOrdenError.Tables[0].Rows.Count > 0)
                {
                    idErrorOrden = dsOrdenError.Tables[0].Rows[0]["idErrorOrden"].ToString();
                }
            }
            if (respuesta == "")
            {
                respuesta = string.Format(
                    "Se autorizaron {0} órdenes, {1} ya se encontraban autorizadas" + (errorList.Count > 0 ? (", se presentaron " + errorList.Count + " errores, identificador de error: " + ErrorHref.Replace("{EO}", idErrorOrden)) : "") + ".|-1",
                    totalAutorizadas, totalAutorizadasPrev);
            }
            Session["Respuesta"] = respuesta;
            Tiempos.Terminar(guidTiempos);
            return Json(new { Respuesta = respuesta, Tipo = 1 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CancelarCreditos(string[] creditosList, string[] paginaActual)
        {
            var guidTiempos = Tiempos.Iniciar();
            var aplicacion = Config.AplicacionActual().Nombre;
            var entUsuario = new EntUsuario();
            var entOrden = new EntOrdenes();
            List<string[]> ordenesList = creditosList.Select(x => x.Split(new[] { "||" }, StringSplitOptions.None)).ToList();
            int idUsuario = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            int paginaContenedora;
            var usuarioPadre = entUsuario.ObtenerUsuarioPorId(idUsuario);
            var ordenesAsociadas = entOrden.ObtenerOrdenesPorIdPadre(usuarioPadre.idUsuario);
            var entFormulario = new EntFormulario().ObtenerListaFormularios(paginaActual[2]).FirstOrDefault(x => x.Captura == 1);
            string respuesta = "";
            var totalCanceladas = 0;
            string paginaOrigen=paginaActual[0];
            Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuario, Request.FilePath, "CancelarCreditos, Origen: " + paginaOrigen);
            if (entFormulario != null)
            {
                var orden = new Orden(usuarioPadre.idRol, Config.AplicacionActual().Productivo, Config.AplicacionActual().ClientId, Config.AplicacionActual().ProductId, entFormulario.Nombre, entFormulario.Version,null);
                Ordenes ordenAsociada = null;
                var errorList = new List<string>();
                paginaContenedora = Convert.ToInt32(paginaActual[1]);
                try
                {
                    var listaTexto = String.Join(",", (ordenesList.Select(x => (x[0])).ToList()).ToArray());
                    Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuario, Request.FilePath, "CancelarCreditos, Origen: " + paginaOrigen + "  creditos: " + listaTexto);
                }catch {}
                

                for (int i = 0; i < ordenesList.Count; i += 100)
                {
                    var listaOrdenes = new List<int>();
                    var sbAuxiliaresOrden = new StringBuilder();
                    var sbAuxiliares = new StringBuilder();
                    int restantes = ordenesList.Count - i;
                    var valoresAProcesar = (restantes <= 100)
                        ? ordenesList.GetRange(i, restantes)
                        : ordenesList.GetRange(i, 100);

                    foreach (string[] creditosInfo in valoresAProcesar)
                    {
                        string numCred = creditosInfo[0].TrimStart('0');
                        string auxiliar = creditosInfo[2].Trim();

                        switch (paginaContenedora)
                        {
                            case 2:
                                ordenAsociada = ordenesAsociadas.Find(x => x.num_Cred == creditosInfo[0] && x.idUsuarioPadre == usuarioPadre.idUsuario && (x.Estatus == 1 || x.Estatus == 11 || x.Estatus == 15 ) && x.idUsuario != 0 );
                                break;
                            case 3:
                                ordenAsociada = ordenesAsociadas.Find(x => x.num_Cred == creditosInfo[0] && x.idUsuarioPadre == usuarioPadre.idUsuario && (x.Estatus == 3 || x.Estatus == 15) && x.idUsuario != 0 );
                                break;
                        }
                                    

                        if (ordenAsociada != null)
                        {
                            if (numCred != "" && auxiliar != "")
                            {
                                sbAuxiliaresOrden.Append("," + ordenAsociada.idOrden);
                                sbAuxiliares.Append("," + auxiliar);
                            }
                            listaOrdenes.Add(ordenAsociada.idOrden);
                        }
                        else
                        {
                            var ordenError= ordenesAsociadas.Find(x => x.num_Cred == creditosInfo[0]);
                            var errEspecifico = "";
                            if (ordenError == null) { errEspecifico = "Usted no tiene asignado este crédito"; }
                            else if (ordenError.idUsuario == 0) { errEspecifico = "Crédito no está asignado a ningún móvil"; }
                            else if (ordenError.Estatus == 4) { errEspecifico = "Crédito se encuentra autorizado"; }
                            else if (ordenError.Estatus == 6) { errEspecifico = "Crédito no se puede cancelar, se encuentra en estado Sincronizando"; }
                            //else if (ordenError.idVisita >= 3) { errEspecifico = "Crédito alcanzó el número máximo de visitas"; }
                            else
                            {
                                switch (paginaContenedora)
                                {
                                    case 2:
                                        if (ordenError.Estatus == 3) { errEspecifico = "Crédito ya se encuentra gestionado"; }
                                        break;
                                }
                            }
                        
                            errorList.Add(numCred + " <-- Error el crédito no se pudo cancelar. ---" + errEspecifico + "---");
                        }
                    }
                    if (listaOrdenes.Count>0)
                    {
                        if (aplicacion.Contains("OriginacionMovil")) //Originacion
                        {
                            totalCanceladas += orden.CambiarEstatusOrdenes(listaOrdenes, 2, true,false,
                                Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario)));
                        }
                        else
                        {
                            totalCanceladas += orden.Cancelar(listaOrdenes,
                                Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario)));
                        }
                        if (sbAuxiliaresOrden.Length > 1 && sbAuxiliares.Length > 1)
                        {
                            new EntOrdenes().ActualizarAuxiliar(sbAuxiliaresOrden.ToString().Substring(1), sbAuxiliares.ToString().Substring(1), idUsuario);
                        }
                    }
                }
                var idErrorOrden = "-1";
                if (errorList.Count > 0)
                {
                    var sbErrores = new StringBuilder();
                    foreach (var l in errorList)
                    {
                        sbErrores.Append(l);
                        sbErrores.Append(Environment.NewLine);
                    }
                    ConexionSql conn = ConexionSql.Instance;
                    DataSet dsOrdenError = conn.InsertaOrdenesError(idUsuario,"Cancelar", paginaOrigen, sbErrores.ToString());
                    if (dsOrdenError.Tables.Count > 0 && dsOrdenError.Tables[0].Rows.Count > 0)
                    {
                        idErrorOrden = dsOrdenError.Tables[0].Rows[0]["idErrorOrden"].ToString();
                    }
                }
                if (respuesta == "")
                {
                    if (totalCanceladas == 0)
                    {
                        respuesta = "Se" + (aplicacion.Contains("OriginacionMovil") ? " rechazó " : " canceló ") + totalCanceladas + " orden" +
                                    (errorList.Count > 0
                                        ? (", se presentaron " + errorList.Count + " errores, identificador de error: " +
                                           ErrorHref.Replace("{EO}", idErrorOrden))
                                        : " , inténtelo mas tarde") + ".|-1";
                    }
                    else if (totalCanceladas > 1)
                        respuesta = "Se" + (aplicacion.Contains("OriginacionMovil") ? " rechazaron " : " cancelaron ") + totalCanceladas +
                                    " órdenes" +
                                    (errorList.Count > 0
                                        ? (", se presentaron " + errorList.Count + " errores, identificador de error: " +
                                           ErrorHref.Replace("{EO}", idErrorOrden))
                                        : "") + ".|-1";
                    else
                    {
                        respuesta = "Se" + (aplicacion.Contains("OriginacionMovil") ? " rechazó " : " canceló ") + totalCanceladas + " orden" +
                                    (errorList.Count > 0
                                        ? (", se presentaron " + errorList.Count + " errores, identificador de error: " +
                                           ErrorHref.Replace("{EO}", idErrorOrden))
                                        : "") + ".|-1";
                    }

                }
            }
            Session["Respuesta"] = respuesta;
            Tiempos.Terminar(guidTiempos);
            return Json(new { Respuesta = respuesta, Tipo = 1 }, JsonRequestBehavior.AllowGet);
        }
        #endregion
      
        /// <summary>
        /// Asignación de Ordenes
        /// </summary>
        /// <param name="ordenes">Array de info de la Orden/Credito [0]- [idorden,estatusOrden] </param>
        /// <param name="idNuevoGestor">id del usuario al cual se va a asignar</param>
        /// <param name="ca">indicador si pertenece a un cambio de asignación o no</param>
        /// <param name="paginaActual">Pagina de la cual se realiza el llamado del método</param>
        /// <returns>Mensaje indicando el resultado de la asignación</returns>
        public ActionResult AsignarOrdenes(string[][] ordenes, string idNuevoGestor, bool ca,string[] paginaActual)
        {
            var guidTiempos = Tiempos.Iniciar();
            ConexionSql sql = ConexionSql.Instance;
            var entUsuario = new EntUsuario();
            var paginaOrigen = "";
            var entOrden = new EntOrdenes();
            int idUsuario = Convert.ToInt32(idNuevoGestor);
            var usuarioPadreid = (SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol)) == "0" ? entUsuario.ObtenerUsuarioPorId(idUsuario).idPadre : Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            var ordenesList = ordenes.ToList();
            var usuarioPadre = entUsuario.ObtenerUsuarioPorId(usuarioPadreid);
            var ordenesAsociadas = entOrden.ObtenerOrdenesPorIdPadre(usuarioPadre.idUsuario);
            string respuesta = "";
            int totOrdenesAsignadas = 0;
            int limiteGestor = Config.LimiteOrdenGestor;
            var cuentaUsuario = new Dictionary<int, int>();
            int idOrden;
            var errorList = new List<string>();
            var entOrdenes = new EntOrdenes();
            if (paginaActual != null)
            {
                paginaOrigen = paginaActual[0];
            }

            if (paginaActual != null)
            {
                var entFormulario = new EntFormulario().ObtenerListaFormularios(paginaActual[2]).FirstOrDefault(x => x.Captura == 1);

                if (entFormulario != null)
                {
                    try
                    {
                        var listaTexto = ordenesList.Select(x => Convert.ToInt32((x[0]))).ToList();
                        Logger.WriteLine(Logger.TipoTraceLog.Trace, usuarioPadreid, Request.FilePath, "AsignarOrdenes, Origen: " + paginaOrigen + " idUsuarioH: " + idUsuario + "  Ordenes: " + String.Join(",", listaTexto));
                    }catch {}
                    

                    var orden = new Orden(usuarioPadre.idRol, Config.AplicacionActual().Productivo,
                        Config.AplicacionActual().ClientId, Config.AplicacionActual().ProductId,
                        entFormulario.Nombre, entFormulario.Version, Config.AplicacionActual().Nombre);
                    Logger.WriteLine(Logger.TipoTraceLog.Trace, usuarioPadreid, Request.FilePath, "AsignarOrdenes, Origen: " + paginaOrigen);

                    for (int i = 0; i < ordenesList.Count; i += 40)
                    {
                        var usuariosOrden = new Dictionary<int, string>();
                        var ordenesSup = new List<int>();
                        int restantes = ordenesList.Count - i;
                        var valoresAProcesar = (restantes <= 40)
                            ? ordenesList.GetRange(i, restantes)
                            : ordenesList.GetRange(i, 40);
                        foreach (var ordenesInfo in valoresAProcesar)
                        {
                            if (usuarioPadre.idRol == 3)
                            {
                                idOrden = Convert.ToInt32(ordenesInfo[0]);
                                try
                                {
                                    //Valida el limite por ususario
                                    if (ordenesInfo[1] == "4")
                                    {
                                        errorList.Add(idUsuario + " <- No se pudo asignar la órden por que se encuentra autorizada.");
                                    }
                                    else
                                    {
                                        var ordenAsociada = ordenesInfo[1]== "3"
                                            ? (ordenesAsociadas.Find(x => x.idOrden == idOrden && (x.Estatus == 3)))
                                            : (ordenesAsociadas.Find(x => x.idOrden == idOrden && (x.Estatus == 1 || x.Estatus == 11 || x.Estatus == 12 || x.Estatus == 15)));
                                        
                                        if (ordenAsociada.idUsuario == idUsuario)
                                        {
                                            errorList.Add(ordenAsociada.num_Cred + " <- No se pudo asignar el crédito porque el usuario ya lo tiene asignado.");
                                            continue;
                                        }
                                        if (cuentaUsuario.ContainsKey(idUsuario))
                                        {
                                            if (usuariosOrden.ContainsKey(idUsuario))
                                            {
                                                var cuentaActual = cuentaUsuario[idUsuario]++;

                                                if (cuentaActual > limiteGestor)
                                                {
                                            
                                                    errorList.Add(ordenAsociada.num_Cred + " <- No se pudo asignar el crédito porque el usuario superó el limite de asignación permitido.");
                                                }
                                                else
                                                {
                                                    usuariosOrden[idUsuario] += "," + idOrden;
                                                }
                                            }
                                            else
                                            {
                                                cuentaUsuario[idUsuario] = cuentaUsuario[idUsuario] + 1;
                                                var cuentaActual = cuentaUsuario[idUsuario];
                                                if (!(cuentaActual > limiteGestor))
                                                {
                                                    usuariosOrden.Add(idUsuario, idOrden.ToString(CultureInfo.InvariantCulture));
                                                }
                                                else
                                                {
                                                    errorList.Add(ordenAsociada.num_Cred + " <- No se pudo asignar el crédito porque el usuario superó el limite de asignación permitido.");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            int cuentaActual = (sql.ObtenerNumeroAsignaciones(idUsuario) + 1);
                                            if (!(cuentaActual > limiteGestor))
                                            {
                                                cuentaUsuario.Add(idUsuario, cuentaActual);
                                                usuariosOrden.Add(idUsuario, idOrden.ToString(CultureInfo.InvariantCulture));
                                            }
                                            else
                                            {
                                                cuentaUsuario.Add(idUsuario, cuentaActual);
                                                errorList.Add(ordenAsociada.num_Cred + " <- No se pudo asignar el crédito porque el usuario superó el limite de asignación permitido.");
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Logger.WriteLine(Logger.TipoTraceLog.Error, usuarioPadre.idUsuario,
                                        Request.FilePath,
                                        "Error: al asignar intentar asignar órdenes - Usuario: " +
                                        idUsuario + " - UsuarioPadre: " + usuarioPadre.idUsuario + " - Error: " +
                                        ex.Message);

                                    errorList.Add(idOrden + " <-- La orden no pudo ser asignada. Por favor intente mas tarde");
                                }
                            }
                            else if ( usuarioPadre.idRol == 2)
                            {
                                if (usuariosOrden.ContainsKey(idUsuario))
                                {
                                    usuariosOrden[idUsuario] += "," + ordenesInfo[0];
                            
                                }
                                else
                                {
                                    usuariosOrden.Add(idUsuario, ordenesInfo[0].ToString(CultureInfo.InvariantCulture));
                                }
                        
                                ordenesSup.Add(0);                                            
                            }
                        }
                        if (usuarioPadre.idRol == 2)
                        {
                            int cantidadAsignadas;
                            cantidadAsignadas = orden.AsignarOrdenes(usuariosOrden[idUsuario].Split(',').ToList(), ordenesSup, true,
                                0, idUsuario, usuarioPadre.idUsuario, usuarioPadre.idDominio, usuarioPadre.idUsuario, Config.LimiteOrdenGestor);

                            if (cantidadAsignadas == 0)
                            {
                                Session["Respuesta"] = "Ocurrió un error al intentar asignar las ordenes, solo se asignaron: " + totOrdenesAsignadas + " ordenes, por favor valide la asignación y vuelva a intentarlo.";
                                Tiempos.Terminar(guidTiempos);
                                return Json(new { Respuesta = respuesta, Tipo = 1 }, JsonRequestBehavior.AllowGet);
                            }

                            totOrdenesAsignadas += cantidadAsignadas;
                        }
                        else if (usuarioPadre.idRol == 3)
                        {
                            foreach (var usuarioOrden in usuariosOrden)
                            {
                                int totOrdenesAsignadasTemp = totOrdenesAsignadas;
                                totOrdenesAsignadas += entOrden.ActualizarEstatusUsuarioOrdenes(usuarioOrden.Value, ca ? 15 : 11, usuarioOrden.Key, true, true, usuarioPadre.idUsuario);
                                if (totOrdenesAsignadasTemp != totOrdenesAsignadas)
                                {
                                    foreach (var ordenVal in usuarioOrden.Value.Split(','))
                                    {
                                        if(Config.AplicacionActual().Nombre.ToUpper().Contains("SIRA"))
                                        {
                                            orden.ActualizarTipoOrden(Convert.ToInt32(ordenVal), SIRAProgramada ? "P" : "NP", 0, usuarioPadre.idUsuario);
                                            entOrdenes.ActualizarAuxiliar(ordenVal, " ", usuarioPadre.idUsuario);
                                        }
                                    }
                                   
                                }
                               
                            }
                        }
                    }
                }
            }
            var idErrorOrden = "-1";
            if (errorList.Count > 0)
            {
                var sbErrores = new StringBuilder();
                foreach (var l in errorList)
                {
                    sbErrores.Append(l);
                    sbErrores.Append(Environment.NewLine);
                }
                ConexionSql conn = ConexionSql.Instance;
                DataSet dsOrdenError = conn.InsertaOrdenesError(usuarioPadre.idUsuario, ca ? "Cambio asignación" : "Asignación", paginaOrigen, sbErrores.ToString());
                if (dsOrdenError.Tables.Count > 0 && dsOrdenError.Tables[0].Rows.Count > 0)
                {
                    idErrorOrden = dsOrdenError.Tables[0].Rows[0]["idErrorOrden"].ToString();
                }
            }
            if (respuesta == "")
            {
                respuesta = "Asignación completa, se asignaron " + totOrdenesAsignadas + " órdenes " + ((errorList.Count > 0) ? ", se presentaron " + errorList.Count + " errores, identificador de error: " + ErrorHref.Replace("{EO}", idErrorOrden) : "") + "|1";
            }
            Session["Respuesta"] = respuesta;
            Tiempos.Terminar(guidTiempos);
            return Json(new { Respuesta = respuesta, Tipo = 1 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutorizarOrdenes(string[][] ordenesList,string[] paginaActual)
        {
            var guidTiempos = Tiempos.Iniciar();
            var ordenes = ordenesList.ToList();
            int idUsuario = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            var entOrden = new EntOrdenes();
            var aplicacion = Config.AplicacionActual().Nombre;
            string respuesta = "";
            var totalAutorizadasPrev = 0;
            var totalAutorizadas = 0;
            string paginaOrigen = "";
            if (paginaActual != null)
            {
                paginaOrigen = paginaActual[0];
            }
            Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuario, Request.FilePath, "AutorizarOrdenes, Origen: " + paginaOrigen);

            try
            {
                var listaTexto = ordenesList.Select(x => Convert.ToInt32((x[0]))).ToList();
                Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuario, Request.FilePath, "AutorizarOrdenes, Origen: " + paginaOrigen + "  Ordenes: " + String.Join(",", listaTexto));
            }catch {}

            for (int i = 0; i < ordenes.Count; i += 100)
            {
                var sb = new StringBuilder();

                int restantes = ordenes.Count - i;
                var valoresAProcesar = (restantes <= 100)
                    ? ordenes.GetRange(i, restantes)
                    : ordenes.GetRange(i, 100);

                foreach (var ordenesInfo in valoresAProcesar)
                {
                    if (ordenesInfo[1] == "3")
                    {
                        sb.Append(",");
                        sb.Append(ordenesInfo[0]);
                    }
                    else
                        totalAutorizadasPrev++;
                }

                if (sb.Length > 0)
                {
                    if (aplicacion.Contains("OriginacionMovil"))
                    {
                        totalAutorizadas = entOrden.AutorizaOriginacion(sb.ToString().Substring(1));
                        if (totalAutorizadas < 0)
                        {
                            totalAutorizadas = 0;
                        }
                        if (totalAutorizadas > 0)
                        {
                            var idOrden=Convert.ToInt32(sb.ToString().Substring(1));
                            try
                            {
                                var pdf = new GeneraPdf();
                                pdf.GeneraPdfCompleto(idOrden);
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "Ordenes", "GeneraPdfCompleto - AutorizaOriginacion_Error: " + error);
                            }
                        }
                    }
                    else
                    {
                        totalAutorizadas += entOrden.ActualizarEstatusUsuarioOrdenes(sb.ToString().Substring(1), 4, -1,
                            true, false, idUsuario);
                        if (totalAutorizadas < 0)
                            totalAutorizadas = 0;
                    }

                    if (Config.AplicacionActual().Productivo && Config.AplicacionActual().idAplicacion == 1) //Solo London
                    {
                        var list = sb.ToString().Substring(1).Split(',').ToList();
                        var objectList = new List<Object>(list);
                        Gestiones.EnviarGestionBatch(objectList, "V","4");
                    }
                }
            }
            if (respuesta == "")
            {
                respuesta = string.Format(
                    "Se autorizaron {0} órdenes, {1} ya se encontraban autorizadas.|-1",
                    totalAutorizadas, totalAutorizadasPrev);
            }
            Session["Respuesta"] = respuesta;
            Tiempos.Terminar(guidTiempos);
            return Json(new { Respuesta = respuesta, Tipo = 1 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CancelarOrdenes(string[][] ordenesList, string idNuevoGestor, string[] paginaActual)
        {
            var guidTiempos = Tiempos.Iniciar();
            var aplicacion = Config.AplicacionActual().Nombre;
            var ordenes = ordenesList.Select(x => Convert.ToInt32((x[0]))).ToList();
            int idUsuario = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            int idRol = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol));
            idRol = (idRol == 0 && idNuevoGestor == "1") ? 3 : idRol;
            var entFormulario = new EntFormulario().ObtenerListaFormularios(paginaActual[2]).FirstOrDefault(x => x.Captura == 1);
            string respuesta = "";
            var totalCanceladas = 0;
            string paginaOrigen;
            paginaOrigen = paginaActual[0];
            Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuario, Request.FilePath, "CancelarOrdenes. Origen: " + paginaOrigen);

            if (entFormulario != null)
            {
                try
                {
                    var listaTexto = ordenesList.Select(x => Convert.ToInt32((x[0]))).ToList();
                    Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuario, Request.FilePath, "CancelarOrdenes, Origen: " + paginaOrigen + "  Ordenes: " + String.Join(",", listaTexto));
                }catch {}

                var orden = new Orden(idRol, Config.AplicacionActual().Productivo, Config.AplicacionActual().ClientId, Config.AplicacionActual().ProductId, entFormulario.Nombre, entFormulario.Version, Config.AplicacionActual().Nombre);

                for (int i = 0; i < ordenes.Count; i += 100)
                {
                    int restantes = ordenes.Count - i;
                    var valoresAProcesar = (restantes <= 100)
                        ? ordenes.GetRange(i, restantes)
                        : ordenes.GetRange(i, 100);

                    if (aplicacion.Contains("OriginacionMovil")) //Originacion
                    {
                        totalCanceladas += orden.CambiarEstatusOrdenes(valoresAProcesar, 2, true, false,
                            Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario)));
                    }
                    else
                    {
                        totalCanceladas += orden.Cancelar(valoresAProcesar, idUsuario);
                        
                    }
                }
            }
            if (respuesta == "")
            {
                if (totalCanceladas == 0)
                {
                    respuesta = "No se pudieron" + (aplicacion.Contains("OriginacionMovil") ? " rechazar " : " cancelar ") +
                                "las órdenes seleccionadas, inténtelo nuevamente más tarde.|-1";
                }
                else if (totalCanceladas > 1)
                    respuesta = "Se" + (aplicacion.Contains("OriginacionMovil") ? " rechazaron " : " cancelaron ") + totalCanceladas +
                                " órdenes.|-1";
                else
                {
                    respuesta = "Se" + (aplicacion.Contains("OriginacionMovil") ? " rechazó " : " canceló ") + totalCanceladas + " orden.|-1";
                }

            }
            Session["Respuesta"] = respuesta;
            Tiempos.Terminar(guidTiempos);
            return Json(new { Respuesta = respuesta, Tipo = 1 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReasignarOrdenes(string[][] ordenesList,string[] paginaActual)
        {
            var guidTiempos = Tiempos.Iniciar();
            ConexionSql sql = ConexionSql.Instance;
            var entOrden = new EntOrdenes();
            var ordenes = ordenesList.ToList();
            int idUsuario = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            string respuesta = "";
            var totalAutorizadasPrev = 0;
            var totalReasignadas = 0;
            var limiteGestor = Config.LimiteOrdenGestor;
            string paginaOrigen = "";
            var errorList = new List<string>();
            if (paginaActual != null)
            {
                paginaOrigen = paginaActual[0];
            }
            Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuario, Request.FilePath, "ReasignarOrdenes, Origen: " + paginaOrigen);

            try
            {
                var listaTexto = ordenesList.Select(x => Convert.ToInt32((x[0]))).ToList();
                Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuario, Request.FilePath, "ReasignarOrdenes, Origen: " + paginaOrigen + " Ordenes: " + String.Join(",", listaTexto));
            }
            catch{}


            var cuentaUsuario = new Dictionary<int, int>();

            for (int i = 0; i < ordenes.Count; i += 40)
            {
                var usuariosOrden = new Dictionary<int, string>();
                
                int restantes = ordenes.Count - i;
                var valoresAProcesar = (restantes <= 40)
                    ? ordenes.GetRange(i, restantes)
                    : ordenes.GetRange(i, 40);

                foreach (var ordenesInfo in valoresAProcesar)
                {
                    if (ordenesInfo[1] == "3")
                    {
                        try
                        {
                            if (cuentaUsuario.ContainsKey(idUsuario))
                            {
                                if (usuariosOrden.ContainsKey(idUsuario))
                                {
                                    var cuentaActual = cuentaUsuario[idUsuario]++;

                                    if (cuentaActual > limiteGestor)
                                    {
                                        errorList.Add(ordenesInfo[0] + " <- No se pudo asignar el crédito " +
                                                      "porque el usuario superó el limite de asignación permitido.");
                                    }
                                    else
                                    {
                                        usuariosOrden[idUsuario] += "," + ordenesInfo[0];
                                    }
                                }
                                else
                                {
                                    cuentaUsuario[idUsuario] = cuentaUsuario[idUsuario] + 1;
                                    var cuentaActual = cuentaUsuario[idUsuario];
                                    if (!(cuentaActual > limiteGestor))
                                    {
                                        usuariosOrden.Add(idUsuario,ordenesInfo[0].ToString(CultureInfo.InvariantCulture));
                                    }
                                    else
                                    {
                                        errorList.Add(ordenesInfo[0] + " <- No se pudo reasignar la orden " +
                                                      "porque el usuario superó el limite de asignación permitido.");
                                    }
                                }
                            }


                            else
                            {
                                int cuentaActual = sql.ObtenerNumeroAsignaciones(idUsuario) + 1;
                                if (!(cuentaActual > limiteGestor))
                                {
                                    cuentaUsuario.Add(idUsuario, cuentaActual);
                                    usuariosOrden.Add(idUsuario, ordenesInfo[0].ToString(CultureInfo.InvariantCulture));
                                }
                                else
                                {
                                    cuentaUsuario.Add(idUsuario, cuentaActual);
                                    errorList.Add(ordenesInfo[0] + " <- No se pudo reasignar la orden " +
                                                  "porque el usuario superó el limite de asignación permitido.");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuario,
                                Request.FilePath,
                                "Error: al asignar intentar asignar créditos - Usuario: " +
                                idUsuario + " - UsuarioPadre: " + idUsuario + " - Error: " +
                                ex.Message);
                        }
                    }
                    else
                        totalAutorizadasPrev++;
                }

                foreach (var usuarioOrden in usuariosOrden)
                {
                    totalReasignadas += entOrden.ActualizarEstatusUsuarioOrdenes(usuarioOrden.Value, 15, -1, true,true, idUsuario);
                }
            }
            var idErrorOrden = "-1";
            if (errorList.Count > 0)
            {
                var sbErrores = new StringBuilder();
                foreach (var l in errorList)
                {
                    sbErrores.Append(l);
                    sbErrores.Append(Environment.NewLine);
                }
                DataSet dsOrdenError = sql.InsertaOrdenesError(idUsuario, "Reasignacion", paginaOrigen, sbErrores.ToString());
                if (dsOrdenError.Tables.Count > 0 && dsOrdenError.Tables[0].Rows.Count > 0)
                {
                    idErrorOrden = dsOrdenError.Tables[0].Rows[0]["idErrorOrden"].ToString();
                }
            }
            if (respuesta == "")
            {
                respuesta = string.Format(
                    "Se reasignaron {0} órdenes, {1} estaban autorizadas {2}.|-1 ",
                    totalReasignadas, totalAutorizadasPrev, (errorList.Count > 0 ? (", se presentaron " + errorList.Count + " errores, identificador de error: " + ErrorHref.Replace("{EO}", idErrorOrden)) : ""));
            }
            Session["Respuesta"] = respuesta;
            Tiempos.Terminar(guidTiempos);
            return Json(new { Respuesta = respuesta, Tipo = 1 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DescargarError(String idError)
        {
            var guidTiempos = Tiempos.Iniciar();
            int idUsuario = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));

            ConexionSql conn = ConexionSql.Instance;
            try
            {
                //String filename = String.Format("error.txt");
                var resultado = new StringBuilder();
                String temporal;
                long contador = 0;
                Response.AddHeader("content-disposition", "attachment;filename=ErrorOrdenes.txt");
                Response.ContentType = "text/plain";
                do
                {
                    temporal = conn.ObtenerOrdenesErrorRango(idError, idUsuario, contador);
                    resultado.Append(temporal);
                    contador += 16384;
                    var buffer = Encoding.ASCII.GetBytes(temporal);
                    if (Response.IsClientConnected)
                    {
                        Response.OutputStream.Write(buffer, 0, buffer.Length);
                        Response.Flush();
                    }
                    else
                    {
                        Tiempos.Terminar(guidTiempos);
                        return null;
                    }
                } while (temporal.Length >= 16383);
                Response.Flush();
                Tiempos.Terminar(guidTiempos);
                return null;
            }
            catch (Exception)
            {
                Response.Flush();
                Tiempos.Terminar(guidTiempos);
                return null;
            }

        }

        public ActionResult ReversarOrdenes(string[][] ordenesList,string[] paginaActual)
        {
            var guidTiempos = Tiempos.Iniciar();
            var ordenes = ordenesList.Select(x => Convert.ToInt32((x[0]))).ToList();
            int idUsuario = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            int idRol = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol));
            var entFormulario = new EntFormulario().ObtenerListaFormularios(paginaActual[2]).FirstOrDefault(x => x.Captura == 1);
            var bloqueo=new EntFormulario().ObtenerBloqueoReverso(ordenes[0]);
            string respuesta = "";
            var totalCanceladas = 0;
            string paginaOrigen = paginaActual[0];

            Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuario, Request.FilePath, "ReversarOrdenes. Origen: " + paginaOrigen);

            if (!Convert.ToBoolean(bloqueo) && idRol == 3)
            {
                Session["Respuesta"] =
                    "No se pudo reversar la orden. Para reversar este dictamen se necesita autorización.|-1";
                Tiempos.Terminar(guidTiempos);
                return Json(new {Respuesta = respuesta, Tipo = 1}, JsonRequestBehavior.AllowGet);
            }

            if (entFormulario != null)
            {
                var orden = new Orden(idRol, Config.AplicacionActual().Productivo, Config.AplicacionActual().ClientId, Config.AplicacionActual().ProductId, entFormulario.Nombre, entFormulario.Version,null);

                for (int i = 0; i < ordenes.Count; i += 100)
                {
                    int restantes = ordenes.Count - i;
                    var valoresAProcesar = (restantes <= 100)
                        ? ordenes.GetRange(i, restantes)
                        : ordenes.GetRange(i, 100);
                    
                        totalCanceladas += orden.CambiarEstatusOrdenes(valoresAProcesar, 3, true,true,
                            Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario)));

                        if (Config.AplicacionActual().Productivo && Config.AplicacionActual().idAplicacion == 1) //Solo London
                        {
                            var objectList = valoresAProcesar.Cast<object>().ToList();
                            Gestiones.EnviarGestionBatch(objectList, "R", "3");
                            
                        }
                }
            }
            if (respuesta == "")
            {
                if (totalCanceladas == 0)
                {
                    respuesta = "No se pudieron reversar las órdenes seleccionadas, inténtelo nuevamente más tarde.|-1";
                }
                else if (totalCanceladas > 1)
                    respuesta = "Se reversaron " +  totalCanceladas + " órdenes.|-1";
                else
                {
                    respuesta = "Se reverso " +  totalCanceladas + " orden.|-1";
                }

            }
            Session["Respuesta"] = respuesta;
            Tiempos.Terminar(guidTiempos);
            return Json(new { Respuesta = respuesta, Tipo = 1 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReasignarOrdenesIncompletas(string[][] ordenesList, string[] paginaActual)
        {
            var guidTiempos = Tiempos.Iniciar();
            var ordenes = ordenesList.Select(x => Convert.ToInt32((x[0]))).ToList();
            var aplicacion = Config.AplicacionActual().Nombre;
            int idUsuario = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            int idRol = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol));
            var entFormulario = new EntFormulario().ObtenerListaFormularios(paginaActual[2]).FirstOrDefault(x => x.Captura == 1);
            string respuesta = "";
            var totalCanceladas = 0;
            string paginaOrigen = paginaActual[0];

            Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuario, Request.FilePath, "ReasignarOrdenesIncompletas. Origen: " + paginaOrigen);

            if (entFormulario != null)
            {
                var orden = new Orden(idRol, Config.AplicacionActual().Productivo, Config.AplicacionActual().ClientId, Config.AplicacionActual().ProductId, entFormulario.Nombre, entFormulario.Version,null);

                for (int i = 0; i < ordenes.Count; i += 100)
                {
                    int restantes = ordenes.Count - i;
                    var valoresAProcesar = (restantes <= 100)
                        ? ordenes.GetRange(i, restantes)
                        : ordenes.GetRange(i, 100);

                    if (aplicacion.Contains("OriginacionMovil"))
                    {
                        totalCanceladas += orden.ReenviarIncompletas(valoresAProcesar,idUsuario);
                    }
                    else
                    {
                        totalCanceladas += orden.CambiarEstatusOrdenes(valoresAProcesar, 3, true,false,
                            Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario)));
                    }

                }
            }
            if (respuesta == "")
            {
                if (totalCanceladas == 0)
                {
                    respuesta = "No se pudieron reenviar las órdenes seleccionadas, inténtelo nuevamente más tarde.|-1";
                }
                else if (totalCanceladas > 1)
                    respuesta = "Se reenviaron " +  totalCanceladas + " órdenes.|-1";
                else
                {
                    respuesta = "Se reenvió " +  totalCanceladas + " orden.|-1";
                }

            }
            Session["Respuesta"] = respuesta;
            Tiempos.Terminar(guidTiempos);
            return Json(new { Respuesta = respuesta, Tipo = 1 }, JsonRequestBehavior.AllowGet);
        }

    }
}