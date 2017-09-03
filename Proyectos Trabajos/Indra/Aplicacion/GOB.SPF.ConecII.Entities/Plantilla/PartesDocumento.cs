using GOB.SPF.ConecII.Entities.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Entities.Plantilla
{
    
    [SchemaNameAttribute("Plantilla")]
    [TableNameAttribute("PartesDocumento")]
    public partial class PartesDocumento
    {
    
        public PartesDocumento()
        {
        }
        
        
        [IdentityKeyAttribute]
        [PrimaryKeyAttribute("IdParteDocumento", AutoIncrement = true)]
        [ColumnAttribute]
        [Display(Name = "IdParteDocumento")]
        public int? IdParteDocumento { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "IdTipoDocumento")]
        public int? IdTipoDocumento { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "RutaLogo")]
        public string RutaLogo { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "Folio")]
        public string Folio { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "Asunto")]
        public string Asunto { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "LugarFecha")]
        public string LugarFecha { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "Paginado")]
        public bool Paginado { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "Ccp")]
        public string Ccp { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "Puesto")]
        public string Puesto { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "Direccion")]
        public string Direccion { get; set; }
        
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
