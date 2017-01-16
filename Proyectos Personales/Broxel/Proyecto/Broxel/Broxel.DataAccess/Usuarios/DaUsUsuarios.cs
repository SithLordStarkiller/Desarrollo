namespace Broxel.DataAccess.Usuarios
{
    using System.Linq;
    using System.Data;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using Entities;
    using Entities.Entidades;

    public class DaUsUsuarios
    {
        private readonly BroxelEntities _contexto = new BroxelEntities();

        #region Metodos

        public UsUsuariosEntity ObtenUsUsuarionPorLogin(string usuario, string contrasena)
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

        public async Task<USUSUARIOS> InsertaUsuarioLinQ(USUSUARIOS usuario)
        {
            using (var proceso = new Repositorio<USUSUARIOS>())
            {
                return await proceso.Insertar(usuario);
            }
        }

        public List<USUSUARIOS> InsertaListaUsuariosLinQ(List<USUSUARIOS> listaUsuarios)
        {
            var a = _contexto.USUSUARIOS.AddRange(listaUsuarios);
            _contexto.SaveChangesAsync();
            return listaUsuarios;
        }

        #endregion

        #region Convertidores

        private static UsUsuariosEntity ConvertirUsUsuario(DataTable tabla)
        {
            var usuario = tabla.AsEnumerable().Select(row => new UsUsuariosEntity
            {
                IdUsuario = row.Field<int>("IdUsuario"),
                IdEstatus = row.Field<int?>("IdEstatus"),
                Usuario = row.Field<string>("Usuario"),
                Contrasena = row.Field<string>("Contrasena")
            }).FirstOrDefault();

            return usuario;
        }

        private static List<UsUsuariosEntity> ConvertirListaUsUsuario(DataTable tabla)
        {
            var lista = Parallel.ForEach(tabla.AsEnumerable().Select(row => new UsUsuariosEntity
            {
                IdUsuario = row.Field<int>("IdUsuario"),
                IdEstatus = row.Field<int?>("IdEstatus"),
                Usuario = row.Field<string>("Usuario"),
                Contrasena = row.Field<string>("Contrasena")
            }).ToList(), drow =>{});

            var listaUsuario = tabla.AsEnumerable().Select(row => new UsUsuariosEntity
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
