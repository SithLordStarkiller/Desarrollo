using System;
using System.Globalization;
using System.Data;
using System.Text;
using System.Web.Mvc;
using PubliPayments.Entidades;

namespace PubliPayments.Controllers
{
    public class ArchivoRespuestasController : Controller
    {
        private DateTime _fecha3000 = new DateTime(3000,1,1);
        private String CrearRenglon(DataRow row)
        {
            return String.Format("{0}" +
                                ",{1}" +
                                ",{2}" +
                                ",{3}" +
                                ",{4}" +
                                ",{5}" +
                                ",{6}" +
                                ",{7}" +
                                ",{8}" +
                                ",{9}" +
                                ",{10}" +
                                ",{11}" +
                                ",{12}" +
                                ",{13}" +
                                ",{14}" +
                                ",{15}" +
                                ",{16}" +
                                ",{17}" +
                                ",{18}" +
                                ",{19}" +
                                ",{20}" +
                                ",{21}" +
                                ",{22}" +
                                ",{23}" +
                                ",{24}" +
                                ",{25}" +
                                ",{26}" +
                                ",{27}" +
                                ",{28}" +
                                ",{29}" +
                                ",{30}" +
                                ",{31}" +
                                ",{32}" +
                                ",{33}" +
                                ",{34}" +
                                ",{35}" 

                                , row["num_cred"]!=null? SinComas(row["num_cred"]):""
                                , (Convert.ToDateTime( row["Fecha"]) != _fecha3000 ) ? "Anterior" : "Actual"
                                , row["Dictamen"] ?? ""
                                , (Convert.ToDateTime(row["Fecha"]) != _fecha3000) ? "Valida no aprobada" : row["EstatusCodigo"] != null ? (row["EstatusCodigo"].ToString().Contains("3")) ? "Valida" : (row["EstatusCodigo"].ToString().Contains("4")) ? "Valida aprobada" : "" : ""
                                , (Convert.ToDateTime(row["Fecha"]) != _fecha3000) ? "Cancelada" : row["Estatus"].ToString()
                                , row["idOrden"] ?? ""
                                , row["TIPO_FORMULARIO"] ?? ""
                                , row["InitialDate"]!=null? FechaFormato(row["InitialDate"].ToString()):""
                                , row["FinalDate"] != null ? FechaFormato(row["FinalDate"].ToString()) : ""
                                , row["FechaRecepcion"] ?? ""
                                , row["EstatusCodigo"] != null ? (row["EstatusCodigo"].ToString().Contains("4")) ? row["FechaModificacion"] : "" : ""
                                , row["Etiqueta"] ?? ""
                                , SinComas(row["AssignedTo"])
                                , SinComas(row["nombre"])
                                , SinComas(row["municipio"])
                                , SinComas(row["cp"])
                                , SinComas(row["estado"])
                                , SinComas(row["TX_CORREO_ELECTRONICO_ACT"])
                                , SinComas(row["FH_NACIMIENTO"])
                                , SinComas(row["promPago"])
                                , SinComas(row["FH_PROMESA_PAGO"])
                                , SinComas(row["aceptaBCN"])
                                , SinComas(row["ppagoMensualAct"])
                                , SinComas(row["NU_TELEFONO_CASA_ACT"])
                                , SinComas(row["NU_TELEFONO_CELULAR_ACT"])
                                , SinComas(row["TX_EDIFICIO_ACT"])
                                , SinComas(row["TX_MUNICIPIO_ACT"])
                                , SinComas(row["TX_COLONIA_ACT"])
                                , SinComas(row["TX_ESTADO_ACT"])
                                , SinComas(row["CV_CODIGO_POSTAL_ACT"])
                                , SinComas(row["TX_ENTRE_CALLE1_ACT"])
                                , SinComas(row["TX_ENTRE_CALLE2_ACT"])
                                , SinComas(row["comentario_final"])
                                , (row["IM_MONTO_MENSUALIDAD_PESOS"] != null && row["IM_MONTO_MENSUALIDAD_PESOS"].ToString() != "") ? (Convert.ToDouble(row["IM_MONTO_MENSUALIDAD_PESOS"], CultureInfo.InvariantCulture)).ToString(CultureInfo.InvariantCulture) : " "
                                , row["CV_PRODUCTO_CONVENIO"] ?? ""
                                , row["CV_CANAL"] ?? "");

        }
        public ActionResult Descargar(String idUsuarioPadre,String tipoFormulario)
        {
            string usuario = SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario);

