using System.ComponentModel;

namespace GOB.SPF.ConecII.Web.Models.Solicitud
{
    public class UiEstadoDeFuerza
    {
        [DisplayName("H")]
        public int? Hombres { get; set; }
        [DisplayName("M")]
        public int? Mujeres { get; set; }
        [DisplayName("I")]
        public int? Indistinto { get; set; }
        [DisplayName("AC")]
        public int? ArmasCortas { get; set; }
        [DisplayName("AL")]
        public int? ArmasLargas { get; set; }
        [DisplayName("M")]
        public int? Municiones { get; set; }
        [DisplayName("U")]
        public int? Uniformes { get; set; }
        [DisplayName("VG")]
        public int? VestuarioGala { get; set; }
        [DisplayName("VMG")]
        public int? VestuarioMediaGala { get; set; }
        [DisplayName("T")]
        public int? EquipoTactico { get; set; }
        [DisplayName("A")]
        public int? EquipoAntomotines { get; set; }
        [DisplayName("TS")]
        public int? Taser { get; set; }
        [DisplayName("R")]
        public int? Radios { get; set; }
        [DisplayName("A")]
        public int? Antenas { get; set; }
    }
}