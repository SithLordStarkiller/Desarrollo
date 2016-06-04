namespace SunCorp.DataAccessSqlServer.EntitiesDataAccesss
{
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using Entities;
    using Entities.Generic;
    using System.Collections.Generic;

    public class EntitiesAccess
    {
        #region UsUsuarios
        public UsUsuarios GetUsUsuarioTSql(UserSession session)
        {
            var executesqlstr = "SELECT * FROM UsUsuarios WHERE Usuario = '"+ session.User + "' AND Contrasena = '"+ session.Password + "'";
            // 
            var para = new[]
            {
                new SqlParameter("@user", session.User),
                new SqlParameter("@password", session.Password)
            };

            var obj = ControllerSqlServer.ExecuteDataTable(ParametersSql.StrConDbLsWebApp, CommandType.Text, executesqlstr, para);

            var resultado = new UsUsuarios();

            if (obj != null)
            {
                resultado = (from DataRow row in obj.Rows
                             select new UsUsuarios
                             {
                                 IdTipoUsuario = (int)row["IdTipoUsuario"],
                                 Contrasena = (string)row["Contrasena"],
                                 Usuario = (string)row["Usuario"],
                                 IdUsuario = (int)row["IdUsuario"],
                             }).ToList().FirstOrDefault();
            }

            return resultado;
        }

        public UsUsuarios GetUsUsuario(UserSession user)
        {
            using (var aux = new Repositorio<UsUsuarios>())
            {
                return aux.Extraer(r => r.Usuario == user.User && r.Contrasena == user.Password);
            }
        }

        #endregion

        #region UsTipoUsuario
        
        public UsTipoUsuario GetTypeUser(UsUsuarios user)
        {
            using (var aux = new Repositorio<UsTipoUsuario>())
            {
                return aux.Extraer(r => r.IdTipoUsuario == user.IdTipoUsuario);
            }
        }

        #endregion

        #region UsZona

        public List<UsZona> GetListUsZona()
        {
            using (var aux = new Repositorio<UsZona>())
            {
                return aux.TablaCompleta();
            }
        }

        public UsZona NewRegUsZona(UsZona zona)
        {
            using (var aux = new Repositorio<UsZona>())
            {
                return aux.Agregar(zona);
            }
        }

        public bool UpdateRegUsZona(UsZona zona)
        {
            using (var aux = new Repositorio<UsZona>())
            {
                return aux.Actualizar(zona);
            }
        }

        public bool DeleteRegUsZona(UsZona zona)
        {
            using (var aux = new Repositorio<UsZona>())
            {
                return aux.Eliminar(zona);
            }
        }
        #endregion

        #region UsUsuarioPorZona

        public List<UsUsuarioPorZona> GetUsUsuarioPorZona(UsUsuarios usUsuario)
        {
            using (var aux = new Repositorio<UsUsuarioPorZona>())
            {
                return aux.Filtro(r => r.IdUsuarios == usUsuario.IdUsuario);
            }
        }

        public List<UsZona> GetListUsZonaUserLinq(List<int> listUserZona)
        {
            using (var aux = new Repositorio<UsZona>())
            {
                var listZonas = aux.TablaCompleta();

                return listZonas.Where(r => listUserZona.Any(x => x == r.IdZona)).ToList();
            }
        }

        #endregion

        #region SisTipoUsuarioPorMenu

        public List<SisTipoUsuarioPorMenu> GetListMenusForTypeUser(UsUsuarios user)
        {
            using (var aux = new Repositorio<SisTipoUsuarioPorMenu>())
            {
                return aux.Filtro(r => r.IdTipoUsuario == user.IdTipoUsuario);
            }
        }

        #endregion
    }
}
