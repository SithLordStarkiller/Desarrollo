using GOB.SPF.ConecII.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Entities.Usuarios
{
    public class NUsuario : IUsuario
    {
        public bool Activo { get; set; }
        public DateTime? FechaFinal { get; set; }
        [Required]
        public DateTime FechaInical { get; set; }
        public int Id { get; set; }
        public int IdExterno { get; set; }
        [Required]
        public Guid? IdPersona { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Login { get; set; }
        public string UserName { get { return Login; } set { Login = value; } }
    }
}