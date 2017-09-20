using System.Collections.Generic;
using GOB.SPF.ConecII.AccessData;
using GOB.SPF.ConecII.AccessData.Repositories;
using GOB.SPF.ConecII.Entities;

namespace GOB.SPF.ConecII.Business
{
    public class InstalacionBusiness
    {
        private int pages { get; set; }

        public int Pages => pages;

        public InstalacionBusiness() { }
        public int Guardar(Instalacion entity)
        {
            int result;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryInstalacion = new RepositoryInstalacion(uow);

                result = entity.Identificador > 0 ? repositoryInstalacion.Actualizar(entity) : repositoryInstalacion.Insertar(entity);

                uow.SaveChanges();
            }
            return result;
        }

        public IEnumerable<Instalacion> ObtenerTodos(Paging paging)
        {
            var list = new List<Instalacion>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositorInstalacion = new RepositoryInstalacion(uow);
                if (list != null)
                {
                    list.AddRange(repositorInstalacion.Obtener(paging));

                }
                pages = repositorInstalacion.Pages;
            }
            return list;
        }

        public Instalacion ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryInstalacion = new RepositoryInstalacion(uow);
                return repositoryInstalacion.ObtenerPorId(id);
            }
        }

        public int CambiarEstatus(Instalacion entity)
        {
            int result;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryInstalacion = new RepositoryInstalacion(uow);
                result = repositoryInstalacion.CambiarEstatus(entity);
                uow.SaveChanges();
            }
            return result;
        }

        public IEnumerable<Instalacion> ObtenerPorCriterio(Paging paging, Instalacion entity)
        {
            var list = new List<Instalacion>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryInstalacion = new RepositoryInstalacion(uow);
                list.AddRange(repositoryInstalacion.ObtenerPorCriterio(paging, entity));
                pages = repositoryInstalacion.Pages;
            }
            return list;
        }
    }
}
