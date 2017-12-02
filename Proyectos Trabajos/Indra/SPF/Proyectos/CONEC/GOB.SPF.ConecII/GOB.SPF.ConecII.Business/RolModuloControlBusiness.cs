using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using AccessData.Repositories;
    using Entities;
    using System.Collections.Generic;
    using System;
    public class RolModuloControlBusiness
    {
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();

        public int Pages { get { return pages; } }

        public RolModuloControlBusiness() { }

        public int Guardar(RolModuloControl entity)
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var Repository = new RepositoryRolesModulosControl(uow);

                if (entity.Identificador > 0)
                    result = Repository.Actualizar(entity);
                else
                    result = Repository.Insertar(entity);

                uow.SaveChanges();
            }
            return result;
        }
        public IEnumerable<RolModuloControl> ObtenerTodos(IPaging paging)
        {
            List<RolModuloControl> list = new List<RolModuloControl>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                var Repository = new RepositoryRolesModulosControl(uow);
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

        public RolModuloControl ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repository = new RepositoryRolesModulosControl(uow);
                return repository.ObtenerPorId(id);
            }
        }
        public int CambiarEstatus(RolModuloControl entity)  // duda...
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repository = new RepositoryRolesModulosControl(uow);
                result = repository.CambiarEstatus(entity);
                uow.SaveChanges();
            }
            return result;
        }

        public IEnumerable<RolModuloControl> ObtenerPorCriterio(IPaging paging, RolModuloControl entity)
        {
            List<RolModuloControl> list = new List<RolModuloControl>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                RepositoryRolesModulosControl repository = new RepositoryRolesModulosControl(uow);
                list.AddRange(repository.ObtenerPorCriterio(paging, entity));
                this.pages = repository.Pages;
            }
            return list;
        }
    }
}
