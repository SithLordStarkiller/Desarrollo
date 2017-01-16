using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TraceLogAnalyzer.Models
{
    public class TiempoModel
    {
        public string Origen { get; set; }
        [Key]
        public string GUID { get; set; }
        public string Inicio { get; set; }
        public string Fin { get; set; }
        public long Diferencia { get; set; }
    }
}