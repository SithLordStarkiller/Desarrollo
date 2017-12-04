namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using AccessData.Repositories;
    using Entities;
    using System.Collections.Generic;
    using System;
    public class RolModuloBusiness

    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }

        public RolModuloBusiness() { }

        public int Guardar(RolModulo entity)
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var RepositoryRolModulo = new RepositoryRolModulo(uow);

                if (entity.Identificador > 0)
                    result = RepositoryRolModulo.Actualizar(entity);
                else
                    result = RepositoryRolModulo.Insertar(entity);

                uow.SaveChanges();
            }
            return result;
        }
        public IEnumerable<RolModulo> ObtenerTodos(Paging paging)
        {
            List<RolModulo> list = new List<RolModulo>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                var Repository = new RepositoryRolModulo(uow);
                list.AddRange(Repository.Obtener(paging));
                this.pages = Repository.Pages;
            }
            return list;
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

        public RolModulo ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repository = new RepositoryRolModulo(uow);
                return repository.ObtenerPorId(id);
            }
        }
        public int CambiarEstatus(RolModulo entity)  // duda...
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repository = new RepositoryRolModulo(uow);
                result = repository.CambiarEstatus(entity);
                uow.SaveChanges();
            }
            return result;
        }

        public IEnumerable<RolModulo> ObtenerPorCriterio(Paging paging, RolModulo entity)
        {
            List<RolModulo> list = new List<RolModulo>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                RepositoryRolModulo repository = new RepositoryRolModulo(uow);
                list.AddRange(repository.ObtenerPorCriterio(paging, entity));
                this.pages = repository.Pages;
            }
            return list;
        }
    }
}
