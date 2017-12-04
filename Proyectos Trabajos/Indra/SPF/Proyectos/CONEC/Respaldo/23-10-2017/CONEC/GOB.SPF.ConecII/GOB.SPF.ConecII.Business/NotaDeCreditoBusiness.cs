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
        public string ObtenerCadenaOriginal(int  identificador,Certificado certificado)
        {
            return null;
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
