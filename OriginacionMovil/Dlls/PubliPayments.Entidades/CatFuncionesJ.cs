//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PubliPayments.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class CatFuncionesJ
    {
        public CatFuncionesJ()
        {
            this.CamposXSubFormularios = new HashSet<CamposXSubFormulario>();
        }
    
        public int idFuncionJS { get; set; }
        public string Nombre { get; set; }
        public string Validacion { get; set; }
        public string FuncionSI { get; set; }
        public string FuncionNo { get; set; }
        public int idFormulario { get; set; }
    
        public virtual ICollection<CamposXSubFormulario> CamposXSubFormularios { get; set; }
        public virtual Formulario Formulario { get; set; }
    }
}
