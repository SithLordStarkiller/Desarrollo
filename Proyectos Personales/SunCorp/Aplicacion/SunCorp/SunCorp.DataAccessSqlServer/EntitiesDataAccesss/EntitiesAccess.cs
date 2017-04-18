namespace SunCorp.DataAccessSqlServer.EntitiesDataAccesss
{
    using System.Linq;
    using Entities;
    using Entities.Generic;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class EntitiesAccess
    {
        //private readonly SunCorpEntities context = new SunCorpEntities();

        #region UsUsuarios

        public async Task<UsUsuarios> GetUsUsuario(UserSession user)
        {
            using (var aux = new Repositorio<UsUsuarios>())
            {
                return await aux.Consulta(r => r.Usuario == user.User && r.Contrasena == user.Password);
            }
        }

        #endregion

        #region UsTipoUsuario

        public async Task<UsTipoUsuario> GetTypeUser(UsUsuarios user)
        {
            using (var aux = new Repositorio<UsTipoUsuario>())
            {
                return await aux.Consulta(r => r.IdTipoUsuario == user.IdTipoUsuario);
            }
        }

        #endregion

        #region UsZona

        public async Task<List<UsZona>> GetListUsZona()
        {
            using (var aux = new Repositorio<UsZona>())
            {
                return await aux.ConsultaLista(r => r.Borrado == false);
            }
        }



        public async Task<UsZona> NewRegUsZona(UsZona zona)
        {
            using (var aux = new Repositorio<UsZona>())
            {
                return await aux.Insertar(zona);
            }
        }

        public async Task<bool> UpdateRegUsZona(UsZona zona)
        {
            using (var aux = new Repositorio<UsZona>())
            {
                return await aux.Actualizar(zona);
            }
        }

        public async Task<bool> DeleteRegUsZona(UsZona zona)
        {
            using (var aux = new Repositorio<UsZona>())
            {
                return await aux.Eliminar(zona);
            }
        }
        #endregion

        #region UsUsuarioPorZona

        public async Task<List<UsUsuarioPorZona>> GetUsUsuarioPorZona(UsUsuarios usUsuario)
        {
            using (var aux = new Repositorio<UsUsuarioPorZona>())
            {
                return await aux.ConsultaLista(r => r.IdUsuarios == usUsuario.IdUsuario);
            }
        }

        public async Task<List<UsZona>> GetListUsZonaUser(List<int> listUserZona)
        {
            using (var aux = new Repositorio<UsZona>())
            {
                var listZonas = await aux.ObtenerTabla();

                return listZonas.Where(r => listUserZona.Any(x => x == r.IdZona)).ToList();
            }
        }

        #endregion

        #region SisTipoUsuarioPorMenu

        public async Task<List<SisTipoUsuarioPorMenu>> GetListMenusForTypeUser(UsUsuarios user)
        {
            using (var aux = new Repositorio<SisTipoUsuarioPorMenu>())
            {
                return await aux.ConsultaLista(r => r.IdTipoUsuario == user.IdTipoUsuario);
            }
        }

        #endregion

        #region ProCatMarca

        public async Task<List<ProCatMarca>> GetListProCatMarca()
        {
            using (var aux = new Repositorio<ProCatMarca>())
            {
                return await aux.ConsultaLista(r => r.Borrado == false);
            }
        }

        public async Task<ProCatMarca> NewRegProCatMarca(ProCatMarca reg)
        {
            using (var aux = new Repositorio<ProCatMarca>())
            {
                return await aux.Insertar(reg);
            }
        }

        public async Task<bool> UpdateRegProCatMarca(ProCatMarca reg)
        {
            using (var aux = new Repositorio<ProCatMarca>())
            {
                return await aux.Actualizar(reg);
            }
        }

        public async Task<bool> DeleteRegProCatMarca(ProCatMarca reg)
        {
            using (var aux = new Repositorio<ProCatMarca>())
            {
                return await aux.Eliminar(reg);
            }
        }

        #endregion

        #region ProCatModelo

        public async Task<List<ProCatModelo>> GetListProCatModelo()
        {
            using (var aux = new Repositorio<ProCatModelo>())
            {
                return await aux.ConsultaLista(r => r.Borrado == false);
            }
        }

        public async Task<ProCatModelo> NewRegProCatModelo(ProCatModelo reg)
        {
            using (var aux = new Repositorio<ProCatModelo>())
            {
                return await aux.Insertar(reg);
            }
        }

        public async Task<bool> UpdateRegProCatModelo(ProCatModelo reg)
        {
            using (var aux = new Repositorio<ProCatModelo>())
            {
                return await aux.Actualizar(reg);
            }
        }

        public async Task<bool> DeleteRegProCatModelo(ProCatModelo reg)
        {
            using (var aux = new Repositorio<ProCatModelo>())
            {
                return await aux.Eliminar(reg);
            }
        }

        #endregion

        #region ProDiviciones

        public async Task<List<ProDiviciones>> GetListProCatDiviciones()
        {
            using (var aux = new Repositorio<ProDiviciones>())
            {
                return await aux.ConsultaLista(r => r.Borrado == false);
            }
        }

        public async Task<ProDiviciones> NewRegProDiviciones(ProDiviciones reg)
        {
            using (var aux = new Repositorio<ProDiviciones>())
            {
                return await aux.Insertar(reg);
            }
        }

        public async Task<bool> UpdateRegProDiviciones(ProDiviciones reg)
        {
            using (var aux = new Repositorio<ProDiviciones>())
            {
                return await aux.Actualizar(reg);
            }
        }

        public async Task<bool> DeleteRegProDiviciones(ProDiviciones reg)
        {
            using (var aux = new Repositorio<ProDiviciones>())
            {
                return await aux.Eliminar(reg);
            }
        }

        #endregion
    }
}
