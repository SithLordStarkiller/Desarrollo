namespace SunCorp.DataAccessSqlServer.EntitiesDataAccesss
{
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using Entities.Entities;

    public class UsUsuariosAccess
    {
        public UsUsuarios GetUsUsuario(string user)
        {
            const string executesqlstr = "SELECT * FROM USUSUARIOS WHERE Usuario = @Usuario";

            var para = new[]
            {
                new SqlParameter("@Usuario", user)
            };

            var obj = ControllerSqlServer.ExecuteDataTable(ParametersSql.StrConDbLsWebApp, CommandType.Text,
                executesqlstr, para);

            var resultado = new UsUsuarios();

            if (obj != null)
            {
                resultado = (from DataRow row in obj.Rows
                             select new UsUsuarios
                             {
                                 Contrasena = (string)row["CONTRASENA"],
                                 Usuario = (string)row["Usuario"],
                                 IdUsuario = (int)row["IdUsuario"],
                             }).ToList().FirstOrDefault();
            }

            return resultado;
        }
    }
}
