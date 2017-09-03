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
            try
            {
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
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /*
            ConfiguracionServicioActualizarConfiguraciones
        */


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


