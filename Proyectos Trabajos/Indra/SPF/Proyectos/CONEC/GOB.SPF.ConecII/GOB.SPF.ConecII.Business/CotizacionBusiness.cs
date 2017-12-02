using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

using GOB.SPF.ConecII.AccessData;
using GOB.SPF.ConecII.AccessData.Repositories;
using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.Entities.Amatzin;
using GOB.SPF.ConecII.Entities.DTO;
using GOB.SPF.ConecII.Business;
using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.Business
{
    public class CotizacionBusiness
    {

        /// <summary>
        /// Regresa la cadena original para la firma
        /// </summary>
        /// <param name="idCotizacion"></param>
        /// <returns>
        /// nombredocumento|folio|razon social|rfc|fecha inicial|fecha final
        
        /// </returns>
        public string ObtenerCadenaComplemento(int identificador)
        {
            string cadena = null;

            //ha que obtener datos del cliente y de la cotizacion
            var clienteBusiness = new ClienteBusiness();
            var cliente = clienteBusiness.ObtenerPorCotizacion(identificador);

            var cotizacion = ObtenerPorId(identificador);

            
            var nombreDocumento = "COTIZACION";
            var folio = cotizacion.Folio;
            var razonSocialCliente = cliente.RazonSocial;
            var rfcCliente = cliente.Rfc;
            var fechaInicio = cotizacion.FechaInicial;
            var fechaFin = cotizacion.FechaFinal;
          

            cadena = String.Format("{0}|{1}|{2}|{3}|{4}|{5}",               
                nombreDocumento,
                folio,
                razonSocialCliente,
                rfcCliente,
                fechaInicio.ToShortDateString(),
                fechaFin.ToShortDateString()            
                );

            return cadena;
        }

        public Cotizacion ObtenerPorId(int identificador)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repository = new RepositoryCotizacion(uow);
                return repository.ObtenerPorId(identificador);
            }
                
        }

        /// <summary>
        /// Guarda la fima digital en la cotizacion
        /// </summary>
        /// <param name="idCotizacion"></param>
        /// <param name="firma"></param>
        /// <returns></returns>
        public bool GuardarFirma(int identificador, byte[] firma)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repository = new RepositoryCotizacion(uow);

                var result = repository.GuardarFirma(identificador, firma);

                uow.SaveChanges();

                return result;

            }
           
        }

    }
}
