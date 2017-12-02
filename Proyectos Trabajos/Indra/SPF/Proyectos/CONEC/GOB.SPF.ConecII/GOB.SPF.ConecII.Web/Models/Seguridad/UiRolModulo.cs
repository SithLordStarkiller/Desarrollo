using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GOB.SPF.ConecII.Interfaces;

namespace GOB.SPF.ConecII.Web.Models.Seguridad
{
    public class UiRolModulo : UiEntity, IRolModulo
    {
        public int Id { get; set; }
        public IRol Rol { get; set; }
        public IModulo Modulo { get; set; }
        public bool Activo { get; set; }
        public DateTime? FechaFinal { get; set; }
        public DateTime FechaInicial { get; set; }
    }
}