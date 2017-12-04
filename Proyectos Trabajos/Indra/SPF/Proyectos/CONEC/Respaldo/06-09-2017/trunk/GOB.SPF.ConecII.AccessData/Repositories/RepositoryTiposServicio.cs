namespace GOB.SPF.ConecII.AccessData
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;

    public class RepositoryTiposServicio : IRepository<TipoServicio>
    {
        private int pages { get; set; }
        public int Pages { get { return pages; } }

        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryTiposServicio(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public IEnumerable<TipoServicio> Obtener(Paging paging)
        {
            var result = new List<TipoServicio>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.TiposServiciosObtener;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" };
                parameters[1] = new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TipoServicio tServicio = new TipoServicio();

                        tServicio.Identificador = Convert.ToInt32(reader["IdTipoServicio"]);
                        tServicio.Nombre = reader["Nombre"].ToString();
                        tServicio.Descripcion = reader["Descripcion"].ToString();
                        tServicio.Clave = reader["Clave"].ToString();
                        tServicio.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            tServicio.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        tServicio.Activo = Convert.ToBoolean(reader["Activo"]);
                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(tServicio);
                    }
                }
                return result;  // yield?
            }

        }

        public IEnumerable<TipoServicio> ObtenerPorCriterio(Paging paging, TipoServicio entity)
        {
            var result = new List<TipoServicio>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.TipoServicioObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@IdTipoServicio" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TipoServicio tiposServicio = new TipoServicio();

                        tiposServicio.Identificador = Convert.ToInt32(reader["IdTipoServicio"]);
                        tiposServicio.Nombre = reader["Nombre"].ToString();
                        tiposServicio.Descripcion = reader["Descripcion"].ToString();
                        tiposServicio.Clave = reader["Clave"].ToString();
                        tiposServicio.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            tiposServicio.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        tiposServicio.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(tiposServicio);
                    }
                }
                return result;  // yield?
            }
        }
        public TipoServicio ObtenerPorId(long id)
        {
            int result = 0;
            TipoServicio tServicio = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.TipoServicioObtenerId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                tServicio = new TipoServicio();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tServicio.Identificador = Convert.ToInt32(reader["IdTipoServicio"]);
                        tServicio.Nombre = reader["Nombre"].ToString();
                        tServicio.Descripcion = reader["Descripcion"].ToString();
                        tServicio.Clave = reader["Clave"].ToString();
                        tServicio.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            tServicio.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        tServicio.Activo = Convert.ToBoolean(reader["Activo"]);
                    }
                }
            }

            return tServicio;
        }

        public int CambiarEstatus(TipoServicio tServicio)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.TipoServicioCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = tServicio.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = tServicio.Activo, ParameterName = "@Activo" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                result = cmd.ExecuteNonQuery();

                //Comita nossa transação.
                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Insertar(TipoServicio tServicio)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.TipoServicioInsertar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[3];

                parameters[0] = new SqlParameter() { Value = tServicio.Nombre, ParameterName = "@Nombre" };
                parameters[1] = new SqlParameter() { Value = tServicio.Descripcion, ParameterName = "@Descripcion" };
                parameters[2] = new SqlParameter() { Value = tServicio.Clave, ParameterName = "@Clave" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Actualizar(TipoServicio tServicio)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.TipoServicioActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[4];

                parameters[0] = new SqlParameter() { Value = tServicio.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = tServicio.Nombre, ParameterName = "@Nombre" };
                parameters[2] = new SqlParameter() { Value = tServicio.Descripcion, ParameterName = "@Descripcion" };
                parameters[3] = new SqlParameter() { Value = tServicio.Clave, ParameterName = "@Clave" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);
                cmd.Parameters.Add(parameters[3]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public string ValidarRegistro(TipoServicio tServicio)
        {
            string result = "";

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.FraccionesValidarRegistro;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = tServicio.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = tServicio.Nombre, ParameterName = "@Nombre" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = reader["Resultado"].ToString();
                    }
                    reader.Close();
                }
                //_unitOfWork.SaveChanges();
            }

            return result;
        }
    }
}