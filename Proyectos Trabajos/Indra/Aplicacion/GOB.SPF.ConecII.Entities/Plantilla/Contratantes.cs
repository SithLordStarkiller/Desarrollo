using GOB.SPF.ConecII.Entities.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Entities.Plantilla
{
    
    [SchemaNameAttribute("Plantilla")]
    [TableNameAttribute("Contratantes")]
    public partial class Contratantes
    {
    
        public Contratantes()
        {
        }
        
        
        [IdentityKeyAttribute]
        [PrimaryKeyAttribute("IdContratante", AutoIncrement = true)]
        [ColumnAttribute]
        [Display(Name = "IdContratante")]
        public int? IdContratante { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "IdTipoDocumento")]
        public int? IdTipoDocumento { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "Cargo")]
        public string Cargo { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "Domicilio")]
        public string Domicilio { get; set; }
        
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
