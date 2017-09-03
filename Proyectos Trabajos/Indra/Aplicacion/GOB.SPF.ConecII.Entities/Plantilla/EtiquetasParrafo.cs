using GOB.SPF.ConecII.Entities.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Entities.Plantilla
{
    
    [SchemaNameAttribute("Plantilla")]
    [TableNameAttribute("EtiquetasParrafo")]
    public partial class EtiquetasParrafo
    {
    
        public EtiquetasParrafo()
        {
        }
        
        
        [IdentityKeyAttribute]
        [PrimaryKeyAttribute("IdEtiquetaParrafo", AutoIncrement = true)]
        [ColumnAttribute]
        [Display(Name = "IdEtiquetaParrafo")]
        public int? IdEtiquetaParrafo { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "IdParrafo")]
        public int? IdParrafo { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "Etiqueta")]
        public string Etiqueta { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "Contenido")]
        public string Contenido { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "Negrita")]
        public bool Negrita { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "Orden")]
        public decimal? Orden { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "FechaInicial")]
        public DateTime? FechaInicial { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "FechaFinal")]
        public DateTime? FechaFinal { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "Activo")]
        public bool Activo { get; set; }
        [ColumnEntityAttribute("EntityParrafos")]
        public virtual Parrafos Parrafos { get; set; }
    }
    
}
