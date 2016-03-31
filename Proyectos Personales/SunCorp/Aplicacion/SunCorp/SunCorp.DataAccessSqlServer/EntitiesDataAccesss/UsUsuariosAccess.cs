namespace SunCorp.DataAccessSqlServer.EntitiesDataAccesss
{
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using Entities.Entities;
    using Entities.Generic;

    public class UsUsuariosAccess
    {
        public UsUsuarios GetUsUsuario(UserSession session)
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
    }
}
