using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using proEntidad;
using mvcSICER.proEntidad;
using proDatos;

namespace mvcSICER.proNegocio
{
    public class clsNegBusqueda
    {
        #region Version1
        public static clsEntResponseBusqueda Busqueda(string empPaterno, string empMaterno, string empNombre, string empCURP, string empRFC, string empNumero, string participante, int start, int limit, string buscar)
        {
            List<spuConsultarDatosIntegranteInternoExterno_Result> lst = new List<spuConsultarDatosIntegranteInternoExterno_Result>();

            string alerta = string.Empty;
            try
            {
                if (buscar == "1")
                {
                    System.Web.HttpContext.Current.Session["query" + System.Web.HttpContext.Current.Session.SessionID] = null;

                    lst = clsDatBusqueda.Busqueda(

                        empPaterno,
                        empMaterno,
                        empNombre,
                        empCURP,
                        empRFC,
                        empNumero,
                        participante,
                        ref alerta);

                }

            }
            catch (Exception ex)
            {
                alerta = ex.Message;
            }

            // return lst;
            #region Limite
            if (start == 0)
            {

                if (lst != null && lst.Count > limit)
                {
                    System.Web.HttpContext.Current.Session["query" + System.Web.HttpContext.Current.Session.SessionID] = lst;
                }
                else
                {
                    if (System.Web.HttpContext.Current.Session["query" + System.Web.HttpContext.Current.Session.SessionID] != null)
                    {
                        lst = (List<spuConsultarDatosIntegranteInternoExterno_Result>)System.Web.HttpContext.Current.Session["query" + System.Web.HttpContext.Current.Session.SessionID];
                    }
                }
            }
            else
            {
                lst = (List<spuConsultarDatosIntegranteInternoExterno_Result>)System.Web.HttpContext.Current.Session["query" + System.Web.HttpContext.Current.Session.SessionID];
            }
            #endregion
            #region Response
            clsEntResponseBusqueda objResponse = new clsEntResponseBusqueda

            {
                lstResponse = lst.Skip(start).Take(limit).ToList(),
                alerta = alerta,
                total = lst.Count()
            };

            return objResponse;
            #endregion


        }


        public static clsEntResponseValidaCurp validaCURP(string curp)
        {
            return clsDatBusqueda.validaCURP(curp);
        }

        #endregion

        #region cosnulta registro.registro

        public static spuConsultarRegistro_Result Consulta(int idRegistro, Guid idEmpleado)
        {
            return clsDatBusqueda.Consulta(idRegistro, idEmpleado);
        }
        #endregion  cosnulta registro.registro


        public static List<spuConsultarCertificacionIE_Result> ConsultaRegistrosCertificacion(int idRegistro)
        {

            return clsDatBusqueda.ConsultaRegistrosCertificacion(idRegistro);
        }

        public static List<spuConsultaUbicacionInterno_Result> ConsultaUbicacionInterno(Guid idEmpleado)
        {

            return clsDatBusqueda.ConsultaUbicacionInterno(idEmpleado);
        }
    }
}
