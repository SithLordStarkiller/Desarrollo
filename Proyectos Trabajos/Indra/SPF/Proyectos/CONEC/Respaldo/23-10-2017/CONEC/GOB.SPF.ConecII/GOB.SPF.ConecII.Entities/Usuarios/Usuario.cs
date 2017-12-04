using GOB.SPF.ConecII.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace GOB.SPF.ConecII.Entities.Usuarios
{
    public class Usuario : IUsuario
    {
        private int _id;
        public Usuario(int id)
        {
            _id = id;
        }
        public bool Activo { get; set; }
        public DateTime? FechaFinal { get; set; }
        public DateTime FechaInical { get; set; }
        public int Id { get { return _id; } }
        public int IdExterno { get; set; }
        public Guid? IdPersona { get; set; }
        public string Login { get; set; }
        public string UserName { get { return Login; } set { Login = value; } }
    }
}
