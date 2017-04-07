using System;
using System.Diagnostics;
using System.Threading;
using PubliPayments.Entidades;
using PubliPayments.Utiles;

namespace PubliPayments.Negocios
{
    public class BloqueoConcurrencia
    {
        /// <summary>
        /// Bloquea la concurrencia de los servicios o tareas que se ejecuten en paralelo
        /// </summary>
        /// <param name="modelo">Modelo con los parametros necesarios para validar la concurrencia</param>
        /// <param name="timeOut">Tiempo que se va a esperar en caso de concurrencia o error</param>
        /// <param name="idUsuarioLog">Usuario con el que se va a logear</param>
        /// <returns>Regresa el modelo con el resultado de la concurrencia</returns>
        public BloqueoConcurrenciaModel BloquearConcurrencia(BloqueoConcurrenciaModel modelo,  int timeOut, int idUsuarioLog)
        {
            bool primeraVuelta = true;
            var ent = new EntBloqueoConcurrencia();
            var sw = new Stopwatch();
            sw.Start();

            while (sw.ElapsedMilliseconds < timeOut | primeraVuelta)
            {
                try
                {
                    if (primeraVuelta)
                    {
                        //Si es primera vuelta intento bloquear
                        modelo = ent.BloquearConcurrencia(modelo);

                        if (modelo.ErrorMensaje == "OK")
                        {
                            modelo.ErrorMensaje = "";
                            modelo.Id = modelo.Error;
                            modelo.Error = 0;
                            return modelo;
                        }
                        if (modelo.Error == 2601)
                        {
                            //Si es una repetida obtengo el resultado de la concurrencia
                            modelo.Error = 0;
                            modelo.ErrorMensaje = "";
                            modelo.Id = -1;
                            modelo = ent.ObtenerConcurrencia(modelo);
                            if (modelo.Estatus != 1)
                            {
                                modelo.Resultado = null; 
                                throw new Exception("Encontrada una concurrencia repetia");
                            }
                            return modelo;
                        }
                        throw new Exception(modelo.ErrorMensaje);
                    }
                    //Consulto el estatus de la concurrencia
                    modelo = ent.ObtenerConcurrencia(modelo);
                    if (modelo.Estatus != 1)
                    {
                        modelo.Resultado = null;
                        throw new Exception("Aun se esta procesando la primer concurrencia - Llave: " + modelo.Llave);
                    }
                    return modelo;
                }
                catch (Exception ex)
                {
                    modelo.Error = -1;
                    modelo.Id = -1;
                    modelo.ErrorMensaje = ex.Message;
                    //Si ocurre un error, o es una concurrencia repetida espera 5 segundos para que puedan terminar los demas procesos
                    Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioLog, "BloquearConcurrencia", ex.Message);
                    Thread.Sleep(5000);
                }

                if (primeraVuelta)
                    primeraVuelta = false;
            }

            return modelo;
        }

        /// <summary>
        /// Actualiza y libera la concurrencia de la llave indicada
        /// </summary>
        /// <param name="modelo">Parametros obligatorios id/resultado</param>
        /// <param name="idUsuarioLog">Usuario con el que se va a logear</param>
        public void ActualizarConcurrencia(BloqueoConcurrenciaModel modelo, int idUsuarioLog)
        {
            Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuarioLog, "BloqueoConcurrencia",
                string.Format("ActualizarConcurrencia id = {0}, resultado = {1}", modelo.Id, modelo.Resultado));

            int intentos = 0;
            while (intentos < 3)
            {
                try
                {
                    var ent = new EntBloqueoConcurrencia();
                    modelo.Estatus = 1; //Setea a terminada la concurrencia
                    ent.ActualizarConcurrencia(modelo);
                    break;
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioLog, "BloqueoConcurrencia",
                        string.Format("ActualizarConcurrencia Error = {0}", ex.Message));
                    Thread.Sleep(1000);
                    intentos++;
                }
            }
        }

        /// <summary>
        /// Obtiene el id estatus y resultado para un bloqueo de concurrencia
        /// </summary>
        /// <param name="modelo">El modelo debe tener la llave, aplicacion, origen y el id en -1 para realizar la busqueda o el solamente el id</param>
        /// <param name="idUsuarioLog">Usuario con el que se va a logear</param>
        /// <returns>Regresa el id estatus y resultado para un bloqueo de concurrencia</returns>
        public BloqueoConcurrenciaModel ObtenerBloqueoConcurrencia(BloqueoConcurrenciaModel modelo, int idUsuarioLog)
        {
            try
            {
                modelo = new EntBloqueoConcurrencia().ObtenerConcurrencia(modelo);
            }
            catch (Exception ex)
            {
                modelo.Id = -1;
                modelo.Resultado = null;
                modelo.Estatus = -1;
                Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioLog, "BloqueoConcurrencia",
                    string.Format("ObtenerBloqueoConcurrencia Error = {0}", ex.Message));
            }

            return modelo;
        }
    }
}
