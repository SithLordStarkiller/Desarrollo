namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using System.Collections.Generic;

    public class FactoresBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }
        
        public bool Guardar(Factor entity)
        {
            bool result = false;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryFactores(uow);

                if (entity.Identificador > 0)
                    result = repositoryCoutas.Actualizar(entity)>0;
                else
                    result = repositoryCoutas.Insertar(entity)>0;

                uow.SaveChanges();
            }
            return result;
        }

        public IEnumerable<Factor> ObtenerTodos(Paging paging)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryFactores(uow);
                return repositoryCoutas.Obtener(paging);
            }
        }

        public IEnumerable<Factor> ObtenerPorCriterio(Paging paging, Factor entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryFactores(uow);
                return repositoryCoutas.ObtenerPorCriterio(paging, entity);
            }
        }

        public Factor ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryFactores(uow);
                return repositoryCoutas.ObtenerPorId(id);
            }
        }

        public bool CambiarEstatus(Factor coutas)
        {
            bool result = false;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryFactores(uow);
                result = repositoryCoutas.CambiarEstatus(coutas)>0;

                uow.SaveChanges();
            }
            return result;
        }

        public IEnumerable<Factor> ObtenerPorClasificacion(Factor entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryFactores(uow);
                return repositoryCoutas.ObtenerPorClasificacion(entity);
            }
        }
    }
}
