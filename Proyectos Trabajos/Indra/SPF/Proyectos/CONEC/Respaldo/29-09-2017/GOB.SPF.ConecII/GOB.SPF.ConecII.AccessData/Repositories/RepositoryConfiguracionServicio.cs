using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.AccessData.Repositories
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;
    using Entities.DTO;

    public class RepositoryConfiguracionServicio : IRepository<ConfiguracionServicio>
    {
        private int pages { get; set; }
        public int Pages { get { return pages; } }

        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryConfiguracionServicio(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public IEnumerable<ConfiguracionServicio> ObtenerPorCriterio(Paging paging, ConfiguracionServicio entity)
        {
            return new List<ConfiguracionServicio>();
        }
        public IEnumerable<ConfiguracionServicioDTO> ObtenerConfiguracionesPorIdTipoServicioIdCentroCosto(int idTipoServicio, string idCentroCosto)
        {
            var result = new List<ConfiguracionServicioDTO>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = ConfiguracionServicios.ConfiguracionServicioObtenerIdTipoServicioIdCentroCosto;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = idTipoServicio, ParameterName = "@IdTipoServicio" });
                cmd.Parameters.Add(new SqlParameter() { Value = idCentroCosto, ParameterName = "@IdCentroCosto" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ConfiguracionServicioDTO configuracionServicio = new ConfiguracionServicioDTO();
                        configuracionServicio.IdTipoServicio = Convert.ToInt32(reader["IdTipoServicio"]);
                        configuracionServicio.IdCentroCostos = reader["IdCentroCosto"].ToString();
                        configuracionServicio.IdRegimenFiscal = Convert.ToInt32(reader["IdRegimenFiscal"]);
                        configuracionServicio.IdTipoPago = Convert.ToInt32(reader["IdTipoPago"]);

                        if (reader["IdActividad"] != null)
                            configuracionServicio.IdActividad = Convert.ToInt32(reader["IdActividad"]);

                        if (reader["IdTipoDocumento"] != null)
                            configuracionServicio.IdTipoDocumento = Convert.ToInt32(reader["IdTipoDocumento"]);

                        if (reader["Tiempo"] != null)
                            configuracionServicio.Tiempo = Convert.ToDecimal(reader["Tiempo"]);

                        if (reader["Aplica"] != null)
                            configuracionServicio.Aplica = Convert.ToBoolean(reader["Aplica"]);

                        if (reader["Obligatoriedad"] != null)
                            configuracionServicio.Obigatoriedad = Convert.ToBoolean(reader["Obligatoriedad"]);

                        configuracionServicio.TipoServicio = reader["TipoServicio"].ToString();
                        configuracionServicio.CentroCostos = reader["CentroCosto"].ToString();
                        configuracionServicio.RegimenFiscal = reader["TipoRegimenFiscal"].ToString();
                        configuracionServicio.TipoPago = reader["TipoPago"].ToString();
                        configuracionServicio.Activad = reader["Actividad"].ToString();
                        configuracionServicio.TipoDocumento = reader["TipoDocumento"].ToString();

                        result.Add(configuracionServicio);
                    }
                }
                return result;  // yield?
            }

        }
        public IEnumerable<ConfiguracionServicioDTO> ObtenerConfiguracionPorIdConfiguracion(long id)
        {
            var result = new List<ConfiguracionServicioDTO>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = ConfiguracionServicios.ConfiguracionServicioObtenerPorIdConfServicio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = id, ParameterName = "@Identificador" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ConfiguracionServicioDTO configuracionServicio = new ConfiguracionServicioDTO();
                        configuracionServicio.IdTipoServicio = Convert.ToInt32(reader["IdTipoServicio"]);
                        configuracionServicio.IdCentroCostos = reader["IdCentroCosto"].ToString();
                        configuracionServicio.IdRegimenFiscal = Convert.ToInt32(reader["IdRegimenFiscal"]);
                        configuracionServicio.IdTipoPago = Convert.ToInt32(reader["IdTipoPago"]);

                        if (reader["IdActividad"] != null)
                            configuracionServicio.IdActividad = Convert.ToInt32(reader["IdActividad"]);

                        if (reader["IdTipoDocumento"] != null)
                            configuracionServicio.IdTipoDocumento = Convert.ToInt32(reader["IdTipoDocumento"]);

                        if (reader["Tiempo"] != null)
                            configuracionServicio.Tiempo = Convert.ToDecimal(reader["Tiempo"]);

                        if (reader["Aplica"] != null)
                            configuracionServicio.Aplica = Convert.ToBoolean(reader["Aplica"]);

                        if (reader["Obligatoriedad"] != null)
                            configuracionServicio.Obigatoriedad = Convert.ToBoolean(reader["Obligatoriedad"]);

                        configuracionServicio.TipoServicio = reader["TipoServicio"].ToString();
                        configuracionServicio.CentroCostos = reader["CentroCosto"].ToString();
                        configuracionServicio.RegimenFiscal = reader["TipoRegimenFiscal"].ToString();
                        configuracionServicio.TipoPago = reader["TipoPago"].ToString();
                        configuracionServicio.Activad = reader["Actividad"].ToString();
                        configuracionServicio.TipoDocumento = reader["TipoDocumento"].ToString();

                        result.Add(configuracionServicio);
                    }
                }
                return result;  // yield?
            }
        }
        public IEnumerable<ConfiguracionServicioDTO> ObtenerPaginado(Paging paging)
        {
            var result = new List<ConfiguracionServicioDTO>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = ConfiguracionServicios.ConfiguracionServicioObtenerTodosPaginados;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ConfiguracionServicioDTO configuracionServicio = new ConfiguracionServicioDTO();
                        configuracionServicio.IdTipoServicio = Convert.ToInt32(reader["IdTipoServicio"]);
                        configuracionServicio.IdCentroCostos = reader["IdCentroCosto"].ToString();
                        configuracionServicio.IdRegimenFiscal = Convert.ToInt32(reader["IdRegimenFiscal"]);
                        configuracionServicio.IdTipoPago = Convert.ToInt32(reader["IdTipoPago"]);

                        if (reader["IdActividad"] != null)
                            configuracionServicio.IdActividad = Convert.ToInt32(reader["IdActividad"]);

                        if (reader["IdTipoDocumento"] != null)
                            configuracionServicio.IdTipoDocumento = Convert.ToInt32(reader["IdTipoDocumento"]);

                        if (reader["Tiempo"] != null)
                            configuracionServicio.Tiempo = Convert.ToDecimal(reader["Tiempo"]);

                        if (reader["Aplica"] != null)
                            configuracionServicio.Aplica = Convert.ToBoolean(reader["Aplica"]);

                        if (reader["Obligatoriedad"] != null)
                            configuracionServicio.Obigatoriedad = Convert.ToBoolean(reader["Obligatoriedad"]);

                        configuracionServicio.TipoServicio = reader["TipoServicio"].ToString();
                        configuracionServicio.CentroCostos = reader["CentroCosto"].ToString();
                        configuracionServicio.RegimenFiscal = reader["TipoRegimenFiscal"].ToString();
                        configuracionServicio.TipoPago = reader["TipoPago"].ToString();
                        configuracionServicio.Activad = reader["Actividad"].ToString();
                        configuracionServicio.TipoDocumento = reader["TipoDocumento"].ToString();


                        //pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(configuracionServicio);
                    }
                }
                return result;  // yield?
            }

        }
        public int InsertarConfiguraciones(IEnumerable<ConfiguracionServicio> configuraciones)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                DataTable dt = ConversorEntityDatatable.TransformarADatatable(configuraciones);
                cmd.CommandText = ConfiguracionServicios.ConfiguracionServicioInsertarConfiguraciones;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter();
                parameters[0].ParameterName = ConfiguracionServicios.ConfiguracionServicioParametro01InsertarConf;
                parameters[0].SqlDbType = SqlDbType.Structured;
                parameters[0].TypeName = ConfiguracionServicios.ConfiguracionServicioTipoTablaUsuario;
                parameters[0].Value = dt;
                cmd.Parameters.Add(parameters[0]);

                System.Data.IDataReader dr = cmd.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        int identity = Convert.ToInt32(dr.GetValue(0));
                        string mensaje = dr.GetValue(1).ToString();

                        if (identity == 0)
                        {
                            dr.Close();
                            result = identity;
                            throw new System.Exception(mensaje);
                        }

                    }

                    dr.Close();
                }
                catch (Exception ex)
                {
                    dr.Close();
                    throw ex;
                }
            }


            return result;
        }
        public IEnumerable<DetalleConfiguracionServicio> DetalleConfiguracion(int page, int rows)
        {
            List<DetalleConfiguracionServicio> result = new List<DetalleConfiguracionServicio>();
            try
            {
                using (var cmd = _unitOfWork.CreateCommand())
                {
                    cmd.CommandText = ConfiguracionServicios.ConfiguracionServicioDetalle;
                    cmd.CommandType = CommandType.StoredProcedure;

                    //SqlParameter[] parameters = new SqlParameter[1];
                    //parameters[0] = new SqlParameter();
                    //parameters[0].ParameterName = ConfiguracionServicios.ConfiguracionServicioParametro01InsertarConf;
                    //parameters[0].SqlDbType = SqlDbType.Structured;
                    //parameters[0].TypeName = ConfiguracionServicios.ConfiguracionServicioTipoTablaUsuario;
                    //parameters[0].Value = dt;
                    //cmd.Parameters.Add(parameters[0]);

                    System.Data.IDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(new DetalleConfiguracionServicio()
                        {
                            Estatus = Convert.ToBoolean(reader["Activo"]),
                            FechaRegistro = DateTime.Now,
                            IdRegimenFiscal = Convert.ToInt32(reader["IdRegimenFiscal"]),
                            IdTipoPago = Convert.ToInt32(reader["IdTipoPago"]),
                            IdTipoServicio = Convert.ToInt32(reader["IdTipoServicio"]),
                            RegimenFiscal = reader["RegimenFiscal"].ToString(),
                            TipoPago = reader["TipoPago"].ToString(),
                            TipoServicio = reader["TipoServicio"].ToString(),
                            IdCentroCosto=Convert.ToInt32(reader["IdCentroCosto"])
                        });                        
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public ConfiguracionTipoServicioArea ObtenerConfiguracionTipoServicioArea(int idTipoServicio, string idCentroCosto)
        {
            ConfiguracionTipoServicioArea confTipSerArea = new ConfiguracionTipoServicioArea();

            List<TiposRegimenFiscalTiposPago> listRegimenFiscalTipoPago = new List<TiposRegimenFiscalTiposPago>();
            List<TiposPagoActividades> listTipoPagoActividad = new List<TiposPagoActividades>();
            List<ActividadesTiposDocumento> listActividadTipoDocumento = new List<ActividadesTiposDocumento>();

            var dtConfiguracionTipoServicioArea = new DataTable();
            var dtTipoServicioArea = new DataTable();
            var dtTiposRegimenFiscalTiposPago = new DataTable();
            var dtTiposPagoActividades = new DataTable();
            var dtActividadesTiposDocumento = new DataTable();

            using (var cmd = _unitOfWork.CreateCommand())
            {

                cmd.CommandText = ConfiguracionServicios.ConfiguracionServicioAreaObtenerIdTipoServicioIdArea;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = idTipoServicio, ParameterName = "@IdTipoServicio" });
                cmd.Parameters.Add(new SqlParameter() { Value = idCentroCosto, ParameterName = "@IdCentroCosto" });

                using (var reader = cmd.ExecuteReader())
                {
                    dtConfiguracionTipoServicioArea.Load(reader);

                    if (dtConfiguracionTipoServicioArea.Rows != null & 
                            dtConfiguracionTipoServicioArea.Rows.Count>0)
                    {
                        
                        DataView dvServicioArea = new DataView(dtConfiguracionTipoServicioArea);
                        dtTipoServicioArea = dvServicioArea.ToTable(true,   "IdConfServicio", "IdTipoServicio", 
                                                                            "IdCentroCosto", "TipoServicioAreaActivo", 
                                                                            "CentroCosto", "TipoServicio");
                        
                        if (dtTipoServicioArea.Rows != null && dtTipoServicioArea.Rows.Count > 0)
                        {
                            #region CONFG_TIPOSERVICIO_AREA
                            foreach (DataRow row in dtTipoServicioArea.Rows)
                            {
                                confTipSerArea.Identificador = Convert.ToInt32(row["IdConfServicio"]);
                                confTipSerArea.Area = new Area()
                                {
                                    IdCentroCosto = row["IdCentroCosto"].ToString(),
                                    Nombre = row["CentroCosto"].ToString()
                                };
                                confTipSerArea.TipoServicio = new TipoServicio()
                                {
                                    Identificador = Convert.ToInt32(row["IdTipoServicio"]),
                                    Nombre = row["TipoServicio"].ToString()
                                };
                            }

                            #endregion CONFG_TIPOSERVICIO_AREA

                            #region CONFG_REGIMENFISCAL_TIPOPAGO

                            DataView dvRegimenFiscalTiposPago = new DataView(dtConfiguracionTipoServicioArea);
                            dtTiposRegimenFiscalTiposPago = dvServicioArea.ToTable(true, "IdTipoRegimenFiscalTipoPago", "IdRegimenFiscal",
                                                                            "TipoRegimenFiscalTipoPagoIdTipoPago", "TipoRegimenFiscalTipoPagoAplica",
                                                                            "TipoRegimenFiscalTipoPagoRegimenFiscal", "TipoRegimenFiscalTipoPagoTipoPago");

                            if (dtTiposRegimenFiscalTiposPago.Rows != null && dtTiposRegimenFiscalTiposPago.Rows.Count > 0)
                            {
                                foreach (DataRow row in dtTiposRegimenFiscalTiposPago.Rows)
                                {
                                    TiposRegimenFiscalTiposPago regimenTipoPago = new TiposRegimenFiscalTiposPago();

                                    regimenTipoPago.Identificador = Convert.ToInt32(row["IdTipoRegimenFiscalTipoPago"]);
                                    regimenTipoPago.Aplica = Convert.ToBoolean(row["TipoRegimenFiscalTipoPagoAplica"]);
                                    regimenTipoPago.ConfiguracionTipoServiciosArea = confTipSerArea;
                                    regimenTipoPago.RegimenFiscal = new RegimenFiscal()
                                    {
                                        Identificador = Convert.ToInt32(row["IdRegimenFiscal"]),
                                        Nombre = row["TipoRegimenFiscalTipoPagoRegimenFiscal"].ToString()
                                    };
                                    regimenTipoPago.TiposPago = new TiposPago()
                                    {
                                        Identificador = Convert.ToInt32(row["TipoRegimenFiscalTipoPagoIdTipoPago"]),
                                        Nombre = row["TipoRegimenFiscalTipoPagoTipoPago"].ToString()
                                    };

                                    listRegimenFiscalTipoPago.Add(regimenTipoPago);
                                }

                            }

                            #endregion CONFG_REGIMENFISCAL_TIPOPAGO

                            #region CONFG_TIPOPAGO_ACTIVIDAD
                            DataView dvTiposPagoActividades = new DataView(dtConfiguracionTipoServicioArea);
                            dtTiposPagoActividades = dvTiposPagoActividades.ToTable(true, "IdTipoPagoActividad", "TipoPagoActividadIdTipoPago",
                                                                            "TipoPagoActividadIdActividad", "TipoPagoActividadAplica", "TipoPagoActividadTiempo",
                                                                            "TipoPagoActividadTipoPago", "TipoPagoActividadActividad");

                            if (dtTiposPagoActividades.Rows != null && dtTiposPagoActividades.Rows.Count > 0)
                            {
                                foreach (DataRow row in dtTiposPagoActividades.Rows)
                                {
                                    TiposPagoActividades tipoPagoActividad = new TiposPagoActividades();

                                    tipoPagoActividad.Identificador = Convert.ToInt32(row["IdTipoPagoActividad"]);
                                    tipoPagoActividad.Aplica = Convert.ToBoolean(row["TipoPagoActividadAplica"]);
                                    tipoPagoActividad.Tiempo = Convert.ToInt32(row["TipoPagoActividadTiempo"]);
                                    tipoPagoActividad.ConfiguracionTipoServicioArea = confTipSerArea;
                                    tipoPagoActividad.Actividad = new Actividad()
                                    {
                                        Identificador = Convert.ToInt32(row["TipoPagoActividadIdActividad"]),
                                        Nombre = row["TipoPagoActividadActividad"].ToString()
                                    };
                                    tipoPagoActividad.TiposPago = new TiposPago()
                                    {
                                        Identificador = Convert.ToInt32(row["TipoPagoActividadIdTipoPago"]),
                                        Nombre = row["TipoPagoActividadTipoPago"].ToString()
                                    };

                                    listTipoPagoActividad.Add(tipoPagoActividad);
                                }

                            }


                            #endregion CONFG_TIPOPAGO_ACTIVIDAD

                            #region CONFG_ACTIVIDAD_TIPODOCUMENTO

                            DataView dvActividadesTiposDocumento = new DataView(dtConfiguracionTipoServicioArea);
                            dtActividadesTiposDocumento = dvActividadesTiposDocumento.ToTable(true, "IdActividadTipoDocumento", 
                                                                                            "ActividadesTiposDocumentoIdActividad",
                                                                                            "ActividadesTiposDocumentoIdTipoDocumento", 
                                                                                            "ActividadesTiposDocumentoAplica", 
                                                                                            "ActividadesTiposDocumentoObligatoriedad",
                                                                                            "ActividadesTiposDocumentoActividad",
                                                                                            "ActividadesTiposDocumentoTipoDocumento");

                            if (dtActividadesTiposDocumento.Rows != null && dtActividadesTiposDocumento.Rows.Count > 0)
                            {
                                foreach (DataRow row in dtActividadesTiposDocumento.Rows)
                                {
                                    ActividadesTiposDocumento actividadTipoPago = new ActividadesTiposDocumento();

                                    actividadTipoPago.Identificador = Convert.ToInt32(row["IdActividadTipoDocumento"]);
                                    actividadTipoPago.Aplica = Convert.ToBoolean(row["ActividadesTiposDocumentoAplica"]);
                                    actividadTipoPago.Obligatoriedad = Convert.ToBoolean(row["ActividadesTiposDocumentoObligatoriedad"]);
                                    actividadTipoPago.ConfiguracionTipoServicioArea = confTipSerArea;
                                    actividadTipoPago.Actividad = new Actividad()
                                    {
                                        Identificador = Convert.ToInt32(row["ActividadesTiposDocumentoIdActividad"]),
                                        Nombre = row["ActividadesTiposDocumentoActividad"].ToString()
                                    };
                                    actividadTipoPago.TipoDocumento= new TipoDocumento()
                                    {
                                        Identificador = Convert.ToInt32(row["ActividadesTiposDocumentoIdTipoDocumento"]),
                                        Nombre = row["ActividadesTiposDocumentoTipoDocumento"].ToString()
                                    };

                                    listActividadTipoDocumento.Add(actividadTipoPago);
                                }

                            }
                            #endregion CONFG_ACTIVIDAD_TIPODOCUMENTO


                            confTipSerArea.ListaTiposRegimenFiscalTiposPago = listRegimenFiscalTipoPago;
                            confTipSerArea.ListaTiposPagoActividades = listTipoPagoActividad;
                            confTipSerArea.ListaActividadesTiposDocumento = listActividadTipoDocumento;
                        }
                    }
                }
            }
                return confTipSerArea;
        }

        #region FUNCIONES_INTERFAZ
        public int CambiarEstatus(ConfiguracionServicio configuracionServicio)
        {
            return 0;
        }
        public int Insertar(ConfiguracionServicio configuracionServicio)
        {
            return 0;
        }
        public int Actualizar(ConfiguracionServicio configuracionServicio)
        {
            return 0;
        }

        int IRepository<ConfiguracionServicio>.Insertar(ConfiguracionServicio entity)
        {
            throw new NotImplementedException();
        }

        ConfiguracionServicio IRepository<ConfiguracionServicio>.ObtenerPorId(long Identificador)
        {
            throw new NotImplementedException();
        }

        int IRepository<ConfiguracionServicio>.CambiarEstatus(ConfiguracionServicio entity)
        {
            throw new NotImplementedException();
        }

        int IRepository<ConfiguracionServicio>.Actualizar(ConfiguracionServicio entity)
        {
            throw new NotImplementedException();
        }

        IEnumerable<ConfiguracionServicio> IRepository<ConfiguracionServicio>.Obtener(Paging paging)
        {
            throw new NotImplementedException();
        }

        IEnumerable<ConfiguracionServicio> IRepository<ConfiguracionServicio>.ObtenerPorCriterio(Paging paging, ConfiguracionServicio entity)
        {
            throw new NotImplementedException();
        }

        #endregion FUNCIONES_INTERFAZ



    }
}


