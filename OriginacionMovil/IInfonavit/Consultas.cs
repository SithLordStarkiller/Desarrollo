using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using IInfonavit.Infonavit;
using IInfonavit.PrecalificacionMejoravit2;
using PubliPayments.Utiles;

namespace IInfonavit
{
    public class Consultas
    {
        public ProgramInterfaceWs_salida Precalifica(ProgramInterfaceWs_entrada entrada)
        {
            ProgramInterfaceWs_salida salida = null;
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                 var proxy = new SOCLDR12PortClient();
                var input = new ProgramInterface {ws_entrada = entrada};
                var ouput = proxy.SOCLDR12Operation(input);
                salida = ouput.ws_salida;
            }
            catch
            {
                salida = null;
            }
            return salida;
        }

        public WsRespuestaPreca PrecalificaNew(ProgramInterfaceWs_entrada entrada, string usuario,
            string nombreOficina, string oficina)
        {
            WsRespuestaPreca salida = null;
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                var proxy = new WSPrecalificacionMejoravitAppSoapClient();
                var output =(WsRespuestaPreca) proxy.PrecaMejoravit(entrada.ws_nss, usuario, entrada.ws_pen_alim, nombreOficina, oficina);
                var model = new PrecalificaModel {ws_nss = entrada.ws_nss,usuario = usuario,ws_pen_alim = entrada.ws_pen_alim,nombreOficina = nombreOficina,oficina = oficina};

                var entidad = SerializeXML.SerializeObject(model);

                Logger.WriteLine(Logger.TipoTraceLog.Log, 0, "TraceServiciosYY", "RequestPrecalifica - " + entidad);

                var LogRequest = SerializeXML.SerializeObject(output);

                Logger.WriteLine(Logger.TipoTraceLog.Log, 0, "TraceServiciosYY", "ResultadoPrecalifica - " + LogRequest);

                if (output != null)
                {
                    salida = output;
                }
            }
            catch(Exception ex)
            {
                salida = null;
            }
            return salida;
        }
        /*
        private ProgramInterfaceWs_salida OutputNewToOutputOld(WsRespuestaPreca o)
        {
            var s = new ProgramInterfaceWs_salida();
            try
            {
                s.ws_codigo = o.WSidMensaje.Trim();
                s.ws_mensaje = o.WSMensaje.Trim();
                s.ws_nrp_pun = o.WSNumRegistroPatronal.Trim();
                s.ws_rfc = o.WSRFC.Trim();
                s.ws_nom = o.WSNombreTitular.Trim();
                s.ws_pat = "";
                s.ws_mat = "";
                s.ws_sex = "0";
                s.ws_eda = "0";
                s.ws_curp = o.WSCurp.Trim();
                s.ws_edo_muni = "0";
                s.ws_edo_mun_nom = "";
                s.ws_pun_cal = o.WSPuntMin.Trim();
                s.ws_smd_df = "0.0";
                s.ws_sal_dia_mon = "0.0";
                s.ws_sal_dia_pun = "0.0";
                s.ws_est_sol = "";
                s.ws_sdo_s92 = "0.0";
                s.ws_sdo_s97 = "0.0";
                s.ws_sdo_sar = "0.0";
                s.ws_bim_fdo = "000";
                s.ws_bim_sar = "000";
                s.ws_ano_bim_top = "1900";
                s.ws_eda_sal_pto = "0";
                s.ws_sdo_sar_pto = "0";
                s.ws_bim_sar_pto = "0";
                s.ws_tot_pto = o.WSPuntTotal;
                s.ws_tot_ir = "0";
                s.ws_tas_int = "0.0";
                s.ws_num_plazos = o.WSNumPlazos;
                var plazos = string.IsNullOrEmpty(o.WSNumPlazos)?0:Convert.ToInt32(o.WSNumPlazos);
                var wsOcur = new ProgramInterfaceWs_salidaWs_ocur[plazos];
                for (var i = 0; i < plazos; i++)
                {
                    wsOcur[i] = new ProgramInterfaceWs_salidaWs_ocur
                    {
                        ws_plazo = o.WS_Ocur[i].Plazo.Trim(),
                        ws_mto_credo = o.WS_Ocur[i].MontoCredito.Trim(),
                        ws_pago = o.WS_Ocur[i].PagoMensual.Trim(),
                        ws_contarias = o.WS_Ocur[i].Contarias.Trim()
                    };
                }
                s.ws_ocur = wsOcur;
                s.ws_gastos = string.IsNullOrEmpty(o.WSGastosApertura) ? "0" : o.WSGastosApertura.Trim();
            }
            catch (Exception e)
            {
                s.ws_codigo = "9999";
                s.ws_mensaje = "Error al consultar el nss: " + e.Message;
            }
            return s;
        }*/
    }

    public class PrecalificaModel
    {
        public string ws_nss { get; set; }
        public string usuario { get; set; }
        public string ws_pen_alim { get; set; }
        public string nombreOficina { get; set; }
        public string oficina { get; set; }
    }
}
