using System.Runtime.Remoting;
using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.AccessData.Repositories
{
    #region Librerias

    using Entities;
    using Schemas;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    #endregion

    public class RepositoryInstalacion : IRepository<Instalacion>
    {
        #region Variables privadas

        private int pages { get; set; }

        private readonly UnitOfWorkCatalog _unitOfWork;

        #endregion

        #region Propiedades públicas

        public int Pages => pages;

        #endregion

        #region Constructor

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

        #endregion

        #region Métodos privados

        private static InstalacionCliente LeerInstalacionConsulta(IDataRecord reader)
        {
            var instalacion = new InstalacionCliente();
            instalacion.Identificador = reader["IdInstalacion"] as int? ?? 0;
            instalacion.IdCliente = reader["IdCliente"] as int? ?? 0;
            instalacion.RazonSocial = reader["RazonSocial"].ToString();
            instalacion.NombreCorto = reader["NombreCorto"].ToString();
            instalacion.Rfc = reader["RFC"].ToString();
            instalacion.Zona.Identificador = reader["IdZona"] as int? ?? 0;
            instalacion.Zona.Nombre = reader["Zona"].ToString();
            instalacion.Estacion.Identificador = reader["IdEstacion"] as int? ?? 0;
            instalacion.Estacion.Nombre = reader["Estacion"].ToString();
            instalacion.Nombre = reader["Nombre"].ToString();
            instalacion.Asentamiento.Municipio.Identificador = reader["IdMunicipio"] as int? ?? 0;
            instalacion.Asentamiento.Municipio.Nombre = reader["NombreMunicipio"].ToString();
            instalacion.Asentamiento.Estado.Identificador = reader["IdEntidad"] as int? ?? 0;
            instalacion.Activo = reader["Activo"] as bool?;
            instalacion.Asentamiento.Estado.Nombre = reader["NombreEntidad"].ToString();
            return instalacion;
        }

        private static InstalacionCliente LeerInstalacion(IDataRecord reader)
        {
            return new InstalacionCliente
            {
               
                IdCliente = reader["IdCliente"] as int? ?? 0,
                RazonSocial = reader["RazonSocial"].ToString(),
                NombreCorto = reader["NombreCorto"].ToString(),
                Rfc = reader["RFC"].ToString(),
                Identificador = reader["IdInstalacion"] as int? ?? 0,
                Zona = { Identificador = reader["IdZona"] as int? ?? 0 },
                Estacion = { Identificador = reader["IdEstacion"] as int? ?? 0 },
                Nombre = reader["Nombre"].ToString(),
                TipoInstalacion = { Identificador = reader["IdTipoInstalacion"] as int? ?? 0 },
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
                Latitud = reader["Latitud"] as double? ?? 0,
                Longitud = reader["Longitud"] as double? ?? 0,
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

        #endregion

        #region Métodos públicos

        /// <summary>
        /// Método para obtener las instalaciones de acuerdo a los parámetros establecidos.
        /// </summary>
        /// <param name="paging">Paginación</param>
        /// <param name="entity">Entidad tipo Instalacion</param>
        /// <returns>Listado de Instalaciones</returns>
        public IEnumerable<InstalacionCliente> ObtenerPorCriterio(IPaging paging, Instalacion entity, Cliente cliente)
        {
            var result = new List<InstalacionCliente>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.InstalacionObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { Value = cliente.Rfc, ParameterName = "@RFC" });
                cmd.Parameters.Add(new SqlParameter { Value = cliente.RazonSocial, ParameterName = "@RazonSocial" });
                cmd.Parameters.Add(new SqlParameter { Value = cliente.NombreCorto, ParameterName = "@NombreCorto" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Nombre, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Activo, ParameterName = "@Activo", IsNullable = true });
                cmd.Parameters.Add(new SqlParameter { Value = paging.CurrentPage, ParameterName = "@Pagina" });
                cmd.Parameters.Add(new SqlParameter { Value = paging.Rows, ParameterName = "@Filas" });
                cmd.Parameters.Add(new SqlParameter { Value = paging.All, ParameterName = "@Todos" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pages = reader["Pagina"] as int? ?? reader.RecordsAffected;
                        var instalacion = LeerInstalacionConsulta(reader);
                        result.Add(instalacion);
                    }
                }
                return result;
            }
        }

        public IEnumerable<InstalacionCliente> ObtenerPorIdCliente(Cliente cliente)
        {
            var result = new List<InstalacionCliente>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.InstalacionObtenerPorIdCliente;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { Value = cliente.Identificador, ParameterName = "@IdCliente" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var instalacion = LeerInstalacionConsulta(reader);
                        result.Add(instalacion);
                    }
                }
                return result;
            }
        }

        public List<string> ObtenerPorNombre(string nombre)
        {

            var result = new List<string>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.InstalacionObtenerPorNombre;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { Value =nombre, ParameterName = "@Nombre" });
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(reader["Nombre"].ToString());
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// Método para obtener el listado completo de las instalaciones.
        /// </summary>
        /// <param name="paging">´Paginación</param>
        /// <returns>Listado paginado de Instalaciones</returns>
        public IEnumerable<InstalacionCliente> Obtener(IPaging paging)
        {
            var result = new List<InstalacionCliente>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.InstalacionObtener;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { Value = paging.CurrentPage, ParameterName = "@Pagina" });
                cmd.Parameters.Add(new SqlParameter { Value = paging.Rows, ParameterName = "@Filas" });
                cmd.Parameters.Add(new SqlParameter { Value = paging.All, ParameterName = "@Todos" });
                InstalacionCliente instalacion;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pages = reader["Pagina"] as int? ?? reader.RecordsAffected;
                        instalacion = LeerInstalacionConsulta(reader);
                        result.Add(instalacion);
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// Método para obtener la instalacion filtrado por el Identificador
        /// </summary>
        /// <param name="identificador">Identificador de la instalación</param>
        /// <returns>Instalacion</returns>
        public Instalacion ObtenerPorId(long identificador)
        {
            Instalacion instalacion = null;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.InstalacionObtenerPorId;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter { Value = identificador, ParameterName = "@IdInstalacion" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        instalacion = LeerInstalacion(reader);
                    }
                }
            }
            return instalacion;
        }

        /// <summary>
        /// Método para cambiar el estatus de la instalación
        /// </summary>
        /// <param name="entity">Instalacion</param>
        /// <returns>Identificador Instalación actualizada</returns>
        public int CambiarEstatus(Instalacion entity)
        {
            int result;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.InstalacionCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter { Value = entity.Identificador, ParameterName = "@IdInstalacion" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Activo, ParameterName = "@Activo" });

                result =cmd.ExecuteNonQuery();
            }
            return result;
        }

        /// <summary>
        /// Método que actualiza la instalacion de acuerdo a los parámetros proporcionados.
        /// </summary>
        /// <param name="entity">Instalación a actualizar</param>
        /// <returns>Identificador de la instalacion actualizada</returns>
        public int Actualizar(Instalacion entity, int IdCliente = 0)
        {
            int result;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.InstalacionActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter { Value = entity.Identificador, ParameterName = "@IdInstalacion" });
                cmd.Parameters.Add(new SqlParameter { Value = IdCliente, ParameterName = "@IdCliente" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Zona.Identificador, ParameterName = "@IdZona" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Estacion.Identificador, ParameterName = "@IdEstacion" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Nombre, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.FechaInicio, ParameterName = "@FechaInicio" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.FechaFin, ParameterName = "@FechaFin" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Calle, ParameterName = "@Calle" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.NumInterior, ParameterName = "@NumInterior" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.NumExterior, ParameterName = "@NumExterior" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Referencia, ParameterName = "@Referencia" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Colindancia, ParameterName = "@Colindancia" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Asentamiento.CodigoPostal, ParameterName = "@CodigoPostal" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Asentamiento.Identificador, ParameterName = "@IdAsentamiento" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Asentamiento.Municipio.Identificador, ParameterName = "@IdMunicipio" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Asentamiento.Estado.Identificador, ParameterName = "@IdEntidad" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Latitud, ParameterName = "@Latitud" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Longitud, ParameterName = "@Longitud" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Fraccion.Identificador, ParameterName = "@IdFraccion" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.TipoInstalacion.Identificador, ParameterName = "@IdTipoInstalacion" });
                result = (int)cmd.ExecuteScalar();
            }
            return result;

        }

        /// <summary>
        /// Método que inserta una nueva instalación.
        /// </summary>
        /// <param name="entity">Nueva instalación</param>
        /// <returns>Identificdor de la instalación creada</returns>
        public int Insertar(Instalacion entity,int IdCliente = 0)
        {
            int result;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.InstalacionInsertar;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter { Value = IdCliente, ParameterName = "@IdCliente" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Zona.Identificador, ParameterName = "@IdZona" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Estacion.Identificador, ParameterName = "@IdEstacion" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Nombre, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.FechaInicio, ParameterName = "@FechaInicio" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.FechaFin, ParameterName = "@FechaFin" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Calle, ParameterName = "@Calle" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.NumInterior, ParameterName = "@NumInterior" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.NumExterior, ParameterName = "@NumExterior" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Referencia, ParameterName = "@Referencia" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Colindancia, ParameterName = "@Colindancia" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Asentamiento.CodigoPostal, ParameterName = "@CodigoPostal" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Asentamiento.Identificador, ParameterName = "@IdAsentamiento" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Asentamiento.Municipio.Identificador, ParameterName = "@IdMunicipio" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Asentamiento.Estado.Identificador, ParameterName = "@IdEntidad" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Latitud, ParameterName = "@Latitud" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Longitud, ParameterName = "@Longitud" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Fraccion.Identificador, ParameterName = "@IdFraccion" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.TipoInstalacion.Identificador, ParameterName = "@IdTipoInstalacion" });

                result = (int)cmd.ExecuteScalar();
            }
            return result;
        }

        public int Insertar(Instalacion entity)
        {
            throw new NotImplementedException();
        }

        public int Actualizar(Instalacion entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Instalacion> ObtenerPorCriterio(IPaging paging, Instalacion entity)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Instalacion> IRepository<Instalacion>.Obtener(IPaging paging)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
