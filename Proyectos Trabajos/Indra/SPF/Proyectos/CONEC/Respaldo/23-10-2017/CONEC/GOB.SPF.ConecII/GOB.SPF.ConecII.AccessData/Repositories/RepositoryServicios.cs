using GOB.SPF.ConecII.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Interfaces.Genericos;


namespace GOB.SPF.ConecII.AccessData.Repositories
{
    public class RepositoryServicios : IRepository<Servicio>
    {
        #region Variables privadas
        private IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public RepositoryServicios(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow;
        }
        #endregion

        #region Métodos públicos
        public int Actualizar(Servicio entity)
        {
            return 1;
        }

        public int ActualizarServicioAcuerdo(Servicio entity)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.ServicioActualizarComite;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { Value = entity.Identificador, ParameterName = "@IdServicio" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.TieneComite, ParameterName = "@TieneComite" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Observaciones, ParameterName = "@ObservacionesComite" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Viable, ParameterName = "@Viable" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.FechaComite, ParameterName = "@FechaComite" });
                return cmd.ExecuteNonQuery();
            }
        }

        public int CambiarEstatus(Servicio entity)
        {
            return 1;
        }

        public int Insertar(Servicio servicio)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                var idSql = new SqlParameter { ParameterName = "@IdServicio", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

                cmd.CommandText = Schemas.Solicitud.ServicioInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { Value = servicio.IdentificadorPadre, ParameterName = "@IdSolicitud" });
                cmd.Parameters.Add(new SqlParameter { Value = servicio.TipoServicio.Identificador, ParameterName = "@IdTipoServicio" });
                cmd.Parameters.Add(new SqlParameter { Value = servicio.Cuota?.Identificador, ParameterName = "@IdCuota" });
                cmd.Parameters.Add(new SqlParameter { Value = servicio.TipoInstalacionesCapacitacion?.Identificador, ParameterName = "@IdTipoInstalacion" });
                cmd.Parameters.Add(new SqlParameter { Value = servicio.FechaInicial, ParameterName = "@FechaInicio" });
                cmd.Parameters.Add(new SqlParameter { Value = servicio.FechaFinal, ParameterName = "@FechaFinal" });
                cmd.Parameters.Add(new SqlParameter { Value = servicio.NumeroPersonas == 0 ? null : (int?)servicio.NumeroPersonas, ParameterName = "@NumeroPersonas" });
                cmd.Parameters.Add(new SqlParameter { Value = servicio.HorasCurso == 0 ? null : (int?)servicio.HorasCurso, ParameterName = "@HorasCurso" });
                cmd.Parameters.Add(new SqlParameter { Value = servicio.Observaciones, ParameterName = "@Observaciones" });
                cmd.Parameters.Add(new SqlParameter { Value = servicio.TieneComite, ParameterName = "@TieneComite" });
                cmd.Parameters.Add(new SqlParameter { Value = servicio.ObservacionesComite ?? (object)DBNull.Value, ParameterName = "@ObservacionesComite" });
                cmd.Parameters.Add(new SqlParameter { Value = servicio.BienCustodia ?? (object)DBNull.Value, ParameterName = "@BienCustodia" });
                cmd.Parameters.Add(new SqlParameter { Value = DateTime.Now, ParameterName = "@FechaComite" });

                cmd.Parameters.Add(idSql);

                cmd.ExecuteNonQuery();
                return (int)idSql.Value;
            }
        }

        public IEnumerable<Servicio> Obtener(IPaging paging)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Servicio> ObtenerPorCriterio(IPaging paging, Servicio entity)
        {
            throw new NotImplementedException();
        }


        public Servicio ObtenerPorId(long Identificador)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Servicio> ObtenerPorIdSolicitud(Solicitud solicitud)
        {
            using (IDbCommand cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.ServiciosObtenerPorIdSolicitud;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = solicitud.Identificador, ParameterName = "@IdSolicitud" });

                DataTable dtServicios = new DataTable();
                DataTable dtDocumentos = new DataTable();
                DataTable dtInstalaciones = new DataTable();

                using (IDataReader lector = cmd.ExecuteReader())
                {
                    dtServicios.Load(lector);
                    dtDocumentos.Load(lector);
                    dtInstalaciones.Load(lector);

                    List<Servicio> listServicios = (from servicios in dtServicios.AsEnumerable()
                                                    join documentos in dtDocumentos.AsEnumerable() on servicios.Field<int>("IdServicio") equals documentos.Field<int>("IdServicio") into servDoc
                                                    from servDocumentos in servDoc.DefaultIfEmpty()
                                                    join instalaciones in dtInstalaciones.AsEnumerable() on servicios.Field<int>("IdServicio") equals instalaciones.Field<int>("IdServicio") into servInst
                                                    from servInstalaciones in servInst.DefaultIfEmpty()
                                                    group new { servicios, servDocumentos, servInstalaciones } by servicios.Field<int>("IdServicio") into g
                                                    let servicio = g.FirstOrDefault().servicios
                                                    let servicioDocumentos = g.FirstOrDefault().servDocumentos != null ? g.Select(r => new ServicioDocumento()
                                                    {
                                                        IdServicio = servicio.Field<int>("IdServicio"),
                                                        ArchivoId = Convert.ToInt32(r.servDocumentos["IdServicioDocumento"]),
                                                        Nombre = r.servDocumentos["Nombre"].ToString()
                                                    }).ToList() : null
                                                    let servicioInstalaciones = g.FirstOrDefault().servInstalaciones != null ? g.Select(r => new ServicioInstalacion()
                                                    {
                                                        NumInterior = r.servInstalaciones["NumInterior"].ToString(),
                                                        NumExterior = r.servInstalaciones["NumExterior"].ToString(),
                                                        Zona = new Zona()
                                                        {
                                                            IdZona = Convert.ToInt32(r.servInstalaciones["IdZona"]),
                                                            Nombre = r.servInstalaciones["Zona"].ToString()
                                                        },
                                                        Estacion = new Estacion()
                                                        {
                                                            Identificador = Convert.ToInt32(r.servInstalaciones["IdEstacion"]),
                                                            Nombre = r.servInstalaciones["Estacion"].ToString()
                                                        },
                                                        Nombre = r.servInstalaciones["Nombre"].ToString(),
                                                        Asentamiento = new Asentamiento()
                                                        {
                                                            Estado = new Estado()
                                                            {
                                                                Identificador = Convert.ToInt32(r.servInstalaciones["IdEntidad"]),
                                                                Nombre = r.servInstalaciones["EntidadFederativa"].ToString()
                                                            },
                                                            Municipio = new Municipio()
                                                            {
                                                                Identificador = Convert.ToInt32(r.servInstalaciones["IdMunicipio"]),
                                                                Nombre = r.servInstalaciones["Municipio"].ToString()
                                                            }
                                                        }
                                                    }).ToList() : null
                                                    select new Servicio()
                                                    {
                                                        IdentificadorPadre = Convert.ToInt32(servicio["IdSolicitud"]),
                                                        Identificador = Convert.ToInt32(servicio["IdServicio"]),
                                                        NumeroPersonas = Convert.ToInt32(servicio["NumeroPersonas"]),
                                                        HorasCurso = servicio.Field<int>("HorasCurso"),
                                                        FechaInicial = servicio.Field<DateTime?>("FechaInicial") != null ? servicio.Field<DateTime>("FechaInicial") : DateTime.Now,
                                                        FechaFinal = servicio.Field<DateTime?>("FechaFinal") != null ? servicio.Field<DateTime>("FechaFinal") : DateTime.Now,
                                                        Observaciones = servicio.Field<string>("Observaciones"),
                                                        BienCustodia = servicio.Field<string>("BienCustodia"),
                                                        TipoServicio = new TipoServicio() { Identificador = servicio.Field<int>("IdTipoServicio"), Nombre = servicio.Field<string>("NombreServicio") },
                                                        Cuota = new Cuota() { Identificador = servicio.Field<int>("IdCuota"), Concepto = servicio.Field<string>("Concepto") },
                                                        TipoInstalacionesCapacitacion = new TipoInstalacionesCapacitacion() { Identificador = servicio.Field<int>("IdTipoInstalacion"), Nombre = servicio.Field<string>("NombreInstalacion") },
                                                        Documentos = servicioDocumentos,
                                                        Instalaciones = servicioInstalaciones
                                                    }).ToList();

                    return listServicios;
                }
            }
        }

        public IEnumerable<ServicioInstalacion> ObtenerInstalacionesPorIdCliente(Solicitud solicitud, int? IdServicio = null)
        {
            using (IDbCommand cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.InstalacionObtenerPorIdSolicitud;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = solicitud.Cliente.Identificador, ParameterName = "@IdCliente" });
                //cmd.Parameters.Add(new SqlParameter() { Value = solicitud.Identificador == 0 ? null : (int?)solicitud.Identificador, ParameterName = "@IdSolicitud" });
                cmd.Parameters.Add(new SqlParameter() { Value = IdServicio == 0 ? null : IdServicio, ParameterName = "@IdServicio" });
                
                DataTable dtInstalaciones = new DataTable();

                using (IDataReader lector = cmd.ExecuteReader())
                {
                    dtInstalaciones.Load(lector);

                    List<ServicioInstalacion> listServicioInstalacion = new List<ServicioInstalacion>();

                    listServicioInstalacion = dtInstalaciones.AsEnumerable().Select(r => new ServicioInstalacion()
                    {
                        Identificador = Convert.ToInt32(r["IdInstalacion"]),
                        Nombre = r["Nombre"].ToString(),
                        Activo = Convert.ToBoolean(r["Activo"]),
                        IdCliente = Convert.ToInt32(r["IdCliente"]),
                        TipoInstalacion = new TipoInstalacion()
                        {
                            Identificador = Convert.ToInt32(r["IdTipoInstalacion"])
                        },
                        Seleccionado = Convert.ToBoolean(r["Seleccionado"]),
                        Zona = new Zona()
                        {
                            IdZona = Convert.ToInt32(r["IdZona"]),
                            Nombre = r["Zona"].ToString(),
                            Vigente = Convert.ToBoolean(r["Vigente"])
                        },
                        Estacion = new Estacion()
                        {
                            Identificador = Convert.ToInt32(r["IdEstacion"]),
                            Nombre = r["Estacion"].ToString()
                        },
                        Asentamiento = new Asentamiento()
                        {
                            Estado = new Estado()
                            {
                                Identificador = Convert.ToInt32(r["IdEntidad"]),
                                Nombre = r["Entidad"].ToString()
                            },
                            Municipio = new Municipio()
                            {
                                Identificador = Convert.ToInt32(r["IdMunicipio"]),
                                Nombre = r["Municipio"].ToString()
                            }
                        }
                    }).ToList();

                    return listServicioInstalacion;
                }
            }
        }
        #endregion
    }
}
