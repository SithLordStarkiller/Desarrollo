﻿namespace GOB.SPF.ConecII.AccessData
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;

    public class RepositoryFactoresEntidadFederativa : IRepository<FactorEntidadFederativa>
    {
        private int pages { get; set; }
        public int Pages { get { return pages; } }

        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryFactoresEntidadFederativa(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public IEnumerable<FactorEntidadFederativa> Obtener(Paging paging)
        {
            var result = new List<FactorEntidadFederativa>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.FactoresEntidadFederativaObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.All, ParameterName = "@todos" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FactorEntidadFederativa factoresEntidadFederativa = new FactorEntidadFederativa();

                        factoresEntidadFederativa.Identificador = Convert.ToInt32(reader["IdFactEntidFed"]);
                        factoresEntidadFederativa.Clasificacion.Identificador = Convert.ToInt32(reader["IdClasificadorFactor"]);
                        factoresEntidadFederativa.Clasificacion.Nombre = reader["ClasificadorFactor"].ToString();
                        factoresEntidadFederativa.Estado.Identificador = Convert.ToInt32(reader["IdEntidFed"]);
                        factoresEntidadFederativa.Factor.Identificador = Convert.ToInt32(reader["IdFactor"]);
                        factoresEntidadFederativa.Factor.Nombre = reader["Factor"].ToString();
                        factoresEntidadFederativa.Descripcion = reader["Descripcion"].ToString();
                        factoresEntidadFederativa.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            factoresEntidadFederativa.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        factoresEntidadFederativa.Activo = Convert.ToBoolean(reader["Activo"]);
                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(factoresEntidadFederativa);
                    }
                }
                return result;  // yield?
            }

        }

        public FactorEntidadFederativa ObtenerPorId(long id)
        {
            int result = 0;
            FactorEntidadFederativa factoresEntidadFederativa = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.FactoresEntidadFederativaObtenerId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                factoresEntidadFederativa = new FactorEntidadFederativa();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        factoresEntidadFederativa.Identificador = Convert.ToInt32(reader["IdFactEntidFed"]);
                        factoresEntidadFederativa.Clasificacion.Identificador = Convert.ToInt32(reader["IdClasificadorFactor"]);
                        factoresEntidadFederativa.Clasificacion.Nombre = reader["ClasificadorFactor"].ToString();
                        factoresEntidadFederativa.Estado.Identificador = Convert.ToInt32(reader["IdEntidFed"]);
                        factoresEntidadFederativa.Factor.Identificador = Convert.ToInt32(reader["IdFactor"]);
                        factoresEntidadFederativa.Factor.Nombre = reader["Factor"].ToString();
                        factoresEntidadFederativa.Descripcion = reader["Descripcion"].ToString();
                        factoresEntidadFederativa.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            factoresEntidadFederativa.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        factoresEntidadFederativa.Activo = Convert.ToBoolean(reader["Activo"]);

                    }
                }
            }

            return factoresEntidadFederativa;
        }

        public int CambiarEstatus(FactorEntidadFederativa factoresEntidadFederativa)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.FactoresEntidadFederativaCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = factoresEntidadFederativa.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = factoresEntidadFederativa.Activo, ParameterName = "@Activo" };

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
                cmd.CommandText = Factores.FactoresEntidadFederativaInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@Tabla", SqlDbType = SqlDbType.Structured, Value = dataTable });
                result = cmd.ExecuteNonQuery();
                return result;
            }
        }

        public int Actualizar(FactorEntidadFederativa factoresEntidadFederativa)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.FactoresEntidadFederativaActualizar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = factoresEntidadFederativa.Identificador, ParameterName = "@Identificador" });
                cmd.Parameters.Add(new SqlParameter() { Value = factoresEntidadFederativa.Clasificacion.Identificador, ParameterName = "@IdClasificadorFactor" });
                cmd.Parameters.Add(new SqlParameter() { Value = factoresEntidadFederativa.Descripcion, ParameterName = "@Descripcion" });
                cmd.Parameters.Add(new SqlParameter() { Value = factoresEntidadFederativa.Estado.Identificador, ParameterName = "@IdEntidFed" });
                cmd.Parameters.Add(new SqlParameter() { Value = factoresEntidadFederativa.Factor.Identificador, ParameterName = "@IdFactor" });
                
                result = cmd.ExecuteNonQuery();

                return result;
            }
        }

        public IEnumerable<FactorEntidadFederativa> ObtenerPorCriterio(Paging paging, FactorEntidadFederativa entity)
        {
            var result = new List<FactorEntidadFederativa>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.FactoresEntidadFederativaObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FactorEntidadFederativa factoresEntidadFederativa = new FactorEntidadFederativa();

                        factoresEntidadFederativa.Identificador = Convert.ToInt32(reader["IdFactEntidFed"]);
                        factoresEntidadFederativa.Clasificacion.Identificador = Convert.ToInt32(reader["IdClasificadorFactor"]);
                        factoresEntidadFederativa.Clasificacion.Nombre = reader["ClasificadorFactor"].ToString();
                        factoresEntidadFederativa.Estado.Identificador = Convert.ToInt32(reader["IdEntidFed"]);
                        factoresEntidadFederativa.Factor.Identificador = Convert.ToInt32(reader["IdFactor"]);
                        factoresEntidadFederativa.Factor.Nombre = reader["Factor"].ToString();
                        factoresEntidadFederativa.Descripcion = reader["Descripcion"].ToString();
                        factoresEntidadFederativa.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            factoresEntidadFederativa.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        factoresEntidadFederativa.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(factoresEntidadFederativa);
                    }
                }
                return result;  // yield?
            }
        }

        public int InsertarFactorEntidadesFederativas(IEnumerable<FactorEntidadFederativa> factorEntidadesFederativas)
        {
            int result = 0;
            try
            {
                using (var cmd = _unitOfWork.CreateCommand())
                {
                    DataTable dt = ConversorEntityDatatable.TransformarADatatable(factorEntidadesFederativas);
                    cmd.CommandText = Factores.FactorEntidadFederativaInsecciones;
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter();
                    parameters[0].ParameterName = Factores.FactorEntidadFederativaParametro01Insertar;
                    parameters[0].SqlDbType = SqlDbType.Structured;
                    parameters[0].TypeName = Factores.FactorEntidadFederativaTipoTablaUsuario;
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

        public int Insertar(FactorEntidadFederativa entity)
        {
            throw new NotImplementedException();
        }
    }
}
