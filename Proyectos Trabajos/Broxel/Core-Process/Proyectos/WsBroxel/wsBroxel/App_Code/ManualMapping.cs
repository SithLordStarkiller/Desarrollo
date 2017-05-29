
namespace wsBroxel
{
    using System;

    public partial class MaquilaResumen
    {
        public String NombreTarjetaHabiente { get; set; }
        public String NumeroTarjeta { get; set; }
        public String FechaExpira { get; set; }
        public String CVC { get; set; }
        public Int32 Id { get; set; }
        public Int32 Procesador { get; set; }
        public string NumCuenta { get; set; }
    }

    public partial class ManualMaq
    {
        public int Id { get; set; }
        public String num_cuenta { get; set; }
    }

    public partial class ConteoODT
    {
        public int total { get; set; }
        public int terminadas { get; set; }
    }

}
