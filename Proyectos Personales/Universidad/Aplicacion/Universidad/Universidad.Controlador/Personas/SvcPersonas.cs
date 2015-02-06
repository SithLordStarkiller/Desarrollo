﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universidad.Controlador.SVRPersonas;
using Universidad.Entidades.ControlUsuario;

namespace Universidad.Controlador.Personas
{
    public class SvcPersonas
    {
        #region Propiedades de la clase

        private readonly SPersonasClient _servicio;

        public SvcPersonas (Sesion sesion)
        {
            var configServicios = new Controlador.ControladorServicios();
            _servicio = new SPersonasClient(configServicios.ObtenBasicHttpBinding(), configServicios.ObtenEndpointAddress(sesion, @"Personas/", "SPersonas.svc"));
        }
        
        #endregion

        #region Operacion de ExisteCorreoUniversidad

        public delegate void ExisteCorreoUniversidadArgs(bool existeCorreo);

        public event ExisteCorreoUniversidadArgs ExisteCorreoUniversidadFinalizado;

        public void ExisteCorreoUniversidad(string correo)
        {
            _servicio.ExisteCorreoUniversidadCompleted +=_servicio_ExisteCorreoUniversidadCompleted;
            _servicio.ExisteCorreoUniversidadAsync(correo);
        }

        private void _servicio_ExisteCorreoUniversidadCompleted(object sender, ExisteCorreoUniversidadCompletedEventArgs e)
        {
            var resultado = e.Result;
            ExisteCorreoUniversidadFinalizado(resultado);
            _servicio.ExisteCorreoUniversidadCompleted -= _servicio_ExisteCorreoUniversidadCompleted;
        }

        #endregion
    }
}
