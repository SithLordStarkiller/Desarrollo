using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiTiposPagoActividades
    {
        public int IdTipoPagoActividad { set; get; }
        public UiConfiguracionTipoServicioArea ConfiguracionTipoServicioArea { set; get; }
        public UiTiposPago TiposPago { set; get; }
        public UiActividad Actividad { set; get; }
        public bool Aplica { set; get; }
        public int Tiempo { set; get; }
        public UiTiposPagoActividades()
        {
            ConfiguracionTipoServicioArea = new UiConfiguracionTipoServicioArea();
            TiposPago = new UiTiposPago();
            Actividad = new UiActividad();
        }
    }
}