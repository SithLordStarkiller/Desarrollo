namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using System.Collections.Generic;

    public class AniosBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }
        
        public int Save(Anio entity)
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryAnios(uow);

                if (entity.Identificador > 0)
                    result = repositoryCoutas.Actualizar(entity);
                else
                    result = repositoryCoutas.Insertar(entity);

                uow.SaveChanges();
            }
            return result;
        }

        public IEnumerable<Anio> ObtenerTodos(Paging paging)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryAnios(uow);
                return repositoryCoutas.Obtener(paging);
            }
        }

        public IEnumerable<Anio> ObtenerPorCriterio(Paging paging, Anio entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryAnios(uow);
                return repositoryCoutas.ObtenerPorCriterio(paging, entity);
            }
        }

        public Anio ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryAnios(uow);
                return repositoryCoutas.ObtenerPorId(id);
            }
        }
    }
}
