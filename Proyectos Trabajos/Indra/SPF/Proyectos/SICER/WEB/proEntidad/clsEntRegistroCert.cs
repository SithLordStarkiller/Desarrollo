using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace proEntidad
{
    public class clsEntRegistroCert
    {
        public int idRegistro { get; set; }
        public int idCertificacion { get; set; }
        public int idCertificacionRegistro { get; set; }
        public DateTime crFechaExamen { get; set; }
        public DateTime crFechaModificacion { get; set; }
        public TimeSpan crHora { get; set; }
        public short idEvaluador { get; set; }
        public bool crVigente { get; set; }
        public short idInstitucionAplicaExamen { get; set; }
        public byte idLugarAplica { get; set; }
        public short idNivelSeguridad { get; set; }
        public short idDependenciaExterna { get; set; }
        public short idInstitucionExterna { get; set; }
        public int idUsuario { get; set; }
        public bool inserto { get; set; }
        public short idZona { get; set; }
        public int idServicio { get; set; }
        public int idInstalacion { get; set; }
    }
}
