using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using PubliPayments.Entidades;
using PubliPayments.Entidades.MYO;
using PubliPayments.Utiles;

namespace Publipayments.Negocios.MYO
{
    public class ValidaReferencias
    {
        public static ResultModel ReferenciasValidas(List<ReferenciasModel> referencias)
        {
            var result = new ResultModel();
            result.Dictamen = "Referencias OK";
            result.Valido = true;

            var familiares = referencias.Where(x => x.Nombre.StartsWith("Ref_Fam")).ToList();
            if (familiares != null && familiares.Count != 0)
            {
                var cuentaValidas = familiares.Where(x => x.Valor == "Si").ToList().Count();
                var minValidas = Math.Floor((decimal)familiares.Count / 2);
                if (cuentaValidas < minValidas)
                {
                    result.Dictamen = "Rechazado por Referencias";
                    result.Valido = false;
                }
            }

            var personales = referencias.Where(x => x.Nombre.StartsWith("Ref_Per")).ToList();
            if (personales != null && personales.Count != 0)
            {
                var cuentaValidas = personales.Where(x => x.Valor == "Si").ToList().Count();
                var minValidas = Math.Floor((decimal)personales.Count / 2);
                if (cuentaValidas < minValidas)
                {
                    result.Dictamen = "Rechazado por Referencias";
                    result.Valido = false;
                }
            }

            var laborales = referencias.Where(x => x.Nombre.StartsWith("Ref_Job")).ToList();
            if (laborales != null && laborales.Count != 0)
            {
                var cuentaValidas = laborales.Where(x => x.Valor == "Si").ToList().Count();
                var minValidas = Math.Floor((decimal)laborales.Count / 2);
                if (cuentaValidas < minValidas)
                {
                    result.Dictamen = "Rechazado por Referencias";
                    result.Valido = false;
                }
            }
            return result;
        }

        public static bool GuardaReferencias(int idOrden, List<ReferenciasModel> referencias)
        {
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "GuardaReferencias", "Guardando referencias - Orden: " + idOrden);
                foreach (var referencia in referencias)
                {

                    EntRespuestasMYO.InsertarRespuesta(idOrden, referencia.Nombre + "Result", referencia.Valor);
                    if (referencia.Comentario != "")
                    {
                        EntRespuestasMYO.InsertarRespuesta(idOrden, referencia.Nombre + "Comentario", referencia.Comentario);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "GuardaReferencias", "Error al guardar referencias - Orden: " + idOrden + " _Error:" + ex.Message);
                return false;
            }

            return true;
        }

        public static bool BorrarReferencias(int idOrden, List<ReferenciasModel> referencias)
        {
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "ReferenciasValidas", "BorrarReferencias - borrando referencias - Orden: " + idOrden);
                foreach (var referencia in referencias)
                {
                    if (referencia.Valor == "No")
                    {
                        EntRespuestasMYO.BorrarRespuestasOrden(idOrden, referencia.Nombre);
                        var entLoan = new EntLoan();
                        entLoan.ActualizarDatosRefImg(referencia.Comentario, Convert.ToInt32(referencia.IdDocumento), referencia.Nombre.ToUpper().Contains("REF_FAM") ? "REF_FAM" : referencia.Nombre.ToUpper().Contains("REF_JOB") ? "REF_JOB" : "REF_PER", 3);
                    }
                }
                CorreosMyo.ReferenciasIncorrectas(referencias, idOrden);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "ReferenciasValidas", "BorrarReferencias - error al borrar referencias - Orden: " + idOrden + " _Error:" + ex.Message);
                return false;
            }

            return true;
        }

    }

}
