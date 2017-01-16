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

        public UsuarioModel ObtenerUsuarioPorId(string idUsuario)
        {
            return new EntUsuario().ObtenerUsuarioPorId(idUsuario);
        }

        /// <summary>
        /// Inserta nuevo usuario.
        /// </summary>
        /// <param name="idPadre">Id el padre.</param>
        /// <param name="idDominio">I del dominio.</param>
        /// <param name="idRol">Rol del nuevo usuario.</param>
        /// <param name="usuario">Usuario.</param>
        /// <param name="nombre">Nombre del usuario.</param>
        /// <param name="password">Password del usuario.</param>
        /// <param name="email">Email del usuario.</param>
        /// <param name="esCallCenter">Define si es CallCenter.</param>
        /// <returns>DataSet con el dominio y el id del nuevo usuario</returns>
        public DataSet InsertaUSuario(String idPadre, String idDominio, String idRol, String usuario, String nombre,
            String password, String email, int esCallCenter)
        {
            return new EntUsuario().InsertaUSuario(idPadre, idDominio, idRol, usuario, nombre, password, email, esCallCenter);
        }
    }
}
