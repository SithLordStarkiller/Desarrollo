using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GOB.SPF.ConecII.AccessData;
using GOB.SPF.ConecII.AccessData.Repositories;
using GOB.SPF.ConecII.Entities;

namespace GOB.SPF.ConecII.Business
{
    public class ReciboBusiness
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identificador"></param>
        /// <param name="certificado"></param>
        /// <returns></returns>
        /// <remarks>
        /// nombredocumento|folio|razon social|rfc|fecha inicial|fecha final
        /// </remarks>
        public string ObtenerCadenaComplemento(int identificador)
        {
            Recibo recibo = null;
            string cadena = null;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repository = new RepositoryRecibo(uow);
                recibo = repository.ObtenerPorId(identificador);
            } 
          
            var nombreDocumento ="RECIBO";
            var folio = recibo.Folio;
            var razonSocialCliente = recibo.Cliente.RazonSocial;
            var rfcCliente = recibo.Cliente.Rfc;
            var fechaInicio = recibo.FechaInicio;
            var fechaFin = recibo.FechaFin;            

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
        public bool GuardarFirma(int identificador, byte[] firma)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repository = new RepositoryRecibo(uow);

                var result = repository.GuardarFirma(identificador, firma);

                uow.SaveChanges();

                return result;

            }
        }
    }
}
