using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.AccessData;
using GOB.SPF.ConecII.AccessData.Repositories;

namespace GOB.SPF.ConecII.Business
{
    public class BitacoraBusiness
    {
        public int InsertarBitacora(Bitacora bitacora)
        {
            var result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryBitacora = new RepositoryBitacora(uow);
                result = repositoryBitacora.Insertar(bitacora);
                uow.SaveChanges();
            }
            return result;
        }
    }
}
