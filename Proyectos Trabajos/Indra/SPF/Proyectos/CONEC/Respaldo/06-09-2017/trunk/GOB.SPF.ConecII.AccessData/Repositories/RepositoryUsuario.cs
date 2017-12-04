
namespace GOB.SPF.ConecII.AccessData.Repositories
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;
    public class RepositoryUsuario : IRepository<Entities.Usuario>
    {
        private int pages { get; set; }
        public int Pages { get { return pages; } }

        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryUsuario(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }
        public IEnumerable<Entities.Usuario> Obtener(Paging paging)
        {
            var result = new List<Entities.Usuario>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.UsuariosObtener;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" };
                parameters[1] = new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Entities.Usuario Usuario = new Entities.Usuario();

                        Usuario.Identificador = Convert.ToInt32(reader["IdUsuario"]);
                        Usuario.IdPersona = Convert.ToInt32(reader["IdPersona"]);
                        Usuario.IdExterno = Convert.ToInt32(reader["IdExterno"]);
                        Usuario.Login = reader["Login"].ToString();
                        Usuario.Password = Convert.ToByte(reader["Password"]);
                        Usuario.Activo = Convert.ToBoolean(reader["Activo"]);

                        if (reader["Paginas"] != null)
                            pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(Usuario);
                    }
                }
                return result;  // yield?
            }

        }

        public Entities.Usuario ObtenerPorId(long id)
        {
            int result = 0;
            Entities.Usuario Usuario = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.UsuariosObtenerPorId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                Usuario = new Entities.Usuario();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Usuario.Identificador = Convert.ToInt32(reader["IdUsuario"]);
                        Usuario.IdPersona = Convert.ToInt32(reader["IdPersona"]);
                        Usuario.IdExterno = Convert.ToInt32(reader["IdExterno"]);
                        Usuario.Login = reader["Login"].ToString();
                        Usuario.Password = Convert.ToByte(reader["Password"]);
                        Usuario.Activo = Convert.ToBoolean(reader["Activo"]);
                    }
                }
            }

            return Usuario;
        }

        public int CambiarEstatus(Entities.Usuario Usuario)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.UsuariosCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = Usuario.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = Usuario.Activo, ParameterName = "@Activo" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                result = cmd.ExecuteNonQuery();

                //Comita nossa transação.
                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Insertar(Entities.Usuario Usuario)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.UsuariosInserta;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = Usuario.IdPersona, ParameterName = "@IdPersona" });
                cmd.Parameters.Add(new SqlParameter() { Value = Usuario.IdExterno, ParameterName = "@IdExterno" });
                cmd.Parameters.Add(new SqlParameter() { Value = Usuario.Login, ParameterName = "@Login" });
                cmd.Parameters.Add(new SqlParameter() { Value = Usuario.Password, ParameterName = "@Password" });
                //cmd.Parameters.Add(new SqlParameter() { Value = Usuario.Activo, ParameterName = "@Activo" });

                result = cmd.ExecuteNonQuery();
                return result;
            }
        }

        public int Actualizar(Entities.Usuario Usuario)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.UsuariosActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[5];

                parameters[0] = new SqlParameter() { Value = Usuario.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = Usuario.IdPersona, ParameterName = "@IdPersona" };
                parameters[2] = new SqlParameter() { Value = Usuario.IdExterno, ParameterName = "@IdExterno" };
                parameters[3] = new SqlParameter() { Value = Usuario.Login, ParameterName = "@Login" };
                parameters[4] = new SqlParameter() { Value = Usuario.Password, ParameterName = "@Password" };
                //parameters[5] = new SqlParameter() { Value = Usuario.Activo, ParameterName = "@Activo" };
             
                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);
                cmd.Parameters.Add(parameters[3]);
                cmd.Parameters.Add(parameters[4]);
                //cmd.Parameters.Add(parameters[5]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public IEnumerable<Entities.Usuario> ObtenerPorCriterio(Paging paging, Entities.Usuario entity)
        {
            var result = new List<Entities.Usuario>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.UsuariosObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Entities.Usuario Usuario = new Entities.Usuario();

                        Usuario.Identificador = Convert.ToInt32(reader["IdUsuario"]);
                        Usuario.IdPersona = Convert.ToInt32(reader["IdPersona"]);
                        Usuario.IdExterno = Convert.ToInt32(reader["IdExterno"]);
                        Usuario.Login = reader["Login"].ToString();
                        Usuario.Password = Convert.ToByte(reader["Password"]);
                        Usuario.Activo = Convert.ToBoolean(reader["Activo"]);
                        Usuario.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            Usuario.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        Usuario.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(Usuario);
                    }
                }
                return result;  // yield?
            }
        }

    }
}
