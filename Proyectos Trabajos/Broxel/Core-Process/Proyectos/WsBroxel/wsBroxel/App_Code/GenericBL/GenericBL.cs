using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text;
using System.Web.Script.Serialization;
using BroxelEncryptCom;
using IdSecure;
using wsBroxel.App_Code.Online;
using wsBroxel.App_Code.SolicitudBL;
using wsBroxel.App_Code.Utils;
using wsBroxel.App_Code.SolicitudBL.Model;
using wsBroxel.wsSMS;
using System.IO;

namespace wsBroxel.App_Code.GenericBL
{
    public class GenericBL
    {
        #region Tranferencias
        /// <summary>
        /// Función para componer el correo de transferencias efectuadas en aplicación.
        /// </summary>
        /// public void ComposeTranferenciasMail(DateTime fechaTx, double monto, string destinatario, string noTx, string to, string productoDe = "", string productoA = "", string cuenta = "")
        public void ComposeTranferenciasMail(DatosEmailTransferencias datosMail, TipoCorreo mail, string productoDe = "", string productoA = "", string cuenta = "")
        {
            try
            {
                // LAHA -- NUEVO FLUJO PARA TEMPLATE DE CORREO B2C
                    dynamic mailConfig = null;
                    const string scr = "data:image/gif;base64,";
                    var conexionMyo = new MYOEntities();
                    List<MailReplacer> replaceVals;
                    var broxelcoRdgEntities = new broxelco_rdgEntities();
                    var usuario = conexionMyo.Users.FirstOrDefault(x => x.Email == datosMail.CorreoUsuario);  //Se obtiene el usuario sobre la tabla de MYO para poder obtener su imagen de perfil y anexarla al correo.

                    if (usuario == null)
                        Trace.WriteLine("No se encontró usuario en MYO para obtener la imagen de perfil. correo del usuario: " + datosMail.CorreoUsuario);

                    IFormatProvider culture = new CultureInfo("es-MX", true);
                    // En base a los productos de origen y destino se selecciona el tipo de correo, transferencia normal o World Elite
                    var mailType = GetMailSelector(productoDe, productoA, cuenta);
                    if (mailType.Type == 2 | mailType.Type ==4 | mailType.Type == 3) 
                        mail = TipoCorreo.Anterior;
                       
                    switch (mail)
                    {
                        case TipoCorreo.SPEI:
                                    var datosClabe =broxelcoRdgEntities.UsuariosOnlineCLABE.FirstOrDefault(x => x.Id == datosMail.IdCLABE);
                                    var idBanco = datosClabe != null ? datosClabe.IdBanco : 0;
                                    var datosBanco = broxelcoRdgEntities.bancos_stp.FirstOrDefault(x => x.id ==idBanco );
                                    replaceVals = new List<MailReplacer>
                                        {
                                            new MailReplacer {Tag = "{Fecha}", Value = datosMail.Fecha.ToString("dd/MMMM/yyyy", culture)},
                                            new MailReplacer {Tag = "{Hora}", Value = datosMail.Fecha.ToString("HH:mm:ss tt")},
                                            new MailReplacer {Tag = "{Monto}", Value = datosMail.Monto.ToString("C2", culture)},
                                            new MailReplacer {Tag = "{Concepto}", Value = datosMail.Concepto},
                                            new MailReplacer {Tag = "{imglUsuario}", Value =usuario!=null && usuario.UserImage!=null ? scr + usuario.UserImage : "https://images.broxel.com/otras/perfil.png"},
                                            new MailReplacer {Tag = "{Usuario}", Value = usuario != null && usuario.Name != null ?usuario.Name : "Usuario Broxel"},
                                            new MailReplacer {Tag = "{Tarjeta}", Value = datosMail.NumeroTarjeta},
                                            new MailReplacer {Tag = "{imgBanco}", Value = datosBanco != null && datosBanco.clave !=null ? datosBanco.clave : "00000"},
                                            new MailReplacer {Tag = "{nombreBanco}", Value = datosBanco != null && datosBanco.nombre !=null ? datosBanco.nombre: "S/N"},
                                            new MailReplacer {Tag = "{CuentaCLABE}", Value = datosClabe != null && datosClabe.CLABE !=null ? datosClabe.CLABE.Substring(datosClabe.CLABE.Length -6,6):""}, 
                                            new MailReplacer {Tag = "{Referencia}", Value = datosMail.Referencia}, 
                                            new MailReplacer {Tag = "{Concepto}", Value = datosMail.Concepto}, 
                                            new MailReplacer {Tag = "{NumAutorizacion}", Value = datosMail.NumeroAutorizacion},
                                            new MailReplacer {Tag = "{Comision}", Value = datosMail.Comision.ToString("C2",culture)},
                                            new MailReplacer {Tag = "{Total}", Value = (Convert.ToDouble(datosMail.Comision) + Convert.ToDouble(datosMail.Monto)).ToString("C2", culture)}
                                        };
                                    mailConfig = GetMailConfig(2, 1, replaceVals); 
                            break;

                        case TipoCorreo.C2C:
                            var usuarioDestinino = conexionMyo.Users.FirstOrDefault(x => x.Email == datosMail.CorreoUsuarioDestino);
                            if (usuarioDestinino == null)
                                Trace.WriteLine("No se encontró usuario destino en MYO para obtener la imagen de perfil. correo del usuario: " + datosMail.CorreoUsuario);

                                replaceVals = new List<MailReplacer>
                                {
                                    new MailReplacer {Tag = "{Fecha}", Value = datosMail.Fecha.ToString("dd/MMMM/yyyy", culture)},
                                    new MailReplacer {Tag = "{Hora}", Value = datosMail.Fecha.ToString("HH:mm:ss tt")},
                                    new MailReplacer {Tag = "{Monto}", Value = datosMail.Monto.ToString("C2", culture)},
                                    new MailReplacer {Tag = "{imgUsuarioOrigen}", Value =usuario!=null && usuario.UserImage!=null ? scr + usuario.UserImage : "https://images.broxel.com/otras/perfil.png"},
                                    new MailReplacer {Tag = "{imgUsuarioDestino}", Value =usuarioDestinino!=null && usuarioDestinino.UserImage!=null ? scr + usuarioDestinino.UserImage : "https://images.broxel.com/otras/perfil.png"},
                                    new MailReplacer {Tag = "{Usuario}", Value = usuario != null && usuario.Name != null ?usuario.Name : "Usuario Broxel"},
                                    new MailReplacer {Tag = "{UsuarioDestino}", Value = datosMail.UsuarioDestino},
                                    new MailReplacer {Tag = "{Tarjeta}", Value = datosMail.NumeroTarjeta},
                                    new MailReplacer {Tag = "{NumAutorizacion}", Value = datosMail.NumeroAutorizacion},
                                    new MailReplacer {Tag = "{Comision}", Value = datosMail.Comision.ToString("C2",culture)},
                                    new MailReplacer {Tag = "{Total}", Value = (Convert.ToDouble(datosMail.Comision) + Convert.ToDouble(datosMail.Monto)).ToString("C2", culture)}
                                };
                                    mailConfig = GetMailConfig(3, 5, replaceVals); //Verificar en la tabla MailConfig esta configuración (3-> portal, 5-> correo C2C)
                            break;

                        case TipoCorreo.MisTarjetas:
                            replaceVals = new List<MailReplacer>
                                {
                                    new MailReplacer {Tag = "{Fecha}", Value = datosMail.Fecha.ToString("dd/MMMM/yyyy", culture)},
                                    new MailReplacer {Tag = "{Hora}", Value = datosMail.Fecha.ToString("HH:mm:ss tt")},
                                    new MailReplacer {Tag = "{Monto}", Value = datosMail.Monto.ToString("C2", culture)},
                                    new MailReplacer {Tag = "{imgUsuarioOrigen}", Value =usuario!=null && usuario.UserImage!=null ? scr + usuario.UserImage : "https://images.broxel.com/otras/perfil.png"},
                                    new MailReplacer {Tag = "{Usuario}", Value = usuario != null && usuario.Name != null ?usuario.Name : "Usuario Broxel"},
                                    new MailReplacer {Tag = "{AliasTarjeta}", Value = datosMail.AliasTarjeta},
                                    new MailReplacer {Tag = "{Tarjeta}", Value = datosMail.NumeroTarjeta},
                                    new MailReplacer {Tag = "{TarjetaDestino}", Value = datosMail.NumeroTarjetaDestino},
                                    new MailReplacer {Tag = "{NumAutorizacion}", Value = datosMail.NumeroAutorizacion},
                                    new MailReplacer {Tag = "{Comision}", Value = datosMail.Comision.ToString("C2",culture)},
                                    new MailReplacer {Tag = "{Total}", Value = (Convert.ToDouble(datosMail.Comision) + Convert.ToDouble(datosMail.Monto)).ToString("C2", culture)}
                                };
                                mailConfig = GetMailConfig(3, 6, replaceVals); //Verificar en la tabla MailConfig esta configuración (3-> portal, 6-> correo MisTarjetas)
                            break;

                        case TipoCorreo.Anterior:

                             replaceVals = new List<MailReplacer>
                            {
                                new MailReplacer {Tag = "{FechaTx}", Value = datosMail.Fecha.ToString("dd DE MMMM yyyy", culture).ToUpper()},
                                new MailReplacer {Tag = "{MontoFormateado}", Value = datosMail.Monto.ToString("C2", culture)},
                                new MailReplacer {Tag = "{NoTx}", Value = datosMail.NumeroAutorizacion},
                                new MailReplacer {Tag = "{Destinatario}", Value = datosMail.UsuarioDestino},
                                new MailReplacer {Tag = "{FechaTxFormateada}",Value = datosMail.Fecha.ToString("dd DE MMMM yyyy, hh:mm:ss", culture).ToUpper()}
                            };

                            if(mailType.Type==2)
                                replaceVals.Add(new MailReplacer { Tag = "{FechaLimite}", Value = mailType.FechaLimite.ToString("dd DE MMMM yyyy", culture).ToUpper()});
                            mailConfig = GetMailConfig(2, mailType.Type, replaceVals);
                            break;
                    }               

                    if (mailConfig != null)
                        Helper.SendMail(mailConfig.de, datosMail.CorreoUsuario, mailConfig.asunto, mailConfig.preconfBody, mailConfig.dePwd, mailConfig.deAlias);

        }
            catch(Exception e)
            {
               Helper.SendMail("broxelonline@broxel.com", "mauricio.lopez@broxel.com, luis.huerta@broxel.com", "Error al enviar correo de transferencia", e.Message, "67896789", "Broxel Fintech");
            }
        }

