using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using PubliPayments.Entidades;

namespace PubliPayments.Models
{
    public class FormularioModelCw
    {
        [Key]
        public int IdFormulario { get; set; }
        public string Formulario { get; set; } 
        public List<CamposXSubFormulario> ListaCamposXFormularios { get; set; }
        public Dictionary<int, List<SelectListItem>> CatalogosListas { get; set; }
        public string Clase { get; set; }
    }
}