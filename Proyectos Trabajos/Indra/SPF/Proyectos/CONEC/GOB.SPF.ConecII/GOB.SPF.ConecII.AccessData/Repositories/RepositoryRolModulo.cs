using System.Linq;
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
    public class RepositoryRolModulo : IRepository<IRolModulo>
    {
        #region variables privadas
        private int pages { get; set; }
        private UnitOfWorkCatalog _unitOfWork;
        #endregion

        #region variables publicas
        public int Pages { get { return pages; } }
        #endregion

        #region constructor
        public RepositoryRolModulo(IUnitOfWork uow)
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

        #region metodo publicos
        public IEnumerable<IRolModulo> Obtener(IPaging paging)
        {
            var result = new List<RolModulo>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesModulosObtener;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = paging.All, ParameterName = "@Todos" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RolModulo rolModulo = new RolModulo()
                        {
                            Id = reader["IdRolModulo"].To<int>(),
                            Modulo = new Entities.Modulos.Modulo() { Id = reader["IdModulo"].To<int>(), Nombre = reader["Modulo"].To<string>() },
                            Rol = new Rol() { Id = reader["IdRol"].To<int>(), Name = reader["Rol"].To<string>()},
                            FechaInicial = reader["IdRolModulo"].To<DateTime>(),
                            FechaFinal = reader["IdRolModulo"].To<DateTime?>(),
                            Activo = reader["IdRolModulo"].To<bool>()
                        };

                        if (reader["Paginas"] != DBNull.Value)
                            pages = reader["Paginas"].To<int>();

                        result.Add(rolModulo);
                    }
                }
                return result;
            }

        }

        public IRolModulo ObtenerPorId(long id)
        {
            int result = 0;
            RolModulo RolModulo = new RolModulo();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesModulosObtenerPorId;
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
                        RolModulo = new RolModulo()
                        {
                            Id = reader["IdRolModulo"].To<int>(),
                            Rol = new Rol()
                            {
                                Id = reader["IdRol"].To<int>(),
                                Name = reader["Rol"].To<string>()
                            },
                            Modulo = new Entities.Modulos.Modulo()
                            {
                                Id = reader["IdModulo"].To<int>(),
                                Nombre = reader["Modulo"].To<string>()
                            },
                            FechaInicial = reader["IdRolModulo"].To<DateTime>(),
                            FechaFinal = reader["IdRolModulo"].To<DateTime?>(),
                            Activo = reader["IdRolModulo"].To<bool>()
                        };
                    }
                }
            }
            return RolModulo;
        }

        public int CambiarEstatus(IRolModulo RolModulo)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesModulosCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = RolModulo.Id, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = RolModulo.Activo, ParameterName = "@Activo" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                result = cmd.ExecuteNonQuery();

                return result;
            }
        }

        public int Insertar(IRolModulo RolModulo)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesModulosInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add(new SqlParameter() { Value = RolModulo.IdRol, ParameterName = "@IdRol" });
                //cmd.Parameters.Add(new SqlParameter() { Value = RolModulo.IdModulo, ParameterName = "@IdModulo" });

                result = cmd.ExecuteNonQuery();
                return result;
            }
        }

        public int Actualizar(IRolModulo RolModulo)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesModulosActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[3];

                parameters[0] = new SqlParameter() { Value = RolModulo.Id, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = RolModulo.Rol.Id, ParameterName = "@IdRol" };
                parameters[2] = new SqlParameter() { Value = RolModulo.Modulo.Id, ParameterName = "@IdModulo" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);

                result = cmd.ExecuteNonQuery();

                return result;
            }
        }

        public IEnumerable<IRolModulo> ObtenerPorCriterio(IPaging paging, IRolModulo entity)
        {
            var result = new List<IRolModulo>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.RolesModulosObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                if (entity.Rol != null)
                {
                    cmd.Parameters.Add(new SqlParameter()
                    {
                        Value = entity.Rol.Id,
                        ParameterName = "@IdRol"
                    });
                    cmd.Parameters.Add(new SqlParameter()
                    {
                        Value = entity.Rol.IdArea,
                        ParameterName = "@IdArea"
                    });
                }
                if (entity.Modulo != null)
                {
                    cmd.Parameters.Add(new SqlParameter()
                    {
                        Value = entity.Modulo.Id,
                        ParameterName = "@IdModulo"
                    });
                    cmd.Parameters.Add(new SqlParameter()
                    {
                        Value = entity.Modulo.SubModulos != null && entity.Modulo.SubModulos.Any()? entity.Modulo.SubModulos.First().Id : 0,
                        ParameterName = "@IdSubModulo"
                    });
                }
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.All, ParameterName = "@Todos" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RolModulo RolModulo = new RolModulo()
                        {
                            Rol = new Rol()
                            {
                                Id = reader["IdRol"].To<int>(),
                                Name = reader["Rol"].To<string>()
                            },
                            Modulo = new Entities.Modulos.Modulo()
                            {
                                Id = reader["IdModulo"].To<int>(),
                                Nombre = reader["Modulo"].To<string>()
                            },
                            FechaInicial = reader["IdRolModulo"].To<DateTime>(),
                            FechaFinal = reader["IdRolModulo"].To<DateTime?>(),
                            Activo = reader["IdRolModulo"].To<bool>()
                        };
                        result.Add(RolModulo);
                    }
                }
                return result;  // yield?
            }
        }
        #endregion
    }
}
