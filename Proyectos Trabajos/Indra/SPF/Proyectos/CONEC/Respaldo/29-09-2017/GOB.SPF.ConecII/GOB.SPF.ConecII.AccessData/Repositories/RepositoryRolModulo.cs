namespace GOB.SPF.ConecII.AccessData.Repositories
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;
    public class RepositoryRolModulo : IRepository<RolModulo>
    {
        private int pages { get; set; }
        public int Pages { get { return pages; } }

        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryRolModulo(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }
        public IEnumerable<RolModulo> Obtener(Paging paging)
        {
            var result = new List<RolModulo>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesModulosObtener;
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
                        RolModulo RolModulo = new RolModulo();

                        RolModulo.Identificador = Convert.ToInt32(reader["IdRolModulo"]);
                        RolModulo.IdRol = Convert.ToInt32(reader["IdRol"]);
                        RolModulo.IdModulo = Convert.ToInt32(reader["IdModulo"]);
                        RolModulo.Activo = reader["Activo"] != DBNull.Value ? Convert.ToBoolean(reader["Activo"]) : false;
                       
                        if (reader["Paginas"] != null)
                            pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(RolModulo);
                    }
                }
                return result;  // yield?
            }

        }

        public RolModulo ObtenerPorId(long id)
        {
            int result = 0;
            RolModulo RolModulo = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesModulosObtenerPorId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                RolModulo = new RolModulo();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RolModulo.Identificador = Convert.ToInt32(reader["IdRolModulo"]);
                        RolModulo.IdRol = Convert.ToInt32(reader["IdRol"]);
                        RolModulo.IdModulo = Convert.ToInt32(reader["IdModulo"]);
                        RolModulo.Activo = Convert.ToBoolean(reader["Activo"]);
                    }
                }
            }

            return RolModulo;
        }

        public int CambiarEstatus(RolModulo RolModulo)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesModulosCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = RolModulo.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = RolModulo.Activo, ParameterName = "@Activo" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                result = cmd.ExecuteNonQuery();

                //Comita nossa transação.
                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Insertar(RolModulo RolModulo)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesModulosInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = RolModulo.IdRol, ParameterName = "@IdRol" });
                cmd.Parameters.Add(new SqlParameter() { Value = RolModulo.IdModulo, ParameterName = "@IdModulo" });

                result = cmd.ExecuteNonQuery();
                return result;
            }
        }

        public int Actualizar(RolModulo RolModulo)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesModulosActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[3];

                parameters[0] = new SqlParameter() { Value = RolModulo.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = RolModulo.IdRol, ParameterName = "@IdRol" };
                parameters[2] = new SqlParameter() { Value = RolModulo.IdModulo, ParameterName = "@IdModulo" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public IEnumerable<RolModulo> ObtenerPorCriterio(Paging paging, RolModulo entity)
        {
            var result = new List<RolModulo>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesModulosObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RolModulo RolModulo = new RolModulo();

                        RolModulo.Identificador = Convert.ToInt32(reader["IdRolModulo"]);
                        RolModulo.IdRol = Convert.ToInt32(reader["IdRol"]);
                        RolModulo.IdModulo = Convert.ToInt32(reader["IdModulo"]);
                        RolModulo.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            RolModulo.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        RolModulo.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(RolModulo);
                    }
                }
                return result;  // yield?
            }
        }

    }
}
