using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PubliPayments.Entidades;

namespace PubliPayments.Negocios
{
    public class UsuariosServicios
    {
        private const string Plantillaconexion = "Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};{4}";

        /// <summary>
        /// Obtiene la informacion que se tenga de un servicio que se encuentre  registrado en la tabla UsuariosServicios
        /// </summary>
        /// <param name="tipo"> Tipo del servicio que se quiere consultar</param>
        /// <param name="idAplicacion"></param>
        /// <returns>Lista compuesta de los elementos que se tengan con ese servicio para la aplicacion que se este corriendo</returns>
        /// 1._Envio de email
        /// 2._Envio de mensajes SMS
        /// 3._Conexion a BD
        public List<UsuariosServiciosModel>  ObtenerUsuariosServicios(int tipo, int idAplicacion=0)
        {
            return new EntUsuariosServicios().ObtenerUsuariosServicios(tipo, idAplicacion);

        }

        /// <summary>
        /// Carga las conexiones a Bd que se tengan como historico
        /// </summary>
        public void AgregarConeccionesHistoricasBD()
        {
            var modeloList = ObtenerUsuariosServicios(3);
            if (modeloList!=null)
            {
                var modeloListOrdenado = modeloList.OrderByDescending(x => x.Orden).Take(3);

                foreach (var model in modeloListOrdenado)
                {
                    ConnectionDB.EstalecerConnectionString(model.Nombre,
                        String.Format(Plantillaconexion, model.Servidor, model.Nombre, model.Usuario, model.Password,
                            model.Extra));
                }
            }
           
        }

    }
}
