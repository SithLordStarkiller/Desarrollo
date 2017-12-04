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
    [RoutePrefix("api/Contraprestacion")]
    public class ContraprestacionController : ApiController
    {

        #region Cetes

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("cetes/cargar")]        
        public IHttpActionResult CargarCetes(RequestCete entity)
        {

            ResultPage<Cete> result = new ResultPage<Cete>();
            
            try
            {
                var ceteBusiness = new CeteBusiness();
                result.Success = ceteBusiness.CargarCetes(entity.Lista);
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("cetes/previsualizar")]
        public IHttpActionResult PrevisualizarCetes(RequestCete entity)
        {

            ResultPage<Cete> result = new ResultPage<Cete>();

            try
            {
                var ceteBusiness = new CeteBusiness();

                result.List = ceteBusiness.ExtraerCetes(entity.Archivo.Base64);
                result.Success = true;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("cetes/obtenerporcriterio")]
        public IHttpActionResult ObtenerPorCriterioCete(RequestCete entity)
        {
            ResultPage<Cete> result = new ResultPage<Cete>();
            try
            {
                var business = new CeteBusiness();
                result.List.AddRange(business.ObtenerPorCriterio(entity.Paging, entity.Criterio));
                result.Page.Pages = business.Pages;               
                result.Success = true;
            }
            catch (SqlException e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            }

            return Ok(result);
        }

        #endregion

        #region EnteroTesofe

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("enterostesofe/cargar")]
        public IHttpActionResult CargarEnteroTesofe(RequestEnteroTesofe entity)
        {
            ResultPage<EnteroTesofe> result = new ResultPage<EnteroTesofe>();

            try
            {
                var enteroTesofeBusiness = new EnteroTesofeBusiness();
                result.Success = enteroTesofeBusiness.CargarEnteroTesofe(entity.Lista);

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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("enterostesofe/previsualizar")]
        public IHttpActionResult PrevisualizarEnteroTesofe(RequestEnteroTesofe entity)
        {

            ResultPage<EnteroTesofe> result = new ResultPage<EnteroTesofe>();

            try
            {
                var business = new EnteroTesofeBusiness();

                result.List = business.ExtraerEnteroTesofe(entity.Archivo.Base64);

                result.Success = true;
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
                result.Errors.Add(new Error { Code = 1000, Message = e.Message});
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            }

            return Ok(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("enterostesofe/obtenerporcriterio")]
        public IHttpActionResult ObtenerPorCriterioEnteroTesofe(RequestEnteroTesofe entity)
        {
            var result = new ResultPage<EnteroTesofe>();
            try
            {
                var business = new EnteroTesofeBusiness();
                result.List.AddRange(business.ObtenerPorCriterio(entity.Paging, entity.Criterio));
                result.Page.Pages = business.Pages;
                result.Success = true;
            }
            catch (SqlException e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            }

            return Ok(result);
        }



        #endregion

        #region Firmar Documentos

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("firmar/recibo")]
        public IHttpActionResult FirmarRecibo(RequestFimaDigital entity)
        {
            ResultPage<Recibo> result = new ResultPage<Recibo>();

            try
            {
                if (entity.IdTipo != Enumeracion.DocumentoFirma.NotaCredito)
                {
                    throw new ConecException("Tipo de documento inválido");
                }

                var reciboBusiness = new ReciboBusiness();
                
                var firmaBusiness = new ConecII.Business.FirmaDigital(entity.Identificador,                                    
                                    entity.Certificado.Base64,
                                    entity.Llave.Base64,
                                    entity.Password,
                                    entity.Razon
                                    );

                 // obtener informacion del certifiacdo
                 var certificado = firmaBusiness.ObtenerCertificado();

                //el business de lo que se esta firmando es quien debe proporcionar la cadena original
                var cadenaOrignal = reciboBusiness.ObtenerCadenaOriginal(entity.Identificador, certificado);
                
                var firma = firmaBusiness.GenerarFirma(cadenaOrignal);

                reciboBusiness.GuardarFirma(entity.Identificador, firma);

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("firmar/notacredito")]
        public IHttpActionResult FirmarNotaCredito(RequestFimaDigital entity)
        {
            ResultPage<NotaDeCredito> result = new ResultPage<NotaDeCredito>();

            try
            {
                if (entity.IdTipo != Enumeracion.DocumentoFirma.NotaCredito)
                {
                    throw new ConecException("Tipo de documento inválido");
                }

                var notaDeCreditoBusiness = new NotaDeCreditoBusiness();
                
                var firmaBusiness = new ConecII.Business.FirmaDigital(entity.Identificador,                                  
                                   entity.Certificado.Base64,
                                   entity.Llave.Base64,
                                   entity.Password,
                                   entity.Razon);

                // obtener informacion del certifiacdo
                var certificado = firmaBusiness.ObtenerCertificado();

                //el business de lo que se esta firmando es quien debe proporcionar la cadena original
                var cadenaOriginal = notaDeCreditoBusiness.ObtenerCadenaOriginal(entity.Identificador, certificado);

                var firma = firmaBusiness.GenerarFirma(cadenaOriginal);

                notaDeCreditoBusiness.GuardarFirma(entity.Identificador, firma);
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

        #region

        /// <summary>
        /// Agrega un certificado emisor
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>
        /// 18/10/2017
        /// Horacio Torres
        /// Creado
        /// </remarks>
        [HttpPost]
        [Route("certificado/agregaremisor")]
        public IHttpActionResult AgregarCertificadoEmisor(RequestEmisor entity)
        {

            ResultPage<string> result = new ResultPage<string>();

            try
            {
                var firma = new ConecII.Business.FirmaDigital();
                var id = firma.AgregarCertificadoEmisor(entity.Archivo.Base64);
                result.Success = (id > 0);
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
