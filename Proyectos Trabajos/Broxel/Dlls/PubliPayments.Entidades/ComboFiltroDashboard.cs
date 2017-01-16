using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PubliPayments.Entidades
{
    public class ComboFiltroDashboard
    {
        private readonly FiltrosDashboard _filtro;
        private readonly String _accion ="";


        public ComboFiltroDashboard(FiltrosDashboard filtrosDash, String accion)
        {
            switch (accion)
            {
                case "FILL_DELEGACION":
                    _filtro = new FiltrosDashboard { Despacho = "%", Delegacion = "%", Estado = "%", Gestor = "%", Supervisor = "%" };
                    break;
                case "FILL_DESPACHO":
                    _filtro= new FiltrosDashboard{Despacho = "%",Delegacion = filtrosDash.Delegacion,Estado = filtrosDash.Estado,Gestor = "%",Supervisor = "%"};
                    break;
                case "FILL_SUPERVISOR":
                    _filtro= new FiltrosDashboard{Despacho =filtrosDash.Despacho ,Delegacion =filtrosDash.Delegacion,Estado =filtrosDash.Estado ,Gestor ="%" ,Supervisor = "%"};
                    break;
                case "FILL_GESTOR":
                    _filtro= new FiltrosDashboard {Despacho = filtrosDash.Despacho,Delegacion =filtrosDash.Delegacion,Estado = filtrosDash.Estado,Gestor = "%",Supervisor = filtrosDash.Supervisor};
                    break;
            }

            _accion = accion;
        }

        public List<OpcionesFiltroDashboard> SelectFiltro()
        {
            var context = new SistemasCobranzaEntities();
            
            var lista = from e in context.ObtenerListasDashboard(_accion,null,null,null,null,null,null,_filtro.Delegacion,_filtro.Despacho,_filtro.Estado,_filtro.Supervisor,_filtro.Gestor)
                select e;

            var listaFinal =
                lista.Select(hijo => new OpcionesFiltroDashboard {Description = hijo.Description.ToString(CultureInfo.InvariantCulture), Value = hijo.Value.ToString(CultureInfo.InvariantCulture)})
                    .ToList();

            return listaFinal;
        }

    }
}