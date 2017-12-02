namespace GOB.SPF.ConecII.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UiCorreoInstalacion
    {
        public int Indice { get; set; }
        public int Identificador { get; set; }
        [MaxLength(50)]
        [EmailAddress(ErrorMessage = @"El formato de correo electrónico es incorrecto")]
        public string CorreoElectronico { get; set; }
        public bool IsActive { get; set; }
    }
}