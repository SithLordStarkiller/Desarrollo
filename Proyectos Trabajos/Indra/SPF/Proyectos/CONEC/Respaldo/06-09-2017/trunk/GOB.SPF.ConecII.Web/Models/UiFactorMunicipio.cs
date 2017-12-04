using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiFactorMunicipio:UiEntity
    {

        public UiFactorMunicipio()
        {
            Municipios = new List<UiMunicipio>();
        }

        public int Identificador { get; set; }

        [DisplayName("Factor:* ")]
        [Required(ErrorMessage ="Seleccione un factor")]
        public int IdFactor { get; set; }

        [DisplayName("Clasificación:* ")]
        [Required(ErrorMessage = "Seleccione una clasificación")]
        public int IdClasificacion { get; set; }

        [DisplayName("Estados:*")]
        [Required(ErrorMessage = "Seleccione un estado")]
        public int IdEstado { get; set; }

        public List<UiClasificacionFactor> Clasificaciones { get; set; }

        public List<UiFactor> Factores { get; set; }

        public List<UiEstado> Estados { get; set; }
        
        public List<UiMunicipio> Municipios { get; set; }

        public string Descripcion { get; set; }

    }
}