namespace GOB.SPF.ConecII.AccessData
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;

    public class RepositoryClasificacionFactor : IRepository<ClasificacionFactor>
    {
        private int pages { get; set; }
        public int Pages { get { return pages; } }

        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryClasificacionFactor(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public IEnumerable<ClasificacionFactor> Obtener(Paging paging)
        {
            List<ClasificacionFactor> result = new List<ClasificacionFactor>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.ClasificacionFactorObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.All, ParameterName = "@Todos" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ClasificacionFactor clasificacionFactor = new ClasificacionFactor();

                        clasificacionFactor.Identificador = Convert.ToInt32(reader["IdClasificacionFactor"]);
                        clasificacionFactor.Nombre = Convert.ToString(reader["Nombre"]);
                        clasificacionFactor.Descripcion = Convert.ToString(reader["Descripcion"]);
                        clasificacionFactor.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            clasificacionFactor.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        clasificacionFactor.Activo = Convert.ToBoolean(reader["Activo"]);
                        pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(clasificacionFactor);
                    }
                }
                return result;  // yield?
            }

        }

        public ClasificacionFactor ObtenerPorId(long id)
        {
            int result = 0;
            ClasificacionFactor clasificacionFactor = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.ClasificacionFactorObtenerId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                clasificacionFactor = new ClasificacionFactor();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clasificacionFactor.Identificador = Convert.ToInt32(reader["IdClasificacionFactor"]);
                        clasificacionFactor.Nombre = Convert.ToString(reader["Nombre"]);
                        clasificacionFactor.Descripcion = Convert.ToString(reader["Descripcion"]);
                        clasificacionFactor.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            clasificacionFactor.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        clasificacionFactor.Activo = Convert.ToBoolean(reader["Activo"]);


                    }
                }
            }

            return clasificacionFactor;
        }

        public int CambiarEstatus(ClasificacionFactor clasificacionFactor)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.ClasificacionFactorCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = clasificacionFactor.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = clasificacionFactor.Activo, ParameterName = "@Activo" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                result = cmd.ExecuteNonQuery();

                //Comita nossa transação.
                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Insertar(ClasificacionFactor clasificacionFactor)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.ClasificacionFactorInsertar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = clasificacionFactor.Nombre, ParameterName = "@Nombre" };
                parameters[1] = new SqlParameter() { Value = clasificacionFactor.Descripcion, ParameterName = "@Descripcion" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Actualizar(ClasificacionFactor clasificacionFactor)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.ClasificacionFactorActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[3];

                parameters[0] = new SqlParameter() { Value = clasificacionFactor.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = clasificacionFactor.Nombre, ParameterName = "@Nombre" };
                parameters[2] = new SqlParameter() { Value = clasificacionFactor.Descripcion, ParameterName = "@Descripcion" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public IEnumerable<ClasificacionFactor> ObtenerPorCriterio(Paging paging, ClasificacionFactor entity)
        {
            var result = new List<ClasificacionFactor>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.ClasificacionFactorObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ClasificacionFactor clasificacionFactor = new ClasificacionFactor();

                        clasificacionFactor.Identificador = Convert.ToInt32(reader["IdClasificacionFactor"]);
                        clasificacionFactor.Nombre = reader["Nombre"].ToString();
                        clasificacionFactor.Descripcion = reader["Descripcion"].ToString();
                        clasificacionFactor.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            clasificacionFactor.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        clasificacionFactor.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(clasificacionFactor);
                    }
                }
                return result;  // yield?
            }
        }
    }
}
