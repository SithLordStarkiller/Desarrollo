namespace Broxel.DataAccess.Usuarios
{
    using System.Linq;
    using System.Data;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using Entities;
    using Entities.Entidades;

    public class DaUsUsuarios
    {
        #region Metodos

        public UsUsuarios ObtenUsUsuarionPorLogin(string usuario, string contrasena)
        {
            var parametros = new SqlParameter[2];

            parametros[0] = new SqlParameter { ParameterName = "@Usuario", DbType = DbType.String, Value = usuario };
            parametros[1] = new SqlParameter { ParameterName = "@Contrasena", DbType = DbType.String, Value = contrasena };

            var resultado = new ManejadorSqlServer().ExecuteDataTable(Constantes.ConexionBaseDatos, CommandType.StoredProcedure, "Usp_ObtenUsUsuarionPorLogin", parametros);

            if (resultado == null || resultado.Rows.Count <= 0)
                return null;

            var usuariResultado = ConvertirUsUsuario(resultado);

            return usuariResultado;
        }

        #endregion

        #region Convertidores

        private UsUsuarios ConvertirUsUsuario(DataTable tabla)
        {
            var usuario = tabla.AsEnumerable().Select(row => new UsUsuarios
            {
                IdUsuario = row.Field<int>("IdUsuario"),
                IdEstatus = row.Field<int?>("IdEstatus"),
                Usuario = row.Field<string>("Usuario"),
                Contrasena = row.Field<string>("Contrasena")
            }).FirstOrDefault();

            return usuario;
        }

        private List<UsUsuarios> ConvertirListaUsUsuario(DataTable tabla)
        {
            var listaUsuario = tabla.AsEnumerable().Select(row => new UsUsuarios
            {
                IdUsuario = row.Field<int>("IdUsuario"),
                IdEstatus = row.Field<int?>("IdEstatus"),
                Usuario = row.Field<string>("Usuario"),
                Contrasena = row.Field<string>("Contrasena")
            }).ToList();

            return listaUsuario;
        }

        #endregion
    }
}