            if (usuario != idUsuarioPadre)
                return File(Encoding.UTF8.GetBytes("Error"),
                        "test/plain",
                        "Respuestas.csv");

            var sb = new StringBuilder();
            sb.AppendLine(
                          "Crédito" +
                          ",Visita" +
                          ",Estatus final" +
                          ",Resultado final" +
                          ",Estatus" +
                          ",Orden" +
                          ",Tipo Formulario"+
                          ",Fecha inicio gestión" +
                          ",Fecha fin de gestión" +
                          ",Fecha de recepción" +
                          ",Fecha de aprobación" +
                          ",Etiqueta" +
                          ",Usuario" +
                          ",Nombre" +
                          ",Municipio" +
                          ",CP" +
                          ",Delegación" +
                          ",Act Correo" +
                          ",Fecha Nacimiento" +
                          ",Promesa Pago" +
                          ",Fecha Promesa de pago" +
                          ", Acepta BCN" +
                          ",Act Pago Mensual" +
                          ",Act Teléfono Casa" +
                          ",Act Teléfono Celular" +
                          ",Act Edificio" +
                          ",Act Municipio" +
                          ",Act Colonia" +
                          ",Act Estado" +
                          ",Act Código Postal" +
                          ",Act Entre Calle 1" +
                          ",Act Entre Calle 2" +
                          ",Comentario Final" +
                          ",Monto Convenio" +
                          ",Tipo Convenio"+
                          ",Canal");

            ConexionSql conn = ConexionSql.Instance;
            DataSet set = conn.ObtenerRespuestasBitacora(0, "0", 0, Int32.Parse(idUsuarioPadre), tipoFormulario);
            DataTable resultado = set.Tables[0];

            foreach (DataRow row in resultado.Rows)
            {
                sb.AppendLine(CrearRenglon(row));

            }


            sb.Replace('á', 'a');
            sb.Replace('é', 'e');
            sb.Replace('í', 'i');
            sb.Replace('ó', 'o');
            sb.Replace('ú', 'u');
            sb.Replace('ñ', 'n');

            return File(Encoding.UTF8.GetBytes(sb.ToString()),
                        "test/plain",
                        "Respuestas.csv");
        }
       
        private string SinComas(Object valor)
        {
            string respuesta = "";
            if (valor != null)
            {
                respuesta= valor.ToString();
                respuesta = respuesta.Replace(",", " ");
            }
            return respuesta;
        }

        private string FechaFormato24(string valor)
        {
            valor = valor.Length > 19 ? valor.Substring(0, 19) : valor;
            string resultado;
            try
            {
                resultado = (valor != "") ? DateTime.ParseExact(valor, "yyyyMMdd HH:mm:ss", CultureInfo.CurrentUICulture).ToString(CultureInfo.CurrentUICulture) : "";
                
            }
            catch (Exception)
            {

                resultado = valor;
            }
            return resultado;
        }

        private string FechaFormato(string valor)
        {
            if (valor=="")
            {
                return "";
            }
            valor = valor.Length > 19 ? valor.Substring(0, 19) : valor;
            string resultado;
            try
            {
                resultado = (valor != "") ? DateTime.ParseExact(valor, "yyyyMMdd HH:mm:ss", CultureInfo.CurrentCulture).ToString(CultureInfo.CurrentCulture) : "";
            }
            catch (Exception)
            {

                resultado = valor;
            }
            return resultado;
        }
    }
}