        /// <summary>
        /// Obtener la configuración de correo para el servicio dado.
        /// </summary>
        /// <param name="idMailConfig">id de Configuración</param>
        /// <param name="idServicio">id de Servicio</param>
        /// <param name="replaceVals">Lista de valores de reemplazo en el cuerpo del correo</param>
        /// <returns></returns>
        public MailConfig GetMailConfig(int idMailConfig, int idServicio, List<MailReplacer> replaceVals )
        {
            using (var ctx = new broxelco_rdgEntities())
            {
                var res =
                    ctx.MailConfig.FirstOrDefault(m => m.idMailConfig == idMailConfig && m.idServicio == idServicio);
                if (res == null) return null;
                res.dePwd = AesEncrypter.Decrypt(res.dePwd, "securepwd");
                foreach (var r in replaceVals)
                {
                    res.preconfBody = res.preconfBody.Replace(r.Tag, r.Value);
                }
                return res;
            }
        }

        /// <summary>
        /// Valida a que servicio le pertenece
        /// </summary>
        /// <param name="productoDe">Producto origen</param>
        /// <param name="productoA"> Producto destino</param>
        /// <param name="cuenta">Numero de cuenta para validar si es producto mejoravit</param>
        /// <returns></returns>
        private MailSelector GetMailSelector(string productoDe, string productoA, string cuenta)
        {
            try
            {

                //var mySql = new MySqlDataAccess();
                //if (productoDe == "D152" && productoA == "K165")
                //    return new MailSelector{Disclamer = true, FechaLimite = mySql.GetFechaLimite(cuenta), Type = 2};
                //return mySql.ValidateUberCard(cuenta) ? new MailSelector{Disclamer = false, FechaLimite = default(DateTime), Type = 3 } : new MailSelector { Disclamer = false, FechaLimite = default(DateTime), Type = 1 };

               
                var mySql = new MySqlDataAccess();
                if (productoDe == "D152" && productoA == "K165")
                     return new MailSelector { Disclamer = true, FechaLimite = mySql.GetFechaLimite(cuenta), Type = 2 };

                if (mySql.ValidaProductoMejoravit(cuenta))
                    return new MailSelector
                    {
                        Disclamer = false,
                        FechaLimite = default(DateTime),
                        Type = 4
                    };

                return mySql.ValidateUberCard(cuenta) ? new MailSelector { Disclamer = false, FechaLimite = default(DateTime), Type = 3 } : new MailSelector { Disclamer = false, FechaLimite = default(DateTime), Type = 1 };

            }
            catch (Exception e)
            {
                Helper.SendMail("broxelonline@broxel.com", "mauricio.lopez@broxel.com", "Error al enviar seleccionar el tipo de correo", "ProductoDe = " + productoDe + ", ProductoA = " + productoA + ", Cuenta = " + cuenta + "Error:" + e.Message, "67896789", "Broxel Fintech");
                return new MailSelector { Disclamer = false, FechaLimite = default(DateTime), Type = 1 };
            }
            
        }
        #endregion
        #region FiltroPagoTransferencias

