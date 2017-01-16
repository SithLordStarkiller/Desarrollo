using System;
using System.ComponentModel.DataAnnotations;

namespace PubliPayments.Entidades
{
    public class UsuarioRelacion
    {
        [Key]
        public String UsuarioGestor { get; set; }
        public String Gestor { get; set; }
        public String Supervisor { get; set; }
        public String Administrador { get; set; }
        public String Despacho { get; set; }
        }

   
}