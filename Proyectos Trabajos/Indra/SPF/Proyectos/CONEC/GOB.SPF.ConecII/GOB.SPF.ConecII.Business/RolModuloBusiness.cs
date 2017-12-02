using GOB.SPF.ConecII.Interfaces;
using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using AccessData.Repositories;
    using Entities;
    using System.Collections.Generic;
    using System;
    public class RolModuloBusiness
    {
        #region variables privadas
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();
        #endregion
        
        #region variables publicas
        public int Pages { get { return pages; } }
        #endregion

        #region constructor
        public RolModuloBusiness() { }
        #endregion

        #region metodos publicos
        public int Guardar(IRolModulo entity)
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                var RepositoryRolModulo = new RepositoryRolModulo(uow);

                if (entity.Id > 0)
                    result = RepositoryRolModulo.Actualizar(entity);
                else
                    result = RepositoryRolModulo.Insertar(entity);

                uow.SaveChanges();
            }
            return result;
        }

        public IEnumerable<IRolModulo> ObtenerTodos()
        {
            List<IRolModulo> list = new List<IRolModulo>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                list.AddRange(new RepositoryRolModulo(uow).Obtener(new Paging() { All = true, Rows = 0, Pages = 0}));
            }
            return list;
        }

        public IRolModulo ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repository = new RepositoryRolModulo(uow);
                return repository.ObtenerPorId(id);
            }
        }
        public int CambiarEstatus(IRolModulo entity)  // duda...
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

        public IEnumerable<IRolModulo> ObtenerPorCriterio(IPaging paging, IRolModulo entity)
        {
            List<IRolModulo> list = new List<IRolModulo>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                RepositoryRolModulo repository = new RepositoryRolModulo(uow);
                list.AddRange(repository.ObtenerPorCriterio(paging, entity));
                this.pages = repository.Pages;
            }
            return list;
        }
        #endregion
    }
}
