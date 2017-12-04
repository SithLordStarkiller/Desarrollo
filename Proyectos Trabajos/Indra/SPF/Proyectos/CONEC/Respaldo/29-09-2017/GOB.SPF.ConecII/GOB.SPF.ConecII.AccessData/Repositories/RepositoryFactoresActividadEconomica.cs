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
        private int pages { get; set; }
        public int Pages { get { return pages; } }

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

                cmd.Parameters.Add(new SqlParameter() { Value = paging.All, ParameterName = "@Todos" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FactorActividadEconomica factoresActividadEconomica = new FactorActividadEconomica();

                        factoresActividadEconomica.Identificador = Convert.ToInt32(reader["IdFactorActividadEconomica"]);
                        factoresActividadEconomica.IdClasificacionFactor = Convert.ToInt32(reader["IdClasificacionFactor"]);
                        factoresActividadEconomica.Clasificacion.Identificador = Convert.ToInt32(reader["IdClasificacionFactor"]);
                        factoresActividadEconomica.Clasificacion.Nombre = reader["ClasificadorFactor"].ToString();
                        factoresActividadEconomica.IdFactor = Convert.ToInt32(reader["IdFactor"]);
                        factoresActividadEconomica.Factor.Identificador = Convert.ToInt32(reader["IdFactor"]);
                        factoresActividadEconomica.Factor.Nombre = reader["Nombre"].ToString();
                        factoresActividadEconomica.IdFraccion = Convert.ToInt32(reader["IdFraccion"]);
                        factoresActividadEconomica.Fraccion.Identificador = Convert.ToInt32(reader["IdFraccion"]);
                        factoresActividadEconomica.Fraccion.Nombre = reader["Fraccion"].ToString();
                        factoresActividadEconomica.IdGrupo = Convert.ToInt32(reader["IdGrupo"]);
                        factoresActividadEconomica.Grupo.Identificador = Convert.ToInt32(reader["IdGrupo"]);
                        factoresActividadEconomica.Grupo.Nombre = reader["Grupo"].ToString();
                        factoresActividadEconomica.IdDivision = Convert.ToInt32(reader["IdDivision"]);
                        factoresActividadEconomica.Division.Identificador = Convert.ToInt32(reader["IdDivision"]);
                        factoresActividadEconomica.Division.NombreDivision = reader["Division"].ToString();
                        factoresActividadEconomica.Descripcion = reader["Descripcion"].ToString();
                        factoresActividadEconomica.Activo = Convert.ToBoolean(reader["Activo"]);
                        pages = Convert.ToInt32(reader["Paginas"]);

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
                        factoresActividadEconomica.Identificador = Convert.ToInt32(reader["IdFactorActividadEconomica"]);
                        factoresActividadEconomica.IdClasificacionFactor = Convert.ToInt32(reader["IdClasificacionFactor"]);
                        factoresActividadEconomica.Clasificacion.Identificador = Convert.ToInt32(reader["IdClasificacionFactor"]);
                        factoresActividadEconomica.Clasificacion.Nombre = reader["ClasificadorFactor"].ToString();
                        factoresActividadEconomica.IdFactor = Convert.ToInt32(reader["IdFactor"]);
                        factoresActividadEconomica.Factor.Identificador = Convert.ToInt32(reader["IdFactor"]);
                        factoresActividadEconomica.Factor.Nombre = reader["Nombre"].ToString();
                        factoresActividadEconomica.IdFraccion = Convert.ToInt32(reader["IdFraccion"]);
                        factoresActividadEconomica.Fraccion.Identificador = Convert.ToInt32(reader["IdFraccion"]);
                        factoresActividadEconomica.Fraccion.Nombre = reader["Fraccion"].ToString();
                        factoresActividadEconomica.IdGrupo = Convert.ToInt32(reader["IdGrupo"]);
                        factoresActividadEconomica.Grupo.Identificador = Convert.ToInt32(reader["IdGrupo"]);
                        factoresActividadEconomica.Grupo.Nombre = reader["Grupo"].ToString();
                        factoresActividadEconomica.IdDivision = Convert.ToInt32(reader["IdDivision"]);
                        factoresActividadEconomica.Division.Identificador = Convert.ToInt32(reader["IdDivision"]);
                        factoresActividadEconomica.Division.NombreDivision = reader["Division"].ToString();
                        factoresActividadEconomica.Descripcion = reader["Descripcion"].ToString();
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
                
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = factoresActividadEconomica.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = factoresActividadEconomica.Activo, ParameterName = "@Activo" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                result = cmd.ExecuteNonQuery();

                //Comita nossa transação.
                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Insertar(DataTable dataTable)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.FactorActividadEconomicaInsertar;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter { ParameterName = "@Tabla", SqlDbType = SqlDbType.Structured, TypeName = Catalogos.FactorActividadEconomicaTipoTablaUsuario, Value = dataTable });
                result = cmd.ExecuteNonQuery();
                return result;
            }
        }

        public string ValidarRegistro(DataTable dataTable)
        {
            string resultado = "";
            try
            {
                using (var cmd = _unitOfWork.CreateCommand())
                {
                    cmd.CommandText = Catalogos.FactorActividadEconomicaValidarRegistro;
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter();
                    parameters[0].ParameterName = Factores.ParametroDataTable;
                    parameters[0].SqlDbType = SqlDbType.Structured;
                    parameters[0].TypeName = Catalogos.FactorActividadEconomicaTipoTablaUsuario;
                    parameters[0].Value = dataTable;
                    cmd.Parameters.Add(parameters[0]);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            resultado = reader["Resultado"].ToString();
                        }
                        reader.Close();
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultado;
        }

        public int Actualizar(FactorActividadEconomica factoresActividadEconomica)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {

                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[4];

                parameters[0] = new SqlParameter() { Value = factoresActividadEconomica.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = factoresActividadEconomica.Descripcion, ParameterName = "@Descripcion" };
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
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdClasificacionFactor, ParameterName = "@IdClasificadorFactor" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdFactor, ParameterName = "@IdFactor" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdGrupo, ParameterName = "@IdGrupo" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdDivision, ParameterName = "@IdDivision" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FactorActividadEconomica factoresActividadEconomica = new FactorActividadEconomica();

                        factoresActividadEconomica.Identificador = Convert.ToInt32(reader["IdFactorActividadEconomica"]);
                        factoresActividadEconomica.IdClasificacionFactor = Convert.ToInt32(reader["IdClasificacionFactor"]);
                        factoresActividadEconomica.Clasificacion.Identificador = Convert.ToInt32(reader["IdClasificacionFactor"]);
                        factoresActividadEconomica.Clasificacion.Nombre = reader["ClasificadorFactor"].ToString();
                        factoresActividadEconomica.IdFactor = Convert.ToInt32(reader["IdFactor"]);
                        factoresActividadEconomica.Factor.Identificador = Convert.ToInt32(reader["IdFactor"]);
                        factoresActividadEconomica.Factor.Nombre = reader["Nombre"].ToString();
                        factoresActividadEconomica.IdFraccion = Convert.ToInt32(reader["IdFraccion"]);
                        factoresActividadEconomica.Fraccion.Identificador = Convert.ToInt32(reader["IdFraccion"]);
                        factoresActividadEconomica.Fraccion.Nombre = reader["Fraccion"].ToString();
                        factoresActividadEconomica.IdGrupo = Convert.ToInt32(reader["IdGrupo"]);
                        factoresActividadEconomica.Grupo.Identificador = Convert.ToInt32(reader["IdGrupo"]);
                        factoresActividadEconomica.Grupo.Nombre = reader["Grupo"].ToString();
                        factoresActividadEconomica.IdDivision = Convert.ToInt32(reader["IdDivision"]);
                        factoresActividadEconomica.Division.Identificador = Convert.ToInt32(reader["IdDivision"]);
                        factoresActividadEconomica.Division.NombreDivision = reader["Division"].ToString();
                        factoresActividadEconomica.Descripcion = reader["Descripcion"].ToString();
                        factoresActividadEconomica.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(factoresActividadEconomica);
                    }
                }
                return result;  // yield?
            }
        }

        public int Insertar(FactorActividadEconomica entity)
        {
            throw new NotImplementedException();
        }
    }
}
