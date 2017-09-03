using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiIntegrante : UiEntity
    {
        public int Identificador { get; set; }
        public string Nombre { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public string Correo { get; set; }
        public string CorreoPersonal { get; set; }
        public string IdArea { get; set; }
        public string Area { get; set; }
        public int IdJerarquia { get; set; }
        public string Jerarquia { get; set; }
        //public bool IsActive { get; set; } 
        public bool IsActive = true;

        public string NombreCompleto => Nombre + " " + ApPaterno + " " + ApMaterno;

        public string CorreoTrabajo => !string.IsNullOrEmpty(Correo.Trim())? Correo.Trim() + "@CNS.GOB.MX" : "Sin Correo";
    }
}