        /// <summary>
        /// Obtiene pagos o transferencias de una cuenta dada para un periodo de tiempo dado
        /// </summary>
        /// <param name="numCuenta">Cuenta a consultar</param>
        /// <param name="fechaInicio">Fecha Inicial de Consulta</param>
        /// <param name="fechaFin">Fecha Final de consulta</param>
        /// <param name="tipo">Tipo de consulta, 2: Pagos, 3: Transferencias</param>
        /// <returns>Lista de pagos o transferencias</returns>
        public List<MovimientoOnline> GetPagoTransferencias(string numCuenta, DateTime fechaInicio, DateTime fechaFin,
            int tipo)
        {
            return new MySqlDataAccess().GetPagosTransferencias(numCuenta, fechaInicio, fechaFin, tipo);
        } 
        #endregion
        #region UsuariosBroxelOnline

        public bool ActualizaContrasenia(string arg)
        {
            var res = false;
            try
            {
                //Desencripta la cadena JSON y obtiene objeto de cambio de contraseña
                var request = ObtenCambiaContrasenaRequest(arg);
                //Valida hand shake
                if (request.Hd != "Br0x3l6789")
                    return false;
                //Valida idSecure
                var id = new IdSecureComp().GetIdUserValid(request.IdUser);
                if (id == 0)
                    return false;
                request.IdUser = id;
                var oldPwd = "";
                using (var ctx = new broxelco_rdgEntities())
                {
                    var user = ctx.UsuariosOnlineBroxel.FirstOrDefault(x => x.Id == request.IdUser);
                    if (user == null)
                        return false;
                    oldPwd = user.Password;
                    user.Password = request.Pwd;
                    ctx.Entry(user).State = EntityState.Modified;
                    ctx.SaveChanges();
                }
                new MySqlDataAccess().InsertCambiaContrasenaLog(request.IdUser, oldPwd, request.Pwd);
                res = true;
            }
            catch (Exception e)
            {
                Trace.WriteLine("CambiarContraseña:"+ e);
                res = false;
            }
            return res;
        }
        private SecureString ConvertToSecureString(string password)
        {
            if (password == null)
                throw new ArgumentNullException("password");

            var securePassword = new SecureString();

            foreach (char c in password)
                securePassword.AppendChar(c);

            securePassword.MakeReadOnly();
            return securePassword;
        }

