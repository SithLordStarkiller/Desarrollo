using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using AccessData.Repositories.Usuarios;
    using Entities;
    using System.Collections.Generic;
    using System;
    using Interfaces;

    public class UsuarioBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Guardar(IUsuario entity)
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var RepositoryUsuario = new RepositoryUsuarios(uow);

                if (entity.Id > 0)
                    result = RepositoryUsuario.Actualizar(entity);
                else
                    result = RepositoryUsuario.Insertar(entity);

                uow.SaveChanges();
            }
            return result;
        }
        public IEnumerable<IUsuario> ObtenerTodos(IPaging paging)
        {
            throw new NotImplementedException();
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

        public IUsuario ObtenerPorId(int id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryUsuario = new RepositoryUsuarios(uow);
                return repositoryUsuario.ObtenerPorId(id);
            }
        }
        public int CambiarEstatus(IUsuario entity)  // duda...
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryUsuario = new RepositoryUsuarios(uow);
                result = repositoryUsuario.CambiarEstatus(entity);
                uow.SaveChanges();
            }
            return result;
        }

        public IUsuario ObtenerPorLogin(string login)
        {
            IUsuario result = null;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryUsuario = new RepositoryUsuarios(uow);
                result = repositoryUsuario.ObtenerPorLogin(login);
            }
            return result;
        }

        public IEnumerable<IUsuario> ObtenerPorCriterio(IPaging paging, IUsuario entity)
        {
            throw new NotImplementedException();
        }
    }
}
