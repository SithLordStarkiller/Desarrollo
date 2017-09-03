using GOB.SPF.ConecII.Entities.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Entities.Plantilla
{
    
    [SchemaNameAttribute("Plantilla")]
    [TableNameAttribute("Parrafos")]
    public partial class Parrafos
    {
    
        public Parrafos()
        {
        }
        
        
        [IdentityKeyAttribute]
        [PrimaryKeyAttribute("IdParrafo", AutoIncrement = true)]
        [ColumnAttribute]
        [Display(Name = "IdParrafo")]
        public int? IdParrafo { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "IdTipoDocumento")]
        public int? IdTipoDocumento { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "Parrafo")]
        public string Nombre { get; set; }
        
        [ColumnAttribute]
        [Display(Name = "Texto")]
        public string Texto { get; set; }
        
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

        public virtual List<EtiquetasParrafo> ListaEtiquetasParrafo { get; set; }
    }
    
}
