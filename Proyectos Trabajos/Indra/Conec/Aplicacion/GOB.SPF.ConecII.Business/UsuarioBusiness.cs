namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using AccessData.Repositories;
    using Entities;
    using System.Collections.Generic;
    using System;

    public class UsuarioBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }

        public UsuarioBusiness() { }

        public int Guardar(Usuario entity)
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var RepositoryUsuario = new RepositoryUsuario(uow);

                if (entity.Identificador > 0)
                    result = RepositoryUsuario.Actualizar(entity);
                else
                    result = RepositoryUsuario.Insertar(entity);

                uow.SaveChanges();
            }
            return result;
        }
        public IEnumerable<Usuario> ObtenerTodos(Paging paging)
        {
            List<Usuario> listUsuario = new List<Usuario>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                var RepositoryUsuario = new RepositoryUsuario(uow);
                listUsuario.AddRange(RepositoryUsuario.Obtener(paging));
                this.pages = RepositoryUsuario.Pages;
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

        public Usuario ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryUsuario = new RepositoryUsuario(uow);
                return repositoryUsuario.ObtenerPorId(id);
            }
        }
        public int CambiarEstatus(Usuario entity)  // duda...
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryUsuario = new RepositoryUsuario(uow);
                result = repositoryUsuario.CambiarEstatus(entity);
                uow.SaveChanges();
            }
            return result;
        }

        public IEnumerable<Usuario> ObtenerPorCriterio(Paging paging, Usuario entity)
        {
            List<Usuario> list = new List<Usuario>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                RepositoryUsuario repositoryUsuario = new RepositoryUsuario(uow);
                list.AddRange(repositoryUsuario.ObtenerPorCriterio(paging, entity));
                this.pages = repositoryUsuario.Pages;
            }
            return list;
        }
    }
}
