using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiGrupoTarifario: UiEntity
    {
        public int Identificador { get; set; }

        [DisplayName("Grupo Tarifario*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre del grupo es requerido")]
        public string Name { get; set; }
        public int Nivel { get; set; }

        [DisplayName("Descripción*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La descripción del grupo es requerido")]
        public string Descripcion { get; set; }
        public bool IsActive { get; set; }
    }
}