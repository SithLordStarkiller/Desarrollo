namespace GOB.SPF.ConecII.AccessData.Repositories
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;
    public class RepositoryRolesModulosControl: IRepository<RolModuloControl>
    {
        private int pages { get; set; }
        public int Pages { get { return pages; } }

        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryRolesModulosControl(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public IEnumerable<RolModuloControl> Obtener(Paging paging)
        {
            var result = new List<RolModuloControl>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesModulosControlObtener;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RolModuloControl objeto = new RolModuloControl();

                        objeto.Identificador = Convert.ToInt32(reader["IdRolModuloControl"]);
                        objeto.IdRolModulo = Convert.ToInt32(reader["IdRolModulo"]);
                        objeto.IdControl = Convert.ToInt32(reader["IdControl"]);
                        objeto.Captura = Convert.ToBoolean(reader["Captura"]);
                        objeto.Consulta = Convert.ToBoolean(reader["Consulta"]);
                        objeto.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            objeto.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        objeto.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(objeto);
                    }
                }
                return result;  // yield?
            }

        }

        public RolModuloControl ObtenerPorId(long id)
        {
            int result = 0;
            RolModuloControl objeto = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesModulosControlObtenerPorId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                objeto = new RolModuloControl();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        objeto.Identificador = Convert.ToInt32(reader["IdRolModuloControl"]);
                        objeto.IdRolModulo = Convert.ToInt32(reader["IdRolModulo"]);
                        objeto.IdControl = Convert.ToInt32(reader["IdControl"]);
                        objeto.Captura = Convert.ToBoolean(reader["Captura"]);
                        objeto.Consulta = Convert.ToBoolean(reader["Consulta"]);
                        objeto.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            objeto.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        objeto.Activo = Convert.ToBoolean(reader["Activo"]);
                    }
                }
            }

            return objeto;
        }

        public int CambiarEstatus(RolModuloControl entity)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesModulosCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@Identificador" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });

                result = cmd.ExecuteNonQuery();

                //Comita nossa transação.
                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Insertar(RolModuloControl entity)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesModulosControlInsertar;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdRolModulo, ParameterName = "@IdRolModulo" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdControl, ParameterName = "@IdControl" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Captura, ParameterName = "@Captura" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Consulta, ParameterName = "@Consulta" });
                
                result = cmd.ExecuteNonQuery();
                
                return result;
            }
        }

        public int Actualizar(RolModuloControl entity)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesModulosControlActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@Identificador" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdRolModulo, ParameterName = "@IdRolModulo" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdControl, ParameterName = "@IdControl" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Captura, ParameterName = "@Captura" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Consulta, ParameterName = "@Consulta" });
                
                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public IEnumerable<RolModuloControl> ObtenerPorCriterio(Paging paging, RolModuloControl entity)
        {
            var result = new List<RolModuloControl>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesModulosControlObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RolModuloControl objeto = new RolModuloControl();

                        objeto.Identificador = Convert.ToInt32(reader["IdRolModuloControl"]);
                        objeto.IdRolModulo = Convert.ToInt32(reader["IdRolModulo"]);
                        objeto.IdControl = Convert.ToInt32(reader["IdControl"]);
                        objeto.Captura = Convert.ToBoolean(reader["Captura"]);
                        objeto.Consulta = Convert.ToBoolean(reader["Consulta"]);
                        objeto.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            objeto.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        objeto.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(objeto);
                    }
                }
                return result;  // yield?
            }
        }
    }
}
