namespace ExamenEdenred.DataAccess.Usuarios
{
    using System.Data;
    using System.Data.SqlClient;
    using System;
    using System.Linq;
    using Entities.Entities;

    public class Usuarios
    {
        public bool ObtenUsuario(int usuarioId)
        {
            const string executesqlstr = "SELECT TOP 1 * FROM US_USUARIOS WHERE ID_USUARIO = @Usuario";

            var para = new[] { 
                new SqlParameter("@Usuario",usuarioId)
            };

            var obj = ControllerSqlServer.ExecuteDataTable(ParametersSql.StrConDbLsWebApp, CommandType.Text, executesqlstr, para);

            var resultado= new UsUsuarios();

            if (obj != null)
            {
                resultado = (from DataRow row in obj.Rows
                             select new UsUsuarios
                             {
                                 Contrasena = (string)row["CONTRASENA"],
                                 Usuario = (string)row["Usuario"],
                                 IdEstatusUsuario = Convert.IsDBNull(row["ID_ESTATUS_USUARIOS"]) ? null : (int?)row["ID_ESTATUS_USUARIOS"],
                                 IdUsuario = (int)row["ID_USUARIO"],
                                 IdHistorial = Convert.IsDBNull(row["ID_HISTORIAL"]) ? null : (int?)row["ID_HISTORIAL"],
                                 IdNivelUsuario = Convert.IsDBNull(row["ID_NIVEL_USUARIO"]) ? null : (int?)row["ID_NIVEL_USUARIO"],
                                 IdTipoUsuario = Convert.IsDBNull(row["ID_TIPO_USUARIO"]) ? null : (int?)row["ID_TIPO_USUARIO"]
                             }).ToList().FirstOrDefault();
            }

            return resultado != null;
        }
    }
}
