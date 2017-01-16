using System;
using System.Globalization;
using System.Data;
using System.Text;
using System.Web.Mvc;
using PubliPayments.Entidades;

namespace PubliPayments.Controllers
{
    public class OrdenesNoAsignadasController : Controller
    {
        public ActionResult Descargar(String idUsuarioPadre = "0", String idDominio = "0", String tipoFormulario="0")
        {
            var sb = new StringBuilder();
            sb.AppendLine("Crédito" +
                          ",Delegación" +
                          ",Saldo" +
                          ",Solución" +
                          ",Tipo Formulario" +
                          ",Estatus"+
                          ",Visita" +
                          ",Meses Recuperar" +
                          ",Monto Recuperar" +
                          ",Pago mínimo" +
                          ",Pago recomendado" +
                          ",Pago tope" +
                          ",Nombre acreditado" +
                          ",Calle" +
                          ",Colonia" +
                          ",Municipio" +
                          ",Cp" +
                          ",Teléfono casa" +
                          ",Teléfono celular" +
                          ",Pago " + DateTime.Now.AddMonths(-1).ToString("MMMM", CultureInfo.CreateSpecificCulture("es-MX")) +
                          ",Pago " + DateTime.Now.AddMonths(-2).ToString("MMMM", CultureInfo.CreateSpecificCulture("es-MX")) +
                          ",Pago " + DateTime.Now.AddMonths(-3).ToString("MMMM", CultureInfo.CreateSpecificCulture("es-MX")) +
                          ",Pago " + DateTime.Now.AddMonths(-4).ToString("MMMM", CultureInfo.CreateSpecificCulture("es-MX")) +
                          ",Canal"+
                          ",Auxiliar");
            int _idRol = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol));
            string usuario = _idRol == 2 || _idRol == 0? "0": (SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            string dominio = SessionUsuario.ObtenerDato(SessionUsuarioDato.IdDominio);

            if ((usuario!=idUsuarioPadre) | (dominio!=idDominio))
                return File(Encoding.UTF8.GetBytes("Error"),
                        "test/plain",
                        "Creditos.csv");

            ConexionSql conn = ConexionSql.Instance;
            DataSet set = conn.ObtenerCreditosNoAsignados(Convert.ToInt32(idUsuarioPadre), Convert.ToInt32(idDominio), tipoFormulario);
            DataTable creditos = set.Tables[0];

            foreach (DataRow row in creditos.Rows)
            {
                sb.AppendLine(String.Format("{0}" +
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
                                            ",{24}"
                                            , row["num_cred"] ?? ""
                                            , row["delegacion"]!=null?SinComas(row["delegacion"]):""
                                            , (Convert.ToDouble((row["saldo"] ?? "0"), CultureInfo.InvariantCulture)).ToString(CultureInfo.InvariantCulture)
                                            , row["soluciones"]!=null?SinComas(row["soluciones"]):""
                                            , row["TIPO_FORMULARIO"] ?? ""
                                            , row["idusuario"]!=null? row["idusuario"].ToString()=="0"? "No asignado":"":""
                                            , row["idVisita"] ?? ""
                                            , row["mesesRecuperar"]!=null?SinComas(row["mesesRecuperar"]):""
                                            , row["montoRecuperar"]!=null?(Convert.ToDouble(row["montoRecuperar"], CultureInfo.InvariantCulture)).ToString(CultureInfo.InvariantCulture):""
                                            , row["pagoMinimo"]!=null?(Convert.ToDouble(row["pagoMinimo"], CultureInfo.InvariantCulture)).ToString(CultureInfo.InvariantCulture):""
                                            , row["pagoRecomendado"]!=null?(Convert.ToDouble(row["pagoRecomendado"], CultureInfo.InvariantCulture)).ToString(CultureInfo.InvariantCulture):""
                                            , row["pagoTope"]!=null?(Convert.ToDouble(row["pagoTope"], CultureInfo.InvariantCulture)).ToString(CultureInfo.InvariantCulture):""
                                            , row["nombre"]!=null? SinComas(row["nombre"]):""
                                            , row["calle"]!=null?SinComas(row["calle"]):""
                                            , row["colonia"]!=null?SinComas(row["colonia"]):""
                                            , row["municipio"]!=null?SinComas(row["municipio"]):""
                                            , row["cp"]!=null?SinComas(row["cp"]):""
                                            , row["telefonoCasa"]!=null?SinComas(row["telefonoCasa"]):""
                                            , row["telefonoCelular"]!=null?SinComas(row["telefonoCelular"]):""
                                            , row["pago_1mes"]!=null?SinComas(row["pago_1mes"]):""
                                            , row["pago_2mes"]!=null?SinComas(row["pago_2mes"]):""
                                            , row["pago_3mes"]!=null? SinComas(row["pago_3mes"]):""
                                            , row["pago_4mes"]!=null? SinComas(row["pago_4mes"]):""
                                            , row["CV_CANAL"] ?? ""
                                            , row["auxiliar"]!=null?SinComas(row["auxiliar"]):""));
            }
        

            sb.Replace('á', 'a');
            sb.Replace('é', 'e');
            sb.Replace('í', 'i');
            sb.Replace('ó', 'o');
            sb.Replace('ú', 'u');
            sb.Replace('ñ', 'n');
            sb.Replace('Á', 'A');
            sb.Replace('É', 'E');
            sb.Replace('Í', 'I');
            sb.Replace('Ó', 'O');
            sb.Replace('Ú', 'U');

            return File(Encoding.UTF8.GetBytes(sb.ToString()),
                        "test/plain",
                        "Creditos.csv");
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
    }

}



