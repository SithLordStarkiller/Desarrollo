using GOB.SPF.ConecII.Entities.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Entities.Plantilla
{
    
    [SchemaNameAttribute("Plantilla")]
    [TableNameAttribute("Instituciones")]
    public partial class Instituciones
    {
    
        public Instituciones()
        {
        }
        
        
        [IdentityKeyAttribute]
        [PrimaryKeyAttribute("IdInstitucion", AutoIncrement = true)]
        [ColumnAttribute]
        [Display(Name = "IdInstitucion")]
        public int? IdInstitucion { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "IdTipoDocumento")]
        public int? IdTipoDocumento { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        
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
