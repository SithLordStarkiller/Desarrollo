using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Services;
using System.Web.UI.WebControls;
using BroxelEncryptCom;
using IdSecure;
using wsBroxel.App_Code.Online;
using wsBroxel.App_Code.SolicitudBL;
using wsBroxel.App_Code.TokenBL;
using wsBroxel.App_Code.VCBL.Models;
using wsBroxel.BussinessLogic;
using wsBroxel.wsSMS;
using wsBroxel.App_Code.RenominacionBL;
using wsBroxel.App_Code.RenominacionBL.Model;
using wsMigraUserToMyo = wsBroxel.wsMigraUsuarioToMyo;
using wsBroxel.App_Code.GenericBL;
using wsBroxel.App_Code.Utils;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace wsBroxel.App_Code.VCBL
{
    /// <summary>
    /// 
    /// </summary>
    public class VCardBL
    {
        /// <summary>
        /// Crea cliente para un id de comercio dado
        /// </summary>
        /// <param name="idComercio"></param>
        /// <param name="email"></param>
        /// <param name="celular"></param>
        /// <param name="clabe"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string CrearClienteComercio(int idComercio, string email = "", string celular = "", string clabe = "")
        {
            var tieneMatriz = false;
            var errCelular = ", pero no se genero usuario por carecer de numero de celular, favor de validar.";
            try
            {
                using (var contex = new broxelco_rdgEntities())
                {
                    var comercio = contex.Comercio.FirstOrDefault(c => c.idComercio == idComercio);
                    if (comercio == null)
                        throw new Exception("El comercio con id " + idComercio + " no existe.");
                    var mySql = new MySqlDataAccess();

                    /*Tomar los valores de la tabla de comercio en caso de no haber sido pasados como argumentos*/

                    if (string.IsNullOrEmpty(email))
                        email = comercio.email_avisos;
                    if (string.IsNullOrEmpty(clabe))
                        clabe = comercio.num_cuenta_clabe;
                    if (string.IsNullOrEmpty(celular))
                        celular = comercio.celular;


                    if (ValidaActualizacionUsuarioMerchant(idComercio, email, celular, clabe))
                        return ("El comercio " + comercio.razon_social + " ya cuenta con merchant account y usuario.");


                    var matriz = string.IsNullOrEmpty(comercio.Matriz) ? 0 : int.Parse(comercio.Matriz);
                    var grupoComercioResult = contex.gruposComercios.FirstOrDefault(g => g.idGrupo == matriz && g.unaCuentaPorGrupo == 1);
                    if (grupoComercioResult != null)// si tiene matriz y la matriz debe tener la cuenta (El comercio que  mandan es una sucursal)
                    {
                        tieneMatriz = true;
                        if (!string.IsNullOrEmpty(grupoComercioResult.claveClienteBroxel) &&
                            !string.IsNullOrEmpty(grupoComercioResult.numCuentaBroxel))  //ya tien un numero de cuenta o clave de cliente asociado? 
                        {
                            comercio.numCuentaBroxel = grupoComercioResult.numCuentaBroxel;    //actualiza la relacion( como la matriz ya tiene una)
                            contex.SaveChanges();
                            try
                            {
                                mySql.UpdateCambioClabe(comercio.idComercio);
                            }
                            catch (Exception e)
                            {
                                Trace.WriteLine("Error al actualizar CambioClabe: " + e);
                            }

                            if (!string.IsNullOrEmpty(celular))
                            {
                                errCelular = " y usuario";
                                //Registra Broxel Online
                                var tcInfo = Helper.GetTarjetaFromCuenta(grupoComercioResult.numCuentaBroxel);
                                if (tcInfo == null)
                                    throw new Exception("No existen datos transaccionales de la cuenta");
                                var resRgtroBrxlOnline =
                                    (UsuarioOnlineResponse)RegistraBroxelOnline(comercio, email, celular, tcInfo);
                                if (resRgtroBrxlOnline == null)
                                    throw new Exception("No se pudo registrar al usuario para el comercio " + idComercio);
                                if (!resRgtroBrxlOnline.Success)
                                    throw new Exception("No se pudo registrar al usuario para el comercio " + idComercio +
                                                        ":" + resRgtroBrxlOnline.UserResponse);
                                //Agregar clave
                                var resClabe = AgregarClabe(resRgtroBrxlOnline.UserBroxel.Id, comercio, clabe, email);

                                if (!resClabe)
                                    throw new Exception("No pudo agregarse CLABE al usuario " +
                                                        resRgtroBrxlOnline.UserBroxel.Id);

                                // Migracion de cliente BroxelOnlineTo Myo

                                ThreadPool.QueueUserWorkItem(delegate
                                {
                                    MigrarClienteToMyo(resRgtroBrxlOnline.UserBroxel.Id);
                                });

                                comercio.tieneUsuarioFintech = true;
                                contex.SaveChanges();
                            }
                            return "Exito al crear la merchant account" + errCelular;
                        }
                    }

                    //crea cliente comercio
                    var creaClientComercioResult = CreaClienteComercioBroxel(comercio);
                    if (creaClientComercioResult == 0)
                        throw new Exception("No se pudo crear cliente-comercio, idComercio: " + idComercio);

                    var maquilaResult = contex.maquila.FirstOrDefault(m => m.id == creaClientComercioResult);

                    if (maquilaResult == null)
                        throw new Exception("No se pudo crear cliente-comercio, idComercio: " + idComercio);

                    var helperResult = Helper.GetTarjetaFromCuenta(maquilaResult.num_cuenta);
                    if (helperResult == null)
                        throw new Exception("No existen datos transaccionales de la cuenta");

                    if (!string.IsNullOrEmpty(celular))
                    {
                        errCelular = " y usuario.";
                        //Registra Broxel Online
                        var resultRgtroBrxlOnline = (UsuarioOnlineResponse)RegistraBroxelOnline(comercio, email, celular, helperResult);

                        if (!resultRgtroBrxlOnline.Success)
                            throw new Exception("No se pudo registrar al usuario para el comercio " + idComercio + ":" + resultRgtroBrxlOnline.UserResponse);

                        //Agregar clave
                        var result = AgregarClabe(resultRgtroBrxlOnline.UserBroxel.Id, comercio, clabe, email);

                        if (!result)
                            throw new Exception("No pudo agregarse CLABE al usuario " + resultRgtroBrxlOnline.UserBroxel.Id);

                        // Migracion de cliente BroxelOnlineTo Myo
                        ThreadPool.QueueUserWorkItem(delegate
                        {
                            MigrarClienteToMyo(resultRgtroBrxlOnline.UserBroxel.Id);
                        });

                        comercio.tieneUsuarioFintech = true;
                    }


                    //actualiza cuenta comercio
                    comercio.numCuentaBroxel = maquilaResult.num_cuenta;
                    maquilaResult.C4ta_linea = comercio.Comercio1;
                    contex.SaveChanges();
                    try
                    {
                        mySql.UpdateCambioClabe(comercio.idComercio);
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine("Error al actualizar CambioClabe: " + e);
                    }


                    var maqClaveCliente = mySql.GetClaveCliente(maquilaResult.id);
                    RenominaClienteComercio(comercio, maquilaResult.num_cuenta, maquilaResult.nro_tarjeta, maqClaveCliente, maquilaResult.producto);
                    if (!tieneMatriz) return "Exito al crear la merchant account" + errCelular; ;

                    grupoComercioResult.claveClienteBroxel = maqClaveCliente;
                    grupoComercioResult.numCuentaBroxel = maquilaResult.num_cuenta;
                    contex.SaveChanges();

                    return "Exito al crear la merchant account" + errCelular;
                }
            }
            catch (Exception e)
            {
                var msg = "Existio un problema al dar de alta el comercio " + idComercio.ToString(CultureInfo.InvariantCulture) + " como cliente: " + e;
                Trace.WriteLine(msg);
                Helper.SendMail("broxelonline@broxel.com", "mauricio.lopez@broxel.com, rodrigo.diazdeleon@broxel.com", "Generacion de clientes comercios", msg, "67896789");
                return "Existio un problema al dar de alta el comercio " + idComercio.ToString(CultureInfo.InvariantCulture) + " como cliente: " + e.Message;
            }
        }

        #region Metodos privados de CrearClienteComercio

        private bool ValidaActualizacionUsuarioMerchant(int idComercio, string email = "", string celular = "",
            string clabe = "")
        {
            var mySql = new MySqlDataAccess();
            var cuenta = mySql.ValidaCreaciónMerchantAccountUsuario(idComercio);
            if (String.IsNullOrEmpty(cuenta)) return false;
            //El comercio ya tiene merchant y usuario, validar si el usuario ligado a la cuenta es igual al usuario en comercio, si no, limpiar la relación y recrear la conexion
            try
            {
                using (var ctx = new broxelco_rdgEntities())
                {
                    var user = (from u in ctx.UsuariosOnlineBroxel
                                join accessos_clientes a in ctx.accessos_clientes on u.Id equals a.IdUsuarioOnlineBroxel
                                where a.cuenta == cuenta
                                select u).FirstOrDefault();
                    if (user == null)
                        return false;
                    if (user.Usuario != email && user.CorreoElectronico != email)
                    {
                        //El usuario que tiene comercio es diferente al usuario que tiene UsuariosBroxelOnline, es una actualización
                        // Actualizo UsuariosBroxelOnline
                        user.Usuario = user.Usuario + "BAKWs";
                        user.CorreoElectronico = user.CorreoElectronico + "BAKWs";
                        ctx.Entry(user).State = EntityState.Modified;
                        ctx.SaveChanges();
                        // Obtengo el registro de accesos_clientes y lo respaldo en accesos_clientesbak y elimino el registro original
                        mySql.RespaldaAccesosClientes(user.Id);
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                var msg = "Existio un problema al validar el alta el comercio " + idComercio + " como cliente: " + e;
                Trace.WriteLine(msg);
                Helper.SendMail("broxelonline@broxel.com", "mauricio.lopez@broxel.com", "Generacion de clientes comercios", msg, "67896789");
                return true;
            }
        }

        public void RenominaClienteComercio(int idComercio)
        {
            try
            {
                using (var ctx = new broxelco_rdgEntities())
                {
                    var comercio = ctx.Comercio.FirstOrDefault(c => c.idComercio == idComercio);
                    if (comercio == null)
                        return;
                    var maq = ctx.maquila.FirstOrDefault(m => m.num_cuenta == comercio.numCuentaBroxel);
                    if (maq == null)
                        return;
                    RenominaClienteComercio(comercio,comercio.numCuentaBroxel,maq.nro_tarjeta,maq.clave_cliente,maq.producto);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(DateTime.Now.ToString("O") + " Error en RenominaClienteComercio, para el idComercio " + idComercio.ToString(CultureInfo.InvariantCulture)+" : " +e);
            }
        }

        private void RenominaClienteComercio(Comercio11 pComercio, string numCuenta, string tarjeta, string claveCliente, string producto)
        {
            try
            {
                using (var ctx = new broxelco_rdgEntities())
                {
                    // Si existe una solicitud de renominacion exitosa, no reintenta la renominacion.
                    var existeRenomina =
                        ctx.RenominacionesInternas.Where(x => x.Cuenta == numCuenta && x.CodigoRespuesta == "-1")
                            .ToList();
                    if (existeRenomina.Any())
                        return;
                }

                var ori = new OriginacionData
                {
                    Nombre = pComercio.razon_social,
                    Genero = "M",
                    Calle = string.IsNullOrEmpty(pComercio.calle) ? "" : pComercio.calle,
                    NumExterior = pComercio.numExterior,
                    NumInterior = pComercio.numInterior,
                    Colonia = pComercio.colonia,
                    CodigoPostal = pComercio.codigo_postal,
                    Municipio = pComercio.delegacion,
                    NumCuenta = numCuenta,
                    NumTarjeta = tarjeta.Substring(tarjeta.Length - 4, 4),
                    ClaveCliente = claveCliente,
                    Producto = producto,
                    Rfc = pComercio.rfc
                };
                var oris = new List<OriginacionData> { ori };
                new RenominaBL().InsertaRenominacionConOriginacion(oris);
            }
            catch (Exception e)
            {
                Helper.SendMail("broxelonline@broxel.com", "mauricio.lopez@broxel.com", "Generacion de clientes comercios", "Error al renominar " + pComercio.razon_social + " (" + pComercio.Comercio1 + ") : " + e, "67896789");
            }
        }

        private int CreaClienteComercioBroxel(Comercio11 pComercio)
        {
            try
            {
                //validacion para saber si el comercio ya fue creado como cliente
                // si este fue creado se retorna true para continuar con el proceso
                using (var contex = new broxelco_rdgEntities())
                {
                    if (pComercio.numCuentaBroxel != null) //Si el comercio ya tiene cuenta, significa que ya fue registado
                    {
                        var clienBrxLayoutResul = contex.clientesBrxLayout.FirstOrDefault(c => c.idLayout == 1);
                        if (clienBrxLayoutResul == null)
                            throw new Exception("No está configurado el clientesBrxLayout 1, favor de revisar");

                        var catClienteLayout = contex.CatCLienteLayout.FirstOrDefault(ccl => ccl.idLayouot == 1);
                        if (catClienteLayout == null)
                            throw new Exception("No está configurado el cliente layout 1, favor de revisar");

                        var maquilaResul =
                            contex.maquila.FirstOrDefault(
                                m =>
                                    m.num_cuenta == pComercio.numCuentaBroxel &&
                                    m.clave_cliente.Contains(clienBrxLayoutResul.clienteBrPrefix) &&
                                    m.producto == catClienteLayout.codigoProducto);
                        if (maquilaResul != null) //checamos que la consistencia de datos sea correcta
                        {
                            var result = (from m in contex.maquila
                                          join d in contex.DetalleClientesBroxel on m.clave_cliente equals d.ClaveCliente
                                          join cc in contex.CatCLienteLayout on m.producto equals cc.codigoProducto
                                          join cb in contex.clientesBrxLayout on cc.idLayouot equals cb.idLayout
                                          where
                                              m.num_cuenta == pComercio.numCuentaBroxel && cc.idLayouot == 1 &&
                                              m.clave_cliente.Contains(clienBrxLayoutResul.clienteBrPrefix) &&
                                              m.producto == d.Producto
                                          select m).ToList();
                            if (result.Count > 0)
                            {
                                return result[0].id;
                                // Este retono significa que el cliente-comercio ya esta registrado y que los datos tienen consistencia
                            }
                            Helper.SendMail("broxelonline@broxel.com",
                                "mauricio.lopez@broxel.com, francisco.sanchez@broxel.com",
                                "Se encontró en un error de inconsistencia de datos al crear el clinte-comercio " +
                                pComercio.idComercio + " con numero de cuenta " + pComercio.numCuentaBroxel,
                                "Inconsistencia de datos ", "67896789");
                            return 0;
                            // este retorno signica que el cliente-comercio ya fue registrado, pero se encontró inconsistencia de datos y se manda un email a los
                            //encargados de este proceso. retorna false para no continuar con el proceso
                        }
                    }
                }

                // si el comercio aun no es creado como cliente, en este proceso se registra.
                using (var contex = new broxelco_rdgEntities())
                {

                    try
                    {
                        var mySql = new MySqlDataAccess();

                        var catClienteLayout = contex.CatCLienteLayout.FirstOrDefault(ccl => ccl.idLayouot == 1);
                        if (catClienteLayout == null)
                            throw new Exception("No está configurado el cliente layout 1, favor de revisar");

                        var maquilaResult = contex.maquila.FirstOrDefault(m => m.clave_cliente == catClienteLayout.pull && m.producto == catClienteLayout.codigoProducto);
                        if (maquilaResult == null)
                            throw new Exception("No existen cuentas disponibles para asignar al cliente.");

                        var helperResult = Helper.GetTarjetaFromCuenta(maquilaResult.num_cuenta);
                        if (helperResult == null)
                            throw new Exception("No existen datos transaccionales de la cuenta");

                        var res = mySql.CreaClienteComercioBroxel(pComercio.idComercio, catClienteLayout.idLayouot, catClienteLayout.codigoProducto, maquilaResult.id);
                        return res ? maquilaResult.id : 0;
                    }
                    catch (Exception e)
                    {
                        var msg = "Existio un problema al dar de alta el comercio como cliente: " + e;
                        Trace.WriteLine(msg);
                        Helper.SendMail("broxelonline@broxel.com", "mauricio.lopez@broxel.com", "Generacion de clientes comercios", msg, "67896789");
                        return 0;
                    }
                }
            }
            catch (Exception e)
            {
                var msg = "Existio un problema al validar el alta el comercio " + pComercio.razon_social + " (" + pComercio.Comercio1 + ") como cliente: " + e;
                Trace.WriteLine(msg);
                Helper.SendMail("broxelonline@broxel.com", "mauricio.lopez@broxel.com", "Generacion de clientes comercios", msg, "67896789");
                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comercio"></param>
        /// <param name="celular"></param>
        /// <param name="helperResult"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        private object RegistraBroxelOnline(Comercio11 comercio, string email, string celular, Tarjeta helperResult)
        {
            try
            {
                var secureComp = new IdSecureComp();
                var wsAdmon = new wsAdmonUsuarios();
                using (var ctx = new broxelco_rdgEntities())
                {
                    var user =
                        ctx.UsuariosOnlineBroxel.FirstOrDefault(x => x.Usuario == email || x.CorreoElectronico == email);
                    if (user != null)
                    {
                        var userOnline = wsAdmon.ObtenCuentasxUsuario(secureComp.GetIdSecure(user.Id));
                        if (userOnline.UserBroxel.Cuentas == null || userOnline.UserBroxel.Cuentas.Count == 0)
                        {
                            // Validar que el usuario no fue creado con la app movil
                            var ccstl = ctx.CreaClienteSinTarjetaLog.FirstOrDefault(c => c.usuario == email);
                            if (ccstl != null)
                            {
                                // Actualiza telefono y celular del usuario
                                user.Telefono = celular;
                                user.Celular = celular;
                                ctx.SaveChanges();
                                new MySqlDataAccess().ClearCreaClientesSinTarjetaLog(email);
                                //Agrega merchant account al usuario
                                var res = wsAdmon.AgregarTarjeta(secureComp.GetIdSecure(user.Id), helperResult.NumeroTarjeta, helperResult.FechaExpira);
                                if (res.Success)
                                {
                                    return wsAdmon.ObtenCuentasxUsuario(secureComp.GetIdSecure(user.Id));
                                }
                                throw new Exception("No fue posible agregar cuenta " + helperResult.Cuenta + ": " + res.UserResponse);
                            }
                        }
                        else if (userOnline.UserBroxel.Cuentas.Any(cuenta => helperResult.Cuenta == cuenta.NumCuenta))
                        {
                            return userOnline;
                        }
                        //El usuario existe, pero no tiene asociada la cuenta de comercio, alertar
                        userOnline.Success = false;
                        userOnline.UserResponse = "El usuario ya existe con otra cuenta que no es mejoravit";
                        return userOnline;
                    }
                }
                var temppass = Regex.Replace(System.Web.Security.Membership.GeneratePassword(10, 0), @"[^a-zA-Z0-9]", m => "9");
                temppass = Helper.Cifrar(temppass);

                var registraUsuario = wsAdmon.RegistrarUsuario(comercio.razon_social, comercio.rfc, celular, "NA", comercio.codigo_postal, email, email, celular, DateTime.UtcNow, temppass, helperResult.NumeroTarjeta, helperResult.FechaExpira);

                return registraUsuario;
            }

            catch (Exception e)
            {
                var msg = "Existio un problema al dar de alta el comercio como usuario online broxel: " + e;
                Trace.WriteLine(msg);
                Helper.SendMail("broxelonline@broxel.com", "mauricio.lopez@broxel.com", "Generacion de clientes comercios", msg, "67896789");
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userBroxelId"></param>
        /// <param name="comercio"></param>
        /// <returns></returns>
        private bool AgregarClabe(int userBroxelId, Comercio11 comercio, string clabe, string email)
        {
            try
            {
                using (var ctx = new broxelco_rdgEntities())
                {
                    var clabeInfo =
                        ctx.UsuariosOnlineCLABE.FirstOrDefault(x => x.IdUsuario == userBroxelId && x.CLABE == clabe);
                    if (clabeInfo != null)
                        return true;

                }
                return new wsDisposiciones().AgregaCLABE(userBroxelId, clabe, email,
                          comercio.razon_social, comercio.rfc, "2f28cb7e-4f76-4afd-b7a6-6f27827f92b0");
            }
            catch (Exception e)
            {
                var msg = "Existio un problema al dar de alta la clabe " + clabe + " para el comercio " + comercio.razon_social + " (" + comercio.Comercio1 + ") : " + e;
                Trace.WriteLine(msg);
                Helper.SendMail("broxelonline@broxel.com", "mauricio.lopez@broxel.com", "Generacion de clientes comercios", msg, "67896789");
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userBroxelId"></param>
        /// <returns></returns>
        private bool MigrarClienteToMyo(int userBroxelId)
        {
            var idUser = new IdSecureComp().GetIdUserValid(userBroxelId);
            try
            {
                var clientMigra = new wsMigraUserToMyo.IwsMigraUsuarioToMyoClient();
                if (idUser == 0)
                {
                    try
                    {
                        using (var ctx = new broxelco_rdgEntities())
                        {
                            var user = ctx.mobile_session.FirstOrDefault(x => x.idUserSecure == userBroxelId);
                            if (user == null)
                                throw new Exception("No hay idUserSecure para " + userBroxelId);
                            idUser = user.idUser;
                        }
                    }
                    catch (Exception e)
                    {
                        Helper.SendMail("broxelonline@broxel.com", "mauricio.lopez@broxel.com, jesus.valdiviezo@broxel.com", "Generacion de clientes comercios", "Error al migrar usuario " + idUser + "(" + userBroxelId + ") : " + e, "67896789");
                        return true;
                    }
                }

                var totalUsuarios = 0;
                
                var resMigra = new wsMigraUserToMyo.ResponseMigracion();

                for (var i = 0; i < 10; i++)
                {
                    resMigra = clientMigra.RealizaMigracionToMyo(idUser);
                    totalUsuarios = resMigra.TotalUsuarios;
                    if (totalUsuarios == 1)
                        break;
                }
                
                if (totalUsuarios != 1)
                    Helper.SendMail("broxelonline@broxel.com", 
                        "mauricio.lopez@broxel.com, jesus.valdiviezo@broxel.com", 
                        "Generacion de clientes comercios", 
                        "Error al migrar usuario " + idUser + "(" + userBroxelId + "): " + resMigra.UserMessage +
                        ". resMigra.Succes: " + resMigra.Success +
                        ", resMigra.TotalUsuarios: " + resMigra.TotalUsuarios +
                        ", resMigra.ErrorCuentas: " + resMigra.ErrorCuentas +
                        ", resMigra.ErrorCliente: " + resMigra.ErrorCliente +
                        ", resMigra.ErrorTarjeta: " + resMigra.ErrorTarjeta +
                        ", resMigra.ExtensionData: " + resMigra.ExtensionData +
                        ", resMigra.UserMessage: " + resMigra.UserMessage +
                        ", resMigra.UsuariosMigradas: " + resMigra.UsuariosMigradas +
                        ", resMigra.UsuariosYaRegistradas: " + resMigra.UsuariosYaRegistradas, "67896789");
                //throw new Exception("Error al migrar usuario " + registraUsuario.UserBroxel.Id + ": " + resMigra.UserMessage);
                return true;
            }
            catch (Exception e)
            {
                Helper.SendMail("broxelonline@broxel.com", "mauricio.lopez@broxel.com, jesus.valdiviezo@broxel.com", "Generacion de clientes comercios", "Error al migrar usuario " + idUser + "(" + userBroxelId + "): " + e, "67896789");
                return true;
            }
        }

        #endregion

        #region Metodos publicos

        /// <summary>
        /// Funcionalidad de creación de clientes en base a una maquila dada.
        /// </summary>
        /// <param name="idUser">id de usuario Broxel Online</param>
        /// <param name="idApp">id de aplicación</param>
        /// <param name="infoMaquila">Id de Maquila y Numero de cuenta</param>
        /// <param name="codigoProducto">Codigo de producto</param>
        /// <param name="originalIdUser">Id de usuario Broxel Online seguro</param>
        /// <param name="esFisica">Indica si la tarjeta es fisica o temporal</param>
        /// <returns></returns>
        public UsuarioOnlineResponse CreaClientes(int idUser, int idApp, MaquilaVcInfo infoMaquila, string codigoProducto, int originalIdUser, bool esFisica = false)
        {
            var mySql = new MySqlDataAccess();
            try
            {
                if (!ApartaMaquila(infoMaquila.IdMaquila))
                    return new UsuarioOnlineResponse
                    {
                        Success = false,
                        UserResponse = "No fue posible agregar la tarjeta " + (esFisica ? "" : "temporal") + "a su cuenta: por favor reintente."
                    };

                var idLog = ValidaUsuarioSinCuenta(idUser);
                if (idLog == 0)
                {
                    return GeneraResponse(idUser);
                }

                if (!mySql.CreaClienteBroxel(idLog, codigoProducto, infoMaquila.IdMaquila, esFisica))
                    return new UsuarioOnlineResponse
                    {
                        Success = false,
                        UserResponse = (esFisica ? "Ocurrio un error al enlazar su tarjeta" : "Ocurrio un error al crear su tarjeta") + ", por favor, vuelva a intentar más tarde"
                    };
                // Cambia cliente en maquila.
                CambiaClienteMaquila(infoMaquila.IdMaquila, idLog);
                // Conecta cuenta a usuarioOnline
                var tc = Helper.GetTarjetaFromCuenta(infoMaquila.NumCuenta);
                var agregaRes = new wsAdmonUsuarios().AgregarTarjeta(originalIdUser, tc.NumeroTarjeta, tc.FechaExpira, esFisica ? "" : "Tarjeta Temporal");
                if (!agregaRes.Success)
                    return new UsuarioOnlineResponse
                    {
                        Success = false,
                        UserResponse = "No fue posible agregar la tarjeta " + (esFisica ? "" : "temporal") + "a su cuenta: " + agregaRes.UserResponse
                    };

                if (!GetEnviaSeedAlCrearTarjeta(idLog)) return GeneraResponse(idUser);

                try
                {
                    var lastDigits = tc.NumeroTarjeta.Substring(tc.NumeroTarjeta.Length - 4);

                    var taskRenominacion = new Task(() => new RenominaBL().InsertaRenominacionVCard(idUser, idApp, codigoProducto, lastDigits));
                    taskRenominacion.Start();

                }
                catch (Exception e)
                {
                    Helper.SendMail("broxelonline@broxel.com", "mauricio.lopez@broxel.com", "CreaClientes", "Error al renominar " + originalIdUser + " : " + e, "67896789");
                }


                return GeneraResponse(idUser);
            }
            catch (Exception e)
            {
                return new UsuarioOnlineResponse
                {
                    Success = false,
                    UserResponse = (esFisica ? "No fue posible enlazar la tarjeta" : "No fue posible obtener la tarjeta temporal: ") + e.Message
                };
            }
        }


        /// <summary>
        /// Funcionalidad de creación de clientes en base a una maquila dada para tarjetas B2C.
        /// </summary>
        /// <param name="producto">producto Broxel Online</param>
        /// <param name="pNombre">nombre</param>
        /// <param name="idMaquila">Id de Maquila</param>
        /// <param name="sNombre">segundo nombre</param>
        /// <param name="aPaterno">apellido paterno</param>
        /// <param name="fechaNacimiento"></param>
        /// <param name="aMaterno"></param>
        /// <param name="rfc"></param>
        /// <param name="colonia"></param>
        /// <param name="calle"></param>
        /// <param name="numeroExt"></param>
        /// <param name="telefono"></param>
        /// <param name="usuario"></param>
        /// <param name="numeroInt"></param>
        /// <param name="noEmpleado"></param>
        /// <param name="delegacionMunicipio"></param>
        /// <param name="estado"></param>
        /// <param name="codigoPostal"></param>
        /// <param name="numCuenta"></param>
        /// <param name="idApp"></param>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public UsuarioOnlineResponse CreaClientesB2C(string producto, int idMaquila, string pNombre, string sNombre,
            string aPaterno,
            string aMaterno,
            DateTime fechaNacimiento,
            string rfc,
            string colonia,
            string calle,
            string numeroExt,
            string numeroInt,
            string telefono,
            string usuario,
            string noEmpleado,
            string delegacionMunicipio,
            string estado, string codigoPostal, string numCuenta, int idApp, int idUser)
        {
            var mySql = new MySqlDataAccess();
            try
            {
                if (!mySql.CreaClienteBroxelB2C(producto, idMaquila, pNombre, sNombre, aPaterno, aMaterno, fechaNacimiento, rfc, colonia, calle, numeroExt, numeroInt, telefono, usuario, noEmpleado, delegacionMunicipio, estado, codigoPostal))
                    return new UsuarioOnlineResponse
                    {
                        Success = false,
                        UserResponse = ("Ocurrio un error al enlazar su tarjeta, por favor, vuelva a intentar más tarde")
                    };
                
                return GeneraResponse(idUser);
            }
            catch (Exception e)
            {
                return new UsuarioOnlineResponse
                {
                    Success = false,
                    UserResponse = ("No fue posible enlazar la tarjeta")
                };
            }
        }

        /// <summary>
        /// Obtiene los datos de la tarjeta virtual
        /// </summary>
        /// <param name="idApp">Id de la aplicación</param>
        /// <param name="usuario">usuario de la applicación</param>
        /// <param name="token">token asociado al usuario</param>
        /// <returns></returns>
        public VCardData GetVCardData(int idApp, string usuario, string token)
        {
            var mySql = new MySqlDataAccess();
            var idConsul = mySql.InsertVcConsulta(idApp, usuario, token);
            var cerPath = AppDomain.CurrentDomain.RelativeSearchPath + "\\client-certMyoTD.der";
            var cuenta = "";
            var clabe = "";
            if (!new TokenService().ValidateTokenIdApp(usuario,
                "CreaVCUser" + idApp.ToString(CultureInfo.InvariantCulture), token))
                return new VCardData { IdTran = 0, Track1 = "", Track2 = "" };

            using (var ctx = new broxelco_rdgEntities())
            {
                var cLog =
                    ctx.CreaClienteSinTarjetaLog.FirstOrDefault(
                        x => x.idApp == idApp && x.usuario == usuario && cuenta != "UNDEFINED");
                if (cLog != null)
                {
                    cuenta = cLog.cuenta;
                }
                var maq = ctx.maquila.FirstOrDefault(y => y.num_cuenta == cuenta);
                if (maq != null)
                    clabe = maq.CLABE;
            }

            if (cuenta == "" || clabe == "")
                return new VCardData { IdTran = 0, Track1 = "", Track2 = "" };

            var tc = Helper.GetTarjetaFromCuenta(cuenta);

            if (tc == null)
                return new VCardData { IdTran = 0, Track1 = "", Track2 = "" };

            //var j = new JVCData { Tarjeta = "1234567890123456", Clabe = "123456789012345678", Cvv = "123", FechaVencimiento = "02/19", Nombre = "PRUEBA SABROSA", FechaConsulta = DateTime.Now.ToString("O") };
            var j = new JVCData { Tarjeta = tc.NumeroTarjeta, Clabe = clabe, Cvv = tc.Cvc2, FechaVencimiento = tc.FechaExpira.Substring(0, 2) + "/" + tc.FechaExpira.Substring(2, 2), Nombre = tc.NombreTarjeta, FechaConsulta = DateTime.Now.ToString("O") };
            var r2 = new Random().Next(1, 3);
            var jS = BuildRandomDataTrack(j);
            var fS = PgpEncrypt.EncryptText(BuildFakeTrack(jS.Length), cerPath);
            jS = PgpEncrypt.EncryptText(jS, cerPath);
            if (idConsul > 0)
                mySql.SetCuentaVcConsulta(idConsul, tc.Cuenta);
            return r2 == 1 ? new VCardData { IdTran = Convert.ToInt64(r2.ToString(CultureInfo.InvariantCulture) + idConsul.ToString(CultureInfo.InvariantCulture)), Track1 = jS, Track2 = fS } : new VCardData { IdTran = Convert.ToInt64(r2.ToString(CultureInfo.InvariantCulture) + idConsul.ToString(CultureInfo.InvariantCulture)), Track1 = fS, Track2 = jS };
        }

        /// <summary>
        /// Obtiene los datos de cualquier tarjeta por capricho mamon
        /// </summary>
        /// <returns></returns>
        public VCardData GetCardData(int idUser, string numCuenta)
        {
            var cerPath = AppDomain.CurrentDomain.RelativeSearchPath + "\\client-certMyoTD.der";
            string clabe;
            //tarjetas adicionales b2c
            var tipo_tc = false;

            var family = new BroxelFamily();
            var esHijaDelUsuario = family.EsHijoDeLaCuentaPadreFamily(idUser, numCuenta);

            using (var ctx = new broxelco_rdgEntities())
            {
                var accesso = ctx.accessos_clientes.FirstOrDefault(a => a.IdUsuarioOnlineBroxel == idUser && a.cuenta == numCuenta);

                //Family
                if (accesso == null && !esHijaDelUsuario)
                    return new VCardData { IdTran = 0, Track1 = "", Track2 = "" };


                var maq = ctx.maquila.FirstOrDefault(y => y.num_cuenta == numCuenta);
                if (maq == null)
                    return new VCardData { IdTran = 0, Track1 = "", Track2 = "" };
                clabe = maq.CLABE;
                var adicional = ctx.TarjetasFisicasAdicionales.FirstOrDefault(s => s.NumCuenta == numCuenta);
                if (adicional != null)
                    tipo_tc = true;
            }

            var tc = tipo_tc ? Helper.GetTarjetaFromCuentaAdicional(numCuenta) : Helper.GetTarjetaFromCuenta(numCuenta);

            if (tc == null)
                return new VCardData { IdTran = 0, Track1 = "", Track2 = "" };

            var j = new JVCData { Tarjeta = tc.NumeroTarjeta, Clabe = clabe, Cvv = tc.Cvc2, FechaVencimiento = tc.FechaExpira.Substring(0, 2) + "/" + tc.FechaExpira.Substring(2, 2), Nombre = tc.NombreTarjeta, FechaConsulta = DateTime.Now.ToString("O") };
            var r2 = new Random().Next(1, 3);
            var jS = BuildRandomDataTrack(j);
            var fS = PgpEncrypt.EncryptText(BuildFakeTrack(jS.Length), cerPath);
            jS = PgpEncrypt.EncryptText(jS, cerPath);
            var idConsul = new Random().Next(99999);
            return r2 == 1 ? new VCardData { IdTran = Convert.ToInt64(r2.ToString(CultureInfo.InvariantCulture) + idConsul.ToString(CultureInfo.InvariantCulture)), Track1 = jS, Track2 = fS } : new VCardData { IdTran = Convert.ToInt64(r2.ToString(CultureInfo.InvariantCulture) + idConsul.ToString(CultureInfo.InvariantCulture)), Track1 = fS, Track2 = jS };
        }

        /// <summary>
        /// Metodo de verificación de celular por medio de token
        /// </summary>
        /// <param name="idUser">Id de usuario Broxel Online</param>
        /// <param name="celular">Telefono celular</param>
        /// <param name="token">Token</param>
        /// <returns></returns>
        public bool ValidaCelularToken(int idUser, string celular, string token)
        {
            try
            {
                using (var ctx = new broxelco_rdgEntities())
                {
                    var cLog = ctx.CreaClienteSinTarjetaLog.FirstOrDefault(x => x.idUsuarioOnlineBroxel == idUser && x.celular == celular);
                    if (cLog != null)
                    {
                        return new TokenService().ValidateTokenIdApp(cLog.usuario, "CreaVCUser" + cLog.idApp, token);
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        /// <summary>
        /// Metodo de verificación de celular por medio de token retornando los valores de la semilla(seed)
        /// </summary>
        /// <param name="idUser">Id de usuario Broxel Online</param>
        /// <param name="celular">Telefono celula</param>
        /// <param name="token">Token</param>
        /// <returns></returns>
        public VcSeedData ValidaCelularTokenOtp(int idUser, string celular, string token)
        {
            try
            {
                using (var ctx = new broxelco_rdgEntities())
                {
                    var cLog = ctx.CreaClienteSinTarjetaLog.FirstOrDefault(x => x.idUsuarioOnlineBroxel == idUser && x.celular == celular);
                    if (cLog != null)
                    {
                        var x = new TokenService().ValidateTokenIdAppOtp(cLog.usuario, "CreaVCUser" + cLog.idApp, token);
                        return new VcSeedData
                        {
                            DescStatus = x.DescStatus,
                            Seed = x.Seed,
                            Status = x.Status
                        };
                    }
                }
            }
            catch (Exception)
            {
                return new VcSeedData { Seed = string.Empty, Status = false, DescStatus = "Error al validar el celular con el token otp." };
            }
            return new VcSeedData
            {
                Seed = string.Empty,
                Status = false,
                DescStatus = "No exite el usuario con id: " + idUser + " y celular: " + celular + "."
            };
        }

        /// <summary>
        /// Método para recalibración de token 
        /// </summary>
        /// <param name="idUser">id de usuario Broxel Online</param>
        /// <param name="idApp">id de aplicación</param>
        /// <param name="token1">token actual</param>
        /// <param name="token2">segundo token generado</param>
        /// <returns></returns>
        public bool RecalibrarToken(int idUser, int idApp, string token1, string token2)
        {
            try
            {
                using (var ctx = new broxelco_rdgEntities())
                {
                    var usuario = ctx.UsuariosOnlineBroxel.FirstOrDefault(x => x.Id == idUser);
                    if (usuario != null)
                    {
                        return new TokenService().CalibrateTokenIdApp(usuario.Usuario,
                            "CreaVCUser" + idApp.ToString(CultureInfo.InvariantCulture), token1, token2);
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
        /// Método para saber si una tarjeta es virtual
        /// </summary>
        /// <param name="numCuenta">Cuenta a revisar</param>
        /// <returns></returns>
        public bool EsVc(string numCuenta)
        {
            using (var ctx = new broxelco_rdgEntities())
            {
                var data = ctx.CreaClienteSinTarjetaLog.FirstOrDefault(x => x.cuenta == numCuenta && x.esFisica == false && x.idApp == 2);
                return (data != null);
            }
        }

        /// <summary>
        /// Método para saber si una tarjeta puede mostrar informacion
        /// </summary>
        /// <param name="numCuenta"></param>
        /// <returns></returns>
        public bool EsMostrarInformacion(string numCuenta)
        {
            using (var ctx = new broxelco_rdgEntities())
            {
                var data = ctx.CreaClienteSinTarjetaLog.FirstOrDefault(x => x.cuenta == numCuenta);
                if (data != null)
                {
                    var dataT = ctx.TarjetasFisicasAdicionales.FirstOrDefault(x => x.NumCuenta == numCuenta);
                    return (dataT == null);
                }
                return false;

                //return (data != null);
            }
        }

        /// <summary>
        /// Cambia el cliente del registro de maquila
        /// </summary>
        /// <param name="idMaquila">id de maquila</param>
        /// <param name="idCreaLog">id de tabla de log de creación de clientes</param>
        /// <returns></returns>
        private bool CambiaClienteMaquila(int idMaquila, int idCreaLog)
        {
            try
            {
                using (var ctx = new broxelco_rdgEntities())
                {
                    var cLog = ctx.CreaClienteSinTarjetaLog.FirstOrDefault(x => x.id == idCreaLog);
                    if (cLog != null)
                    {
                        var maq = ctx.maquila.FirstOrDefault(y => y.id == idMaquila);
                        if (maq != null)
                        {
                            try
                            {
                                maq.clave_cliente = cLog.claveCliente;
                                maq.IdNivelDeCuenta = 1;
                                ctx.Entry(maq).State = EntityState.Modified;
                                ctx.SaveChanges();
                                return true;
                            }
                            catch (Exception e)
                            {
                                Trace.Write(DateTime.Now.ToString("O") + "- Error al cambiar el cliente de maquila al cliente " + idCreaLog.ToString(CultureInfo.InvariantCulture) + " en base de datos mysql, intentando en replica: " + e);
                                try
                                {
                                    using (var ctxR = new BroxelCommonEntities())
                                    {
                                        var refer = ctxR.RelacionIDsReplicado.FirstOrDefault(r => r.IdOrigen == idMaquila);
                                        if (refer == null)
                                            return false;
                                        var maqRep = ctxR.Maquila.FirstOrDefault(mr => mr.id == refer.IdDestino);
                                        if (maqRep == null)
                                            return false;
                                        maqRep.clave_cliente = cLog.claveCliente;
                                        maqRep.IdNivelDeCuenta = 1;
                                        ctxR.Entry(maqRep).State = EntityState.Modified;
                                        ctxR.SaveChanges();
                                        return true;
                                    }
                                }
                                catch (Exception e1)
                                {
                                    Trace.Write(DateTime.Now.ToString("O") + "- Error al cambiar el cliente de maquila al cliente " + idCreaLog.ToString(CultureInfo.InvariantCulture) + " en base de datos mysql, intentando en replica: " + e);
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Trace.Write(DateTime.Now.ToString("O") + "- Error al cambiar el cliente de maquila al cliente " + idCreaLog.ToString(CultureInfo.InvariantCulture) + ": " + e);
                return false;
            }
            return false;
        }

        /// <summary>
        /// Quita el id de maquila del pull
        /// </summary>
        /// <param name="idMaquila">id de maquila</param>
        /// <returns></returns>
        private bool ApartaMaquila(int idMaquila)
        {
            try
            {
                using (var ctx = new broxelco_rdgEntities())
                {
                    var maq = ctx.maquila.FirstOrDefault(y => y.id == idMaquila);
                    if (maq != null)
                    {
                        maq.clave_cliente = "APARTADA";
                        maq.IdNivelDeCuenta = 1;
                        ctx.Entry(maq).State = EntityState.Modified;
                        ctx.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Trace.Write(DateTime.Now.ToString("O") + "- Error al remover el id de maquila " + idMaquila.ToString(CultureInfo.InvariantCulture) + ": " + e);
                return false;
            }
            return false;
        }
        /// <summary>
        /// Genera respuesta para obten TC
        /// </summary>
        /// <param name="idUsuario">idUsuario</param>
        /// <returns>Objeto UsuarioOnlineResponse con información del usuario y cuentas asociadas</returns>
        private UsuarioOnlineResponse GeneraResponse(int idUsuario)
        {
            using (var ctx = new broxelco_rdgEntities())
            {
                var user = ctx.UsuariosOnlineBroxel.FirstOrDefault(x => x.Id == idUsuario);
                if (user != null)
                {
                    return new UsuarioOnlineRequest().Login(user.Usuario, Helper.DesCifrar(user.Password));
                }
            }
            return null;
        }

        /// <summary>
        /// Metodo para reenviar seed por SMS
        /// </summary>
        /// <param name="idUser">id de usuario Broxel Online</param>
        /// <param name="idApp">id de aplicación</param>
        /// <param name="celular">telefono celular</param>
        /// <returns>true si pudo enviar SMS, false en caso de error</returns>
        public bool ReenviaSeedSMS(int idUser, int idApp, string celular = "")
        {
            try
            {
                var tkData = new MySqlDataAccess().GetUsuario(idUser);
                if (tkData == null)
                    return false;
                var tk = new MySqlDataAccess().GetTokenUsuario(tkData.Usuario,
                    "CreaVCUser" + idApp.ToString(CultureInfo.InvariantCulture));
                if (tk != null)
                    return MandaSeedSMS(tk.TokenSeed, (string.IsNullOrEmpty(celular)) ? tkData.Celular : celular);

            }
            catch (Exception e)
            {
                return false;
            }
            return false;
        }

        /// <summary>
        /// Metodo para reenviar seed por SMS retornando los valores de la semilla(seed)
        /// </summary>
        /// <param name="idUser">id de usuario Broxel Online</param>
        /// <param name="idApp">id de aplicación</param>
        /// <param name="celular">telefono celular</param>
        /// <returns>objeto con los valores de la semilla(seed)</returns>
        public VcSeedData ReenviaSeedSMSOtp(int idUser, int idApp, string celular = "")
        {
            try
            {
                var mySql = new MySqlDataAccess();
                var tkData = mySql.GetUsuario(idUser);
                if (tkData == null)
                    return new VcSeedData { Seed = string.Empty, Status = false,
                        DescStatus = "No exite un usuario con el id: " + idUser + "." };
                var valIdApp = "CreaVCUser" + idApp.ToString(CultureInfo.InvariantCulture);
                var tk = mySql.GetTokenUsuario(tkData.Usuario, valIdApp);
                if (tk != null)
                {
                    var otp = TokenBroxel.GeneraNumero(tk.TokenSeed, DateTime.Now);
                    var sms = MandaSeedSMS(otp, (string.IsNullOrEmpty(celular)) ? tkData.Celular : celular);
                    var updToken = mySql.UpdOtpToken(otp, tkData.Usuario, valIdApp);

                    var status = (updToken && sms);
                    var desc = status ? "otp creado y enviado con éxito." : "No se pudo enviar y/o actualizar el otp.";
                    var seed = status ? tk.TokenSeed : string.Empty;
                    return new VcSeedData { Seed = seed, Status = status, DescStatus = desc };
                }
                return new VcSeedData
                {
                    Seed = string.Empty, Status = false,
                    DescStatus = "No exite token para el usuario: " + tkData.Usuario + " con Id App: " + valIdApp + "."
                };

            }
            catch (Exception e)
            {
                return new VcSeedData { Seed = string.Empty, Status = false, DescStatus = "Error al generar y reenviar el token otp." };
            }
        }

        /// <summary>
        /// Genera y manda token al telefono registrado
        /// </summary>
        /// <param name="tkData">Datos del token, usuario y celular</param>
        /// <param name="idApp">Id de Aplicación</param>
        /// <param name="idCreaLog">Id de tarjeta de log Creacion de cliente</param>
        /// <param name="enviaSeed">Determina si se envia o no el seed creado</param>
        /// <returns></returns>
        public bool GeneraYMandaToken(VcTokenData tkData, int idApp, long idCreaLog, bool enviaSeed)
        {
            try
            {
                var pwd = AesEncrypterToken.Encrypt("CreaVC" + idApp.ToString(CultureInfo.InvariantCulture));
                var hoy = DateTime.Now;
                IFormatProvider culture = new System.Globalization.CultureInfo("es-MX", true);
                var hoyS = hoy.ToString("dd DE MMMM 2014, hh:mm:ss", culture).ToUpper();
                var pwd2 = AesEncrypterToken.Encrypt(pwd + "|" + hoyS);
                var seed = new TokenService().GetTokenSeed(tkData.Usuario, "CreaVCUser" + idApp.ToString(CultureInfo.InvariantCulture), pwd2, tkData.Celular);
                if (enviaSeed)
                    MandaSeedSMS(seed, tkData.Celular);
                try
                {
                    using (var ctx = new broxelco_rdgEntities())
                    {
                        var cLog = ctx.CreaClienteSinTarjetaLog.FirstOrDefault(x => x.id == idCreaLog);
                        if (cLog != null)
                        {
                            cLog.tieneToken = true;
                            ctx.Entry(cLog).State = EntityState.Modified;
                            ctx.SaveChanges();
                        }
                    }
                }
                catch (Exception)
                {
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        /// <summary>
        /// Genera y manda token al telefono registrado retornando los valores de la semilla(seed)
        /// </summary>
        /// <param name="tkData">Datos del token, usuario y celular</param>
        /// <param name="idApp">Id de Aplicación</param>
        /// <param name="idCreaLog">Id de tarjeta de log Creacion de cliente</param>
        /// <param name="enviaSeed">Determina si se envia o no el seed creado</param>
        /// <returns></returns>
        public VcSeedData GeneraYMandaTokenOtp(VcTokenData tkData, int idApp, long idCreaLog, bool enviaSeed)
        {
            try
            {
                var mySql = new MySqlDataAccess();
                var sms = false;
                var pwd = AesEncrypterToken.Encrypt("CreaVC" + idApp.ToString(CultureInfo.InvariantCulture));
                var hoy = DateTime.Now;
                IFormatProvider culture = new CultureInfo("es-MX", true);
                var hoyS = hoy.ToString("dd DE MMMM 2014, hh:mm:ss", culture).ToUpper();
                var pwd2 = AesEncrypterToken.Encrypt(pwd + "|" + hoyS);
                var valIdApp = "CreaVCUser" + idApp.ToString(CultureInfo.InvariantCulture);
                var seed = new TokenService().GetTokenSeed(tkData.Usuario, valIdApp, pwd2, tkData.Celular);
                var otp = TokenBroxel.GeneraNumero(seed, DateTime.Now);
                if (enviaSeed)
                    sms = MandaSeedSMS(otp, tkData.Celular);
                var updToken = mySql.UpdOtpToken(otp, tkData.Usuario, valIdApp);
                try
                {
                    using (var ctx = new broxelco_rdgEntities())
                    {
                        var cLog = ctx.CreaClienteSinTarjetaLog.FirstOrDefault(x => x.id == idCreaLog);
                        if (cLog != null)
                        {
                            cLog.tieneToken = true;
                            ctx.Entry(cLog).State = EntityState.Modified;
                            ctx.SaveChanges();
                        }
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
                var status = (updToken && sms);
                var desc = status ? "otp creado y enviado con éxito.": "No se pudo enviar y actualizar el otp.";
                return new VcSeedData { Seed = seed, Status = status, DescStatus = desc };
            }
            catch (Exception)
            {
                return new VcSeedData { Seed = string.Empty, Status = false, DescStatus = "Error al generar y enviar el token otp." };
            }
        }

        /// <summary>
        /// Obtiene la tarjeta virtual del pull
        /// </summary>
        /// <param name="idApp">id de la aplicacón</param>
        /// <param name="prod">código de producto</param>
        /// <returns></returns>
        public MaquilaVcInfo GetMaquilaFromPull(int idApp, string prod)
        {
            return new MySqlDataAccess().GetIdMaquilaFromPull(idApp, prod);
        }

        /// <summary>
        /// Obtiene la tarjeta virtual de AON del pull
        /// </summary>
        /// <param name="idApp">id de la aplicacón</param>
        /// <param name="prod">código de producto</param>
        /// <returns></returns>
        public MaquilaVcInfo GetMaquilaAonFromPull(int idApp, string prod)
        {
            return new MySqlDataAccess().GetIdMaquilaAonFromPull(idApp, prod);
        }

        /// <summary>
        /// Valida si un usuario tiene o no cuenta aún
        /// </summary>
        /// <param name="idUsuario">id de Usuario</param>
        /// <returns></returns>
        public int ValidaUsuarioSinCuenta(int idUsuario)
        {
            using (var ctx = new broxelco_rdgEntities())
            {
                var data =
                    ctx.CreaClienteSinTarjetaLog.FirstOrDefault(
                        x => x.idUsuarioOnlineBroxel == idUsuario && x.tieneCuenta == false);
                return data != null ? data.id : 0;
            }
        }

        /// <summary>
        /// Actualiza si un cliente tiene una cuenta (aunque no sea virtual o fisica MYO
        /// </summary>
        /// <param name="idUsuario">idUsuario</param>
        public void ActualizaTieneCuenta(int idUsuario)
        {
            using (var ctx = new broxelco_rdgEntities())
            {
                var data = ctx.CreaClienteSinTarjetaLog.FirstOrDefault(x => x.idUsuarioOnlineBroxel == idUsuario);
                if (data == null) return;
                data.tieneCuenta = true;
                ctx.Entry(data).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }
        /// <summary>
        /// Actualiza el id de usuario Broxel asignado al usuario sin tarjeta.
        /// </summary>
        /// <param name="id">id de tabla de log de creacion de clientes</param>
        /// <param name="idUsuario">id de usuario Broxel Online</param>
        /// <returns></returns>
        public bool UpdateIdUsuarioBroxelOnlineLog(long id, int idUsuario)
        {
            using (var ctx = new broxelco_rdgEntities())
            {
                var cLog = ctx.CreaClienteSinTarjetaLog.FirstOrDefault(x => x.id == id);
                if (cLog == null) return false;
                cLog.idUsuarioOnlineBroxel = idUsuario;
                ctx.Entry(cLog).State = EntityState.Modified;
                ctx.SaveChanges();
                return true;
            }
            return false;
        }
        /// <summary>
        /// Obtiene la configuración default para el skin
        /// </summary>
        /// <param name="idApp">id de la aplicación</param>
        /// <returns></returns>
        public clienteBroxelLayout GetClienteBroxelLayout(int idApp)
        {
            using (var ctx = new broxelco_rdgEntities())
            {
                return ctx.clienteBroxelLayout.FirstOrDefault(x => x.idApp == idApp);
            }
        }

        /// <summary>
        /// Log de creación de usuarios sin tarjeta
        /// </summary>
        /// <param name="pNombre">Primer Nombre</param>
        /// <param name="sNombre">Segundo Nombre</param>
        /// <param name="aPaterno">Apellido Paterno</param>
        /// <param name="aMaterno">Apellido Materno</param>
        /// <param name="rfc">RFC</param>
        /// <param name="sexo">sexo</param>
        /// <param name="calle">calle</param>
        /// <param name="numeroExt">numero exterior</param>
        /// <param name="numeroInt">numero interior</param>
        /// <param name="codigoPostal">codigo postal</param>
        /// <param name="colonia">colonia</param>
        /// <param name="delegacionMunicipio">Delegación o domicilio</param>
        /// <param name="ciudad">Ciudad</param>
        /// <param name="estado">Estado</param>
        /// <param name="usuario">usuario</param>
        /// <param name="celular">celular</param>
        /// <param name="telefono">telefono</param>
        /// <param name="fechaNacimiento">fecha de nacimiento</param>
        /// <param name="contrasenia">contraseña</param>
        /// <param name="noEmpleado">numero de empleado</param>
        /// <param name="idApp">id de aplicación</param>
        /// <param name="enviaSeedRegistro">indica si se envia el seed en el registro o en la creación de la tarjeta</param>
        /// <returns>Id Generado para el log de creación de cliente</returns>
        public long GeneraCreaClienteLog(string pNombre, string sNombre, string aPaterno, string aMaterno, string rfc,
            string sexo,
            string calle, string numeroExt, string numeroInt, string codigoPostal, string colonia,
            string delegacionMunicipio, string ciudad, string estado,
            string usuario, string celular, string telefono, string fechaNacimiento,
            string contrasenia, string noEmpleado, int idApp, bool enviaSeedRegistro = false)
        {
            var res = 0;
            try
            {
                using (var ctx = new broxelco_rdgEntities())
                {
                    var clienteLogUser = (from s in ctx.CreaClienteSinTarjetaLog
                        where s.usuario == usuario && s.idUsuarioOnlineBroxel != 0
                        select s).FirstOrDefault();
                        
                    if (clienteLogUser != null)
                    {
                        res = clienteLogUser.id;
                    }
                    else
                    {
                        var clienteLog = new CreaClienteSinTarjetaLog
                        {
                            pNombre = pNombre,
                            sNombre = sNombre,
                            aPaterno = aPaterno,
                            aMaterno = aMaterno,
                            rfc = rfc,
                            calle = calle,
                            sexo = sexo,
                            numeroExt = numeroExt,
                            numeroInt = numeroInt,
                            codigoPostal = codigoPostal,
                            colonia = colonia,
                            delegacionMunicipio = delegacionMunicipio,
                            ciudad = ciudad,
                            estado = estado,
                            usuario = usuario,
                            celular = celular,
                            telefono = telefono,
                            fechaNacimiento = Convert.ToDateTime(fechaNacimiento),
                            contrasenia = Helper.Cifrar(contrasenia),
                            noEmpleado = noEmpleado,
                            idApp = idApp,
                            claveCliente = "UNDEFINED",
                            idUsuarioOnlineBroxel = 0,
                            tieneToken = false,
                            tieneCuenta = false,
                            cuenta = "UNDEFINED",
                            enviaTokenRegistro = enviaSeedRegistro,
                            folioODT = "UNDEFINED",
                            folioRenominacion = "UNDEFINED"
                        };
                        ctx.CreaClienteSinTarjetaLog.Add(clienteLog);
                        ctx.SaveChanges();
                        res = clienteLog.id;
                    }
                }
            }
            catch (Exception e)
            {
                res = 0;
            }
            return res;
        }

        /// <summary>
        /// Log de creación de usuarios sin tarjeta con usuario Broxel
        /// </summary>
        /// <param name="pNombre">Primer Nombre</param>
        /// <param name="sNombre">Segundo Nombre</param>
        /// <param name="aPaterno">Apellido Paterno</param>
        /// <param name="aMaterno">Apellido Materno</param>
        /// <param name="rfc">RFC</param>
        /// <param name="sexo">sexo</param>
        /// <param name="calle">calle</param>
        /// <param name="numeroExt">numero exterior</param>
        /// <param name="numeroInt">numero interior</param>
        /// <param name="codigoPostal">codigo postal</param>
        /// <param name="colonia">colonia</param>
        /// <param name="delegacionMunicipio">Delegación o domicilio</param>
        /// <param name="ciudad">Ciudad</param>
        /// <param name="estado">Estado</param>
        /// <param name="usuario">usuario</param>
        /// <param name="celular">celular</param>
        /// <param name="telefono">telefono</param>
        /// <param name="fechaNacimiento">fecha de nacimiento</param>
        /// <param name="contrasenia">contraseña</param>
        /// <param name="noEmpleado">numero de empleado</param>
        /// <param name="idApp">id de aplicación</param>
        /// <param name="enviaSeedRegistro">indica si se envia el seed en el registro o en la creación de la tarjeta</param>
        /// <param name="idUsuarioBroxel">id de usuario broxel </param>
        /// <returns>Id Generado para el log de creación de cliente</returns>
        public long GeneraCreaClienteLogBroxel(string pNombre, string sNombre, string aPaterno, string aMaterno, string rfc,
            string sexo,
            string calle, string numeroExt, string numeroInt, string codigoPostal, string colonia,
            string delegacionMunicipio, string ciudad, string estado,
            string usuario, string celular, string telefono, string fechaNacimiento,
            string contrasenia, string noEmpleado, int idApp, int idUsuarioBroxel, bool enviaSeedRegistro = false)
        {
            var res = 0;
            try
            {
                using (var ctx = new broxelco_rdgEntities())
                {
                    var clienteLog = new CreaClienteSinTarjetaLog
                    {
                        pNombre = pNombre,
                        sNombre = sNombre,
                        aPaterno = aPaterno,
                        aMaterno = aMaterno,
                        rfc = rfc,
                        calle = calle,
                        sexo = sexo,
                        numeroExt = numeroExt,
                        numeroInt = numeroInt,
                        codigoPostal = codigoPostal,
                        colonia = colonia,
                        delegacionMunicipio = delegacionMunicipio,
                        ciudad = ciudad,
                        estado = estado,
                        usuario = usuario,
                        celular = celular,
                        telefono = telefono,
                        fechaNacimiento = Convert.ToDateTime(fechaNacimiento),
                        contrasenia = Helper.Cifrar(contrasenia),
                        noEmpleado = noEmpleado,
                        idApp = idApp,
                        claveCliente = "UNDEFINED",
                        idUsuarioOnlineBroxel = idUsuarioBroxel,
                        tieneToken = false,
                        tieneCuenta = false,
                        cuenta = "UNDEFINED",
                        enviaTokenRegistro = enviaSeedRegistro,
                        folioODT = "UNDEFINED",
                        folioRenominacion = "UNDEFINED"
                    };
                    ctx.CreaClienteSinTarjetaLog.Add(clienteLog);
                    ctx.SaveChanges();
                    res = clienteLog.id;
                }
            }
            catch (Exception e)
            {
                res = 0;
            }
            return res;
        }

        /// <summary>
        /// Actualiza número de celular de usuarios sin cuenta, registrados en UsuariosBroxelOnline
        /// </summary>
        /// <param name="idUser">identificador del usuario</param>
        /// <param name="idApp">identificador de la aplicación</param>
        /// <param name="newCelular">nuevo numero de celular</param>
        /// <returns></returns>
        public bool ActualizaCelularUsuariosSinCuenta(int idUser, int idApp, string newCelular)
        {
            var step = 0;
            try
            {
                using (var ctx = new broxelco_rdgEntities())
                {
                    //Actualizar telefono en UsuariosOnlineBroxel
                    var uOb = ctx.UsuariosOnlineBroxel.FirstOrDefault(u => u.Id == idUser);
                    if (uOb == null)
                        return false;
                    uOb.Celular = newCelular;
                    ctx.Entry(uOb).State = EntityState.Modified;
                    ctx.SaveChanges();
                    step = 1;
                    //Actualizar CreaClientes
                    var cLog =
                        ctx.CreaClienteSinTarjetaLog.FirstOrDefault(
                            c => c.idUsuarioOnlineBroxel == idUser && c.idApp == idApp);
                    if (cLog == null)
                        return true;
                    cLog.celular = newCelular;
                    ctx.Entry(cLog).State = EntityState.Modified;
                    ctx.SaveChanges();
                    //Actualiza TokenBroxel
                    new MySqlDataAccess().UpdDeviceToken(uOb.Usuario, "CreaVCUser" + idApp.ToString(CultureInfo.InvariantCulture), newCelular);
                    return true;
                }
            }
            catch (Exception)
            {
                return step == 1;
            }
        }

        /// <summary>
        /// Activa cuenta
        /// </summary>
        /// <param name="numCuenta">numero de cuenta a activar</param>
        /// <returns></returns>
        public bool ActivarCuentaTarjetaVirtual(string numCuenta)
        {
            try
            {
                var taskActivacion = new Task(() => new BroxelService().ActivacionDeCuenta(numCuenta, "TarjetaVirtualOnDemand"));
                taskActivacion.Start();
            }
            catch (Exception e)
            {
                Helper.SendMail("broxelonline@broxel.com", "josesalvador.macias@broxel.com", "TarjetaVirtualOnDemand", "Error al activar cuenta " + numCuenta + " : " + e, "67896789");
                return false;
            }
            return true;
        }
		/// <summary>
		/// Obtiene los datos de cualquier tarjeta y encripta
		/// </summary>
		/// <returns></returns>
		public VCardData GetCardDataPagoServicio(Tarjeta t, int idPsc)
		{
			var j = new JVCData { Tarjeta = t.NumeroTarjeta, Clabe = t.Cuenta, Cvv = t.Cvc2, FechaVencimiento = t.FechaExpira, Nombre = t.NombreTarjeta, FechaConsulta = DateTime.Now.ToString("O") };
			var r2 = new Random().Next(1, 3);
			var jS = BuildRandomDataTrack(j);
			var fS = AesEncrypter.Encrypt(jS, Convert.ToString(idPsc));
			var idConsul = new Random().Next(99999);

			var insert = new MySqlDataAccess().instertPscDatosAd(fS,idPsc);
			if (insert == true)
			{
				return new VCardData { IdTran = Convert.ToInt64(r2.ToString(CultureInfo.InvariantCulture) + idConsul.ToString(CultureInfo.InvariantCulture)), Track1 = jS, Track2 = fS };
			}
			else
			{
				return new VCardData { IdTran = 0, Track1 = "Error al insertar en la base de datos", Track2 = "No se pudo completar la transacción" };
			}
		}
		/// <summary>
		/// Deserealiza y desencrypta los datos de una tarjeta
		/// </summary>
		/// <returns></returns>
		public JVCData GetDeserealizePagoServicio(string obj, int id)
		{
			var des = AesEncrypter.Decrypt(obj, Convert.ToString(id));
			var response = JsonConvert.DeserializeObject<JVCData>(des);
			return response;
		}
		#endregion

		#region Metodos privados

		/// <summary>
		/// Envia semilla por SMS
		/// </summary>
		/// <param name="seed">semilla</param>
		/// <param name="celular">telefono celular</param>
		/// <returns>true si pudo enviar SMS, false en caso de error</returns>
		private bool MandaSeedSMS(string seed, string celular)
        {
            var msg = ConfigurationManager.AppSettings["VCSMS"];
            var proxySms = new ServicioSMSClient();
            var host = ConfigurationManager.AppSettings["OtpHostSMS"];
            var cred = new Credenciales
            {
                Username = "br0x3l",
                Password = "bTEax3l",
                Host = !string.IsNullOrEmpty(host) ? host : "http://api.c3ntrosms.com:8585/Api/rec.php"
            };
            var sms = new SMS
            {
                Mensaje = msg.Replace("{0}", seed),
                Telefono = celular
            };
            var res = proxySms.EnviarSMSC3ntro(sms, cred);
            return res.Enviado;
        }

        /// <summary>
        /// Método para generar el track de información con posiciones Random
        /// </summary>
        /// <param name="data">Track JSON con información transaccional de la tarjeta</param>
        /// <returns>Track JSON con datos colocados de forma aleatoria</returns>
        private static string BuildRandomDataTrack(JVCData data)
        {
            var attributes = data.GetType().GetProperties();
            var jAtributtes = attributes.Select(attribute => "\"" + attribute.Name + "\":\"" + attribute.GetValue(data, null) + "\"").ToList();
            var count = jAtributtes.Count;
            var jString = "{";
            var r = new Random();
            while (count != 0)
            {
                var i = r.Next(count) - 1;
                var index = i > 0 ? i : 0;
                jString += jAtributtes[index];
                jAtributtes.RemoveAt(index);
                count--;
                if (count != 0)
                    jString += ", ";
            }
            jString += "}";
            return jString;
        }

        /// <summary>
        /// Genera un track falso
        /// </summary>
        /// <param name="len">Longitud del track JSON con información transaccional</param>
        /// <returns>Una cadena falsa con valores alfanumericos aleatorios.</returns>
        private static string BuildFakeTrack(int len)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[len];
            var random = new Random();

            for (var i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new string(stringChars);
            return finalString;
        }

        private bool GetEnviaSeedAlCrearTarjeta(int idLog)
        {
            try
            {
                using (var ctx = new broxelco_rdgEntities())
                {
                    var cLog = ctx.CreaClienteSinTarjetaLog.FirstOrDefault(x => x.id == idLog);
                    if (cLog != null)
                    {
                        return !cLog.enviaTokenRegistro;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public bool VerificaCreaCliente(int idApp)
        {
            try
            {
                var ctx = new broxelco_rdgEntities();

                var clienteBroxelLayout = ctx.clienteBroxelLayout.SingleOrDefault(x => x.idApp == idApp);
                return (clienteBroxelLayout != null && clienteBroxelLayout.creaCliente);
            }

            catch (Exception e)
            {
                return false;
            }

        }

        public bool VerificaCuentaActiva(int idUsuario)
        {
            try
            {
                var ctx = new broxelco_rdgEntities();
                var creaClienteSinTarjetaLog = ctx.CreaClienteSinTarjetaLog.SingleOrDefault(x => x.idUsuarioOnlineBroxel == idUsuario);
                if (creaClienteSinTarjetaLog == null) return false;
                var verificador = creaClienteSinTarjetaLog.tieneCuenta;
                return verificador != null && (bool)verificador;
            }

            catch (Exception e)
            {
                Trace.WriteLine("Error al verificar si el usuario tiene una cuenta AON: " + e.Message);
                return false;
            }
        }

        public bool verificarClienteLog(int idUsuario)
        {
            bool respuesta = false;
            try
            {
                var ctx = new broxelco_rdgEntities();

                var clienteSinTarjetaLog = ctx.CreaClienteSinTarjetaLog.SingleOrDefault(x => x.idUsuarioOnlineBroxel == idUsuario);

                if (clienteSinTarjetaLog != null)
                    respuesta = true;
                else
                    respuesta = false;
            }
            catch (Exception e)
            {
                Trace.WriteLine("Error al verificar si el usuario existe en la tabla de CreaClienteSinTarjetaLog: " + e.Message);
                respuesta = false;
            }

            return respuesta;
        }

        public bool verificaCuentasExistentes(string codProducto)
        {
            var ctx = new broxelco_rdgEntities();
            var query = "Select ifnull(count(b.id),0) as NumTarjetas, a.producto as Producto from clienteBroxelLayout a " +
                      "left join maquila b on a.producto = b.producto and a.pull = b.clave_cliente " +
                      "where a.producto = '" + codProducto + "' group by a.producto";

            var res = ctx.Database.SqlQuery<TarjetaDisponibles>(query).FirstOrDefault();
            return res != null && res.NumTarjetas > 0;
        }

        private class TarjetaDisponibles
        {
            public int NumTarjetas { get; set; }
            public string Producto { get; set; }
        }

        #endregion
    }
}