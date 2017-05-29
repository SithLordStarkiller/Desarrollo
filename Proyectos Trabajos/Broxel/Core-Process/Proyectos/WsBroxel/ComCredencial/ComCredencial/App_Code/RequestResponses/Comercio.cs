using System;

namespace ComCredencial.RequestResponses
{
    [Serializable]
    public class Comercio
    {
        public int idComercio { get; set; }
        public String CodigoComercio { get; set; }
        public String NombreComercio { get; set; }
        public String Producto { get; set; }
        public String Usuario { get; set; }
    }

    [Serializable]
    public class CommerceEditRequest : Request
    {
        public Comercio Comercio { get; set; }
        public String RazonSocial { get; set; }
        public String RFC { get; set; }
        public String Calle { get; set; }
        public String Delegacion { get; set; }
        public String Colonia { get; set; }
        public String Estado { get; set; }
        public String CodigoPostal { get; set; }
        public String Telefono { get; set; }
        public String Email { get; set; }
        public Boolean DoctosCompletos { get; set; }
        public String Banco { get; set; }
        public String NumSucursal { get; set; }
        public new String NumCuenta { get; set; }
        public String NumCuentaClabe { get; set; }
        public String EmailAvisos { get; set; }
        public Boolean AceptoTerminos { get; set; }
        public String FechaAceptoTerminos { get; set; }
        public String UsuarioAceptaTerminos { get; set; }
        public String ComMC { get; set; }
    }

    [Serializable]
    public class ComercioResponse : Response
    {
        public ComercioResponse()
        {
        }
    }
}