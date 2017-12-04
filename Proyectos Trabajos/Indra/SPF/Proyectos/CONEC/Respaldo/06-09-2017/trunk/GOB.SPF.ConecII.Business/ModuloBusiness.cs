namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using AccessData.Repositories;
    using Entities;
    using System.Collections.Generic;
    using System;

    public class ModuloBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }

        public ModuloBusiness() { }

        public int Guardar(Modulo entity)
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var RepositoryModulo = new RepositoryModulo(uow);

                if (entity.Identificador > 0)
                    result = RepositoryModulo.Actualizar(entity);
                else
                    result = RepositoryModulo.Insertar(entity);

                uow.SaveChanges();
            }
            return result;
        }
        public IEnumerable<Modulo> ObtenerTodos(Paging paging)
        {
            List<Modulo> listModulo = new List<Modulo>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                var RepositoryModulo = new RepositoryModulo(uow);
                listModulo.AddRange(RepositoryModulo.Obtener(paging));
                this.pages = RepositoryModulo.Pages;
            }
            return listModulo;
        }
        //public IEnumerable<Modulo> ObtenerListado()
        //{
        //    List<Modulo> listModulo = new List<Modulo>();
        //    using (var uow = UnitOfWorkFactory.Create())
        //    {
        //        var repositoryModulo = new RepositoryModulo(uow);
        //        listModulo.AddRange(repositoryModulo());
        //    }
        //    return listModulo;
        //}

        public Modulo ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryModulo = new RepositoryModulo(uow);
                return repositoryModulo.ObtenerPorId(id);
            }
        }
        public int CambiarEstatus(Modulo entity)  // duda...
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryModulo = new RepositoryModulo(uow);
                result = repositoryModulo.CambiarEstatus(entity);
                uow.SaveChanges();
            }
            return result;
        }

        public IEnumerable<Modulo> ObtenerPorCriterio(Paging paging, Modulo entity)
        {
            List<Modulo> list = new List<Modulo>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                RepositoryModulo repositoryModulo = new RepositoryModulo(uow);
                list.AddRange(repositoryModulo.ObtenerPorCriterio(paging, entity));
                this.pages = repositoryModulo.Pages;
            }
            return list;
        }
    }
}
