using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using System.Collections.Generic;

    public class JerarquiasBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }        

        public int Save(Jerarquia entity)
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryJerarquias(uow);

                if (entity.Identificador > 0)
                    result = repositoryCoutas.Actualizar(entity);
                else
                    result = repositoryCoutas.Insertar(entity);

                uow.SaveChanges();
            }
            return result;
        }

        public IEnumerable<Jerarquia> ObtenerTodos(IPaging paging)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryJerarquias(uow);
                return repositoryCoutas.Obtener(paging);
            }
        }

        public IEnumerable<Jerarquia> ObtenerPorCriterio(IPaging paging, Jerarquia entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryJerarquias(uow);
                return repositoryCoutas.ObtenerPorCriterio(paging, entity);
            }
        }

        public Jerarquia ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryJerarquias(uow);
                return repositoryCoutas.ObtenerPorId(id);
            }
        }

        public int CambiarEstatus(Jerarquia coutas)
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryCoutas = new RepositoryJerarquias(uow);
                result = repositoryCoutas.CambiarEstatus(coutas);

                uow.SaveChanges();
            }
            return result;
        }
    }
}
