namespace GOB.SPF.ConecII.AccessData
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;

    public class RepositoryFactores : IRepository<Factor>
    {
        private int pages { get; set; }
        public int Pages { get { return pages; } }

        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryFactores(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public IEnumerable<Factor> Obtener(Paging paging)
        {
            var result = new List<Factor>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.FactoresObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.All, ParameterName = "@todos" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Factor factores = new Factor();

                        factores.Identificador = Convert.ToInt32(reader["IdFactor"]);
                        factores.IdTipoServicio = Convert.ToInt32(reader["IdTipoServicio"]);
                        factores.TipoServicio = reader["TipoServicio"].ToString();
                        factores.IdClasificacionFactor = Convert.ToInt32(reader["IdClasificacionFactor"]);
                        factores.ClasificadorFactor = reader["ClasificadorFactor"].ToString();
                        factores.IdMedidaCobro = Convert.ToInt32(reader["IdMedidaCobro"]);
                        factores.MedidaCobro = reader["MedidaCobro"].ToString();
                        factores.Nombre = Convert.ToString(reader["Factor"]);
                        factores.Descripcion = Convert.ToString(reader["Descripcion"]);
                        factores.CuotaFactor = Convert.ToDecimal(reader["CuotaFactor"]);
                        factores.FechaAutorizacion = Convert.ToDateTime(reader["FechaAutorizacion"]);
                        factores.FechaEntradaVigor = Convert.ToDateTime(reader["FechaEntradaVigor"]);
                        factores.FechaTermino = Convert.ToDateTime(reader["FechaTermino"]);
                        factores.FechaPublicacionDof = Convert.ToDateTime(reader["FechaPublicacionDof"]);
                        factores.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            factores.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        factores.Activo = Convert.ToBoolean(reader["Activo"]);
                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(factores);
                    }
                }
                return result;  // yield?
            }

        }

        public Factor ObtenerPorId(long id)
        {
            int result = 0;
            Factor factores = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.FactoresObtenerId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                factores = new Factor();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        factores.Identificador = Convert.ToInt32(reader["IdFactor"]);
                        factores.IdTipoServicio = Convert.ToInt32(reader["IdTipoServicio"]);
                        factores.TipoServicio = reader["TipoServicio"].ToString();
                        factores.IdClasificacionFactor = Convert.ToInt32(reader["IdClasificacionFactor"]);
                        factores.ClasificadorFactor = reader["ClasificadorFactor"].ToString();
                        factores.IdMedidaCobro = Convert.ToInt32(reader["IdMedidaCobro"]);
                        factores.MedidaCobro = reader["MedidaCobro"].ToString();
                        factores.Nombre = Convert.ToString(reader["Factor"]);
                        factores.Descripcion = Convert.ToString(reader["Descripcion"]);
                        factores.CuotaFactor = Convert.ToDecimal(reader["CuotaFactor"]);
                        factores.FechaAutorizacion = Convert.ToDateTime(reader["FechaAutorizacion"]);
                        factores.FechaEntradaVigor = Convert.ToDateTime(reader["FechaEntradaVigor"]);
                        factores.FechaTermino = Convert.ToDateTime(reader["FechaTermino"]);
                        factores.FechaPublicacionDof = Convert.ToDateTime(reader["FechaPublicacionDof"]);
                        factores.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            factores.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        factores.Activo = Convert.ToBoolean(reader["Activo"]);

                    }
                }
            }

            return factores;
        }

        public int CambiarEstatus(Factor factores)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.FactoresCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = factores.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = factores.Activo, ParameterName = "@Activo" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                result = cmd.ExecuteNonQuery();

                //Comita nossa transação.
                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Insertar(Factor factores)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.FactoresInsertar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[10];

                parameters[0] = new SqlParameter() { Value = factores.IdTipoServicio, ParameterName = "@IdTipoServicio" };
                parameters[1] = new SqlParameter() { Value = factores.IdClasificacionFactor, ParameterName = "@IdClasificacionFactor" };
                parameters[2] = new SqlParameter() { Value = factores.IdMedidaCobro, ParameterName = "@IdMedidaCobro" };
                parameters[3] = new SqlParameter() { Value = factores.Nombre, ParameterName = "@Factor" };
                parameters[4] = new SqlParameter() { Value = factores.Descripcion, ParameterName = "@Descripcion" };
                parameters[5] = new SqlParameter() { Value = factores.CuotaFactor, ParameterName = "@CuotaFactor" };
                parameters[6] = new SqlParameter() { Value = factores.FechaAutorizacion, ParameterName = "@FechaAutorizacion" };
                parameters[7] = new SqlParameter() { Value = factores.FechaEntradaVigor, ParameterName = "@FechaEntradaVigor" };
                parameters[8] = new SqlParameter() { Value = factores.FechaTermino, ParameterName = "@FechaTermino" };
                parameters[9] = new SqlParameter() { Value = factores.FechaPublicacionDof, ParameterName = "@FechaPublicacionDof" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);
                cmd.Parameters.Add(parameters[3]);
                cmd.Parameters.Add(parameters[4]);
                cmd.Parameters.Add(parameters[5]);
                cmd.Parameters.Add(parameters[6]);
                cmd.Parameters.Add(parameters[7]);
                cmd.Parameters.Add(parameters[8]);
                cmd.Parameters.Add(parameters[9]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Actualizar(Factor factores)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.FactoresActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[11];

                parameters[0] = new SqlParameter() { Value = factores.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = factores.IdTipoServicio, ParameterName = "@IdTipoServicio" };
                parameters[2] = new SqlParameter() { Value = factores.IdClasificacionFactor, ParameterName = "@IdClasificacionFactor" };
                parameters[3] = new SqlParameter() { Value = factores.IdMedidaCobro, ParameterName = "@IdMedidaCobro" };
                parameters[4] = new SqlParameter() { Value = factores.Nombre, ParameterName = "@Factor" };
                parameters[5] = new SqlParameter() { Value = factores.Descripcion, ParameterName = "@Descripcion" };
                parameters[6] = new SqlParameter() { Value = factores.CuotaFactor, ParameterName = "@CuotaFactor" };
                parameters[7] = new SqlParameter() { Value = factores.FechaAutorizacion, ParameterName = "@FechaAutorizacion" };
                parameters[8] = new SqlParameter() { Value = factores.FechaEntradaVigor, ParameterName = "@FechaEntradaVigor" };
                parameters[9] = new SqlParameter() { Value = factores.FechaTermino, ParameterName = "@FechaTermino" };
                parameters[10] = new SqlParameter() { Value = factores.FechaPublicacionDof, ParameterName = "@FechaPublicacionDof" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);
                cmd.Parameters.Add(parameters[3]);
                cmd.Parameters.Add(parameters[4]);
                cmd.Parameters.Add(parameters[5]);
                cmd.Parameters.Add(parameters[6]);
                cmd.Parameters.Add(parameters[7]);
                cmd.Parameters.Add(parameters[8]);
                cmd.Parameters.Add(parameters[9]);
                cmd.Parameters.Add(parameters[10]);


                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public IEnumerable<Factor> ObtenerPorCriterio(Paging paging, Factor entity)
        {
            var result = new List<Factor>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.FactoresObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Factor factores = new Factor();

                        factores.Identificador = Convert.ToInt32(reader["IdFactor"]);
                        factores.IdTipoServicio = Convert.ToInt32(reader["IdTipoServicio"]);
                        factores.TipoServicio = reader["TipoServicio"].ToString();
                        factores.IdClasificacionFactor = Convert.ToInt32(reader["IdClasificacionFactor"]);
                        factores.ClasificadorFactor = reader["ClasificadorFactor"].ToString();
                        factores.IdMedidaCobro = Convert.ToInt32(reader["IdMedidaCobro"]);
                        factores.MedidaCobro = reader["MedidaCobro"].ToString();
                        factores.Nombre = Convert.ToString(reader["Factor"]);
                        factores.Descripcion = Convert.ToString(reader["Descripcion"]);
                        factores.CuotaFactor = Convert.ToDecimal(reader["CuotaFactor"]);
                        factores.FechaAutorizacion = Convert.ToDateTime(reader["FechaAutorizacion"]);
                        factores.FechaEntradaVigor = Convert.ToDateTime(reader["FechaEntradaVigor"]);
                        factores.FechaTermino = Convert.ToDateTime(reader["FechaTermino"]);
                        factores.FechaPublicacionDof = Convert.ToDateTime(reader["FechaPublicacionDof"]);
                        factores.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            factores.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        factores.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(factores);
                    }
                }
                return result;  // yield?
            }
        }

        public IEnumerable<Factor> ObtenerPorClasificacion(Factor entity)
        {
            var result = new List<Factor>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Factores.FactoresObtenerPorClasificacion;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdClasificacionFactor, ParameterName = "@IdClasificacion" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Factor factores = new Factor();

                        factores.Identificador = Convert.ToInt32(reader["IdFactor"]);
                        factores.IdTipoServicio = Convert.ToInt32(reader["IdTipoServicio"]);
                        factores.IdClasificacionFactor = Convert.ToInt32(reader["IdClasificacionFactor"]);
                        factores.IdMedidaCobro = Convert.ToInt32(reader["IdMedidaCobro"]);
                        factores.Nombre = Convert.ToString(reader["Factor"]);
                        factores.Descripcion = Convert.ToString(reader["Descripcion"]);
                        factores.CuotaFactor = Convert.ToDecimal(reader["CuotaFactor"]);
                        factores.FechaAutorizacion = Convert.ToDateTime(reader["FechaAutorizacion"]);
                        factores.FechaEntradaVigor = Convert.ToDateTime(reader["FechaEntradaVigor"]);
                        factores.FechaTermino = Convert.ToDateTime(reader["FechaTermino"]);
                        if(!(reader["FechaPublicacionDof"] == DBNull.Value))
                        factores.FechaPublicacionDof = Convert.ToDateTime(reader["FechaPublicacionDof"]);                        
                        factores.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(factores);
                    }
                }
                return result;  // yield?
            }
        }
    }
}
