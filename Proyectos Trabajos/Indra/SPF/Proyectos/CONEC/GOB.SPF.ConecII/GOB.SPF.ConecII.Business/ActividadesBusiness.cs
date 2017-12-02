using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using Entities;
    using System.Collections.Generic;

    public class ActividadesBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }

        public bool Guardar(Actividad entity)
        {
            bool result = false;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryActividades = new RepositoryActividades(uow);

                if (entity.Identificador > 0)
                    result = repositoryActividades.Actualizar(entity) > 0;
                else
                    result = repositoryActividades.Insertar(entity) > 0;

                uow.SaveChanges();
            }
            return result;
        }

        public IEnumerable<Actividad> ObtenerTodos(IPaging paging)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryActividades = new RepositoryActividades(uow);
                return repositoryActividades.Obtener(paging);
            }
        }

        public IEnumerable<Actividad> ObtenerPorCriterio(IPaging paging, Actividad entity)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryActividades = new RepositoryActividades(uow);
                return repositoryActividades.ObtenerPorCriterio(paging, entity);
            }
        }

        public Actividad ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryActividades = new RepositoryActividades(uow);
                return repositoryActividades.ObtenerPorId(id);
            }
        }

        public bool CambiarEstatus(Actividad entity)
        {
            bool result = false;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryActividades = new RepositoryActividades(uow);
                result = repositoryActividades.CambiarEstatus(entity) > 0;

                uow.SaveChanges();
            }
            return result;
        }
    }
}