        private CambiaContrasenaRequest ObtenCambiaContrasenaRequest(string arg)
        {
            var cerPath = AppDomain.CurrentDomain.RelativeSearchPath + "\\cert-pck12MyoTD.p12";
            Trace.WriteLine("ObtenCambiaContrasenaRequest path:" + cerPath);
            var decryptArg = PgpEncrypt.DecryptTextNetServer(arg, cerPath, ConvertToSecureString("My0t3as1"));
            var recibe = new JavaScriptSerializer();
            var jsonrecibe = recibe.Deserialize<dynamic>(decryptArg);
            var resultado = new CambiaContrasenaRequest
            {
                Hd = jsonrecibe["hd"],
                Fecha = jsonrecibe["fecha"],
                IdApp = jsonrecibe["idApp"],
                IdUser = jsonrecibe["idSecure"],
                Pwd = jsonrecibe["pwd"]
            };
            return resultado;
        }
        #endregion
        #region Autorizaciones OTP
        /// <summary>
        /// Enviar El OTP por medio de Email.
        /// </summary>
        /// <param name="emailNotificar">Email al cual se va a mandar el correo.</param>
        /// <param name="otp">El token(otp) para envial a email.</param>
        /// <param name="vigencia">La vigencia en minutos del Token.</param>
        /// <returns>resultado verdadero en caso de éxito.</returns>
        public static bool EnviarMailOtp(string emailNotificar, string otp, int vigencia)
        {
            try
            {
                IFormatProvider culture = new CultureInfo("es-MX", true);
                var fecha = DateTime.Now;

                var replaceVals = new List<MailReplacer>
                {
                    new MailReplacer {Tag = "{FECHA}", Value = fecha.ToString("dd MMMM yyyy", culture).ToUpper()},
                    new MailReplacer {Tag = "{CODIGO}", Value = otp},
                    new MailReplacer {Tag = "{VIGENCIA}",Value = vigencia.ToString()}
                };
                var mailConfig = GetMailOtpConfig(3, 1, replaceVals);
                if (mailConfig == null) return false;
                Helper.SendMail(mailConfig.de, emailNotificar, mailConfig.asunto, mailConfig.preconfBody, mailConfig.dePwd, mailConfig.deAlias);
                return true;
            }
            catch (Exception e)
            {
                Trace.WriteLine("Ocurrio un error al enviar mail OTP: " + e);
                return false;
            }
        }

