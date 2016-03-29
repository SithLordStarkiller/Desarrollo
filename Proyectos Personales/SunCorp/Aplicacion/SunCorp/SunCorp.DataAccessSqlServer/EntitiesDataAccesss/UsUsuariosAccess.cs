namespace SunCorp.DataAccessSqlServer.EntitiesDataAccesss
{
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using Entities.Entities;

    public class UsUsuariosAccess
    {
        public UsUsuarios GetUsUsuario(string user, string password)
        {
            const string executesqlstr = "SELECT * FROM UsUsuarios WHERE usuario = '@user' AND contrasena = '@password'";
            // 
            var para = new[]
            {
                new SqlParameter("@user", user),
                new SqlParameter("@password", password)
            };

            var obj = ControllerSqlServer.ExecuteDataTable(ParametersSql.StrConDbLsWebApp, CommandType.Text, executesqlstr, para);

            var resultado = new UsUsuarios();

            if (obj != null)
            {
                resultado = (from DataRow row in obj.Rows
                             select new UsUsuarios
                             {
                                 Contrasena = (string)row["Contrasena"],
                                 Usuario = (string)row["Usuario"],
                                 IdUsuario = (int)row["IdUsuario"],
                             }).ToList().FirstOrDefault();
            }

            return resultado;
        }
    }
}
