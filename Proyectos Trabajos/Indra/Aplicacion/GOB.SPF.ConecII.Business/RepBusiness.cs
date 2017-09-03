using GOB.SPF.ConecII.DataAgents;
using GOB.SPF.ConecII.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Business
{
    public class RepBusiness
    {
        private int pages { get; set; }

        public int Pages { get { return pages; } }

        #region JERARQUIAS
        public List<Jerarquia> ObtenerJerarquias(Paging Paging)
        {
            Paging Paginado = new Paging();
            ServiceREP service = new ServiceREP();
            List<Jerarquia> list = service.ObtenerJerarquias(Paginado);
            return list;
        }
        #endregion

        #region AREAS
        public List<Area> ObtenerAreas()
        {
            Paging Paginado = new Paging();
            ServiceREP service = new ServiceREP();
            List<Area> list = service.ObtenerAreas(Paginado);
            return list;
        }
        #endregion

        #region INTEGRANTES
        public List<Integrante> ObtenerIntegrantes(Paging Paging)
        {
            Paging Paginado = new Paging();
            ServiceREP service = new ServiceREP();
            List<Integrante> list = service.ObtenerIntegrantes(Paginado);
            return list;
        }
        #endregion
    }
}
