using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DevExpress.Office.Utils;


namespace PubliPayments.Models
{
    public class AdminUsuariosModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = @"Usuario")]
        public string Usuario { get; set; }

        [Required]
        //[DataType(DataType.Text)]
        [Display(Name = @"Nombre")]
        public string Nombre { get; set; }

        [Required]
        //[DataType(DataType.Text)]
        [Display(Name = @"Email")]
        public string Correo { get; set; }

        [Required]
        public string Accion { get; set; }

        [Required]
        public int idUsuario { get; set; }

        [Required]
        [Display(Name = @"CallCenter")]
        public int EsCallCenter { get; set; }

        public List<DownList> Lista { get; set; }
    }
}