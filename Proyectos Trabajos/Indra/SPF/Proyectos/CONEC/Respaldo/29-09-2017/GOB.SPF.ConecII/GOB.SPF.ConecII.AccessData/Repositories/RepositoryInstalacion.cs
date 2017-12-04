using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GOB.SPF.ConecII.AccessData.Schemas;
using GOB.SPF.ConecII.Entities;

namespace GOB.SPF.ConecII.AccessData.Repositories
{
    public class RepositoryInstalacion : IRepository<Instalacion>
    {
        private int pages { get; set; }
        public int Pages => pages;

        private readonly UnitOfWorkCatalog _unitOfWork;

        public RepositoryInstalacion(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public int Insertar(Instalacion entity)
        {
            var result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Solicitud.InstalacionInsertar;
                cmd.CommandType = CommandType.StoredProcedure;

                result = cmd.ExecuteNonQuery();

            }
            return result;
        }

        public Instalacion ObtenerPorId(long identificador)
        {
            Instalacion instalacion = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Solicitud.InstalacionObtenerPorId;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter(){Value = identificador,ParameterName = "@IdInstalacion"
                });
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                      instalacion=  LeerInstalacion(reader);
                    }
                }
            }
            return instalacion;
        }

        private static Instalacion LeerInstalacion(IDataRecord reader)
        {
            return  new Instalacion
            {
                Cliente =
                {
                    Identificador = reader["IdCliente"] as int? ?? 0,
                    RazonSocial = reader["RazonSocial"].ToString(),
                    NombreCorto = reader["NombreCorto"].ToString(),
                    Rfc = reader["RFC"].ToString()
                },
                Identificador = reader["IdInstalacion"] as int? ?? 0,
                Zona = {Identificador = reader["IdZona"] as int? ?? 0},
                Estacion = {Identificador = reader["IdEstacion"] as int? ?? 0},
                Nombre = reader["Nombre"].ToString(),
                TipoInstalacion = {Identificador = reader["IdTipoInstalacion"] as int? ?? 0},
                FechaInicio = reader["FechaInicio"] != null ? Convert.ToDateTime(reader["FechaInicio"]) : DateTime.Now,
                FechaFin = reader["FechaFin"] as DateTime?,
                Calle = reader["Calle"].ToString(),
                NumInterior = reader["NumInterior"].ToString(),
                NumExterior = reader["NumExterior"].ToString(),
                Colindancia = reader["Colindancia"].ToString(),
                Asentamiento =
                {
                    CodigoPostal = reader["CodigoPostal"].ToString(),
                    Municipio =
                    {
                        Identificador = reader["IdMunicipio"] as int? ?? 0,
                        Nombre = reader["Municipio"].ToString()
                    },
                    Estado =
                    {
                        Identificador = reader["IdEntidad"] as int? ?? 0,
                        Nombre = reader["Entidad"].ToString()
                    }
                },
                Latitud = reader["Latitud"] as decimal? ?? 0,
                Longitud = reader["Longitud"] as decimal? ?? 0,
                Division =
                {
                    Identificador = reader["IdDivision"] as int? ?? 0,
                    NombreDivision = reader["Division"].ToString()
                },
                Grupo =
                {
                    Identificador = reader["IdGrupo"] as int? ?? 0,
                    Descripcion = reader["Grupo"].ToString()
                },
                Fraccion =
                {
                    Identificador = reader["IdFraccion"] as int? ?? 0,
                    Nombre = reader["Fraccion"].ToString()
                },
                Activo = Convert.ToBoolean(reader["Activo"])
            };
        }

        private static Instalacion LeerInstalacionConsulta(IDataRecord reader)
        {
            var instalacion = new Instalacion
            {
                Identificador = Convert.ToInt32(reader["IdInstalacion"]),
                Cliente =
                {
                    Identificador = Convert.ToInt32(reader["IdCliente"]),
                    RazonSocial = reader["RazonSocial"].ToString(),
                    NombreCorto = reader["NombreCorto"].ToString()
                },
                Zona = { Identificador = Convert.ToInt32(reader["IdZona"]) },
                Estacion = { Identificador = Convert.ToInt32(reader["IdEstacion"]) },
                Nombre = reader["Nombre"].ToString()
            };
            instalacion.Asentamiento.Municipio.Identificador = Convert.ToInt32(reader["IdMunicipio"]);
            instalacion.Asentamiento.Municipio.Nombre = reader["NombreMunicipio"].ToString();
            instalacion.Asentamiento.Estado.Identificador = Convert.ToInt32(reader["IdEntidad"]);
            instalacion.Asentamiento.Estado.Nombre = reader["NombreEntidad"].ToString();
            return instalacion;
        }

        public int CambiarEstatus(Instalacion entity)
        {

            var result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Solicitud.InstalacionCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                var parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = entity.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                result = cmd.ExecuteNonQuery();
            }
            return result;
        }

        public int Actualizar(Instalacion entity)
        {
            var result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Solicitud.InstalacionActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                var parameters = new SqlParameter[3];


                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);

                result = cmd.ExecuteNonQuery();
            }
            return result;

        }

        public IEnumerable<Instalacion> Obtener(Paging paging)
        {
            var result = new List<Instalacion>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Solicitud.InstalacionObtener;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                Instalacion instalacion;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pages = reader["Paginas"] != null ? Convert.ToInt32(reader["Paginas"]) : reader.RecordsAffected;

                        instalacion = LeerInstalacionConsulta(reader);
                        result.Add(instalacion);
                    }
                }
                return result;
            }
        }

        public IEnumerable<Instalacion> ObtenerPorCriterio(Paging paging, Instalacion entity)
        {
            var result = new List<Instalacion>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Solicitud.InstalacionObtenerPorCriterio;
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@RFC" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@RazonSocial" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@NombreCorto" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@Activo" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var instalacion = LeerInstalacionConsulta(reader);
                        pages = reader["Paginas"] != null ? Convert.ToInt32(reader["Paginas"]) : reader.RecordsAffected;
                        result.Add(instalacion);
                    }
                }
                return result;
            }
        }
    }
}
