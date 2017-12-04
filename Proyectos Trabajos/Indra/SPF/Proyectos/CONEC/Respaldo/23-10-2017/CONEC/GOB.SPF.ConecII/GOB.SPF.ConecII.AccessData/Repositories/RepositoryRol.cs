using GOB.SPF.ConecII.Interfaces;
using GOB.SPF.ConecII.Interfaces.Genericos;
using GOB.SPF.ConecII.Library;

namespace GOB.SPF.ConecII.AccessData.Repositories
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;
    using Library;

    public class RepositoryRol : IRepository<IRol>
    {
        #region variables privadas
        private int pages { get; set; }
        private UnitOfWorkCatalog _unitOfWork;
        #endregion

        #region variables publicas
        public int Pages { get { return pages; } }
        #endregion

        #region constructor
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
        #endregion

        #region metodos publicos
        public IEnumerable<IRol> Obtener(IPaging paging)
        {
            var result = new List<Rol>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesObtener;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = paging.All, ParameterName = "@Todos" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Rol rol = new Rol(reader["IdRol"].To<int>());
                        rol.IdParentRol = reader["IdParentRol"].To<int?>(null);
                        rol.Name = reader["Nombre"].To<string>();
                        rol.Descripcion = reader["Descripcion"].To<string>();
                        rol.Activo = reader["Activo"].To<bool>();
                        rol.FechaInicial = reader["FechaInicial"].To<DateTime>();
                        rol.IdArea = reader["IdArea"].To<int?>(null);
                        rol.FechaFinal = reader["FechaFinal"].To<DateTime?>(null);

                        pages = reader["Paginas"].To<int>();

                        result.Add(rol);
                    }
                }
                return result;
            }
        }

        public IRol ObtenerPorId(long id)
        {
            int result = 0;
            Rol rol = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesObtenerPorId;
                cmd.CommandType = CommandType.StoredProcedure;
                
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rol = new Rol(reader["IdRol"].To<int>())
                        {
                            IdParentRol = reader["IdParentRol"].To<int?>(null),
                            Name = reader["Nombre"].To<string>(),
                            Descripcion = reader["Descripcion"].To<string>(),
                            IdArea = reader["IdArea"].To<int?>(null),
                            FechaInicial = reader["FechaInicial"].To<DateTime>(),
                            FechaFinal = reader["FechaFinal"].To<DateTime?>(null),
                            Activo = reader["Activo"].To<bool>()
                        };
                    }
                }
            }

            return rol;
        }

        public int CambiarEstatus(IRol Rol)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = Rol.Id, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = Rol.Activo, ParameterName = "@Activo" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                result = cmd.ExecuteNonQuery();
                return result;
            }
        }

        public int Insertar(IRol Rol)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = Rol.IdParentRol, ParameterName = "@IdParentRol" });
                cmd.Parameters.Add(new SqlParameter() { Value = Rol.Name, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter() { Value = Rol.Descripcion, ParameterName = "@Descripcion" });
                cmd.Parameters.Add(new SqlParameter() { Value = Rol.IdArea, ParameterName = "@IdArea" });

                result = cmd.ExecuteNonQuery();
                return result;
            }
        }

        public int Actualizar(IRol Rol)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = Rol.Id, ParameterName = "@Identificador" });
                cmd.Parameters.Add(new SqlParameter() { Value = Rol.Name, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter() { Value = Rol.Descripcion, ParameterName = "@Descripcion" });

                result = cmd.ExecuteNonQuery();

                return result;
            }
        }

        public IEnumerable<IRol> ObtenerPorCriterio(IPaging paging, IRol entity)
        {
            var result = new List<Rol>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.Name, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo ? 1: 0, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.All ? 1: 0, ParameterName = "@Todos" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Rol rol = new Rol(Convert.ToInt32(reader["IdRol"]));
                        if(reader["IdParentRol"] != DBNull.Value)
                            rol.IdParentRol = Convert.ToInt32(reader["IdParentRol"]);
                        rol.Name = reader["Nombre"].ToString();
                        rol.Descripcion = reader["Descripcion"].ToString();
                        rol.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (reader["FechaFinal"] != DBNull.Value)
                            rol.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        rol.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(rol);
                    }
                }
                return result;  // yield?
            }
        }

        public IEnumerable<IRol> ObtenerRolesPorTipoUsuario(IRol entity, bool usuarioExterno)
        {
            var result = new List<Rol>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesObtenerPorTipoUsuario;
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter() { Value = usuarioExterno, ParameterName = "@UsuarioExterno" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdArea, ParameterName = "@IdArea" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Name, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo ? 1 : 0, ParameterName = "@Activo" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Rol rol = new Rol(reader["IdRol"].To<int>())
                        {
                            IdParentRol = reader["IdParentRol"].To<int?>(null),
                            Name = reader["Nombre"].To<string>(),
                            Descripcion = reader["Descripcion"].To<string>(),
                            IdArea = reader["IdArea"].To<int?>(null),
                            FechaInicial = reader["FechaInicial"].To<DateTime>(),
                            FechaFinal = reader["FechaFinal"].To<DateTime?>(null),
                            Activo = reader["Activo"].To<bool>()
                        };
                        result.Add(rol);
                    }
                }
                return result;
            }
        }

        public int UsuariosActivosPorRol(int idRol)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.UsuariosActivosPorRol;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = idRol, ParameterName = "@IdRol" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = reader["UsuariosActivos"].To<int>();
                    }
                }
                return result;
            }
        }
        #endregion
    }
}
