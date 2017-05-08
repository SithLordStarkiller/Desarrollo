﻿namespace Suncorp.Helpers.CustomExcepciones
{
    using System;

    /// <summary>
    /// Clase que implementa una excepcion personalizada para detectar si un usuario no realizo un acceso
    /// </summary>
    public class UserNotFindException : Exception
    {
        /// <summary>
        /// Campo para almacenar el usuario con el que se realizo el login
        /// </summary>
        public string Usuario { get; }

        /// <summary>
        /// Contraseña encriptada con la cual el usuario realizo el login
        /// </summary>
        public string Contrasena { get; }

        public UserNotFindException(string usuario, string contrasena) : base("No se encontro al usuario o fallo contraseña")
        {
            Usuario = usuario;
            Contrasena = contrasena;
        }

    }
}
