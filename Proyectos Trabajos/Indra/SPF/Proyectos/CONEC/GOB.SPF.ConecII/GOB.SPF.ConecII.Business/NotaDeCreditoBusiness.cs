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
    public class NotaDeCreditoBusiness
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identificador"></param>
        /// <returns></returns>
        /// <remarks>
        /// cadena complemento:
        /// nombredocumento|
        /// folio de nota de crédito|
        /// Razón Social del Cliente|
        /// RFC del cliente|
        /// Folio del recibo asociado(fecha inicio y fin, dd/mm/yyyy)
        /// </remarks>
        public string ObtenerCadenaComplemento(int  identificador)
        {
            NotaDeCredito nota = new NotaDeCredito();
            string cadena = null;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repository = new RepositoryNotaDeCredito(uow);
                nota = repository.ObtenerPorId(identificador);
            }

            var nombreDocumento = "NOTA DE CREDITO";
            var folio = nota.Folio;
            var razonSocialCliente = string.Empty;
            var rfcCliente = nota.Recibo.Cliente.Rfc;
            var folioRecibo = nota.Recibo.Folio;
            var fechaInicio = nota.Recibo.FechaInicio;
            var fechaFin = nota.Recibo.FechaFin;

            cadena = String.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}",
                nombreDocumento,
                folio,
                razonSocialCliente,
                rfcCliente,
                folioRecibo,
                fechaInicio.ToShortDateString(),
                fechaFin.ToShortDateString()
                );

            return cadena;
        }

        public bool GuardarFirma(int identificador, byte[] firma)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repository = new RepositoryNotaDeCredito(uow);

                var result = repository.GuardarFirma(identificador, firma);

                uow.SaveChanges();

                return result;

            }
        }
    }
}
