using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using proEntidad;
using proSeguridad;

namespace proDatos
{
    public class clsDatExamen
    {
        public static List<spuConsultarTemasCertificacionExamen_Result> consultaTemasdeCertificacion(int idCertificacion)
        {
            #region Inicialización de objetos
            sicerEntities context_Entiti;
            List<spuConsultarTemasCertificacionExamen_Result> lista = new List<spuConsultarTemasCertificacionExamen_Result>();
            #endregion

           

            #region Conexion
            string strConnection_Entiti = clsDatConexion.getConnectionString_Entity();
            #endregion

            #region Response
            using (context_Entiti = new sicerEntities(strConnection_Entiti))
            {
                lista = context_Entiti.spuConsultarTemasCertificacionExamen(idCertificacion).ToList();
                return lista;
            }
            #endregion
        }

        public static List<spuConsultarFuncionesTema_Result> consultaFuncionesTema(int idTema)
        {
            #region Inicialización de objetos
            sicerEntities context_Entiti;
            List<spuConsultarFuncionesTema_Result> lista = new List<spuConsultarFuncionesTema_Result>();
            #endregion


            #region Conexion
            string strConnection_Entiti = clsDatConexion.getConnectionString_Entity();
            #endregion

            #region Response
            using (context_Entiti = new sicerEntities(strConnection_Entiti))
            {
                lista = context_Entiti.spuConsultarFuncionesTema(idTema).ToList();
                return lista;
            }
            #endregion
        }


        public static List<spuConsultarPreguntasTema_Result> consultaPreguntasTema(int idTema)
        {
            #region Inicialización de objetos
            sicerEntities context_Entiti;
            List<spuConsultarPreguntasTema_Result> lista = new List<spuConsultarPreguntasTema_Result>();
            #endregion


            #region Conexion
            string strConnection_Entiti = clsDatConexion.getConnectionString_Entity();
            #endregion

            #region Response
            using (context_Entiti = new sicerEntities(strConnection_Entiti))
            {
                lista = context_Entiti.spuConsultarPreguntasTema(idTema).ToList();
                return lista;
            }
            #endregion
        }


        public static List<spuConsultarRespuestasTema_Result> consultaRespuestasTema(int idTema)
        {
            #region Inicialización de objetos
            sicerEntities context_Entiti;
            List<spuConsultarRespuestasTema_Result> lista = new List<spuConsultarRespuestasTema_Result>();
            #endregion


            #region Conexion
            string strConnection_Entiti = clsDatConexion.getConnectionString_Entity();
            #endregion

            #region Response
            using (context_Entiti = new sicerEntities(strConnection_Entiti))
            {
                lista = context_Entiti.spuConsultarRespuestasTema(idTema).ToList();
                return lista;
            }
            #endregion
        }

        public static List<spuConsultarCertificacionesRegistro_Result> consultaCertificacionesRegistro(int idRegistro)
        {
            #region Inicialización de objetos
            sicerEntities context_Entiti;
            List<spuConsultarCertificacionesRegistro_Result> lista = new List<spuConsultarCertificacionesRegistro_Result>();
            #endregion


            #region Conexion
            string strConnection_Entiti = clsDatConexion.getConnectionString_Entity();
            #endregion

            #region Response
            using (context_Entiti = new sicerEntities(strConnection_Entiti))
            {
                lista = context_Entiti.spuConsultarCertificacionesRegistro(idRegistro).ToList();
                return lista;
            }
            #endregion
        }
        

        

        public static List<spuConsultarCertificacion_Result> consultarCertificacion(int idCertificacion)
        {
            #region Inicialización de objetos
            sicerEntities context_Entiti;
            List<spuConsultarCertificacion_Result> lista = new List<spuConsultarCertificacion_Result>();
            #endregion

            #region Revisión de session
            if (System.Web.HttpContext.Current.Session["objSession" + System.Web.HttpContext.Current.Session.SessionID] == null)
                return new List<spuConsultarCertificacion_Result>();
            #endregion
            
            #region Conexion
            string strConnection_Entiti = clsDatConexion.getConnectionString_Entity((clsEntSesion)System.Web.HttpContext.Current.Session["objSession" + System.Web.HttpContext.Current.Session.SessionID]);
            #endregion


            #region Response
            using (context_Entiti = new sicerEntities(strConnection_Entiti))
            {
                lista = context_Entiti.spuConsultarCertificacion(idCertificacion).ToList();
                return lista;
            }
            #endregion


        }



        public static int validaIngresoCertificacion(int idCertificacionRegistro, int idRegistro)
        {
            #region Inicialización de objetos
            sicerEntities context_Entiti;
            int respuesta=0;
            string alerta;
            #endregion


            #region Conexion
            string strConnection_Entiti = clsDatConexion.getConnectionString_Entity();
            #endregion



            #region Response
            using (context_Entiti = new sicerEntities(strConnection_Entiti))
            {
                    try
                    {
                        System.Data.Objects.ObjectParameter @idValidacion = new System.Data.Objects.ObjectParameter("idValidacion", typeof(int));

                        context_Entiti.spuValidaIngresoCertificacion(
                            idCertificacionRegistro,
                            idRegistro,
                            idValidacion
                           );

                        respuesta = Convert.ToInt32(idValidacion.Value);


                    }
                    catch (Exception ex)
                    {
                        alerta = ex.Message;
                    }
                
            }
            #endregion

            return respuesta;

         


        }


    }
}
