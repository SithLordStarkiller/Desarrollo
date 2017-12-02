using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using System.Collections.Generic;

    public class GruposTarifarioBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }        

       

        public IEnumerable<VehiculoTarifario> ObtenerTodos(IPaging paging)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryGruposTarifario(uow);
                return repositoryCoutas.Obtener(paging);
            }
        }

        public IEnumerable<VehiculoTarifario> ObtenerPorCriterio(IPaging paging, VehiculoTarifario entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryGruposTarifario(uow);
                return repositoryCoutas.ObtenerPorCriterio(paging, entity);
            }
        }

        public VehiculoTarifario ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryGruposTarifario(uow);
                return repositoryCoutas.ObtenerPorId(id);
            }
        }

        
    }
}
