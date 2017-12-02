using GOB.SPF.ConecII.DataAgents;
using GOB.SPF.ConecII.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.Business
{
    public class McsBusiness
    {
        private int pages { get; set; }

        public int Pages { get { return pages; } }

        #region ESTADOS
        public List<Estado> ObtenerEstados()
        {
            ServiceMCS service = new ServiceMCS();
            List<Estado> list = service.ObtenerEstados();
            return list;
        }
        #endregion ESTADOS

        #region MUNICIPIOS
        public List<Municipio> ObtenerMunipios(Estado estado)
        {
            ServiceMCS service = new ServiceMCS();
            List<Municipio> list = service.ObtenerMunicipios(estado);
            return list;
        }

        #endregion  MUNICIPIOS

        #region ASENTAMIENTOS

        public List<Asentamiento> ObtenerAsentamientos(Asentamiento asentamiento)
        {
            var service = new ServiceMCS();
            var list = service.ObtenerAsentamientos(asentamiento);
            return list;
        }

        #endregion ASENTAMIENTOS

        #region TARIFARIO

        public List<GrupoTarifario> ObtenerTarifario(Paging Paging)
        {
            ServiceMCS service = new ServiceMCS();
            List<GrupoTarifario> list = service.ObtenerTarifario(Paging);
            return list;
        }
        #endregion TARIFARIO

        #region TIPOINSTALACION
        public List<TipoInstalacion> ObtenerTipoInstalacion()
        {
            var service = new ServiceMCS();
            var list = service.ObtenerTiposInstalacion();
            return list;
        }
        #endregion

        #region Estaciones

        public List<Estacion> ObtenerEstaciones()
        {
            var service = new ServiceMCS();
            var list = service.ObtenerEstaciones();
            return list;
        }

        #endregion

        #region Zonas

        public List<Zona> ObtenerZonas()
        {
            var service = new ServiceMCS();
            var list = service.ObtenerZonas();
            return list;
        }

        #endregion
    }
}
