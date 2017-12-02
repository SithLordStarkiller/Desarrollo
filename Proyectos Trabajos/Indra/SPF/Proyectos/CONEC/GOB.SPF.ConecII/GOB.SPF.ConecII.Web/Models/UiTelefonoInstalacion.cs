namespace GOB.SPF.ConecII.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UiTelefonoInstalacion 
    {
        public UiTipoTelefono TipoTelefono { get; set; }
        
        [StringLength(10)]
        [RegularExpression(@"^[0-9]*$")]
        public string Numero { get; set; }
        [MaxLength(50)]
        public string Extension { get; set; }
        public bool IsActive { get; set; }
    }
}