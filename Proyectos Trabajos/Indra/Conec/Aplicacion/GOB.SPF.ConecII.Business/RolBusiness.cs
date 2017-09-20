namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using AccessData.Repositories;
    using Entities;
    using System.Collections.Generic;
    public class RolBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }

        public RolBusiness() { }

        public int Guardar(Rol entity)
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var RepositoryRol = new RepositoryRol(uow);

                if (entity.Identificador > 0)
                    result = RepositoryRol.Actualizar(entity);
                else
                    result = RepositoryRol.Insertar(entity);

                uow.SaveChanges();
            }
            return result;
        }
        public IEnumerable<Rol> ObtenerTodos(Paging paging)
        {
            List<Rol> listRol = new List<Rol>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                var RepositoryRol = new RepositoryRol(uow);
                listRol.AddRange(RepositoryRol.Obtener(paging));
                this.pages = RepositoryRol.Pages;
            }
            return listRol;
        }
        //public IEnumerable<Rol> ObtenerListado()
        //{
        //    List<Rol> listRol = new List<Rol>();
        //    using (var uow = UnitOfWorkFactory.Create())
        //    {
        //        var repositoryRol = new RepositoryRol(uow);
        //        listRol.AddRange(repositoryRol());
        //    }
        //    return listRol;
        //}

        public Rol ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryRol = new RepositoryRol(uow);
                return repositoryRol.ObtenerPorId(id);
            }
        }
        public int CambiarEstatus(Rol entity)  // duda...
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryRol = new RepositoryRol(uow);
                result = repositoryRol.CambiarEstatus(entity);
                uow.SaveChanges();
            }
            return result;
        }

        public IEnumerable<Rol> ObtenerPorCriterio(Paging paging, Rol entity)
        {
            List<Rol> list = new List<Rol>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                RepositoryRol repositoryRol = new RepositoryRol(uow);
                list.AddRange(repositoryRol.ObtenerPorCriterio(paging, entity));
                this.pages = repositoryRol.Pages;
            }
            return list;
        }
    }
}
