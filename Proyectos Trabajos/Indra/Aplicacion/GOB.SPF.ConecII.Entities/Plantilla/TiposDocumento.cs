using GOB.SPF.ConecII.Entities.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Entities.Plantilla
{
    
    [SchemaNameAttribute("Plantilla")]
    [TableNameAttribute("TiposDocumento")]
    public partial class TiposDocumento
    {
    
        public TiposDocumento()
        {
        }
        
        
        [IdentityKeyAttribute]
        [PrimaryKeyAttribute("IdTipoDocumento", AutoIncrement = true)]
        [ColumnAttribute]
        [Display(Name = "IdTipoDocumento")]
        public int? IdTipoDocumento { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "Documento")]
        public string Nombre { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "FechaInicial")]
        public DateTime? FechaInicial { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "FechaFinal")]
        public DateTime? FechaFinal { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "Activo")]
        public bool Activo { get; set; }
    }
    
}
