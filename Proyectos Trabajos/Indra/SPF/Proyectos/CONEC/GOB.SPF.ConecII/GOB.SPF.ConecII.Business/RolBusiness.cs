using GOB.SPF.ConecII.Interfaces;
using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.Business
{
    using AccessData;
    using AccessData.Repositories;
    using Entities;
    using System.Collections.Generic;
    using System.Linq;

    public class RolBusiness
    {
        #region variables privadas
        private int pages { get; set; }

        private readonly UnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();
        #endregion

        #region variables publicas
        public int Pages { get { return pages; } }
        #endregion

        #region constructor
        public RolBusiness() { }
        #endregion

        #region metodos publicos
        public int Guardar(IRol entity)
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                result = entity.Id > 0 ? new RepositoryRol(uow).Actualizar(entity) : new RepositoryRol(uow).Insertar(entity);

                uow.SaveChanges();
            }
            return result;
        }

        public IEnumerable<IRol> ObtenerTodos(IPaging paging)
        {
            List<IRol> listRol = new List<IRol>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                var RepositoryRol = new RepositoryRol(uow);
                listRol.AddRange(RepositoryRol.Obtener(paging));
                this.pages = RepositoryRol.Pages;
            }
            return listRol;
        }

        public IRol ObtenerPorId(long id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                return new RepositoryRol(uow).ObtenerPorId(id);
            }
        }
        public int CambiarEstatus(IRol entity)
        {
            int result = 0;
            using (var uow = UnitOfWorkFactory.Create())
            {
                if (!entity.Activo) //Se valida que no lo tengan asignado
                    if (UsuariosActivosPorRol(entity.Id) == 0)
                        result = new RepositoryRol(uow).CambiarEstatus(entity);
                    else
                        return 0;
                else
                    result = new RepositoryRol(uow).CambiarEstatus(entity);
                uow.SaveChanges();
            }
            return result;
        }

        public IEnumerable<IRol> ObtenerPorCriterio(IPaging paging, IRol entity)
        {
            List<IRol> list = new List<IRol>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                RepositoryRol repositoryRol = new RepositoryRol(uow);
                list.AddRange(repositoryRol.ObtenerPorCriterio(paging, entity));
                this.pages = repositoryRol.Pages;
            }
            return list;
        }

        public IRol ObtenerPorNombre(string roleName)
        {
            IRol role = new Rol();
            using (var uow = UnitOfWorkFactory.Create())
            {
                role = new RepositoryRol(uow).ObtenerPorCriterio(new Paging() { All = true }, new Rol() { Name = roleName, Activo = true }).FirstOrDefault();
            }
            return role;
        }

        public IEnumerable<IRol> ObtenerRolesPorTipoUsuario(IRol rol, bool usuarioExterno)
        {
            List<IRol> list = new List<IRol>();
            using (var uow = UnitOfWorkFactory.Create())
            {
                RepositoryRol repositoryRol = new RepositoryRol(uow);
                list.AddRange(repositoryRol.ObtenerRolesPorTipoUsuario(rol, usuarioExterno));
                this.pages = repositoryRol.Pages;
            }
            return list;
        }
        #endregion

        #region metodos privados
        private int UsuariosActivosPorRol(int idRol)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                return new RepositoryRol(uow).UsuariosActivosPorRol(idRol);
            }
        }
        #endregion
    }
}
