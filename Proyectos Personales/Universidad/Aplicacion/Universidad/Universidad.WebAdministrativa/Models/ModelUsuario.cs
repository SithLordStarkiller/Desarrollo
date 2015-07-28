namespace Universidad.WebAdministrativa.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class ModelUsuario
    {
        [DisplayName("Id")]
        public int IdUsuario { get; set; }

        [DisplayName("Nuevo Usuario")]
        [Required(ErrorMessage = "El usuarios es un campo obligatorio")]
        public string Usuario { get; set; }

        [DisplayName("Cantraseña")]
        [Required(ErrorMessage = "La contraseña es un campo obligatorio")]
        public string Contrasena { get; set; }

        [DisplayName("Confirmacion contraseña")]
        [Required(ErrorMessage = "La contraseña debe ser confirmada")]
        public string ConfirmacionContrasena { get; set; }

        [DisplayName("Tipo de usuario")]
        [Required(ErrorMessage = "El tipo de usuario es un campo obligatorio")]
        public int IdTipoUsuario { get; set; }

        [DisplayName("Nivel de usuario")]
        [Required(ErrorMessage = "El nivel de usuarios es un campo obligatorio")]
        public int IdNivelUsuario { get; set; }

        [DisplayName("Estatus de usuario")]
        [Required(ErrorMessage = "El estatus del usuario es un campo obligatorio")]
        public int IdEstatusUsuario { get; set; }
    }
}