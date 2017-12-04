namespace GOB.SPF.ConecII.AccessData.Repositories
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;
    public class RepositoryModulo : IRepository<Modulo>
    {
        private int pages { get; set; }
        public int Pages { get { return pages; } }

        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryModulo(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }
        public IEnumerable<Modulo> Obtener(Paging paging)
        {
            var result = new List<Modulo>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.ModulosObtener;
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
                        Modulo Modulo = new Modulo();

                        Modulo.Identificador = Convert.ToInt32(reader["IdModulo"]);
                        Modulo.IdentificadorSubModulo = Convert.ToInt32(reader["IdPadre"]);
                        Modulo.Nombre = reader["Nombre"].ToString();
                        Modulo.Descripcion = reader["Descripcion"].ToString();
                        Modulo.Activo = Convert.ToBoolean(reader["Activo"]);

                        if (reader["Paginas"] != null)
                            pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(Modulo);
                    }
                }
                return result;  // yield?
            }

        }

        public Modulo ObtenerPorId(long id)
        {
            int result = 0;
            Modulo Modulo = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.ModulosObtenerPorId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                Modulo = new Modulo();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Modulo.Identificador = Convert.ToInt32(reader["IdModulo"]);
                        Modulo.IdentificadorSubModulo = Convert.ToInt32(reader["IdPadre"]);
                        Modulo.Nombre = reader["Nombre"].ToString();
                        Modulo.Descripcion = reader["Descripcion"].ToString();
                        Modulo.Activo = Convert.ToBoolean(reader["Activo"]);

                    }
                }
            }

            return Modulo;
        }

        public int CambiarEstatus(Modulo Modulo)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.ModulosCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = Modulo.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = Modulo.Activo, ParameterName = "@Activo" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                result = cmd.ExecuteNonQuery();

                //Comita nossa transação.
                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Insertar(Modulo Modulo)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.ModulosInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = Modulo.IdentificadorSubModulo, ParameterName = "@IdPadre" });
                cmd.Parameters.Add(new SqlParameter() { Value = Modulo.Nombre, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter() { Value = Modulo.Descripcion, ParameterName = "@Descripcion" });

                result = cmd.ExecuteNonQuery();
                return result;
            }
        }

        public int Actualizar(Modulo Modulo)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.ModulosActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[4];

                parameters[0] = new SqlParameter() { Value = Modulo.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = Modulo.IdentificadorSubModulo, ParameterName = "@IdPadre" };
                parameters[2] = new SqlParameter() { Value = Modulo.Nombre, ParameterName = "@Nombre" };
                parameters[3] = new SqlParameter() { Value = Modulo.Descripcion, ParameterName = "@Descripcion" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);
                cmd.Parameters.Add(parameters[3]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public IEnumerable<Modulo> ObtenerPorCriterio(Paging paging, Modulo entity)
        {
            var result = new List<Modulo>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.ModulosObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Modulo Modulo = new Modulo();

                        Modulo.Identificador = Convert.ToInt32(reader["IdModulo"]);
                        Modulo.IdentificadorSubModulo = Convert.ToInt32(reader["IdPadre"]);
                        Modulo.Nombre = reader["Nombre"].ToString();
                        Modulo.Descripcion = reader["Descripcion"].ToString();
                        Modulo.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            Modulo.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        Modulo.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(Modulo);
                    }
                }
                return result;  // yield?
            }
        }

    }
}
