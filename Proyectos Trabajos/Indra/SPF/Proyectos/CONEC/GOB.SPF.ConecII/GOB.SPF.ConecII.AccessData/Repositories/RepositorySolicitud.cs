using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Entities;
using System.Data.SqlClient;
using System.Data;
using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.AccessData.Repositories
{
    public class RepositorySolicitud : IRepository<Solicitud>
    {
        #region variables privadas
        private IUnitOfWork _unitOfWork;
        #endregion

        #region constructor
        public RepositorySolicitud(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }
        #endregion

        #region métodos públicos
        public int Actualizar(Solicitud solicitud)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.SolicitudActualizar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { Value = solicitud.Identificador, ParameterName = "@IdSolicitud" });
                cmd.Parameters.Add(new SqlParameter { Value = solicitud.Cliente.Identificador, ParameterName = "@IdCliente" });
                cmd.Parameters.Add(new SqlParameter { Value = solicitud.TipoSolicitud.Identificador, ParameterName = "@idTipoSolicitud" });
                cmd.Parameters.Add(new SqlParameter { Value = solicitud.Documento.ArchivoId, ParameterName = "@DocumentoSoporte" });
                cmd.Parameters.Add(new SqlParameter { Value = solicitud.Minuta ?? (object)DBNull.Value, ParameterName = "@Minuta" });
                cmd.Parameters.Add(new SqlParameter { Value = solicitud.Folio, ParameterName = "@Folio" });
                cmd.Parameters.Add(new SqlParameter { Value = solicitud.Cancelado, ParameterName = "@Cancelado" });
                cmd.ExecuteNonQuery();
            }
            return 1;
        }

        public int CambiarEstatus(Solicitud solicitud)
        {
            return 1;
        }

        public int Insertar(Solicitud solicitud)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.SolicitudInsertar;
                cmd.CommandType = CommandType.StoredProcedure;

                var idSql = new SqlParameter { ParameterName = "@IdSolicitud", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

                cmd.Parameters.Add(new SqlParameter { Value = solicitud.Cliente.Identificador, ParameterName = "@IdCliente" });
                cmd.Parameters.Add(new SqlParameter { Value = solicitud.TipoSolicitud.Identificador, ParameterName = "@idTipoSolicitud" });
                cmd.Parameters.Add(new SqlParameter { Value = solicitud.Documento.ArchivoId, ParameterName = "@DocumentoSoporte" });
                cmd.Parameters.Add(new SqlParameter { Value = solicitud.Folio, ParameterName = "@Folio" });
                cmd.Parameters.Add(idSql);

                cmd.ExecuteNonQuery();
                return (int)idSql.Value;
            }
        }

        public IEnumerable<Solicitud> Obtener(IPaging paging)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Solicitud> ObtenerPorCriterio(IPaging paging, Solicitud entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Solicitud> ObtenerPorCriterio(IPaging paging, Solicitud entity, DateTime? fechaRegistroMin, DateTime? fechaRegistroMax)
        {
            List<Solicitud> solicitudes = new List<Solicitud>();
            using (IDbCommand cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.SolicitudesObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.Folio, ParameterName = "@Folio" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Servicios.Count > 0 ? entity.Servicios.FirstOrDefault().TipoServicio.Identificador : 0, ParameterName = "@IdTipoServicio" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Cliente != null ? entity.Cliente.RazonSocial ?? string.Empty : string.Empty, ParameterName = "@RazonSocial" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Cliente != null ? entity.Cliente.NombreCorto ?? string.Empty : string.Empty, ParameterName = "@NombreCorto" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Cliente != null ? entity.Cliente.Rfc ?? string.Empty : string.Empty, ParameterName = "@RFC" });
                if (fechaRegistroMin != null && fechaRegistroMax != null)
                {
                    cmd.Parameters.Add(new SqlParameter() { Value = fechaRegistroMin.Value.Date.ToString("yyyy-MM-dd"), ParameterName = "@FechaRegistroMin" });
                    cmd.Parameters.Add(new SqlParameter() { Value = fechaRegistroMax.Value.Date.ToString("yyyy-MM-dd"), ParameterName = "@FechaRegistroMax" });
                }
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Cliente != null && entity.Cliente.RegimenFiscal != null ? entity.Cliente.RegimenFiscal.Identificador : 0, ParameterName = "@IdRegimenFiscal" });
                //cmd.Parameters.Add(new SqlParameter() { Value = entity.Cliente != null && entity.Cliente.Sector != null ? entity.Cliente.Sector.Identificador : 0, ParameterName = "@IdSector" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Cancelado, ParameterName = "@Estatus" });
                cmd.Parameters.Add(new SqlParameter { Value = paging.All ? 1 : 0, ParameterName = "@Todos" });
                cmd.Parameters.Add(new SqlParameter { Value = paging.CurrentPage, ParameterName = "@Pagina" });
                cmd.Parameters.Add(new SqlParameter { Value = paging.Rows, ParameterName = "@Filas" });

                using (IDataReader lector = cmd.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        Solicitud solicitud = new Solicitud()
                        {
                            Identificador = Convert.ToInt32(lector["IdSolicitud"]),
                            Cliente = new Cliente()
                            {
                                Identificador = Convert.ToInt32(lector["IdCliente"]),
                                RazonSocial = lector["RazonSocial"].ToString(),
                                NombreCorto = lector["NombreCorto"].ToString(),
                                Rfc = lector["RFC"].ToString(),
                                RegimenFiscal = new RegimenFiscal()
                                {
                                    Identificador = Convert.ToInt32(lector["IdRegimenFiscal"]),
                                    Nombre = lector["RegimenFiscal"].ToString()
                                },
                                Sector = new Sector()
                                {
                                    Identificador = Convert.ToInt32(lector["IdSector"]),
                                    Descripcion = lector["Sector"].ToString()
                                }
                            },
                            TipoSolicitud = new TipoSolicitud() { Identificador = Convert.ToInt32(lector["IdTipoSolicitud"]), Nombre = lector["TipoSolicitud"].ToString() },
                            Folio = Convert.ToInt32(lector["Folio"]),
                            DocumentoSoporte = Convert.ToInt32(lector["DocumentoSoporte"]),
                            FechaRegistro = Convert.ToDateTime(lector["FechaRegistro"]),
                        };

                        if (lector["Minuta"] != DBNull.Value)
                            solicitud.Minuta = Convert.ToInt32(lector["Minuta"]);

                        if (lector["Cancelado"] != DBNull.Value)
                            solicitud.Cancelado = Convert.ToBoolean(lector["Cancelado"]);

                        solicitud.Paging.Pages = Convert.ToInt32(lector["Paginas"]);
                        solicitudes.Add(solicitud);
                    }

                }
            }
            return solicitudes;
        }

        public Solicitud ObtenerSolicitudServiciosAcuerdosPorIdSolicitud(Solicitud entity)
        {
            Solicitud solicitud = new Solicitud();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.SolicitudServiciosAcuerdosPorIdSolicitud;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@IdSolicitud" });

                DataTable dtSolicitud = new DataTable();
                DataTable dtServicios = new DataTable();
                DataTable dtAcuerdos = new DataTable();

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    dtSolicitud.Load(reader);
                    dtServicios.Load(reader);
                    dtAcuerdos.Load(reader);

                    if (dtSolicitud.Rows != null & dtSolicitud.Rows.Count > 0)
                    {
                        DataRow rowSolicitud = dtSolicitud.Rows[0];

                        List<Servicio> listServicios = (from acuerdos in dtAcuerdos.AsEnumerable()
                                                        join servicios in dtServicios.AsEnumerable() on acuerdos.Field<int>("IdServicio") equals servicios.Field<int>("IdServicio")
                                                        group new { acuerdos, servicios } by acuerdos.Field<int>("IdServicio") into g
                                                        let serv = g.FirstOrDefault().servicios
                                                        let acuerdos = g.Select(r => new Acuerdo() { IdentificadorPadre = serv.Field<int>("IdServicio"), Identificador = Convert.ToInt32(r.acuerdos["IdServicioAcuerdo"]), Convenio = r.acuerdos["Acuerdo"].ToString(), Fecha = Convert.ToDateTime(r.acuerdos["FechaCumplimiento"]), Responsable = Guid.Parse(r.acuerdos["Responsable"].ToString()), IsActive = !String.IsNullOrEmpty(r.acuerdos["Activo"].ToString()) ? Convert.ToBoolean(r.acuerdos["Activo"]) : true }).ToList()
                                                        select new Servicio()
                                                        {
                                                            IdentificadorPadre = Convert.ToInt32(rowSolicitud["IdSolicitud"]),
                                                            Identificador = g.Key,
                                                            TipoServicio = new TipoServicio() { Identificador = serv.Field<int>("IdTipoServicio"), Nombre = serv.Field<string>("TipoServicio") },
                                                            Cuota = new Cuota() { Identificador = serv.Field<int>("IdCuota") },
                                                            TipoInstalacionesCapacitacion = new TipoInstalacionesCapacitacion() { Identificador = serv.Field<int>("IdTipoInstalacion"), Nombre = serv.Field<string>("TipoInstalacion") },
                                                            Integrantes = serv.Field<int>("NumeroPersonas"),
                                                            HorasCurso = serv.Field<int>("HorasCurso"),
                                                            FechaInicial = serv.Field<DateTime?>("FechaInicial") != null ? serv.Field<DateTime>("FechaInicial") : DateTime.Now,
                                                            FechaFinal = serv.Field<DateTime?>("FechaFinal") != null ? serv.Field<DateTime>("FechaFinal") : DateTime.Now,
                                                            Observaciones = serv.Field<string>("Observaciones"),
                                                            TieneComite = serv.Field<bool?>("TieneComite") != null ? serv.Field<bool>("TieneComite") : false,
                                                            ObservacionesComite = serv.Field<string>("ObservacionesComite"),
                                                            BienCustodia = serv.Field<string>("BienCustodia"),
                                                            FechaComite = serv.Field<DateTime?>("FechaComite") != null ? serv.Field<DateTime>("FechaComite") : DateTime.Now,
                                                            Viable = serv.Field<bool?>("Viable") != null ? serv.Field<bool>("Viable") : false,
                                                            Acuerdos = acuerdos
                                                        }).ToList();

                        solicitud = new Solicitud()
                        {
                            Identificador = Convert.ToInt32(rowSolicitud["IdSolicitud"]),
                            Cliente = new Cliente() { Identificador = Convert.ToInt32(rowSolicitud["IdCliente"]), RazonSocial = rowSolicitud["Cliente"].ToString() },
                            TipoSolicitud = new TipoSolicitud() { Identificador = Convert.ToInt32(rowSolicitud["IdTipoSolicitud"]), Descripcion = rowSolicitud["TipoSolicitud"].ToString() },
                            Documento = new Entities.Amatzin.Documento() { ArchivoId = Convert.ToInt32(rowSolicitud["DocumentoSoporte"]) },
                            Folio = Convert.ToInt32(rowSolicitud["Folio"]),
                            FechaRegistro = Convert.ToDateTime(rowSolicitud["FechaRegistro"]),
                            Servicios = listServicios
                        };

                        if (!String.IsNullOrEmpty(rowSolicitud["Minuta"].ToString()))
                            solicitud.Minuta = Convert.ToInt32(rowSolicitud["Minuta"]);

                        if (!String.IsNullOrEmpty(rowSolicitud["Cancelado"].ToString()))
                            solicitud.Cancelado = Convert.ToBoolean(rowSolicitud["Cancelado"]);
                    }
                    reader.Close();
                }
                return solicitud;
            }
        }

        public List<Solicitud> ObtenerSolicitudPorIdCliente(long identificador)
        {
            var result = new List<Solicitud>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.SolicitudesObtenerPorIdCliente;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = identificador, ParameterName = "@IdCliente" });
                
                DataTable dtSolicitud = new DataTable();
                DataTable dtServicios = new DataTable();
                DataTable dtInstalaciones = new DataTable();

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    dtSolicitud.Load(reader);
                    dtServicios.Load(reader);
                    dtInstalaciones.Load(reader);

                    var resultado = dtSolicitud.AsEnumerable().Select(p=> new Solicitud()
                    {
                        Identificador = Convert.ToInt32(p["IdSolicitud"]),
                        IdCliente = Convert.ToInt32(p["IdCliente"]),
                        Cliente = new Cliente()
                        {
                            Identificador = Convert.ToInt32(p["IdCliente"]),
                            RazonSocial = p["RazonSocial"].ToString(),
                            NombreCorto = p["NombreCorto"].ToString(),
                            Rfc = p["RFC"].ToString()
                        },
                        TipoSolicitud = new TipoSolicitud()
                        {
                            Identificador  = Convert.ToInt32(p["IdTipoSolicitud"]),
                            Nombre = p["TipoSolicitud"].ToString()
                        },
                        DocumentoSoporte = Convert.ToInt32(p["DocumentoSoporte"]),
                        Folio = Convert.ToInt32(p["Folio"]),
                        FechaRegistro = Convert.ToDateTime(p["FechaRegistro"]),
                        Minuta = Convert.ToInt32(p["Minuta"]),
                        Cancelado = Convert.ToBoolean(p["Cancelado"]),
                        Estatus = new Estatus()
                        {
                            Identificador = Convert.ToInt32(p["IdEstatus"]),
                            Nombre = p["EstatusSolicitud"].ToString()
                        },
                        Servicios = dtServicios.AsEnumerable().Where(q=> q["IdSolicitud"] == p["IdSolicitud"]).Select(r=> new Servicio()
                        {
                            Identificador = Convert.ToInt32(r["IdServicio"]),
                            TipoServicio = new TipoServicio()
                            {
                                Identificador = Convert.ToInt32(r["IdTipoServicio"]),
                                Nombre = r["TipoServicio"].ToString()
                            },
                            Cuota = new Cuota()
                            {
                                Identificador = Convert.ToInt32(r["IdCuota"]),
                                Concepto = r["Cuota"].ToString()
                            },
                            TipoInstalacionesCapacitacion = new TipoInstalacionesCapacitacion()
                            {
                                Identificador = Convert.ToInt32(r["IdTipoInstalacion"]),
                                Nombre = r["TipoInstalacion"].ToString()
                            },
                            NumeroPersonas = Convert.ToInt32(r["NumeroPersonas"]),
                            HorasCurso = Convert.ToInt32(r["HorasCurso"]),
                            FechaInicial = Convert.ToDateTime(r["FechaInicial"]),
                            FechaFinal = Convert.ToDateTime(r["FechaFinal"]),
                            Observaciones = r["Observaciones"].ToString(),
                            TieneComite = Convert.ToBoolean(r["TieneComite"]),
                            ObservacionesComite = r["ObservacionesComite"].ToString(),
                            BienCustodia = r["BienCustodia"].ToString(),
                            FechaComite = Convert.ToDateTime(r["FechaComite"]),
                            Viable = Convert.ToBoolean(r["Viable"]),
                            Estatus = new Estatus()
                            {
                                Identificador = Convert.ToInt32(r["IdEstatus"]),
                                Nombre = r["EstatusServicio"].ToString()
                            },
                            Instalaciones = dtInstalaciones.AsEnumerable().Where(w => w["IdServicio"] == r["IdServicio"]).Select(i => new ServicioInstalacion()
                            {
                                Identificador = Convert.ToInt32(i["IdInstalacion"]),
                                Nombre = i["Nombre"].ToString(),
                                IdCliente = Convert.ToInt32(i["IdCliente"]),
                                Zona = new Zona()
                                {
                                    Identificador = Convert.ToInt32(i["IdZona"]),
                                    Nombre = i["Zona"].ToString(),
                                    Vigente = Convert.ToBoolean(i["Vigente"])
                                },
                                Estacion = new Estacion()
                                {
                                    Identificador = Convert.ToInt32(i["IdEstacion"]),
                                    Nombre = i["Estacion"].ToString()
                                },
                                Asentamiento = new Asentamiento()
                                {
                                    Municipio = new Municipio()
                                    {
                                        Identificador = Convert.ToInt32(i["IdMunicipio"]),
                                        Nombre = i["Municipio"].ToString()
                                    },
                                    Estado = new Estado()
                                    {
                                        Identificador = Convert.ToInt32(i["IdEntidad"]),
                                        Nombre = i["Entidad"].ToString()
                                    }
                                },
                                Activo = Convert.ToBoolean(i["Activo"])
                            }).ToList()
                        }).ToList()
                    }).ToList();

                    result = resultado;
                }
                return result;
            }
        }

        public IEnumerable<Servicio> ObtenerServiciosPorIdCliente(long identificador)
        {
            var result = new List<Servicio>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.ServiciosObtenerPorIdCliente;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = identificador, ParameterName = "@IdCliente" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Servicio servicio = new Servicio();

                        servicio.Identificador = reader["IdServicio"] as int? ?? 0;
                        servicio.IdSolicitud = Convert.ToInt32(reader["IdSolicitud"]);
                        servicio.TipoServicio = new TipoServicio
                        {
                            Identificador = reader["IdTipoServicio"] as int? ?? 0,
                            Nombre = reader["Nombre"] as string ?? "",
                            Clave = reader["Clave"] as string ?? "",
                            Activo = reader["Activo"] as bool? ?? false,
                        };
                        servicio.Cuota = new Cuota { Identificador = reader["IdCuota"] as int? ?? 0 };
                        servicio.TipoInstalacionesCapacitacion = new TipoInstalacionesCapacitacion { Identificador = reader["IdTipoInstalacion"] as int? ?? 0 };
                        servicio.NumeroPersonas = reader["NumeroPersonas"] as int? ?? 0;
                        servicio.HorasCurso = reader["HorasCurso"] as int? ?? 0;
                        servicio.FechaInicio = reader["FechaInicial"] as DateTime? ?? DateTime.Now;
                        servicio.FechaFinal = reader["FechaFinal"] as DateTime? ?? DateTime.Now;
                        servicio.FechaFin = reader["FechaFinal"] as DateTime? ?? DateTime.Now;
                        servicio.FechaComite = reader["FechaComite"] as DateTime? ?? DateTime.Now;
                        servicio.Observaciones = reader["Observaciones"] as string ?? "";
                        servicio.TieneComite = reader["TieneComite"] as bool? ?? false;
                        servicio.ObservacionesComite = reader["ObservacionesComite"] as string ?? "";

                        result.Add(servicio);

                    }
                }
            }

            return result;
        }

        public IEnumerable<ServicioDocumento> ObtenerDocumentosPorIdCliente(long identificador)
        {
            var result = new List<ServicioDocumento>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.ServicioDocumentosObtenerPorIdCliente;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = identificador, ParameterName = "@IdCliente" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ServicioDocumento documento = new ServicioDocumento();

                        documento.Identificador = reader["IdServicioDocumento"] as int? ?? 0;
                        documento.IdServicio = reader["IdServicio"] as int? ?? 0;
                        documento.TipoDocumento = new TipoDocumento { Identificador = reader["IdTipoDocumento"] as int? ?? 0 };
                        documento.Nombre = reader["Nombre"] as string ?? "";
                        documento.ArchivoIdParent = reader["DocumentoSoporte"] as int? ?? 0;
                        documento.FechaRegistro = reader["FechaRegistro"] as DateTime? ?? DateTime.Now;
                        documento.Observaciones = reader["Observaciones"] as string ?? "";

                        result.Add(documento);

                    }
                }
            }

            return result;
        }

        public Solicitud ObtenerPorId(long Identificador)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.SolicitudesObtenerPorId;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter()
                {
                    Value = Identificador,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);
                Solicitud result = new Solicitud();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = new Solicitud()
                        {
                            Identificador = Convert.ToInt32(reader["IdSolicitud"]),
                            Cliente = new Cliente()
                            {
                                Identificador = Convert.ToInt32(reader["IdCliente"]),
                                RazonSocial = Convert.ToString(reader["RazonSocial"]),
                                NombreCorto = Convert.ToString(reader["NombreCorto"]),
                                Rfc = Convert.ToString(reader["RFC"]),
                                RegimenFiscal = new RegimenFiscal()
                                {
                                    Identificador = Convert.ToInt32(reader["IdRegimenFiscal"]),
                                    Nombre = reader["RegimenFiscal"].ToString()
                                },
                                Sector = new Sector()
                                {
                                    Identificador = Convert.ToInt32(reader["IdSector"]),
                                    Descripcion = reader["Sector"].ToString()
                                }
                            },
                            TipoSolicitud = new TipoSolicitud()
                            {
                                Identificador = Convert.ToInt32(reader["IdTipoSolicitud"]),
                                Nombre = Convert.ToString(reader["TipoSolicitud"])
                            },
                            DocumentoSoporte = Convert.ToInt32(reader["DocumentoSoporte"]),
                            Folio = Convert.ToInt32(reader["Folio"]),
                            FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"])
                        };

                        if (reader["Minuta"] != DBNull.Value)
                            result.Minuta = Convert.ToInt32(reader["Minuta"]);

                        if (reader["Cancelado"] != DBNull.Value)
                            result.Cancelado = Convert.ToBoolean(reader["Cancelado"]);
                    }
                    return result;
                }
            }
        }

        public IEnumerable<Solicitud> ObtenerSolicitudesServicios(IPaging paging)
        {
            var result = new List<Solicitud>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.SolicitudServiciosObtener;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter { Value = paging.All, ParameterName = "@Todos" });
                cmd.Parameters.Add(new SqlParameter { Value = paging.CurrentPage, ParameterName = "@Pagina" });
                cmd.Parameters.Add(new SqlParameter { Value = paging.Pages, ParameterName = "@Filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var solicitud = SolicitudConsultaServicio(reader);
                        result.Add(solicitud);
                    }
                }
            }

            return result;
        }

        private static Solicitud SolicitudConsultaServicio(IDataRecord reader)
        {
            return new Solicitud
            {
                Identificador = reader["IdSolicitud"] as int? ?? 0,
                Cliente = new Cliente
                {
                    NombreCorto = reader["NombreCorto"].ToString(),
                    Rfc = reader["RFC"].ToString()
                },
                Servicio = new Servicio
                {
                    Identificador = reader["IdServicio"] as int? ?? 0,
                    TipoServicio = new TipoServicio
                    {
                        Identificador = reader["IdTipoServicio"] as int? ?? 0,
                        Nombre = reader["TipoServicio"].ToString()
                    }
                }
            };
        }

        public IEnumerable<Solicitud> ObtenerSolicitudesServiciosPorCriterio(IPaging paging, Solicitud criterioBusqueda)
        {
            var result = new List<Solicitud>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.SolicitudServiciosObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter { IsNullable = true, Value = paging.All, ParameterName = "@Todos" });
                cmd.Parameters.Add(new SqlParameter { IsNullable = true, Value = paging.CurrentPage, ParameterName = "@Pagina" });
                cmd.Parameters.Add(new SqlParameter { IsNullable = true, Value = paging.Pages, ParameterName = "@Filas" });
                cmd.Parameters.Add(new SqlParameter { IsNullable = true, Value = criterioBusqueda.Identificador, ParameterName = "@IdSolicitud" });
                cmd.Parameters.Add(new SqlParameter { IsNullable = true, Value = criterioBusqueda.Cliente.RazonSocial, ParameterName = "@RazonSocial" });
                cmd.Parameters.Add(new SqlParameter { IsNullable = true, Value = criterioBusqueda.Cliente.NombreCorto, ParameterName = "@NombreCorto" });
                cmd.Parameters.Add(new SqlParameter { IsNullable = true, Value = criterioBusqueda.Servicio.Estatus.Identificador, ParameterName = "@Estatus" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var solicitud = SolicitudConsultaServicio(reader);
                        result.Add(solicitud);
                    }
                }
            }

            return result;
        }
        #endregion

        #region métodos privados

        #endregion
    }
}
