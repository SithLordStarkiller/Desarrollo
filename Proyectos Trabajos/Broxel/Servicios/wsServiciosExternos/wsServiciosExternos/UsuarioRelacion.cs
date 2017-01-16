using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wsServiciosExternos
{
    public class UsuarioRelacion
    {
        [Key]
        public String UsuarioGestor { get; set; }
        public String Gestor { get; set; }
        public String Supervisor { get; set; }
        public String Administrador { get; set; }
        public String Despacho { get; set; }
        }

    public enum TipoConsulta
    {
        Gestor=1,
        Supervisor=2,
        Administrador=3,
        Despacho=4,
        General=5
    }
}