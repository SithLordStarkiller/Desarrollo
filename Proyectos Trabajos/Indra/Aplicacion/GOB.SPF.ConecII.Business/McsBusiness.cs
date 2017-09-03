using GOB.SPF.ConecII.DataAgents;
using GOB.SPF.ConecII.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        #endregion

        public List<Municipio> ObtenerMunipios(Estado estado)
        {
            ServiceMCS service = new ServiceMCS();
            List<Municipio> list = service.ObtenerMunicipios(estado);
            return list;
        }

        #region TARIFARIO

        public List<GrupoTarifario> ObtenerTarifario(Paging Paging)
        {
            ServiceMCS service = new ServiceMCS();
            List<GrupoTarifario> list = service.ObtenerTarifario(Paging);
            return list;
        }
        #endregion
    }
}
