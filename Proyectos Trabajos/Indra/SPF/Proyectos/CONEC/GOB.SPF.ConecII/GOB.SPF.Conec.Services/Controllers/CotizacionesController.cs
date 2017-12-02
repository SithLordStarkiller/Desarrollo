using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GOB.SPF.ConecII.Business;
using System.Data.SqlClient;
using System.Diagnostics;
using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.Entities.Request;

namespace GOB.SPF.Conec.Services.Controllers
{
    /// <summary>
    /// 
    /// </summary>    
    [RoutePrefix("api/Cotizacion")]
    public class CotizacionesController : ApiController
    {
        #region Firmar Documentos

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>
        /// Horacio Torres
        /// 13/10/2017
        /// Creado
        /// </remarks>  
        [AllowAnonymous]
        [HttpPost]
        [Route("firmar/cotizacion")]
        public IHttpActionResult FirmarCotizacion(RequestFimaDigital entity)
        {
            ResultPage<Cotizacion> result = new ResultPage<Cotizacion>();

            try
            {
                if (entity.IdTipo != Enumeracion.DocumentoFirma.Cotizacion)
                {
                    throw new ConecException("Tipo de documento inválido");
                }                

                var cotizacionBusiness = new CotizacionBusiness();

                //el business de lo que se esta firmando es quien debe proporcionar la cadena original
                var cadena = cotizacionBusiness.ObtenerCadenaComplemento(entity.Identificador);

                var firmaBusiness = new ConecII.Business.FirmaDigital(entity.Identificador,                                             
                                              entity.Certificado.Base64,
                                              entity.Llave.Base64,
                                              entity.Password
                                              );               
                
                var firma = firmaBusiness.GenerarFirma(cadena); 
                 
                cotizacionBusiness.GuardarFirma(entity.Identificador, firma);
            }
            catch (SqlException e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            }
            catch (ConecException ex)
            {
                result.Errors.Add(new Error { Code = 1000, Message = ex.Message });
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            }

            return Ok(result);
                       
        }
        
        #endregion
    }
}
