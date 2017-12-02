namespace GOB.SPF.ConecII.Web.Models
{
    using Interfaces;

    using System;
    using System.ComponentModel.DataAnnotations;

    public class UiControl : UiEntity, IControl
    {
        public int Identificador { get; set; }
        public int IdTipoControl { get; set; }
        public int IdModulo { get; set; }
        [MaxLength(60)]
        public string Nombre { get; set; }
        [MaxLength(100)]
        public string Descripcion { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime? FechaFinal { get; set; }
        public bool Activo { get; set; }
    }
}