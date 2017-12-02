namespace GOB.SPF.ConecII.AccessData.Repositories
{
    using Entities.Modulos;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;
    using System.Linq;
    using Interfaces;
    using Entities;
    using Interfaces.Genericos;
    using Library;

    public class RepositoryModulo : IRepository<IModulo>
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
        public IEnumerable<IModulo> Obtener(IPaging paging)
        {
            List<Entities.Modulos.Modulo> result = new List<Entities.Modulos.Modulo>();

            
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.ModulosObtener;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = paging.All ? 1 : 0, ParameterName = "@Todos" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                DataTable dtModulos = new DataTable();
                DataTable dtSubModulos = new DataTable();

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    dtModulos.Load(reader);
                    dtSubModulos.Load(reader);

                    if (dtModulos != null & dtModulos.Rows.Count > 0)
                    {
                        result = (from modulos in dtModulos.AsEnumerable()
                                  join subModulos in dtSubModulos.AsEnumerable() on modulos.Field<int>("IdModulo") equals subModulos.Field<int>("IdPadre")
                                  group new { modulos, subModulos } by subModulos.Field<int>("IdPadre") into g
                                  let mod = g.FirstOrDefault().modulos
                                  let subs3 = g.Where(s3 => s3.subModulos.Field<int?>("IdNivel3") != null).Select(s3 => new Entities.Modulos.Modulo()
                                  {
                                      IdPadre = s3.subModulos.Field<int>("IdNivel2"),
                                      Id = s3.subModulos.Field<int>("IdNivel3"),
                                      Nombre = s3.subModulos.Field<string>("NombreNivel3")
                                  }).ToList()
                                  let subs2 = g.Where(s2 => s2.subModulos.Field<int?>("IdNivel2") != null).Select(s2 => new Entities.Modulos.Modulo()
                                  {
                                      IdPadre = s2.subModulos.Field<int>("IdNivel1"),
                                      Id = s2.subModulos.Field<int>("IdNivel2"),
                                      Nombre = s2.subModulos.Field<string>("NombreNivel2")
                                  }).ToList()
                                  let subs1 = (from sub1 in g.Select(sub1 => sub1.subModulos)
                                               group sub1 by sub1.Field<int>("IdNivel1") into subg1
                                               let subMod1 = subg1.FirstOrDefault()
                                               select new Entities.Modulos.Modulo()
                                               {
                                                   Id = subMod1.Field<int>("IdNivel1"),
                                                   IdPadre = subMod1.Field<int>("IdPadre"),
                                                   Nombre = subMod1.Field<string>("NombreNivel1")
                                               }).ToList()
                                  select new Entities.Modulos.Modulo()
                                  {
                                      Id = mod.Field<int>("IdModulo"),
                                      IdPadre = mod.Field<int?>("IdPadre"),
                                      Nombre = mod.Field<string>("Nombre"),
                                      Controlador = mod.Field<string>("Controlador"),
                                      Accion = mod.Field<string>("Accion"),
                                      Descripcion = mod.Field<string>("Descripcion"),
                                      FechaInicial = mod.Field<DateTime>("FechaInicial"),
                                      FechaFinal = mod.Field<DateTime?>("FechaFinal"),
                                      Activo = mod.Field<bool>("Activo"),
                                      SubModulos = subs1.Where(s1 => s1.IdPadre == mod.Field<int>("IdModulo")).Select(sm1 =>
                                          new Entities.Modulos.Modulo()
                                          {
                                              Id = sm1.Id,
                                              IdPadre = sm1.IdPadre,
                                              Nombre = sm1.Nombre,
                                              SubModulos = subs2.Where(s2 => s2.IdPadre == sm1.Id).Select(sm2 =>
                                              new Entities.Modulos.Modulo()
                                              {
                                                  Id = sm2.Id,
                                                  IdPadre = sm2.IdPadre,
                                                  Nombre = sm2.Nombre,
                                                  SubModulos = subs3.Where(s3 => s3.IdPadre == sm2.Id).ToList()
                                              }).ToList()
                                          }).ToList()
                                  }).ToList();
                    }
                }
                return result;
            }

        }

        public IModulo ObtenerPorId(long id)
        {
            Entities.Modulos.Modulo Modulo = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.ModulosObtenerPorId;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                DataTable dtModulos = new DataTable();
                DataTable dtSubModulos = new DataTable();

                Modulo = new Entities.Modulos.Modulo();
                using (IDataReader reader = cmd.ExecuteReader())
                {
                    dtModulos.Load(reader);
                    dtSubModulos.Load(reader);

                    if (dtModulos != null & dtModulos.Rows.Count > 0)
                    {
                        Modulo.Id = dtModulos.Rows[0]["IdModulo"].To<int>();
                        Modulo.IdPadre = dtModulos.Rows[0]["IdPadre"].To<int>();
                        Modulo.Nombre = dtModulos.Rows[0]["Nombre"].To<string>();
                        Modulo.Controlador = dtModulos.Rows[0]["Controlador"].To<string>();
                        Modulo.Accion = dtModulos.Rows[0]["Accion"].To<string>();
                        Modulo.Descripcion = dtModulos.Rows[0]["Descripcion"].To<string>();
                        Modulo.FechaInicial = dtModulos.Rows[0]["FechaInicial"].To<DateTime>();
                        Modulo.FechaFinal = dtModulos.Rows[0]["FechaFinal"].To<DateTime?>(null);
                        Modulo.Activo = dtModulos.Rows[0]["Activo"].To<bool>();

                        var listSubModulos = (from subModulos in dtSubModulos.AsEnumerable()
                            group subModulos by subModulos.Field<int>("IdNivel1")
                            into subg1
                            let subMod1 = subg1.FirstOrDefault()
                            let subs3 = subg1.Where(s3 => s3.Field<int?>("IdNivel3") != null).Select(s3 => new Entities.Modulos.Modulo()
                            {
                                IdPadre = s3.Field<int>("IdNivel2"),
                                Id = s3.Field<int>("IdNivel3"),
                                Nombre = s3.Field<string>("NombreNivel3")
                            }).ToList()
                            let subs2 = subg1.Where(s2 => s2.Field<int?>("IdNivel2") != null).Select(s2 => new Entities.Modulos.Modulo()
                            {
                                IdPadre = s2.Field<int>("IdNivel1"),
                                Id = s2.Field<int>("IdNivel2"),
                                Nombre = s2.Field<string>("NombreNivel2")
                            }).ToList()
                            select new Entities.Modulos.Modulo()
                            {
                                Id = subMod1.Field<int>("IdNivel1"),
                                IdPadre = Modulo.Id,
                                Nombre = subMod1.Field<string>("NombreNivel1"),
                                Activo = true,
                                SubModulos = subs2.Where(s2 => s2.IdPadre == subMod1.Field<int>("IdNivel1")).Select(sm2 =>
                                    new Entities.Modulos.Modulo()
                                    {
                                        Id = sm2.Id,
                                        IdPadre = sm2.IdPadre,
                                        Nombre = sm2.Nombre,
                                        SubModulos = subs3.Where(s3 => s3.IdPadre == sm2.Id).ToList()
                                    }).ToList()
                            }).ToList();

                        Modulo.SubModulos = listSubModulos;
                    }
                }
            }

            return Modulo;
        }

        public int CambiarEstatus(IModulo Modulo)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.ModulosCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = Modulo.Id, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = Modulo.Activo, ParameterName = "@Activo" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                result = cmd.ExecuteNonQuery();

                return result;
            }
        }

        public int Insertar(IModulo Modulo)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.ModulosInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = Modulo.IdPadre, ParameterName = "@IdPadre" });
                cmd.Parameters.Add(new SqlParameter() { Value = Modulo.Nombre, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter() { Value = Modulo.Descripcion, ParameterName = "@Descripcion" });
                cmd.Parameters.Add(new SqlParameter() { Value = Modulo.Controlador, ParameterName = "@Controlador" });
                cmd.Parameters.Add(new SqlParameter() { Value = Modulo.Accion, ParameterName = "@Accion" });

                result = cmd.ExecuteNonQuery();
                return result;
            }
        }

        public int Actualizar(IModulo Modulo)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.ModulosActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = Modulo.Id, ParameterName = "@Identificador" });
                cmd.Parameters.Add(new SqlParameter() { Value = Modulo.IdPadre == 0 ? null : Modulo.IdPadre, ParameterName = "@IdPadre" });
                cmd.Parameters.Add(new SqlParameter() { Value = Modulo.Nombre, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter() { Value = Modulo.Descripcion, ParameterName = "@Descripcion" });
                cmd.Parameters.Add(new SqlParameter() { Value = Modulo.Controlador, ParameterName = "@Controlador" });
                cmd.Parameters.Add(new SqlParameter() { Value = Modulo.Accion, ParameterName = "@Accion" });

                result = cmd.ExecuteNonQuery();

                return result;
            }
        }

        public IEnumerable<IModulo> ObtenerPorCriterio(IPaging paging, IModulo entity)
        {
            var result = new List<IModulo>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.ModulosObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.Id, ParameterName = "@IdMenu" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.SubModulos != null && entity.SubModulos.Any() ? entity.SubModulos.First().Id : 0, ParameterName = "@IdSubMenu" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.All, ParameterName = "@Todos" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Entities.Modulos.Modulo Modulo = new Entities.Modulos.Modulo();

                        Modulo.Id = Convert.ToInt32(reader["IdModulo"]);
                        Modulo.IdPadre = reader["IdPadre"] != DBNull.Value ? Convert.ToInt32(reader["IdPadre"]) : 0;
                        Modulo.Nombre = reader["Nombre"].ToString();
                        Modulo.Controlador = reader["Controlador"] != DBNull.Value ? reader["Controlador"].ToString() : "";
                        Modulo.Accion = reader["Accion"] != DBNull.Value ? reader["Accion"].ToString() : "";
                        Modulo.Descripcion = reader["Descripcion"].ToString();
                        Modulo.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (reader["FechaFinal"] != DBNull.Value)
                            Modulo.FechaFinal = reader["Accion"] != DBNull.Value ? Convert.ToDateTime(reader["FechaFinal"]): new DateTime();
                        Modulo.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(Modulo);
                    }
                }
                return result;
            }
        }

        public IEnumerable<IModulo> ObtenerSubModulos(long idModuloPadre)
        {
            List<IModulo> result = new List<IModulo>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.ObtenerSubModulosPorIdPadre;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = idModuloPadre, ParameterName = "@IdPadre" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Entities.Modulos.Modulo modulo = new Entities.Modulos.Modulo()
                        {
                            Id = Convert.ToInt32(reader["IdModulo"]),
                            IdPadre = Convert.ToInt32(reader["IdPadre"]),
                            Nombre = reader["Nombre"].ToString(),
                            Controlador = reader["Controlador"].ToString(),
                            Accion = reader["Accion"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            FechaInicial = Convert.ToDateTime(reader["FechaInicial"]),
                            Activo = Convert.ToBoolean(reader["Activo"])
                        };

                        if (reader["FechaFinal"] != DBNull.Value)
                            modulo.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);

                        result.Add(modulo);
                    }
                }
                return result;
            }

        }

    }
}
