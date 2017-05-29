using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using srvBroxel.Controllers;
using srvBroxel.DAL;
using System.Text.RegularExpressions;

namespace srvBroxel
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class SrvBroxel : IsrvBroxel
    {
        /// <summary>
        /// Realiza el alta de un cliente en MySql.
        /// </summary>
        /// <param name="pNombreCorto">Nombre corto del cliente.</param>
        /// <param name="pNombre">Nombre(s) del cliente.</param>
        /// <param name="pApPaterno">Apellido paterno.</param>
        /// <param name="pApMaterno">Apellido materno.</param>
        /// <param name="pRazonSocial">Razón social, solo en caso de que se trate de persona moral.</param>
        /// <param name="pFechaNacimientoConstitucion">Fecha de nacimiento o de constitución del cliente.</param>
        /// <param name="pRfc">RFC.</param>
        /// <param name="pNombreRepLegal">Nombre(s) representante legal.</param>
        /// <param name="pApPaternoRepLegal">Apellido paterno del representante legal.</param>
        /// <param name="pApMaternoRepLegal">Apellido materno del representante legal.</param>
        /// <param name="pFecNacRepLegal">Fecha de nacimiento del representante legal.</param>
        /// <param name="pRfcRepLegal">RFC del representante legal.</param>
        /// <param name="pCalle">Calle del cliente.</param>
        /// <param name="pNumExterior">Número exterior del domicilio del cliente.</param>
        /// <param name="pNumInterior">Número interior del domicilio del cliente.</param>
        /// <param name="pColonia">Colonia del domicilio del cliente.</param>
        /// <param name="pDelegacionMunicipio">Delegación o municipio del domicilio del cliente.</param>
        /// <param name="pCp">Código postal del domicilio del cliente</param>
        /// <param name="pEstado">Estado del domicilio del cliente</param>
        /// <param name="pClavePais">Clave del país del domicilio del cliente.</param>
        /// <param name="pTelefono">Número telefónico del cliente</param>
        /// <param name="pCorreoContacto">Correo electrónico de contacto.</param>
        /// <param name="pReportaPld">Bandera de si se reporta a PLD o no.</param>
        /// <param name="pEmiteFactura">Bandera de si se emite factura o no.</param>
        /// <param name="pProducto">Clave del producto relacionado con el cliente.</param>
        /// <param name="pMaquilaId">Identificador de Maquila para obtener la CLABE correspodiente.</param>
        /// <returns></returns>
        public string AltaClienteBroxel(String pNombreCorto, String pNombre, String pApPaterno, String pApMaterno,
            String pRazonSocial, DateTime pFechaNacimientoConstitucion, String pRfc, String pNombreRepLegal,
            String pApPaternoRepLegal, String pApMaternoRepLegal, DateTime pFecNacRepLegal, String pRfcRepLegal,
            String pCalle, String pNumExterior, String pNumInterior, String pColonia, String pDelegacionMunicipio,
            String pCp, String pEstado, String pClavePais, String pTelefono, String pCorreoContacto, Boolean pReportaPld,
            Boolean pEmiteFactura, String pProducto, int pMaquilaId)
        {
            var db = new broxelco_rdgEntities();
            
            long idClienteBroxel = 0;
            long agrupacionClientesId = 0;
            long detalleClientesBroxelId = 0;
            long clienteComisionId = 0;

            try
            {
                //Se genera el folioCliente
                //using (var dbTrans = db.Database.BeginTransaction())               
                //{
                    try
                    {
                        
                        bool continua = true;

                        var query =
                            "SELECT COALESCE(substr(max(`claveCliente`),7,6)+1,1,substr(max(`claveCliente`),7,6)+1) AS folio FROM clientesBroxel WHERE SUBSTRING(`claveCliente`,1,5)='" +
                            DateTime.Now.ToString("yy") + "MYO'";

                        string folio = "";

                        Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " + " AltaClienteBroxel: Query: " + query);

                        var firstOrDefault = db.Database.SqlQuery<String>(query).FirstOrDefault();
                        if (firstOrDefault != null)
                        {
                            folio = DateTime.Now.ToString("yy") + "MYO" + firstOrDefault.PadLeft(6).Replace(' ', '0');
                        }

                        Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " + " AltaClienteBroxel: Folio " + folio);


                        //Se Guarda en clientesBroxel
                        try
                        {
                            var clientesBroxelsController = new clientesBroxelsController();

                            var clientesBroxel = new clientesBroxel
                            {
                                claveCliente = folio,
                                GrupoCliente = folio,
                                NombreCorto = string.IsNullOrEmpty(pNombreCorto) ? "" : pNombreCorto.ToUpper(),
                                razonSocial =
                                    string.IsNullOrEmpty(pRazonSocial)
                                        ? pNombre.ToUpper() + " " + pApPaterno.ToUpper() + " " + pApMaterno.ToUpper()
                                        : pRazonSocial.ToUpper(),
                                fechaConstitucion = pFechaNacimientoConstitucion,
                                rfc = string.IsNullOrEmpty(pRfc) ? "" : pRfc.ToUpper(),
                                clavePais = string.IsNullOrEmpty(pClavePais) ? "MX" : pClavePais.ToUpper(),
                                actividadEconomica = 9999999,
                                nombreRepLegal =
                                    string.IsNullOrEmpty(pNombreRepLegal)
                                        ? pNombre.ToUpper()
                                        : pNombreRepLegal.ToUpper(),
                                aPaternoRegLegal =
                                    string.IsNullOrEmpty(pApPaternoRepLegal)
                                        ? pApPaterno.ToUpper()
                                        : pApPaternoRepLegal.ToUpper(),
                                aMaternoRepLegal =
                                    string.IsNullOrEmpty(pApMaternoRepLegal)
                                        ? pApMaterno.ToUpper()
                                        : pApMaternoRepLegal.ToUpper(),
                                fechaNacimientoRepLegal = pFecNacRepLegal,
                                rfcRepLegal =
                                    string.IsNullOrEmpty(pRfcRepLegal)
                                        ? string.IsNullOrEmpty(pRfc) ? "" : pRfc.ToUpper()
                                        : pRfcRepLegal.ToUpper(),
                                curpRepLegal = " ",
                                tipoIdRepLegal = " ",
                                tipoIdOtroRepLegal = " ",
                                autoridadEmiteRepLegal = " ",
                                numIdentificacionRepLegal = " ",
                                colonia = string.IsNullOrEmpty(pColonia) ? " " : pColonia.ToUpper(),
                                calle = string.IsNullOrEmpty(pCalle) ? " " : pCalle.ToUpper(),
                                numExt = string.IsNullOrEmpty(pNumExterior) ? " " : pNumExterior.ToUpper(),
                                numInt = string.IsNullOrEmpty(pNumInterior) ? " " : pNumInterior.ToUpper(),
                                codigoPostal = string.IsNullOrEmpty(pCp) ? " " : pCp,
                                paisTel = "MX",
                                tel = string.IsNullOrEmpty(pTelefono) ? " " : pTelefono,
                                Activo = true,
                                ClaveClienteWeb = folio,
                                CLABEDestino = " ",
                                CorreoContacto = string.IsNullOrEmpty(pCorreoContacto) ? " " : pCorreoContacto.ToUpper(),
                                Localidad = string.IsNullOrEmpty(pColonia) ? " " : pColonia.ToUpper(),
                                Referencia = " ",
                                MunicipioDelegacion =
                                    string.IsNullOrEmpty(pDelegacionMunicipio) ? " " : pDelegacionMunicipio.ToUpper(),
                                Estado = string.IsNullOrEmpty(pEstado) ? " " : pEstado.ToUpper(),
                                ReportaPLD = pReportaPld,
                                EmiteFactura = pEmiteFactura,
                                FechaCreacion = DateTime.Now,
                                UsuarioCreacion = "MYOWebService"

                            };

                            clientesBroxelsController.PostclientesBroxel(clientesBroxel);
                            Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                            "AltaClienteBroxel: Se escribió en clientesBroxel con id: " +
                                            clientesBroxel.id);

                            idClienteBroxel = clientesBroxel.id;


                        }
                        catch (Exception ex)
                        {
                            ErrorHandling.EscribeError(ex);
                            continua = false;

                        }


                        if (continua)
                        {
                            try
                            {
                                //Se inserta en AgrupacionClientes
                                var agrupacionClientesController = new AgrupacionClientesController();
                                var agrupacionClientes = new AgrupacionClientes
                                {
                                    GrupoFiscal = false,
                                    ClaveAgrupacion = folio,
                                    NombreCorto = string.IsNullOrEmpty(pNombreCorto) ? "" : pNombreCorto.ToUpper(),
                                    razonSocial =
                                        string.IsNullOrEmpty(pRazonSocial)
                                            ? pNombre.ToUpper() + " " + pApPaterno.ToUpper() + " " +
                                              pApMaterno.ToUpper()
                                            : pRazonSocial.ToUpper(),
                                    fechaConstitucion = pFechaNacimientoConstitucion,
                                    rfc = string.IsNullOrEmpty(pRfc) ? "" : pRfc.ToUpper(),
                                    clavePais = string.IsNullOrEmpty(pClavePais) ? "MX" : pClavePais.ToUpper(),
                                    nombreRepLegal =
                                        string.IsNullOrEmpty(pNombreRepLegal)
                                            ? pNombre.ToUpper()
                                            : pNombreRepLegal.ToUpper(),
                                    aPaternoRegLegal =
                                        string.IsNullOrEmpty(pApPaternoRepLegal)
                                            ? pApPaterno.ToUpper()
                                            : pApPaternoRepLegal.ToUpper(),
                                    aMaternoRepLegal =
                                        string.IsNullOrEmpty(pApMaternoRepLegal)
                                            ? pApMaterno.ToUpper()
                                            : pApMaternoRepLegal.ToUpper(),
                                    fechaNacimientoRepLegal = pFecNacRepLegal,
                                    rfcRepLegal =
                                        string.IsNullOrEmpty(pRfcRepLegal)
                                            ? (string.IsNullOrEmpty(pRfc) ? "" : pRfc.ToUpper())
                                            : pRfcRepLegal.ToUpper(),
                                    curpRepLegal = " ",
                                    tipoIdRepLegal = " ",
                                    tipoIdOtroRepLegal = " ",
                                    autoridadEmiteRepLegal = " ",
                                    numIdentificacionRepLegal = " ",
                                    colonia = string.IsNullOrEmpty(pColonia) ? " " : pColonia.ToUpper(),
                                    calle = string.IsNullOrEmpty(pCalle) ? " " : pCalle.ToUpper(),
                                    numExt = string.IsNullOrEmpty(pNumExterior) ? " " : pNumExterior.ToUpper(),
                                    numInt = string.IsNullOrEmpty(pNumInterior) ? " " : pNumInterior.ToUpper(),
                                    codigoPostal = string.IsNullOrEmpty(pCp) ? " " : pCp,
                                    paisTel = "MX",
                                    tel = string.IsNullOrEmpty(pTelefono) ? " " : pTelefono,
                                    CLABEDestino = " ",
                                    CorreoContacto =
                                        string.IsNullOrEmpty(pCorreoContacto) ? " " : pCorreoContacto.ToUpper(),
                                    Localidad = string.IsNullOrEmpty(pColonia) ? " " : pColonia.ToUpper(),
                                    Referencia = " ",
                                    MunicipioDelegacion =
                                        string.IsNullOrEmpty(pDelegacionMunicipio)
                                            ? " "
                                            : pDelegacionMunicipio.ToUpper(),
                                    Estado = string.IsNullOrEmpty(pEstado) ? " " : pEstado.ToUpper(),
                                    ReportaPLD = pReportaPld,
                                    EmiteFactura = pEmiteFactura

                                };

                                agrupacionClientesController.PostAgrupacionClientes(agrupacionClientes);

                                agrupacionClientesId = agrupacionClientes.id;

                                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                                "AltaClienteBroxel: Se escribió en AgrupacionClientes con folio:" +
                                                folio);

                            }
                            catch (Exception ex)
                            {
                                ErrorHandling.EscribeError(ex);
                                continua = false;

                            }
                        }

                        if (continua)
                        {

                            //Se recupera la cuenta clabe de maquila para insertarla o actualizarla en el detalle de cliente
                            var maquilaConsulta = new maquilasController();

                            var maquilaResult = maquilaConsulta.Getmaquila().FirstOrDefault(m => m.id == pMaquilaId);

                            //ToDo: Se debe buscar en base al cliente inicial de maquila las condiciones en DetalleClientesBroxel y heredarlas a este cliente
                            //Se guarda DetalleClientesBroxel
                            var detalleClientesBroxelController = new DetalleClientesBroxelsController();
                            var detClnteBrxlOri =
                                detalleClientesBroxelController.GetDetalleClientesBroxel()
                                    .FirstOrDefault(d => d.ClaveCliente == maquilaResult.DCClaveCliente);
                            
                            var detalleClientesBroxel = new DetalleClientesBroxel();

                            if (detClnteBrxlOri != null)
                            {
                                detalleClientesBroxel.IdCliente = Convert.ToInt32(idClienteBroxel);
                                detalleClientesBroxel.ClaveCliente = folio;
                                detalleClientesBroxel.Producto = pProducto;
                                detalleClientesBroxel.LineaCreditoOriginal = detClnteBrxlOri.LineaCreditoOriginal;
                                detalleClientesBroxel.Dispersado = detClnteBrxlOri.Dispersado;
                                detalleClientesBroxel.Abonos = detClnteBrxlOri.Abonos;
                                detalleClientesBroxel.CargosB1010 = detClnteBrxlOri.CargosB1010;
                                detalleClientesBroxel.TransferenciasB1010 = detClnteBrxlOri.TransferenciasB1010;
                                detalleClientesBroxel.DevolucionesDeCuentas = detClnteBrxlOri.DevolucionesDeCuentas;
                                detalleClientesBroxel.PagosCredito = detClnteBrxlOri.PagosCredito;
                                detalleClientesBroxel.LineaCreditoActual = detClnteBrxlOri.LineaCreditoActual;
                                detalleClientesBroxel.TopeMaximo = detClnteBrxlOri.TopeMaximo;
                                detalleClientesBroxel.TipoRiesgo = detClnteBrxlOri.TipoRiesgo;
                                detalleClientesBroxel.TipoComisionDisposicion = detClnteBrxlOri.TipoComisionDisposicion;
                                detalleClientesBroxel.ComisionDisposicion = detClnteBrxlOri.ComisionDisposicion;
                                detalleClientesBroxel.ATM = detClnteBrxlOri.ATM;
                                detalleClientesBroxel.EnviaPagos = detClnteBrxlOri.EnviaPagos;
                                detalleClientesBroxel.EnviaSMSs = detClnteBrxlOri.EnviaSMSs;
                                detalleClientesBroxel.EncolaDispersiones = detClnteBrxlOri.EncolaDispersiones;
                                detalleClientesBroxel.TipoComisionTransferencia = detClnteBrxlOri.TipoComisionTransferencia;
                                detalleClientesBroxel.ComisionTransferencia = detClnteBrxlOri.ComisionTransferencia;
                                detalleClientesBroxel.TipoConceptoComisionTransferencia = detClnteBrxlOri.TipoConceptoComisionTransferencia;
                                detalleClientesBroxel.RecibeTransferencia = detClnteBrxlOri.RecibeTransferencia;
                                detalleClientesBroxel.FactorDeposito = detClnteBrxlOri.FactorDeposito;
                                detalleClientesBroxel.CambioDeNip = detClnteBrxlOri.CambioDeNip;
                                detalleClientesBroxel.AplicaSPEI = detClnteBrxlOri.AplicaSPEI;
                                detalleClientesBroxel.DesglosaIEPS = detClnteBrxlOri.DesglosaIEPS;
                                detalleClientesBroxel.UsuarioCreacion = "MYOWebService";
                                detalleClientesBroxel.FechaUsuarioCreacion = DateTime.Now;
                                detalleClientesBroxel.Activo = true;
                                detalleClientesBroxel.CLABE = maquilaResult.CLABE;

                            }
                            else
                            {
                                //TODO: Se debe cambiar en la defición el tipo de dato para el campo IdCliente, debe ser un LONG

                                detalleClientesBroxel.IdCliente = Convert.ToInt32(idClienteBroxel);
                                detalleClientesBroxel.ClaveCliente = folio;
                                detalleClientesBroxel.Producto = pProducto;
                                detalleClientesBroxel.LineaCreditoOriginal = 0;
                                detalleClientesBroxel.Dispersado = 0;
                                detalleClientesBroxel.Abonos = 0;
                                detalleClientesBroxel.CargosB1010 = 0;
                                detalleClientesBroxel.TransferenciasB1010 = 0;
                                detalleClientesBroxel.DevolucionesDeCuentas = 0;
                                detalleClientesBroxel.PagosCredito = 0;
                                detalleClientesBroxel.LineaCreditoActual = 0;
                                detalleClientesBroxel.TopeMaximo = 0;
                                detalleClientesBroxel.TipoRiesgo = 3;
                                detalleClientesBroxel.TipoComisionDisposicion = 1;
                                detalleClientesBroxel.ComisionDisposicion = 0.0150;
                                detalleClientesBroxel.ATM = 100;
                                detalleClientesBroxel.EnviaPagos = false;
                                detalleClientesBroxel.EnviaSMSs = false;
                                detalleClientesBroxel.EncolaDispersiones = false;
                                detalleClientesBroxel.TipoComisionTransferencia = 1;
                                detalleClientesBroxel.ComisionTransferencia = 0;
                                detalleClientesBroxel.TipoConceptoComisionTransferencia = 1;
                                detalleClientesBroxel.RecibeTransferencia = 1;
                                detalleClientesBroxel.FactorDeposito = 1;
                                detalleClientesBroxel.CambioDeNip = 2;
                                detalleClientesBroxel.AplicaSPEI = true;
                                detalleClientesBroxel.DesglosaIEPS = false;
                                detalleClientesBroxel.UsuarioCreacion = "MYOWebService";
                                detalleClientesBroxel.FechaUsuarioCreacion = DateTime.Now;
                                detalleClientesBroxel.Activo = true;
                                detalleClientesBroxel.CLABE = maquilaResult.CLABE;

                            }

                            try
                            {


                                detalleClientesBroxelController.PostDetalleClientesBroxel(detalleClientesBroxel);

                                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                                "AltaClienteBroxel: Se escribió en DetalleClientesBroxel con id" +
                                                detalleClientesBroxel.Id);

                                detalleClientesBroxelId = detalleClientesBroxel.Id;

                            }
                            catch (Exception exception)
                            {
                                ErrorHandling.EscribeError(exception);
                                continua = false;
                            }

                        }

                        if (continua)
                        {

                            //Insertar en ClienteComisiones
                            var clienteComisionesController = new ClientesComisionesController();
                            var clienteComision = new ClientesComisiones
                            {
                                CodigoComision = "CO0001",
                                ClaveCliente = folio,
                                Producto = pProducto,
                                Porcentaje = "0",
                                Monto = "",
                                Concepto = "DISPERSION DE FONDOS",
                                Descripcion = "",
                                UsuarioCreacion = "MYOWebService",
                                FechaHoraCreacion = DateTime.Now
                            };
                            try
                            {


                                clienteComisionesController.PostClientesComisiones(clienteComision);

                                clienteComisionId = clienteComision.Id;

                                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                                "AltaClienteBroxel: Se insertó en clienteComision con folio: " + folio +
                                                " y producto: " + pProducto);
                            }
                            catch (Exception exception)
                            {
                                ErrorHandling.EscribeError(exception);
                                continua = false;
                            }
                        }

                        if (continua)
                        {
                            //Se genera la bitácora correspondiente
                            var bitacora = new LogDetalleClientesBroxel
                            {
                                FechaHoraCreacion = DateTime.Now,
                                IdDetalleClientesBroxel = Convert.ToInt32(detalleClientesBroxelId),
                                CampoAfectado = "LineaCreditoOriginal",
                                Monto = 0,
                                Motivo = "CargaInicial",
                                UsuarioCreacion = "MYOWebService"
                            };

                            var logDetalleClientesBroxelController = new LogDetalleClientesBroxelsController();
                            logDetalleClientesBroxelController.PostLogDetalleClientesBroxel(bitacora);

                            Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                            "AltaClienteBroxel: Se escribió en LogDetalleClientesBroxel");
                        }

                        //dbTrans.Commit();

                        if(continua)
                            return idClienteBroxel + "|" + folio;

                        EliminaInfoCliente(idClienteBroxel, agrupacionClientesId, detalleClientesBroxelId, clienteComisionId);

                        return "Error: No se ha podido insertar el cliente, por favor reinténtelo";

                    }
                    catch (DbEntityValidationException entityValidationException)
                    {
                        //dbTrans.Rollback();
                        foreach (var validationErrors in entityValidationException.EntityValidationErrors)
                        {
                            var exceptionText = new StringBuilder();
                            exceptionText.AppendFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", validationErrors.Entry.Entity.GetType().Name, validationErrors.Entry.State);
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                exceptionText.AppendFormat("Property: {0} Error: {1}",
                                    validationError.PropertyName,
                                    validationError.ErrorMessage);
                            }
                            ErrorHandling.EscribeError(exceptionText.ToString());
                            Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " + " DbEntityValidationException: " + exceptionText);
                        }

                        EliminaInfoCliente(idClienteBroxel, agrupacionClientesId, detalleClientesBroxelId, clienteComisionId);

                        return "Error en AltaCliente -entityValidation";
                    }

                    catch (Exception ex)
                    {
                        //dbTrans.Rollback();
                        ErrorHandling.EscribeError(ex);

                        Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " + "Error en AltaCliente -exception 1" + ex);
                        
                        EliminaInfoCliente(idClienteBroxel, agrupacionClientesId, detalleClientesBroxelId, clienteComisionId);

                        return "Error en AltaCliente -exception 1";                        
                    }
                //}
            }

            catch (Exception exception)
            {
                ErrorHandling.EscribeError(exception);

                EliminaInfoCliente(idClienteBroxel, agrupacionClientesId, detalleClientesBroxelId, clienteComisionId);

                return "Error en AltaCliente -exception 2";
            }
        }

        /// <summary>
        /// Elimina los registros de un cliente
        /// </summary>
        /// <param name="idClienteBroxel">Identificador de la tabla clienteBroxel.</param>
        /// <param name="agrupacionClientesId">Identificador de la tabla AgrupacionClientes.</param>
        /// <param name="detalleClientesBroxelId">Identificador de la tabla DetalleClientesBroxel.</param>
        /// <param name="clienteComisionId">Identificador de la tabla ClientesComisiones.</param>
        private void EliminaInfoCliente(long idClienteBroxel, long agrupacionClientesId, long detalleClientesBroxelId, long clienteComisionId)
        {
            try
            {
                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                " Inicio DeleteData: idClienteBroxel:" + idClienteBroxel + " agrupacionClientesId: " +
                                agrupacionClientesId + " detalleClientesBroxelId: " + detalleClientesBroxelId +
                                " clienteComisionId: " + clienteComisionId);

                                                               

                if (idClienteBroxel != 0)
                {
                    Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                    " DeleteData: Borrado clientesBroxelController");

                    var clientesBroxelController = new clientesBroxelsController();
                    clientesBroxelController.DeleteclientesBroxel(idClienteBroxel);
                }

                if (agrupacionClientesId != 0)
                {
                    Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                    " DeleteData: Borrado agrupacionClienteController");

                    var agrupacionClienteController = new AgrupacionClientesController();
                    agrupacionClienteController.DeleteAgrupacionClientes(agrupacionClientesId);
                }

                if (detalleClientesBroxelId != 0)
                {
                    Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                    " DeleteData: Borrado detalleClientesBroxelController");

                    var detalleClientesBroxelController = new DetalleClientesBroxelsController();
                    detalleClientesBroxelController.DeleteDetalleClientesBroxel(detalleClientesBroxelId);
                }

                if (clienteComisionId != 0)
                {
                    Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                   " DeleteData: Borrado clienteComisionController");

                    var clienteComisionController = new ClientesComisionesController();
                    clienteComisionController.DeleteClientesComisiones(clienteComisionId);
                }


                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                   " Fin DeleteData ");

            }
            catch (Exception exception)
            {
                ErrorHandling.EscribeError(exception);

            }
        }


        /// <summary>
        /// Método de generar una ODT para emitir una cuenta en Credencial
        /// </summary>
        /// <param name="pClaveCliente">Clave de cliente.</param>
        /// <param name="pUsuarioCreacion">Usuario de creación de la ODT.</param>
        /// <param name="pEstado">The p estado.</param>
        /// <param name="pNombreSolicitante">The p nombre solicitante.</param>
        /// <param name="pEmailNotificacion">The p email notificacion.</param>
        /// <param name="pCodigoProducto">The p codigo producto.</param>
        /// <param name="pCodigoServicio">The p codigo servicio.</param>
        /// <param name="pPorcBonificacion">The p porc bonificacion.</param>
        /// <param name="pCodigoPeriodoBonificacion">The p codigo periodo bonificacion.</param>
        /// <param name="pCantidadCuotasBonificacion">The p cantidad cuotas bonificacion.</param>
        /// <param name="pFechaDesde">The p fecha desde.</param>
        /// <param name="pMarcaTpp">The p marca TPP.</param>
        /// <param name="pGrupoDeLiquidacion">The p grupo de liquidacion.</param>
        /// <param name="pHabilitaCompra">The p habilita compra.</param>
        /// <param name="pUsuarioGenera">The p usuario genera.</param>
        /// <param name="pNombreTarjetahabiente">The p nombre tarjetahabiente.</param>
        /// <param name="pCalle">The p calle.</param>
        /// <param name="pNumExterior">The p number exterior.</param>
        /// <param name="pNumInterior">The p number interior.</param>
        /// <param name="pLocalidad">The p localidad.</param>
        /// <param name="pColonia">The p colonia.</param>
        /// <param name="pTelefono">The p telefono.</param>
        /// <param name="pEstadoDetalleCuentas">The p estado detalle cuentas.</param>
        /// <param name="pCodigoProvincia">The p codigo provincia.</param>
        /// <param name="pCodigoPostal">The p codigo postal.</param>
        /// <param name="pTipoDeDocumento">The p tipo de documento.</param>
        /// <param name="pNumeroDeDocumento">The p numero de documento.</param>
        /// <param name="pEmail">The p email.</param>
        /// <param name="pDenominacionTarjeta">The p denominacion tarjeta.</param>
        /// <param name="pFechaDeNacimiento">The p fecha de nacimiento.</param>
        /// <param name="pSexo">The p sexo.</param>
        /// <param name="p4TaLinea">The p4 ta linea.</param>
        /// <returns></returns>
        public String GeneraOdt(String pClaveCliente, String pUsuarioCreacion, String pEstado, String pNombreSolicitante,
            String pEmailNotificacion, String pCodigoProducto, String pCodigoServicio, Decimal pPorcBonificacion,
            String pCodigoPeriodoBonificacion, String pCantidadCuotasBonificacion, DateTime pFechaDesde,
            String pMarcaTpp, String pGrupoDeLiquidacion, String pHabilitaCompra, String pUsuarioGenera,
            String pNombreTarjetahabiente, String pCalle, String pNumExterior, String pNumInterior, String pLocalidad,
            String pColonia, String pTelefono, String pEstadoDetalleCuentas, String pCodigoProvincia,
            String pCodigoPostal, String pTipoDeDocumento, String pNumeroDeDocumento, String pEmail,
            String pDenominacionTarjeta, DateTime pFechaDeNacimiento, String pSexo, String p4TaLinea)
        {

            var db = new broxelco_rdgEntities();
            try
            {
                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " + " Entro a GeneraOdt ");

                //Se genera en transaccion
                using (var dbTrans = db.Database.BeginTransaction())
                {
                    try
                    {
                        //Se genera el folio de la ODT     
                           
                        var query =
                            "SELECT COALESCE(substr(max(`Folio`),8,6)+1,1,substr(max(`Folio`),8,6)+1) AS folio FROM OrdenDeTrabajo WHERE SUBSTRING(`Folio`,1,7)='" +
                            DateTime.Now.ToString("yyMM") + "ODT'";

                        string folio = "";

                        var firstOrDefault = db.Database.SqlQuery<String>(query).FirstOrDefault();
                        if (firstOrDefault != null)
                        {
                            folio = DateTime.Now.ToString("yyMM") + "ODT" + firstOrDefault.PadLeft(6).Replace(' ', '0');
                        }

                        Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " + "Folio " + folio);

                        //Se almacena la información en OrdenDeTrabajo
                        var file = "files/" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 13) +
                                   "-EALTSOC_651_" +
                                   DateTime.Now.ToString("yyyymmdd");

                        

                        var ordenDeTrabajoController = new OrdenDeTrabajoesController();
                        var ordenDeTrabajo = new OrdenDeTrabajo
                        {
                            Folio = folio,
                            ClaveCliente = string.IsNullOrEmpty(pClaveCliente) ? "" : pClaveCliente,
                            UsuarioCreacion = string.IsNullOrEmpty(pUsuarioCreacion) ? " " : pUsuarioCreacion,
                            FechaCreacion = DateTime.Now,
                            Estado = string.IsNullOrEmpty(pEstado) ? "ON HOLD" : pEstado,
                            NombreSolicitante = string.IsNullOrEmpty(pNombreSolicitante) ? " " : pNombreSolicitante.ToUpper(),
                            EmailNotificacion = string.IsNullOrEmpty(pEmailNotificacion) ? " " : pEmailNotificacion.ToUpper(),
                            CodigoDeProducto = string.IsNullOrEmpty(pCodigoProducto) ? " " : pCodigoProducto.ToUpper(),
                            CodigoServicio = string.IsNullOrEmpty(pCodigoServicio) ? "220" : pCodigoServicio.ToUpper(),
                            PorcentajeBonificacion = pPorcBonificacion,
                            CodigoPeriodoBonificacion =
                                string.IsNullOrEmpty(pCodigoPeriodoBonificacion) ? "0" : pCodigoPeriodoBonificacion.ToUpper(),
                            CantidadCuotasBonificacion = string.IsNullOrEmpty(pCantidadCuotasBonificacion) ? "0" : pCantidadCuotasBonificacion,
                            FechaDesde = pFechaDesde,
                            MarcaTPP = "N",//string.IsNullOrEmpty(pMarcaTpp) ? "N" : pMarcaTpp,
                            GrupoDeLiquidacion = string.IsNullOrEmpty(pGrupoDeLiquidacion) ? "9" : pGrupoDeLiquidacion.ToUpper(),
                            HabilitaCompra = string.IsNullOrEmpty(pHabilitaCompra) ? "S" : pHabilitaCompra.ToUpper(),
                            UsuarioGenera = string.IsNullOrEmpty(pUsuarioGenera) ? "MYOWebService".ToUpper() : pUsuarioGenera.ToUpper(),
                            FechaGenera = DateTime.Now
                        };

                        ordenDeTrabajoController.PostOrdenDeTrabajo(ordenDeTrabajo);
                        var id = ordenDeTrabajo.Id;

                        Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " + " ID ODT: " + id);

                        file = file + id.ToString(CultureInfo.InvariantCulture).PadLeft(6).Replace(" ", "0") + ".txt";
                        ordenDeTrabajo.File = file;
                        ordenDeTrabajoController.PutOrdenDeTrabajo(id, ordenDeTrabajo);

                        Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " + "File Name:" +
                                        file);

                        //Se inserta DetalleCuentasOrdenDeTrabajo
                        var numExterior = string.IsNullOrEmpty(pNumExterior) ? "" : pNumExterior.ToUpper();
                        var catEstadosController = new estadosCredencialsController();
                        var catCodProvinciaController = new ciudadesCredencialsController();

                        var estadosCredencial =
                            catEstadosController.GetestadosCredencial()
                                .FirstOrDefault(e => e.Estado.Contains(pEstadoDetalleCuentas.ToUpper()));

                        var ciudadesCredencial =
                            catCodProvinciaController.GetciudadesCredencial()
                                .FirstOrDefault(
                                    p => p.IdEstado == estadosCredencial.id && p.Ciudad.Contains(pCodigoProvincia.ToUpper()));

                        if (ciudadesCredencial != null)
                        {
                            var detalleCuentasOrdenDeTrabajoController = new DetalleCuentasOrdenDeTrabajoesController();

                            var detalleCuentasOrdenDeTrabajo = new DetalleCuentasOrdenDeTrabajo
                            {
                                IdOrdenTrabajo = (int?) id,
                                FolioOrdenTrabajo = folio.ToUpper(),
                                CodigoDeProducto =
                                    string.IsNullOrEmpty(pCodigoProducto) ? "" : pCodigoProducto.ToUpper(),
                                CodigoServicio = string.IsNullOrEmpty(pCodigoServicio) ? "" : pCodigoServicio.ToUpper(),
                                NombreTarjetahabiente =
                                    string.IsNullOrEmpty(pNombreTarjetahabiente) ? "" : pNombreTarjetahabiente.ToUpper(),
                                CalleYNumPuerta =
                                    string.IsNullOrEmpty(pCalle) ? "" : pCalle.ToUpper() + " " + numExterior.ToUpper(),
                                Interior = string.IsNullOrEmpty(pNumInterior) ? " " : pNumInterior.ToUpper(),
                                Localidad = string.IsNullOrEmpty(pLocalidad) ? " " : pLocalidad.ToUpper(),
                                Colonia = string.IsNullOrEmpty(pColonia) ? " " : pColonia.ToUpper(),
                                Telefono = string.IsNullOrEmpty(pTelefono) ? "5544330303" : pTelefono,
                                CodigoEstado = (int?) estadosCredencial.id,
                                CodigoProvincia = (int?) ciudadesCredencial.Id,
                                CodigoPostal = string.IsNullOrEmpty(pCodigoPostal) ? "" : pCodigoPostal.ToUpper(),
                                TipoDeDocumento =
                                    string.IsNullOrEmpty(pTipoDeDocumento) ? "" : pTipoDeDocumento.ToUpper(),
                                NumeroDeDocumento =
                                    string.IsNullOrEmpty(pNumeroDeDocumento) ? "" : pNumeroDeDocumento.ToUpper(),
                                GrupoDeLiquidacion =
                                    string.IsNullOrEmpty(pGrupoDeLiquidacion) ? "9" : pGrupoDeLiquidacion.ToUpper(),
                                HabilitaCompra = string.IsNullOrEmpty(pHabilitaCompra) ? "S" : pHabilitaCompra.ToUpper(),
                                Email = string.IsNullOrEmpty(pEmail) ? " " : pEmail.ToUpper()
                            };

                            detalleCuentasOrdenDeTrabajoController.PostDetalleCuentasOrdenDeTrabajo(
                                detalleCuentasOrdenDeTrabajo);
                            var idDetalleCuentasOrdenTrabajo = detalleCuentasOrdenDeTrabajo.Id;

                            Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                            "detalleCuentasOrdenDeTrabajo.Id" + detalleCuentasOrdenDeTrabajo.Id);


                            //Se almacena DetalleTarjetasOrdenDeTrabajo
                            var detalleTarjetasOrdenDeTrabajoController =
                                new DetalleTarjetasOrdenDeTrabajoesController();
                            var detalleTarjetasOrdenDeTrabajo = new DetalleTarjetasOrdenDeTrabajo
                            {
                                IdOrdenTrabajo = (int?) id,
                                IdCuentaOrdenDeTrabajo = (int?) idDetalleCuentasOrdenTrabajo,
                                FolioOrdenTrabajo = folio.ToUpper(),
                                Tipo = "TITULAR",
                                DenominacionTarjeta =
                                    string.IsNullOrEmpty(pDenominacionTarjeta) ? " " : pDenominacionTarjeta.ToUpper(),
                                FechaDeNacimiento = pFechaDeNacimiento,
                                Sexo = string.IsNullOrEmpty(pSexo) ? " " : pSexo.ToUpper(),
                                PorcentajeBonificacion = pPorcBonificacion,
                                CodigoPeriodoBonificacion =
                                    string.IsNullOrEmpty(pCodigoPeriodoBonificacion)
                                        ? "0"
                                        : pCodigoPeriodoBonificacion.ToUpper(),
                                CantidadCuotasBonificacion =
                                    string.IsNullOrEmpty(pCantidadCuotasBonificacion)
                                        ? "0"
                                        : pCantidadCuotasBonificacion.ToUpper(),
                                FechaDesde = pFechaDesde,
                                C4ta_linea = string.IsNullOrEmpty(p4TaLinea) ? " " : p4TaLinea.ToUpper(),
                                MarcaTPP = "N", //string.IsNullOrEmpty(pMarcaTpp) ? "N" : pMarcaTpp,
                                NumeroDeDocumento =
                                    string.IsNullOrEmpty(pNumeroDeDocumento) ? " " : pNumeroDeDocumento.ToUpper(),
                                CodigoDeProducto =
                                    string.IsNullOrEmpty(pCodigoProducto) ? "" : pCodigoProducto.ToUpper()
                            };

                            detalleTarjetasOrdenDeTrabajoController.PostDetalleTarjetasOrdenDeTrabajo(
                                detalleTarjetasOrdenDeTrabajo);

                            Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                            " Insert DetalleTarjetasOrdenDeTrabajo");

                            Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                            "Entrada a ArmaArchivo");

                            ArmaArchivo(folio);

                            Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                            "Salida de ArmaArchivo");

                            dbTrans.Commit();

                            var wsBitacora = new wsBitacora.WsBitacorasClient();
                            wsBitacora.InsertaBitacora("srvBroxel", "broxelco_rdgEntities", "",
                                "GeneraOdt", "Generación de ODT con folio: " + folio, GetIPAdd(), pUsuarioCreacion);

                            return folio;
                        }
                        dbTrans.Rollback();
                        return "Error No se encontró el código de la ciudad enviada";
                    }

                    catch (DbEntityValidationException entityValidationException)
                    {
                        foreach (var validationErrors in entityValidationException.EntityValidationErrors)
                        {
                            var exceptionText = new StringBuilder();
                            exceptionText.AppendFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", validationErrors.Entry.Entity.GetType().Name, validationErrors.Entry.State);
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                exceptionText.AppendFormat("Property: {0} Error: {1}",
                                    validationError.PropertyName,
                                    validationError.ErrorMessage);
                            }
                            ErrorHandling.EscribeError(exceptionText.ToString());
                        }
                        dbTrans.Rollback();

                        return "Error en GeneraOdt1";
                    }

                    catch (Exception exception)
                    {
                        dbTrans.Rollback();
                        ErrorHandling.EscribeError(exception);
                        return "Error en GeneraOdt2";
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorHandling.EscribeError(ex);
                return "Error en GeneraOdt3";
            }
        }


        /// <summary>
        /// Método de generar una ODT para emitir una cuenta en Credencial
        /// </summary>
        /// <param name="pClaveCliente">Clave de cliente.</param>
        /// <param name="pUsuarioCreacion">Usuario de creación de la ODT.</param>
        /// <param name="pEstado">The p estado.</param>
        /// <param name="pNombreSolicitante">The p nombre solicitante.</param>
        /// <param name="pEmailNotificacion">The p email notificacion.</param>
        /// <param name="pCodigoProducto">The p codigo producto.</param>
        /// <param name="pCodigoServicio">The p codigo servicio.</param>
        /// <param name="pPorcBonificacion">The p porc bonificacion.</param>
        /// <param name="pCodigoPeriodoBonificacion">The p codigo periodo bonificacion.</param>
        /// <param name="pCantidadCuotasBonificacion">The p cantidad cuotas bonificacion.</param>
        /// <param name="pFechaDesde">The p fecha desde.</param>
        /// <param name="pMarcaTpp">The p marca TPP.</param>
        /// <param name="pGrupoDeLiquidacion">The p grupo de liquidacion.</param>
        /// <param name="pHabilitaCompra">The p habilita compra.</param>
        /// <param name="pUsuarioGenera">The p usuario genera.</param>
        /// <param name="pNombreTarjetahabiente">The p nombre tarjetahabiente.</param>
        /// <param name="pCalle">The p calle.</param>
        /// <param name="pNumExterior">The p number exterior.</param>
        /// <param name="pNumInterior">The p number interior.</param>
        /// <param name="pLocalidad">The p localidad.</param>
        /// <param name="pColonia">The p colonia.</param>
        /// <param name="pTelefono">The p telefono.</param>
        /// <param name="pEstadoDetalleCuentas">The p estado detalle cuentas.</param>
        /// <param name="pCodigoProvincia">The p codigo provincia.</param>
        /// <param name="pCodigoPostal">The p codigo postal.</param>
        /// <param name="pTipoDeDocumento">The p tipo de documento.</param>
        /// <param name="pNumeroDeDocumento">The p numero de documento.</param>
        /// <param name="pEmail">The p email.</param>
        /// <param name="pDenominacionTarjeta">The p denominacion tarjeta.</param>
        /// <param name="pFechaDeNacimiento">The p fecha de nacimiento.</param>
        /// <param name="pSexo">The p sexo.</param>
        /// <param name="p4TaLinea">The p4 ta linea.</param>
        /// <returns></returns>
        public String GeneraOdtAdicional(String pClaveCliente, String pUsuarioCreacion, String pEstado, String pNombreSolicitante,
            String pEmailNotificacion, String pCodigoProducto, String pCodigoServicio, Decimal pPorcBonificacion,
            String pCodigoPeriodoBonificacion, String pCantidadCuotasBonificacion, DateTime pFechaDesde,
            String pMarcaTpp, String pGrupoDeLiquidacion, String pHabilitaCompra, String pUsuarioGenera,
            String pNombreTarjetahabiente, String pCalle, String pNumExterior, String pNumInterior, String pLocalidad,
            String pColonia, String pTelefono, String pEstadoDetalleCuentas, String pCodigoProvincia,
            String pCodigoPostal, String pTipoDeDocumento, String pNumeroDeDocumento, String pEmail,
            String pDenominacionTarjeta, DateTime pFechaDeNacimiento, String pSexo, String p4TaLinea, String CuentaTitular, Int32 pidMaquila)
        {
           
            var db = new broxelco_rdgEntities();
            try
            {
                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " + " Entro a GeneraOdt ");

                //Se genera en transaccion
                
                using (var dbTrans = db.Database.BeginTransaction())
                {
                    try
                    {
                        //Se genera el folio de la ODT     

                        var query = "SELECT COALESCE(substr(max(`Folio`),8,6)+1,1,substr(max(`Folio`),8,6)+1) AS folio FROM OrdenDeTrabajo WHERE SUBSTRING(`Folio`,1,7)='" +
                            DateTime.Now.ToString("yyMM") + "ODT'";

                        string folio = "";

                        var firstOrDefault = db.Database.SqlQuery<Double>(query).FirstOrDefault();
                        if (firstOrDefault != 0)
                        {
                            folio = DateTime.Now.ToString("yyMM") + "ODT" + firstOrDefault.ToString().PadLeft(6).Replace(' ', '0');
                        }

                        Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " + "Folio " + folio);

                        //Se almacena la información en OrdenDeTrabajo
                        var file = "files/" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 13) +
                                   "-EALTSOC_651_" +
                                   DateTime.Now.ToString("yyyymmdd");



                        var ordenDeTrabajoController = new OrdenDeTrabajoesController();
                        var ordenDeTrabajo = new OrdenDeTrabajo
                        {
                            Folio = folio,
                            ClaveCliente = string.IsNullOrEmpty(pClaveCliente) ? "" : pClaveCliente,
                            UsuarioCreacion = string.IsNullOrEmpty(pUsuarioCreacion) ? " " : pUsuarioCreacion,
                            FechaCreacion = DateTime.Now,
                            Estado = string.IsNullOrEmpty(pEstado) ? "ON HOLD" : pEstado,
                            NombreSolicitante = string.IsNullOrEmpty(pNombreSolicitante) ? " " : pNombreSolicitante.ToUpper(),
                            EmailNotificacion = string.IsNullOrEmpty(pEmailNotificacion) ? " " : pEmailNotificacion.ToUpper(),
                            CodigoDeProducto = string.IsNullOrEmpty(pCodigoProducto) ? " " : pCodigoProducto.ToUpper(),
                            CodigoServicio = string.IsNullOrEmpty(pCodigoServicio) ? "220" : pCodigoServicio.ToUpper(),
                            PorcentajeBonificacion = pPorcBonificacion,
                            CodigoPeriodoBonificacion =
                                string.IsNullOrEmpty(pCodigoPeriodoBonificacion) ? "0" : pCodigoPeriodoBonificacion.ToUpper(),
                            CantidadCuotasBonificacion = string.IsNullOrEmpty(pCantidadCuotasBonificacion) ? "0" : pCantidadCuotasBonificacion,
                            FechaDesde = pFechaDesde,
                            MarcaTPP = "N",//string.IsNullOrEmpty(pMarcaTpp) ? "N" : pMarcaTpp,
                            GrupoDeLiquidacion = string.IsNullOrEmpty(pGrupoDeLiquidacion) ? "9" : pGrupoDeLiquidacion.ToUpper(),
                            HabilitaCompra = string.IsNullOrEmpty(pHabilitaCompra) ? "S" : pHabilitaCompra.ToUpper(),
                            UsuarioGenera = string.IsNullOrEmpty(pUsuarioGenera) ? "MYOWebService".ToUpper() : pUsuarioGenera.ToUpper(),
                            FechaGenera = DateTime.Now
                        };

                        ordenDeTrabajoController.PostOrdenDeTrabajo(ordenDeTrabajo);
                        var id = ordenDeTrabajo.Id;

                        Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " + " ID ODT: " + id);

                        file = file + id.ToString(CultureInfo.InvariantCulture).PadLeft(6).Replace(" ", "0") + ".txt";
                        ordenDeTrabajo.File = file;
                        ordenDeTrabajoController.PutOrdenDeTrabajo(id, ordenDeTrabajo);

                        Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " + "File Name:" +
                                        file);

                        //Se inserta DetalleCuentasOrdenDeTrabajo
                        var numExterior = string.IsNullOrEmpty(pNumExterior) ? "" : pNumExterior.ToUpper();
                        var catEstadosController = new estadosCredencialsController();
                        var catCodProvinciaController = new ciudadesCredencialsController();

                       
                        string textoNormalizado = pEstadoDetalleCuentas.Normalize(NormalizationForm.FormD);
                        Regex reg = new Regex("[^a-zA-Z0-9 ]");
                        string estado = reg.Replace(textoNormalizado, "");


                        var estadosCredencial =
                            catEstadosController.GetestadosCredencial()
                                .FirstOrDefault(e => e.Estado.Contains(estado.ToUpper()));

                        if(estadosCredencial == null)
                        {
                            estadosCredencial = catEstadosController.GetestadosCredencial()
                               .FirstOrDefault(e => e.Estado.Contains("DISTRITO FEDERAL")); 
                        }
                        
                        var ciudadesCredencial =
                            catCodProvinciaController.GetciudadesCredencial()
                                .FirstOrDefault(
                                    p => p.IdEstado == estadosCredencial.id && p.Ciudad.Contains(pCodigoProvincia.ToUpper()));


                        if (ciudadesCredencial == null)
                        {
                            ciudadesCredencial =
                            catCodProvinciaController.GetciudadesCredencial()
                                .FirstOrDefault(
                                    p => p.IdEstado == estadosCredencial.id);
                        }

                        if (ciudadesCredencial != null)
                        {
                            var detalleCuentasOrdenDeTrabajoController = new DetalleCuentasOrdenDeTrabajoesController();

                            var detalleCuentasOrdenDeTrabajo = new DetalleCuentasOrdenDeTrabajo
                            {
                                IdOrdenTrabajo = (int?)id,
                                FolioOrdenTrabajo = folio.ToUpper(),
                                CodigoDeProducto =
                                    string.IsNullOrEmpty(pCodigoProducto) ? "" : pCodigoProducto.ToUpper(),
                                CodigoServicio = string.IsNullOrEmpty(pCodigoServicio) ? "" : pCodigoServicio.ToUpper(),
                                NombreTarjetahabiente =
                                    string.IsNullOrEmpty(pNombreTarjetahabiente) ? "" : pNombreTarjetahabiente.ToUpper(),
                                CalleYNumPuerta =
                                    string.IsNullOrEmpty(pCalle) ? "" : pCalle.ToUpper() + " " + numExterior.ToUpper(),
                                Interior = string.IsNullOrEmpty(pNumInterior) ? " " : pNumInterior.ToUpper(),
                                Localidad = string.IsNullOrEmpty(pLocalidad) ? " " : pLocalidad.ToUpper(),
                                Colonia = string.IsNullOrEmpty(pColonia) ? " " : pColonia.ToUpper(),
                                Telefono = string.IsNullOrEmpty(pTelefono) ? "5544330303" : pTelefono,
                                CodigoEstado = (int?)estadosCredencial.id,
                                CodigoProvincia = (int?)ciudadesCredencial.Id,
                                CodigoPostal = string.IsNullOrEmpty(pCodigoPostal) ? "" : pCodigoPostal.ToUpper(),
                                TipoDeDocumento =
                                    string.IsNullOrEmpty(pTipoDeDocumento) ? "" : pTipoDeDocumento.ToUpper(),
                                NumeroDeDocumento =
                                    string.IsNullOrEmpty(pNumeroDeDocumento) ? "" : pNumeroDeDocumento.ToUpper(),
                                GrupoDeLiquidacion =
                                    string.IsNullOrEmpty(pGrupoDeLiquidacion) ? "9" : pGrupoDeLiquidacion.ToUpper(),
                                HabilitaCompra = string.IsNullOrEmpty(pHabilitaCompra) ? "S" : pHabilitaCompra.ToUpper(),
                                Email = string.IsNullOrEmpty(pEmail) ? " " : pEmail.ToUpper(),
                                IdMaquila = pidMaquila,
                               
                            };

                            detalleCuentasOrdenDeTrabajoController.PostDetalleCuentasOrdenDeTrabajo(
                                detalleCuentasOrdenDeTrabajo);
                            var idDetalleCuentasOrdenTrabajo = detalleCuentasOrdenDeTrabajo.Id;

                            Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                            "detalleCuentasOrdenDeTrabajo.Id" + detalleCuentasOrdenDeTrabajo.Id);


                            //Se almacena DetalleTarjetasOrdenDeTrabajo
                            var detalleTarjetasOrdenDeTrabajoController =
                                new DetalleTarjetasOrdenDeTrabajoesController();
                            var detalleTarjetasOrdenDeTrabajo = new DetalleTarjetasOrdenDeTrabajo
                            {
                                IdOrdenTrabajo = (int?)id,
                                Cuenta = CuentaTitular,
                                IdCuentaOrdenDeTrabajo = (int?)idDetalleCuentasOrdenTrabajo,
                                FolioOrdenTrabajo = folio.ToUpper(),
                                Tipo = "ADICIONAL",
                                DenominacionTarjeta =
                                    string.IsNullOrEmpty(pDenominacionTarjeta) ? " " : pDenominacionTarjeta.ToUpper(),
                                FechaDeNacimiento = pFechaDeNacimiento,
                                Sexo = string.IsNullOrEmpty(pSexo) ? " " : pSexo.ToUpper(),
                                PorcentajeBonificacion = pPorcBonificacion,
                                CodigoPeriodoBonificacion =
                                    string.IsNullOrEmpty(pCodigoPeriodoBonificacion)
                                        ? "0"
                                        : pCodigoPeriodoBonificacion.ToUpper(),
                                CantidadCuotasBonificacion =
                                    string.IsNullOrEmpty(pCantidadCuotasBonificacion)
                                        ? "0"
                                        : pCantidadCuotasBonificacion.ToUpper(),
                                FechaDesde = pFechaDesde,
                                C4ta_linea = string.IsNullOrEmpty(p4TaLinea) ? " " : p4TaLinea.ToUpper(),
                                MarcaTPP = "N", //string.IsNullOrEmpty(pMarcaTpp) ? "N" : pMarcaTpp,
                                NumeroDeDocumento =
                                    string.IsNullOrEmpty(pNumeroDeDocumento) ? " " : pNumeroDeDocumento.ToUpper(),
                                CodigoDeProducto =
                                    string.IsNullOrEmpty(pCodigoProducto) ? "" : pCodigoProducto.ToUpper(),
                                IdRegistroTc = null
                            };

                            detalleTarjetasOrdenDeTrabajoController.PostDetalleTarjetasOrdenDeTrabajo(
                                detalleTarjetasOrdenDeTrabajo);

                            Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                            " Insert DetalleTarjetasOrdenDeTrabajo");

                            Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                            "Entrada a ArmaArchivo");

                            ArmaArchivoAdicionales(folio);

                            Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                            "Salida de ArmaArchivo");

                            dbTrans.Commit();

                            var wsBitacora = new wsBitacora.WsBitacorasClient();
                            wsBitacora.InsertaBitacora("srvBroxel", "broxelco_rdgEntities", "",
                                "GeneraOdt", "Generación de ODT con folio: " + folio, GetIPAdd(), pUsuarioCreacion);

                            //agregar a ftp
                            //

                            return folio;
                        }
                        dbTrans.Rollback();
                        return "Error No se encontró el código de la ciudad enviada";
                    }

                    catch (DbEntityValidationException entityValidationException)
                    {
                        foreach (var validationErrors in entityValidationException.EntityValidationErrors)
                        {
                            var exceptionText = new StringBuilder();
                            exceptionText.AppendFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", validationErrors.Entry.Entity.GetType().Name, validationErrors.Entry.State);
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                exceptionText.AppendFormat("Property: {0} Error: {1}",
                                    validationError.PropertyName,
                                    validationError.ErrorMessage);
                            }
                            ErrorHandling.EscribeError(exceptionText.ToString());
                        }
                        dbTrans.Rollback();
                        ErrorHandling.EscribeError("Error en GeneraOdt1: " + entityValidationException.Message);
                        return "Error en GeneraOdt1";
                    }

                    catch (Exception exception)
                    {
                        dbTrans.Rollback();
                        ErrorHandling.EscribeError("Error en GeneraOdt2: " + exception.Message);
                        return "Error en GeneraOdt2";
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorHandling.EscribeError("Error en GeneraOdt3: " + ex.Message);
                return "Error en GeneraOdt3";
            }
        }




        /// <summary>
        /// Obtiene el IdUsuarioOnlineBroxel de la tabla accessos_clientes.
        /// </summary>
        /// <param name="idClientesBroxel">Id de la tabla clientesBroxel.</param>
        /// <returns>Id correspondiente o -1 en caso de error</returns>
        public Int64 GetUsuarioOnline(int idClientesBroxel)
        {
            try
            {
                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                "Inicio GetUsuarioOnline: idClientesBroxel: " + idClientesBroxel);

                //Se optimiza el método de obtención del Id

                #region Comentada
                //var clienteBroxelController = new clientesBroxelsController();

                //Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                //                "GetUsuarioOnline: Antes de clienteBroxelController.GetclientesBroxel");
 
                //var clienteBroxel = clienteBroxelController.GetclientesBroxel().FirstOrDefault(c=> c.id == idClientesBroxel);                

                //if (clienteBroxel != null)
                //{
                //    Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                //                "GetUsuarioOnline:  clienteBroxel: " + SerializeToXml.SerializeObject(clienteBroxel));

                //    Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                //                "GetUsuarioOnline: Antes de maquilaController.Getmaquila");

                //    var maquilaController = new maquilasController();

                //    var maquila =
                //        maquilaController.Getmaquila()
                //            .FirstOrDefault(m => m.clave_cliente == clienteBroxel.claveCliente);

                //    if (maquila != null)
                //    {
                //        Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                //                "GetUsuarioOnline:  maquila: " + SerializeToXml.SerializeObject(maquila));

                //        var accesosclientesController = new AccessosClientesController();

                //        var accesoCliente =
                //            accesosclientesController.Getaccessos_clientes()
                //                .FirstOrDefault(c => c.IdMaquila == maquila.id);
                        
                //        if (accesoCliente != null)
                //        {
                //            Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                //                "GetUsuarioOnline:  accesoCliente: " + SerializeToXml.SerializeObject(accesoCliente));

                //            return accesoCliente.IdUsuarioOnlineBroxel;
                //        }

                //        Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                //                "GetUsuarioOnline:  No se encontró clienteBroxel:maquila:accesoCliente");


                //        return -1;

                //    }

                //    Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                //                "GetUsuarioOnline:  No se encontró clienteBroxel:maquila");

                //    return -1;

                //}

                #endregion

                var dbContext = new broxelco_rdgEntities();

                var result = (from cl in dbContext.clientesBroxel
                    join maq in dbContext.maquila on cl.claveCliente equals maq.clave_cliente
                    join ac in dbContext.accessos_clientes on maq.id equals ac.IdMaquila
                    where cl.id == idClientesBroxel
                    select ac.IdUsuarioOnlineBroxel).SingleOrDefault();

                return result;


            }
            catch (Exception exception)
            {
                ErrorHandling.EscribeError(exception);
                return -1;
            }
            
        }

        /// <summary>
        /// Validas the estatus odt.
        /// </summary>
        /// <param name="pFolioOdt">The p folio odt.</param>
        /// <returns></returns>
        public EstatusOdtResponse ValidaEstatusOdt(String pFolioOdt)
            {
            try
            {
                var detalleTarjetasOrdenTrabajoController = new DetalleTarjetasOrdenDeTrabajoesController();
                var estatusOdt = new EstatusOdtResponse();
                var detalleTarjeta =
                    detalleTarjetasOrdenTrabajoController.GetDetalleTarjetasOrdenDeTrabajo().FirstOrDefault(dt => dt.FolioOrdenTrabajo == pFolioOdt);

                if (detalleTarjeta != null) estatusOdt.NumeroCuenta = detalleTarjeta.Cuenta;
                
                //ToDo: Obtener la Clabe de Maquila
                var db = new broxelco_rdgEntities();
                var clabe = (from det in db.DetalleCuentasOrdenDeTrabajo
                    join maq in db.maquila on det.IdMaquila equals maq.id
                    where det.FolioOrdenTrabajo == pFolioOdt
                    select new {maq.CLABE, maq.nro_tarjeta} 
                    ).ToArray();

                estatusOdt.Clabe = clabe[0].ToString();

                //ToDo: Obtener el número de tarjeta
                estatusOdt.Tarjeta = clabe[1].ToString();

                return estatusOdt;

            }
            catch (Exception exception)
            {
                ErrorHandling.EscribeError(exception);

                var estatusOdt = new EstatusOdtResponse {NumeroCuenta = "Error en ValidaEstatusOdt"};
                return estatusOdt;
            }
        }

        /// <summary>
        /// Armas the archivo.
        /// </summary>
        /// <param name="folioOdt">The folio odt.</param>
        /// <returns></returns>
        public bool ArmaArchivo(string folioOdt)
        {
            try
            {
                var path = ConfigurationManager.AppSettings["FilePath"];

                var file = "" + "EALTSOC_651_" +
                                   DateTime.Now.ToString("yyyyMMdd");

                
                //Se obtiene la información de las tablas
                var ordenDeTrabajoController = new OrdenDeTrabajoesController();
                var odt = ordenDeTrabajoController.GetOrdenDeTrabajo().FirstOrDefault(o => o.Folio == folioOdt);
                if (odt != null && (odt.Estado.ToUpper().Equals("NUEVO") || odt.Estado.ToUpper().Equals("GENERANDO") || odt.Estado.ToUpper().Equals("ON HOLD")))
                {
                    file = file + "_" + odt.Id.ToString(CultureInfo.InvariantCulture).PadLeft(6).Replace(" ", "0") + ".txt";

                    if (!File.Exists(path + "\\" + file))
                    {
                        int contador = 0;
                        using (var sw = File.CreateText(path +"\\" + file))
                        {
                            //Se obtiene la información de los catálogos
                            var estadosCredencialController = new estadosCredencialsController();
                            var ciudadesCredencialController = new ciudadesCredencialsController();
                            var productosBroxelController = new productos_broxelController();
                            var detalleCuentasOdtController = new DetalleCuentasOrdenDeTrabajoesController();
                            var detalleTarjetaCuentasOdtController = new DetalleTarjetasOrdenDeTrabajoesController();

                            var detTarjCtasOdt =
                                detalleTarjetaCuentasOdtController.GetDetalleTarjetasOrdenDeTrabajo()
                                    .FirstOrDefault(t => t.IdOrdenTrabajo == odt.Id);

                            var detCtasOdt =
                                detalleCuentasOdtController.GetDetalleCuentasOrdenDeTrabajo().Where(d => d.IdOrdenTrabajo == odt.Id);

                            foreach (DetalleCuentasOrdenDeTrabajo detalle in detCtasOdt)
                            {
                                var linea = "";
                                var letraEdo = estadosCredencialController.GetestadosCredencial().FirstOrDefault(e => e.id == detalle.CodigoEstado);
                                var letraEstado = "";
                                if (letraEdo != null)
                                {
                                    letraEstado = letraEdo.LetraEstado;
                                }

                                var codCiudad =
                                    ciudadesCredencialController.GetciudadesCredencial()
                                        .FirstOrDefault(c => c.Id == detalle.CodigoProvincia);
                                var ciudad = "";

                                if (codCiudad != null)
                                {
                                    ciudad = codCiudad.CodigoCiudad;
                                }

                                var codAfinidad =
                                    productosBroxelController.Getproductos_broxel()
                                        .FirstOrDefault(p => p.codigo == detalle.CodigoDeProducto);
                                var codigoAfinidad = "0000";
                                var codigo100 = "0";

                                if (codAfinidad != null)
                                {
                                    codigoAfinidad = codAfinidad.tipo.Equals("Credito") ? "0000" : "0002";
                                    codigo100 = codAfinidad.tipo.Equals("Credito") ? "100" : "0";                                    
                                }

                                linea += ("C" + detalle.CodigoDeProducto);
                                contador++;
                                linea += (contador.ToString(CultureInfo.InvariantCulture).PadLeft(4, '0'));
                                linea += ("651000");
                                linea += (detalle.NombreTarjetahabiente.ToUpper().PadRight(35, ' '));
                                linea += (detalle.CalleYNumPuerta.ToUpper().PadRight(30, ' '));
                                linea += (detalle.Interior != null ? detalle.Interior.ToUpper().PadRight(10, ' ') : " ".PadRight(10, ' '));
                                linea += (detalle.Localidad != null ? detalle.Localidad.ToUpper().PadRight(25, ' ') : " ".PadRight(25, ' '));
                                linea += (detalle.Colonia != null ? detalle.Colonia.ToUpper().PadRight(15, ' ') : " ".PadRight(15, ' '));
                                linea += (detalle.Telefono != null ? detalle.Telefono.ToUpper().PadRight(11, ' ') : " ".PadRight(11, ' '));
                                linea += (letraEstado.PadLeft(1, ' '));
                                linea += (detalle.CodigoPostal != null ? detalle.CodigoPostal.ToUpper().PadLeft(5, '0') : "0".PadLeft(5, '0'));
                                linea += (ciudad.PadLeft(3, ' '));
                                linea += (detalle.TipoDeDocumento != null ? detalle.TipoDeDocumento.ToUpper().PadRight(3, ' ') : " ".PadRight(3, ' '));
                                linea += ("0".PadRight(9, '0'));
                                linea += (" ".PadRight(13, ' '));
                                linea += (detalle.GrupoDeLiquidacion.ToUpper().PadRight(1, ' '));
                                linea += ("01");
                                linea += (codigoAfinidad.PadRight(4, ' '));
                                linea += (" ".PadRight(1, ' '));
                                linea += (codigo100.PadLeft(11, '0'));
                                linea += ("2 00");
                                linea += (" ");
                                linea += (detalle.CodigoServicio);
                                linea += (" ".PadRight(11, ' '));
                                linea += ("0".PadRight(11, '0'));
                                linea += (detalle.HabilitaCompra.PadRight(1, ' '));
                                linea += (" ");
                                linea += (" ");
                                linea += ("0".PadRight(9, '0'));
                                linea += (" ".PadRight(2, ' '));
                                linea += (" ".PadRight(11, ' '));
                                linea += (" ".PadRight(19, ' '));
                                linea += (codigo100.PadLeft(8, '0'));
                                linea += (" ".PadRight(2, ' '));
                                linea += ("0".PadRight(3, '0'));
                                linea += (" ".PadRight(20, ' '));
                                linea += (" ".PadRight(5, ' '));
                                linea += (" ".PadRight(3, ' '));
                                linea += (" ".PadRight(10, ' '));
                                linea += (" ".PadRight(7, ' '));
                                linea += (" ".PadRight(25, ' '));
                                linea += (detalle.Email != null ? detalle.Email.ToUpper().PadRight(60, ' ') : " ".PadRight(60, ' '));
                                linea += (detalle.NumeroDeDocumento != null ? detalle.NumeroDeDocumento.ToUpper().PadRight(30, ' ') : " ".PadRight(30, ' '));
                                linea += (" ".PadRight(85, ' '));
                                linea = linea.ToUpper();
                                sw.WriteLine(linea);

                                //Se inserta en la tabla de maquila en caso de que no exista
                                if (detalle.IdMaquila == null && detTarjCtasOdt != null)
                                {
                                    var maquilaController = new maquilasController();
                                    //Todo Insertar sexo y fecha de nacimiento en Maquila 
                                    var maquila = new maquila
                                    {
                                        nombre_titular = detalle.NombreTarjetahabiente,
                                        domicilio = detalle.CalleYNumPuerta,
                                        NumInterior = detalle.Interior,
                                        localidad = detalle.Localidad,
                                        codigo_postal = detalle.CodigoPostal,
                                        NumDocumento = detalle.NumeroDeDocumento,
                                        TipoDocumento = detalle.TipoDeDocumento,
                                        Telefono = detalle.Telefono,
                                        producto = detalle.CodigoDeProducto,
                                        Hijos = "N",
                                        Ocupacion = "EMPLEADO",
                                        nombre_tarjethabiente = detTarjCtasOdt.DenominacionTarjeta,
                                        grupo_cuenta = odt.GrupoDeLiquidacion,
                                        clave_cliente = odt.ClaveCliente,
                                        C4ta_linea = detTarjCtasOdt.C4ta_linea,
                                        email = detalle.Email,
                                        RFC = detTarjCtasOdt.RFCEmpleado,
                                        IdODTTarj = (int?) detalle.Id
                                    };

                                    //Se inserta en maquila
                                    maquilaController.Postmaquila(maquila);

                                    detalle.IdMaquila = maquila.id;
                                    detalleCuentasOdtController.PutDetalleCuentasOrdenDeTrabajo(detalle.Id, detalle);

                                    //Se obtiene la clabe correspondiente 
                                    var wsClabe = new wsClabe.WebServiceClabeClient();
                                    var clabe = wsClabe.GenerarClabeBroxel(maquila.id);

                                    if(clabe != null && !clabe.ErrorValidacion.Equals(string.Empty))
                                    {
                                        ErrorHandling.EscribeError("Error en Generación Clabe: " + clabe.ErrorValidacion);
                                    }
                                    else
                                    {
                                        //Se escribe la clabe en DetalleClientesBroxel
                                        var detalleclnteBrxlController = new DetalleClientesBroxelsController();
                                        var detclntbrxl = new DetalleClientesBroxel {CLABE = clabe.Clabe};

                                        detalleclnteBrxlController.PutDetalleClientesBroxel(detclntbrxl.Id, detclntbrxl);
                                    }
                                }
                                
                                //Se inserta el detalleTarjetasOrdenTrabajo
                                DetalleCuentasOrdenDeTrabajo detalle1 = detalle;
                                var detTarjOdt = detalleTarjetaCuentasOdtController.GetDetalleTarjetasOrdenDeTrabajo()
                                    .Where(t => t.IdCuentaOrdenDeTrabajo == detalle1.Id);

                                var contadorTarjetas = 0;
                                foreach (DetalleTarjetasOrdenDeTrabajo detTarjetasOdt in detTarjOdt)
                                {
                                    linea = "T";
                                    linea += detalle.CodigoDeProducto;
                                    contadorTarjetas++;
                                    linea += contadorTarjetas.ToString(CultureInfo.InvariantCulture).PadLeft(4, '0');
                                    linea += (detTarjetasOdt.Cuenta != null ? detTarjetasOdt.Cuenta.PadRight(9, '0') : "0".PadRight(9, '0'));
                                    linea += (detTarjetasOdt.DenominacionTarjeta != null ? detTarjetasOdt.DenominacionTarjeta.ToUpper().PadRight(21, ' ') : " ".PadRight(21, ' '));
                                    linea += (" ".PadRight(5, ' '));
                                    linea += (" ".PadRight(10, ' '));
                                    linea += (detTarjetasOdt.FechaDeNacimiento != null ? Convert.ToDateTime(detTarjetasOdt.FechaDeNacimiento).ToString("yyyyMMdd") : "");
                                    linea += (detTarjetasOdt.Sexo != null ? detTarjetasOdt.Sexo.ToUpper().PadRight(1, ' ') : " ".PadRight(1, ' '));
                                    linea += (detalle.TipoDeDocumento != null ? detalle.TipoDeDocumento.ToUpper().PadRight(3, ' ') : " ".PadRight(3, ' '));
                                    linea += ("0".PadRight(9, '0'));
                                    linea += (detTarjetasOdt.Tipo.Equals("TITULAR") ? "T" : "A");
                                    linea += ("1");
                                    linea += ("S");
                                    linea +=
                                        (detTarjetasOdt.PorcentajeBonificacion.ToString()
                                            .Replace(".", "")
                                            .PadLeft(5, '0'));
                                    linea += (detTarjetasOdt.CodigoPeriodoBonificacion.ToUpper().PadRight(1, ' '));
                                    linea += (detTarjetasOdt.CantidadCuotasBonificacion.ToUpper().PadLeft(2, ' '));
                                    linea += (Convert.ToDateTime(detTarjetasOdt.FechaDesde).ToString("yyyyMMdd"));
                                    linea += (" ".PadRight(20, ' '));
                                    linea += (" ".PadRight(1, ' '));
                                    linea += (" ".PadRight(1, ' '));                                    
                                    linea += (" ".PadRight(3, ' '));
                                    linea += (" ".PadRight(2, ' '));
                                    linea += ("0".PadRight(9, '0'));
                                    linea += "S";
                                    linea += (" ".PadRight(24, ' '));
                                    linea += (" ".PadRight(1, ' '));
                                    linea += (" ".PadRight(9, ' '));
                                    linea += (detTarjetasOdt.C4ta_linea.PadRight(20, ' '));
                                    linea += (" ".PadRight(30, ' '));
                                    linea += (" ".PadRight(10, ' '));
                                    linea += (" ".PadRight(11, ' '));
                                    linea += (" ".PadRight(25, ' '));
                                    linea += (" ".PadRight(15, ' '));
                                    linea += (" ".PadRight(1, ' '));
                                    linea += (" ".PadRight(5, ' '));
                                    linea += (" ".PadRight(3, ' '));
                                    linea += (detTarjetasOdt.MarcaTPP.ToUpper().PadRight(1, ' '));
                                    linea += (detTarjetasOdt.NumeroDeDocumento.ToUpper().PadRight(30, ' '));
                                    linea += (" ".PadLeft(40, ' '));
                                    linea += (odt.ClaveCliente.PadLeft(15, ' '));

                                    int idRegistroTc = 0;
                                    if (detTarjetasOdt.IdRegistroTc == null)
                                    {
                                        //Se inserta Registro_TC
                                        var registroTcController = new registro_tcController();
                                        var registrotc = new registro_tc
                                        {
                                            idmaquila = detalle.IdMaquila,
                                            IdODTTarj = detTarjetasOdt.IdOrdenTrabajo,
                                            nombre = detTarjetasOdt.DenominacionTarjeta,
                                            codigo_de_producto = odt.CodigoDeProducto,
                                            codigoServicio = odt.CodigoServicio,
                                            tipo = detTarjetasOdt.Tipo.Equals("TITULAR")?"00":"01",
                                            registroBroxel = detTarjetasOdt.IdTarjetaCliente
                                        };
                                        registroTcController.Postregistro_tc(registrotc);
                                        idRegistroTc = registrotc.id;

                                        detTarjetasOdt.IdRegistroTc = registrotc.id;
                                        detalleTarjetaCuentasOdtController.PutDetalleTarjetasOrdenDeTrabajo(
                                            detTarjetasOdt.Id, detTarjetasOdt);
                                    }
                                    else
                                    {
                                        idRegistroTc = (int) detTarjetasOdt.IdRegistroTc;
                                    }

                                    linea += (idRegistroTc.ToString(CultureInfo.InvariantCulture).PadLeft(15, ' '));
                                    linea += (" ".PadLeft(140, ' '));
                                    linea = linea.ToUpper();
                                    sw.WriteLine(linea);
                                    
                                }
                            }
                        }

                        //Se cambia el estatus de la ODT a procesado
                        odt.Estado = "PROCESADO WS";
                        odt.File = file;
                        ordenDeTrabajoController.PutOrdenDeTrabajo(odt.Id, odt);

                        

                    }

                }

                var wsBitacora = new wsBitacora.WsBitacorasClient();
                wsBitacora.InsertaBitacora("srvBroxel", "broxelco_rdgEntities", "",
                    "ArmaArchivo", "Armado de archivo ODT con folio: " + folioOdt, GetIPAdd(), "");

                return true;
            }
            catch (Exception exception)
            {
                ErrorHandling.EscribeError(exception);
                return false;
            }
        }


        /// <summary>
        /// Armas the archivo.
        /// </summary>
        /// <param name="folioOdt">The folio odt.</param>
        /// <returns></returns>
        public bool ArmaArchivoAdicionales(string folioOdt)
        {
            try
            {
                var path = ConfigurationManager.AppSettings["FilePath"];

                var file = "" + "EALTSOC_651_" +
                                   DateTime.Now.ToString("yyyyMMdd");


                //Se obtiene la información de las tablas
                var ordenDeTrabajoController = new OrdenDeTrabajoesController();
                var odt = ordenDeTrabajoController.GetOrdenDeTrabajo().FirstOrDefault(o => o.Folio == folioOdt);
                if (odt != null && (odt.Estado.ToUpper().Equals("NUEVO") || odt.Estado.ToUpper().Equals("GENERANDO") || odt.Estado.ToUpper().Equals("ON HOLD")))
                {
                    file = file + "_" + odt.Id.ToString(CultureInfo.InvariantCulture).PadLeft(6).Replace(" ", "0") + ".txt";

                    if (!File.Exists(path + "\\" + file))
                    {
                        int contador = 0;
                        using (var sw = File.CreateText(path + "\\" + file))
                        {
                            //Se obtiene la información de los catálogos
                            var estadosCredencialController = new estadosCredencialsController();
                            var ciudadesCredencialController = new ciudadesCredencialsController();
                            var productosBroxelController = new productos_broxelController();
                            var detalleCuentasOdtController = new DetalleCuentasOrdenDeTrabajoesController();
                            var detalleTarjetaCuentasOdtController = new DetalleTarjetasOrdenDeTrabajoesController();

                            var detTarjCtasOdt =
                                detalleTarjetaCuentasOdtController.GetDetalleTarjetasOrdenDeTrabajo()
                                    .FirstOrDefault(t => t.IdOrdenTrabajo == odt.Id);

                            var detCtasOdt =
                                detalleCuentasOdtController.GetDetalleCuentasOrdenDeTrabajo().Where(d => d.IdOrdenTrabajo == odt.Id);

                            foreach (DetalleCuentasOrdenDeTrabajo detalle in detCtasOdt)
                            {
                                var linea = "";
                                var letraEdo = estadosCredencialController.GetestadosCredencial().FirstOrDefault(e => e.id == detalle.CodigoEstado);
                                var letraEstado = "";
                                if (letraEdo != null)
                                {
                                    letraEstado = letraEdo.LetraEstado;
                                }

                                var codCiudad =
                                    ciudadesCredencialController.GetciudadesCredencial()
                                        .FirstOrDefault(c => c.Id == detalle.CodigoProvincia);
                                var ciudad = "";

                                if (codCiudad != null)
                                {
                                    ciudad = codCiudad.CodigoCiudad;
                                }

                                var codAfinidad =
                                    productosBroxelController.Getproductos_broxel()
                                        .FirstOrDefault(p => p.codigo == detalle.CodigoDeProducto);
                                var codigoAfinidad = "0000";
                                var codigo100 = "0";

                                if (codAfinidad != null)
                                {
                                    codigoAfinidad = codAfinidad.tipo.Equals("Credito") ? "0000" : "0002";
                                    codigo100 = codAfinidad.tipo.Equals("Credito") ? "100" : "0";
                                }

                                //linea += ("C" + detalle.CodigoDeProducto);
                                //contador++;
                                //linea += (contador.ToString(CultureInfo.InvariantCulture).PadLeft(4, '0'));
                                //linea += ("651000");
                                //linea += (detalle.NombreTarjetahabiente.ToUpper().PadRight(35, ' '));
                                //linea += (detalle.CalleYNumPuerta.ToUpper().PadRight(30, ' '));
                                //linea += (detalle.Interior != null ? detalle.Interior.ToUpper().PadRight(10, ' ') : " ".PadRight(10, ' '));
                                //linea += (detalle.Localidad != null ? detalle.Localidad.ToUpper().PadRight(25, ' ') : " ".PadRight(25, ' '));
                                //linea += (detalle.Colonia != null ? detalle.Colonia.ToUpper().PadRight(15, ' ') : " ".PadRight(15, ' '));
                                //linea += (detalle.Telefono != null ? detalle.Telefono.ToUpper().PadRight(11, ' ') : " ".PadRight(11, ' '));
                                //linea += (letraEstado.PadLeft(1, ' '));
                                //linea += (detalle.CodigoPostal != null ? detalle.CodigoPostal.ToUpper().PadLeft(5, '0') : "0".PadLeft(5, '0'));
                                //linea += (ciudad.PadLeft(3, ' '));
                                //linea += (detalle.TipoDeDocumento != null ? detalle.TipoDeDocumento.ToUpper().PadRight(3, ' ') : " ".PadRight(3, ' '));
                                //linea += ("0".PadRight(9, '0'));
                                //linea += (" ".PadRight(13, ' '));
                                //linea += (detalle.GrupoDeLiquidacion.ToUpper().PadRight(1, ' '));
                                //linea += ("01");
                                //linea += (codigoAfinidad.PadRight(4, ' '));
                                //linea += (" ".PadRight(1, ' '));
                                //linea += (codigo100.PadLeft(11, '0'));
                                //linea += ("2 00");
                                //linea += (" ");
                                //linea += (detalle.CodigoServicio);
                                //linea += (" ".PadRight(11, ' '));
                                //linea += ("0".PadRight(11, '0'));
                                //linea += (detalle.HabilitaCompra.PadRight(1, ' '));
                                //linea += (" ");
                                //linea += (" ");
                                //linea += ("0".PadRight(9, '0'));
                                //linea += (" ".PadRight(2, ' '));
                                //linea += (" ".PadRight(11, ' '));
                                //linea += (" ".PadRight(19, ' '));
                                //linea += (codigo100.PadLeft(8, '0'));
                                //linea += (" ".PadRight(2, ' '));
                                //linea += ("0".PadRight(3, '0'));
                                //linea += (" ".PadRight(20, ' '));
                                //linea += (" ".PadRight(5, ' '));
                                //linea += (" ".PadRight(3, ' '));
                                //linea += (" ".PadRight(10, ' '));
                                //linea += (" ".PadRight(7, ' '));
                                //linea += (" ".PadRight(25, ' '));
                                //linea += (detalle.Email != null ? detalle.Email.ToUpper().PadRight(60, ' ') : " ".PadRight(60, ' '));
                                //linea += (detalle.NumeroDeDocumento != null ? detalle.NumeroDeDocumento.ToUpper().PadRight(30, ' ') : " ".PadRight(30, ' '));
                                //linea += (" ".PadRight(85, ' '));
                                //linea = linea.ToUpper();
                                //sw.WriteLine(linea);

                               
                                //Se inserta el detalleTarjetasOrdenTrabajo
                                DetalleCuentasOrdenDeTrabajo detalle1 = detalle;
                                var detTarjOdt = detalleTarjetaCuentasOdtController.GetDetalleTarjetasOrdenDeTrabajo()
                                    .Where(t => t.IdCuentaOrdenDeTrabajo == detalle1.Id);

                                
                                var contadorTarjetas = 0;
                                foreach (DetalleTarjetasOrdenDeTrabajo detTarjetasOdt in detTarjOdt)
                                {
                                    //se agrega a maquila
                                    if (detalle.IdMaquila == null)
                                    {
                                        maquilasController maquilaC = new maquilasController();

                                        maquila maquiloca = new maquila();
                                        maquiloca.nombre_titular = detalle.NombreTarjetahabiente;
                                        maquiloca.domicilio = detalle.CalleYNumPuerta;
                                        maquiloca.localidad = detalle.Localidad;
                                        maquiloca.codigo_postal = detalle.CodigoPostal;
                                        maquiloca.NumDocumento = detalle.NumeroDeDocumento;
                                        maquiloca.TipoDocumento = detalle.TipoDeDocumento;
                                        maquiloca.Telefono = detalle.Telefono;
                                        maquiloca.Sexo = detTarjetasOdt.Sexo;
                                        maquiloca.FechaDeNacimiento = detTarjCtasOdt.FechaDeNacimiento;
                                        maquiloca.Hijos = "N";
                                        maquiloca.Ocupacion = "EMPLEADO";
                                        maquiloca.nombre_tarjethabiente = detTarjCtasOdt.DenominacionTarjeta;
                                        maquiloca.grupo_cuenta = odt.GrupoDeLiquidacion;
                                        maquiloca.producto = odt.CodigoDeProducto;
                                        maquiloca.clave_cliente = odt.ClaveCliente;
                                        maquiloca.C4ta_linea = detTarjCtasOdt.C4ta_linea;
                                        maquiloca.email = detalle.Email;
                                        maquiloca.RFC = detTarjCtasOdt.RFCEmpleado;
                                        maquiloca.CURP = detTarjCtasOdt.CURP;
                                        maquiloca.IMSS = detTarjCtasOdt.IMSS;
                                        maquiloca.IdODTTarj = (int)odt.Id;

                                        maquilaC.Postmaquila(maquiloca);
                                        var id = maquiloca.id;

                                        detalle1.IdMaquila = id;
                                        var responseDet = new DetalleCuentasOrdenDeTrabajoesController().PutDetalleCuentasOrdenDeTrabajo(detalle1.Id, detalle1);
                                    }

                                    linea = "T";
                                    linea += detalle.CodigoDeProducto;
                                    contadorTarjetas++;
                                    linea += contadorTarjetas.ToString(CultureInfo.InvariantCulture).PadLeft(4, '0');
                                    linea += (detTarjetasOdt.Cuenta != null ? detTarjetasOdt.Cuenta.PadRight(9, '0') : "0".PadRight(9, '0'));
                                    linea += (detTarjetasOdt.DenominacionTarjeta != null ? detTarjetasOdt.DenominacionTarjeta.ToUpper().PadRight(21, ' ') : " ".PadRight(21, ' '));
                                    linea += (" ".PadRight(5, ' '));
                                    linea += (" ".PadRight(10, ' '));
                                    linea += (detTarjetasOdt.FechaDeNacimiento != null ? Convert.ToDateTime(detTarjetasOdt.FechaDeNacimiento).ToString("yyyyMMdd") : "");
                                    linea += (detTarjetasOdt.Sexo != null ? detTarjetasOdt.Sexo.ToUpper().PadRight(1, ' ') : " ".PadRight(1, ' '));
                                    linea += (detalle.TipoDeDocumento != null ? detalle.TipoDeDocumento.ToUpper().PadRight(3, ' ') : " ".PadRight(3, ' '));
                                    linea += ("0".PadRight(9, '0'));
                                    linea += (detTarjetasOdt.Tipo.Equals("TITULAR") ? "T" : "A");
                                    linea += ("1");
                                    linea += ("S");
                                    linea +=
                                        (detTarjetasOdt.PorcentajeBonificacion.ToString()
                                            .Replace(".", "")
                                            .PadLeft(5, '0'));
                                    linea += (detTarjetasOdt.CodigoPeriodoBonificacion.ToUpper().PadRight(1, ' '));
                                    linea += (detTarjetasOdt.CantidadCuotasBonificacion.ToUpper().PadLeft(2, ' '));
                                    linea += (Convert.ToDateTime(detTarjetasOdt.FechaDesde).ToString("yyyyMMdd"));
                                    linea += (" ".PadRight(20, ' '));
                                    linea += (" ".PadRight(1, ' '));
                                    linea += (" ".PadRight(1, ' '));
                                    linea += (" ".PadRight(3, ' '));
                                    linea += (" ".PadRight(2, ' '));
                                    linea += ("0".PadRight(9, '0'));
                                    linea += "S";
                                    linea += (" ".PadRight(24, ' '));
                                    linea += (" ".PadRight(1, ' '));
                                    linea += (" ".PadRight(9, ' '));
                                    linea += (detTarjetasOdt.C4ta_linea.PadRight(20, ' '));
                                    linea += (" ".PadRight(30, ' '));
                                    linea += (" ".PadRight(10, ' '));
                                    linea += (" ".PadRight(11, ' '));
                                    linea += (" ".PadRight(25, ' '));
                                    linea += (" ".PadRight(15, ' '));
                                    linea += (" ".PadRight(1, ' '));
                                    linea += (" ".PadRight(5, ' '));
                                    linea += (" ".PadRight(3, ' '));
                                    linea += (detTarjetasOdt.MarcaTPP.ToUpper().PadRight(1, ' '));
                                    linea += (detTarjetasOdt.NumeroDeDocumento.ToUpper().PadRight(30, ' '));
                                    linea += (" ".PadLeft(40, ' '));
                                    linea += (odt.ClaveCliente.PadLeft(15, ' '));

                                    int idRegistroTc = 0;
                                    if (detTarjetasOdt.IdRegistroTc == null)
                                    {
                                        //Se inserta Registro_TC
                                        var registroTcController = new registro_tcController();
                                        var registrotc = new registro_tc
                                        {
                                            idmaquila = detalle.IdMaquila,
                                            IdODTTarj = detTarjetasOdt.IdOrdenTrabajo,
                                            nombre = detTarjetasOdt.DenominacionTarjeta,
                                            codigo_de_producto = odt.CodigoDeProducto,
                                            codigoServicio = odt.CodigoServicio,
                                            tipo = detTarjetasOdt.Tipo.Equals("TITULAR") ? "00" : "01",
                                            registroBroxel = detTarjetasOdt.IdTarjetaCliente == null ? "" : detTarjetasOdt.IdTarjetaCliente
                                        };

                                        

                                        registroTcController.Postregistro_tc(registrotc);
                                        idRegistroTc = registrotc.id;

                                        detTarjetasOdt.IdRegistroTc = registrotc.id;
                                        detalleTarjetaCuentasOdtController.PutDetalleTarjetasOrdenDeTrabajo(
                                            detTarjetasOdt.Id, detTarjetasOdt);
                                    }
                                    else
                                    {
                                        idRegistroTc = (int)detTarjetasOdt.IdRegistroTc;
                                    }

                                    linea += (idRegistroTc.ToString(CultureInfo.InvariantCulture).PadLeft(15, ' '));
                                    linea += (" ".PadLeft(140, ' '));
                                    linea = linea.ToUpper();
                                    sw.WriteLine(linea);

                                }
                            }
                        }

                        //Se cambia el estatus de la ODT a procesado
                        odt.Estado = "PROCESADO WS";
                        odt.File = file;
                        ordenDeTrabajoController.PutOrdenDeTrabajo(odt.Id, odt);
                    }

                }

                var wsBitacora = new wsBitacora.WsBitacorasClient();
                wsBitacora.InsertaBitacora("srvBroxel", "broxelco_rdgEntities", "",
                    "ArmaArchivo", "Armado de archivo ODT con folio: " + folioOdt, GetIPAdd(), "");

                return true;
            }
            catch (Exception exception)
            {
                ErrorHandling.EscribeError(exception);
                return false;
            }
        }


        /// <summary>
        /// Obtiene la clabe de la tabla de maquila
        /// </summary>
        /// <param name="pIdMaquila">Identificador de la tabla de maquila.</param>
        /// <returns>string con la clabe o vacio</returns>
        public String ObtenerMaquilaInfo(Int32 pIdMaquila)
        {
            try
            {
                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                " Inicia ActualizaClienteMaquila: pIdMaquila: " + pIdMaquila );
                var maqController = new maquilasController();

                var clabe = maqController.Getmaquila().FirstOrDefault(m=>m.id==pIdMaquila);

                if (clabe != null)
                    return clabe.CLABE;

                return "";

            }
            catch (Exception exception)
            {
                ErrorHandling.EscribeError(exception);
                return "";
            }
        }

        /// <summary>
        /// Actualizas the cliente maquila.
        /// </summary>
        /// <param name="pIdMaquila">The p identifier maquila.</param>
        /// <param name="pIdCliente">The p identifier cliente.</param>
        /// <param name="pClaveCliente">The p clave cliente.</param>
        /// <param name="pUsuario">The p usuario.</param>
        /// <returns></returns>
        public Boolean ActualizaClienteMaquila(Int32 pIdMaquila, Int32 pIdCliente, String pClaveCliente, String pUsuario)
        {
            try
            {
                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                " Inicia ActualizaClienteMaquila: pIdMaquila: " + pIdMaquila + " pClaveCliente: " + pClaveCliente);



                var maquilaController = new maquilasController();
                var maquila = maquilaController.Getmaquila().FirstOrDefault(m=> m.id == pIdMaquila);

                if (maquila != null)
                {
                    maquila.clave_cliente = pClaveCliente;
                    maquilaController.Putmaquila(pIdMaquila, maquila);

                    var wsBitacora = new wsBitacora.WsBitacorasClient();
                    var result = wsBitacora.InsertaBitacora("srvBroxel", "broxelco_rdgEntities", "maquila",
                        "Actualizacion Cliente", "Actualizacion Cliente Maquila", GetIPAdd(), pUsuario);
                    return true;
                }
                return false;

            }
            catch (DbEntityValidationException entityValidationException)
            {                
                foreach (var validationErrors in entityValidationException.EntityValidationErrors)
                {
                    var exceptionText = new StringBuilder();
                    exceptionText.AppendFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", validationErrors.Entry.Entity.GetType().Name, validationErrors.Entry.State);
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        exceptionText.AppendFormat("Property: {0} Error: {1}",
                            validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                    ErrorHandling.EscribeError(exceptionText.ToString());
                }
                return false;
            }
            catch (Exception exception)
            {
                ErrorHandling.EscribeError(exception);
                if (exception.InnerException != null)
                {
                    ErrorHandling.EscribeError(exception.InnerException.Message);
                }
                return false;
            }
            
        }

        public Int64 Login(Int64 idUser)
        {
            try
            {
                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " + " Login: idUser: " +
                                idUser);

                //Se genera el IdUser seguro
                var secureController = new SecureLoginsController();               

                return secureController.GetIdUserSecure(idUser);
            }
            catch (Exception exception)
            {
                ErrorHandling.EscribeError(exception);
                return 0;
            }
        }

        public Int64 ValidateUser(Int64 idSecureId)
        {
            try
            {
                //Se valida si el usuario proporcionado es válido
                var secureController = new SecureLoginsController();

                return secureController.GetIdUserValid(idSecureId);
            }
            catch (Exception exception)
            {
                ErrorHandling.EscribeError(exception);
                return 0;
            }
        }

        private string GetIPAdd()
        {
            var visitorIpAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (String.IsNullOrEmpty(visitorIpAddress))
                visitorIpAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (string.IsNullOrEmpty(visitorIpAddress))
                visitorIpAddress = HttpContext.Current.Request.UserHostAddress;

            if (string.IsNullOrEmpty(visitorIpAddress) || visitorIpAddress.Trim() == "::1")
            {

                visitorIpAddress = "::1";
            }

            return visitorIpAddress;
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }        
    }

    public static class ErrorHandling
    {
        /// <summary>
        /// Escribe en un log la excepción del sistema.
        /// </summary>
        /// <param name="pException">Excepción del sistema.</param>
        public static void EscribeError(Exception pException)
        {
            try
            {
                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " + "srvBroxelODT.Error: Source: " + pException.Source + " Message: " + pException.Message + " - InnerException: " + (null != pException.InnerException ? pException.InnerException.Message + " " + pException.InnerException.StackTrace : string.Empty));
                //using (var writer = new StreamWriter(HttpRuntime.AppDomainAppPath + "ErrLog.txt", true, Encoding.UTF8))
                //{
                //    writer.WriteLine("srvBroxelODT.Error: " + DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " Source: " + pException.Source + " Message: " + pException.Message + " - InnerException: " + (null != pException.InnerException ? pException.InnerException.StackTrace : string.Empty));                   
                //    writer.Close();
                //}

            }
            catch (Exception ex)
            {

                using (var writer = new StreamWriter(HttpRuntime.AppDomainAppPath + "ErrLog.txt", true, Encoding.UTF8))
                {
                    writer.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " + "Error EscribeError:  Source: " + ex.Source + " Message: " + ex.Message + " - InnerException: " +  (null != ex.InnerException  ? ex.InnerException.StackTrace : string.Empty));                    
                    writer.Close();
                }
            }
        }

        /// <summary>
        /// Escribe en un log el error.
        /// </summary>
        /// <param name="pMessage">Mensaje a escribir.</param>
        public static void EscribeError(string pMessage)
        {
            try
            {
                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " + "srvBroxelODT.Error: Message: " + pMessage);
                //using (var writer = new StreamWriter(HttpRuntime.AppDomainAppPath + "ErrLog.txt", true, Encoding.UTF8))
                //{
                //    writer.WriteLine("srvBroxelODT.Error: " + DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " Message: " + pMessage);
                //    writer.Close();
                //}

            }
            catch (Exception ex)
            {
                using (var writer = new StreamWriter(HttpRuntime.AppDomainAppPath + "ErrLog.txt", true, Encoding.UTF8))
                {
                    writer.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " + "Error EscribeError: Source: " + ex.Source + " Message: " + ex.Message + " - InnerException: " + (null != ex.InnerException ? ex.InnerException.StackTrace : string.Empty));
                    writer.Close();
                }
            }
        }
    }
}


