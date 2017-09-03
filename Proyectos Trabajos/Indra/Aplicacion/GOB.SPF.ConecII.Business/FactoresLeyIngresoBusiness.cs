namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using System.Collections.Generic;

    public class FactoresLeyIngresoBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }

        public bool Guardar(FactorLeyIngreso entity)
        {
            bool result = false;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFactorLeyIngreso = new RepositoryFactoresLeyIngreso(uow);

                if (entity.Identificador > 0)
                    result = repositoryFactorLeyIngreso.Actualizar(entity) > 0;
                else
                    result = repositoryFactorLeyIngreso.Insertar(entity) > 0;

                uow.SaveChanges();
            }
            return result;
        }

        public IEnumerable<FactorLeyIngreso> ObtenerTodos(Paging paging)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFactorLeyIngreso = new RepositoryFactoresLeyIngreso(uow);
                return repositoryFactorLeyIngreso.Obtener(paging);
            }
        }

        public IEnumerable<FactorLeyIngreso> ObtenerPorCriterio(Paging paging, FactorLeyIngreso entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFactorLeyIngreso = new RepositoryFactoresLeyIngreso(uow);
                return repositoryFactorLeyIngreso.ObtenerPorCriterio(paging, entity);
            }
        }

        public FactorLeyIngreso ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFactorLeyIngreso = new RepositoryFactoresLeyIngreso(uow);
                return repositoryFactorLeyIngreso.ObtenerPorId(id);
            }
        }

        public bool CambiarEstatus(FactorLeyIngreso tServicio)
        {
            bool result = false;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryFactorLeyIngreso = new RepositoryFactoresLeyIngreso(uow);
                result = repositoryFactorLeyIngreso.CambiarEstatus(tServicio) > 0;

                uow.SaveChanges();
            }
            return result;
        }
    }
}
