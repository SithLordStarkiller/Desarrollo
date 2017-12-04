namespace GOB.SPF.ConecII.AccessData.Repositories
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;
    public class RepositoryRolUsuario : IRepository<RolUsuario>
    {
        private int pages { get; set; }
        public int Pages { get { return pages; } }

        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryRolUsuario(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }
        public IEnumerable<RolUsuario> Obtener(Paging paging)
        {
            var result = new List<RolUsuario>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesUsuariosObtener;
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
                        RolUsuario RolUsuario = new RolUsuario();
                        RolUsuario.Identificador = Convert.ToInt32(reader["IdRolesUsuarios"]);
                        RolUsuario.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);
                        RolUsuario.IdRol = Convert.ToInt32(reader["IdRol"]);
                        RolUsuario.Activo = Convert.ToBoolean(reader["Activo"]);

                        if (reader["Paginas"] != null)
                            pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(RolUsuario);
                    }
                }
                return result;  // yield?
            }

        }

        public RolUsuario ObtenerPorId(long id)
        {
            int result = 0;
            RolUsuario RolUsuario = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesUsuariosObtenerPorId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                RolUsuario = new RolUsuario();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RolUsuario.Identificador = Convert.ToInt32(reader["IdRolesUsuarios"]);
                        RolUsuario.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);
                        RolUsuario.IdRol = Convert.ToInt32(reader["IdRol"]);
                        RolUsuario.Activo = Convert.ToBoolean(reader["Activo"]);
                    }
                }
            }

            return RolUsuario;
        }

        public int CambiarEstatus(RolUsuario RolUsuario)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesUsuariosCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = RolUsuario.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = RolUsuario.Activo, ParameterName = "@Activo" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                result = cmd.ExecuteNonQuery();

                //Comita nossa transação.
                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Insertar(RolUsuario RolUsuario)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesUsuariosInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = RolUsuario.IdUsuario, ParameterName = "@IdUsuario" });
                cmd.Parameters.Add(new SqlParameter() { Value = RolUsuario.IdRol, ParameterName = "@IdRol" });
                
                result = cmd.ExecuteNonQuery();
                return result;
            }
        }

        public int Actualizar(RolUsuario RolUsuario)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesUsuariosActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[3];

                parameters[0] = new SqlParameter() { Value = RolUsuario.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = RolUsuario.IdUsuario, ParameterName = "@IdUsuario" };
                parameters[2] = new SqlParameter() { Value = RolUsuario.IdRol, ParameterName = "@IdRol" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public IEnumerable<RolUsuario> ObtenerPorCriterio(Paging paging, RolUsuario entity)
        {
            var result = new List<RolUsuario>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesUsuariosObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RolUsuario RolUsuario = new RolUsuario();

                        RolUsuario.Identificador = Convert.ToInt32(reader["IdRolesUsuarios"]);
                        RolUsuario.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);
                        RolUsuario.IdRol = Convert.ToInt32(reader["IdRol"]);
                        RolUsuario.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            RolUsuario.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        RolUsuario.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(RolUsuario);
                    }
                }
                return result;  // yield?
            }
        }

    }
}
