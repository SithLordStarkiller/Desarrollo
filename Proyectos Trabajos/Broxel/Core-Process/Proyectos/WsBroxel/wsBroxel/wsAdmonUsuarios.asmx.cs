using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Management;
using System.Web.Services;
using IdSecure;
using wsBroxel.App_Code;
using wsBroxel.App_Code.GenericBL.Model;
using wsBroxel.App_Code.Online;
using wsBroxel.App_Code.PreAutorizador;
using wsBroxel.App_Code.RenominacionBL;
using wsBroxel.App_Code.RenominacionBL.Model;
using wsBroxel.App_Code.RequestResponses;
using wsBroxel.App_Code.SolicitudBL;
using wsBroxel.App_Code.SolicitudBL.Model;
using wsBroxel.App_Code.GenericBL;
using wsBroxel.App_Code.TCControlVL;
using wsBroxel.App_Code.TCControlVL.Model;
using wsBroxel.App_Code.Utils;
using wsBroxel.App_Code.VCBL;
using wsBroxel.App_Code.VCBL.Models;
using wsBroxel.App_Code.AsignacionLineaBL;
using System.Web.Script.Serialization;
using wsBroxel.App_Code.TokenBL;
using System.Reflection;
using System.Data.SqlClient;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using wsBroxel.BussinessLogic;

