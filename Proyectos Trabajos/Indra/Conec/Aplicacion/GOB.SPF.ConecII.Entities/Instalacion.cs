using System;
using System.Collections.Generic;

namespace GOB.SPF.ConecII.Entities
{
    public class Instalacion : TEntity
    {
        public Instalacion()
        {
            Cliente = new Cliente();
            Zona = new Zona();
            Estacion = new Estacion();
            TipoInstalacion = new TipoInstalacion();
            TelefonosInstalacion = new List<Telefono>();
            CorreosInstalacion = new List<Correo>();
            Asentamiento = new Asentamiento();
            Fraccion = new Fraccion();
        }
        public Cliente Cliente { get; set; }
        public Zona Zona { get; set; }
        public Estacion Estacion { get; set; }
        public string Nombre { get; set; }
        public TipoInstalacion TipoInstalacion { get; set; }
        public List<Telefono> TelefonosInstalacion { get; set; }
        public List<Correo> CorreosInstalacion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Calle { get; set; }
        public string NumInterior { get; set; }
        public string NumExterior { get; set; }
        public string Referencia { get; set; }
        public string Colindancia { get; set; }
        public string CodigoPostal { get; set; }
        public Asentamiento Asentamiento { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public Fraccion Fraccion { get; set; }
        public bool Activo { get; set; }
    }
}
