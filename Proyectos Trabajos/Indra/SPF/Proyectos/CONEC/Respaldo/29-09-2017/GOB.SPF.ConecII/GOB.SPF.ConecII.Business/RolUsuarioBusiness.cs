namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using AccessData.Repositories;
    using Entities;
    using System.Collections.Generic;
    using System;
    public class RolUsuarioBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }

        public RolUsuarioBusiness() { }

        public int Guardar(RolUsuario entity)
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var RepositoryRolUsuario = new RepositoryRolUsuario(uow);

                if (entity.Identificador > 0)
                    result = RepositoryRolUsuario.Actualizar(entity);
                else
                result = RepositoryRolUsuario.Insertar(entity);

                uow.SaveChanges();
            }
            return result;
        }
        public IEnumerable<RolUsuario> ObtenerTodos(Paging paging)
        {
            List<RolUsuario> listUsuario = new List<RolUsuario>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                var RepositoryRolUsuario = new RepositoryRolUsuario(uow);
                listUsuario.AddRange(RepositoryRolUsuario.Obtener(paging));
                this.pages = RepositoryRolUsuario.Pages;
            }
            return listUsuario;
        }
        //public IEnumerable<Usuario> ObtenerListado()
        //{
        //    List<Usuario> listUsuario = new List<Usuario>();
        //    using (var uow = UnitOfWorkFactory.Create())
        //    {
        //        var repositoryUsuario = new RepositoryUsuario(uow);
        //        listUsuario.AddRange(repositoryUsuario());
        //    }
        //    return listUsuario;
        //}

        public RolUsuario ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryRolUsuario = new RepositoryRolUsuario(uow);
                return repositoryRolUsuario.ObtenerPorId(id);
            }
        }
        public int CambiarEstatus(RolUsuario entity)  // duda...
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryRolUsuario = new RepositoryRolUsuario(uow);
                result = repositoryRolUsuario.CambiarEstatus(entity);
                uow.SaveChanges();
            }
            return result;
        }

        public IEnumerable<RolUsuario> ObtenerPorCriterio(Paging paging, RolUsuario entity)
        {
            List<RolUsuario> list = new List<RolUsuario>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                RepositoryRolUsuario repositoryRolUsuario = new RepositoryRolUsuario(uow);
                list.AddRange(repositoryRolUsuario.ObtenerPorCriterio(paging, entity));
                this.pages = repositoryRolUsuario.Pages;
            }
            return list;
        }
    }
}
