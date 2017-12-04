namespace GOB.SPF.ConecII.AccessData
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;
    using System.Globalization;

    public class RepositoryFactoresLeyIngreso : IRepository<FactorLeyIngreso>
    {
        private int pages { get; set; }
        public int Pages { get { return pages; } }

        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryFactoresLeyIngreso(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public IEnumerable<FactorLeyIngreso> Obtener(Paging paging)
        {
            var result = new List<FactorLeyIngreso>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.FactoresLeyIngresoObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FactorLeyIngreso factoresLeyIngreso = new FactorLeyIngreso();

                        factoresLeyIngreso.Identificador = Convert.ToInt32(reader["IdFactorLI"]);
                        factoresLeyIngreso.IdAnio = Convert.ToInt32(reader["IdAnio"]);
                        factoresLeyIngreso.Anio = reader["Anio"].ToString();
                        factoresLeyIngreso.IdMes = Convert.ToInt32(reader["IdMes"]);
                        factoresLeyIngreso.Mes = reader["Mes"].ToString();
                        factoresLeyIngreso.Factor = Convert.ToDouble(reader["Factor"]);
                        factoresLeyIngreso.FactorTexto = Convert.ToString(reader["Factor"]);
                        factoresLeyIngreso.Activo = Convert.ToBoolean(reader["Activo"]);
                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(factoresLeyIngreso);
                    }
                }
                return result;  // yield?
            }

        }

        public FactorLeyIngreso ObtenerPorId(long id)
        {
            int result = 0;
            FactorLeyIngreso factoresLeyIngreso = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.FactoresLeyIngresoObtenerId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                factoresLeyIngreso = new FactorLeyIngreso();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        factoresLeyIngreso.Identificador = Convert.ToInt32(reader["IdFactorLI"]);
                        factoresLeyIngreso.IdAnio = Convert.ToInt32(reader["IdAnio"]);
                        factoresLeyIngreso.Anio = reader["Anio"].ToString();
                        factoresLeyIngreso.IdMes = Convert.ToInt32(reader["IdMes"]);
                        factoresLeyIngreso.Mes = reader["Mes"].ToString();
                        factoresLeyIngreso.Factor = Convert.ToDouble(reader["Factor"]);
                        factoresLeyIngreso.FactorTexto = Convert.ToString(reader["Factor"]);
                        factoresLeyIngreso.Activo = Convert.ToBoolean(reader["Activo"]);

                    }
                }
            }

            return factoresLeyIngreso;
        }

        public int CambiarEstatus(FactorLeyIngreso factoresLeyIngreso)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.FactoresLeyIngresoCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = factoresLeyIngreso.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = factoresLeyIngreso.Activo, ParameterName = "@Activo" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                result = cmd.ExecuteNonQuery();

                //Comita nossa transação.
                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Insertar(FactorLeyIngreso factoresLeyIngreso)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.FactoresLeyIngresoInsertar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[3];
                
                parameters[0] = new SqlParameter() { Value = factoresLeyIngreso.IdAnio, ParameterName = "@IdAnio" };
                parameters[1] = new SqlParameter() { Value = factoresLeyIngreso.IdMes, ParameterName = "@IdMes" };
                parameters[2] = new SqlParameter() { Value = Convert.ToDouble(factoresLeyIngreso.Factor), ParameterName = "@Factor" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Actualizar(FactorLeyIngreso factoresLeyIngreso)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.FactoresLeyIngresoActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[4];

                parameters[0] = new SqlParameter() { Value = factoresLeyIngreso.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = factoresLeyIngreso.IdAnio, ParameterName = "@IdAnio" };
                parameters[2] = new SqlParameter() { Value = factoresLeyIngreso.IdMes, ParameterName = "@IdMes" };
                parameters[3] = new SqlParameter() { Value = Convert.ToDouble(factoresLeyIngreso.Factor), ParameterName = "@Factor" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);
                cmd.Parameters.Add(parameters[3]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public IEnumerable<FactorLeyIngreso> ObtenerPorCriterio(Paging paging, FactorLeyIngreso entity)
        {
            var result = new List<FactorLeyIngreso>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.FactoresLeyIngresoObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdAnio, ParameterName = "@IdAnio" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FactorLeyIngreso factoresLeyIngreso = new FactorLeyIngreso();

                        factoresLeyIngreso.Identificador = Convert.ToInt32(reader["IdFactorLI"]);
                        factoresLeyIngreso.IdAnio = Convert.ToInt32(reader["IdAnio"]);
                        factoresLeyIngreso.Anio = reader["Anio"].ToString();
                        factoresLeyIngreso.IdMes = Convert.ToInt32(reader["IdMes"]);
                        factoresLeyIngreso.Mes = reader["Mes"].ToString();
                        factoresLeyIngreso.Factor = Convert.ToDouble(reader["Factor"]);
                        factoresLeyIngreso.FactorTexto = Convert.ToString(reader["Factor"]);
                        factoresLeyIngreso.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(factoresLeyIngreso);
                    }
                }
                return result;  // yield?
            }
        }

        public string ValidarRegistro(FactorLeyIngreso entity)
        {
            string result = "";

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.FactoresLeyIngresoValidarRegistro;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[3];

                parameters[0] = new SqlParameter() { Value = entity.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = entity.IdAnio, ParameterName = "@IdAnio" };
                parameters[2] = new SqlParameter() { Value = entity.IdMes, ParameterName = "@IdMes" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);


                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = reader["Resultado"].ToString();
                    }
                    reader.Close();
                }
            }

            return result;
        }

    }
}