namespace wsBroxel
{
    /// <summary>
    /// Summary description for wsAdmonUsuarios
    /// </summary>
    [WebService(Namespace = "wsAdmonUsuarios")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    // ReSharper disable once InconsistentNaming
    public class wsAdmonUsuarios : WebService
    {
        BroxelService _bs = new BroxelService();
        private const int CUENTA_LENGHT = 9;

        #region Methods Exposed

        #region Usuarios

        #region Consultas Usuario

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        [WebMethod]
        public UsuariosOnlineBroxel GetUser(int idUser)
        {
            broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
            var userData = broxelcoRdgEntities.UsuariosOnlineBroxel.FirstOrDefault(x => x.Id == idUser);
            if (userData != null) return userData;
            idUser = new IdSecureComp().GetIdUserValid(idUser);
            userData = broxelcoRdgEntities.UsuariosOnlineBroxel.FirstOrDefault(x => x.Id == idUser);
            return userData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [WebMethod]
        public UsuariosOnlineBroxel GetUserByMail(String email)
        {
            broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
            return broxelcoRdgEntities.UsuariosOnlineBroxel.FirstOrDefault(x => x.CorreoElectronico == email);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <param name="tipoLogin"></param>
        /// <returns></returns>
        [WebMethod]
        public UsuarioOnlineResponse Login(String userName, String passWord, int tipoLogin = 0)
        {
            UsuarioOnlineRequest uor = new UsuarioOnlineRequest();
            return uor.Login(userName, passWord, tipoLogin);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [WebMethod]
        public LoginComercioResponse LoginComerci(String usuario, String password)
        {
            broxelco_rdgEntities dbBroxelcoRdgEntities = new broxelco_rdgEntities();
            LoginComercioResponse lcr = new LoginComercioResponse();
            String passwordEnc = Helper.CalculateMD5Hash(password);

            var user = dbBroxelcoRdgEntities.catalogo_acceso_comercios.FirstOrDefault(x => x.usuario == usuario);
            if (user != null && String.Equals(user.password, passwordEnc, StringComparison.CurrentCultureIgnoreCase))
            {
                lcr.Success = 1;
                lcr.UserResponse = "Login valido";
                lcr.idComercio = Convert.ToInt32(user.comercio);
                lcr.Afiliacion = user.afiliacion;
                lcr.idUsuario = user.id;
                lcr.CodigoRespuesta = -1;
            }
            else
            {
                lcr.UserResponse = "Login invalido";
            }
            return lcr;
        }

        #endregion

        #region Registrar Usuario

        /// <summary>
        /// Realiza el preregistro de un usuario
        /// </summary>
        /// <param name="nombreCompleto">Nombre Completo</param>
        /// <param name="celular">Numero telefonico de celular</param>
        /// <param name="correoElectronico">Correo Electronico / Usuario</param>
        /// <param name="numTarjeta">Numero de tarjeta</param>
        /// <param name="fechaExpiracion">Fecha de expiración de la tarjeta</param>
        /// <param name="palabraClave">palabra clave</param>
        /// <param name="contrasenia">Contraseña</param>
        /// <param name="host">Origen dela petición</param>
        /// <returns></returns>
        [WebMethod] //razonsocialcomercio
        public UsuarioOnlineResponse PreRegistrarUsuario(String nombreCompleto, String celular, String correoElectronico,
            String numTarjeta, String fechaExpiracion, String palabraClave, String contrasenia,
            String host = "online.broxel.com")
        {
            return RegistrarUsuarioBL(nombreCompleto, "", "", "", "", correoElectronico, correoElectronico, celular,
                default(DateTime), contrasenia, numTarjeta, fechaExpiracion, palabraClave, host);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nombreCompleto"></param>
        /// <param name="rfc"></param>
        /// <param name="telefono"></param>
        /// <param name="sexo"></param>
        /// <param name="codigoPostal"></param>
        /// <param name="usuario"></param>
        /// <param name="correoElectronico"></param>
        /// <param name="celular"></param>
        /// <param name="fechaNacimiento"></param>
        /// <param name="contrasenia"></param>
        /// <param name="numTarjeta"></param>
        /// <param name="fechaExpiracion"></param>
        /// <param name="host"></param>
        /// <returns></returns>
        [WebMethod]
        public UsuarioOnlineResponse RegistrarUsuario(String nombreCompleto, String rfc, String telefono,
            String sexo, String codigoPostal, String usuario, String correoElectronico, String celular,
            DateTime fechaNacimiento,
            String contrasenia, String numTarjeta, String fechaExpiracion, String host = "online.broxel.com")
        {
            return RegistrarUsuarioBL(nombreCompleto, rfc, telefono, sexo, codigoPostal, usuario, correoElectronico,
                celular, fechaNacimiento, contrasenia, numTarjeta, fechaExpiracion, "", host);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pNombre"></param>
        /// <param name="sNombre"></param>
        /// <param name="aPaterno"></param>
        /// <param name="aMaterno"></param>
        /// <param name="rfc"></param>
        /// <param name="sexo"></param>
        /// <param name="calle"></param>
        /// <param name="numeroExt"></param>
        /// <param name="numeroInt"></param>
        /// <param name="codigoPostal"></param>
        /// <param name="colonia"></param>
        /// <param name="delegacionMunicipio"></param>
        /// <param name="ciudad"></param>
        /// <param name="estado"></param>
        /// <param name="usuario"></param>
        /// <param name="celular"></param>
        /// <param name="telefono"></param>
        /// <param name="fechaNacimiento"></param>
        /// <param name="contrasenia"></param>
        /// <param name="noEmpleado"></param>
        /// <param name="idApp"></param>
        /// <param name="enviaSeedRegistro"></param>
        /// <param name="enviarEmailBienvenida"></param>
        /// <param name="esOTP"></param>
        /// <returns></returns>
        [WebMethod]
        public UsuarioOnlineResponse RegistrarUsuarioSinCuenta(string pNombre, string sNombre, string aPaterno,
            string aMaterno, string rfc, string sexo,
            string calle, string numeroExt, string numeroInt, string codigoPostal, string colonia,
            string delegacionMunicipio, string ciudad, string estado,
            string usuario, string celular, string telefono, string fechaNacimiento,
            string contrasenia, string noEmpleado, int idApp, bool enviaSeedRegistro = false,
            bool enviarEmailBienvenida = true, bool esOTP = false)
        {
            long idLog = 0;
            var vcBl = new VCardBL();
            clienteBroxelLayout cLayout = null;
            try
            {
                cLayout = vcBl.GetClienteBroxelLayout(idApp);
                if (cLayout == null)
                    throw new Exception();
            }
            catch (Exception)
            {
                return new UsuarioOnlineResponse
                {
                    Success = false,
                    UserResponse = "El tipo de aplicación es inválido o no está dado de alta."
                };
            }
            try
            {
                //valida usuario

                var broxelcoRdgEntities = new broxelco_rdgEntities();
                if (broxelcoRdgEntities.UsuariosOnlineBroxel.Count(x => x.Usuario == usuario) > 0 &&
                    broxelcoRdgEntities.CreaClienteSinTarjetaLog.FirstOrDefault(
                        s => s.usuario == usuario && s.idUsuarioOnlineBroxel != 0 && s.tieneCuenta == false) != null)
                {
                    //mandar semilla
                    var tkData = new VcTokenData {Usuario = usuario, Celular = celular};
                    if (esOTP)
                        vcBl.GeneraYMandaTokenOtp(tkData, idApp, idLog, true);
                    else
                        vcBl.GeneraYMandaToken(tkData, idApp, idLog, true);

                    BitacoraUsuariosOnlineBroxel("RegistroUsuario", "TBD", usuario,
                        idApp.ToString(CultureInfo.InvariantCulture));

                    return Login(usuario, contrasenia);
                }

                // Primero grabamos el log
                idLog = vcBl.GeneraCreaClienteLog(pNombre, sNombre, aPaterno, aMaterno, rfc,
                    sexo == "MASCULINO" ? "M" : "F", calle, numeroExt,
                    numeroInt, codigoPostal, colonia, delegacionMunicipio, ciudad, estado, usuario, celular, telefono,
                    fechaNacimiento, contrasenia, noEmpleado, idApp, enviaSeedRegistro);
                if (idLog > 0)
                {
                    //Generamos el UsuarioBroxelOnline.
                    usuario = usuario.Trim();
                    int idUsuario = -1;
                    if (celular.Length >= 13)
                        return new UsuarioOnlineResponse
                        {
                            Success = false,
                            UserResponse = "Longitud de celular incorrecta. Favor de ingresar 10 digitos."
                        };

                    try
                    {
                        UsuariosOnlineBroxel user = new UsuariosOnlineBroxel
                        {
                            NombreCompleto = pNombre + " " + sNombre + " " + aPaterno + " " + aMaterno,
                            RFC = rfc,
                            Sexo = sexo,
                            Telefono = telefono,
                            CP = codigoPostal,
                            Usuario = usuario,
                            CorreoElectronico = usuario,
                            Celular = celular,
                            FechaNacimiento = Convert.ToDateTime(fechaNacimiento),
                            Password = Helper.Cifrar(contrasenia),
                            FechaCreacion = DateTime.Now,
                            palabraClave = ""
                        };
                        broxelcoRdgEntities.UsuariosOnlineBroxel.Add(user);
                        broxelcoRdgEntities.SaveChanges();
                        idUsuario = user.Id;
                        if (!vcBl.UpdateIdUsuarioBroxelOnlineLog(idLog, idUsuario))
                            Helper.SendMail("broxelonline@broxel.com",
                                " alberto.ortiz@broxel.com, mauricio.lopez@broxel.com",
                                "Fallo registro de usuario sin cuenta mail " + usuario,
                                "Error : El usuario " + idUsuario + " no se pudo conectar al log " + idLog, "67896789");
                        BitacoraUsuariosOnlineBroxel("RegistroOK", "TBD", usuario, cLayout.host);
                        if (enviarEmailBienvenida)
                        {
                            Mailing.EnviaCorreoBienvenida(user.NombreCompleto, "Por asignar", usuario, usuario,
                                cLayout.host);
                        }
                        // Genera Token.
                        var tkData = new VcTokenData {Usuario = usuario, Celular = celular};
                        if (esOTP)
                            vcBl.GeneraYMandaTokenOtp(tkData, idApp, idLog, enviaSeedRegistro);
                        else
                            vcBl.GeneraYMandaToken(tkData, idApp, idLog, enviaSeedRegistro);
                        return Login(usuario, contrasenia);
                    }
                    catch (Exception e)
                    {
                        Helper.SendMail("broxelonline@broxel.com",
                            " alberto.ortiz@broxel.com, mauricio.lopez@broxel.com",
                            "Fallo registro de usuario sin cuenta mail " + usuario, "Error : " + e, "67896789");
                        BorrarUsuario(idUsuario, 0);
                        return new UsuarioOnlineResponse
                        {
                            Success = false,
                            UserResponse = "Ocurrio un error al crear el usuario. Intente nuevamente."
                        };
                    }
                }
                return new UsuarioOnlineResponse
                {
                    Success = false,
                    UserResponse = "No se pudo grabar en base de datos (ClienteLog)"
                };
            }
            catch (Exception e)
            {
                return new UsuarioOnlineResponse
                {
                    Success = false,
                    UserResponse = "Ocurrio un error al crear el usuario. Intente nuevamente."
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [WebMethod]
        public int RegistrarUsuarioMejoravit(OriginacionData data)
        {
            try
            {
                using (var ctx = new broxelco_rdgEntities())
                {
                    var user =
                        ctx.UsuariosOnlineBroxel.FirstOrDefault(
                            x => x.Usuario == data.Email || x.CorreoElectronico == data.Email);
                    var idSecComp = new IdSecureComp();
                    var tc = Helper.GetTarjetaFromCuenta(data.NumCuenta);

                    if (tc == null)
                        return -1;

                    if (user == null)
                    {
                        var resReg = RegistrarUsuarioBL(data.Nombre, "", data.TelefonoMovil, data.Genero,
                            data.CodigoPostal,
                            data.Email, data.Email, data.TelefonoMovil,
                            DateTime.ParseExact(data.FechaNacimiento, "yyyy-MM-dd", null),
                            "", tc.NumeroTarjeta, tc.FechaExpira, ConfigurationManager.AppSettings["MejoravitBatchId"],
                            "SALDOS.MEJORAVIT.COM");
                        if (resReg.Success)
                        {
                            var idUser = idSecComp.GetIdUserValid(resReg.UserBroxel.Id);
                            new wsDisposiciones().AgregaCLABE(resReg.UserBroxel.Id, data.Clabe, data.Email, data.Nombre,
                                "",
                                "2f28cb7e-4f76-4afd-b7a6-6f27827f92b0", "SALDOS.MEJORAVIT.COM");
                            return idUser;
                        }

                        return -1;
                    }
                    else
                    {
                        var resReg = RegistrarUsuarioBL(data.Nombre, "", data.TelefonoMovil, data.Genero,
                            data.CodigoPostal,
                            "dummyfor" + data.NumCuenta + "@broxel.com", "dummyfor" + data.NumCuenta + "@broxel.com",
                            data.TelefonoMovil,
                            DateTime.ParseExact(data.FechaNacimiento, "yyyy-MM-dd", null),
                            "", tc.NumeroTarjeta, tc.FechaExpira, ConfigurationManager.AppSettings["MejoravitBatchId"],
                            "SALDOS.MEJORAVIT.COM");
                        if (resReg.Success)
                        {
                            var idUser = idSecComp.GetIdUserValid(resReg.UserBroxel.Id);
                            new wsDisposiciones().AgregaCLABE(resReg.UserBroxel.Id, data.Clabe, data.Email, data.Nombre,
                                "",
                                "2f28cb7e-4f76-4afd-b7a6-6f27827f92b0", "SALDOS.MEJORAVIT.COM");
                            return idUser;
                        }
                        return -1;
                    }
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine("Error al pre registrar cuenta " + data.NumCuenta + ": " + e);
                return -1;
            }
        }

        #endregion

        #region Actualizar Usuario

        /// <summary>
        /// 
        /// </summary>
        /// <param name="celular"></param>
        /// <param name="correo"></param>
        /// <param name="codigoPostal"></param>
        /// <param name="fechaNacimiento"></param>
        /// <param name="id"></param>
        /// <param name="nombreCompleto"></param>
        /// <param name="rfc"></param>
        /// <param name="sexo"></param>
        /// <param name="contrasenia"></param>
        /// <returns></returns>
        [WebMethod]
        public UsuarioOnlineResponse ActualizarUsuario(String celular, String correo, String codigoPostal,
            String fechaNacimiento, int id, String nombreCompleto, String rfc, String sexo, String contrasenia)
        {

            if (celular.Length >= 13)
                return new UsuarioOnlineResponse
                {
                    Success = false,
                    UserResponse = "Longitud de celular incorrecta. Favor de ingresar 10 digitos."
                };
            if (contrasenia.Length > 0 && contrasenia.Length < 8)
                return new UsuarioOnlineResponse
                {
                    Success = false,
                    UserResponse = "Longitud de contraseña incorrecta. Favor de ingresar al menos 8 caracteres."
                };
            UsuariosOnlineBroxel aActualizar = new UsuariosOnlineBroxel
            {
                Celular = celular,
                CorreoElectronico = correo,
                CP = codigoPostal,
                FechaNacimiento = DateTime.Parse(fechaNacimiento),
                Id = id,
                NombreCompleto = nombreCompleto,
                RFC = rfc,
                Sexo = sexo
            };
            return ActualizaUsuario(aActualizar, contrasenia);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioAActualizar"></param>
        /// <param name="contrasenia"></param>
        /// <returns></returns>
        [WebMethod]
        public UsuarioOnlineResponse ActualizaUsuario(UsuariosOnlineBroxel usuarioAActualizar, String contrasenia = "")
        {
            //MLS idUserSecure
            var idUser = new IdSecureComp().GetIdUserValid(usuarioAActualizar.Id);
            if (idUser == 0)
            {
                return new UsuarioOnlineResponse
                {
                    Success = false,
                    UserResponse = "Usuario inválido o su sesión expiró."
                };
            }
            usuarioAActualizar.Id = idUser;
            if (contrasenia.Length > 0 && contrasenia.Length < 8)
                return new UsuarioOnlineResponse
                {
                    Success = false,
                    UserResponse = "Longitud de contraseña incorrecta. Favor de ingresar al menos 8 caracteres."
                };
            broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
            try
            {
                var user = broxelcoRdgEntities.UsuariosOnlineBroxel.FirstOrDefault(x => x.Id == usuarioAActualizar.Id);
                if (user != null)
                {
                    if (user.Usuario != usuarioAActualizar.Usuario)
                        if (
                            broxelcoRdgEntities.UsuariosOnlineBroxel.Count(x => x.Usuario == usuarioAActualizar.Usuario) >
                            0)
                            return new UsuarioOnlineResponse
                            {
                                Success = false,
                                UserResponse = "Este usuario ya esta registrado."
                            };
                    user.Usuario = usuarioAActualizar.Usuario;

                    // var cuentas = broxelcoRdgEntities.maquila.Where(x => x.usuario_web == usuarioAActualizar.Id);
                    var accesosCliente =
                        broxelcoRdgEntities.accessos_clientes.Where(
                            x => x.IdUsuarioOnlineBroxel == user.Id && x.IdMaquila != null).ToList();

                    foreach (var acceso in accesosCliente)
                    {
                        var maq = broxelcoRdgEntities.maquila.SingleOrDefault(x => x.id == acceso.IdMaquila);
                        //TODO Checar si esto tambien debe actualizar alias
                        AgregaOActualizaRelacion(usuarioAActualizar.Celular, acceso.cuenta, usuarioAActualizar.Usuario,
                            usuarioAActualizar.NombreCompleto, usuarioAActualizar.Id,
                            usuarioAActualizar.CorreoElectronico, maq.id, "");
                    }
                    if (contrasenia != "")
                        user.Password = Helper.Cifrar(contrasenia);
                    user.NombreCompleto = usuarioAActualizar.NombreCompleto;
                    user.RFC = usuarioAActualizar.RFC;
                    user.Telefono = usuarioAActualizar.Telefono;
                    user.CP = usuarioAActualizar.CP;
                    user.CorreoElectronico = usuarioAActualizar.CorreoElectronico;
                    user.Celular = usuarioAActualizar.Celular;
                    user.FechaNacimiento = usuarioAActualizar.FechaNacimiento;
                    broxelcoRdgEntities.SaveChanges();
                    return new UsuarioOnlineResponse
                    {
                        UserResponse = "TRANSACCION EXITOSA",
                        Success = true,
                        Fecha = DateTime.Now.ToShortTimeString()
                    };
                }
                return new UsuarioOnlineResponse {UserResponse = "Se generó un error al guardar los cambios"};
            }
            catch (Exception ex)
            {
                return new UsuarioOnlineResponse();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="numCuenta"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        [WebMethod]
        public bool ActualizaAlias(int id, string numCuenta, string alias)
        {
            //MLS idUserSecure
            var idUser = new IdSecureComp().GetIdUserValid(id);
            if (idUser == 0)
            {
                return false;
            }
            id = idUser;

            using (var ctx = new broxelco_rdgEntities())
            {
                var rel =
                    ctx.accessos_clientes.FirstOrDefault(a => a.IdUsuarioOnlineBroxel == id && a.cuenta == numCuenta);
                if (rel == null) return false;
                rel.Alias = alias;
                ctx.Entry(rel).State = EntityState.Modified;
                ctx.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="idApp"></param>
        /// <param name="newCelular"></param>
        /// <returns></returns>
        [WebMethod]
        public bool ActualizaCelularUsrSinCuenta(int idUser, int idApp, string newCelular)
        {
            //MLS idUserSecure
            var id = new IdSecureComp().GetIdUserValid(idUser);
            if (id == 0)
            {
                return false;
            }
            idUser = id;
            return new VCardBL().ActualizaCelularUsuariosSinCuenta(idUser, idApp, newCelular);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="idDispositivo"></param>
        /// <param name="tipo"></param>
        /// <param name="notificar"></param>
        /// <returns></returns>
        [WebMethod]
        public bool EstablecerDispositivo(string usuario, string idDispositivo, string tipo, string notificar)
        {
            broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
            try
            {
                var usuarioOnlineBroxel =
                    broxelcoRdgEntities.UsuariosOnlineBroxel.FirstOrDefault(x => x.Usuario == usuario);
                if (usuarioOnlineBroxel != null)
                {
                    var dispositivo =
                        broxelcoRdgEntities.dispositivos.FirstOrDefault(
                            x => x.IdUsuarioOnlineBroxel == usuarioOnlineBroxel.Id && x.IdDispositivo == idDispositivo);
                    if (notificar == "1")
                    {
                        if (dispositivo != null)
                        {
                            dispositivo.Activo = 1;
                            broxelcoRdgEntities.SaveChanges();
                            return true;
                        }
                        var d = new dispositivos
                        {
                            Activo = 1,
                            IdDispositivo = idDispositivo,
                            IdUsuarioOnlineBroxel = usuarioOnlineBroxel.Id,
                            Tipo = tipo
                        };
                        broxelcoRdgEntities.dispositivos.Add(d);
                        broxelcoRdgEntities.SaveChanges();
                        return true;
                    }
                    if (notificar == "0")
                    {
                        if (dispositivo != null)
                        {
                            dispositivo.Activo = 0;
                            broxelcoRdgEntities.SaveChanges();
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region Favoritos

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="numTarjeta"></param>
        /// <param name="alias"></param>
        /// <param name="host"></param>
        /// <returns></returns>
        [WebMethod]
        public bool InsertaFavorito(int id, string numTarjeta, string alias, string host = "")
        {
            try
            {
                //MLS idUserSecure
                var idUser = new IdSecureComp().GetIdUserValid(id);
                if (idUser == 0)
                {
                    throw new Exception("Usuario inválido o la sesión expiró");
                }
                id = idUser;

                using (var ctx = new broxelco_rdgEntities())
                {
                    var cuenta = Helper.GetCuentaFromTarjeta(numTarjeta);
                    if (string.IsNullOrEmpty(cuenta))
                    {
                        cuenta = Helper.GetCuentaFromTarjetaAdicional(numTarjeta);
                        if(String.IsNullOrEmpty(cuenta))
                            throw new Exception("La tarjeta es inválida");
                    }

                    var valida = new MySqlDataAccess();
                    if (valida.validarMerchant(cuenta))
                    {
                        return false;
                    }
                    if (valida.ValidaProductoMejoravit(cuenta))
                    {
                        return false;
                    }

                    var valfav = ctx.OnLineFavoritos.FirstOrDefault(x => x.idUser == id && x.cuentaFavorita == cuenta);
                    if (valfav != null)
                    {
                        if (valfav.status == 0)
                        {
                            valfav.status = 1;
                            valfav.alias = alias;
                            ctx.Entry(valfav).State = EntityState.Modified;
                            ctx.SaveChanges();
                        }
                        return true;
                    }

                    var fav = new OnLineFavoritos
                    {
                        idUser = id,
                        cuentaFavorita = cuenta,
                        alias = alias,
                        status = 1
                    };
                    ctx.OnLineFavoritos.Add(fav);
                    ctx.SaveChanges();

                    try
                    {
                        if (!(String.IsNullOrEmpty(host)) &&
                            ConfigurationManager.AppSettings["EnviarCorreoAviso"] == "1")
                        {
                            var usuario = ctx.UsuariosOnlineBroxel.FirstOrDefault(x => x.Id == id);
                            string tarjetaMask = numTarjeta.Substring(6) + "** ****" +
                                                 numTarjeta.Substring(numTarjeta.Length - 4);
                            //if (usuario != null)
                            // Mailing.EnviaCorreoAvisoMovimiento("Tarjeta Favorita", 1, "tarjeta favorita", tarjetaMask, usuario.NombreCompleto, host, usuario.CorreoElectronico);
                        }
                    }
                    catch (Exception ex)
                    {
                        Helper.SendMail("broxelonline@broxel.com", "luis.huerta@broxel.com",
                            "EnviaCorreoAvisoMovimiento",
                            "Se intento enviar el correo de notificación de que se dio de alta una tarjeta favorita  con la excpeción " +
                            ex, "67896789");
                    }
                    return true;
                }

            }
            catch (Exception e)
            {
                Trace.WriteLine(DateTime.Now.ToString("O") + "Inserta´Favorito: Error :" + e);
                throw;
            }
        }

        /// <summary>
        /// Metodo para eliminar cuentas favoritas
        /// </summary>
        /// <param name="idUsuario">Id de usuario</param>
        /// <param name="idFavorito">Id de favorito</param>
        /// <returns></returns>
        [WebMethod]
        public bool EliminaFavorito(int idUsuario, int idFavorito, string host = "")
        {
            try
            {
                //MLS idUserSecure
                var idUser = new IdSecureComp().GetIdUserValid(idUsuario);
                if (idUser == 0)
                {
                    return false;
                }
                idUsuario = idUser;


                using (var ctx = new broxelco_rdgEntities())
                {
                    var valfav = ctx.OnLineFavoritos.FirstOrDefault(x => x.idUser == idUsuario && x.id == idFavorito);
                    if (valfav != null)
                    {
                        if (valfav.status == 1)
                        {
                            valfav.status = 0;
                            ctx.Entry(valfav).State = EntityState.Modified;
                            ctx.SaveChanges();

                            try
                            {
                                if (!(String.IsNullOrEmpty(host)) &&
                                    ConfigurationManager.AppSettings["EnviarCorreoAviso"] == "1")
                                {
                                    var usuario = ctx.UsuariosOnlineBroxel.FirstOrDefault(x => x.Id == idUsuario);
                                    string tarjetaFavorita =
                                        Helper.GetTarjetaFromCuenta(valfav.cuentaFavorita).NumeroTarjeta;
                                    string tarjetaMask = tarjetaFavorita.Substring(6) + "** ****" +
                                                         tarjetaFavorita.Substring(tarjetaFavorita.Length - 4);
                                    //if (usuario != null)
                                    //Mailing.EnviaCorreoAvisoMovimiento("Tarjeta Favorita", 0, "tarjeta favorita", tarjetaMask, usuario.NombreCompleto, host, usuario.CorreoElectronico);
                                }
                            }
                            catch (Exception ex)
                            {
                                Helper.SendMail("broxelonline@broxel.com", "luis.huerta@broxel.com",
                                    "EnviaCorreoAvisoMovimiento",
                                    "Se intento enviar el correo de notificación de que se dio de baja una tarjeta favorita  con la excpeción " +
                                    ex, "67896789");
                            }

                            return true;
                        }
                        throw new Exception("El favorito ya se habia eliminado");
                    }
                    throw new Exception("El favorito no existe");
                }

            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idFavorito"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        [WebMethod]
        public bool ActualizaAliasFavorito(int idFavorito, string alias)
        {
            using (var ctx = new broxelco_rdgEntities())
            {
                var valfav = ctx.OnLineFavoritos.FirstOrDefault(x => x.id == idFavorito);
                if (valfav == null)
                    return false;
                valfav.alias = alias;
                ctx.Entry(valfav).State = EntityState.Modified;
                ctx.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [WebMethod]
        public List<Favorito> GetFavoritos(int id)
        {
            //MLS idUserSecure
            var idUser = new IdSecureComp().GetIdUserValid(id);
            if (idUser == 0)
            {
                return new List<Favorito>();
            }
            id = idUser;

            return new MySqlDataAccess().GetFavoritos(id);
        }

        #endregion

        #region RecuperarContrasenia

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="origen"></param>
        /// <returns></returns>
        [WebMethod]
        public Boolean RecuperarContraseniaConOrigen(int idUsuario, String origen)
        {
            //MLS idUserSecure
            broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
            try
            {
                UsuariosOnlineBroxel uo =
                    broxelcoRdgEntities.UsuariosOnlineBroxel.FirstOrDefault(x => x.Id == idUsuario) ??
                    broxelcoRdgEntities.UsuariosOnlineBroxel.FirstOrDefault(
                        x => x.Id == new IdSecureComp().GetIdUserValid(idUsuario));
                if (uo != null)
                {
                    string temppass = Regex.Replace(System.Web.Security.Membership.GeneratePassword(10, 0),
                        @"[^a-zA-Z0-9]", m => "9");
                    uo.Password = Helper.Cifrar(temppass);
                    broxelcoRdgEntities.SaveChanges();
                    Mailing.EnviaCorreoOlvidoContrasenia(uo.NombreCompleto, uo.Usuario, temppass, uo.CorreoElectronico,
                        origen);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="host"></param>
        /// <returns></returns>
        [WebMethod]
        public Boolean RecuperarContrasenia(int idUsuario, String host)
        {
            //MLS idUserSecure
            var idUser = new IdSecureComp().GetIdUserValid(idUsuario);
            if (idUser == 0)
            {
                return false;
            }
            idUsuario = idUser;

            broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
            try
            {
                UsuariosOnlineBroxel uo = broxelcoRdgEntities.UsuariosOnlineBroxel.FirstOrDefault(x => x.Id == idUsuario);
                if (uo != null)
                {
                    string temppass = Regex.Replace(System.Web.Security.Membership.GeneratePassword(10, 0),
                        @"[^a-zA-Z0-9]", m => "9");
                    uo.Password = Helper.Cifrar(temppass);
                    broxelcoRdgEntities.SaveChanges();
                    Mailing.EnviaCorreoOlvidoContrasenia(uo.NombreCompleto, uo.Usuario, temppass, uo.CorreoElectronico,
                        host);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numTarjeta"></param>
        /// <param name="fechaExpira"></param>
        /// <param name="correoElectronico"></param>
        /// <param name="host"></param>
        /// <returns></returns>
        [WebMethod]
        public Boolean RecuperarContraseniaConMail(String numTarjeta, String fechaExpira, String correoElectronico,
            String host)
        {
            try
            {
                var Usuario = GetUserByMail(correoElectronico.ToLower());
                if (Usuario != null)
                {
                    return RecuperarContraseniaConOrigen(Usuario.Id, host);

                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion

        #endregion

        #region Clientes

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="pNombre"></param>
        /// <param name="sNombre"></param>
        /// <param name="aPaterno"></param>
        /// <param name="aMaterno"></param>
        /// <param name="rfc"></param>
        /// <param name="calle"></param>
        /// <param name="sexo"></param>
        /// <param name="numeroExt"></param>
        /// <param name="numeroInt"></param>
        /// <param name="codigoPostal"></param>
        /// <param name="colonia"></param>
        /// <param name="delegacionMunicipio"></param>
        /// <param name="ciudad"></param>
        /// <param name="estado"></param>
        /// <param name="usuario"></param>
        /// <param name="celular"></param>
        /// <param name="telefono"></param>
        /// <param name="fechaNacimiento"></param>
        /// <param name="contrasenia"></param>
        /// <param name="noEmpleado"></param>
        /// <param name="idApp"></param>
        /// <param name="enviaSeedRegistro"></param>
        /// <returns></returns>
        [WebMethod]
        public UsuarioOnlineResponse RegistraClienteSinTarjetaLog(int idUser, string pNombre, string sNombre,
            string aPaterno, string aMaterno, string rfc, string calle, string sexo, string numeroExt, string numeroInt,
            string codigoPostal, string colonia, string delegacionMunicipio,
            string ciudad, string estado, string usuario, string celular, string telefono, string fechaNacimiento,
            string contrasenia, string noEmpleado, int idApp, bool enviaSeedRegistro)
        {
            UsuarioOnlineResponse respuesta = new UsuarioOnlineResponse();
            //MLS idUserSecure
            var id = new IdSecureComp().GetIdUserValid(idUser);
            if (id == 0)
            {
                respuesta.Success = false;
                respuesta.UserResponse = "Usuario no valido.";

            }
            var originalIdUser = idUser;
            idUser = id;

            try
            {
                var vcBl = new VCardBL();

                using (var ctx = new broxelco_rdgEntities())
                {
                    var data =
                        ctx.CreaClienteSinTarjetaLog.FirstOrDefault(
                            x => x.idUsuarioOnlineBroxel == idUser);
                    if (data != null)
                    {
                        if (data.tieneCuenta == true)
                        {
                            respuesta.Success = false;
                            respuesta.UserResponse = "Usuario ya tiene cuenta asignada";
                        }
                        else
                        {
                            respuesta.Success = true;
                        }
                    }
                    else
                    {
                        var idLog = vcBl.GeneraCreaClienteLogBroxel(pNombre, sNombre, aPaterno, aMaterno, rfc, sexo,
                            calle, numeroExt,
                            numeroInt, codigoPostal, colonia, delegacionMunicipio, ciudad, estado, usuario, celular,
                            telefono,
                            fechaNacimiento, contrasenia, noEmpleado, idApp, idUser, enviaSeedRegistro);
                        if (idLog > 0)
                        {
                            respuesta.Success = true;
                        }
                        else
                        {
                            respuesta.Success = false;
                            respuesta.UserResponse = "Ocurrio un error al insertar en la tabla creaclientelog";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.Success = false;
                respuesta.UserResponse = ex.Message;
            }

            return respuesta;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public UsuarioOnlineResponse AgregarClienteNoActualizado()
        {
            //MLS idUserSecure
            return AgregarClienteNoActualizadoBL();
        }

        #endregion

        #region Tarjetas

        #region Agregar Tarjeta

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="numTarjeta"></param>
        /// <param name="fechaExpiracion"></param>
        /// <param name="alias"></param>
        /// <param name="identifier"></param>
        /// <param name="host"></param>
        /// <returns></returns>
        [WebMethod]
        public UsuarioOnlineResponse AgregarTarjeta(int id, String numTarjeta, String fechaExpiracion, string alias = "",
            string identifier = "", string host = "")
        {
            //MLS idUserSecure
            var idUser = new IdSecureComp().GetIdUserValid(id);
            if (idUser == 0)
            {
                return new UsuarioOnlineResponse
                {
                    Success = false,
                    UserResponse = "Usuario inválido o su sesión expiró."
                };
            }
            id = idUser;
            return AgregarTarjetaBL(id, numTarjeta, fechaExpiracion, idUser, alias, identifier, host);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="numTarjeta"></param>
        /// <param name="fechaExpiracion"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        [WebMethod]
        public UsuarioOnlineResponse AgregarTarjetaMYO(int id, string numTarjeta, string fechaExpiracion,
            string alias = "")
        {
            var idUser = new IdSecureComp().GetIdUserValid(id);
            if (idUser == 0)
            {
                return new UsuarioOnlineResponse
                {
                    Success = false,
                    UserResponse = "Usuario invalido o sesion expiro."
                };
            }

            id = idUser;

            return AgregarTarjetaMYOBL(id, numTarjeta, fechaExpiracion, idUser, alias);
        }

        #endregion

        #region Consultar Tarjeta

        /// <summary>
        /// Obtiene los datos de una tarjeta mediante un idSecure
        /// </summary>
        /// <param name="idSecure">Id de Seguridad.</param>
        /// <param name="numCuenta">Numero de cuenta de la tarjeta.</param>
        /// <returns>Tarjeta: objeto con los datos de la tarjeta.</returns>
        [WebMethod]
        public Tarjeta GetDatosTarjeta(int idSecure, string numCuenta)
        {
            //MLS idUserSecure
            var idUser = new IdSecureComp().GetIdUserValid(idSecure);
            if (idUser == 0) return new Tarjeta();
            using (var ctx = new broxelco_rdgEntities())
            {
                var accesso =
                    ctx.accessos_clientes.FirstOrDefault(a => a.IdUsuarioOnlineBroxel == idUser && a.cuenta == numCuenta);

                var family = new BroxelFamily();
                bool esHija = family.EsHijoDeLaCuentaPadreFamily(idUser, numCuenta);

                var maq = ctx.maquila.FirstOrDefault(y => y.num_cuenta == numCuenta);
                if ((accesso == null && !esHija) || maq == null)
                    return new Tarjeta();
            }
            return Helper.GetTarjetaFromCuenta(numCuenta) ?? new Tarjeta();
        }

        /// <summary>
        /// Obtiene los datos de una tarjeta mediante un idSecure  y valida si la tarjeta tiene una tarjeta adicional.
        /// </summary>
        /// <param name="idSecure">Id de Seguridad.</param>
        /// <param name="numCuenta">Numero de cuenta de la tarjeta.</param>
        /// <returns>Tarjeta: objeto con los datos de la tarjeta.</returns>
        [WebMethod]
        public Tarjeta GetDatosTarjetaConAdicional(int idSecure, string numCuenta)
        {
            //MLS idUserSecure
            var idUser = new IdSecureComp().GetIdUserValid(idSecure);
            if (idUser == 0) return new Tarjeta();

            var tieneAdicional = false;
            using (var ctx = new broxelco_rdgEntities())
            {
                var family = new BroxelFamily();
                bool esHija = family.EsHijoDeLaCuentaPadreFamily(idUser, numCuenta);
                var accesso =
                    ctx.accessos_clientes.FirstOrDefault(a => a.IdUsuarioOnlineBroxel == idUser && a.cuenta == numCuenta);
                var maq = ctx.maquila.FirstOrDefault(y => y.num_cuenta == numCuenta);
                if ((accesso == null && !esHija) || maq == null)
                    return new Tarjeta();

                var tarjetaAdicional = ctx.TarjetasFisicasAdicionales.FirstOrDefault(ta => ta.NumCuenta == numCuenta);
                if (tarjetaAdicional != null)
                {
                    tieneAdicional = true;
                }
            }

            Tarjeta tarjetaRetorno;
            if (tieneAdicional)
            {
                tarjetaRetorno = Helper.GetTarjetaFromCuentaAdicional(numCuenta) ?? new Tarjeta();
            }
            else
            {
                tarjetaRetorno = Helper.GetTarjetaFromCuenta(numCuenta) ?? new Tarjeta();
            }

            return tarjetaRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="tarjeta"></param>
        /// <param name="cuenta"></param>
        /// <returns></returns>
        [WebMethod]
        public OperarTarjetaResponse ConsultaEstadoTarjeta(int idUser, string tarjeta, string cuenta = "")
        {
            //MLS idUserSecure
            var id = new IdSecureComp().GetIdUserValid(idUser);
            if (id == 0)
            {
                return new OperarTarjetaResponse {Success = 0, UserResponse = "Usuario inválido o su sesión expiró."};
            }
            idUser = id;
            return new TcControlBL().ConsultaEstado(idUser, tarjeta, cuenta);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tarjeta"></param>
        /// <returns></returns>
        [WebMethod]
        public OperarTarjetaResponse ConsultaEstadoTarjetaInter(string tarjeta)
        {
            return new TcControlBL().ConsultaEstado(-500, tarjeta, "");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="idApp"></param>
        /// <param name="codigoProducto"></param>
        /// <returns></returns>
        [WebMethod]
        public UsuarioOnlineResponse ObtenTarjetaVirtual(int idUser, int idApp, string codigoProducto)
        {
            //  MLS idUserSecure
            var id = new IdSecureComp().GetIdUserValid(idUser);
            if (id == 0)
            {
                return new UsuarioOnlineResponse
                {
                    Success = false,
                    UserResponse = "El usuario es inválido o su sesión expiró, favor de reingresar."
                };

            }
            var originalIdUser = idUser;
            idUser = id;

            try
            {
                var vcBl = new VCardBL();
                //// verifica si debe de crear cliente para asignar tarjeta virtual
                var verificarCuentasExistentes = vcBl.verificaCuentasExistentes(codigoProducto);

                if (verificarCuentasExistentes)
                {

                    var verificador = vcBl.VerificaCreaCliente(idApp);

                    Trace.WriteLine("Verificador es verdadero o false " + verificador);

                    if (verificador)
                    {
                        Trace.WriteLine("El verificador es verdadero " + verificador);
                        //Obtiene tarjeta virtual del pull
                        var infoMaquila = vcBl.GetMaquilaFromPull(idApp, codigoProducto);

                        Trace.WriteLine("Obteniendo numero de cuenta: " + infoMaquila.NumCuenta);

                        if (infoMaquila == null)
                            return new UsuarioOnlineResponse
                            {
                                Success = false,
                                UserResponse =
                                    "Por el momento no existen tarjetas disponibles, por favor, reintente más tarde"
                            };
                        // Crea Cliente en clientes Broxel y le asocia la tarjeta virtual.
                        var respuesta = vcBl.CreaClientes(idUser, idApp, infoMaquila, codigoProducto.Trim(),
                            originalIdUser);

                        if (!respuesta.Success) return respuesta;

                        Helper.GetTarjetaFromCuenta(infoMaquila.NumCuenta);
                        var resAct = vcBl.ActivarCuentaTarjetaVirtual(infoMaquila.NumCuenta);
                        respuesta.UserResponse = infoMaquila.NumCuenta;

                        if (resAct)
                        {
                            return respuesta;
                        }
                        return new UsuarioOnlineResponse
                        {
                            Success = true,
                            UserResponse = infoMaquila.NumCuenta
                        };
                    }
                    // verifica si el usuario tiene una cuenta activa
                    Trace.WriteLine("El verificador es verdadero " + verificador);

                    // verificar que el usuario exista en la tabla CreaClienteSinTarjetaLog

                    var verificadorCliente = vcBl.verificarClienteLog(idUser);

                    if (verificadorCliente)
                    {
                        return new UsuarioOnlineResponse
                        {
                            Success = true,
                            UserResponse = "Ya existe un usuario pre-registrado",
                        };
                    }
                    else
                    {
                        var respuesta = new UsuarioOnlineResponse();
                        var myoEntities = new MYOEntities();
                        var broxelEntities = new broxelco_rdgEntities();

                        var userMyo = myoEntities.Users.SingleOrDefault(x => x.BroxelUserId == idUser);
                        var userBroxel = broxelEntities.UsuariosOnlineBroxel.SingleOrDefault(x => x.Id == idUser);

                        if (userBroxel == null)
                        {
                            return new UsuarioOnlineResponse
                            {
                                Success = false,
                                UserResponse = "El usuario no existe en base de datos Broxel. Favor de verificar"
                            };
                        }

                        var idLog = vcBl.GeneraCreaClienteLogBroxel(userMyo.Name, userMyo.SecondName,
                            userMyo.LastNameFather, userMyo.LastNameMother, "", userMyo.Gender,
                            userMyo.Domicile, userMyo.NumExt, userMyo.NumInt, userMyo.PostalCode, userMyo.Colony,
                            userMyo.DelTown,
                            userMyo.City, userMyo.Estate, userBroxel.Usuario, userMyo.PhoneNumber, userMyo.PhoneNumber,
                            userMyo.BirthDate.ToShortDateString(),
                            userBroxel.Password, "", idApp, idUser, true);

                        if (idLog > 0)
                        {
                            respuesta.Success = true;
                        }
                        else
                        {
                            respuesta.Success = false;
                            respuesta.UserResponse = "Ocurrio un error al insertar en la tabla creaclientelog";
                            return respuesta;
                        }

                    }

                    //////// Verifica que haya una cuenta activa

                    var verificadorCuenta = vcBl.VerificaCuentaActiva(idUser);

                    Trace.WriteLine("Verificando si el usuario tiene una cuenta AON activa: " + verificadorCuenta);

                    if (!verificadorCuenta)
                        return new UsuarioOnlineResponse
                        {
                            Success = false,
                            UserResponse =
                                "Por el momento no existen tarjetas disponibles, por favor, reintentar más tarde"
                        };
                    Trace.WriteLine("El usuario ya tiene una cuenta activa: " + verificadorCuenta);
                    return new UsuarioOnlineResponse
                    {
                        Success = true,
                        UserResponse = "El usuario ya tiene una cuenta activa"
                    };
                }
                return new UsuarioOnlineResponse
                {
                    Success = false,
                    UserResponse = "Por el momento no existen tarjetas disponibles, por favor, reintentar más tarde"
                };
            }
            catch (Exception e)
            {
                return new UsuarioOnlineResponse
                {
                    Success = false,
                    UserResponse = "No fue posible obtener la tarjeta temporal: " + e.Message
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="idApp"></param>
        /// <param name="codigoProducto"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [WebMethod]
        public UsuarioOnlineResponse ObtenTarjetaVirtualMYO(int idUser, int idApp, string codigoProducto,
            RegistraClienteSinTarjetaModel model)
        {
            //MLS idUserSecure
            var id = new IdSecureComp().GetIdUserValid(idUser);
            if (id == 0)
            {
                return new UsuarioOnlineResponse
                {
                    Success = false,
                    UserResponse = "El usuario es inválido o su sesión expiró, favor de reingresar."
                };

            }
            var originalIdUser = idUser;
            idUser = id;

            try
            {
                var vcBl = new VCardBL();

                var infoMaquila = vcBl.GetMaquilaAonFromPull(idApp, codigoProducto);
                if (infoMaquila == null)
                    return new UsuarioOnlineResponse
                    {
                        Success = false,
                        UserResponse = "Por el momento no existen tarjetas disponibles, por favor, reintentar más tarde"
                    };

                var response = RegistraClienteSinTarjetaLog(model.idUser, model.pNombre, model.sNombre, model.aPaterno,
                    model.aMaterno, model.rfc, model.calle, model.sexo, model.numeroExt, model.numeroInt,
                    model.codigoPostal, model.colonia, model.delegacionMunicipio, model.ciudad, model.estado,
                    model.usuario, model.celular, model.telefono, model.fechaNacimiento, model.contrasenia,
                    model.noEmpleado, model.idApp, model.enviaSeedRegistro);

                if (!response.Success)
                    return new UsuarioOnlineResponse
                    {
                        Success = false,
                        UserResponse = response.UserResponse
                    };
                //crea cliente broxel
                var resp = vcBl.CreaClientes(idUser, idApp, infoMaquila, codigoProducto.Trim(), originalIdUser);

                if (!resp.Success) return resp;
                Helper.GetTarjetaFromCuenta(infoMaquila.NumCuenta);
                var resAct = vcBl.ActivarCuentaTarjetaVirtual(infoMaquila.NumCuenta);

                resp.UserResponse = infoMaquila.NumCuenta;

                if (resAct)
                {
                    return resp;
                }
                return new UsuarioOnlineResponse
                {
                    Success = true,
                    UserResponse = infoMaquila.NumCuenta
                };
            }
            catch (Exception e)
            {
                return new UsuarioOnlineResponse
                {
                    Success = false,
                    UserResponse = "No fue posible obtener la tarjeta temporal: " + e.Message
                };
            }
        }

        /// <summary>
        /// Obtiene datos de la tarjeta virtual
        /// </summary>
        /// <param name="idApp">Id de la aplicación solicitante</param>
        /// <param name="usuario">Id del usuario solicitante</param>
        /// <param name="token">Token del usuario solicitante</param>
        /// <returns></returns>
        [WebMethod]
        public VCardData GetTarjetaVirtual(int idApp, string usuario, string token)
        {
            return new VCardBL().GetVCardData(idApp, usuario, token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="numCuenta"></param>
        /// <returns></returns>
        [WebMethod]
        public VCardData GetDatosTxCard(int idUser, string numCuenta)
        {
            //MLS idUserSecure
            var id = new IdSecureComp().GetIdUserValid(idUser);
            if (id == 0)
            {
                return new VCardData {IdTran = 0, Track1 = "", Track2 = ""};
            }
            idUser = id;

            return new VCardBL().GetCardData(idUser, numCuenta);
        }

        #endregion

        #region Registrar Tarjeta

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="idApp"></param>
        /// <param name="tarjeta"></param>
        /// <param name="fechaVencimiento"></param>
        /// <returns></returns>
        [WebMethod]
        public UsuarioOnlineResponse RegistraTarjetaFisica(int idUser, int idApp, string tarjeta,
            string fechaVencimiento)
        {
            //MLS idUserSecure
            var id = new IdSecureComp().GetIdUserValid(idUser);
            if (id == 0)
            {
                return new UsuarioOnlineResponse
                {
                    Success = false,
                    UserResponse = "El usuario es inválido o su sesión expiró, favor de reingresar."
                };

            }
            var originalIdUser = idUser;
            idUser = id;
            try
            {
                var maq = GetMaquilaPorTarjetaYFecha(tarjeta, fechaVencimiento);
                if (maq == null)
                {
                    return new UsuarioOnlineResponse
                    {
                        Success = false,
                        UserResponse = "El usuario es inválido o su sesión expiró, favor de reingresar."
                    };
                }
                // TODO Implementar logica para validar la tarjeta a asociar.
                var vcBl = new VCardBL();
                var infoMaquila = new MaquilaVcInfo {IdMaquila = maq.id, NumCuenta = maq.num_cuenta};
                return vcBl.CreaClientes(idUser, idApp, infoMaquila, maq.producto, originalIdUser, true);
            }
            catch (Exception e)
            {
                return new UsuarioOnlineResponse
                {
                    Success = false,
                    UserResponse = "No fue posible asociar la tarjeta con el usuario. " + e.Message
                };
            }
        }

        #endregion

        #region Activar Tarjeta

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="tarjeta"></param>
        /// <param name="cuenta"></param>
        /// <returns></returns>
        [WebMethod]
        public OperarTarjetaResponse ActivarTarjeta(int idUser, string tarjeta, string cuenta = "")
        {
            //MLS idUserSecure
            var id = new IdSecureComp().GetIdUserValid(idUser);
            if (id == 0)
            {
                return new OperarTarjetaResponse {Success = 0, UserResponse = "Usuario inválido o su sesión expiró."};
            }
            idUser = id;
            return new TcControlBL().CambiarEstado(idUser, tarjeta, cuenta, EstadosTarjetas.Operativa);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="idApp"></param>
        /// <param name="tarjeta"></param>
        /// <param name="fechaVencimiento"></param>
        /// <returns></returns>
        [WebMethod]
        public UsuarioOnlineResponse ActivarTarjetaFisica(int idUser, int idApp, string tarjeta, string fechaVencimiento)
        {
            // TODO MLS Implementar lógica para activacion de tarjeta fisica.
            return new UsuarioOnlineResponse
            {
                Success = true,
                UserResponse = "Tarjeta física activada de forma correcta."
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tarjeta"></param>
        /// <returns></returns>
        [WebMethod]
        public OperarTarjetaResponse ActivarTarjetaInter(string tarjeta)
        {
            return new TcControlBL().CambiarEstado(-500, tarjeta, "", EstadosTarjetas.Operativa);
        }

        #endregion

        #region Bloquear Tarjeta

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="tarjeta"></param>
        /// <param name="cuenta"></param>
        /// <returns></returns>
        [WebMethod]
        public OperarTarjetaResponse BloquearTarjeta(int idUser, string tarjeta, string cuenta = "")
        {
            //MLS idUserSecure
            var id = new IdSecureComp().GetIdUserValid(idUser);
            if (id == 0)
            {
                return new OperarTarjetaResponse {Success = 0, UserResponse = "Usuario inválido o su sesión expiró."};
            }
            idUser = id;
            return new TcControlBL().CambiarEstado(idUser, tarjeta, cuenta, EstadosTarjetas.BolBloqueoTemporal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tarjeta"></param>
        /// <returns></returns>
        [WebMethod]
        public OperarTarjetaResponse BloquearTarjetaInter(string tarjeta)
        {
            return new TcControlBL().CambiarEstado(-500, tarjeta, "", EstadosTarjetas.BolBloqueoTemporal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tarjeta"></param>
        /// <returns></returns>
        [WebMethod]
        public OperarTarjetaResponse BloquearTarjetaInterOtro(string tarjeta)
        {
            return new TcControlBL().CambiarEstado(-500, tarjeta, "", EstadosTarjetas.BolOtros);
        }

        #endregion

        #region Reportar Tarjeta

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="tarjeta"></param>
        /// <param name="cuenta"></param>
        /// <returns></returns>
        [WebMethod]
        public OperarTarjetaResponse ReportarRoboTarjeta(int idUser, string tarjeta, string cuenta = "")
        {
            //MLS idUserSecure
            var id = new IdSecureComp().GetIdUserValid(idUser);
            if (id == 0)
            {
                return new OperarTarjetaResponse {Success = 0, UserResponse = "Usuario inválido o su sesión expiró."};
            }
            idUser = id;
            return new TcControlBL().CambiarEstado(idUser, tarjeta, cuenta, EstadosTarjetas.BolRobo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tarjeta"></param>
        /// <returns></returns>
        [WebMethod]
        public OperarTarjetaResponse ReportaXRoboTarjetaInter(string tarjeta)
        {
            return new TcControlBL().CambiarEstado(-500, tarjeta, "", EstadosTarjetas.BolRobo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tarjeta"></param>
        /// <returns></returns>
        [WebMethod]
        public OperarTarjetaResponse ReportaXExtravioTarjetaInter(string tarjeta)
        {
            return new TcControlBL().CambiarEstado(-500, tarjeta, "", EstadosTarjetas.BolExtravio);
        }

        #endregion

        #region Cambiar NIP

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nombreTarjeta"></param>
        /// <param name="numeroTarjeta"></param>
        /// <param name="fechaExpira"></param>
        /// <param name="cvc2"></param>
        /// <param name="nipNuevo"></param>
        /// <param name="idUsuario"></param>
        /// <param name="sitio"></param>
        /// <returns></returns>
        [WebMethod]
        public NIPResponse CambiarNip(String nombreTarjeta, String numeroTarjeta, String fechaExpira, String cvc2,
            String nipNuevo, int idUsuario, String sitio = "online.broxel.com")
        {
            //MLS idUserSecure
            var idUser = new IdSecureComp().GetIdUserValid(idUsuario);
            if (idUser == 0)
            {
                return new NIPResponse
                {
                    UserResponse = "Usuario inválido o su sesión expiró."
                };
            }
            idUsuario = idUser;
            broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();

            try
            {
                var maq = GetMaquilaPorTarjetaYFecha(numeroTarjeta, fechaExpira);
                if (maq != null)
                {
                    var valida = new MySqlDataAccess();
                    if (valida.ValidaProductoMejoravit(maq.num_cuenta))
                    {
                        return new NIPResponse
                        {
                            UserResponse = "No puedes cambiar el NIP"
                        };
                    }

                    var detalleClienteBroxel =
                        Entities.Instance.DetalleClientesBroxel.FirstOrDefault(
                            x => x.ClaveCliente == maq.clave_cliente && x.Producto == maq.producto);
                    var producto = Entities.Instance.ProductosBroxel.FirstOrDefault(x => x.codigo == maq.producto);

                    if (((detalleClienteBroxel.CambioDeNip == 1) ||
                         ((detalleClienteBroxel.CambioDeNip == 2) && (producto.CambioDeNip == 1))))
                    {
                        var accesoCliente =
                            broxelcoRdgEntities.accessos_clientes.FirstOrDefault(x => x.IdMaquila == maq.id);

                        if (accesoCliente != null && accesoCliente.IdUsuarioOnlineBroxel == idUsuario)
                        {
                            Trace.WriteLine("IdSecure recibido en CambiarNip: " + idUsuario);
                            Trace.WriteLine("idUsuario obtenido: " + idUser);
                            var res = _bs.CambiarNip(nombreTarjeta, numeroTarjeta, fechaExpira, cvc2, nipNuevo, idUser,
                                sitio);
                            BitacoraUsuariosOnlineBroxel("CambioDeNip",
                                accesoCliente.cuenta + " - " + res.CodigoRespuesta,
                                idUsuario.ToString(CultureInfo.InvariantCulture), sitio);
                            return res;
                        }
                        return new NIPResponse {UserResponse = "Esta tarjeta no pertecene a su cuenta."};
                    }
                    return new NIPResponse {UserResponse = "Esta producto no acepta cambios de NIP."};
                }
                return new NIPResponse {UserResponse = "No se encontraron datos de esta tarjeta"};
            }
            catch (Exception)
            {
                return new NIPResponse {UserResponse = "Error al procesar la transacción"};
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numCuenta"></param>
        /// <param name="terminacion"></param>
        /// <param name="cvc2"></param>
        /// <param name="nipNuevo"></param>
        /// <param name="idUsuario"></param>
        /// <param name="sitio"></param>
        /// <returns></returns>
        [WebMethod]
        public NIPResponse CambiarNipMobile(String numCuenta, String terminacion, String cvc2, String nipNuevo,
            int idUsuario, String sitio = "wsMobile.broxel.com")
        {
            //MLS idUserSecure
            var idUser = new IdSecureComp().GetIdUserValid(idUsuario);
            if (idUser == 0)
            {
                return new NIPResponse
                {
                    UserResponse = "Usuario inválido o su sesión expiró"
                };
            }
            idUsuario = idUser;

            var valida = new MySqlDataAccess();
            if (valida.ValidaProductoMejoravit(numCuenta))
            {
                return new NIPResponse
                {
                    UserResponse = "No puedes cambiar el NIP"
                };
            }

            broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();

            try
            {
                var tfa = broxelcoRdgEntities.TarjetasFisicasAdicionales.FirstOrDefault(s => s.NumCuenta == numCuenta);
                Tarjeta t = tfa!= null ? Helper.GetTarjetaFromCuentaAdicional(numCuenta) : Helper.GetTarjetaFromCuenta(numCuenta);
                if (t == null)
                    return new NIPResponse {UserResponse = "No se encontró tarjeta."};
                if (t.NumeroTarjeta != null && t.NumeroTarjeta.Substring(12) != terminacion)
                    return new NIPResponse {UserResponse = "No se encontró tarjeta."};
                var maq = GetMaquila(numCuenta);
                if (t.Cvc2 == cvc2)
                {
                    var detalleClienteBroxel =
                        Entities.Instance.DetalleClientesBroxel.FirstOrDefault(
                            x => x.ClaveCliente == maq.clave_cliente && x.Producto == maq.producto);
                    if (detalleClienteBroxel == null)
                        return new NIPResponse
                        {
                            UserResponse =
                                "Error al procesar la transacción. La tarjeta no esta asociada a un programa válido"
                        };
                    var producto = Entities.Instance.ProductosBroxel.FirstOrDefault(x => x.codigo == maq.producto);
                    if (producto == null)
                        return new NIPResponse
                        {
                            UserResponse =
                                "Error al procesar la transacción. La tarjeta no esta asociada a un producto válido"
                        };

                    if (((detalleClienteBroxel.CambioDeNip == 1) ||
                         ((detalleClienteBroxel.CambioDeNip == 2) && (producto.CambioDeNip == 1))))
                    {
                        var accesoCliente =
                            broxelcoRdgEntities.accessos_clientes.FirstOrDefault(x => x.IdMaquila == maq.id);
                        if (accesoCliente != null && accesoCliente.IdUsuarioOnlineBroxel == idUsuario)
                        {
                            Trace.WriteLine("IdSecure recibido en CambiarNip: " + idUsuario);
                            Trace.WriteLine("idUsuario obtenido: " + idUser);
                            var res = _bs.CambiarNip(t.NombreTarjeta, t.NumeroTarjeta, t.FechaExpira, cvc2, nipNuevo,
                                idUser, sitio);
                            BitacoraUsuariosOnlineBroxel("CambioDeNip",
                                accesoCliente.cuenta + " - " + res.CodigoRespuesta,
                                idUsuario.ToString(CultureInfo.InvariantCulture), sitio);
                            return res;
                        }
                        return new NIPResponse {UserResponse = "Esta tarjeta no pertenece a su cuenta."};
                    }
                    return new NIPResponse {UserResponse = "Esta producto no acepta cambios de NIP."};
                }
                return new NIPResponse {UserResponse = "Datos de tarjeta erróneos."};
            }
            catch (Exception)
            {
                return new NIPResponse {UserResponse = "Error al procesar la transacción."};
            }
        }

        #endregion

        #region Eliminar - Cancelar Tarjeta

        /// <summary>
        /// Nuevo método para eliminación de tarjetas
        /// </summary>
        /// <param name="id">Id de usuario Broxel Online</param>
        /// <param name="numTarjeta">Tarjeta a eliminar</param>
        /// <returns>Respuesta Usuario Online</returns>
        [WebMethod]
        public UsuarioOnlineResponse EliminarTarjeta(int id, String numCuenta, string host = "")
        {
            //MLS idUserSecure
            var idUser = new IdSecureComp().GetIdUserValid(id);
            if (idUser == 0)
            {
                return new UsuarioOnlineResponse
                {
                    Success = false,
                    UserResponse = "Usuario inválido o su sesión expiró."
                };
            }
            id = idUser;

            broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
            var usuario = broxelcoRdgEntities.UsuariosOnlineBroxel.FirstOrDefault(x => x.Id == id);
            if (usuario != null)
            {
                var cuentas =
                    broxelcoRdgEntities.accessos_clientes.Where(
                        x => x.IdUsuarioOnlineBroxel == id && x.IdMaquila != null);
                if (cuentas.Count() == 1)
                    return new UsuarioOnlineResponse
                    {
                        Success = false,
                        UserResponse = "No es posible eliminar la tarjeta, ya que es la única asociada al usuario."
                    };
                var maq = broxelcoRdgEntities.maquila.FirstOrDefault(x => x.num_cuenta == numCuenta);
                if (maq == null)
                    return new UsuarioOnlineResponse {Success = false, UserResponse = "No se encontró la cuenta."};
                var accesoCliente =
                    broxelcoRdgEntities.accessos_clientes.FirstOrDefault(
                        x => x.IdMaquila == maq.id && x.IdUsuarioOnlineBroxel == id);
                if (accesoCliente == null)
                    return new UsuarioOnlineResponse
                    {
                        Success = false,
                        UserResponse = "Esta tarjeta actualmente no se encuentra ligada a esta cuenta."
                    };
                try
                {
                    EliminaRelacion(maq.id);
                    broxelcoRdgEntities.SaveChanges();

                    try
                    {
                        if (!(String.IsNullOrEmpty(host)) &&
                            ConfigurationManager.AppSettings["EnviarCorreoAviso"] == "1")
                        {
                            //Mailing.EnviaCorreoAvisoMovimiento("Tarjeta Eliminada", 0, "Tarjeta", maq.nro_tarjeta, usuario.NombreCompleto, host, usuario.CorreoElectronico);
                        }
                    }
                    catch (Exception ex)
                    {
                        Helper.SendMail("broxelonline@broxel.com", "luis.huerta@broxel.com",
                            "EnviaCorreoAvisoMovimiento",
                            "Se intento enviar el correo de notificación de que se dio de baja una tarjeta  con la excpeción " +
                            ex, "67896789");
                    }
                    return new UsuarioOnlineResponse
                    {
                        Fecha = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        Success = true,
                        UserResponse = "Transacción exitosa"
                    };
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine(
                            "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                }
                catch (Exception)
                {
                    return new UsuarioOnlineResponse
                    {
                        Success = false,
                        UserResponse = "Ocurrió un error al eliminar la tarjeta. Intente nuevamente."
                    };
                }
            }
            try
            {
                BitacoraUsuariosOnlineBroxel("EliminarTarjeta", numCuenta,
                    (usuario != null ? usuario.Usuario : id.ToString(CultureInfo.InvariantCulture)), "");
            }
            catch
            {
            }
            return new UsuarioOnlineResponse();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tarjeta"></param>
        /// <returns></returns>
        [WebMethod]
        public OperarTarjetaResponse CancelarTarjetaInter(string tarjeta)
        {
            return new TcControlBL().CambiarEstado(-500, tarjeta, "", EstadosTarjetas.BolCancelada);
        }

        #endregion

        #endregion

        #region Movimientos

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numCuenta"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <param name="pagina"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [WebMethod]
        public MovimientoOnlineResponse GetMovimientoOnlineBroxel(String numCuenta, DateTime fechaInicio,
            DateTime fechaFin, int pagina, string email)
        {
            _bs = new BroxelService();
            return _bs.GetMovimientoOnlineBroxel(numCuenta, fechaInicio, fechaFin, pagina, email);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numCuenta"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <param name="pagina"></param>
        /// <param name="email"></param>
        /// <param name="tipo"></param>
        /// <returns></returns>
        [WebMethod]
        public MovimientoOnlineResponse GetMovimientoOnlineBroxelPorTipo(String numCuenta, DateTime fechaInicio,
            DateTime fechaFin, int pagina, string email, int tipo)
        {
            _bs = new BroxelService();
            return _bs.GetMovimientoOnlineBroxelPorTipo(numCuenta, fechaInicio, fechaFin, pagina, email, tipo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numCuenta"></param>
        /// <param name="fechaDesde"></param>
        /// <param name="fechaHasta"></param>
        /// <returns></returns>
        [WebMethod]
        public MovimientoOnlineResponse GetMovimientosConRango(String numCuenta, DateTime fechaDesde,
            DateTime fechaHasta)
        {
            return _bs.GetMovimientosConRango(numCuenta, fechaDesde, fechaHasta);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numCuenta"></param>
        /// <param name="usuarioOnline"></param>
        /// <returns></returns>
        [WebMethod]
        public MovimientoOnlineResponse Get5UltimosMovimientos(String numCuenta, String usuarioOnline)
        {
            MovimientoOnlineResponse movsARegresar = new MovimientoOnlineResponse();
            MovimientosResponse mr = _bs.GetUltimaPagMovimientosCuenta(numCuenta, usuarioOnline);

            if (mr.Success == 1)
            {
                movsARegresar.Success = true;
                movsARegresar.Fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                foreach (var movimiento in mr.Movimientos.Where(x => x.Aprobada))
                {
                    movsARegresar.Movimientos.Add(new MovimientoOnline
                    {
                        Cargo = Convert.ToDecimal(movimiento.Monto),
                        Descripcion = movimiento.Comercio,
                        Fecha = DateTime.Parse(movimiento.Fecha),
                        NumTarjeta = movimiento.Tarjeta,
                    });
                }
            }
            return movsARegresar;
        }

        #endregion

        #region Maquila

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numTarjeta"></param>
        /// <param name="fechaVencimiento"></param>
        /// <returns></returns>
        [WebMethod]
        public vw_maquila GetMaquilaPorTarjetaYFecha(String numTarjeta, String fechaVencimiento)
        {
            if (numTarjeta.Length != 16)
            {
                return null;
            }
            broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
            String queryToDb = "select m.* from vw_maquila m "
                               + "join registri_broxel r "
                               + "on r.nruco=m.num_cuenta "
                               + "and left(m.nro_tarjeta,6)='" + numTarjeta.Substring(0, 6) + "' "
                               + "and right(m.nro_tarjeta,4)='" + numTarjeta.Substring(12, 4) + "' "
                               + "and right(r.folio_de_registro,6)='" + numTarjeta.Substring(6, 6) + "' "
                               + "and replace(right(r.fecha_de_registro,5),'-','')='" + fechaVencimiento + "'";

            return broxelcoRdgEntities.vw_maquila.SqlQuery(queryToDb).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numeroTarjeta"></param>
        /// <param name="fechaVencimiento"></param>
        /// <returns></returns>
        [WebMethod]
        public List<vw_maquila> GetVwMaquila(String numeroTarjeta, String fechaVencimiento)
        {
            broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
            if (numeroTarjeta.Length != 16)
            {
                return null;
            }
            String queryToDb = "select m.* from vw_maquila m "
                               + "join registri_broxel r "
                               + "on r.nruco=m.num_cuenta "
                               + "and left(m.nro_tarjeta,6)='" + numeroTarjeta.Substring(0, 6) + "' "
                               + "and right(m.nro_tarjeta,4)='" + numeroTarjeta.Substring(12, 4) + "' "
                               + "and right(r.folio_de_registro,6)='" + numeroTarjeta.Substring(6, 6) + "' "
                               + "and right(r.fecha_de_registro,5)='" + fechaVencimiento + "'";

            return broxelcoRdgEntities.vw_maquila.SqlQuery(queryToDb).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numeroDeCuenta"></param>
        /// <returns></returns>
        [WebMethod]
        public maquila GetMaquila(String numeroDeCuenta)
        {
            broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
            return broxelcoRdgEntities.maquila.FirstOrDefault(x => x.num_cuenta == numeroDeCuenta);
        }

        #endregion

        #region Cuentas

        #region Consultar Cuenta

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numeroTarjeta"></param>
        /// <param name="vencimiento"></param>
        /// <param name="cvc"></param>
        /// <returns></returns>
        [WebMethod]
        public String ObtenerNumeroCuentaPorTarjeta(String numeroTarjeta, String vencimiento, String cvc)
        {
            String query = @"select m.Id, m.num_cuenta from maquila m
                             join `registri_broxel` r
                             on m.num_cuenta = r.NRucO
                             where left(m.`nro-tarjeta`, 6) = left('" + numeroTarjeta + @"', 6)
                             and right(m.`nro-tarjeta`, 4) = right('" + numeroTarjeta + @"', 4)
                             and replace(right(r.fecha_de_registro,5), '-', '') = '" + vencimiento + @"' 
                             and Concat(right(right(left(r.transacciones,5),3),1),left(right(left(r.transacciones,5),3),1), right(left(right(left(r.transacciones,5),3),2),1)) = '" +
                           cvc + @"'
                             order by m.Id desc;";

            using (var ctx = new broxelco_rdgEntities())
            {
                var cuenta = ctx.Database.SqlQuery<ManualMaq>(query).ToList();
                if (cuenta.Count <= 0) return "0";
                return cuenta[0].num_cuenta;
            }
        }

        /// <summary>
        /// Obtiene toda la información de las cuentas de un usuario.
        /// </summary>
        /// <param name="idUser">Identificador seguro del usuario </param>
        /// <returns>Objeto UsuarioOnlineResponse</returns>
        [WebMethod]
        public UsuarioOnlineResponse ObtenCuentasxUsuario(int idUser)
        {

            var id = new IdSecureComp().GetIdUserValid(idUser);
            if (id == 0)
            {
                return null;
            }

            var usuOnResp = new UsuarioOnlineRequest();
            return usuOnResp.ObtenUsuarioInfo(id);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numCuenta"></param>
        /// <param name="idUser"></param>
        /// <returns></returns>
        [WebMethod]
        public SaldoOnlineResponse GetSaldoCuentaUsuario(String numCuenta, Int32 idUser)
        {
            //MLS idUserSecure
            var id = new IdSecureComp().GetIdUserValid(idUser);
            if (id == 0)
            {
                return new SaldoOnlineResponse {Success = false};
            }
            idUser = id;



            broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
            var cuenta = broxelcoRdgEntities.maquila.FirstOrDefault(x => x.num_cuenta == numCuenta);



            var acceso =
                broxelcoRdgEntities.accessos_clientes.FirstOrDefault(x => x.cuenta == numCuenta && x.IdMaquila != null);
            if (cuenta != null && acceso != null && acceso.IdUsuarioOnlineBroxel == idUser)
                return OnlineHelper.GetSaldosCuenta(numCuenta);


            var family = new BroxelFamily();
            /*Validación para  family*/
            if (family.EsHijoDeLaCuentaPadreFamily(idUser, numCuenta))
            {
                return OnlineHelper.GetSaldosCuenta(numCuenta);
            }
            /*Validacion para Family*/


            return new SaldoOnlineResponse {Success = false};
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numeroDeCuenta"></param>
        /// <returns></returns>
        [WebMethod]
        public List<vw_EdoCuenta> GetEdosCuenta(String numeroDeCuenta)
        {
            broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
            var p = broxelcoRdgEntities.vw_EdoCuenta.Where(x => x.numero_de_cuenta == numeroDeCuenta).
                OrderByDescending(x => x.fecha_ultima_liquidacion).ToList();
            return p;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numeroDeCuenta"></param>
        /// <returns></returns>
        [WebMethod]
        public productos_broxel GetProductoBroxel(String numeroDeCuenta)
        {
            broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
            var maquila = broxelcoRdgEntities.maquila.FirstOrDefault(x => x.num_cuenta == numeroDeCuenta);
            if (maquila != null)
            {
                var codigoProducto = maquila.producto;
                return broxelcoRdgEntities.productos_broxel.FirstOrDefault(x => x.codigo == codigoProducto);
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numeroDeCuenta"></param>
        /// <returns></returns>
        [WebMethod]
        public registri_broxel GetRegistri(String numeroDeCuenta)
        {
            broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
            try
            {
                return
                    broxelcoRdgEntities.registri_broxel.Where(x => x.NrucO == numeroDeCuenta)
                        .OrderBy(x => x.id)
                        .ToList()
                        .Last();
            }
            catch (Exception)
            {
                throw new Exception(
                    "No se encontraron datos de alguna tarjeta correspondiente a esa cuenta y terminación. Favor de contactar a soporte.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numeroDeCuenta"></param>
        /// <returns></returns>
        [WebMethod]
        public List<vw_EdoCuenta> ObtenerFechasEdosCuentas(String numeroDeCuenta)
        {
            broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
            var edosCuenta =
                broxelcoRdgEntities.vw_EdoCuenta.Where(x => x.numero_de_cuenta == numeroDeCuenta)
                    .OrderByDescending(x => x.fecha_ultima_liquidacion)
                    .Take(6).ToList();

            var maq = broxelcoRdgEntities.maquila.FirstOrDefault(x => x.num_cuenta == numeroDeCuenta);
            var prod = Entities.Instance.ProductosBroxel.FirstOrDefault(x => x.codigo == maq.producto);

            if (prod != null && prod.tipo == "Expenses" && edosCuenta.Count <= 0)
            {
                DateTime today = DateTime.Now;
                DateTime monthAgo =
                    (new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month))).AddMonths(-1);
                DateTime sixMonthsAgo = monthAgo.AddMonths(-5);

                var fechas = (from a in broxelcoRdgEntities.ind_movimientos
                    where a.NroRuc == numeroDeCuenta
                          && DateTime.Compare(a.Origen.Value, monthAgo) < 0
                          && DateTime.Compare(a.Origen.Value, sixMonthsAgo) > 0
                    orderby a.Origen descending
                    select new
                    {
                        Anio = a.Origen.Value.Year,
                        Mes = a.Origen.Value.Month
                    }).Distinct().ToList();

                edosCuenta = fechas.Select(fecha => new vw_EdoCuenta
                {
                    fecha_ultima_liquidacion = new DateTime(fecha.Anio, fecha.Mes, 1),
                    grupo_de_liquidacion = "1",
                    id = -1
                }).ToList();

                edosCuenta.Reverse();
            }
            return edosCuenta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="NumCuenta"></param>
        /// <returns></returns>
        [WebMethod]
        public CuentaOnline GetCuentaLogin(String NumCuenta)
        {
            return OnlineHelper.GetCuentaLogin(NumCuenta);
        }

        #endregion

        #region Activar Cuenta

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numCuenta"></param>
        /// <param name="idUser"></param>
        /// <returns></returns>
        [WebMethod]
        public OperarCuentaResponse Activacion(String numCuenta, int idUser)
        {
            try
            {
                //MLS idUserSecure
                var id = new IdSecureComp().GetIdUserValid(idUser);
                if (id == 0)
                {
                    return new OperarCuentaResponse();
                }
                idUser = id;


                var valida = new MySqlDataAccess();
                if (valida.ValidaProductoMejoravit(numCuenta))
                {
                    return new OperarCuentaResponse
                    {
                        UserResponse = "No puedes activar la cuenta"
                    };
                }

                broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();

                var accesoCliente =
                    broxelcoRdgEntities.accessos_clientes.FirstOrDefault(
                        x => x.cuenta == numCuenta && x.IdMaquila != null);
                if (accesoCliente != null && accesoCliente.IdUsuarioOnlineBroxel == idUser)
                {
                    return _bs.ActivacionDeCuenta(numCuenta, accesoCliente.Email);
                }
            }
            catch (Exception)
            {
                return new OperarCuentaResponse();
            }
            return new OperarCuentaResponse();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numCuenta"></param>
        /// <param name="nombreSolicitante"></param>
        /// <returns></returns>
        [WebMethod]
        public OperarCuentaResponse ActivacionDeCuenta(String numCuenta, String nombreSolicitante)
        {
            return new BroxelService().ActivacionDeCuenta(numCuenta, nombreSolicitante);
        }

        #endregion

        #region Bloquear Cuenta

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numCuenta"></param>
        /// <param name="idUser"></param>
        /// <returns></returns>
        [WebMethod]
        public OperarCuentaResponse Bloqueo(String numCuenta, int idUser)
        {
            try
            {
                //MLS idUserSecure
                var id = new IdSecureComp().GetIdUserValid(idUser);
                if (id == 0)
                {
                    return new OperarCuentaResponse();
                }
                idUser = id;


                var validar = new MySqlDataAccess();

                if (validar.ValidaProductoMejoravit(numCuenta))
                {
                    return new OperarCuentaResponse
                    {
                        UserResponse = "No puedes bloquear la cuenta"
                    };
                }

                broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();

                var accesoCliente =
                    broxelcoRdgEntities.accessos_clientes.FirstOrDefault(
                        x => x.cuenta == numCuenta && x.IdMaquila != null);
                if (accesoCliente != null && accesoCliente.IdUsuarioOnlineBroxel == idUser)
                {
                    return _bs.BloqueoDeCuenta(numCuenta, accesoCliente.Email);
                }
            }
            catch (Exception)
            {
                return new OperarCuentaResponse();
            }
            return new OperarCuentaResponse();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numCuenta"></param>
        /// <param name="nombreSolicitante"></param>
        /// <returns></returns>
        [WebMethod]
        public OperarCuentaResponse BloqueoDeCuenta(String numCuenta, String nombreSolicitante)
        {

            var validar = new MySqlDataAccess();
            if (validar.ValidaProductoMejoravit(numCuenta))
            {
                return new OperarCuentaResponse
                {
                    UserResponse = "No puedes bloquear la cuenta"
                };
            }


            return new BroxelService().BloqueoDeCuenta(numCuenta, nombreSolicitante);
        }

        #endregion

        #endregion

        #region Correos

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grupoDeLiquidacion"></param>
        /// <param name="fechaUltimaLiquidacion"></param>
        /// <returns></returns>
        [WebMethod]
        public Boolean NotificacionEstadoDeCuenta(string grupoDeLiquidacion, DateTime fechaUltimaLiquidacion)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                Helper.NotificaEstadosDeCuenta(grupoDeLiquidacion, fechaUltimaLiquidacion);
            });
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grupoDeLiquidacion"></param>
        /// <param name="fechaLiquidacionActual"></param>
        /// <returns></returns>
        [WebMethod]
        public Boolean EnviaCorreosEdosCuentaMensual(string grupoDeLiquidacion, DateTime fechaLiquidacionActual)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                Helper.NotificaEstadosDeCuenta(grupoDeLiquidacion, fechaLiquidacionActual);
            });
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="monto"></param>
        /// <param name="Destinatario"></param>
        /// <param name="noTx"></param>
        /// <param name="to"></param>
        /// <param name="productoDe"></param>
        /// <param name="productoA"></param>
        /// <param name="numCuenta"></param>
        [WebMethod]
        public void PruebaCorreoTran(double monto, string Destinatario, string noTx, string to, string productoDe,
            string productoA, string numCuenta)
        {

            var numeroTarjeta = "1234567890987654";
            var numeroTarjetaDestino = "123456789090988779";
            var concepto = "";
            // SPE
            //var datosCorreo = new DatosEmailTransferencias
            //{
            //    Fecha = DateTime.Now,
            //    Monto = Convert.ToDouble(170.34m),
            //    Usuario = "Adan Huerta",
            //    CorreoUsuario = "adanhuertaarce@yahoo.com.mx",
            //    IdCLABE = 9999,
            //    NumeroTarjeta = numeroTarjeta.Substring(numeroTarjeta.Length - 4, 4),
            //    Referencia = "234332434",
            //    Concepto = string.IsNullOrEmpty(concepto) ? "DISPOSICION CREDITO " : concepto,
            //    NumeroAutorizacion = "1213212121212",
            //    Comision = 10.22,
            //    NumeroCuenta = numCuenta
            //};

            //C2C
            //var datosCorreo = new DatosEmailTransferencias
            //{
            //    Fecha = DateTime.Now,
            //    Monto = Convert.ToDouble(monto),
            //    Usuario = "ADAN",
            //    UsuarioDestino = "ADAN DESTINO",
            //    CorreoUsuario = "tsuhaba@yopmail.com",
            //    CorreoUsuarioDestino = "myoapp5@inflexionsoftware.com",
            //    NumeroTarjeta = numeroTarjeta.Substring(numeroTarjeta.Length - 4, 4),
            //    NumeroAutorizacion = "12121212",
            //    Comision = Convert.ToDouble(340m)
            //};

            //MisTarjetas
            var datosCorreo = new DatosEmailTransferencias
            {
                Fecha = DateTime.Now,
                Monto = Convert.ToDouble(monto),
                Usuario = "Adan",
                AliasTarjeta = "Mi Alias",
                CorreoUsuario = "luis.huerta@broxel.com",
                NumeroTarjeta = numeroTarjeta.Substring(numeroTarjeta.Length - 4, 4),
                NumeroTarjetaDestino = numeroTarjetaDestino.Substring(numeroTarjetaDestino.Length - 4, 4),
                NumeroAutorizacion = "12121212",
                Comision = Convert.ToDouble(500)
            };
            new GenericBL().ComposeTranferenciasMail(datosCorreo, TipoCorreo.MisTarjetas);
            //  new GenericBL().ComposeTranferenciasMail(DateTime.Now.AddDays(-15),monto,Destinatario,noTx,to,productoDe,productoA,numCuenta,"1234567890987654");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        [WebMethod]
        public void PruebaCorreoBienvenida(string host)
        {
            Mailing.EnviaCorreoBienvenida("PLOMERIA GARCIA DE MONTERREY, S.A. DE C.V.", "0008133300** ****3300",
                "jefecontabilidad@plomeriagarcia.com.mx", "jefecontabilidad@plomeriagarcia.com.mx", host, "K175");
        }

        #endregion

        #region Tranferencias

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cuentaOrigen"></param>
        /// <param name="monto"></param>
        /// <param name="tarjetaDestino"></param>
        /// <param name="IdUsuario"></param>
        /// <param name="conceptoTransferencia"></param>
        /// <returns></returns>
        [WebMethod]
        public TransferenciaResponse TransferenciaAOtrasCuentas(String cuentaOrigen, Decimal monto, String tarjetaDestino, Int32 IdUsuario, String conceptoTransferencia)
        {
            // TODO: Cambiar OnlineBroxel
            //MLS idUserSecure

            var secComp = new IdSecureComp();

            var idUser = secComp.GetIdUserValid(IdUsuario);
            if (idUser == 0)
            {
                return new TransferenciaResponse
                {
                    UserResponse = "Usuario inválido o la sesión expiró"
                };
            }
            IdUsuario = idUser;

            broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();

            var usuario = broxelcoRdgEntities.UsuariosOnlineBroxel.FirstOrDefault(x => x.Id == IdUsuario);
            if (usuario == null)
            {
                return new TransferenciaResponse
                {
                    UserResponse = "No se encontró el usuario"
                };
            }

            var ac =
                broxelcoRdgEntities.accessos_clientes.FirstOrDefault(
                    a => a.cuenta == cuentaOrigen && a.IdUsuarioOnlineBroxel == IdUsuario);
            if (ac == null)
            {
                return new TransferenciaResponse
                {
                    UserResponse = "Existió un error al validar la cuenta de origen. Reintente"
                };
            }

            String emailUsuario = usuario.CorreoElectronico;
            BroxelService broxelService = new BroxelService();
            TransferenciaResponse transferenciaResponse = new TransferenciaResponse();

            // Después de que el usuario es valido, validar si tiene producto mejoravit.  

            //Inicio Logica mamalona
            try
            {
                if (tarjetaDestino.Length == CUENTA_LENGHT)
                    tarjetaDestino = Helper.GetTarjetaFromCuenta(tarjetaDestino).NumeroTarjeta;
                else if (tarjetaDestino.Length < 16)
                {
                    var idFavorito = secComp.GetIdUserValid(Convert.ToInt32(tarjetaDestino));
                    if (idFavorito != 0)
                    {
                        var fav =
                            broxelcoRdgEntities.OnLineFavoritos.FirstOrDefault(
                                x => x.id == idFavorito && x.idUser == idUser);
                        if (fav == null)
                            throw new Exception("TarjetaDestino: " + tarjetaDestino + "idFav: " +
                                                idFavorito.ToString(CultureInfo.InvariantCulture));
                        tarjetaDestino = Helper.GetTarjetaFromCuenta(fav.cuentaFavorita).NumeroTarjeta;
                    }
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(DateTime.Now.ToString("O") + "TransferenciaAOtrasCuentas: " + e);
                return new TransferenciaResponse
                {
                    UserResponse = "La cuenta destino no es válida",
                };
            }
            //Fin Logica mamalona


            string cuentaDestino = Helper.GetCuentaFromTarjeta(tarjetaDestino);

            var valida = new MySqlDataAccess();
            if (valida.ValidaProductoMejoravit(cuentaOrigen, cuentaDestino))
            {

                return new TransferenciaResponse
                {
                    UserResponse = "No puedes realizar ó recibir transferencias a tarjetas mejoravit"
                };

            }


            maquila maquilaOrigen = broxelcoRdgEntities.maquila.FirstOrDefault(x => x.num_cuenta == cuentaOrigen);
            Tarjeta tarDestino = new Tarjeta(numTarjeta: tarjetaDestino,
                cuenta: Helper.GetCuentaFromTarjeta(tarjetaDestino));

            if (maquilaOrigen == null)
                return new TransferenciaResponse
                {
                    UserResponse = "No se encontro la cuenta origen.",
                };
            if (String.IsNullOrEmpty(tarDestino.Cuenta))
                return new TransferenciaResponse
                {
                    UserResponse = "No se encontro la tarjeta destino.",
                };

            maquila maquilaDestino = broxelcoRdgEntities.maquila.FirstOrDefault(x => x.num_cuenta == tarDestino.Cuenta);

            if (maquilaDestino == null)
                return new TransferenciaResponse
                {
                    UserResponse = "No se encontro la cuenta destino.",
                };

            var query =
                "SELECT COALESCE(substr(max(`folio`),6,6)+1,1,substr(max(`folio`),6,6)+1) AS folio FROM TransferenciasSolicitud WHERE SUBSTRING(`folio`,1,5)='" +
                DateTime.Now.ToString("yyMM") + "T'";

            // ReSharper disable once PossibleNullReferenceException
            var folio = DateTime.Now.ToString("yyMM") + "T" +
                        broxelcoRdgEntities.Database.SqlQuery<String>(query)
                            .FirstOrDefault()
                            .PadLeft(6)
                            .Replace(' ', '0');
            try
            {
                transferenciaResponse = broxelService.TransferenciaDeCuentas(cuentaOrigen, monto, tarjetaDestino,
                    emailUsuario, "P2P");
                if (transferenciaResponse.CodigoRespuesta == -1)
                {
                    //obtener el email del usuario destino para obtener su imagen de perfil en MYO.
                    var datosCuentaDestino =
                        broxelcoRdgEntities.accessos_clientes.FirstOrDefault(x => x.cuenta == cuentaDestino);
                    var datosCorreo = new DatosEmailTransferencias
                    {
                        Fecha = DateTime.Now,
                        Monto = Convert.ToDouble(monto),
                        Usuario = usuario.NombreCompleto,
                        UsuarioDestino = maquilaDestino.nombre_titular,
                        CorreoUsuario = usuario.CorreoElectronico,
                        CorreoUsuarioDestino = datosCuentaDestino != null ? datosCuentaDestino.Email : "",
                        NumeroTarjeta = maquilaOrigen.nro_tarjeta.Substring(maquilaOrigen.nro_tarjeta.Length - 4, 4),
                        NumeroAutorizacion = transferenciaResponse.NumeroAutorizacion,
                        Comision = Convert.ToDouble(transferenciaResponse.Comision.MontoComision)
                    };
                    new GenericBL().ComposeTranferenciasMail(datosCorreo, TipoCorreo.C2C);
                    //new GenericBL().ComposeTranferenciasMail(DateTime.Now,Convert.ToDouble(monto),maquilaDestino.nombre_titular,folio,emailUsuario);
                }
                TransferenciasSolicitud transferenciasSolicitud = new TransferenciasSolicitud
                {
                    ClaveCliente = maquilaOrigen.clave_cliente,
                    FechaHoraAprobacion = DateTime.Now,
                    FechaHoraCreacion = DateTime.Now,
                    FechaHoraEjecucion = DateTime.Now,
                    FechaHoraVerificacion = DateTime.Now,
                    Folio = folio,
                    Origen = "OnlineApp",
                    Producto = maquilaOrigen.producto,
                    Tipo = "P2P",
                    UsuarioAprobacion = emailUsuario,
                    UsuarioCreacion = emailUsuario,
                    UsuarioEjecucion = emailUsuario,
                    UsuarioVerificacion = emailUsuario,
                    Estado = transferenciaResponse.CodigoRespuesta == -1 ? "COMPLETA" : "CON ERRORES",
                    AreaSolicitante = "CLIENTE",
                    EmailNotificacion = emailUsuario,
                    MontoTotal = transferenciaResponse.SaldoDestinoDespues - transferenciaResponse.SaldoDestinoAntes,
                    Solicitante = emailUsuario,
                    TotalDeCuentas = 1,
                    Cliente = "",
                    RfcCliente = "",
                    ConceptoTransferencia = conceptoTransferencia,
                };
                broxelcoRdgEntities.TransferenciasSolicitud.Add(transferenciasSolicitud);
                broxelcoRdgEntities.SaveChanges();
                TransferenciasDetalle transferenciasDetalle = new TransferenciasDetalle
                {

                    ClaveClienteDestino = maquilaDestino.clave_cliente,
                    ClaveClienteOrigen = maquilaOrigen.clave_cliente,
                    CodigoAutorizacion = transferenciaResponse.NumeroAutorizacion,
                    CodigoRespuesta = transferenciaResponse.CodigoRespuesta.ToString(CultureInfo.InvariantCulture),
                    MontoComision = Convert.ToDecimal(transferenciaResponse.Comision.MontoComision),
                    CuentaDestino = maquilaDestino.num_cuenta,
                    CuentaOrigen = maquilaOrigen.num_cuenta,
                    IdMovimiento = transferenciaResponse.IdMovimiento.ToString(CultureInfo.InvariantCulture),
                    IdSolicitud = transferenciasSolicitud.Id,
                    Monto = monto,
                    ProductoDestino = maquilaDestino.producto,
                    ProductoOrigen = maquilaOrigen.producto,
                    SaldoDestinoAntes = transferenciaResponse.SaldoDestinoAntes,
                    SaldoDestinoDespues = transferenciaResponse.SaldoDestinoDespues,
                    SaldoOrigenAntes = transferenciaResponse.SaldoOrigenAntes,
                    SaldoOrigenDespues = transferenciaResponse.SaldoOrigenDespues,
                    TipoComision = transferenciaResponse.Comision.TipoComision,
                    TipoConceptoComision = transferenciaResponse.Comision.TipoConceptoComision,
                    FolioSolicitud = folio,
                };
                broxelcoRdgEntities.TransferenciasDetalle.Add(transferenciasDetalle);
                broxelcoRdgEntities.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                String p = String.Empty;
                foreach (var eve in e.EntityValidationErrors)
                {
                    p += String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    p = eve.ValidationErrors.Aggregate(p,
                        (current, ve) =>
                            current +
                            String.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                }
                var args = ", cuentaOrigen= " + cuentaOrigen + ", monto = " +
                           monto.ToString(CultureInfo.InvariantCulture) + ", tarjetaDestino = " + tarjetaDestino +
                           ", IdUsuario = " + IdUsuario.ToString(CultureInfo.InvariantCulture);

                Helper.SendMail("broxelonline@broxel.com", " alberto.ortiz@broxel.com, mauricio.lopez@broxel.com",
                    "Fallo registro de Transferencia en : wsAdmonUsuarios - DBEntity",
                    "DbEntityValidationException en : " + p + e + ". Argumentos: " + args, "67896789");
            }
            catch (Exception e)
            {
                var args = ", cuentaOrigen= " + cuentaOrigen + ", monto = " +
                           monto.ToString(CultureInfo.InvariantCulture) + ", tarjetaDestino = " + tarjetaDestino +
                           ", IdUsuario = " + IdUsuario.ToString(CultureInfo.InvariantCulture);

                Helper.SendMail("broxelonline@broxel.com", " alberto.ortiz@broxel.com, mauricio.lopez@broxel.com",
                    "Fallo registro de Transferencia en : wsAdmonUsuarios ", "Error : " + e + ". Argumentos: " + args,
                    "67896789");
            }
            return transferenciaResponse;
        }

        /// <summary>
        /// Metodo utilizado para realizar el pago de premios de LN.
        /// </summary>
        /// <param name="monto"></param>
        /// <param name="tarjetaDestino"></param>
        /// <param name="idUsuario"></param>
        /// <param name="conceptoTransferencia"></param>
        /// <returns></returns>
        [WebMethod]
        public TransferenciaResponse TransferenciaPagoPremio(decimal monto, string tarjetaDestino, int idUsuario, string conceptoTransferencia)
        {
            //MLS idUserSecure
            var secComp = new IdSecureComp();

            var idUser = secComp.GetIdUserValid(idUsuario);
            if (idUser == 0)
            {
                return new TransferenciaResponse
                {
                    UserResponse = "Usuario inválido o la sesión expiró"
                };
            }
            idUsuario = idUser;

            var broxelcoRdgEntities = new broxelco_rdgEntities();

            var usuario = broxelcoRdgEntities.UsuariosOnlineBroxel.FirstOrDefault(x => x.Id == idUsuario);
            if (usuario == null)
            {
                return new TransferenciaResponse
                {
                    UserResponse = "No se encontró el usuario"
                };
            }

            var emailUsuario = usuario.CorreoElectronico;
            var broxelService = new BroxelService();
            var transferenciaResponse = new TransferenciaResponse();
            try
            {
                if (tarjetaDestino.Length == CUENTA_LENGHT)
                    tarjetaDestino = Helper.GetTarjetaFromCuenta(tarjetaDestino).NumeroTarjeta;
                else if (tarjetaDestino.Length < 16)
                {
                    var idFavorito = secComp.GetIdUserValid(Convert.ToInt32(tarjetaDestino));
                    if (idFavorito != 0)
                    {
                        var fav =
                            broxelcoRdgEntities.OnLineFavoritos.FirstOrDefault(
                                x => x.id == idFavorito && x.idUser == idUser);
                        if (fav == null)
                            throw new Exception("TarjetaDestino: " + tarjetaDestino + "idFav: " +
                                                idFavorito.ToString(CultureInfo.InvariantCulture));
                        tarjetaDestino = Helper.GetTarjetaFromCuenta(fav.cuentaFavorita).NumeroTarjeta;
                    }
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(DateTime.Now.ToString("O") + "TransferenciaAOtrasCuentas: " + e);
                return new TransferenciaResponse
                {
                    UserResponse = "La cuenta destino no es válida",
                };
            }
            var cuentaOrigen = ConfigurationManager.AppSettings["CuentaOrigenLN"];
            var cuentaDestino = Helper.GetCuentaFromTarjeta(tarjetaDestino);

            var valida = new MySqlDataAccess();
            if (valida.ValidaProductoMejoravit(cuentaOrigen, cuentaDestino))
            {

                return new TransferenciaResponse
                {
                    UserResponse = "No puedes realizar ó recibir transferencias a tarjetas mejoravit"
                };

            }

            var maquilaOrigen = broxelcoRdgEntities.maquila.FirstOrDefault(x => x.num_cuenta == cuentaOrigen);
            var tarDestino = new Tarjeta(numTarjeta: tarjetaDestino, cuenta: Helper.GetCuentaFromTarjeta(tarjetaDestino));

            if (maquilaOrigen == null)
                return new TransferenciaResponse
                {
                    UserResponse = "No se encontro la cuenta origen.",
                };
            if (string.IsNullOrEmpty(tarDestino.Cuenta))
                return new TransferenciaResponse
                {
                    UserResponse = "No se encontro la tarjeta destino.",
                };

            var maquilaDestino = broxelcoRdgEntities.maquila.FirstOrDefault(x => x.num_cuenta == tarDestino.Cuenta);

            if (maquilaDestino == null)
                return new TransferenciaResponse
                {
                    UserResponse = "No se encontro la cuenta destino.",
                };

            var query =
                "SELECT COALESCE(substr(max(`folio`),6,6)+1,1,substr(max(`folio`),6,6)+1) AS folio FROM TransferenciasSolicitud WHERE SUBSTRING(`folio`,1,5)='" +
                DateTime.Now.ToString("yyMM") + "T'";

            // ReSharper disable once PossibleNullReferenceException
            var folio = DateTime.Now.ToString("yyMM") + "T" +
                        broxelcoRdgEntities.Database.SqlQuery<string>(query)
                            .FirstOrDefault()
                            .PadLeft(6)
                            .Replace(' ', '0');
            try
            {
                transferenciaResponse = broxelService.TransferenciaDeCuentas(cuentaOrigen, monto, tarjetaDestino,
                    emailUsuario, "P2P");
                if (transferenciaResponse.CodigoRespuesta == -1)
                {
                    //obtener el email del usuario destino para obtener su imagen de perfil en MYO.
                    var datosCuentaDestino =
                        broxelcoRdgEntities.accessos_clientes.FirstOrDefault(x => x.cuenta == cuentaDestino);
                    var datosCorreo = new DatosEmailTransferencias
                    {
                        Fecha = DateTime.Now,
                        Monto = Convert.ToDouble(monto),
                        Usuario = usuario.NombreCompleto,
                        UsuarioDestino = maquilaDestino.nombre_titular,
                        CorreoUsuario = usuario.CorreoElectronico,
                        CorreoUsuarioDestino = datosCuentaDestino != null ? datosCuentaDestino.Email : "",
                        NumeroTarjeta = maquilaOrigen.nro_tarjeta.Substring(maquilaOrigen.nro_tarjeta.Length - 4, 4),
                        NumeroAutorizacion = transferenciaResponse.NumeroAutorizacion,
                        Comision = Convert.ToDouble(transferenciaResponse.Comision.MontoComision)
                    };
                    new GenericBL().ComposeTranferenciasMail(datosCorreo, TipoCorreo.C2C);
                    //new GenericBL().ComposeTranferenciasMail(DateTime.Now,Convert.ToDouble(monto),maquilaDestino.nombre_titular,folio,emailUsuario);
                }
                var transferenciasSolicitud = new TransferenciasSolicitud
                {
                    ClaveCliente = maquilaOrigen.clave_cliente,
                    FechaHoraAprobacion = DateTime.Now,
                    FechaHoraCreacion = DateTime.Now,
                    FechaHoraEjecucion = DateTime.Now,
                    FechaHoraVerificacion = DateTime.Now,
                    Folio = folio,
                    Origen = "OnlineApp",
                    Producto = maquilaOrigen.producto,
                    Tipo = "P2P",
                    UsuarioAprobacion = emailUsuario,
                    UsuarioCreacion = emailUsuario,
                    UsuarioEjecucion = emailUsuario,
                    UsuarioVerificacion = emailUsuario,
                    Estado = transferenciaResponse.CodigoRespuesta == -1 ? "COMPLETA" : "CON ERRORES",
                    AreaSolicitante = "CLIENTE",
                    EmailNotificacion = emailUsuario,
                    MontoTotal = transferenciaResponse.SaldoDestinoDespues - transferenciaResponse.SaldoDestinoAntes,
                    Solicitante = emailUsuario,
                    TotalDeCuentas = 1,
                    Cliente = "",
                    RfcCliente = "",
                    ConceptoTransferencia = conceptoTransferencia,
                };
                broxelcoRdgEntities.TransferenciasSolicitud.Add(transferenciasSolicitud);
                broxelcoRdgEntities.SaveChanges();
                var transferenciasDetalle = new TransferenciasDetalle
                {

                    ClaveClienteDestino = maquilaDestino.clave_cliente,
                    ClaveClienteOrigen = maquilaOrigen.clave_cliente,
                    CodigoAutorizacion = transferenciaResponse.NumeroAutorizacion,
                    CodigoRespuesta = transferenciaResponse.CodigoRespuesta.ToString(CultureInfo.InvariantCulture),
                    MontoComision = Convert.ToDecimal(transferenciaResponse.Comision.MontoComision),
                    CuentaDestino = maquilaDestino.num_cuenta,
                    CuentaOrigen = maquilaOrigen.num_cuenta,
                    IdMovimiento = transferenciaResponse.IdMovimiento.ToString(CultureInfo.InvariantCulture),
                    IdSolicitud = transferenciasSolicitud.Id,
                    Monto = monto,
                    ProductoDestino = maquilaDestino.producto,
                    ProductoOrigen = maquilaOrigen.producto,
                    SaldoDestinoAntes = transferenciaResponse.SaldoDestinoAntes,
                    SaldoDestinoDespues = transferenciaResponse.SaldoDestinoDespues,
                    SaldoOrigenAntes = transferenciaResponse.SaldoOrigenAntes,
                    SaldoOrigenDespues = transferenciaResponse.SaldoOrigenDespues,
                    TipoComision = transferenciaResponse.Comision.TipoComision,
                    TipoConceptoComision = transferenciaResponse.Comision.TipoConceptoComision,
                    FolioSolicitud = folio,
                };
                broxelcoRdgEntities.TransferenciasDetalle.Add(transferenciasDetalle);
                broxelcoRdgEntities.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var p = string.Empty;
                foreach (var eve in e.EntityValidationErrors)
                {
                    p += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    p = eve.ValidationErrors.Aggregate(p,
                        (current, ve) =>
                            current +
                            string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                }
                var args = ", cuentaOrigen= " + cuentaOrigen + ", monto = " +
                           monto.ToString(CultureInfo.InvariantCulture) + ", tarjetaDestino = " + tarjetaDestino +
                           ", IdUsuario = " + idUsuario.ToString(CultureInfo.InvariantCulture);

                Helper.SendMail("broxelonline@broxel.com", " alberto.ortiz@broxel.com, mauricio.lopez@broxel.com",
                    "Fallo registro de Transferencia en : wsAdmonUsuarios - DBEntity",
                    "DbEntityValidationException en : " + p + e + ". Argumentos: " + args, "67896789");
            }
            catch (Exception e)
            {
                var args = ", cuentaOrigen= " + cuentaOrigen + ", monto = " +
                           monto.ToString(CultureInfo.InvariantCulture) + ", tarjetaDestino = " + tarjetaDestino +
                           ", IdUsuario = " + idUsuario.ToString(CultureInfo.InvariantCulture);

                Helper.SendMail("broxelonline@broxel.com", " alberto.ortiz@broxel.com, mauricio.lopez@broxel.com",
                    "Fallo registro de Transferencia en : wsAdmonUsuarios ", "Error : " + e + ". Argumentos: " + args,
                    "67896789");
            }
            return transferenciaResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cuentaOrigen"></param>
        /// <param name="monto"></param>
        /// <param name="tarjetaDestino"></param>
        /// <param name="IdUsuario"></param>
        /// <param name="conceptoTransferencia"></param>
        /// <returns></returns>
        [WebMethod]
        public TransferenciaResponse TransferenciaPagoRecompensas(String cuentaOrigen, List<CuentasRecompensa> cuentasInfo, Int32 idUsuario, String referencia)
        {
            // TODO: Cambiar OnlineBroxel
            //MLS idUserSecure

            var secComp = new IdSecureComp();
            var valida = new MySqlDataAccess();

            var idUser = secComp.GetIdUserValid(idUsuario);
            if (idUser == 0)
            {
                return new TransferenciaResponse
                {
                    UserResponse = "Usuario inválido o la sesión expiró"
                };
            }
            idUsuario = idUser;

            var broxelcoRdgEntities = new broxelco_rdgEntities();

            var usuario = broxelcoRdgEntities.UsuariosOnlineBroxel.FirstOrDefault(x => x.Id == idUsuario);
            if (usuario == null)
            {
                return new TransferenciaResponse
                {
                    UserResponse = "No se encontró el usuario"
                };
            }

            var broxelService = new BroxelService();
            var transferenciaResponse = new TransferenciaResponse();

            if (cuentasInfo == null || cuentasInfo.Count == 0)
            {
                return new TransferenciaResponse
                {
                    UserResponse = "No se pueden procesar listas de cuentas vacias"
                };
            }

            var cuentasInfoInt = new List<CuentaRecompensaInterna>();
            Decimal montoTotal = 0;

            //Inicio Logica mamalona
            try
            {
                for (var i=0; i<cuentasInfo.Count;i++)
                {
                    var cuentaInfoInt = new CuentaRecompensaInterna
                    {
                        Cuenta =cuentasInfo[i].Cuenta,
                        Monto = cuentasInfo[i].Monto
                    };
                    var tarjeta = Helper.GetTarjetaFromCuenta(cuentasInfo[i].Cuenta);
                    if(tarjeta==null)
                        throw new Exception("La cuenta " + cuentasInfo[i].Cuenta + " es inválida");
                    cuentaInfoInt.Tarjeta = tarjeta;

                    // Después de que el usuario es valido, validar si tiene producto mejoravit.  
                    if (valida.ValidaProductoMejoravit(cuentaOrigen, cuentasInfo[i].Cuenta))
                        throw new Exception("No puedes realizar ó recibir transferencias a tarjetas mejoravit");

                    cuentaInfoInt.maquila =
                        broxelcoRdgEntities.maquila.FirstOrDefault(x => x.num_cuenta == cuentaInfoInt.Cuenta);

                    if (cuentaInfoInt.maquila == null)
                        throw new Exception("No se encontro la cuenta destino " + cuentasInfo[i].Cuenta + ".");

                    montoTotal += cuentasInfo[i].Monto;

                    cuentasInfoInt.Add(cuentaInfoInt);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(DateTime.Now.ToString("O") + "TransferenciaAOtrasCuentas: " + e);
                return new TransferenciaResponse
                {
                    UserResponse = e.Message,
                };
            }
            //Fin Logica mamalona
            

            maquila maquilaOrigen = broxelcoRdgEntities.maquila.FirstOrDefault(x => x.num_cuenta == cuentaOrigen);
            
            if (maquilaOrigen == null)
                return new TransferenciaResponse
                {
                    UserResponse = "No se encontro la cuenta origen.",
                };
            
            var query = "SELECT COALESCE(substr(max(`folio`),6,6)+1,1,substr(max(`folio`),6,6)+1) AS folio FROM TransferenciasSolicitud WHERE SUBSTRING(`folio`,1,5)='" + DateTime.Now.ToString("yyMM") + "T'";

            // ReSharper disable once PossibleNullReferenceException
            var folio = DateTime.Now.ToString("yyMM") + "T" + broxelcoRdgEntities.Database.SqlQuery<String>(query).FirstOrDefault().PadLeft(6).Replace(' ', '0');
            try
            {
                var transferenciasSolicitud = new TransferenciasSolicitud
                {
                    ClaveCliente = maquilaOrigen.clave_cliente,
                    FechaHoraCreacion = DateTime.Now,
                    Folio = folio,
                    Origen = "Corporate",
                    Producto = maquilaOrigen.producto,
                    Tipo = "ConcentradoraACuentas",
                    UsuarioCreacion = "asignaciondelinea@broxel.com",
                    UsuarioAprobacion = "",
                    FechaHoraAprobacion = DateTime.Now,
                    UsuarioEjecucion = "",
                    FechaHoraEjecucion = DateTime.Now,
                    Estado = "NUEVO",
                    AreaSolicitante = "CLIENTE",
                    EmailNotificacion = "asignaciondelinea@broxel.com",
                    MontoTotal = montoTotal,
                    Solicitante = "WebService",
                    TotalDeCuentas = cuentasInfo.Count,
                    Cliente = "",
                    RfcCliente = "",
                    ConceptoTransferencia = referencia
                };
                broxelcoRdgEntities.TransferenciasSolicitud.Add(transferenciasSolicitud);
                broxelcoRdgEntities.SaveChanges();

                foreach (var interna in cuentasInfoInt)
                {
                    var transferenciasDetalle = new TransferenciasDetalle
                    {

                        ClaveClienteDestino = interna.maquila.clave_cliente,
                        ClaveClienteOrigen = maquilaOrigen.clave_cliente,
                        MontoComision = 0,
                        CuentaDestino = interna.maquila.num_cuenta,
                        CuentaOrigen = maquilaOrigen.num_cuenta,
                        IdSolicitud = transferenciasSolicitud.Id,
                        Monto = interna.Monto,
                        ProductoDestino = interna.maquila.producto,
                        ProductoOrigen = maquilaOrigen.producto,
                        FolioSolicitud = folio,
                    };
                    broxelcoRdgEntities.TransferenciasDetalle.Add(transferenciasDetalle);
                    broxelcoRdgEntities.SaveChanges();
                }
                transferenciaResponse.Success = 1;
                transferenciaResponse.NumeroAutorizacion = transferenciasSolicitud.Id.ToString(CultureInfo.InvariantCulture);
                transferenciaResponse.CodigoRespuesta = -1;
                transferenciaResponse.UserResponse = "Transferencia creada con el folio: " + folio;
            }
            catch (DbEntityValidationException e)
            {
                String p = String.Empty;
                foreach (var eve in e.EntityValidationErrors)
                {
                    p += String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    p = eve.ValidationErrors.Aggregate(p, (current, ve) => current + String.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                }

                var argsList = cuentasInfo.Aggregate("", (current, c) => current + (", monto = " + c.Monto.ToString(CultureInfo.InvariantCulture) + ", cuenta = " + c.Cuenta));

                var args = ", cuentaOrigen= " + cuentaOrigen + 
                           argsList +
                           ", IdUsuario = " + idUsuario.ToString(CultureInfo.InvariantCulture);

                Helper.SendMail("broxelonline@broxel.com", " alberto.ortiz@broxel.com, mauricio.lopez@broxel.com", "Fallo registro de Transferencia en : wsAdmonUsuarios - DBEntity", "DbEntityValidationException en : " + p + e + ". Argumentos: " + args, "67896789");
            }
            catch (Exception e)
            {
                var argsList = cuentasInfo.Aggregate("", (current, c) => current + (", monto = " + c.Monto.ToString(CultureInfo.InvariantCulture) + ", cuenta = " + c.Cuenta));

                var args = ", cuentaOrigen= " + cuentaOrigen +
                           argsList +
                           ", IdUsuario = " + idUsuario.ToString(CultureInfo.InvariantCulture);

                Helper.SendMail("broxelonline@broxel.com", " alberto.ortiz@broxel.com, mauricio.lopez@broxel.com", "Fallo registro de Transferencia en : wsAdmonUsuarios ", "Error : " + e + ". Argumentos: " + args, "67896789");
            }
            return transferenciaResponse;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cuentaOrigen"></param>
        /// <param name="monto"></param>
        /// <param name="tarjetaDestino"></param>
        /// <param name="eMail"></param>
        /// <param name="conceptoTransferencia"></param>
        /// <returns></returns>
        [WebMethod]
        public TransferenciaResponse TransferenciaAOtrasCuentasByEmail(String cuentaOrigen, Decimal monto,
            String tarjetaDestino, String eMail, string conceptoTransferencia)
        {
            // TODO: Cambiar OnlineBroxel

            broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
            BroxelCommonEntities broxelCommonEntities = new BroxelCommonEntities();
            //Se realiza la busqueda del usuario via eMail

            var usuario = broxelcoRdgEntities.UsuariosOnlineBroxel.FirstOrDefault(x => x.CorreoElectronico == eMail);
            if (usuario == null)
            {
                return new TransferenciaResponse
                {
                    UserResponse = "No se encontró el usuario"
                };
            }

            var ac =
                broxelcoRdgEntities.accessos_clientes.FirstOrDefault(
                    a => a.cuenta == cuentaOrigen && a.IdUsuarioOnlineBroxel == usuario.Id);
            if (ac == null)
            {
                var hija = broxelCommonEntities.CuentaAsociadaFamily.FirstOrDefault(s => s.NumeroCuenta == cuentaOrigen);

                if (hija != null)
                {
                    var padre = broxelCommonEntities.CuentaPadreFamily.FirstOrDefault(s => s.Id == hija.IdCuentaPadre);
                    if (padre != null)
                    {
                        if (usuario.Id != padre.IdUsuarioOnlineBroxel)
                        {
                            return new TransferenciaResponse
                            {
                                UserResponse = "Existió un error al validar la cuenta de origen. Reintente"
                            };
                        }
                    }
                    else
                    {
                        return new TransferenciaResponse
                        {
                            UserResponse = "Existió un error al validar la cuenta de origen. Reintente"
                        };
                    }
                }
                else
                {
                    return new TransferenciaResponse
                    {
                        UserResponse = "Existió un error al validar la cuenta de origen. Reintente"
                    };
                }
            }


            //Inicio Logica mamalona
            try
            {
                if (tarjetaDestino.Length == CUENTA_LENGHT)
                    tarjetaDestino = Helper.GetTarjetaFromCuenta(tarjetaDestino).NumeroTarjeta;
                else if (tarjetaDestino.Length < 16)
                {
                    var idFavorito = new IdSecureComp().GetIdUserValid(Convert.ToInt32(tarjetaDestino));
                    if (idFavorito != 0)
                    {
                        var fav =
                            broxelcoRdgEntities.OnLineFavoritos.FirstOrDefault(
                                x => x.id == idFavorito && x.idUser == usuario.Id);
                        if (fav == null)
                            throw new Exception("TarjetaDestino: " + tarjetaDestino + "idFav: " +
                                                idFavorito.ToString(CultureInfo.InvariantCulture));
                        tarjetaDestino = Helper.GetTarjetaFromCuenta(fav.cuentaFavorita).NumeroTarjeta;
                    }
                }

            }
            catch (Exception e)
            {
                Trace.WriteLine(DateTime.Now.ToString("O") + "TransferenciaAOtrasCuentasByEmail: " + e);
                return new TransferenciaResponse
                {
                    UserResponse = "La cuenta destino no es válida",
                };
            }
            //Fin Logica mamalona

            string cuentaDestino = Helper.GetCuentaFromTarjeta(tarjetaDestino);

            var valida = new MySqlDataAccess();
            if (valida.ValidaProductoMejoravit(cuentaOrigen, cuentaDestino))
            {
                return new TransferenciaResponse
                {
                    UserResponse = "No puedes realizar o recibir transferencias a tarjetas Mejoravit"
                };
            }


            Int32 idUsuario = usuario.Id;

            String emailUsuario = usuario.CorreoElectronico;
            BroxelService broxelService = new BroxelService();
            TransferenciaResponse transferenciaResponse = new TransferenciaResponse();


            maquila maquilaOrigen = broxelcoRdgEntities.maquila.FirstOrDefault(x => x.num_cuenta == cuentaOrigen);

            var cuenta = Helper.GetCuentaFromTarjeta(tarjetaDestino);
            if (String.IsNullOrEmpty(cuenta))
            {
                cuenta = Helper.GetCuentaFromTarjetaAdicional(tarjetaDestino);

                if (!String.IsNullOrEmpty(cuenta))
                {
                    var tarjetaTitular = Helper.GetTarjetaFromCuenta(cuenta);
                    tarjetaDestino = tarjetaTitular != null ? tarjetaTitular.NumeroTarjeta : tarjetaDestino;
                }
            }

            Tarjeta tarDestino = new Tarjeta(numTarjeta: tarjetaDestino,cuenta: cuenta);

            if (maquilaOrigen == null)
                return new TransferenciaResponse
                {
                    UserResponse = "No se encontró la cuenta origen.",
                };
            if (String.IsNullOrEmpty(tarDestino.Cuenta))
                return new TransferenciaResponse
                {
                    UserResponse = "No se encontró la tarjeta destino.",
                };

            maquila maquilaDestino = broxelcoRdgEntities.maquila.FirstOrDefault(x => x.num_cuenta == tarDestino.Cuenta);

            if (maquilaDestino == null)
                return new TransferenciaResponse
                {
                    UserResponse = "No se encontró la cuenta destino.",
                };

            var query =
                "SELECT COALESCE(substr(max(`folio`),6,6)+1,1,substr(max(`folio`),6,6)+1) AS folio FROM TransferenciasSolicitud WHERE SUBSTRING(`folio`,1,5)='" +
                DateTime.Now.ToString("yyMM") + "T'";

            // ReSharper disable once PossibleNullReferenceException
            var folio = DateTime.Now.ToString("yyMM") + "T" +
                        broxelcoRdgEntities.Database.SqlQuery<String>(query)
                            .FirstOrDefault()
                            .PadLeft(6)
                            .Replace(' ', '0');
            try
            {
                transferenciaResponse = broxelService.TransferenciaDeCuentas(cuentaOrigen, monto, tarjetaDestino,
                    emailUsuario, "P2P");
                if (transferenciaResponse.CodigoRespuesta == -1)
                {

                    //obtener el email del usuario destino para obtener su imagen de perfil en MYO.
                    var datosCuentaDestino =
                        broxelcoRdgEntities.accessos_clientes.FirstOrDefault(x => x.cuenta == cuentaDestino);
                    var datosCorreo = new DatosEmailTransferencias
                    {
                        Fecha = DateTime.Now,
                        Monto = Convert.ToDouble(monto),
                        Usuario = usuario.NombreCompleto,
                        UsuarioDestino = maquilaDestino.nombre_titular,
                        CorreoUsuario = usuario.CorreoElectronico,
                        CorreoUsuarioDestino = datosCuentaDestino != null ? datosCuentaDestino.Email : "",
                        NumeroTarjeta = maquilaOrigen.nro_tarjeta.Substring(maquilaOrigen.nro_tarjeta.Length - 4, 4),
                        NumeroAutorizacion = transferenciaResponse.NumeroAutorizacion,
                        Comision = Convert.ToDouble(transferenciaResponse.Comision.MontoComision)
                    };
                    new GenericBL().ComposeTranferenciasMail(datosCorreo, TipoCorreo.C2C);
                    // new GenericBL().ComposeTranferenciasMail(DateTime.Now, Convert.ToDouble(monto), maquilaDestino.nombre_titular, folio, emailUsuario);
                }
                TransferenciasSolicitud transferenciasSolicitud = new TransferenciasSolicitud
                {
                    ClaveCliente = maquilaOrigen.clave_cliente,
                    FechaHoraAprobacion = DateTime.Now,
                    FechaHoraCreacion = DateTime.Now,
                    FechaHoraEjecucion = DateTime.Now,
                    FechaHoraVerificacion = DateTime.Now,
                    Folio = folio,
                    Origen = "OnlineApp",
                    Producto = maquilaOrigen.producto,
                    Tipo = "P2P",
                    UsuarioAprobacion = emailUsuario,
                    UsuarioCreacion = emailUsuario,
                    UsuarioEjecucion = emailUsuario,
                    UsuarioVerificacion = emailUsuario,
                    Estado = transferenciaResponse.CodigoRespuesta == -1 ? "COMPLETA" : "CON ERRORES",
                    AreaSolicitante = "CLIENTE",
                    EmailNotificacion = emailUsuario,
                    MontoTotal = transferenciaResponse.SaldoDestinoDespues - transferenciaResponse.SaldoDestinoAntes,
                    Solicitante = emailUsuario,
                    TotalDeCuentas = 1,
                    Cliente = "",
                    RfcCliente = "",
                    ConceptoTransferencia = conceptoTransferencia,
                };
                broxelcoRdgEntities.TransferenciasSolicitud.Add(transferenciasSolicitud);
                broxelcoRdgEntities.SaveChanges();
                TransferenciasDetalle transferenciasDetalle = new TransferenciasDetalle
                {

                    ClaveClienteDestino = maquilaDestino.clave_cliente,
                    ClaveClienteOrigen = maquilaOrigen.clave_cliente,
                    CodigoAutorizacion = transferenciaResponse.NumeroAutorizacion,
                    CodigoRespuesta = transferenciaResponse.CodigoRespuesta.ToString(CultureInfo.InvariantCulture),
                    MontoComision = Convert.ToDecimal(transferenciaResponse.Comision.MontoComision),
                    CuentaDestino = maquilaDestino.num_cuenta,
                    CuentaOrigen = maquilaOrigen.num_cuenta,
                    IdMovimiento = transferenciaResponse.IdMovimiento.ToString(CultureInfo.InvariantCulture),
                    IdSolicitud = transferenciasSolicitud.Id,
                    Monto = monto,
                    ProductoDestino = maquilaDestino.producto,
                    ProductoOrigen = maquilaOrigen.producto,
                    SaldoDestinoAntes = transferenciaResponse.SaldoDestinoAntes,
                    SaldoDestinoDespues = transferenciaResponse.SaldoDestinoDespues,
                    SaldoOrigenAntes = transferenciaResponse.SaldoOrigenAntes,
                    SaldoOrigenDespues = transferenciaResponse.SaldoOrigenDespues,
                    TipoComision = transferenciaResponse.Comision.TipoComision,
                    TipoConceptoComision = transferenciaResponse.Comision.TipoConceptoComision,
                    FolioSolicitud = folio,
                };
                broxelcoRdgEntities.TransferenciasDetalle.Add(transferenciasDetalle);
                broxelcoRdgEntities.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                String p = String.Empty;
                foreach (var eve in e.EntityValidationErrors)
                {
                    p += String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    p = eve.ValidationErrors.Aggregate(p,
                        (current, ve) =>
                            current +
                            String.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                }
                var args = ", cuentaOrigen= " + cuentaOrigen + ", monto = " +
                           monto.ToString(CultureInfo.InvariantCulture) + ", tarjetaDestino = " + tarjetaDestino +
                           ", IdUsuario = " + idUsuario.ToString(CultureInfo.InvariantCulture);

                Helper.SendMail("broxelonline@broxel.com", " alberto.ortiz@broxel.com, mauricio.lopez@broxel.com",
                    "Fallo registro de Transferencia en : wsAdmonUsuarios - DBEntity",
                    "DbEntityValidationException en : " + p + e + ". Argumentos: " + args, "67896789");
            }
            catch (Exception e)
            {
                var args = ", cuentaOrigen= " + cuentaOrigen + ", monto = " +
                           monto.ToString(CultureInfo.InvariantCulture) + ", tarjetaDestino = " + tarjetaDestino +
                           ", IdUsuario = " + idUsuario.ToString(CultureInfo.InvariantCulture);

                Helper.SendMail("broxelonline@broxel.com", " alberto.ortiz@broxel.com, mauricio.lopez@broxel.com",
                    "Fallo registro de Transferencia en : wsAdmonUsuarios ", "Error : " + e + ". Argumentos: " + args,
                    "67896789");
            }
            return transferenciaResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cuentaOrigen"></param>
        /// <param name="monto"></param>
        /// <param name="cuentaDestino"></param>
        /// <param name="IdUsuario"></param>
        /// <returns></returns>
        [WebMethod]
        public TransferenciaResponse TransferenciaEntreMisCuentas(String cuentaOrigen, Decimal monto,
            String cuentaDestino, Int32 IdUsuario)
        {
            //MLS idUserSecure
            var idUser = new IdSecureComp().GetIdUserValid(IdUsuario);
            if (idUser == 0)
            {
                return new TransferenciaResponse
                {
                    UserResponse = "Usuario inválido o la sesión expiró"
                };
            }
            IdUsuario = idUser;


            var valida = new MySqlDataAccess();

            if (valida.ValidaProductoMejoravit(cuentaOrigen, cuentaDestino))
            {
                return new TransferenciaResponse
                {
                    UserResponse = "No puedes realizar ó recibir transferencias a tarjetas mejoravit"
                };
            }


            //TODO : Poner botón de bloqueo/act en online Broxel - Movimientos dependiendo de estado cuenta
            //TODO : Quitar email usuario de parametro y obtenerlo de UsuariosOnlineBroxel 
            //TODO : Cambiar OnlineBroxel
            broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
            var usuario = broxelcoRdgEntities.UsuariosOnlineBroxel.FirstOrDefault(x => x.Id == IdUsuario);
            if (usuario == null)
            {
                return new TransferenciaResponse
                {
                    UserResponse = "No se encontró el usuario"
                };
            }

            var listaCuentas =
                broxelcoRdgEntities.accessos_clientes.Where(
                    x => x.IdUsuarioOnlineBroxel == IdUsuario && x.IdMaquila != null).Select(x => x.cuenta).ToList();
            //var listaCuentas = broxelcoRdgEntities.maquila.Where(x => x.usuario_web == IdUsuario).Select(s => s.num_cuenta).ToList();

            if (!listaCuentas.Contains(cuentaOrigen) || !listaCuentas.Contains(cuentaDestino))
            {
                return new TransferenciaResponse
                {
                    UserResponse = "Cuenta especificada no pertenece al usuario"
                };
            }

            String emailUsuario = usuario.CorreoElectronico;

            BroxelService broxelService = new BroxelService();
            TransferenciaResponse transferenciaResponse = new TransferenciaResponse();

            Tarjeta tarDestino = Helper.GetTarjetaFromCuenta(cuentaDestino);
            //Tarjeta tarOrigen = Helper.GetTarjetaFromCuenta(cuentaOrigen);
            maquila maquilaOrigen = broxelcoRdgEntities.maquila.FirstOrDefault(x => x.num_cuenta == cuentaOrigen);


            if (maquilaOrigen == null)
                return new TransferenciaResponse
                {
                    UserResponse = "No se encontro la cuenta origen.",
                };
            if (String.IsNullOrEmpty(tarDestino.Cuenta))
                return new TransferenciaResponse
                {
                    UserResponse = "No se encontro la tarjeta destino.",
                };

            maquila maquilaDestino = broxelcoRdgEntities.maquila.FirstOrDefault(x => x.num_cuenta == tarDestino.Cuenta);

            var query =
                "SELECT COALESCE(substr(max(`folio`),6,6)+1,1,substr(max(`folio`),6,6)+1) AS folio FROM TransferenciasSolicitud WHERE SUBSTRING(`folio`,1,5)='" +
                DateTime.Now.ToString("yyMM") + "T'";
            // ReSharper disable once PossibleNullReferenceException
            var folio = DateTime.Now.ToString("yyMM") + "T" +
                        broxelcoRdgEntities.Database.SqlQuery<String>(query)
                            .FirstOrDefault()
                            .PadLeft(6)
                            .Replace(' ', '0');
            try
            {
                transferenciaResponse = broxelService.TransferenciaDeCuentas(cuentaOrigen, monto,
                    tarDestino.NumeroTarjeta, emailUsuario, "P2P");
                if (transferenciaResponse.CodigoRespuesta == -1)
                {
                    //Obtener el alias de la cuenta destino.
                    var datosCuentaDestino =
                        broxelcoRdgEntities.accessos_clientes.FirstOrDefault(x => x.cuenta == cuentaDestino);
                    var datosCorreo = new DatosEmailTransferencias
                    {
                        Fecha = DateTime.Now,
                        Monto = Convert.ToDouble(monto),
                        Usuario = usuario.NombreCompleto,
                        UsuarioDestino = maquilaDestino.nombre_titular,
                        AliasTarjeta =
                            datosCuentaDestino != null && datosCuentaDestino.Alias != null
                                ? datosCuentaDestino.Alias
                                : "",
                        CorreoUsuario = usuario.CorreoElectronico,
                        NumeroTarjeta = maquilaOrigen.nro_tarjeta.Substring(maquilaOrigen.nro_tarjeta.Length - 4, 4),
                        NumeroTarjetaDestino =
                            maquilaDestino.nro_tarjeta.Substring(maquilaDestino.nro_tarjeta.Length - 4, 4),
                        NumeroAutorizacion = transferenciaResponse.NumeroAutorizacion,
                        Comision = Convert.ToDouble(transferenciaResponse.Comision.MontoComision)
                    };
                    new GenericBL().ComposeTranferenciasMail(datosCorreo, TipoCorreo.MisTarjetas, maquilaOrigen.producto,
                        maquilaDestino.producto, maquilaOrigen.num_cuenta);
                    //new GenericBL().ComposeTranferenciasMail(DateTime.Now, Convert.ToDouble(monto), maquilaDestino.nombre_titular, folio, emailUsuario, maquilaOrigen.producto, maquilaDestino.producto,maquilaOrigen.num_cuenta);
                }

                TransferenciasSolicitud transferenciasSolicitud = new TransferenciasSolicitud
                {
                    ClaveCliente = maquilaOrigen.clave_cliente,
                    FechaHoraAprobacion = DateTime.Now,
                    FechaHoraCreacion = DateTime.Now,
                    FechaHoraVerificacion = DateTime.Now,
                    Folio = folio,
                    Origen = "OnlineApp",
                    Producto = maquilaOrigen.producto,
                    Tipo = "P2P",
                    UsuarioAprobacion = emailUsuario,
                    UsuarioCreacion = emailUsuario,
                    UsuarioEjecucion = emailUsuario,
                    UsuarioVerificacion = emailUsuario,
                    Estado = transferenciaResponse.CodigoRespuesta == -1 ? "COMPLETA" : "CON ERRORES",
                    AreaSolicitante = "CLIENTE",
                    EmailNotificacion = emailUsuario,
                    MontoTotal = transferenciaResponse.SaldoDestinoDespues - transferenciaResponse.SaldoDestinoAntes,
                    Solicitante = emailUsuario,
                    TotalDeCuentas = 1,
                    Cliente = "",
                    RfcCliente = "",
                };
                broxelcoRdgEntities.TransferenciasSolicitud.Add(transferenciasSolicitud);
                broxelcoRdgEntities.SaveChanges();
                TransferenciasDetalle transferenciasDetalle = new TransferenciasDetalle
                {
                    // ReSharper disable once PossibleNullReferenceException
                    ClaveClienteDestino = maquilaDestino.clave_cliente,
                    ClaveClienteOrigen = maquilaOrigen.clave_cliente,
                    CodigoAutorizacion = transferenciaResponse.NumeroAutorizacion,
                    CodigoRespuesta = transferenciaResponse.CodigoRespuesta.ToString(CultureInfo.InvariantCulture),
                    MontoComision = Convert.ToDecimal(transferenciaResponse.Comision.MontoComision),
                    CuentaDestino = maquilaDestino.num_cuenta,
                    CuentaOrigen = maquilaOrigen.num_cuenta,
                    IdMovimiento = transferenciaResponse.IdMovimiento.ToString(CultureInfo.InvariantCulture),
                    IdSolicitud = transferenciasSolicitud.Id,
                    Monto = monto,
                    ProductoDestino = maquilaDestino.producto,
                    ProductoOrigen = maquilaOrigen.producto,
                    SaldoDestinoAntes = transferenciaResponse.SaldoDestinoAntes,
                    SaldoDestinoDespues = transferenciaResponse.SaldoDestinoDespues,
                    SaldoOrigenAntes = transferenciaResponse.SaldoOrigenAntes,
                    SaldoOrigenDespues = transferenciaResponse.SaldoOrigenDespues,
                    TipoComision = transferenciaResponse.Comision.TipoComision,
                    TipoConceptoComision = transferenciaResponse.Comision.TipoConceptoComision,
                    FolioSolicitud = folio,
                };
                broxelcoRdgEntities.TransferenciasDetalle.Add(transferenciasDetalle);
                broxelcoRdgEntities.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                String p = String.Empty;
                foreach (var eve in e.EntityValidationErrors)
                {
                    p += String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    p = eve.ValidationErrors.Aggregate(p,
                        (current, ve) =>
                            current +
                            String.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                }
                var args = ", cuentaOrigen= " + cuentaOrigen + ", monto = " +
                           monto.ToString(CultureInfo.InvariantCulture) + ", cuentaDestino = " + cuentaDestino +
                           ", IdUsuario = " + IdUsuario.ToString(CultureInfo.InvariantCulture);

                Helper.SendMail("broxelonline@broxel.com", " alberto.ortiz@broxel.com, mauricio.lopez@broxel.com",
                    "Fallo registro de Transferencia en : wsAdmonUsuarios - DBEntity",
                    "DbEntityValidationException en : " + p + e + ". Argumentos: " + args, "67896789");
            }
            catch (Exception e)
            {
                var args = ", cuentaOrigen= " + cuentaOrigen + ", monto = " +
                           monto.ToString(CultureInfo.InvariantCulture) + ", cuentaDestino = " + cuentaDestino +
                           ", IdUsuario = " + IdUsuario.ToString(CultureInfo.InvariantCulture);

                Helper.SendMail("broxelonline@broxel.com", " alberto.ortiz@broxel.com, mauricio.lopez@broxel.com",
                    "Fallo registro de Transferencia en : wsAdmonUsuarios ", "Error : " + e + ". Argumentos: " + args,
                    "67896789");
            }
            return transferenciaResponse;
        }

        #endregion

        #region Renominacion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="originaciones"></param>
        /// <param name="ejecuta"></param>
        /// <returns></returns>
        [WebMethod]
        public string RenominacionOri(List<OriginacionData> originaciones, bool ejecuta)
        {
            return new RenominaBL().InsertaRenominacionConOriginacion(originaciones, ejecuta);
        }

        /// <summary>
        /// Método utilizado para realizar una renominación externa.
        /// </summary>
        /// <param name="renominacion">Datos necesarios para realizar la renominación externa.</param>
        /// <returns></returns>
        [WebMethod]
        public RenominacionExternaResponse RenominacionExterna(RenominacionExternaData renominacion)
        {
            var res = new RenominacionExternaResponse();
            var valNumCuenta = true;
            try
            {
                maquila maq = null;
                if (!string.IsNullOrEmpty(renominacion.NumeroCuenta))
                {
                    maq = GetMaquila(renominacion.NumeroCuenta);
                    if (maq == null && !string.IsNullOrEmpty(renominacion.CampoUnivoco))
                    {
                        maq = GetMaquilaPorCampoUnivoco(renominacion.CampoUnivoco);
                        renominacion.NumeroCuenta = maq.num_cuenta;
                        valNumCuenta = false;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(renominacion.CampoUnivoco))
                    {
                        maq = GetMaquilaPorCampoUnivoco(renominacion.CampoUnivoco);
                        renominacion.NumeroCuenta = maq.num_cuenta;
                        valNumCuenta = false;
                    }
                }
                if (maq != null) res = new RenominaBL().InsertaRenominacionExterna(renominacion, maq.clave_cliente, maq.producto, maq.nro_tarjeta, valNumCuenta);
                else
                {
                    res.Respuesta = 1;
                    res.Descripcion = "No se encontraron registros con los datos proporcionados. Verifique Campo Univoco.";
                }
            }
            catch (Exception e)
            {
                Helper.SendMail("broxelonline@broxel.com", "mauricio.lopez@broxel.com",
                    "Error al realizar la Renominación Externa.", e.Message, "67896789", "Broxel Fintech");
                Trace.WriteLine(DateTime.Now.ToString("O") + " Error al realizar la renominación externa. -> " + e);
                res.Respuesta = 1;
                res.Descripcion = "Error al intentar Renominar.";
            }
            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idCreaClienteSinTarjeta"></param>
        [WebMethod]
        public void RenominacionVCardBatch(int idCreaClienteSinTarjeta)
        {
            using (var ctx = new broxelco_rdgEntities())
            {


                var creas = ctx.CreaClienteSinTarjetaLog.Where(c => c.cuenta != "UNDEFINED").ToList();
                foreach (var crea in creas)
                {
                    try
                    {

                        var maq = ctx.maquila.FirstOrDefault(m => m.num_cuenta == crea.cuenta);
                        if (maq == null)
                            throw new Exception("No existen datos de maquila para la cuenta " + crea.cuenta);
                        var idUser = crea.idUsuarioOnlineBroxel ?? 0;
                        var idApp = crea.idApp ?? 0;
                        new RenominaBL().InsertaRenominacionVCard(idUser, idApp, maq.producto,
                            maq.nro_tarjeta.Substring(maq.nro_tarjeta.Length - 4, 4));
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine("Error en RenominacionVCardBatch: " + e);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idCreaClienteSinTarjeta"></param>
        /// <returns></returns>
        [WebMethod]
        public bool RenominacionVCardbyIdCreaClienteSinTarjeta(int idCreaClienteSinTarjeta)
        {
            using (var ctx = new broxelco_rdgEntities())
            {
                try
                {
                    Trace.WriteLine("RenominacionVCardbyIdCreaClienteSinTarjeta: " + idCreaClienteSinTarjeta);
                    var creas = ctx.CreaClienteSinTarjetaLog.FirstOrDefault(c => c.id == idCreaClienteSinTarjeta);

                    Trace.WriteLine("RenominacionVCardbyIdCreaClienteSinTarjeta numCuenta: " + creas.cuenta);
                    var maq = ctx.maquila.FirstOrDefault(m => m.num_cuenta == creas.cuenta);
                    if (maq == null)
                        throw new Exception("No existen datos de maquila para la cuenta " + creas.cuenta);
                    var idUser = creas.idUsuarioOnlineBroxel ?? 0;
                    var idApp = creas.idApp ?? 0;
                    Trace.WriteLine("RenominacionVCardbyIdCreaClienteSinTarjeta iduser: " + idUser);
                    Trace.WriteLine("RenominacionVCardbyIdCreaClienteSinTarjeta idApp: " + idApp);

                    Trace.WriteLine("RenominacionVCardbyIdCreaClienteSinTarjeta empieza la task: ");
                    new RenominaBL().InsertaRenominacionVCard(idUser, idApp, maq.producto,
                        maq.nro_tarjeta.Substring(maq.nro_tarjeta.Length - 4, 4));
                    //respuesta.Success = true;
                }
                catch (Exception e)
                {
                    Trace.WriteLine("Error en RenominacionVCardBatch: " + e);
                    //respuesta.Success = false;
                    return false;
                }

            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idOnlineBroxel"></param>
        /// <param name="nombreCompleto"></param>
        /// <param name="genero"></param>
        /// <returns></returns>
        [WebMethod]
        public OnlineBroxelResponse RenominacionOnlineBroxel(int idOnlineBroxel, string nombreCompleto, string genero)
        {
            var updateOnlineBroxel = new UpdateOnlineBroxel();

            return updateOnlineBroxel.UpdateInfoOnlineBroxel(idOnlineBroxel, nombreCompleto, genero);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUsuarioOnline"></param>
        /// <param name="idMaquila"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        [WebMethod]
        public bool RenominarByIdMaquila(int idUsuarioOnline, int idMaquila, string username)
        {
            BroxelCards bc = new BroxelCards();
            return bc.RenominacionbyIdUsuarioOnline(idUsuarioOnline, idMaquila, username);
        }
        #endregion

        #region Token - Seed
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="celular"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [WebMethod]
        public bool ValidaCelularToken(int idUser, string celular, string token)
        {
            //MLS idUserSecure
            var id = new IdSecureComp().GetIdUserValid(idUser);
            if (id == 0)
            {
                return false;
            }
            idUser = id;
            return new VCardBL().ValidaCelularToken(idUser, celular, token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="celular"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [WebMethod]
        public VcSeedData ValidaCelularTokenOtp(int idUser, string celular, string token)
        {
            //MLS idUserSecure
            var id = new IdSecureComp().GetIdUserValid(idUser);
            if (id == 0)
            {
                return new VcSeedData
                {
                    Seed = string.Empty,
                    Status = false,
                    DescStatus = "No exite el usuario con id: " + idUser + "."
                };
            }
            idUser = id;
            return new VCardBL().ValidaCelularTokenOtp(idUser, celular, token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="idApp"></param>
        /// <param name="token1"></param>
        /// <param name="token2"></param>
        /// <returns></returns>
        [WebMethod]
        public bool RecalibrarToken(int idUser, int idApp, string token1, string token2)
        {
            //MLS idUserSecure
            var id = new IdSecureComp().GetIdUserValid(idUser);
            if (id == 0)
            {
                return false;
            }
            idUser = id;

            return new VCardBL().RecalibrarToken(idUser, idApp, token1, token2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="idApp"></param>
        /// <param name="celular"></param>
        /// <returns></returns>
        [WebMethod]
        public bool ReenviarSeed(int idUser, int idApp, string celular = "")
        {
            //MLS idUserSecure
            var id = new IdSecureComp().GetIdUserValid(idUser);
            if (id == 0)
            {
                return false;
            }
            idUser = id;
            return new VCardBL().ReenviaSeedSMS(idUser, idApp, celular);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="idApp"></param>
        /// <param name="celular"></param>
        /// <returns></returns>
        [WebMethod]
        public VcSeedData ReenviarSeedOtp(int idUser, int idApp, string celular = "")
        {
            //MLS idUserSecure
            var id = new IdSecureComp().GetIdUserValid(idUser);
            if (id == 0)
            {
                return new VcSeedData
                {
                    Seed = string.Empty,
                    Status = false,
                    DescStatus = "No exite un usuario con el id: " + idUser + "."
                };
            }
            idUser = id;
            return new VCardBL().ReenviaSeedSMSOtp(idUser, idApp, celular);
        }
        #endregion

        #region Configuraciones y Consultas
        /// <summary>
        /// Se obtienen el cargo de las comisiones de red de pago.
        /// </summary>
        /// <returns>Todas los cargos de comisión disponibles.</returns>
        [WebMethod]
        public ComisionRedPagosResult GetComisionesRedPagos()
        {
            var comisiones = new ComisionRedPagosResult();

            var comisionMysql = new MySqlDataAccess().ObtenerComisionesRedPagos();

            if (comisionMysql != null)
            {
                comisiones.Success = true;
                comisiones.ComisionesRedPagos = comisionMysql;
                comisiones.UserResponse = "sin errores.";
            }
            else
            {
                comisiones.Success = false;
                comisiones.ComisionesRedPagos = new List<ComisionRedPagos>();
            }

            comisiones.Fecha = DateTime.Now.ToString("dd/MM/yyyy");

            return comisiones;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grupoDeLiquidacion"></param>
        /// <returns></returns>
        [WebMethod]
        public List<fechas_corte_x_grupos> GetFechasCortePorGrupo(String grupoDeLiquidacion)
        {
            broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
            return broxelcoRdgEntities.fechas_corte_x_grupos.Where(x => x.grupo == grupoDeLiquidacion).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grupoDeLiquidacion"></param>
        /// <param name="fechaUltimaLiquidacion"></param>
        /// <returns></returns>
        [WebMethod]
        public Boolean TimbrarEdosCuenta(string grupoDeLiquidacion, DateTime fechaUltimaLiquidacion)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                Helper.TimbrarEdosCuenta(grupoDeLiquidacion, fechaUltimaLiquidacion);
            });
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        [WebMethod]
        public List<GruposPreAut> GetGruposPreAutorizador(int idUsuario)
        {
            //MLS idUserSecure
            var idUser = new IdSecureComp().GetIdUserValid(idUsuario);
            if (idUser == 0)
            {
                return new List<GruposPreAut>();
            }
            return new PreAutorizadorBL().GetGruposPreAut();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="cuenta"></param>
        /// <param name="key"></param>
        /// <param name="reglas"></param>
        /// <returns></returns>
        [WebMethod]
        public PreAutResponse AltaBajaCuentaGrupo(int idUsuario, string cuenta, string key, List<Reglas> reglas)
        {
            //MLS idUserSecure
            var idUser = new IdSecureComp().GetIdUserValid(idUsuario);
            if (idUser == 0)
            {
                return new PreAutResponse
                {
                    IdTransaccion = 0,
                    Message = "Usuario inválido o su sesión expiró"
                };
            }
            return new PreAutorizadorBL().AltaBajaCuentaGrupo(idUser, cuenta, key, reglas);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        [WebMethod]
        public bool ActualizaRelacion(string arg)
        {
            return new GenericBL().ActualizaContrasenia(arg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="originaciones"></param>
        /// <param name="estado"></param>
        /// <param name="sinAtm"></param>
        /// <param name="ejecuta"></param>
        /// <returns></returns>
        [WebMethod]
        public string DispersionOri(List<OriginacionData> originaciones, string estado, bool sinAtm = false, bool ejecuta = false)
        {
            return new AsignaLineaBL().InsertaDispersionConOriginacion(originaciones, estado, sinAtm, ejecuta);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="password"></param>
        /// <param name="cuenta"></param>
        /// <returns></returns>
        [WebMethod]
        public InfoCard GetInfoCard(string usuario, string password, string cuenta)
        {
            try
            {
                using (var ctx = new Broxel_MejoravitEntities())
                {
                    var userData = ctx.UsuariosInfonavit.FirstOrDefault(x => x.idUsuario == usuario && x.status == 1);
                    if (userData == null)
                        return new InfoCard { Resultado = false, Mensaje = "Usuario o password invalidos" };
                    var passdec = AesEcbPKCS5Encrypter.Decrypt(password, usuario);
                    var passData = passdec.Split('|');
                    if (passData[0] != AesEcbPKCS5Encrypter.Decrypt(userData.password, usuario))
                        return new InfoCard { Resultado = false, Mensaje = "Usuario o password invalidos" };
                    var tc = "";
                    if (userData.environment == "PROD")
                    {
                        var tcInfo = Helper.GetTarjetaFromCuenta(cuenta);
                        if (tcInfo == null)
                            return new InfoCard { Resultado = false, Mensaje = "Cuenta Inexistente" };
                        tc = tcInfo.NumeroTarjeta;
                    }
                    else
                    {
                        tc = "522339" + new Random().Next(999999).ToString("D6", CultureInfo.InvariantCulture) +
                             new Random().Next(9999).ToString("D4", CultureInfo.InvariantCulture);
                    }

                    var jsRet = "{\"tarjeta\":\"" + tc + "\", \"Random\":" +
                                new Random().Next(999999).ToString("D6", CultureInfo.InvariantCulture) + "}";
                    return new InfoCard { Resultado = true, Mensaje = AesEcbPKCS5Encrypter.Encrypt(jsRet, cuenta) };
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine("GetInfoCard: Error: " + e);
                return new InfoCard { Resultado = false, Mensaje = "Error al consultar: " + e.Message };
            }
        }

        /// <summary>
        /// Obtiene el Json para la configuración de parametrizaciones.
        /// </summary>
        /// <param name="numCuenta"></param>
        /// <returns></returns>
        [WebMethod]
        public string ObtenerJsonConfiguracion(string numCuenta)
        {
            var jsonData = "{";
            var broxelcoRdgEntities = new broxelco_rdgEntities();
            var maq = broxelcoRdgEntities.maquila.FirstOrDefault(x => x.num_cuenta == numCuenta);
            if (maq != null)
            {
                var clavecliente = maq.clave_cliente;
                var producto = maq.producto;
                var broxelCommonEntities = new BroxelCommonEntities();
                // Busca la configuración con clave de cliente exacta.
                var funcionalidades = from catFuncionalidades in broxelCommonEntities.CatFuncionalidades
                    join catFuncXInfoClientes in broxelCommonEntities.CatFuncionalidadesXInfoClientes on catFuncionalidades.IdCatFuncionalidades equals catFuncXInfoClientes.IdFuncionalidad
                    join infoClientes in broxelCommonEntities.InfoClientes on catFuncXInfoClientes.IdInfoCuentaCliente equals infoClientes.IdInfoClientes
                    where infoClientes.ClaveCliente == clavecliente && infoClientes.Producto == producto
                   select new { IdFuncionalidad = catFuncionalidades.IdCatFuncionalidades, nombreFuncionalidad = catFuncionalidades.NombreFuncionalidad };
                // Busca la configuración con clave de cliente genèrica(contains).
                if (funcionalidades == null || funcionalidades.ToList().Count == 0)
                {
                    clavecliente = Regex.Replace(clavecliente, @"[\d-]", string.Empty);
                    funcionalidades = from catFuncionalidades in broxelCommonEntities.CatFuncionalidades
                        join catFuncXInfoClientes in broxelCommonEntities.CatFuncionalidadesXInfoClientes on catFuncionalidades.IdCatFuncionalidades equals catFuncXInfoClientes.IdFuncionalidad
                        join infoClientes in broxelCommonEntities.InfoClientes on catFuncXInfoClientes.IdInfoCuentaCliente equals infoClientes.IdInfoClientes
                        where infoClientes.ClaveCliente == clavecliente && infoClientes.Producto == producto
                        select new { IdFuncionalidad = catFuncionalidades.IdCatFuncionalidades, nombreFuncionalidad = catFuncionalidades.NombreFuncionalidad };
                }
                // Busca la configuracón de cliente por default
                if (funcionalidades == null ||funcionalidades.ToList().Count == 0)
                {
                    funcionalidades =
                    from catFuncionalidades in broxelCommonEntities.CatFuncionalidades
                    join catFuncXInfoClientes in broxelCommonEntities.CatFuncionalidadesXInfoClientes on catFuncionalidades.IdCatFuncionalidades equals catFuncXInfoClientes.IdFuncionalidad
                    join infoClientes in broxelCommonEntities.InfoClientes on catFuncXInfoClientes.IdInfoCuentaCliente equals infoClientes.IdInfoClientes
                    where infoClientes.ClaveCliente == "NA" && infoClientes.Producto == "NA"
                    select new { IdFuncionalidad = catFuncionalidades.IdCatFuncionalidades, nombreFuncionalidad = catFuncionalidades.NombreFuncionalidad };
                }
                jsonData = Enumerable.Aggregate(funcionalidades, jsonData, (current, funcionalidad) => current + ("\"" + funcionalidad.nombreFuncionalidad + "\": 0,"));
            }
            else
            {
                var clavecliente = "INTBRX";
                var producto = "BCE0";
                var broxelCommonEntities = new BroxelCommonEntities();
                var funcionalidades =
                    from catFuncionalidades in broxelCommonEntities.CatFuncionalidades
                    join catFuncXInfoClientes in broxelCommonEntities.CatFuncionalidadesXInfoClientes on catFuncionalidades.IdCatFuncionalidades equals catFuncXInfoClientes.IdFuncionalidad
                    join infoClientes in broxelCommonEntities.InfoClientes on catFuncXInfoClientes.IdInfoCuentaCliente equals infoClientes.IdInfoClientes
                    where clavecliente.Contains(infoClientes.ClaveCliente) && infoClientes.Producto == producto
                    select new { IdFuncionalidad = catFuncionalidades.IdCatFuncionalidades, nombreFuncionalidad = catFuncionalidades.NombreFuncionalidad };

                jsonData = Enumerable.Aggregate(funcionalidades, jsonData, (current, funcionalidad) => current + ("\"" + funcionalidad.nombreFuncionalidad + "\": 0,"));
            }

            if (jsonData.Length == 1)
                return jsonData + "}";
            return jsonData.Substring(0, jsonData.Length - 1) + "}";
        }

        /// <summary>
        /// Establece una cuenta como funcionalidad PTP(Peer to peer)
        /// </summary>
        /// <param name="numeroCuenta">numero de cuenta</param>
        /// <param name="idSecure">id de usuario broxel</param>
        /// <returns></returns>
        [WebMethod]
        public bool EstablecerPTP(string numeroCuenta, int idSecure)
        {
            try
            {
                var idUsuarioBroxel = new IdSecureComp().GetIdUserValid(idSecure);
                bool respuesta = false;
                Trace.WriteLine("EstablecerPTP -> idSecure recibido en el método: " + idSecure + " idUsuarioBroxel generado: " + idUsuarioBroxel);

                if (idUsuarioBroxel == 0)
                {
                    Trace.WriteLine("EstablecerPTP -> idUsuarioBroxel generado del componente: " + idUsuarioBroxel);
                    respuesta = false;
                }
                else
                {
                    using (var context = new broxelco_rdgEntities())
                    {
                        var existeCuenta = context.accessos_clientes.Where(x => x.IdUsuarioOnlineBroxel == idUsuarioBroxel && x.cuenta == numeroCuenta).FirstOrDefault();
                        if (existeCuenta == null)
                        {
                            Trace.WriteLine("EstablecerPTP -> existeCuenta es null, para el usuarioBroxel: " + idUsuarioBroxel + "con numeroCuenta: " + numeroCuenta);
                            respuesta = false;
                        }
                        else
                        {
                            var cuentas = context.accessos_clientes.Where(x => x.IdUsuarioOnlineBroxel == idUsuarioBroxel).ToList();
                            foreach (var cuenta in cuentas)
                            {
                                if (cuenta.cuenta == numeroCuenta)
                                {
                                    cuenta.P2P_Activo = true;
                                    context.Entry(cuenta).State = EntityState.Modified;
                                    context.SaveChanges();
                                }
                                else
                                {
                                    if (cuenta.P2P_Activo)
                                    {
                                        cuenta.P2P_Activo = false;
                                        context.Entry(cuenta).State = EntityState.Modified;
                                        context.SaveChanges();
                                    }
                                }
                            }
                            respuesta = true;
                        }
                    }
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("No se pudo establecer la cuenta: " + numeroCuenta + " con el idSecure " + idSecure + "como P2P.  ERROR -->: " + ex.Message);
                return false;
            }
        }
        #endregion

        #region Family
        /// <summary>
        /// Asocia una o varias tarjetas a una cuenta padre.
        /// </summary>
        /// <param name="idApp">id aplicación</param>
        /// <param name="codigoProducto">código de producto</param>
        [WebMethod]
        public MaquilaVcInfo GetMaquilFromPullFamily(int idApp, string codigoProducto)
        {
            var tarjetaSolicitada = new MaquilaVcInfo();
            try
            {
                var vcBl = new VCardBL();
                tarjetaSolicitada = vcBl.GetMaquilaAonFromPull(idApp, codigoProducto);

            }
            catch (Exception error)
            {
                Trace.WriteLine("ERROR  -> GetMaquilFromPullFamily:   " + error.Message);
                Helper.SendMail("broxelonline@broxel.com", "luis.huerta@broxel.com", "ERROR -> GetTarjetasMaquilaFromPull_Family", error.Message, "67896789");
            }
            return tarjetaSolicitada;
        }

        /// <summary>
        /// Crea y envia el Archivo para MyCard.
        /// </summary>
        /// <param name="parametros">Datos proporcionados para realizar las consultas.</param>
        /// <param name="dirEnvioTarjeta">Datos de la dirección a donde se enviaran las tarjetas.</param>
        /// <param name="datos">Datos adicionales para las tajetas.</param>
        [WebMethod]
        public bool CrearArchivoMyCard(List<ParametrosFamily> parametros, DireccionEnvioTarjetaFisica dirEnvioTarjeta, DatosAdicionales datos)
        {
            Trace.WriteLine(DateTime.Now.ToString("O") + " Dentro de CrearArchivoMyCard.");
            bool res;
            try
            {
                if (parametros != null && parametros.Any())
                {
                    if (dirEnvioTarjeta != null)
                    {
                        if (datos != null)
                            res = new GenericBL().CrearArchivoMyCard(parametros, dirEnvioTarjeta, datos);
                        else
                            throw new Exception("No se recibieron los datos adicionales de la cuenta padre.");
                    }
                    else
                        throw new Exception("No se recibieron los datos de la dirección de envio de las tarjetas");
                }
                else
                    throw new Exception("No se recibieron parametros para crear archivo MyCard");
            }
            catch (Exception e)
            {
                Helper.SendMail("broxelonline@broxel.com", "omar.vidal@broxel.com",
                    "Error al generar el archivo de MyCard.", e.Message, "67896789", "Broxel Fintech");
                Trace.WriteLine(DateTime.Now.ToString("O") + " Error al generar el archivo de MyCard -> " + e);
                res = false;
            }

            return res;
        }

        /// <summary>
        /// Obtiene los datos de la tarjeta validos para LN.
        /// </summary>
        /// <param name="idSecure"></param>
        /// <param name="numCuenta"></param>
        /// <returns></returns>
        [WebMethod]
        public Tarjeta ObtenerTarjetaValidaLN(int idSecure, string numCuenta)
        {
            //MLS idUserSecure
            var idUser = new IdSecureComp().GetIdUserValid(idSecure);
            if (idUser == 0) return new Tarjeta();

            using (var ctx = new broxelco_rdgEntities())
            {
                var family = new BroxelFamily();
                var esHija = family.EsHijoDeLaCuentaPadreFamily(idUser, numCuenta);

                var accesso =
                    ctx.accessos_clientes.FirstOrDefault(a => a.IdUsuarioOnlineBroxel == idUser && a.cuenta == numCuenta);
                var maq = ctx.maquila.FirstOrDefault(y => y.num_cuenta == numCuenta);

                if (accesso == null || esHija || maq == null)
                    return new Tarjeta();
            }
            return Helper.GetTarjetaFromCuenta(numCuenta) ?? new Tarjeta();
        }
        #endregion

        #endregion

        #region Private Methods

        private UsuarioOnlineResponse RegistrarUsuarioBL(string nombreCompleto, string rfc, string telefono,
            string sexo, string codigoPostal, string usuario, string correoElectronico, string celular, DateTime fechaNacimiento,
            string contrasenia, string numTarjeta, string fechaExpiracion, string palabraClave = "", string host = "online.broxel.com")
        {
            usuario = usuario.Trim();
            int idUsuario = -1;
            int idMaquila = -1;
            if (celular.Length >= 13)
                return new UsuarioOnlineResponse
                {
                    Success = false,
                    UserResponse = "Longitud de celular incorrecta. Favor de ingresar 10 dígitos."
                };
            /* TODO MLS Probar este cambio antes de enviar a produccion
            if (!new RegexUtilities().IsValidEmail(usuario))
            {
                return new UsuarioOnlineResponse
                {
                    Success = false,
                    UserResponse = "El usuario no es un correo electronico válido, favor de verificar."
                };
            }
            */

            broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
            try
            {
                UsuariosOnlineBroxel user = new UsuariosOnlineBroxel
                {
                    NombreCompleto = nombreCompleto,
                    RFC = rfc,
                    Sexo = sexo,
                    Telefono = telefono,
                    CP = codigoPostal,
                    Usuario = usuario,
                    CorreoElectronico = correoElectronico,
                    Celular = celular,
                    FechaNacimiento = Convert.ToDateTime(fechaNacimiento),
                    Password = Helper.Cifrar(contrasenia),
                    FechaCreacion = DateTime.Now,
                    palabraClave = palabraClave
                };

                var maq = GetMaquilaPorTarjetaYFecha(numTarjeta, fechaExpiracion);

                if (maq == null)
                {
                    BitacoraUsuariosOnlineBroxel("Registro", "NoExiste " + numTarjeta, usuario, host);
                    return new UsuarioOnlineResponse
                    {
                        Success = false,
                        UserResponse = "No se encontró la tarjeta con los datos ingresados. Favor de intentarlo nuevamente."
                    };
                }

                if (palabraClave != ConfigurationManager.AppSettings["MejoravitBatchId"])
                {
                    var perc = new MySqlDataAccess().MontoPorcentaje(maq.num_cuenta);
                    if (perc > 0)
                    {
                        BitacoraUsuariosOnlineBroxel("RegistroFallido", "Mejoravit - NoAplica", usuario, host);
                        return new UsuarioOnlineResponse { Success = false, UserResponse = "No es posible registrarse con esta tarjeta." };
                    }
                }

                var accesoCliente = broxelcoRdgEntities.accessos_clientes.FirstOrDefault(x => x.IdMaquila == maq.id);
                if (accesoCliente != null)
                {
                    BitacoraUsuariosOnlineBroxel("RegistroFallido", "TarjetaLigada", usuario, host);
                    return new UsuarioOnlineResponse { Success = false, UserResponse = "Esta tarjeta actualmente se encuentra ligada a otra cuenta. " };
                }
                if (maq.producto == "K153")
                {
                    BitacoraUsuariosOnlineBroxel("RegistroFallido", "K153 - NoAplica", usuario, host);
                    return new UsuarioOnlineResponse { Success = false, UserResponse = "No es posible registrarse con esta tarjeta." };
                }
                if (broxelcoRdgEntities.UsuariosOnlineBroxel.Count(x => x.Usuario == usuario) > 0)
                {
                    BitacoraUsuariosOnlineBroxel("RegistroFallido", numTarjeta, usuario, host);
                    return new UsuarioOnlineResponse { Success = false, UserResponse = "Este usuario ya esta registrado." };
                }
                if (broxelcoRdgEntities.UsuariosOnlineBroxel.Count(x => x.CorreoElectronico == correoElectronico) > 0)
                {
                    BitacoraUsuariosOnlineBroxel("RegistroFallido", numTarjeta, usuario, host);
                    return new UsuarioOnlineResponse { Success = false, UserResponse = "El correo ya existe, favor de ingresar con sus credenciales y agregar tarjeta a su cuenta." };
                }

                try
                {
                    accesoCliente = new accessos_clientes();
                    maquila maq1 = broxelcoRdgEntities.maquila.FirstOrDefault(x => x.num_cuenta == maq.num_cuenta);
                    if (maq1 != null)
                    {
                        broxelcoRdgEntities.UsuariosOnlineBroxel.Add(user);
                        broxelcoRdgEntities.SaveChanges();
                        idUsuario = user.Id;
                        idMaquila = maq1.id;
                        // TODO Checar si esto tambien debe actualizar alias
                        if (ConfigurationManager.AppSettings["B2cCards"].Contains(maq.producto))
                        {
                            var tc = Helper.GetTarjetaFromCuenta(maq1.num_cuenta);
                            var res = AgregarTarjetaBL(idUsuario, tc.NumeroTarjeta, tc.FechaExpira, idUsuario);

                            if (res.Success)
                                return Login(usuario, contrasenia);
                            else
                                return new UsuarioOnlineResponse { Success = false, UserResponse = "Ocurrio un error al crear el usuario. Intente nuevamente." };
                        }
                        else
                        {
                            AgregaOActualizaRelacion(celular, maq1.num_cuenta, usuario, nombreCompleto, idUsuario, correoElectronico, idMaquila, "");
                        }
                        try
                        {
                            broxelcoRdgEntities.SaveChanges();
                            BitacoraUsuariosOnlineBroxel("RegistroOK", accesoCliente.cuenta, usuario, host);
                            var tcMask = numTarjeta.Substring(6) + "** ****" +
                                         numTarjeta.Substring(numTarjeta.Length - 4);
                            Mailing.EnviaCorreoBienvenida(nombreCompleto, tcMask, usuario, correoElectronico, host, maq.producto); broxelcoRdgEntities.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            Helper.SendMail("broxelonline@broxel.com", " alberto.ortiz@broxel.com, mauricio.lopez@broxel.com", "Fallo registro de usuario ligando maquila y user id " + correoElectronico, "Error : " + ex, "67896789");
                            BorrarUsuario(idUsuario, idMaquila);
                            return new UsuarioOnlineResponse { Success = false, UserResponse = "Ocurrio un error al crear el usuario. Intente nuevamente." };
                        }
                    }
                    return Login(usuario, contrasenia);

                }
                catch (DbEntityValidationException e)
                {
                    String p = String.Empty;

                    foreach (var eve in e.EntityValidationErrors)
                    {
                        p += String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        p = eve.ValidationErrors.Aggregate(p, (current, ve) => current + String.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                    BorrarUsuario(idUsuario, idMaquila);
                    Helper.SendMail("broxelonline@broxel.com", " alberto.ortiz@broxel.com, mauricio.lopez@broxel.com", "Fallo registro de usuario DBEntity", "DbEntityValidationException Error en usuario " + p + e, "67896789");
                }
                catch (Exception e)
                {
                    Helper.SendMail("broxelonline@broxel.com", " alberto.ortiz@broxel.com, mauricio.lopez@broxel.com", "Fallo registro de usuario mail " + correoElectronico, "Error : " + e, "67896789");
                    BorrarUsuario(idUsuario, idMaquila);
                    return new UsuarioOnlineResponse { Success = false, UserResponse = "Ocurrió un error al crear el usuario. Intente nuevamente." };
                }
            }
            catch (Exception e)
            {
                Helper.SendMail("broxelonline@broxel.com", " alberto.ortiz@broxel.com, mauricio.lopez@broxel.com", "Fallo registro de usuario catch all mail " + correoElectronico, "Error : " + e, "67896789");
                BorrarUsuario(idUsuario, idMaquila);
                return new UsuarioOnlineResponse { Success = false, UserResponse = "Ocurrio un error al crear el usuario. Intente nuevamente." };
            }
            return new UsuarioOnlineResponse();
        }
        private void BitacoraUsuariosOnlineBroxel(string accion, string extra, string usuario, string sitio)
        {
            try
            {
                broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
                broxelcoRdgEntities.LogUsuariosOnlineBroxel.Add(new LogUsuariosOnlineBroxel
                {
                    Accion = accion,
                    Extra = extra,
                    FechaHora = DateTime.Now,
                    Usuario = usuario,
                    Sitio = sitio
                });
                broxelcoRdgEntities.SaveChanges();
            }
            catch (Exception e)
            {
                try
                {
                    Helper.SendMail("broxelonline@broxel.com", " alberto.ortiz@broxel.com, mauricio.lopez@broxel.com", "Fallo registro de bitacora en wsAdmonUSuarios ", "Error : " + e, "67896789");
                }
                catch (Exception)
                {
                    // ReSharper disable once UnusedVariable.Compiler
                    bool doNothing = true;
                }
            }
        }

        private void BorrarUsuario(int idUsuario, int idMaquila)
        {
            if (idUsuario < 0) return;
            try
            {
                broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
                var query = "delete from UsuariosOnlineBroxel where Id = " + idUsuario;
                broxelcoRdgEntities.Database.ExecuteSqlCommand(query);
                query = "delete from accessos_clientes where idUsuarioOnlineBroxel = " + idUsuario;
                broxelcoRdgEntities.Database.ExecuteSqlCommand(query);
            }
            catch (Exception e)
            {
                Helper.SendMail("broxelonline@broxel.com", " alberto.ortiz@broxel.com, mauricio.lopez@broxel.com", "Fallo al cancelar registro de usuario", "Error al borrar usuario con id " + idUsuario + ". Favor de verificar." + e, "67896789");
            }
        }

        private void EliminaRelacion(int idMaquila)
        {
            var broxelcoRdgEntities = new broxelco_rdgEntities();
            var cuentas = broxelcoRdgEntities.accessos_clientes.Where(x => x.IdMaquila == idMaquila);
            foreach (var cuenta in cuentas)
            {
                broxelcoRdgEntities.accessos_clientes.Remove(cuenta);
                broxelcoRdgEntities.Entry(cuenta).State = EntityState.Deleted;

            }
            broxelcoRdgEntities.SaveChanges();

        }

        private void AgregaOActualizaRelacion(string celular, string pcuenta, string usuario, string nombreCompleto, int idUsuarioOnlineBroxel, string email, int idMaquila, string alias)
        {
            broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
            var cuentas = broxelcoRdgEntities.accessos_clientes.Where(x => x.IdMaquila == idMaquila).ToList();
            if (cuentas.Count > 0)
            {
                foreach (var cuenta in cuentas)
                {
                    cuenta.usuario = usuario;
                    cuenta.nombre_completo = nombreCompleto;
                    cuenta.IdUsuarioOnlineBroxel = idUsuarioOnlineBroxel;
                    cuenta.celular = celular;
                    cuenta.Email = email;
                    cuenta.cuenta = pcuenta;
                    cuenta.IdMaquila = idMaquila;
                    cuenta.Alias = alias;
                    cuenta.IdProcesador = 1;
                }
            }
            else
                broxelcoRdgEntities.accessos_clientes.Add(new accessos_clientes
                {
                    usuario = usuario,
                    nombre_completo = nombreCompleto,
                    IdUsuarioOnlineBroxel = idUsuarioOnlineBroxel,
                    celular = celular,
                    Email = email,
                    IdMaquila = idMaquila,
                    cuenta = pcuenta,
                    ActivoCuenta = false,
                    FechaHoraCreacion = DateTime.Now,
                    Alias = alias,
                    IdProcesador = 1
                });
            broxelcoRdgEntities.SaveChanges();
        }

        private UsuarioOnlineResponse AgregarTarjetaBL(int id, string numTarjeta, string fechaExpiracion, int idUser, string alias = "", string identifier = "", string host = "")
        {
            // MLS Clientes que no agregan una fisica.
            var vCard = new VCardBL();
            var broxelcoRdgEntities = new broxelco_rdgEntities();

            var usuario = broxelcoRdgEntities.UsuariosOnlineBroxel.FirstOrDefault(x => x.Id == id);
            if (usuario != null)
            {
                var maq = GetMaquilaPorTarjetaYFecha(numTarjeta, fechaExpiracion); // validar de acuerdo al IdDelProcesador.
                if (maq == null)
                    return new UsuarioOnlineResponse { Success = false, UserResponse = "No se encontró la cuenta" };

                var accesoCliente = broxelcoRdgEntities.accessos_clientes.FirstOrDefault(x => x.IdMaquila == maq.id); //validar de acuerdo al id del procesador.
                if (accesoCliente != null)
                {
                    return accesoCliente.IdUsuarioOnlineBroxel != id ? new UsuarioOnlineResponse { Success = false, UserResponse = "Esta tarjeta actualmente se encuentra ligada a otra cuenta." } : new UsuarioOnlineResponse { Fecha = DateTime.Now.ToString(CultureInfo.InvariantCulture), Success = true, UserResponse = "Transacción Exitosa" };
                }

                if (maq.producto == "K153")
                    return new UsuarioOnlineResponse { Success = false, UserResponse = "No es posible registrarse con esta tarjeta" };
                try
                {
                    //sacar las cuentas del usuario y validar si alguna es mejoravit.
                    if (identifier != ConfigurationManager.AppSettings["MejoravitBatchId"])
                    {
                        var cuentas = broxelcoRdgEntities.accessos_clientes.Where(x => x.IdUsuarioOnlineBroxel == id && x.IdMaquila != null).Select(x => x.cuenta).ToList();
                        var valida = new MySqlDataAccess();

                        // Valida si una cuneta tiene asignado los productos: K174,K175,K151
                        foreach (var numCuenta in cuentas)
                        {
                            if (valida.validarMerchant(numCuenta))
                            {
                                return new UsuarioOnlineResponse
                                {
                                    UserResponse = "No puedes agregar esta tarjeta"
                                };
                            }
                        }

                        foreach (var numCuenta in cuentas)
                        {
                            if (valida.ValidaProductoMejoravit(numCuenta))
                            {
                                return new UsuarioOnlineResponse
                                {
                                    UserResponse = "No puedes agregar más tarjetas a un usuario con cuenta Mejoravit"
                                };
                            }
                        }
                    }

                    //Family y b2c
                    if (ConfigurationManager.AppSettings["B2cCards"].Contains(maq.producto))
                    {
                        UsuarioOnlineResponse respuesta = new UsuarioOnlineResponse();
                        bool flujoFamily = true;
                        BroxelFamily bf = new BroxelFamily();
                        BroxelCards bc = new BroxelCards();
                        bool registro = true;
                        switch (maq.producto)
                        {
                            case "D152":
                                //Buscar dentro de los bci nuevos faltante
                                try
                                {
                                    if (maq.clave_cliente.Contains("BRI"))
                                    {
                                        var cuentasHijas = bc.ObtenerCuentasbyClaveCliente(maq.num_cuenta,
                                            maq.clave_cliente);

                                        //FAMILY
                                        if (cuentasHijas.Any())
                                        {
                                            //agregar padre
                                            if (bf.AgregarCuentaPadre(usuario.Id, maq.num_cuenta, maq.clave_cliente))
                                            {
                                                bool agregadaFF = true;
                                                foreach (var ctahija in cuentasHijas)
                                                {
                                                    if (ctahija.producto == "K165" && agregadaFF)
                                                    {
                                                        agregadaFF = false;
                                                        AgregaOActualizaRelacion(usuario.Celular, ctahija.num_cuenta,
                                                            usuario.Usuario,
                                                            usuario.NombreCompleto, usuario.Id,
                                                            usuario.CorreoElectronico,
                                                            ctahija.id, alias);
                                                        broxelcoRdgEntities.SaveChanges();

                                                        //renomina
                                                        bc.RenominacionbyIdUsuarioOnline(usuario.Id, ctahija.id, usuario.Usuario);

                                                    }
                                                    else
                                                    {
                                                        //agregar hijas
                                                        if (bf.AgregarCuentaHija(usuario.Id, maq.num_cuenta,
                                                            ctahija.num_cuenta))
                                                        {
                                                            //agregar configuracion
                                                            if (
                                                                !bf.AgregarAccionCuentaHija(ctahija.num_cuenta, 1, false))
                                                            {
                                                                bc.MailError(
                                                                    "Ocurrio un error al enlazar la configuracion de la cuenta hija: " +
                                                                    ctahija.num_cuenta);
                                                                registro = false;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            bc.MailError(
                                                                "Ocurrio un error al enlazar la cuenta hija: " +
                                                                ctahija.num_cuenta);
                                                            registro = false;
                                                        }
                                                    }

                                                    //activa cuenta
                                                    bc.ActivarCuentaClienteIndividual(ctahija.num_cuenta);
                                                }
                                            }
                                            else
                                            {
                                                bc.MailError("Ocurrio un error al enlazar la cuenta padre: " +
                                                             maq.num_cuenta);
                                                registro = false;
                                            }
                                        }
                                    }

                                    //agrega accesos clientes
                                    AgregaOActualizaRelacion(usuario.Celular, maq.num_cuenta, usuario.Usuario,
                                        usuario.NombreCompleto, usuario.Id, usuario.CorreoElectronico, maq.id, alias);
                                    broxelcoRdgEntities.SaveChanges();

                                    //activa cuenta
                                    bc.ActivarCuentaClienteIndividual(maq.num_cuenta);

                                    //renomina
                                    bc.RenominacionbyIdUsuarioOnline(usuario.Id, maq.id, usuario.Usuario);
                                }
                                catch (Exception ex)
                                {
                                    registro = false;
                                    bc.MailError("Ocurrio un error al recuperar la informacion: " + ex.Message);
                                }
                                respuesta.Success = registro;
                                respuesta.UserResponse = registro ? "Transacción exitosa." : "Ocurrio un error al agregar la tarjeta";
                                break;
                            case "K165":
                                try
                                {
                                    //Tipo de cliente es individual o corporativo
                                    if (maq.clave_cliente.Contains("BRI"))
                                    {
                                        //validar que ya este agregada la tarjeta word elite
                                        var cuentas = bc.ObtenerCuentasbyClaveCliente(maq.num_cuenta, maq.clave_cliente);
                                        bool estaWe = false;
                                        bool hayPadre = false;
                                        string cuentaPadre = "";
                                        foreach (var cuenta in cuentas)
                                        {
                                            if (cuenta.producto == "D152")
                                            {
                                                estaWe = true;
                                                flujoFamily = true;
                                                if (!bc.EstaAsociadasAUsuario(cuenta.num_cuenta))
                                                {
                                                    bc.MailError(
                                                        "No se asoció la tarjeta ya que aun no agrega su tarjeta Word Elite : ");
                                                    respuesta.Success = false;
                                                    respuesta.UserResponse =
                                                        "No se asoció la tarjeta ya que aun no agrega su tarjeta Word Elite.";
                                                }
                                                else
                                                {
                                                    //el que esta agregando la cuenta es el padre de family?
                                                    if (bf.EsPadre(maq.num_cuenta, usuario.Id))
                                                    {
                                                        //agregar a corrusel
                                                        if (!bf.AgregarAccionCuentaHija(maq.num_cuenta, 2, true))
                                                        {
                                                            bc.MailError(
                                                                "Ocurrio un error al enlazar la configuracion de la cuenta hija: " +
                                                                maq.num_cuenta);
                                                            registro = false;
                                                        }

                                                        //activar cuenta
                                                        bc.ActivarCuentaClienteIndividual(maq.num_cuenta);
                                                    }
                                                    else
                                                    {
                                                        //agrega accesos clientes
                                                        AgregaOActualizaRelacion(usuario.Celular, maq.num_cuenta,
                                                            usuario.Usuario,
                                                            usuario.NombreCompleto, usuario.Id,
                                                            usuario.CorreoElectronico, maq.id, alias);
                                                        broxelcoRdgEntities.SaveChanges();

                                                        //activa cuenta
                                                        bc.ActivarCuentaClienteIndividual(maq.num_cuenta);

                                                        //renomina
                                                        bc.RenominacionbyIdUsuarioOnline(usuario.Id, maq.id,
                                                            usuario.Usuario);

                                                        //agregar a corrusel
                                                        if (!bf.AgregarAccionCuentaHija(maq.num_cuenta, 2, true))
                                                        {
                                                            bc.MailError(
                                                                "Ocurrio un error al enlazar la configuracion de la cuenta hija: " +
                                                                maq.num_cuenta);
                                                            registro = false;
                                                        }
                                                    }
                                                    respuesta.Success = registro;
                                                    respuesta.UserResponse = registro
                                                        ? "Transacción exitosa."
                                                        : "Ocurrio un error al agregar la tarjeta";
                                                    break;
                                                }
                                            }
                                            else
                                                flujoFamily = false;

                                            if (bf.EsCuentaPadre(cuenta.num_cuenta))
                                            {
                                                hayPadre = true;
                                                cuentaPadre = cuenta.num_cuenta;
                                            }

                                        }

                                        if (!estaWe)
                                        {
                                            //verificar si hay cuenta padre
                                            if (hayPadre)
                                            {
                                                //el usuario es el padre?
                                                if (bf.EsPadre(maq.num_cuenta, usuario.Id))
                                                {
                                                    //mostrar en carrusel
                                                    if (!bf.AgregarAccionCuentaHija(maq.num_cuenta, 2, true))
                                                    {
                                                        bc.MailError(
                                                            "Ocurrio un error al enlazar la configuracion de la cuenta hija: " +
                                                            maq.num_cuenta);
                                                        registro = false;
                                                    }

                                                    //activa cuenta
                                                    bc.ActivarCuentaClienteIndividual(maq.num_cuenta);
                                                }
                                                else
                                                {
                                                    //mostrar en carrusel
                                                    if (!bf.AgregarAccionCuentaHija(maq.num_cuenta, 2, true))
                                                    {
                                                        bc.MailError(
                                                            "Ocurrio un error al enlazar la configuracion de la cuenta hija: " +
                                                            maq.num_cuenta);
                                                        registro = false;
                                                    }

                                                    //activa cuenta
                                                    bc.ActivarCuentaClienteIndividual(maq.num_cuenta);

                                                    //renomina
                                                    bc.RenominacionbyIdUsuarioOnline(usuario.Id, maq.id,
                                                        usuario.Usuario);

                                                    //agregar relacion
                                                    AgregaOActualizaRelacion(usuario.Celular, maq.num_cuenta,
                                                               usuario.Usuario,
                                                               usuario.NombreCompleto, usuario.Id,
                                                               usuario.CorreoElectronico, maq.id, alias);
                                                    broxelcoRdgEntities.SaveChanges();
                                                }
                                            }
                                            else
                                            {
                                                //agregar como padre
                                                if (!bf.AgregarCuentaPadre(usuario.Id, maq.num_cuenta, maq.clave_cliente))
                                                {
                                                    bc.MailError("Ocurrio un error al asociar cuenta padre: " + maq.num_cuenta);
                                                    registro = false;
                                                }

                                                //activa cuenta
                                                bc.ActivarCuentaClienteIndividual(maq.num_cuenta);

                                                //renomina
                                                bc.RenominacionbyIdUsuarioOnline(usuario.Id, maq.id,
                                                    usuario.Usuario);

                                                //agregar relacion
                                                AgregaOActualizaRelacion(usuario.Celular, maq.num_cuenta,
                                                           usuario.Usuario,
                                                           usuario.NombreCompleto, usuario.Id,
                                                           usuario.CorreoElectronico, maq.id, alias);
                                                broxelcoRdgEntities.SaveChanges();
                                            }
                                            respuesta.Success = registro;
                                            respuesta.UserResponse = registro
                                                ? "Transacción exitosa."
                                                : "Ocurrio un error al agregar la tarjeta";
                                        }
                                    }
                                    else
                                        flujoFamily = false;
                                }
                                catch (Exception ex)
                                {
                                    respuesta.Success = false;
                                    respuesta.UserResponse = "Ocurrio un error al agregar la tarjeta";
                                    bc.MailError("Ocurrio un error al enlazar el prod K152: " + maq.num_cuenta + "   error --> : " + ex.Message);
                                }
                                break;
                            case "K166":
                                try
                                {
                                    bool agregarAcceso = true;

                                    if (!bc.TieneRegistroEnCreaCliente(maq.num_cuenta))
                                    {
                                        if (maq.clave_cliente.Contains("MYO") || maq.clave_cliente.Contains("BRI"))
                                        {
                                            // cuenta de family
                                            if (bf.EsHija(maq.num_cuenta))
                                            {
                                                //confirmar tarjeta
                                                if (!bf.ConfirmarCuentaFamily(maq.num_cuenta))
                                                {
                                                    bc.MailError(
                                                        "Ocurrio un error al enlazar la configuracion de la cuenta hija: " +
                                                        maq.num_cuenta);
                                                    registro = false;
                                                }


                                                if (bf.EsPadre(maq.num_cuenta, usuario.Id))
                                                {
                                                    //mostrar carrusel
                                                    if (!bf.AgregarAccionCuentaHija(maq.num_cuenta, 2, true))
                                                    {
                                                        bc.MailError(
                                                            "Ocurrio un error al enlazar la configuracion de la cuenta hija: " +
                                                            maq.num_cuenta);
                                                        registro = false;
                                                    }

                                                    //activa cuenta
                                                    bc.ActivarCuentaClienteIndividual(maq.num_cuenta);

                                                    agregarAcceso = false;
                                                }
                                                else
                                                {
                                                    // mostrar carrusel
                                                    if (!bf.AgregarAccionCuentaHija(maq.num_cuenta, 2, true))
                                                    {
                                                        bc.MailError(
                                                            "Ocurrio un error al enlazar la configuracion de la cuenta hija: " +
                                                            maq.num_cuenta);
                                                        registro = false;
                                                    }

                                                    // activar en family
                                                    if (!bf.AgregarAccionCuentaHija(maq.num_cuenta, 1, true))
                                                    {
                                                        bc.MailError(
                                                            "Ocurrio un error al enlazar la configuracion de la cuenta hija: " +
                                                            maq.num_cuenta);
                                                        registro = false;
                                                    }
                                                    //activa cuenta
                                                    bc.ActivarCuentaClienteIndividual(maq.num_cuenta);

                                                    agregarAcceso = true;
                                                }
                                            }
                                            else
                                            {
                                                agregarAcceso = true;
                                            }
                                        }
                                        else
                                        {
                                            //cuenta pull
                                            //usuario tiene un cliente individual?
                                            var cuentasCi = bc.ObtenerCuentasIndividuales(usuario.Id);

                                            if (cuentasCi.Any())
                                            {
                                                if (cuentasCi.Any(s => s.clave_cliente.Contains("BRI")))
                                                {
                                                    var clave =
                                                        cuentasCi.FirstOrDefault(s => s.clave_cliente.Contains("BRI"));
                                                    //cuenta word elite
                                                    if (clave != null)
                                                        bc.CambiaClienteMaquila(maq.num_cuenta, clave.clave_cliente);
                                                    registro = true;
                                                }
                                                else
                                                {
                                                    //cuenta b2c
                                                    var clave =
                                                        cuentasCi.FirstOrDefault(s => s.clave_cliente.Contains("MYO"));
                                                    //cuenta word elite
                                                    if (clave != null)
                                                        bc.CambiaClienteMaquila(maq.num_cuenta, clave.clave_cliente);
                                                    registro = true;
                                                }
                                            }
                                            else
                                            {
                                                //genera cliente indivudual
                                                var resp = bc.CreaClienteIndividual(usuario.Usuario, maq.num_cuenta,
                                                    maq.producto, maq.id, usuario.Id);

                                                registro = resp;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        agregarAcceso = false;
                                        flujoFamily = false;
                                    }


                                    if (agregarAcceso)
                                    {
                                        //agrega accesos clientes
                                        AgregaOActualizaRelacion(usuario.Celular, maq.num_cuenta,
                                            usuario.Usuario,
                                            usuario.NombreCompleto, usuario.Id,
                                            usuario.CorreoElectronico, maq.id, alias);
                                        broxelcoRdgEntities.SaveChanges();

                                        //activa cuenta
                                        bc.ActivarCuentaClienteIndividual(maq.num_cuenta);

                                        //renomina
                                        bc.RenominacionbyIdUsuarioOnline(usuario.Id, maq.id,
                                            usuario.Usuario);
                                    }

                                    respuesta.Success = registro;
                                    respuesta.UserResponse = registro
                                        ? "Transacción exitosa."
                                        : "Ocurrio un error al agregar la tarjeta";
                                }
                                catch (Exception ex)
                                {
                                    respuesta.Success = false;
                                    respuesta.UserResponse = "Ocurrio un error al agregar la tarjeta";
                                    bc.MailError("Ocurrio un error al enlazar el prod K166: " + maq.num_cuenta +
                                                 "   error --> : " + ex.Message);
                                }
                                break;
                            default:
                                respuesta.Success = false;
                                respuesta.UserResponse = "Esta tarjeta actualmente se encuentra ligada a otra cuenta.";
                                break;
                        }
                        if (flujoFamily)
                            return respuesta;
                    }

                    //........................................................................

                    maquila maq1 = broxelcoRdgEntities.maquila.FirstOrDefault(x => x.num_cuenta == maq.num_cuenta);
                    Debug.Assert(maq1 != null, "maq1 != null");
                    AgregaOActualizaRelacion(usuario.Celular, maq1.num_cuenta, usuario.Usuario, usuario.NombreCompleto, usuario.Id, usuario.CorreoElectronico, maq.id, alias);
                    broxelcoRdgEntities.SaveChanges();

                    //Activar cuenta
                    var resActivacion = new BroxelCards().ActivarCuentaClienteIndividual(maq1.num_cuenta);
                    if (!resActivacion)
                    {
                        Helper.SendMail("broxelonline@broxel.com", "josesalvador.macias@broxel.com", "Error al activar la cuenta", "usuario : "+ usuario.Usuario +" -- cuenta: " + maq.num_cuenta  , "67896790");
                    }

                    try
                    {
                        if (vCard.ValidaUsuarioSinCuenta(idUser) == 0 && broxelcoRdgEntities.accessos_clientes.Where(a => a.IdUsuarioOnlineBroxel == idUser && a.IdMaquila != null).ToList().Count == 0)
                            vCard.ActualizaTieneCuenta(id);
                    }
                    catch (Exception ex)
                    {
                        Helper.SendMail("broxelonline@broxel.com", "mauricio.lopez@broxel.com", "Error en actualizacion CreaClienteSinTarjetaLog", "Fallo actualización para el idUser: " + id + ": " + ex, "67896790");
                    }
                    try
                    {
                        if (!(String.IsNullOrEmpty(host)) && ConfigurationManager.AppSettings["EnviarCorreoAviso"] == "1")
                        {
                            string tarjetaMask = numTarjeta.Substring(6) + "** ****" +
                                       numTarjeta.Substring(numTarjeta.Length - 4);
                            //Mailing.EnviaCorreoAvisoMovimiento("Tarjeta Agregada", 1, "Tarjeta", tarjetaMask, usuario.NombreCompleto, host, usuario.CorreoElectronico);
                        }
                    }
                    catch (Exception ex)
                    {
                        Helper.SendMail("broxelonline@broxel.com", "luis.huerta@broxel.com", "EnviaCorreoAvisoMovimiento", "Se intento enviar el correo de notificación de que se dio de alta una tarjeta  con la excpeción " + ex, "67896789");
                    }
                    return new UsuarioOnlineResponse { Fecha = DateTime.Now.ToString(CultureInfo.InvariantCulture), Success = true, UserResponse = "Transacción exitosa" };
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Trace.WriteLine(DateTime.Now.ToString("O") + " AgregarTarjetaBL Entity of type \"{0}\" in state \"{1}\" has the following validation errors:" +
                            eve.Entry.Entity.GetType().Name + ", " + eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {

                            Trace.WriteLine("- Property: \" " + ve.PropertyName + "\", Error: \" " + ve.ErrorMessage + "\"");
                        }
                    }
                }
                catch (Exception e)
                {
                    Trace.WriteLine(DateTime.Now.ToString("O") + " AgregarTarjetaBL: Error " + e);
                    return new UsuarioOnlineResponse { Success = false, UserResponse = "Ocurrió un error al crear el usuario. Intente nuevamente." };
                }
            }
            try
            {
                BitacoraUsuariosOnlineBroxel("AgregarTarjeta", numTarjeta, (usuario != null ? usuario.Usuario : id.ToString(CultureInfo.InvariantCulture)), "");
            }
            catch (Exception e)
            {
                Trace.WriteLine(DateTime.Now.ToString("O") + " AgregarTarjetaBL.BitacoraUsuariosOnlineBroxel: Error " +
                                e);
            }
            return new UsuarioOnlineResponse();
        }

        private UsuarioOnlineResponse AgregarClienteNoActualizadoBL()
        {
            // MLS Clientes que no agregan una fisica.
            var vCard = new VCardBL();
            var broxelcoRdgEntities = new broxelco_rdgEntities();
            try
            {
                var numerosCuenta = GetCuentasSinAsociar();

                if (numerosCuenta.Any())
                {
                    foreach (var cuenta in numerosCuenta)
                    {
                        var tc = Helper.GetTarjetaFromCuenta(cuenta.cuenta);

                        var numTarjeta = tc.NumeroTarjeta;
                        var fechaExpiracion = tc.FechaExpira;
                        var id = cuenta.IdUsuarioOnlineBroxel;
                        var host = "online.broxel.com";

                        var maq = GetMaquilaPorTarjetaYFecha(numTarjeta, fechaExpiracion); // validar de acuerdo al IdDelProcesador.
                        if (maq == null)
                            return new UsuarioOnlineResponse { Success = false, UserResponse = "No se encontró la cuenta" };

                        if (maq.producto == ConfigurationManager.AppSettings["SolicitudFisica"])
                        {
                            var myoEntities = new MYOEntities();

                            var userMyo = myoEntities.Users.FirstOrDefault(s => s.UserName == cuenta.usuario);

                            if (userMyo != null)
                            {
                                var res =
                                    broxelcoRdgEntities.CreaClienteSinTarjetaLog.FirstOrDefault(
                                        x => x.cuenta == maq.num_cuenta);

                                if (res == null)
                                {

                                    try
                                    {
                                        //activacion de cuenta
                                        var vcBl = new VCardBL();
                                        var resAct = vcBl.ActivarCuentaTarjetaVirtual(maq.num_cuenta);

                                        if (!resAct)
                                            Helper.SendMail("broxelonline@broxel.com", "josesalvador.macias@broxel.com",
                                                "CreaClientes", "Error al activar cuenta " + cuenta.IdUsuarioOnlineBroxel + " : ",
                                                "67896789");


                                        //var tc = Helper.GetTarjetaFromCuenta(maq.num_cuenta);
                                        var lastDigits = tc.NumeroTarjeta.Substring(tc.NumeroTarjeta.Length - 4);
                                        try
                                        {
                                            //renominacion
                                            var firstOrDefault =
                                                broxelcoRdgEntities.maquila.FirstOrDefault(s => s.id == maq.id);
                                            if (firstOrDefault != null)
                                            {
                                                List<OriginacionData> originacion = new List<OriginacionData>();
                                                OriginacionData ori = new OriginacionData
                                                {
                                                    NumCuenta = maq.num_cuenta,
                                                    ApellidoMaterno = userMyo.LastNameMother,
                                                    ApellidoPaterno = userMyo.LastNameFather,
                                                    Calle = userMyo.Domicile,
                                                    Clabe = firstOrDefault.CLABE,
                                                    ClaveCliente = firstOrDefault.clave_cliente,
                                                    CodigoPostal = userMyo.PostalCode,
                                                    Colonia = userMyo.Colony,
                                                    Email = userMyo.Email,
                                                    EstadoCivil = "",
                                                    FechaNacimiento = userMyo.BirthDate.ToString("yyyy-MM-dd"),
                                                    Genero = userMyo.Gender,
                                                    Municipio = userMyo.DelTown,
                                                    NumExterior = userMyo.NumExt,
                                                    NumInterior = userMyo.NumInt,
                                                    NumTarjeta = lastDigits,
                                                    TelefonoMovil = userMyo.PhoneNumber,
                                                    Producto = maq.producto
                                                };

                                                originacion.Add(ori);
                                                var respo = RenominacionOri(originacion, true);

                                                if (respo == "")
                                                    Helper.SendMail("broxelonline@broxel.com",
                                                        "josesalvador.macias@broxel.com", "CreaClientes",
                                                        "Error al renominar " + cuenta.IdUsuarioOnlineBroxel, "67896789");

                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            Helper.SendMail("broxelonline@broxel.com", "josesalvador.macias@broxel.com",
                                                "CreaClientes", "Error al renominar " + cuenta.IdUsuarioOnlineBroxel + " : " + e, "67896789");
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Helper.SendMail("broxelonline@broxel.com", "mauricio.lopez@broxel.com",
                                            "CreaClientes", "Error al renominar " + cuenta.IdUsuarioOnlineBroxel + " : " + ex, "67896789");
                                    }
                                }
                                try
                                {
                                    if (!(String.IsNullOrEmpty(host)) &&
                                        ConfigurationManager.AppSettings["EnviarCorreoAviso"] == "1")
                                    {
                                        string tarjetaMask = numTarjeta.Substring(6) + "** ****" +
                                                             numTarjeta.Substring(numTarjeta.Length - 4);
                                        //Mailing.EnviaCorreoAvisoMovimiento("Tarjeta Agregada", 1, "Tarjeta", tarjetaMask,
                                        //    cuenta.nombre_completo, host, cuenta.Email);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Helper.SendMail("broxelonline@broxel.com", "luis.huerta@broxel.com",
                                        "EnviaCorreoAvisoMovimiento",
                                        "Se intento enviar el correo de notificación de que se dio de alta una tarjeta  con la excpeción " +
                                        ex, "67896789");
                                }
                            }


                        }

                    }
                }
                return new UsuarioOnlineResponse
                {
                    Success = true,
                    UserResponse = "No se encontró al usuario."
                };

            }
            catch (Exception ex)
            {
                return new UsuarioOnlineResponse
                {
                    Success = false,
                    UserResponse = "No se encontró al usuario."
                };
            }
        }

        private List<accessos_clientes> GetCuentasSinAsociar()
        {
            List<accessos_clientes> accesos = new List<accessos_clientes>();
            var broxelcoRdgEntities = new broxelco_rdgEntities();
            DateTime fecha = new DateTime(2017, 02, 08);

            var numerosCuenta = (from a in broxelcoRdgEntities.accessos_clientes
                                 join m in broxelcoRdgEntities.maquila on a.cuenta equals m.num_cuenta
                                 where m.producto == "K166" &&
                                 a.FechaHoraCreacion > fecha &&
                                 m.nombre_titular.Contains("B2C")
                                 orderby a.id descending
                                 select new
                                 {
                                     a.cuenta,
                                     a.IdUsuarioOnlineBroxel,
                                     a.usuario,
                                     a.Email,
                                     a.nombre_completo
                                 });

            if (numerosCuenta.Any())
            {
                foreach (var item in numerosCuenta)
                {
                    accessos_clientes ac = new accessos_clientes();
                    ac.Email = item.Email;
                    ac.cuenta = item.cuenta;
                    ac.IdUsuarioOnlineBroxel = item.IdUsuarioOnlineBroxel;
                    ac.usuario = item.usuario;
                    ac.nombre_completo = item.nombre_completo;
                    accesos.Add(ac);
                }
            }

            return accesos;
        }

        [WebMethod]
        public bool ActualizarRegistrosResagados()
        {
            bool resp = false;
            try
            {
                var repo = new broxelco_rdgEntities();

                DateTime fec = new DateTime(2017,03,30);

                var cuentasb2C = (from m in repo.maquila
                    join ac in repo.accessos_clientes
                    on m.num_cuenta equals ac.cuenta
                    join ub in repo.UsuariosOnlineBroxel
                    on ac.IdUsuarioOnlineBroxel equals ub.Id
                    where m.producto == "K166" && ac.FechaHoraCreacion > fec && m.clave_cliente.Contains("BRC")
                    select new
                    {
                        claveCliente = m.clave_cliente,
                        FechaCreacion = ac.FechaHoraCreacion,
                        Usuario = ac.usuario,
                        Celular = ac.celular,
                        Cuenta = ac.cuenta,
                        Nombre = m.nombre_titular,
                        IdUserBroxel = ac.IdUsuarioOnlineBroxel,
                        Clabe = m.CLABE,
                        IdMaquila = m.id
                    }).ToList().OrderByDescending(s=> s.FechaCreacion);


                if (cuentasb2C.Any())
                {
                    foreach (var cuenta in cuentasb2C)
                    {
                        var detalle = repo.DetalleClientesBroxel.FirstOrDefault(s => s.CLABE == cuenta.Clabe);

                        if (detalle != null)
                        {
                            var clienteBrox = repo.clientesBroxel.FirstOrDefault(s => s.tel == cuenta.Celular);

                            if (clienteBrox != null)
                            {
                                var maquila = repo.maquila.FirstOrDefault(s => s.id == cuenta.IdMaquila);

                                if (maquila != null)
                                {
                                    maquila.clave_cliente = detalle.ClaveCliente;
                                    repo.SaveChanges();
                                }
                            }
                        }
                        else
                        {
                            BroxelCards bc = new BroxelCards();
                            if (!bc.CreaClienteIndividual(cuenta.Usuario, cuenta.Cuenta, "K166", cuenta.IdMaquila,
                                (int) cuenta.IdUserBroxel))
                            {
                                Trace.WriteLine("Ocurrio un error al crear el cliente" + " -----------> " +
                                                cuenta.IdMaquila);
                            }
                            else
                            {
                                if (
                                    !bc.RenominacionbyIdUsuarioOnline((int) cuenta.IdUserBroxel, cuenta.IdMaquila,
                                        cuenta.Usuario))
                                {
                                    Trace.WriteLine("Ocurrio un error al renominar cuenta" + " -----------> " +
                                                cuenta.IdMaquila);
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                resp = false;
            }
            return resp;
        }

        private UsuarioOnlineResponse AgregarTarjetaMYOBL(int id, string numTarjeta, string fechaExpiracion, int idUser, string alias = "", string identifier = "")
        {
            var broxelcoRdgEntities = new broxelco_rdgEntities();

            var usuario = broxelcoRdgEntities.UsuariosOnlineBroxel.FirstOrDefault(x => x.Id == id);
            if (usuario != null)
            {
                var maq = GetMaquilaPorTarjetaYFecha(numTarjeta, fechaExpiracion);
                if (maq == null)
                    return new UsuarioOnlineResponse { Success = false, UserResponse = "No se encontro la cuenta" };

                var accesoCliente = broxelcoRdgEntities.accessos_clientes.FirstOrDefault(x => x.IdMaquila == maq.id);
                if (accesoCliente != null)
                {
                    return accesoCliente.IdUsuarioOnlineBroxel != id ? new UsuarioOnlineResponse { Success = false, UserResponse = "Esta tarjeta actualmente se encuentra ligada a otra cuenta." } : new UsuarioOnlineResponse { Fecha = DateTime.Now.ToString(CultureInfo.InvariantCulture), Success = true, UserResponse = "TRANSACCION EXITOSA" };
                }

                if (maq.producto == "K153")
                    return new UsuarioOnlineResponse { Success = false, UserResponse = "No es posible registrarse con esta tarjeta" };
                try
                {
                    //sacar las cuentas del usuario y validar si alguna es mejoravit.
                    if (identifier != ConfigurationManager.AppSettings["MejoravitBatchId"])
                    {
                        var cuentas = broxelcoRdgEntities.accessos_clientes.Where(x => x.IdUsuarioOnlineBroxel == id && x.IdMaquila != null).Select(x => x.cuenta).ToList();
                        var valida = new MySqlDataAccess();

                        // Valida si una cuneta tiene asignado los productos: K174,K175,K151
                        foreach (var numCuenta in cuentas)
                        {
                            if (valida.validarMerchant(numCuenta))
                            {
                                return new UsuarioOnlineResponse
                                {
                                    UserResponse = "No puedes agregar esta tarjeta"
                                };
                            }
                        }

                        foreach (var numCuenta in cuentas)
                        {
                            if (valida.ValidaProductoMejoravit(numCuenta))
                            {
                                return new UsuarioOnlineResponse
                                {
                                    UserResponse = "No puedes agregar más tarjetas a un usuario con cuenta Mejoravit"
                                };
                            }

                        }
                    }

                    //---------------------------------------------------------------------------

                    maquila maq1 = broxelcoRdgEntities.maquila.FirstOrDefault(x => x.num_cuenta == maq.num_cuenta);
                    Debug.Assert(maq1 != null, "maq1 != null");
                    // AgregaOActualizaRelacion(usuario.Celular, maq1.num_cuenta, usuario.Usuario, usuario.NombreCompleto, usuario.Id, usuario.CorreoElectronico, maq.id, alias);
                    broxelcoRdgEntities.SaveChanges();
                    try
                    {
                        broxelcoRdgEntities.accessos_clientes.Add(new accessos_clientes
                        {
                            usuario = usuario.Usuario,
                            nombre_completo = usuario.NombreCompleto,
                            IdUsuarioOnlineBroxel = usuario.Id,
                            celular = usuario.Celular,
                            Email = usuario.CorreoElectronico,
                            IdMaquila = maq.id,
                            cuenta = maq1.num_cuenta,
                            ActivoCuenta = true,
                            FechaHoraCreacion = DateTime.Now,
                            Alias = alias,
                            IdProcesador = 1
                        });

                        broxelcoRdgEntities.SaveChanges();


                        //if (vCard.ValidaUsuarioSinCuenta(idUser) == 0 && broxelcoRdgEntities.accessos_clientes.Where(a => a.IdUsuarioOnlineBroxel == idUser).ToList().Count == 0)
                        //    vCard.ActualizaTieneCuenta(id);
                    }
                    catch (Exception ex)
                    {
                        Helper.SendMail("broxelonline@broxel.com", "mauricio.lopez@broxel.com", "Error en actulizacion CreaClienteSinTarjetaLog", "Fallo actualización para el idUser: " + id + ": " + ex, "67896790");

                    }
                    return new UsuarioOnlineResponse { Fecha = DateTime.Now.ToString(CultureInfo.InvariantCulture), Success = true, UserResponse = "TRANSACCION EXITOSA" };
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                }
                catch (Exception)
                {
                    return new UsuarioOnlineResponse { Success = false, UserResponse = "Ocurrio un error al crear el usuario. Intente nuevamente." };
                }
            }
            try
            {
                BitacoraUsuariosOnlineBroxel("AgregarTarjeta", numTarjeta, (usuario != null ? usuario.Usuario : id.ToString(CultureInfo.InvariantCulture)), "");
            }
            catch
            {
            }
            return new UsuarioOnlineResponse();

        }

        private maquila GetMaquilaPorCampoUnivoco(string campoUnivoco)
        {
            broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
            return broxelcoRdgEntities.maquila.FirstOrDefault(x => x.C4ta_linea == campoUnivoco);
        }
        #endregion

    }
}
