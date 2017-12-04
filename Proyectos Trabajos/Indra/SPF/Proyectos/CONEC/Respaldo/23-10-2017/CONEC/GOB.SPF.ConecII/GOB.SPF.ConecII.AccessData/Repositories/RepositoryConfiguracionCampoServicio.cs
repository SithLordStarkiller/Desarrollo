using GOB.SPF.ConecII.AccessData;
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
    public class RepositoryConfiguracionCampoServicio : IRepository<ConfiguracionCampoServicio>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RepositoryConfiguracionCampoServicio(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;
            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public int Actualizar(ConfiguracionCampoServicio entity)
        {
            throw new NotImplementedException();
        }

        public int CambiarEstatus(ConfiguracionCampoServicio entity)
        {
            throw new NotImplementedException();
        }

        public int Insertar(ConfiguracionCampoServicio entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ConfiguracionCampoServicio> Obtener(IPaging paging)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ConfiguracionCampoServicio> ObtenerPorCriterio(IPaging paging, ConfiguracionCampoServicio entity)
        {
            throw new NotImplementedException();
        }

        public ConfiguracionCampoServicio ObtenerPorId(long Identificador)
        {
            throw new NotImplementedException();
        }

        public ConfiguracionCampoServicio ObtenerPorId(int Identificador)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Configuracion.ConfiguracionCampoServicioObtenerPorId;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { Value = Identificador, ParameterName = "@IdTipoServicio" });
                var configuracion = new ConfiguracionCampoServicio();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        configuracion = LeerConfiguracion(reader);
                    }
                    return configuracion;
                }
            }
        }

        private ConfiguracionCampoServicio LeerConfiguracion(IDataReader reader)
        {
            var configuracion = new ConfiguracionCampoServicio();

            configuracion.Identificador = Convert.ToInt32(reader["IdCampoServicio"]);
            configuracion.IdTipoServicio = Convert.ToInt32(reader["IdTipoServicio"]);
            configuracion.Cuota = (bool) reader["Cuota"];
            configuracion.TipoInstalacion = (bool) reader["TipoInstalacion"];
            configuracion.NumeroPersonas = (bool) reader["NumeroPersonas"];
            configuracion.HorasCurso = (bool) reader["HorasCurso"];
            configuracion.FechaInicial = (bool) reader["FechaInicial"];
            configuracion.FechaFinal = (bool) reader["FechaFinal"];
            configuracion.BienCustodia = (bool) reader["BienCustodia"];
            configuracion.Observaciones = (bool) reader["Observaciones"];
            configuracion.InstalacionesCliente = (bool) reader["InstalacionesCliente"];
            configuracion.Documentos = (bool) reader["Documentos"];
            configuracion.Viable = (bool) reader["Viable"];
            configuracion.TieneComite = (bool) reader["TieneComite"];
            configuracion.FechaComite = (bool) reader["FechaComite"];
            configuracion.ObservacionesComite = (bool) reader["ObservacionesComite"];
            configuracion.InstalacionesClienteMultiple = (bool) reader["InstalacionesClienteMultiple"];
            configuracion.PeriodoCapacitacion = (bool) reader["PeriodoCapacitacion"];
            configuracion.EstadoDeFuerza = (bool) reader["EstadoDeFuerza"];
            configuracion.Signatarios = (bool) reader["Signatarios"];
            configuracion.CuotaInstalacion = (bool) reader["CuotaInstalacion"];
            configuracion.EstacionMonitoreoLocal = (bool) reader["EstacionMonitoreoLocal"];

            return configuracion;
        }
    }
}
