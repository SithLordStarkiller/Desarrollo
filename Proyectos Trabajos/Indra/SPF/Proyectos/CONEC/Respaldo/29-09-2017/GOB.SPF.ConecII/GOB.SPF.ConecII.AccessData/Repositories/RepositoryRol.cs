namespace GOB.SPF.ConecII.AccessData.Repositories
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;

    public class RepositoryRol : IRepository<Rol>
    {
        private int pages { get; set; }
        public int Pages { get { return pages; } }

        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryRol(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }
        public IEnumerable<Rol> Obtener(Paging paging)
        {
            var result = new List<Rol>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesObtener;
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
                        Rol Rol = new Rol();

                        Rol.Identificador = Convert.ToInt32(reader["IdRol"]);
                        Rol.IdentificadorSubRol = Convert.ToInt32(reader["IdSubRol"]);
                        Rol.Nombre = reader["Nombre"].ToString();
                        Rol.Descripcion = reader["Descripcion"].ToString();
                        Rol.Activo = Convert.ToBoolean(reader["Activo"]);

                        if (reader["Paginas"] != null)
                            pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(Rol);
                    }
                }
                return result;  // yield?
            }

        }

        public Rol ObtenerPorId(long id)
        {
            int result = 0;
            Rol Rol = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesObtenerPorId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                Rol = new Rol();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Rol.Identificador = Convert.ToInt32(reader["IdRol"]);
                        Rol.IdentificadorSubRol = Convert.ToInt32(reader["IdSubRol"]);
                        Rol.Nombre = reader["Nombre"].ToString();
                        Rol.Descripcion = reader["Descripcion"].ToString();
                        Rol.Activo = Convert.ToBoolean(reader["Activo"]);

                    }
                }
            }

            return Rol;
        }

        public int CambiarEstatus(Rol Rol)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = Rol.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = Rol.Activo, ParameterName = "@Activo" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                result = cmd.ExecuteNonQuery();

                //Comita nossa transação.
                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Insertar(Rol Rol)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = Rol.IdentificadorSubRol, ParameterName = "@IdSubRol" });
                cmd.Parameters.Add(new SqlParameter() { Value = Rol.Nombre, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter() { Value = Rol.Descripcion, ParameterName = "@Descripcion" });

                result = cmd.ExecuteNonQuery();
                return result;
            }
        }

        public int Actualizar(Rol Rol)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[4];

                parameters[0] = new SqlParameter() { Value = Rol.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = Rol.IdentificadorSubRol, ParameterName = "@IdSubRol" };
                parameters[2] = new SqlParameter() { Value = Rol.Nombre, ParameterName = "@Nombre" };
                parameters[3] = new SqlParameter() { Value = Rol.Descripcion, ParameterName = "@Descripcion" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);
                cmd.Parameters.Add(parameters[3]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public IEnumerable<Rol> ObtenerPorCriterio(Paging paging, Rol entity)
        {
            var result = new List<Rol>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Rol Rol = new Rol();

                        Rol.Identificador = Convert.ToInt32(reader["IdRol"]);
                        Rol.IdentificadorSubRol = Convert.ToInt32(reader["IdSubRol"]);
                        Rol.Nombre = reader["Nombre"].ToString();
                        Rol.Descripcion = reader["Descripcion"].ToString();
                        Rol.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            Rol.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        Rol.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(Rol);
                    }
                }
                return result;  // yield?
            }
        }

    }
}
