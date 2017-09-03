namespace GOB.SPF.ConecII.AccessData
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;

    public class RepositoryFactoresMunicipio : IRepository<FactorMunicipio>
    {
        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryFactoresMunicipio(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public IEnumerable<FactorMunicipio> Obtener(Paging paging)
        {
            var result = new List<FactorMunicipio>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.FactoresMunicipioObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FactorMunicipio factoresMunicipio = new FactorMunicipio();

                        factoresMunicipio.Identificador = Convert.ToInt32(reader["IdFactMpio"]);
                        factoresMunicipio.DescFactMpio = reader["DescFactMpio"].ToString();
                        factoresMunicipio.IdClasificadorFactor = Convert.ToInt32(reader["IdClasificadorFactor"]);
                        factoresMunicipio.IdGrupo = Convert.ToInt32(reader["IdGrupo"]);
                        factoresMunicipio.IdEntidFed = Convert.ToInt32(reader["IdEntidFed"]);
                        factoresMunicipio.DescEntidFed = reader["DescEntidFed"].ToString();
                        factoresMunicipio.FechaInicio = Convert.ToDateTime(reader["FechaInicio"]);
                        factoresMunicipio.FechaFin = Convert.ToDateTime(reader["FechaFin"]);
                        factoresMunicipio.Activo = Convert.ToBoolean(reader["Activo"]);
                        factoresMunicipio.IdFactor = Convert.ToInt32(reader["IdFactor"]);

                        result.Add(factoresMunicipio);
                    }
                }
                return result;  // yield?
            }

        }

        public FactorMunicipio ObtenerPorId(long id)
        {
            int result = 0;
            FactorMunicipio factoresMunicipio = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.FactoresMunicipioObtenerId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                factoresMunicipio = new FactorMunicipio();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        factoresMunicipio.Identificador = Convert.ToInt32(reader["IdFactMpio"]);
                        factoresMunicipio.DescFactMpio = reader["DescFactMpio"].ToString();
                        factoresMunicipio.IdClasificadorFactor = Convert.ToInt32(reader["IdClasificadorFactor"]);
                        factoresMunicipio.IdGrupo = Convert.ToInt32(reader["IdGrupo"]);
                        factoresMunicipio.IdEntidFed = Convert.ToInt32(reader["IdEntidFed"]);
                        factoresMunicipio.DescEntidFed = reader["DescEntidFed"].ToString();
                        factoresMunicipio.FechaInicio = Convert.ToDateTime(reader["FechaInicio"]);
                        factoresMunicipio.FechaFin = Convert.ToDateTime(reader["FechaFin"]);
                        factoresMunicipio.Activo = Convert.ToBoolean(reader["Activo"]);
                        factoresMunicipio.IdFactor = Convert.ToInt32(reader["IdFactor"]);

                    }
                }
            }

            return factoresMunicipio;
        }

        public int CambiarEstatus(FactorMunicipio factoresMunicipio)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.FactoresMunicipioCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[8];

                parameters[0] = new SqlParameter() { Value = factoresMunicipio.DescFactMpio, ParameterName = "@DescFactMpio" };
                parameters[1] = new SqlParameter() { Value = factoresMunicipio.Activo, ParameterName = "@Activo" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                result = cmd.ExecuteNonQuery();

                //Comita nossa transação.
                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Insertar(DataTable datatable)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.FactoresMunicipioInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@Tabla", SqlDbType = SqlDbType.Structured, Value = datatable });                
                result = cmd.ExecuteNonQuery();

                return result;
            }
        }

        public int Actualizar(FactorMunicipio factoresMunicipio)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.FactoresMunicipioActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[7];

                parameters[0] = new SqlParameter() { Value = factoresMunicipio.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = factoresMunicipio.DescFactMpio, ParameterName = "@DescFactMpio" };
                parameters[2] = new SqlParameter() { Value = factoresMunicipio.IdClasificadorFactor, ParameterName = "@IdClasificadorFactor" };
                parameters[3] = new SqlParameter() { Value = factoresMunicipio.IdGrupo, ParameterName = "@IdGrupo" };
                parameters[4] = new SqlParameter() { Value = factoresMunicipio.IdEntidFed, ParameterName = "@IdEntidFed" };
                parameters[5] = new SqlParameter() { Value = factoresMunicipio.DescEntidFed, ParameterName = "@DescEntidFed" };
                parameters[6] = new SqlParameter() { Value = factoresMunicipio.IdFactor, ParameterName = "@IdFactor" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);
                cmd.Parameters.Add(parameters[3]);
                cmd.Parameters.Add(parameters[4]);
                cmd.Parameters.Add(parameters[5]);
                cmd.Parameters.Add(parameters[6]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public IEnumerable<FactorMunicipio> ObtenerPorCriterio(Paging paging, FactorMunicipio entity)
        {
            var result = new List<FactorMunicipio>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.FactoresMunicipioObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.DescFactMpio, ParameterName = "@DescFactMpio" });
                
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FactorMunicipio factoresMunicipio = new FactorMunicipio();

                        factoresMunicipio.Identificador = Convert.ToInt32(reader["IdFactMpio"]);
                        factoresMunicipio.DescFactMpio = reader["DescFactMpio"].ToString();
                        factoresMunicipio.IdClasificadorFactor = Convert.ToInt32(reader["IdClasificadorFactor"]);
                        factoresMunicipio.IdGrupo = Convert.ToInt32(reader["IdGrupo"]);
                        factoresMunicipio.IdEntidFed = Convert.ToInt32(reader["IdEntidFed"]);
                        factoresMunicipio.DescEntidFed = reader["DescEntidFed"].ToString();
                        factoresMunicipio.FechaInicio = Convert.ToDateTime(reader["FechaInicio"]);
                        factoresMunicipio.FechaFin = Convert.ToDateTime(reader["FechaFin"]);
                        factoresMunicipio.Activo = Convert.ToBoolean(reader["Activo"]);
                        factoresMunicipio.IdFactor = Convert.ToInt32(reader["IdFactor"]);

                        result.Add(factoresMunicipio);
                    }
                }
                return result;  // yield?
            }
        }

        public int Insertar(FactorMunicipio entity)
        {
            throw new NotImplementedException();
        }
    }
}
