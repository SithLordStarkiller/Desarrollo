using System;

namespace wsBroxel
{
    /// <summary>
    /// Clase con los datos necesarios para mostrar en el correo de transferencias(SPEI, C2C, Mis tarjetas, Otras tarjetas, Favoritos) 
    /// </summary>
   public class DatosEmailTransferencias
    {
       /// <summary>
       /// Fecha que se envía el correo
       /// </summary>
       public DateTime Fecha { get; set; }
       /// <summary>
       /// Monto de la transferencia
       /// </summary>
       public Double  Monto { get; set; }
       /// <summary>
       /// Nombre del usuario quien realiza la transferencia.
       /// </summary>
       public string Usuario { get; set; }
       /// <summary>
       /// Nombre del usuario destino para transferencias C2C
       /// </summary>
       public string UsuarioDestino { get; set; }
       /// <summary>
       /// Correro del usuario que realiza la transferencia.
       /// </summary>
        public string CorreoUsuario { get; set; }
       /// <summary>
       /// Correo del usuario destino para transferencias C2C y obtener su imagen de perfil en base al correo.
       /// </summary>
        public string CorreoUsuarioDestino { get; set; }
       /// <summary>
       /// Nombre del alias asignada a la tarjeta para transferencias entre mis tarjetas
       /// </summary>
        public string AliasTarjeta { get; set; }
        /// <summary>
       /// id de la CLABE interbancaria registrada por el usuario.
       /// </summary>
       public Int64 IdCLABE { get; set; }
       /// <summary>
       /// Numero de tarjeta donde se realiza el cargo.
       /// </summary>
       public string NumeroTarjeta { get; set; }
       /// <summary>
       /// Numero de la tarjeta destino Transferencias Mis tarjetas
       /// </summary>
       public string NumeroTarjetaDestino { get; set; }
       /// <summary>
       /// Numero de cuenta de la tarjeta que realiza el cargo.
       /// </summary>
        public string NumeroCuenta { get; set; }
        /// <summary>
       /// Numero de referencia que regresa el servicio de transferencias.
       /// </summary>
       public string Referencia { get; set;}
       /// <summary>
       /// Concepto asignado por el usuario.
       /// </summary>
       public string Concepto { get; set; }
       /// <summary>
       /// Numero de autorización del cargo.
       /// </summary>
       public string NumeroAutorizacion { get; set; }
       /// <summary>
       /// Comisión asignada por la transferencia.
       /// </summary>
       public Double Comision { get; set; }

    }
}
