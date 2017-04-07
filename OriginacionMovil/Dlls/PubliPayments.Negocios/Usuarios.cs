using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using PubliPayments.Entidades;

namespace PubliPayments.Negocios
{
    public class Usuarios
    {
        public List<UsuarioModel> ObtenerUsuarios(int idDominio, int idPadre = -1, int idRol = -1, int estatus = -1, string delegacion = "%")
        {
            var datos = new EntUsuario().ObtenerUsuarios(idDominio, idPadre, idRol, estatus, delegacion);
            return datos;
        }

        /// <summary>
        /// Se encarga de cambiar las dependencias de un usuario padre-hijo
        /// </summary>
        /// <param name="idPadreViejo">idUsuario padre que se tiene actualmente</param>
        /// <param name="idPadreNuevo">idUsuario padre que tendrá asignado </param>
        /// <param name="idHijo">Usuario a mover</param>
        /// <returns>Mensaje con el resultado de la acción</returns>
        public string ReasignarUsuarios(string idPadreViejo, string idPadreNuevo, string idHijo)
        {
            return new EntUsuario().ReasignarUsuarios(idPadreViejo, idPadreNuevo, idHijo);
        }
    }
}
