using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiTipoInstalacion:UiEntity
    {
        [Key]
        public int Identificador { get; set; }

        [Required]
        [DisplayName("Tipo Instalación*:")]
        public string Nombre { get; set; }
    }
}