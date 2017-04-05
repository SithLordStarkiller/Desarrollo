
namespace Mock
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class VmFormulario
    {
        [DisplayName("Estado")]
        //[RegularExpression("^[0-9]{0-11}$", ErrorMessage = "El numero de seguro social no es valido")]
        public int IdEstado { get; set; }

        [DisplayName("Trabajo")]
        //[StringLength(11)]
        //[RegularExpression("^[0-9]{11}$", ErrorMessage = "El numero de seguro social no es valido")]
        public int IdTrabajo { get; set; }

        [DisplayName("NSS")]
        //[StringLength(11)]
        //[RegularExpression("^[0-9]{11}$", ErrorMessage = "El numero de seguro social no es valido")]
        public int IdLugar { get; set; }

        [DisplayName("NSS")]
        //[StringLength(11)]
        //[RegularExpression("^[0-9]{11}$", ErrorMessage = "El numero de seguro social no es valido")]
        public string Telefono { get; set; }

        [DisplayName("NSS")]
        //[StringLength(11)]
        //[RegularExpression("^[0-9]{11}$", ErrorMessage = "El numero de seguro social no es valido")]
        public string Clave { get; set; }
    }
}