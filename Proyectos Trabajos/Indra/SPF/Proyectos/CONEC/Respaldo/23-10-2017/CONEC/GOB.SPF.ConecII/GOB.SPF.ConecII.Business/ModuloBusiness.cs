namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using AccessData.Repositories;
    using Entities;
    using System.Collections.Generic;
    using System;
    using Interfaces;
    using Interfaces.Genericos;

    public class ModuloBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }

        public ModuloBusiness() { }

        public int Guardar(IModulo entity)
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var RepositoryModulo = new RepositoryModulo(uow);

                if (entity.Id > 0)
                    result = RepositoryModulo.Actualizar(entity);
                else
                    result = RepositoryModulo.Insertar(entity);

                uow.SaveChanges();
            }
            return result;
        }
        public IEnumerable<IModulo> ObtenerTodos(IPaging paging)
        {
            List<IModulo> listModulo = new List<IModulo>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                var RepositoryModulo = new RepositoryModulo(uow);
                listModulo.AddRange(RepositoryModulo.Obtener(paging));
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

        public IEnumerable<IModulo> ObtenerTodos()
        {
            using (var uow = UnitOfWorkFactory.CreateSecurityUnitOfWork(false))
            {
                var RepositoryModulo = new AccessData.Repositories.Modulos.RepositoryModulo(uow);
                var modulos = RepositoryModulo.ObtenerTodos();
                return modulos;
            }
        }

        public IModulo ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repositoryModulo = new RepositoryModulo(uow);
                return repositoryModulo.ObtenerPorId(id);
            }
        }
        public int CambiarEstatus(IModulo entity)  // duda...
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

        public IEnumerable<IModulo> ObtenerPorCriterio(IPaging paging, IModulo entity)
        {
            List<IModulo> list = new List<IModulo>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                RepositoryModulo repositoryModulo = new RepositoryModulo(uow);
                list.AddRange(repositoryModulo.ObtenerPorCriterio(paging, entity));
                this.pages = repositoryModulo.Pages;
            }
            return list;
        }

        public IEnumerable<IModulo> ObtenerSubModulosPorIdPadre(long idPadre)
        {
            List<IModulo> list = new List<IModulo>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                list.AddRange(new RepositoryModulo(uow).ObtenerSubModulos(idPadre));
            }
            return list;
        }
    }
}
