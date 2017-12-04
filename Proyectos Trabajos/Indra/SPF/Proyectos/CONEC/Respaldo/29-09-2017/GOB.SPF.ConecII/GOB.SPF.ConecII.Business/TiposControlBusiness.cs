namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using AccessData.Repositories;
    using Entities;
    using System.Collections.Generic;
    using System;
    public class TiposControlBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }

        public TiposControlBusiness() { }

        public int Guardar(TipoControl entity)
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var Repository = new RepositoryTiposControl(uow);

                if (entity.Identificador > 0)
                    result = Repository.Actualizar(entity);
                else
                    result = Repository.Insertar(entity);

                uow.SaveChanges();
            }
            return result;
        }
        public IEnumerable<TipoControl> ObtenerTodos(Paging paging)
        {
            List<TipoControl> list = new List<TipoControl>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                var Repository = new RepositoryTiposControl(uow);
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

        public TipoControl ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repository = new RepositoryTiposControl(uow);
                return repository.ObtenerPorId(id);
            }
        }
        public int CambiarEstatus(TipoControl entity)  // duda...
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repository = new RepositoryTiposControl(uow);
                result = repository.CambiarEstatus(entity);
                uow.SaveChanges();
            }
            return result;
        }

        public IEnumerable<TipoControl> ObtenerPorCriterio(Paging paging, TipoControl entity)
        {
            List<TipoControl> list = new List<TipoControl>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                RepositoryTiposControl repository = new RepositoryTiposControl(uow);
                list.AddRange(repository.ObtenerPorCriterio(paging, entity));
                this.pages = repository.Pages;
            }
            return list;
        }
    }
}
