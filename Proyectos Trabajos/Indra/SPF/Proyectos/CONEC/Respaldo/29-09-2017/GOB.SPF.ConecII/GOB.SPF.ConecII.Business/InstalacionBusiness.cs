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

        public int Guardar(Instalacion entity)
        {
            int result;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryInstalacion = new RepositoryInstalacion(uow);
                result = entity.Identificador > 0
                    ? repositoryInstalacion.Actualizar(entity)
                    : repositoryInstalacion.Insertar(entity);

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

        public IEnumerable<Instalacion> ObtenerTodos(Paging paging)
        {
            var list = new List<Instalacion>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositorInstalacion = new RepositoryInstalacion(uow);
                paging.CurrentPage = paging.CurrentPage == 0 ? 1 : paging.CurrentPage;
                list.AddRange(repositorInstalacion.Obtener(paging));
                pages = repositorInstalacion.Pages;
            }
            return list;
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

        public Instalacion ObtenerPorId(long id)
        {
            Instalacion instalacion;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryInstalacion = new RepositoryInstalacion(uow);
                var respositoryCorreo = new RepositoryCorreo(uow);
                var repositoryTelefono = new RepositoryTelefono(uow);
                instalacion= repositoryInstalacion.ObtenerPorId(id);
                if (instalacion!=null)
                {
                    instalacion.CorreosInstalacion = respositoryCorreo.ObtenerPorIdInstalacion(id);
                    instalacion.TelefonosInstalacion = repositoryTelefono.ObtenerPorIdInstalacion(id);
                }
                else
                {
                    throw new ConecException("No se encontro el cliente");
                }
                uow.SaveChanges();
            }
            return instalacion;
        }
    }
}
