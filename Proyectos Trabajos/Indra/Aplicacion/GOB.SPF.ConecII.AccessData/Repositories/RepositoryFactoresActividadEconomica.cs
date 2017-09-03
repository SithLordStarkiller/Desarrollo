namespace GOB.SPF.ConecII.AccessData
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;

    public class RepositoryFactoresActividadEconomica : IRepository<FactorActividadEconomica>
    {
        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryFactoresActividadEconomica(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public IEnumerable<FactorActividadEconomica> Obtener(Paging paging)
        {
            var result = new List<FactorActividadEconomica>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.FactoresActividadEconomicaObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FactorActividadEconomica factoresActividadEconomica = new FactorActividadEconomica();

                        factoresActividadEconomica.Identificador = Convert.ToInt32(reader["IdFacActividadEconomica"]);
                        factoresActividadEconomica.DescFacActividadEconomica = reader["DescFacActividadEconomica"].ToString();
                        factoresActividadEconomica.IdFraccion = Convert.ToInt32(reader["IdFraccion"]);
                        factoresActividadEconomica.IdFactor = Convert.ToInt32(reader["IdFactor"]);
                        factoresActividadEconomica.FechaInicio = Convert.ToDateTime(reader["FechaInicio"]);
                        factoresActividadEconomica.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        factoresActividadEconomica.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(factoresActividadEconomica);
                    }
                }
                return result;  // yield?
            }

        }

        public FactorActividadEconomica ObtenerPorId(long id)
        {
            int result = 0;
            FactorActividadEconomica factoresActividadEconomica = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.FactoresActividadEconomicaObtenerId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                factoresActividadEconomica = new FactorActividadEconomica();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        factoresActividadEconomica.Identificador = Convert.ToInt32(reader["IdFacActividadEconomica"]);
                        factoresActividadEconomica.DescFacActividadEconomica = reader["DescFacActividadEconomica"].ToString();
                        factoresActividadEconomica.IdFraccion = Convert.ToInt32(reader["IdFraccion"]);
                        factoresActividadEconomica.IdFactor = Convert.ToInt32(reader["IdFactor"]);
                        factoresActividadEconomica.FechaInicio = Convert.ToDateTime(reader["FechaInicio"]);
                        factoresActividadEconomica.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        factoresActividadEconomica.Activo = Convert.ToBoolean(reader["Activo"]);

                    }
                }
            }

            return factoresActividadEconomica;
        }

        public int CambiarEstatus(FactorActividadEconomica factoresActividadEconomica)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.FactoresActividadEconomicaCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = factoresActividadEconomica.Identificador, ParameterName = "@Identificador" };
                parameters[6] = new SqlParameter() { Value = factoresActividadEconomica.Activo, ParameterName = "@Activo" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                result = cmd.ExecuteNonQuery();

                //Comita nossa transação.
                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Insertar(FactorActividadEconomica factoresActividadEconomica)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.FactoresActividadEconomicaInsertar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[6];

                parameters[0] = new SqlParameter() { Value = factoresActividadEconomica.DescFacActividadEconomica, ParameterName = "@DescFacActividadEconomica" };
                parameters[1] = new SqlParameter() { Value = factoresActividadEconomica.IdFraccion, ParameterName = "@IdFraccion" };
                parameters[2] = new SqlParameter() { Value = factoresActividadEconomica.IdFactor, ParameterName = "@IdFactor" };
                parameters[3] = new SqlParameter() { Value = factoresActividadEconomica.FechaInicio, ParameterName = "@FechaInicio" };
                parameters[4] = new SqlParameter() { Value = factoresActividadEconomica.FechaFinal, ParameterName = "@FechaFinal" };
                parameters[5] = new SqlParameter() { Value = factoresActividadEconomica.Activo, ParameterName = "@Activo" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);
                cmd.Parameters.Add(parameters[3]);
                cmd.Parameters.Add(parameters[4]);
                cmd.Parameters.Add(parameters[5]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Actualizar(FactorActividadEconomica factoresActividadEconomica)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.FactoresActividadEconomicaActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[4];

                parameters[0] = new SqlParameter() { Value = factoresActividadEconomica.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = factoresActividadEconomica.DescFacActividadEconomica, ParameterName = "@DescFacActividadEconomica" };
                parameters[2] = new SqlParameter() { Value = factoresActividadEconomica.IdFraccion, ParameterName = "@IdFraccion" };
                parameters[3] = new SqlParameter() { Value = factoresActividadEconomica.IdFactor, ParameterName = "@IdFactor" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);
                cmd.Parameters.Add(parameters[3]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public IEnumerable<FactorActividadEconomica> ObtenerPorCriterio(Paging paging, FactorActividadEconomica entity)
        {
            var result = new List<FactorActividadEconomica>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.FactoresActividadEconomicaObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.DescFacActividadEconomica, ParameterName = "@DescFacActividadEconomica" });
                
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FactorActividadEconomica factoresActividadEconomica = new FactorActividadEconomica();

                        factoresActividadEconomica.Identificador = Convert.ToInt32(reader["IdFacActividadEconomica"]);
                        factoresActividadEconomica.DescFacActividadEconomica = reader["DescFacActividadEconomica"].ToString();
                        factoresActividadEconomica.IdFraccion = Convert.ToInt32(reader["IdFraccion"]);
                        factoresActividadEconomica.IdFactor = Convert.ToInt32(reader["IdFactor"]);
                        factoresActividadEconomica.FechaInicio = Convert.ToDateTime(reader["FechaInicio"]);
                        factoresActividadEconomica.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        factoresActividadEconomica.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(factoresActividadEconomica);
                    }
                }
                return result;  // yield?
            }
        }
    }
}
