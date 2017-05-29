using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Net;
using wsBroxel.App_Code.SolicitudBL.Model;

namespace wsBroxel.App_Code.SolicitudBL.Operaciones
{
    /// <summary>
    ///  La clase se encarga de cobrar la comisión de el uso de los pagos de las comisiones que se generen en red de pagos.
    /// </summary>
    public class CobrarComisionRedPagos : IOperacion
    {
        /// <summary>
        /// Ejecuta la operación de separación de las comisiones que cobra red de pagos.
        /// </summary>
        /// <param name="oper">argumentos para los pagos.</param>
        /// <param name="mySql">clase de operaciones a MySql.</param>
        /// <returns></returns>
        public DispResponse Execute(OperArguments oper, MySqlDataAccess mySql)
        {
            var folioSolicitudCargo = new List<string>();
            try
            {
                //auxiliar para todas las comisiones.
                var comisiones = new List <DetalleComisionAsignacion>();
                var comisionesCanal = new List <DetalleComisionAsignacion>();

                var claveCliente = mySql.ObtenerClaveCliente(oper.NumeroCuenta);

                //se obtienen las comisiones de los canales..
                oper.Canal = mySql.ObtenerCanal(oper.IdUser, oper.Folio);
                if(oper.Canal!=null)
                    comisionesCanal = mySql.ObtenerComisionCanal(oper.IdUser,oper.Canal.ToUpper(),claveCliente,oper.Producto);
                if (comisionesCanal.Any())
                {
                    comisiones.AddRange(comisionesCanal);
                }

                //se valida el tipo de operación.
                if (oper.TipoOper == TiposOperacion.Dispersion)
                {
                    //se obtienen las comisiones adicionales como por ejemplo incremento de linea de crédito
                    var comisionesTarjeta = mySql.ObtenerComisionTarjeta(oper.Producto, claveCliente);
                    if (comisionesTarjeta.Any())
                    {
                        comisiones.AddRange(comisionesTarjeta);
                    }

                }
                if (comisiones.Any())
                {

                    foreach (var comision in comisiones)
                    {
                        //parámetro para el folio
                        var folioParametro = new ObjectParameter("Folio", typeof(string));

                        //comisión de acuerdo al tipo que aplica.
                        var montoComision = comision.TipoAplica == "porcentaje" ? ObtenPorcentaje(oper.Monto, comision.Valor) : comision.Valor;

                        //procedure que realiza la operación para generar el folio de la solicitud del cargo.
                        using (var proxySql = new BroxelSQLEntities())
                        {
                            proxySql.SpInsertarCargoSolicitud(mySql.ObtenerClaveCliente(oper.NumeroCuenta), oper.NumeroCuenta, oper.Producto, comision.idComercio,
                            comision.NombreComercio, "WebService", "CARGO", "asignaciondelinea@broxel.com", "asignaciondelinea@broxel.com", 0, montoComision, true, folioParametro);

                        }

                        if (folioParametro.Value.ToString() != "Error" && folioParametro.Value.ToString() != string.Empty)
                            folioSolicitudCargo.Add(folioParametro.Value.ToString());
                    }


                    oper.FolioSolicitudCargo = folioSolicitudCargo;
                }



            }
            catch (Exception e)
            {
                mySql.InsertDispersionErr(oper.Folio,"Error al intentar cobrar comisiones " + e.Message);
                oper.FolioSolicitudCargo = null;
            }

            return null;
        }

        /// <summary>
        /// Obtiene el porcentaje de un monto.
        /// </summary>
        /// <param name="monto">monto que se quiere obtener el porcentaje.</param>
        /// <param name="porcentaje">porcentaje a obtener.</param>
        /// <returns>porcentaje en monto.</returns>
        private decimal ObtenPorcentaje(decimal monto, decimal porcentaje)
        {
            return (monto * porcentaje);
        }
    }
}