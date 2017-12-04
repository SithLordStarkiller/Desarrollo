namespace GOB.SPF.ConecII.AccessData
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;

    public class RepositoryTiposPago : IRepository<TiposPago>
    {
        private UnitOfWorkCatalog _unitOfWork;
        private int pages { get; set; }
        public int Pages { get { return pages; } }

        public RepositoryTiposPago(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public IEnumerable<TiposPago> Obtener(Paging paging)
        {
            var result = new List<TiposPago>();

            using (var cmd = _unitOfWork.CreateCommand())
            {

                cmd.CommandText = Catalogos.TiposPagoObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TiposPago tiposPagos = new TiposPago();

                        tiposPagos.Identificador = Convert.ToInt32(reader["IdTipoPago"]);
                        tiposPagos.Nombre = Convert.ToString(reader["Nombre"]);
                        tiposPagos.Descripcion = Convert.ToString(reader["Descripcion"]);
                        tiposPagos.Actividad = Convert.ToBoolean(reader["Actividad"]);
                        tiposPagos.Activo = Convert.ToBoolean(reader["Activo"]);
                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(tiposPagos);
                    }
                }
                return result;  // yield?
            }

        }

        public TiposPago ObtenerPorId(long id)
        {
            int result = 0;
            TiposPago tiposPagos = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.TiposPagoObtenerId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                tiposPagos = new TiposPago();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tiposPagos.Identificador = Convert.ToInt32(reader["IdTiposPago"]);
                        tiposPagos.Nombre = Convert.ToString(reader["Nombre"]);
                        tiposPagos.Descripcion = Convert.ToString(reader["Descripcion"]);
                        tiposPagos.Actividad = Convert.ToBoolean(reader["Actividad"]);
                        tiposPagos.Activo = Convert.ToBoolean(reader["Activo"]);


                    }
                }
            }

            return tiposPagos;
        }

        public int CambiarEstatus(TiposPago entity)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = _unitOfWork.CreateCommand())
            {

                cmd.CommandText = Catalogos.TiposPagoCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@Identificador" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });

                result = cmd.ExecuteNonQuery();

                //Comita nossa transação.
                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Insertar(TiposPago entity)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.TiposPagoInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Nombre, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Descripcion, ParameterName = "@Descripcion" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Actividad, ParameterName = "@Actividad" });


                result = cmd.ExecuteNonQuery();
                return result;
            }
        }

        public int Actualizar(TiposPago entity)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.TiposPagoActualizar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@Identificador" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Nombre, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Descripcion, ParameterName = "@Descripcion" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Actividad, ParameterName = "@Actividad" });

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public IEnumerable<TiposPago> ObtenerPorCriterio(Paging paging, TiposPago entity)
        {
            var result = new List<TiposPago>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.TiposPagoObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdRegimenFiscal, ParameterName = "@IdRegimenFiscal" });
                //cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                //cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });


                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TiposPago tiposPagos = new TiposPago();

                        tiposPagos.Identificador = Convert.ToInt32(reader["IdTiposPago"]);
                        tiposPagos.Nombre = Convert.ToString(reader["Nombre"]);
                        tiposPagos.Descripcion = reader["Descripcion"].ToString();
                        tiposPagos.Actividad = Convert.ToBoolean(reader["Actividad"]);
                        tiposPagos.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            tiposPagos.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        tiposPagos.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(tiposPagos);
                    }
                    reader.Close();
                }
                return result;  // yield?
            }
        }
    }
}
