using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiAreasValidadoras : UiEntity
    {
        public int IdAreasValidadoras { get; set; }
        public int IdTipoServicio { get; set; }
        public string TipoServicio { get; set; }
        public int IdActividad { get; set; }
        public string Actividad { get; set; }
        public string IdCentroCosto { get; set; }
        [MaxLength(6)]
        public string CentroCostos { get; set; }
        public bool Obligatorio { get; set; }
        public bool EsActivo { get; set; }
    }
}