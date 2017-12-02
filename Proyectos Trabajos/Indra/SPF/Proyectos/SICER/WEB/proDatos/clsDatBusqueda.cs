using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using proEntidad;
using proSeguridad;
using System.Transactions;

namespace proDatos
{
    public class clsDatBusqueda
    {
        #region BUSQUEDA
        public static List<spuConsultarDatosIntegranteInternoExterno_Result> Busqueda(string empPaterno, string empMaterno, string empNombre, string empCURP, string empRFC, string empNumero, string participante, ref string alerta)
        {

            sicerEntities conext_Entiti;//EXINEntities conext_Entiti;
            List<spuConsultarDatosIntegranteInternoExterno_Result> lst = new List<spuConsultarDatosIntegranteInternoExterno_Result>();

            string strConnection_Entiti = clsDatConexion.getConnectionString_Entity((clsEntSesion)System.Web.HttpContext.Current.Session["objSession" + System.Web.HttpContext.Current.Session.SessionID]);
            //Guid idEmpleado = Guid.Empty;
            using ((conext_Entiti = new sicerEntities((strConnection_Entiti))))
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    try
                    {
                        lst = conext_Entiti.spuConsultarDatosIntegranteInternoExterno(
                               empPaterno,
                               empMaterno,
                               empNombre,
                               empCURP,
                               empRFC,
                               empNumero,
                               participante).ToList();



                    }
                    catch (Exception e)
                    {
                        alerta = e.Message;
                    }
                    ts.Complete();
                }
            }
            return lst;

        }


         public static clsEntResponseValidaCurp validaCURP(string curp)
        {

            sicerEntities conext_Entiti;
            clsEntResponseValidaCurp objResponse = new clsEntResponseValidaCurp();
            objResponse.alerta = String.Empty;
            string strConnection_Entiti = clsDatConexion.getConnectionString_Entity((clsEntSesion)System.Web.HttpContext.Current.Session["objSession" + System.Web.HttpContext.Current.Session.SessionID]);
            
            //Guid idEmpleado = Guid.Empty;
            using ((conext_Entiti = new sicerEntities((strConnection_Entiti))))
            {
                    try
                    {
                        System.Data.Objects.ObjectParameter parResultado = new System.Data.Objects.ObjectParameter("resultado", typeof(bool));
                        System.Data.Objects.ObjectParameter parNombreParticipante = new System.Data.Objects.ObjectParameter("nombreParticipante", typeof(int));

                        conext_Entiti.spuValidarCURP(curp, parResultado, parNombreParticipante);

                        objResponse.nombreParticipante = Convert.ToString(parNombreParticipante.Value);
                        objResponse.respuesta = Convert.ToString(parResultado.Value);
                            
                        
                    }
                    catch (Exception e)
                    {
                        objResponse.alerta = e.Message;
                    }
             
            }
            return objResponse;

        }


        #endregion

        #region consulta registro.registro

        public static spuConsultarRegistro_Result Consulta(int idRegistro,Guid idEmpleado)
        {
            sicerEntities conext_Entiti;
            //List<clsEntResponseInsertaRegistro> objResult = new List<clsEntResponseInsertaRegistro>();

            spuConsultarRegistro_Result resultado = new spuConsultarRegistro_Result();
            int idUsuario = (int)((clsEntSesion)System.Web.HttpContext.Current.Session["objSession" + System.Web.HttpContext.Current.Session.SessionID]).Usuario.IdUsuario;

            string strConnection_Entiti = clsDatConexion.getConnectionString_Entity((clsEntSesion)System.Web.HttpContext.Current.Session["objSession" + System.Web.HttpContext.Current.Session.SessionID]);
            using (conext_Entiti = new sicerEntities(strConnection_Entiti))
            {
                using (TransactionScope ts = new TransactionScope())
                {

                     resultado = conext_Entiti.spuConsultarRegistro(idRegistro, idEmpleado).FirstOrDefault();

                }
            }

            return resultado;

        }
        #endregion consulta registro.registro

        #region consultaGRID
        public static List<spuConsultarCertificacionIE_Result> ConsultaRegistrosCertificacion(int idRegistro)
        {
            sicerEntities conext_Entiti;
            List<clsEntResponseInsertaRegistro> objResult = new List<clsEntResponseInsertaRegistro>();
            List<clsEntRegistroCert> objResult1 = new List<clsEntRegistroCert>();
            List<spuConsultarCertificacionIE_Result> respuesta = new List<spuConsultarCertificacionIE_Result>();

            int idUsuario = (int)((clsEntSesion)System.Web.HttpContext.Current.Session["objSession" + System.Web.HttpContext.Current.Session.SessionID]).Usuario.IdUsuario;

            string strConnection_Entiti = clsDatConexion.getConnectionString_Entity((clsEntSesion)System.Web.HttpContext.Current.Session["objSession" + System.Web.HttpContext.Current.Session.SessionID]);
            using (conext_Entiti = new sicerEntities(strConnection_Entiti))
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    respuesta = conext_Entiti.spuConsultarCertificacionIE(idRegistro).ToList();
                    return respuesta;

                }
            }


        }

        #endregion


        public static List<spuConsultaUbicacionInterno_Result> ConsultaUbicacionInterno(Guid idEmpleado)
        {
            sicerEntities conext_Entiti;
            List<clsEntResponseInsertaRegistro> objResult = new List<clsEntResponseInsertaRegistro>();
            List<clsEntRegistroCert> objResult1 = new List<clsEntRegistroCert>();

            int idUsuario = (int)((clsEntSesion)System.Web.HttpContext.Current.Session["objSession" + System.Web.HttpContext.Current.Session.SessionID]).Usuario.IdUsuario;

            string strConnection_Entiti = clsDatConexion.getConnectionString_Entity((clsEntSesion)System.Web.HttpContext.Current.Session["objSession" + System.Web.HttpContext.Current.Session.SessionID]);
            using (conext_Entiti = new sicerEntities(strConnection_Entiti))
            {
                using (TransactionScope ts = new TransactionScope())
                {

                    return conext_Entiti.spuConsultaUbicacionInterno(idEmpleado).ToList();

                }
            }
        }
    }
}
