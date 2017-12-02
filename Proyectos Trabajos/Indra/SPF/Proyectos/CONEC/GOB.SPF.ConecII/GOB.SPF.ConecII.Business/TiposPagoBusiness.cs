using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using System.Collections.Generic;
    public class TiposPagoBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }

        public int Guardar(TiposPago entity)
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryTiposPago = new RepositoryTiposPago(uow);

                if (entity.Identificador > 0)
                    result = repositoryTiposPago.Actualizar(entity);
                else
                    result = repositoryTiposPago.Insertar(entity);

                uow.SaveChanges();
            }
            return result;
        }

        public IEnumerable<TiposPago> ObtenerTodos(IPaging paging)
        {

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryTiposPago = new RepositoryTiposPago(uow);
                return repositoryTiposPago.Obtener(paging);
            }
        }

        public TiposPago ObtenerPorId(long id)
        {

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryTiposPago = new RepositoryTiposPago(uow);
                return repositoryTiposPago.ObtenerPorId(id);
            }
        }

        public int CambiarEstatus(TiposPago entity)
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryTiposPago = new RepositoryTiposPago(uow);
                result = repositoryTiposPago.CambiarEstatus(entity);

                uow.SaveChanges();
            }
            return result;
        }

        public IEnumerable<TiposPago> ObtenerPorCriterio(IPaging paging, TiposPago entity)
        {

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryTiposPago = new RepositoryTiposPago(uow);
                return repositoryTiposPago.ObtenerPorCriterio(paging, entity);
            }
        }

    }
}
