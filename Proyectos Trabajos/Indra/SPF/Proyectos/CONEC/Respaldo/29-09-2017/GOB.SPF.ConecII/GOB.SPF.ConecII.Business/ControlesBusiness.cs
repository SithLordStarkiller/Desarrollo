namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using AccessData.Repositories;
    using Entities;
    using System.Collections.Generic;
    using System;
    public class ControlesBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }

        public ControlesBusiness() { }

        public int Guardar(Control entity)
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var RepositoryControles = new RepositoryControles(uow);

                if (entity.Identificador > 0)
                    result = RepositoryControles.Actualizar(entity);
                else
                    result = RepositoryControles.Insertar(entity);

                uow.SaveChanges();
            }
            return result;
        }
        public IEnumerable<Control> ObtenerTodos(Paging paging)
        {
            List<Control> list = new List<Control>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                var Repository = new RepositoryControles(uow);
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

        public Control ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repository = new RepositoryControles(uow);
                return repository.ObtenerPorId(id);
            }
        }
        public int CambiarEstatus(Control entity)  // duda...
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repository = new RepositoryControles(uow);
                result = repository.CambiarEstatus(entity);
                uow.SaveChanges();
            }
            return result;
        }

        public IEnumerable<Control> ObtenerPorCriterio(Paging paging, Control entity)
        {
            List<Control> list = new List<Control>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                RepositoryControles repository = new RepositoryControles(uow);
                list.AddRange(repository.ObtenerPorCriterio(paging, entity));
                this.pages = repository.Pages;
            }
            return list;
        }
    }
}
