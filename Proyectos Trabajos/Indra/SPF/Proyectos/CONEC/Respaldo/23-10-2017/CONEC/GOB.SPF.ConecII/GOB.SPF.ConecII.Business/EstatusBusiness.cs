using System.Collections.Generic;
using GOB.SPF.ConecII.AccessData;
using GOB.SPF.ConecII.AccessData.Repositories;
using GOB.SPF.ConecII.Entities;

namespace GOB.SPF.ConecII.Business
{
    public class EstatusBusiness
    {
        public IEnumerable<Estatus> ObtenerPorCriterio(int identificadorNegocio)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repository = new RepositoryEstatus(uow);
                return repository.ObtenerPorCriterio(identificadorNegocio);
            }
        }
    }
}
