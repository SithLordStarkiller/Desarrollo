namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using GOB.SPF.ConecII.AccessData.Repositories;

    using System.Collections.Generic;

    public class ClienteBusiness
    {

        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }

        public IEnumerable<Cliente> ObtenerTodos(Paging paging)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCliente = new RepositoryCliente(uow);
                return repositoryCliente.Obtener(paging);
            }
        }

        public IEnumerable<Cliente> ObtenerPorRazonSocial(string searchText)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCliente = new RepositoryCliente(uow);
                return repositoryCliente.ObtenerPorRazonSocial(searchText);
            }
        }

        public IEnumerable<Cliente> ObtenerPorNombreCorto(string searchText)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCliente = new RepositoryCliente(uow);
                return repositoryCliente.ObtenerPorNombreCorto(searchText);
            }
        }

        public IEnumerable<Cliente> ObtenerPorCriterio(Paging paging, Cliente entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCliente = new RepositoryCliente(uow);
                return repositoryCliente.ObtenerPorCriterio(paging, entity);
            }
        }
    }
}
