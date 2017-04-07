using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PubliPayments.Entidades;
using PubliPayments.Utiles;

namespace PubliPayments.Negocios
{
    public class ReporteGeneral
    {
        private readonly DateTime _fecha3000 = new DateTime(3000, 1, 1);

        public int GeneraReporteSupervisores()
        {
            int reportes;
            try
            {
                var usr = new EntUsuario();
                var ds = usr.ObtenerUsuariosPorRol(3);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    reportes = ds.Tables[0].Rows.Count;

                    Parallel.ForEach(ds.Tables[0].Rows.OfType<DataRow>(),
                        row => GeneraReporteGeneral(row["idUsuario"].ToString(), Convert.ToInt32(row["idRol"])));
                }
                else
                {
                    return -2;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "ReporteGeneral",
                    "GeneraReporteSupervisores - Error:" + ex.Message);

                return -1;
            }
            return reportes;
        }

        public void GeneraReporteGeneral(string idPadre, int idRol)
        {
            var guidTiempos = Tiempos.Iniciar();
            var reportes = new Reportes();
            try
            {
                var reporte = GeneraReporte(idPadre, idPadre, idRol, true);
                if (reporte.ToString().Length<5)
                {
                    Tiempos.Terminar(guidTiempos);
                    return;
                }
                ConexionSql conn = ConexionSql.Instance;
                DataSet estatusDataSet = conn.ObtenerEstatusReporte(Int32.Parse(idPadre), null, 1);
                int idReporte = -1;
                if (estatusDataSet != null && estatusDataSet.Tables.Count > 0)
                {
                    if (estatusDataSet.Tables[0].Rows.Count > 0)
                    {
                        idReporte = Convert.ToInt32(estatusDataSet.Tables[0].Rows[0]["idReporte"]);
                    }
                    reportes.InsertaReporteTexto(idReporte, reporte, int.Parse(idPadre), 1);
                }

            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, Convert.ToInt16(idPadre), "GeneraReporteGeneral",
                    "Error: Generar - " + ex.Message +
                    (ex.InnerException != null ? " - Inner: " + ex.InnerException.Message : ""));
                reportes.InsertaReporteTexto(-1,
                    new StringBuilder(
                        "Ocurrio un error al intentar generar el archivo, por favor intente mas tarde, si el problema continua reportelo a soporte."),
                        int.Parse(idPadre), 1);
            }
            Tiempos.Terminar(guidTiempos);
        }

        public void GeneraReporteSupervisoresDelegacion(int idUsuarioPadre)
        {
            var guidTiempos = Tiempos.Iniciar();
            var reporteGeneral=new StringBuilder();
            var reportes = new Reportes();
            try
            {
               var usr = new EntUsuario();
                 var supervisores = usr.ObtenerUsuarioPorDelegacion(idUsuarioPadre, 3);

                if (supervisores != null && supervisores.Tables.Count > 0 && supervisores.Tables[0].Rows.Count > 0)
                {
                    int i = 1;
                    foreach (var super in supervisores.Tables[0].Rows.OfType<DataRow>())
                    {

                        reporteGeneral.Append(GeneraReporte(super["idUsuario"].ToString(),
                            idUsuarioPadre.ToString(),Convert.ToInt32(super["idRol"]), (i == 1)));
                        i++;
                    }
                    ConexionSql conn = ConexionSql.Instance;
                    DataSet estatusDataSet = conn.ObtenerEstatusReporte(idUsuarioPadre, null, 1);
                    int idReporte = -1;
                    if (estatusDataSet != null && estatusDataSet.Tables.Count > 0)
                    {
                        if (estatusDataSet.Tables[0].Rows.Count > 0)
                        {
                            idReporte = Convert.ToInt32(estatusDataSet.Tables[0].Rows[0]["idReporte"]);
                        }
                        reportes.InsertaReporteTexto(idReporte, reporteGeneral, idUsuarioPadre, 1);
                    }
                }
               
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, Convert.ToInt16(idUsuarioPadre), "GeneraReporteSupervisoresDelegacion",
                  "Error: Generar - " + ex.Message +
                  (ex.InnerException != null ? " - Inner: " + ex.InnerException.Message : ""));
                reportes.InsertaReporteTexto(-1,
                    new StringBuilder(
                        "Ocurrio un error al intentar generar el archivo, por favor intente mas tarde, si el problema continua reportelo a soporte."),
                        idUsuarioPadre, 1);
            }
            Tiempos.Terminar(guidTiempos);
        }

        public StringBuilder GeneraReporte(string idPadre, string idUsuarioReporte, int idRol, bool cabecero)
        {
            var guidTiempos = Tiempos.Iniciar();
            var sb = new StringBuilder();
            if (idRol != 3)
            {
                Tiempos.Terminar(guidTiempos);
                return sb;
            }
            int idRolReporte = 0;
            if (idPadre!=idUsuarioReporte)
            {
              var usuario=  new EntUsuario().ObtenerUsuarioPorId(Convert.ToInt32(idUsuarioReporte));
                idRolReporte = usuario.idRol;
            }
            if (cabecero)
            {
                sb.AppendLine(
                "Gestor Ant" +
                ",Estatus final Ant" +
                ",Resultado final Ant" +
                ",Crédito" +
                ",Gestor" +
                ",Visita" +
                ",Estatus final" +
                ",Resultado final" +
                ",Orden" +
                ",Estatus" +
                ",Tipo de Formulario" +
                ",Fecha inicio gestión" +
                ",Fecha fin de gestión" +
                ",Fecha de recepción" +
                ",Fecha de aprobación" +
                ",Nombre acreditado" +
                ",Municipio" +
                ",Delegación" +
                ",CP" +
                ",colonia" +
                ",calle" +
                ",Teléfono casa" +
                ",Teléfono celular" +
                ",Act Municipio" +
                ",Act Estado" +
                ",Act Código Postal" +
                ",Act Colonia" +
                ",Act Edificio" +
                ",Act Entre Calle 1" +
                ",Act Entre Calle 2" +
                ",Act Teléfono Casa" +
                ",Act Teléfono Celular" +
                ",Act Correo" +
                ",Comentario Final" +
                ",Solución" +
                ",Etiqueta" +
                ",Saldo" +
                ",Meses Recuperar" +
                ",Monto Recuperar" +
                ",Pago mínimo" +
                ",Pago recomendado" +
                ",Pago tope" +
                ",Pago " + DateTime.Now.AddMonths(-1).ToString("MMMM", CultureInfo.CreateSpecificCulture("es-MX")) +
                ",Pago " + DateTime.Now.AddMonths(-2).ToString("MMMM", CultureInfo.CreateSpecificCulture("es-MX")) +
                ",Pago " + DateTime.Now.AddMonths(-3).ToString("MMMM", CultureInfo.CreateSpecificCulture("es-MX")) +
                ",Pago " + DateTime.Now.AddMonths(-4).ToString("MMMM", CultureInfo.CreateSpecificCulture("es-MX")) +
                ",Promesa Pago" +
                ",Fecha Promesa de pago" +
                ",Act Pago Mensual" +
                ", Acepta BCN" +
                ",Monto Convenio" +
                ",Producto Convenio" +
                ",Celular SMS" +
                ",Tipo Teléfono" +
                ",Canal" +
                ",Auxiliar" + (idRolReporte == 5 ? ",Supervisor,Despacho" : ""));
            }
            
            try
            {
                ConexionSql conn = ConexionSql.Instance;
                DataSet set = conn.ObtenerReporteGeneral(Int32.Parse(idPadre), Int32.Parse(idUsuarioReporte));
                DataTable resultado = set.Tables.Count > 0 ? set.Tables[0] : null;
                if (resultado == null || resultado.Rows.Count<2)
                {
                    Tiempos.Terminar(guidTiempos);
                    return new StringBuilder("");
                }
                int i = 0;
                do
                {
                    int incremento = 0;

                    if (i < resultado.Rows.Count - 1)
                        if (resultado.Rows[i]["idOrden"].ToString() == resultado.Rows[i + 1]["idOrden"].ToString())
                        {
                            string datosAnterior = string.Format("{0},{1},{2}", resultado.Rows[i+1]["GestorResp"],
                                resultado.Rows[i + 1]["Dictamen"],
                                (Convert.ToDateTime(resultado.Rows[i + 1]["Fecha"]) != _fecha3000
                                    ? "Valida no aprobada"
                                    : resultado.Rows[i + 1]["EstatusCodigo"] != null
                                        ? resultado.Rows[i + 1]["EstatusCodigo"].ToString() == "3"
                                            ? "Valida"
                                            : resultado.Rows[i + 1]["EstatusCodigo"].ToString() == "4"
                                                ? "Valida aprobada"
                                                : ""
                                        : ""));

                            sb.AppendLine(CrearRenglon(resultado.Rows[i], idRolReporte, datosAnterior));
                            incremento = 2;
                        }
                        else
                        {
                            sb.AppendLine(CrearRenglon(resultado.Rows[i], idRolReporte, ",,"));
                            incremento=1;
                        }
                    else
                    {
                        sb.AppendLine(CrearRenglon(resultado.Rows[i], idRolReporte, ",,"));
                        incremento=1;
                    }


                    i = i + incremento;
                } while (i<resultado.Rows.Count);

                sb.Replace('á', 'a');
                sb.Replace('é', 'e');
                sb.Replace('í', 'i');
                sb.Replace('ó', 'o');
                sb.Replace('ú', 'u');
                sb.Replace('ñ', 'n');

            }
            catch (Exception ex)
            {
                Tiempos.Terminar(guidTiempos);
                return new StringBuilder("Ocurrio un error al intentar generar el archivo, por favor intente mas tarde, si el problema continua reportelo a soporte.");
            }
            Tiempos.Terminar(guidTiempos);
            return sb;
        }
      
        private String CrearRenglon(DataRow row, int idRolReporte,string datosAnterior)
        {
            return
                String.Format(
                    "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},{36},{37},{38},{39},{40},{41},{42},{43},{44},{45},{46},{47},{48},{49},{50},{51},{52},{53}"
                    , datosAnterior
                    , row["num_cred"] ?? ""
                    , row["GestorOrd"] ?? ""
                    , row["idVisita"] ?? ""
                    , row["Dictamen"] ?? ""
                    ,
                    (Convert.ToDateTime(row["Fecha"]) != _fecha3000)
                        ? "Valida no aprobada"
                        : row["EstatusCodigo"] != null
                            ? row["EstatusCodigo"].ToString() == "3"
                                ? "Valida"
                                : row["EstatusCodigo"].ToString() == "4" ? "Valida aprobada" : ""
                            : ""
                    , row["idOrden"] ?? ""
                    ,
                    (Convert.ToDateTime(row["Fecha"]) != _fecha3000)
                        ? "Cancelada"
                        : row["idUsuario"] != null
                            ? (row["idUsuario"].ToString() != "0") ? row["Estatus"] : row["GestorOrd"]
                            : ""
                    , row["TIPO_FORMULARIO"] ?? ""
                    , row["InitialDate"] != null ? FechaFormato(row["InitialDate"].ToString()) : ""
                    , row["FinalDate"] != null ? FechaFormato(row["FinalDate"].ToString()) : ""
                    , row["FechaRecepcion"] ?? ""
                    ,
                    row["EstatusCodigo"] != null
                        ? (row["EstatusCodigo"].ToString() == "4") ? row["FechaModificacion"] : ""
                        : ""
                    , row["nombreAcreditado"] != null ? SinComas(row["nombreAcreditado"]) : ""
                    , row["municipio"] != null ? SinComas(row["municipio"]) : ""
                    , row["delegacion"] != null ? SinComas(row["delegacion"]) : ""
                    , row["cp"] != null ? SinComas(row["cp"]) : ""
                    , row["colonia"] != null ? SinComas(row["colonia"]) : ""
                    , row["calle"] != null ? SinComas(row["calle"]) : ""
                    , row["telefonoCasa"] ?? ""
                    , row["telefonoCelular"] ?? ""
                    , row["TX_MUNICIPIO_ACT"] != null ? SinComas(row["TX_MUNICIPIO_ACT"]) : ""
                    , row["TX_ESTADO_ACT"] != null ? SinComas(row["TX_ESTADO_ACT"]) : ""
                    , row["CV_CODIGO_POSTAL_ACT"] != null ? SinComas(row["CV_CODIGO_POSTAL_ACT"]) : ""
                    , row["TX_COLONIA_ACT"] != null ? SinComas(row["TX_COLONIA_ACT"]) : ""
                    , row["TX_EDIFICIO_ACT"] != null ? SinComas(row["TX_EDIFICIO_ACT"]) : ""
                    , row["TX_ENTRE_CALLE1_ACT"] != null ? SinComas(row["TX_ENTRE_CALLE1_ACT"]) : ""
                    , row["TX_ENTRE_CALLE2_ACT"] != null ? SinComas(row["TX_ENTRE_CALLE2_ACT"]) : ""
                    , row["NU_TELEFONO_CASA_ACT"] != null ? SinComas(row["NU_TELEFONO_CASA_ACT"]) : ""
                    , row["NU_TELEFONO_CELULAR_ACT"] != null ? SinComas(row["NU_TELEFONO_CELULAR_ACT"]) : ""
                    , row["TX_CORREO_ELECTRONICO_ACT"] != null ? SinComas(row["TX_CORREO_ELECTRONICO_ACT"]) : ""
                    , row["comentario_final"] != null ? SinComas(row["comentario_final"]) : ""
                    , row["soluciones"] != null ? SinComas(row["soluciones"]) : ""
                    , row["Etiqueta"] ?? ""
                    ,
                    row["saldo"] != null
                        ? (Convert.ToDouble(row["saldo"], CultureInfo.InvariantCulture)).ToString(
                            CultureInfo.InvariantCulture)
                        : " "
                    ,
                    row["mesesRecuperar"] != null
                        ? (Convert.ToDouble(row["mesesRecuperar"], CultureInfo.InvariantCulture)).ToString(
                            CultureInfo.InvariantCulture)
                        : " "
                    ,
                    row["montoRecuperar"] != null
                        ? (Convert.ToDouble(row["montoRecuperar"], CultureInfo.InvariantCulture)).ToString(
                            CultureInfo.InvariantCulture)
                        : " "
                    ,
                    row["pagoMinimo"] != null
                        ? (Convert.ToDouble(row["pagoMinimo"], CultureInfo.InvariantCulture)).ToString(
                            CultureInfo.InvariantCulture)
                        : " "
                    ,
                    row["pagoRecomendado"] != null
                        ? (Convert.ToDouble(row["pagoRecomendado"], CultureInfo.InvariantCulture)).ToString(
                            CultureInfo.InvariantCulture)
                        : " "
                    ,
                    row["pagoTope"] != null
                        ? (Convert.ToDouble(row["pagoRecomendado"], CultureInfo.InvariantCulture)).ToString(
                            CultureInfo.InvariantCulture)
                        : " "
                    , row["pago_1mes"] ?? ""
                    , row["pago_2mes"] ?? ""
                    , row["pago_3mes"] ?? ""
                    , row["pago_4mes"] ?? ""
                    , row["promPago"] != null ? SinComas(row["promPago"]) : ""
                    , row["FH_PROMESA_PAGO"] != null ? SinComas(row["FH_PROMESA_PAGO"]) : ""
                    , row["ppagoMensualAct"] != null ? SinComas(row["ppagoMensualAct"]) : ""
                    , row["aceptaBCN"] != null ? SinComas(row["aceptaBCN"]) : ""
                    ,
                    (row["IM_MONTO_MENSUALIDAD_PESOS"] != null && row["IM_MONTO_MENSUALIDAD_PESOS"].ToString() != "")
                        ? (Convert.ToDouble(row["IM_MONTO_MENSUALIDAD_PESOS"], CultureInfo.InvariantCulture)).ToString(
                            CultureInfo.InvariantCulture)
                        : " "
                    , row["CV_PRODUCTO_CONVENIO"] ?? ""
                    , row["TEL_CELULAR_SMS"] ?? ""
                    , row["TIPO_TEL"] ?? ""
                    , row["CV_CANAL"] ?? ""
                    , row["Auxiliar"] != null ? SinComas(row["Auxiliar"]) : "") +
                    (idRolReporte == 5 ? String.Format(",{0},{1}", row["usuarioPadre"] ?? "", row["dominio"] ?? "") : "");
        }

        private string SinComas(Object valor)
        {
            string respuesta = "";
            if (valor != null)
            {
                respuesta = valor.ToString();
                respuesta = respuesta.Replace(",", " ");
            }
            return respuesta;
        }

        private string FechaFormato(string valor)
        {
            if (valor == "")
            {
                return "";
            }
            valor = valor.Length > 19 ? valor.Substring(0, 19) : valor;
            string resultado;
            try
            {
                resultado = (valor != "")
                    ? DateTime.ParseExact(valor, "yyyyMMdd HH:mm:ss", CultureInfo.CurrentCulture)
                        .ToString(CultureInfo.CurrentCulture)
                    : "";
            }
            catch (Exception)
            {

                resultado = valor;
            }
            return resultado;
        }

    }
}
