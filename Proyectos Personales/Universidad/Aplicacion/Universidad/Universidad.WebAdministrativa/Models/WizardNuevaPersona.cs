using Universidad.Entidades;

namespace Universidad.WebAdministrativa.Models
{
    public class WizardNuevaPersona
    {
        public PER_PERSONAS DatosPersona { get; set; }
        public DIR_DIRECCIONES DireccionPersona { get; set; }
        public PER_CAT_TELEFONOS TelefonoPersona { get; set; }
        public PER_FOTOGRAFIA FotografiaPersona { get; set; }
        public PER_MEDIOS_ELECTRONICOS MediosPersona { get; set; }
    }
}