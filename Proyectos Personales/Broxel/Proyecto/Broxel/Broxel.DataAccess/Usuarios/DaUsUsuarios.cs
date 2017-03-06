namespace Broxel.DataAccess.Usuarios
{
    using System;
    using System.Linq;
    using System.Data;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using System.Reflection;
    using Entities;
    using Helpers.Log;
    using Helpers.CustomExceptions;

    /// <summary>
    /// Clase encargada de manejar el acceso a datos de los usuarios
    /// </summary>
    public class DaUsUsuarios
    {
        private readonly BroxelEntities _contexto = new BroxelEntities();

        private readonly Logger _logLogger = new Logger(Constantes.UrlLog, Constantes.ConnectionStringTrabajo);

        #region Metodos

        public USUSUARIOS ObtenUsUsuarionPorLoginAdo(string usuario, string contrasena)
        {
            try
            {
                var parametros = new SqlParameter[2];

                parametros[0] = new SqlParameter { ParameterName = "@Usuario", DbType = DbType.String, Value = usuario };
                parametros[1] = new SqlParameter { ParameterName = "@Contrasena", DbType = DbType.String, Value = contrasena };

                var task =
                    Task<DataTable>.Factory.StartNew(
                        () =>
                            new ManejadorSqlServer().ExecuteDataTable(Constantes.ConexionBaseDatos,
                                CommandType.StoredProcedure, "Usp_ObtenUsUsuarionPorLogin", parametros));

                var resultado = task.Result;

                if (resultado == null || resultado.Rows.Count <= 0)
                    throw new UserNotFindException(usuario, contrasena);

                var usuariResultado = (USUSUARIOS)ConvertirUsUsuario(resultado, false);

                return usuariResultado;
            }
            catch (Exception e)
            {
                Task.Factory.StartNew(
                    () =>
                        _logLogger.EscribeLog(Logger.TipoLog.Preventivo,
                            Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                            MethodBase.GetCurrentMethod().Name, "Login error", "", e, ""));
                throw;
            }
        }

        public async Task<USUSUARIOS> ObtenUsUsuarionPorLoginLinQ(string usuario, string contrasena)
        {
            try
            {
                using (var proceso = new Repositorio<USUSUARIOS>())
                {
                    var resultado = await proceso.Consulta(x => x.USUARIO == usuario && x.CONTRASENA == contrasena);

                    if (resultado == null)
                        throw new UserNotFindException(usuario, contrasena);

                    return resultado;
                }
            }
            catch (Exception e)
            {
                _logLogger.EscribeLog(Logger.TipoLog.Preventivo,
                    Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                    MethodBase.GetCurrentMethod().Name, "Login error", "", e, "");
                throw;
            }
        }

        public async Task<USUSUARIOS> InsertaUsuarioLinQ(USUSUARIOS usuario)
        {
            try
            {
                using (var proceso = new Repositorio<USUSUARIOS>())
                {
                    return await proceso.Insertar(usuario);
                }
            }
            catch (Exception e)
            {
                           _logLogger.EscribeLog(Logger.TipoLog.Preventivo,
                               Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                               MethodBase.GetCurrentMethod().Name, "Login error", "", e, "");
                throw;
            }
        }

        public List<USUSUARIOS> InsertaListaUsuariosLinQ(List<USUSUARIOS> listaUsuarios)
        {
            try
            {
                _contexto.USUSUARIOS.AddRange(listaUsuarios);
                //_contexto.();
                return listaUsuarios;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Convertidores

        public static object ConvertirUsUsuario(DataTable tabla, bool list)
        {
            try
            {
                if (!list)
                {
                    var usuario = tabla.AsEnumerable().Select(row => new USUSUARIOS
                    {
                        IDUSUARIO = row.Field<int>("IDUSUARIO"),
                        IDESTATUS = row.Field<int?>("IDESTATUS"),
                        USUARIO = row.Field<string>("USUARIO"),
                        CONTRASENA = row.Field<string>("CONTRASENA")
                    });

                    return usuario.FirstOrDefault();
                }

                var lista = Parallel.ForEach(tabla.AsEnumerable().Select(row => new USUSUARIOS
                {
                    IDUSUARIO = row.Field<int>("IDUSUARIO"),
                    IDESTATUS = row.Field<int?>("IDESTATUS"),
                    USUARIO = row.Field<string>("USUARIO"),
                    CONTRASENA = row.Field<string>("CONTRASENA")
                }).ToList(), drow => { });

                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