        /// <summary>
        /// Obtiene la configuración del Mail para enviar el OTP.
        /// </summary>
        /// <param name="idMailConfig">Id del mail de configuración.</param>
        /// <param name="idServicio">Id del servicio para la configuración del mail.</param>
        /// <param name="replaceVals">Los valores a reemplazar dentro del formato html configurado.</param>
        /// <returns>Configuración del mail solicitado.</returns>
        private static MailConfig GetMailOtpConfig(int idMailConfig, int idServicio, IEnumerable<MailReplacer> replaceVals)
        {
            using (var ctx = new broxelco_rdgEntities())
            {
                var res =
                    ctx.MailConfig.FirstOrDefault(m => m.idMailConfig == idMailConfig && m.idServicio == idServicio);
                if (res == null) return null;
                res.dePwd = AesEncrypter.Decrypt(res.dePwd, "securepwd");
                foreach (var r in replaceVals)
                {
                    res.preconfBody = res.preconfBody.Replace(r.Tag, r.Value);
                }
                return res;
            }
        }
        #endregion
        #region Persiste Mensaje
        /// <summary>
        /// Función para guardar los mensajes de persistencia de credenciales de BroxelService.
        /// </summary>
        /// <param name="fechaInicio">Fecha inicio de llamada al método.</param>
        /// <param name="fechaFin">Fecha fin de termino de proceso.</param>
        /// <param name="numCuenta">El número de cuenta del cliente.</param>
        /// <param name="idMetodo">Id del metodo llamado.</param>
        /// <param name="idServicio">Id del servicio utilizado.</param>
        /// <param name="request">El request de la llamada al método.</param>
        /// <param name="response">El response de respuesta de llamada.</param>
        /// <param name="idMovimiento">Id del movimiento registrado.</param>
        /// <param name="idAnulacion">Id de la anulacion registrada.</param>
        public void PersisteMensaje(DateTime fechaInicio, DateTime fechaFin, string numCuenta, int idMetodo,
            int idServicio, string request, string response, int idMovimiento = 0, int idAnulacion = 0)
        {
            try
            {
                using (var ctx = new BroxelEntities())
                {
                    var msg = new LogMensajes
                    {
                        FechaInicio = fechaInicio,
                        FechaFin = fechaFin,
                        IdMetodo = idMetodo,
                        IdServicio = idServicio,
                        numCuenta = numCuenta,
                    };
                    if (!string.IsNullOrEmpty(request))
                        msg.request = AesEncrypter.Encrypt(request,
                            string.IsNullOrEmpty(numCuenta) ? "sinCuenta" : numCuenta);
                    if (!string.IsNullOrEmpty(response))
                        msg.response = AesEncrypter.Encrypt(response,
                            string.IsNullOrEmpty(numCuenta) ? "sinCuenta" : numCuenta);
                    if (idMovimiento != 0)
                        msg.IdMovimiento = idMovimiento;
                    if (idAnulacion != 0)
                        msg.IdAnulacion = idAnulacion;
                    ctx.LogMensajes.Add(msg);
                    ctx.SaveChanges();
                }

            }
            catch (DbEntityValidationException ex)
            {
                var exception = new StringBuilder();
                foreach (var eve in ex.EntityValidationErrors)
                {
                    exception.AppendFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        exception.AppendFormat("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
                Helper.SendMail("broxelonline@broxel.com", "mauricio.lopez@broxel.com, omar.vidal@broxel.com", "Error al persistir mensaje en base", "idMetodo:" + idMetodo.ToString(CultureInfo.InvariantCulture) + ", idServicio " + idServicio.ToString(CultureInfo.InvariantCulture) + "Error " + exception, "67896789", "Broxel Fintech");
            }
            catch (Exception e) 
            {
                Trace.WriteLine("Ocurrio un error al persistir mensaje: " + e);
                Helper.SendMail("broxelonline@broxel.com", "mauricio.lopez@broxel.com, omar.vidal@broxel.com", "Error al persistir mensaje en base" , "idMetodo:" + idMetodo.ToString(CultureInfo.InvariantCulture) + ", idServicio " + idServicio.ToString(CultureInfo.InvariantCulture) + " Detalle del error: " + e, "67896789", "Broxel Fintech");
            }
        }
        #endregion
        #region Niveles de Usuario

        /// <summary>
        /// Valida y pone en cuarentena cuentas
        /// </summary>
        /// <param name="numCuenta"></param>
        /// <param name="limiteDeCompra"></param>
        /// <param name="usuario">usuario que ejecuta</param>
        public void ValidaYCuarentena(string numCuenta, decimal limiteDeCompra, string usuario)
        {
            try
            {
                //Valida el monto del limite de compra
                var mySql = new MySqlDataAccess();
                if (mySql.ValidaCuentaEnCuarentena(numCuenta))
                    return;
                var idNivel = 0;
                var cuarentenaData = mySql.ValidaCuarentena(numCuenta, limiteDeCompra, usuario, ref idNivel);
                if (idNivel > 0)
                {
                    var avisos = mySql.ObtieneNivelDeCuentaAvisos(idNivel, numCuenta);
                    if (avisos.Count > 0)
                        EnviaAvisosNivelCuenta(numCuenta, limiteDeCompra, avisos);
                }
                if (cuarentenaData == null) return;
                var res = new BroxelService().BloqueoDeCuenta(numCuenta, usuario);
                if (res.Success != 1)
                    if (res.CodigoRespuesta != -33)
                        throw new Exception("No se pudo bloquear la cuenta: " + res.UserResponse);
                var id = mySql.InsertaEnCuarentena(cuarentenaData);
                if (id == 0)
                    throw new Exception("No se pudo insertar en CuarentenaCuentas");
            }
            catch (Exception e)
            {
                Trace.WriteLine("Error al validar y meter en cuarentena: " + e);
            }
        }
        /// <summary>
        /// Valida si una cuenta esta en cuarentena de niveles de servicio
        /// </summary>
        /// <param name="numCuenta">Numero de cuenta</param>
        /// <returns></returns>
        public bool ValidaCuentaEnCuarentena(string numCuenta)
        {
            return new MySqlDataAccess().ValidaCuentaEnCuarentena(numCuenta);
        }

        /// <summary>
        /// Sacas the cuenta cuarentena.
        /// </summary>
        /// <param name="cuenta">The cuenta.</param>
        /// <param name="usuario">The usuario.</param>
        /// <returns></returns>
        public int SacaCuentaCuarentena(string cuenta, string usuario)
        {
            if (new MySqlDataAccess().SacaDeCuarentena(cuenta, usuario) <= 0) return 0;
            new BroxelService().ActivacionDeCuenta(cuenta, usuario);
            return 1;
        }

        /// <summary>
        /// Envia los avisos nivel cuenta.
        /// </summary>
        /// <param name="numCuenta">numero de cuenta.</param>
        /// <param name="monto">monto.</param>
        /// <param name="avisos">avisos.</param>
        /// <returns></returns>
        public static bool EnviaAvisosNivelCuenta(string numCuenta, decimal monto, List<NivelDeCuentaAvisos> avisos )
        {
            var res = false;
            try
            {
                foreach (var aviso in from aviso in avisos let limite = aviso.Limite*aviso.Porcentaje where (monto >= limite && monto < aviso.Limite) || (monto > limite && avisos.Last().Equals(aviso)) select aviso)
                {                    
                    using (var ctx = new broxelco_rdgEntities())
                    {
                        var sms = false;
                        var mail = false;
                        var cliente = (from c in ctx.accessos_clientes
                            where c.cuenta == numCuenta
                            select c).First();
                        
                        if (cliente != null)
                        {
                            sms = EnviarAvisoNivelSms(aviso.SMSBody, cliente.celular);
                            mail = EnviarAvisoNivelMail(aviso, cliente.nombre_completo, cliente.Email);
                        }
                        if (sms || mail)
                        {
                            res = true;
                            var mySql = new MySqlDataAccess();
                            mySql.InsertLogMensajeAvisoNivel(cliente.celular, numCuenta, aviso.SMSBody, monto, aviso.IdAviso);
                        }
                    }                    
                    break;
                }
            }
            catch (Exception e)
            {
                res = false;
                Trace.WriteLine("Error al enviar avisos del nivel de cuenta: " + e);
            }
            return res;
        }

        /// <summary>
        /// Enviar el aviso nivel SMS.
        /// </summary>
        /// <param name="msg">Mensage.</param>
        /// <param name="celular">celular.</param>
        /// <returns>Éxito</returns>
        private static bool EnviarAvisoNivelSms(string msg, string celular)
        {
            var proxySms = new ServicioSMSClient();
            var cred = new Credenciales
            {
                Username = "br0x3l",
                Password = "bTEax3l",
                Host = "http://api.c3ntrosms.com:8585/Api/rec.php"
            };
            var sms = new SMS
            {
                Mensaje = msg,
                Telefono = celular
            };
            var res = proxySms.EnviarSMSC3ntro(sms, cred);
            return res.Enviado;
        }

        /// <summary>
        /// Enviar el aviso nivel mail.
        /// </summary>
        /// <param name="aviso">aviso.</param>
        /// <param name="nombreCliente"> Nombre del cliente.</param>
        /// <param name="emailNotificar"> Email para notificar.</param>
        /// <returns></returns>
        private static bool EnviarAvisoNivelMail(NivelDeCuentaAvisos aviso, string nombreCliente, string emailNotificar)
        {
            try
            {
                if (string.IsNullOrEmpty(aviso.MailBody.Trim()) && string.IsNullOrEmpty(aviso.DePwd.Trim()))
                    return false;
                IFormatProvider culture = new CultureInfo("es-MX", true);
                var fecha = DateTime.Now;
                aviso.MailBody= aviso.MailBody.Replace("[Nombre_Cliente]", nombreCliente);
                aviso.MailBody= aviso.MailBody.Replace("[FECHA]", fecha.ToString("dd MMMM yyyy", culture).ToUpper());
                aviso.DePwd = AesEncrypter.Decrypt(aviso.DePwd, "securepwd");
                Helper.SendMail(aviso.DeMail, emailNotificar, aviso.Asunto, aviso.MailBody, aviso.DePwd,
                    aviso.DeAlias);
                return true;
            }
            catch (Exception e)
            {
                Trace.WriteLine("Ocurrio un error al enviar mail de Aviso de Nivel: " + e);
                return false;
            }
        }
        #endregion
        #region Envió de cambio de NIP por correo
        /// <summary>
        /// Envía correo al usuario que realizo un cambio de NIP
        /// </summary>
        /// <param name="nombreUsuario">nombre del usuario</param>
        /// <param name="tarjeta">Últimos 4 dígitos de la tarjeta</param>
        /// <param name="correoDestino">correo del usuario</param>
        public void EmailCambioNIP(string nombreUsuario, string tarjeta, string correoDestino, string host="")
        {
            try
            {
                IFormatProvider culture = new CultureInfo("es-MX", true);
                var fecha = DateTime.Now;

                var replaceTag = new List<MailReplacer>
                {
                    new MailReplacer {Tag = "{FECHA}", Value = fecha.ToString("dd MMMM yyyy",culture)},
                    new MailReplacer {Tag = "{NombreUsuario}", Value = nombreUsuario},
                    new MailReplacer {Tag = "{Tarjeta}", Value = tarjeta}
                };

                var mailConfigCambioNIP = GetMailConfig(3, 3, replaceTag); //Revisa la tabla de MailConfig esta configuración.
                if (mailConfigCambioNIP != null)
                {
                    Helper.SendMail(mailConfigCambioNIP.de, correoDestino, mailConfigCambioNIP.asunto, mailConfigCambioNIP.preconfBody, mailConfigCambioNIP.dePwd, mailConfigCambioNIP.deAlias);
                }
                else
                {
                    Helper.SendMail("broxelonline@broxel.com", "luis.huerta@broxel.com", "CAMBIO DE NIP-> No existe configuración en BD", "No hay configuración de email en la tabla MailConfig para mandar el correo de cambio de NIP.", "67896789", "Broxel Fintech");
                }

            }
            catch (Exception e)
            {
                Helper.SendMail("broxelonline@broxel.com", "luis.huerta@broxel.com", "ERROR EN ENVIAR CORREO DE CAMBIO DE NIP", e.InnerException.Message, "67896789", "Broxel Fintech");
                Trace.WriteLine("ERROR AL ENVIAR MAIL DE CAMBIO DE NIP -> " + e.InnerException.Message);
            }
        }
        #endregion
        #region Creacion y Envío de Archivo de MyCard

        /// <summary>
        /// Crea y envia un Archivo para MyCard.
        /// </summary>
        /// <param name="parametros">Datos a reemplazar en las cuentas a consultar</param>
        /// <param name="dirEnvioTarjeta">Datos de la dirección a donde se enviaran las tarjetas.</param>
        /// <param name="datos">Datos adicionales para las tajetas.</param>
        public bool CrearArchivoMyCard(List<ParametrosFamily> parametros, DireccionEnvioTarjetaFisica dirEnvioTarjeta, DatosAdicionales datos)
        {
            var res = true;
            var nombreCliente = string.Empty;
            string fileName;
            try
            {
                if (!string.IsNullOrEmpty(datos.numCuentaPadre) && datos.idUsuarioOnline > 0)
                {
                    nombreCliente = new MySqlDataAccess().ObtenerNombreCliente(datos.numCuentaPadre, datos.idUsuarioOnline);
                    if (string.IsNullOrEmpty(nombreCliente))
                        throw new Exception("El número de la cuenta padre: " + datos.numCuentaPadre + ", con el id: " + datos.idUsuarioOnline + " no existe!");
                }
                else
                    throw new Exception("No se recibieron todos los datos adicionales de la Cuenta Padre.");
            }
            catch (Exception e)
            {
                Helper.SendMail("broxelonline@broxel.com", "omar.vidal@broxel.com",
                    "Error al generar el archivo de MyCard.", e.Message, "67896789", "Broxel Fintech");
                Trace.WriteLine(DateTime.Now.ToString("O") + " Error al generar el archivo de MyCard -> " + e);
                res = false;
            }

            if (res)
            {
                res = false;
                fileName = DateTime.Now.ToString("yyyyMMdd_hhmm") + "_N_651_221_K166_" + nombreCliente.Normalize(NormalizationForm.FormD).ToUpper() + ".txt";
                try
                {
                    var rutaLocal = AppDomain.CurrentDomain.BaseDirectory;
                    var regList = 0;
                    var regSave = 0;

                    var path = rutaLocal + fileName;
                    if (File.Exists(path))
                        File.Delete(path);

                    var listLineas = new List<string>();
                    var listNumCuentas = new List<string>();
                    var listNumTelefonos = new List<string>();

                    foreach (var parametro in parametros)
                    {
                        var cuenta = parametro.numCuenta;
                        var nombre = parametro.nombreTarjeta.ToUpper();
                        var telefono = parametro.telefono;
                        //Se obtienen los datos de la tarjeta relacionada al numero de cuenta.
                        var tarjeta = Helper.GetTarjetaFromCuenta(cuenta);
                        if (tarjeta != null)
                        {
                            var nip = GenerarNumeroAleatorio();
                            if (!string.IsNullOrEmpty(nip))
                            {
                                var linea = new SQLDataAccess().GetLineaMyCard(cuenta);
                                if (!string.IsNullOrEmpty(linea))
                                {
                                    linea = AesEncrypter.Decrypt(linea, "ftpdb");
                                    var cad = linea.Substring(376, 26);
                                    if (nombre.Length > 26)
                                        nombre = nombre.Substring(0, 26);
                                    else
                                    {
                                        while (nombre.Length < 26)
                                            nombre += " ";
                                    }
                                    linea = linea.Replace(cad, nombre);
                                    var l1 = linea.Substring(0, 603);
                                    var l2 = linea.Substring(607);
                                    linea = l1 + nip + l2;
                                    listNumCuentas.Add(cuenta);
                                    listNumTelefonos.Add(telefono);
                                    listLineas.Add(linea);
                                }
                                else
                                    Trace.WriteLine(DateTime.Now.ToString("O") + " No existe la cuenta: " + cuenta + "en ReportLog.");
                            }
                            else
                                Trace.WriteLine(DateTime.Now.ToString("O") + " No se generó el Nip para la cuenta: " + cuenta + ".");
                        }
                        else
                            Trace.WriteLine(DateTime.Now.ToString("O") + " No se obtuvieron los datos de la tarjeta para la cuenta: " + cuenta + " en el armado de cuenta.");
                        regList++;
                    }

                    if (listNumCuentas.Count > 0)
                    {
                        using (var ctx = new broxelco_rdgEntities())
                        {
                            regSave = 0;
                            var folio = GenerarFolioFamily(datos.idUsuarioOnline);
                            if (!string.IsNullOrEmpty(folio))
                            {
                                for (int i = 0; i < listNumCuentas.Count; i++)
                                {
                                    dirEnvioTarjeta.IdFolio = folio;
                                    dirEnvioTarjeta.numero_cuenta = listNumCuentas[i];
                                    dirEnvioTarjeta.Telefono = listNumTelefonos[i];
                                    dirEnvioTarjeta.FechaIngreso = DateTime.Now;
                                    dirEnvioTarjeta.Nombre_recibe = nombreCliente;
                                    dirEnvioTarjeta.IdUsuarioOnline = datos.idUsuarioOnline;
                                    dirEnvioTarjeta.Estatus = 2;
                                    ctx.DireccionEnvioTarjetaFisica.Add(dirEnvioTarjeta);
                                    ctx.SaveChanges();
                                    regSave++;
                                }
                            }
                            else
                                throw new Exception("No se generó folio para el Número de Cuenta Padre: " + datos.numCuentaPadre);
                        }

                        using (var fileHandler = new StreamWriter(path))
                        {
                            foreach (var line in listLineas)
                            {
                                fileHandler.WriteLine(line);
                            }
                            fileHandler.Close();
                        }

                        if (EncriptaArchivo(path, AppDomain.CurrentDomain.RelativeSearchPath + "/MyCard.asc"))
                        {
                            var attachments = new string[1];
                            attachments[0] = path + ".pgp";
                            //Helper.SendMail("broxelonline@broxel.com", "pacifico.perez@broxel.com",
                            //    "alejandro.gonzalez@broxel.com, elias.acosta@broxel.com, roberto.becerril@broxel.com, florentino.reyes@broxel.com, omar.vidal@broxel.com",
                            //    "ARCHIVO MYCARD: " + fileName, "Se guardaron "+ regSave +" registros de un total de "+ regList + " recibidos.", "67896789", "Broxel Fintech", attachments);

                            Helper.SendMailMyCard("broxelonline@broxel.com", "omar.vidal@broxel.com", "", "Archivo Mycard: " + fileName,
                                "Se guardaron " + regSave + " registros de un total de " + regList + " recibidos. Cliente: " + nombreCliente.ToUpper() + ", Número de Cuenta: " + datos.numCuentaPadre,
                                "67896789", "Broxel Fintech", attachments);
                            File.Delete(path + ".pgp");
                            File.Delete(path);
                            Trace.WriteLine(DateTime.Now.ToString("O") + " Fin de CrearArchivoMyCard.");
                            res = true;
                        }
                    }
                    else
                        throw new Exception("No se encontraron registros con las cuentas proporcionadas para la cuenta Padre: " + datos.numCuentaPadre);
                }

                catch (Exception e)
                {
                    Helper.SendMail("broxelonline@broxel.com", "omar.vidal@broxel.com",
                        "Error al generar el archivo de MyCard " + fileName, e.Message, "67896789", "Broxel Fintech");
                    Trace.WriteLine(DateTime.Now.ToString("O") +  " Error al generar el archivo de MyCard -> " + e);
                    res = false;
                }
            }
            return res;
        }

        static bool EncriptaArchivo(string path, string publicKey)
        {
            try
            {
                var pgpKey = new PgpEncryptionKeys(publicKey, null, null);
                var fileInfo = new FileInfo(path);
                using (var filePgp = File.Create(path + ".pgp"))
                {
                    new PgpEncryptMyCard(pgpKey).Encrypt(filePgp, fileInfo);
                }
                return true;
            }
            catch (Exception e)
            {
                Helper.SendMail("broxelonline@broxel.com", "omar.vidal@broxel.com", "Error al subir archivo " + path, "Error al encriptar : " + e.Message + ", " + e.InnerException, "67896789", "Broxel Fintech");
                return false;
            }
        }

        /// <summary>
        /// Genera el folio para la cuenta padre asignada y lo obtiene en caso de que ya exista el base de datos. 
        /// </summary>
        /// <param name="idUsuarioOnline">Id Usuario Online de la cuenta Padre</param>
        /// <returns></returns>
        private string GenerarFolioFamily(int idUsuarioOnline)
        {
            var folio = string.Empty;
            try
            {
                folio = new MySqlDataAccess().ObtenerFolioExistente(idUsuarioOnline);
                if (string.IsNullOrEmpty(folio))
                {
                    var pref = DateTime.Now.ToString("yyMM") + "FAM";
                    var ultFolio = new MySqlDataAccess().ObtenerUltimoFolio(pref);
                    if (!string.IsNullOrEmpty(ultFolio) && ultFolio.Length > 7)
                    {
                        var num = Convert.ToInt32(ultFolio.Substring(7));
                        num = num + 1;
                        var res = num.ToString();
                        while (res.Length < 6)
                            res = "0" + res;
                        folio = pref + res;
                    }
                    else
                        folio = pref + "000001";
                }
            }
            catch (Exception)
            {
                folio = string.Empty;
            }
            return folio;
        }

        private string GenerarNumeroAleatorio()
        {
            var numero = string.Empty;
            try
            {
                var r1 = new Random().Next(0, 9);

                var r2 = new Random().Next(0, 9);
                while(r2 == r1 || r2 == r1 + 1)
                    r2 = new Random().Next(0, 9);

                var r3 = new Random().Next(0, 9);
                while (r3 == r2 || r3 == r2 + 1)
                    r3 = new Random().Next(0, 9);

                var r4 = new Random().Next(0, 9);
                while (r4 == r3 || r4 == r3 + 1)
                    r4 = new Random().Next(0, 9);

                numero = r1.ToString() + r2.ToString() + r3.ToString() + r4.ToString();
            }
            catch (Exception)
            {
                numero = string.Empty;
            }
            return numero;
        }

        #endregion
    }
}