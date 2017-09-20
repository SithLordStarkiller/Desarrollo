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
        private int pages { get; set; }
        public int Pages { get { return pages; } }

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
                cmd.Parameters.Add(new SqlParameter() { Value = paging.All, ParameterName = "@Todos" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FactorMunicipio factoresMunicipio = new FactorMunicipio();

                        factoresMunicipio.Identificador = Convert.ToInt32(reader["IdFactorMunicipio"]);
                        factoresMunicipio.Clasificacion.Identificador = Convert.ToInt32(reader["IdClasificacionFactor"]);
                        factoresMunicipio.IdClasificacionFactor = Convert.ToInt32(reader["IdClasificacionFactor"]);
                        factoresMunicipio.Clasificacion.Nombre = reader["ClasificadorFactor"].ToString();
                        factoresMunicipio.Estado.Identificador = Convert.ToInt32(reader["IdEstado"]);
                        factoresMunicipio.Factor.Identificador = Convert.ToInt32(reader["IdFactor"]);
                        factoresMunicipio.Municipio.Identificador = Convert.ToInt32(reader["IdMunicipio"]);
                        factoresMunicipio.IdFactor = Convert.ToInt32(reader["IdFactor"]);
                        factoresMunicipio.Factor.Nombre = reader["Nombre"].ToString();
                        factoresMunicipio.Descripcion = reader["Descripcion"].ToString();
                        factoresMunicipio.Activo = Convert.ToBoolean(reader["Activo"]);
                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(factoresMunicipio);
                    }
                }
                return result;  // yield?
            }

        }

        public string ValidarRegistro(DataTable dataTable)
        {
            string resultado = "";
            try
            {
                using (var cmd = _unitOfWork.CreateCommand())
                {
                    cmd.CommandText = Factores.FactoresMunicipioValidarRegistros;
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter();
                    parameters[0].ParameterName = Factores.ParametroDataTable;
                    parameters[0].SqlDbType = SqlDbType.Structured;
                    parameters[0].TypeName = Factores.FactorMunicipioTipoTablaUsuario;
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
                        factoresMunicipio.Identificador = Convert.ToInt32(reader["IdFactorMunicipio"]);
                        factoresMunicipio.Clasificacion.Identificador = Convert.ToInt32(reader["IdClasificacionFactor"]);
                        factoresMunicipio.IdClasificacionFactor = Convert.ToInt32(reader["IdClasificacionFactor"]);
                        factoresMunicipio.Clasificacion.Nombre = reader["ClasificadorFactor"].ToString();
                        factoresMunicipio.Estado.Identificador = Convert.ToInt32(reader["IdEstado"]);
                        factoresMunicipio.Factor.Identificador = Convert.ToInt32(reader["IdFactor"]);
                        factoresMunicipio.Municipio.Identificador = Convert.ToInt32(reader["IdMunicipio"]);
                        factoresMunicipio.IdFactor = Convert.ToInt32(reader["IdFactor"]);
                        factoresMunicipio.Factor.Nombre = reader["Nombre"].ToString();
                        factoresMunicipio.Descripcion = reader["Descripcion"].ToString();
                        factoresMunicipio.Activo = Convert.ToBoolean(reader["Activo"]);

                    }
                }
            }

            return factoresMunicipio;
        }

        public IEnumerable<FactorMunicipio> ObtenerMunicipios(FactorMunicipio entity)
        {
            var result = new List<FactorMunicipio>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.FactoresMunicipioObtenerMunicipios;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdClasificacionFactor, ParameterName = "@IdClasificadorFactor" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FactorMunicipio factoresMunicipio = new FactorMunicipio();

                        factoresMunicipio.Identificador = Convert.ToInt32(reader["IdFactorMunicipio"]);
                        factoresMunicipio.Clasificacion.Identificador = Convert.ToInt32(reader["IdClasificacionFactor"]);
                        factoresMunicipio.IdClasificacionFactor = Convert.ToInt32(reader["IdClasificacionFactor"]);
                        factoresMunicipio.Clasificacion.Nombre = reader["ClasificadorFactor"].ToString();
                        factoresMunicipio.Estado.Identificador = Convert.ToInt32(reader["IdEstado"]);
                        factoresMunicipio.IdEstado = Convert.ToInt32(reader["IdEstado"]);
                        factoresMunicipio.Factor.Identificador = Convert.ToInt32(reader["IdFactor"]);
                        factoresMunicipio.Municipio.Identificador = Convert.ToInt32(reader["IdMunicipio"]);
                        factoresMunicipio.IdMunicipio = Convert.ToInt32(reader["IdMunicipio"]);
                        factoresMunicipio.IdFactor = Convert.ToInt32(reader["IdFactor"]);
                        factoresMunicipio.Factor.Nombre = reader["Nombre"].ToString();
                        factoresMunicipio.Descripcion = reader["Descripcion"].ToString();
                        factoresMunicipio.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(factoresMunicipio);
                    }
                }
                return result;  // yield?
            }
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

                parameters[0] = new SqlParameter() { Value = factoresMunicipio.Descripcion, ParameterName = "@Descripcion" };
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
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@Tabla", SqlDbType = SqlDbType.Structured, TypeName = Factores.FactorMunicipioTipoTablaUsuario, Value = datatable });
                result = cmd.ExecuteNonQuery();

                return result;
            }
        }

        public int InsertarFactorMunicipios(IEnumerable<FactorMunicipio> factorMunicipio)
        {
            int result = 0;
            try
            {
                using (var cmd = _unitOfWork.CreateCommand())
                {
                    DataTable dt = ConversorEntityDatatable.TransformarADatatable(factorMunicipio);
                    cmd.CommandText = Factores.FactoresMunicipioInsertar;
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter();
                    parameters[0].ParameterName = Factores.ParametroDataTable;
                    parameters[0].SqlDbType = SqlDbType.Structured;
                    parameters[0].TypeName = Factores.FactorMunicipioTipoTablaUsuario;
                    parameters[0].Value = dt;
                    cmd.Parameters.Add(parameters[0]);

                    System.Data.IDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        int identity = Convert.ToInt32(dr.GetValue(0));
                        string mensaje = dr.GetValue(1).ToString();

                        if (identity == 0)
                        {
                            dr.Close();
                            result = identity;
                            throw new System.Exception(mensaje);
                        }

                    }

                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
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
                parameters[1] = new SqlParameter() { Value = factoresMunicipio.Descripcion, ParameterName = "@Descripcion" };
                parameters[2] = new SqlParameter() { Value = factoresMunicipio.IdClasificacionFactor, ParameterName = "@IdClasificadorFactor" };
                parameters[3] = new SqlParameter() { Value = factoresMunicipio.IdGrupo, ParameterName = "@IdGrupo" };
                parameters[4] = new SqlParameter() { Value = factoresMunicipio.IdEstado, ParameterName = "@IdEstado" };
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
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdClasificacionFactor, ParameterName = "@IdClasificadorFactor" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdFactor, ParameterName = "@IdFactor" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdEstado, ParameterName = "@IdEstado" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdMunicipio, ParameterName = "@IdMunicipio" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FactorMunicipio factoresMunicipio = new FactorMunicipio();

                        factoresMunicipio.Identificador = Convert.ToInt32(reader["IdFactorMunicipio"]);
                        factoresMunicipio.Clasificacion.Identificador = Convert.ToInt32(reader["IdClasificacionFactor"]);
                        factoresMunicipio.IdClasificacionFactor = Convert.ToInt32(reader["IdClasificacionFactor"]);
                        factoresMunicipio.Clasificacion.Nombre = reader["ClasificadorFactor"].ToString();
                        factoresMunicipio.Estado.Identificador = Convert.ToInt32(reader["IdEstado"]);
                        factoresMunicipio.Factor.Identificador = Convert.ToInt32(reader["IdFactor"]);
                        factoresMunicipio.Municipio.Identificador = Convert.ToInt32(reader["IdMunicipio"]);
                        factoresMunicipio.IdFactor = Convert.ToInt32(reader["IdFactor"]);
                        factoresMunicipio.Factor.Nombre = reader["Nombre"].ToString();
                        factoresMunicipio.Descripcion = reader["Descripcion"].ToString();
                        factoresMunicipio.Activo = Convert.ToBoolean(reader["Activo"]);

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
