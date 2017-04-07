using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PubliPayments.Utiles;
using PubliPayments.Entidades;

namespace PubliPayments.Negocios
{
   public class RegistroUsuario
    {
       /// <summary>
       /// Activa un usuario y le crea una orden para poder guardar los registros de su originación en caso que sea necesario
       /// </summary>
       /// <param name="externalId">identificador donde si no ex un entero, seguro pertenece a una originación </param>
       /// <param name="externalType"> nombre del formulario que se esta procesando</param>
       /// <param name="assignedTo">Usuario que esta mandando la información</param>
       /// <returns>id Orden perteneciente al registro del usuario</returns>
       public int RegistroUsuarioPPM(string externalId, string externalType, string assignedTo=null)
       {
           try
           {
               var entUsuario = new EntUsuario();
               var enOrdenes = new EntOrdenes();
               var usuario = entUsuario.ObtenerUsuarioXUsuario(assignedTo);
               var usuariopadre = entUsuario.ObtenerUsuarioPorId(usuario.IdPadre);
               var orden = enOrdenes.GeneraOrdenXml(externalId, usuario.IdUsuario, usuariopadre.idUsuario,usuariopadre.idPadre, 1, -1);
               entUsuario.ActualizarUsuario(usuario.IdUsuario, null, null, null, 1, null);
               return Convert.ToInt32(orden.Tables[2].Rows[0]["idOrden"]);

           }
           catch (Exception ex)
           {
               Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "RegistroUsuario", "RegistroUsuarioPPM_" + ex.Message);
           }

           return 0;
       }
    }
}
