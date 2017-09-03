using GOB.SPF.ConecII.Entities.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Entities.Plantilla
{
    
    [SchemaNameAttribute("Plantilla")]
    [TableNameAttribute("Estiquetas")]
    public partial class Estiquetas
    {
    
        public Estiquetas()
        {
        }
        
        
        [IdentityKeyAttribute]
        [PrimaryKeyAttribute("IdEtiqueta", AutoIncrement = true)]
        [ColumnAttribute]
        [Display(Name = "IdEtiqueta")]
        public int? IdEtiqueta { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "IdTipoDocumento")]
        public int? IdTipoDocumento { get; set; }
        
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
        [ColumnEntityAttribute("EntityTiposDocumento")]
        public virtual TiposDocumento TiposDocumento { get; set; }
    }
    
}
