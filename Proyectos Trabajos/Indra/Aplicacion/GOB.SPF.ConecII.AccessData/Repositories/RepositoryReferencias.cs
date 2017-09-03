namespace GOB.SPF.ConecII.AccessData
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;

    public class RepositoryReferencias : IRepository<Referencia>
    {
        private UnitOfWorkCatalog _unitOfWork;
        private int pages { get; set; }
        public int Pages { get { return pages; } }

        public RepositoryReferencias(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public IEnumerable<Referencia> Obtener(Paging paging)
        {
            var result = new List<Referencia>();

            using (var cmd = _unitOfWork.CreateCommand())
            {

                cmd.CommandText = Catalogos.ReferenciasObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Referencia referencias = new Referencia();

                        referencias.Identificador = Convert.ToInt32(reader["IdReferencia"]);
                        referencias.ClaveReferencia = Convert.ToInt32(reader["ClaveReferencia"]);
                        referencias.Descripcion = Convert.ToString(reader["Descripcion"]);
                        //referencias.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        //referencias.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        referencias.Activo = Convert.ToBoolean(reader["Activo"]);
                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(referencias);
                    }
                }
                return result;  // yield?
            }

        }

        public Referencia ObtenerPorId(long id)
        {
            int result = 0;
            Referencia referencias = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.ReferenciasObtenerId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                referencias = new Referencia();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        referencias.Identificador = Convert.ToInt32(reader["IdReferencia"]);
                        referencias.ClaveReferencia = Convert.ToInt32(reader["ClaveReferencia"]);
                        referencias.Descripcion = Convert.ToString(reader["Descripcion"]);
                        referencias.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        referencias.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        referencias.Activo = Convert.ToBoolean(reader["Activo"]);


                    }
                }
            }

            return referencias;
        }

        public int CambiarEstatus(Referencia entity)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = _unitOfWork.CreateCommand())
            {

                cmd.CommandText = Catalogos.ReferenciasCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@Identificador" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });

                result = cmd.ExecuteNonQuery();

                //Comita nossa transação.
                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Insertar(Referencia entity)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.ReferenciasInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.ClaveReferencia, ParameterName = "@ClaveReferencia" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Descripcion, ParameterName = "@Descripcion" });

                result = cmd.ExecuteNonQuery();
                return result;
            }
        }

        public int Actualizar(Referencia entity)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.ReferenciasActualizar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@Identificador" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.ClaveReferencia, ParameterName = "@ClaveReferencia" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Descripcion, ParameterName = "@Descripcion" });
                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public IEnumerable<Referencia> ObtenerPorCriterio(Paging paging, Referencia entity)
        {
            var result = new List<Referencia>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.ReferenciasObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });


                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Referencia referencias = new Referencia();

                        referencias.Identificador = Convert.ToInt32(reader["IdReferencia"]);
                        referencias.ClaveReferencia = Convert.ToInt32(reader["ClaveReferencia"]);
                        referencias.Descripcion = reader["Descripcion"].ToString();
                        referencias.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            referencias.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        referencias.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(referencias);
                    }
                }
                return result;  // yield?
            }
        }

        public string ValidarRegistro(Referencia entity)
        {
            string result = "";

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.ReferenciasValidarRegistro;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@Identificador" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.ClaveReferencia, ParameterName = "@ClaveReferencia" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Descripcion, ParameterName = "@Descripcion" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = reader["Resultado"].ToString();
                    }
                }
                //_unitOfWork.SaveChanges();
            }
            return result;

        }
    }
}
