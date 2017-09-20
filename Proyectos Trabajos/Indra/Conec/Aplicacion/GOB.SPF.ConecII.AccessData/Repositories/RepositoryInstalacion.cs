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
            Instalacion instalacion = new Instalacion();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Solicitud.InstalacionObtenerPorId;
                cmd.CommandType = CommandType.StoredProcedure;
                var parameter = new SqlParameter()
                {
                    Value = identificador,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);


                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        instalacion = LeerEntidadInstalacion(reader);

                    }
                }
            }
            return instalacion;
        }

        private static Instalacion LeerEntidadInstalacion(IDataReader reader)
        {
            return new Instalacion
            {
                Identificador = Convert.ToInt32(reader["IdInstalacion"]),
                Cliente = {
                    Identificador = Convert.ToInt32(reader["IdCliente"]),
                    RazonSocial = reader["RazonSocial"].ToString(),
                    NombreCorto = reader["NombreCorto"].ToString()
                },
                Zona = { Identificador = Convert.ToInt32(reader["IdZona"]) },
                Estacion = { Identificador = Convert.ToInt32(reader["IdEstacion"]) },
                Nombre = reader["Nombre"].ToString(),
                Asentamiento =
                {
                    Municipio =
                    {
                        Identificador = Convert.ToInt32(reader["IdMunicipio"]),
                        Nombre = reader["NombreMunicipio"].ToString(),
                    },
                    Estado =
                    {
                        Identificador = Convert.ToInt32(reader["IdEntidad"]),
                        Nombre = reader["NombreEntidad"].ToString()
                    }
                }
            };
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

                var parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" };
                parameters[1] = new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                Instalacion instalacion;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pages = reader["Paginas"] != null ? Convert.ToInt32(reader["Paginas"]) : reader.RecordsAffected;

                        instalacion = LeerEntidadInstalacion(reader);
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
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var instalacion = LeerEntidadInstalacion(reader);
                        pages = reader["Paginas"] != null ? Convert.ToInt32(reader["Paginas"]) : reader.RecordsAffected;
                        result.Add(instalacion);
                    }
                }
                return result;
            }
        }
    }
}
