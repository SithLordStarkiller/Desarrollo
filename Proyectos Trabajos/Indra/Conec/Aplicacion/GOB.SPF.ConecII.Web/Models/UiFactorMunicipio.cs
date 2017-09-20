using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiFactorMunicipio : UiEntity
    {

        public UiFactorMunicipio()
        {
            Municipios = new List<UiMunicipio>();
        }

        public int Identificador { get; set; }

        [DisplayName("Factor:*")]
        [Required(ErrorMessage = "El factor es requerido")]
        public int IdFactor { get; set; }
        public string Factor { get; set; }

        [DisplayName("Clasificación:*")]
        [Required(ErrorMessage = "La clasificación factor es requerida")]
        public int IdClasificacionFactor { get; set; }
        public string ClasificadorFactor { get; set; }

        [DisplayName("Estados:*")]
        [Required(ErrorMessage = "El estado es requerido")]
        public int IdEstado { get; set; }
        public string NomEstado { get; set; }
        public List<UiClasificacionFactor> Clasificaciones { get; set; }

        public List<UiFactor> Factores { get; set; }

        public List<UiEstado> Estados { get; set; }
        public int IdMunicipio { get; set; }

        [DisplayName("Municipios:*")]
        public List<UiMunicipio> Municipios { get; set; }

        [Required(ErrorMessage = "Seleccione al menos un municipio")]
        public List<UiMunicipio> MunicipiosDestino { get; set; }
        public string MunicipiosGrupo { get; set; }

        [DisplayName("Descripción:")]
        public string Descripcion { get; set; }
        public bool IsActive { get; set; }

    }
}