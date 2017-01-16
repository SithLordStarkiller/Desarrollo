using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PubliPayments.Entidades.MYO;


namespace Publipayments.Negocios.MYO
{
    public class Notificaciones
    {
        /// <summary>
        /// Insertar nuevo registro para notificacion
        /// </summary>
        /// <param name="idUsuario">idUsuario del sistema de FLOCK</param>
        /// <param name="titulo">Titulo de la notificacion</param>
        /// <param name="mensaje">Texto que contendra el cuerpo de la notificacion</param>
        /// <param name="tipo">El tipo de importancia que tiene la notificacion Normal, Urgente</param>
        /// <param name="contexto">La forma en la cual se tratara la notificacion 1:Generico, 2:Solicitud </param>
        /// <returns></returns>
        public bool Notificacion(int idUsuario, string titulo, string mensaje, string tipo, int contexto )
        {
            var ent = new EntNotificaciones();
            var resultado = ent.InsertarNotificaciones(idUsuario, titulo, mensaje, titulo, contexto);

            if (resultado.Tables.Count > 0 && resultado.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(resultado.Tables[0].Rows[0]["idNotification"]) > 0;
            }

            return false;
        }
    }
}
