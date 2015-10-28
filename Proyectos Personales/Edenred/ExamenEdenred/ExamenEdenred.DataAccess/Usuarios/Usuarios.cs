namespace ExamenEdenred.DataAccess.Usuarios
{
    using System.Data;
    using System.Data.SqlClient;
    using System;
    using System.Linq;
    using Entities.Entities;

    public class Usuarios
    {
        public UsUsuarios ObtenUsuario(int usuarioId)
        {
            const string executesqlstr = "SELECT * FROM US_USUARIOS WHERE IdUsuario = @Usuario";

            var para = new[]
            {
                new SqlParameter("@Usuario", usuarioId)
            };

            var obj = ControllerSqlServer.ExecuteDataTable(ParametersSql.StrConDbLsWebApp, CommandType.Text,
                executesqlstr, para);

            var resultado = new UsUsuarios();

            if (obj != null)
            {
                resultado = (from DataRow row in obj.Rows
                    select new UsUsuarios
                    {
                        Contrasena = (string) row["CONTRASENA"],
                        Usuario = (string) row["Usuario"],
                        IdUsuario = (int) row["IdUsuario"],
                    }).ToList().FirstOrDefault();
            }

            return resultado;
        }

        public bool GuardaArchivo(string texto)
        {
            try
            {
                var dt = new DataTable();

                var tableData = texto.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var col = from cl in tableData[0].Split(";".ToCharArray())
                    select new DataColumn(cl);
                dt.Columns.AddRange(col.ToArray());

                (from st in tableData.Skip(1)
                    select dt.Rows.Add(st.Split(";".ToCharArray()))).ToList();


                var resultado = (from DataRow row in dt.Rows
                    select new PerPersonas
                    {
                        IdPersona = -1,
                        Nombre = (string)row["Nombre"],
                        Sexo = (string)row["Sexo"],
                        Edad = Convert.ToInt32(row["Edad"])
                    }).ToList();

                foreach (var para in resultado.Select(item => new[]
                {
                    new SqlParameter("@Nombre", item.Nombre),
                    new SqlParameter("@Sexo", item.Sexo),
                    new SqlParameter("@Edad", item.Edad),
                    new SqlParameter("@IdPersona", -1)
                }))
                {
                    ControllerSqlServer.ExecuteNonQuery(ParametersSql.StrConDbLsWebApp,
                        CommandType.StoredProcedure, "Usp_InsertaPersonas", para);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}