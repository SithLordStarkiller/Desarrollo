using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using IdSecure;
using wsBroxel.App_Code.SolicitudBL;
using wsBroxel.App_Code.VCBL;
using System.Configuration;
using System.Diagnostics;

namespace wsBroxel.App_Code.Online
{
    [Serializable]
    public class UsuarioOnline
    {
        public Int32 Id { get; set; }
        public String Nombre { get; set; }
        public String CodigoPostal { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
        public String RFC { get; set; }
        public String Telefono { get; set; }
        public String Celular { get; set; }
        public String Email { get; set; }
        public String Usuario { get; set; }
        public String Sexo { get; set; }
        public String Sesion { get; set; }
        public List<CuentaOnline> Cuentas { get; set; }

        public UsuarioOnline()
        {

        }
    }

    [Serializable]
    public class UsuarioOnlineRequest : OnlineRequest
    {
        broxelco_rdgEntities _broxelCo = new broxelco_rdgEntities();
        BroxelService webService = new BroxelService();

        public UsuarioOnlineRequest(){}
        public UsuarioOnlineRequest(UsuarioOnline usr)
        {
            
        }

        public UsuarioOnlineResponse ObtenUsuarioInfo(int idUsuario)
        {
            var usuarios = _broxelCo.UsuariosOnlineBroxel.Where(x => x.Id  == idUsuario).ToList();
            if (usuarios.Count == 1)
            {
                return GeneraDatosRegreso(usuarios[0], "");
            }
            return new UsuarioOnlineResponse
            {
                Success = false,
                UserResponse = "No se encontró la información de ese usuario."
            };

        }

        public UsuarioOnlineResponse Login(string username, string password, int tipoLogin=0)
        {
            var usuarios = _broxelCo.UsuariosOnlineBroxel.Where(x => x.Usuario.ToLower() == username.ToLower()).ToList();
            if (usuarios.Count == 1)
            {
                var usuario = usuarios[0];
                string pass = usuario.Password;
                if (Helper.Cifrar(password) == pass)
                {
                    _broxelCo.LogUsuariosOnlineBroxel.Add(new LogUsuariosOnlineBroxel
                    {
                        Accion = "Login",
                        FechaHora = DateTime.Now,
                        Usuario = username,
                        tipoLogin = tipoLogin
                    });
                    session_dash sessionData = new session_dash 
                    {
                        idUser = usuario.Id,
                        vigencia = DateTime.Now.AddMinutes(20),
                        entrada = DateTime.Now,
                        idAplicacion = 7 //Intercambiar entre mobile y onlinebroxel (requiere nuevo parámetro)
                    };

                    _broxelCo.session_dash.Add(sessionData);
                    try
                    {
                        _broxelCo.SaveChanges();
                    }
                    catch { }

                    String sesion = GeneraSesion(usuario.Id, sessionData.idsession_dash);
                    return GeneraDatosRegreso(usuario, sesion);
                }
            }
            _broxelCo.LogUsuariosOnlineBroxel.Add(new LogUsuariosOnlineBroxel
                    {
                        Accion = "LoginFallido",
                        FechaHora = DateTime.Now,
                        Usuario = username,
                        tipoLogin = tipoLogin
                    });
                    try
                    {
                        _broxelCo.SaveChanges();
                    }
                    catch { }
            return new UsuarioOnlineResponse
            {
                Success = false,
                UserResponse = "Usuario y/o Contraseña inválidos"
            };
        }

        private string GeneraSesion(int idUsuario, int idSesion)
        {
            return Helper.ToBase64(idSesion + "|" + idUsuario + "|" + DateTime.Now.ToString("dd/M/yyyy HH:mm:ss"));
        }

        private UsuarioOnlineResponse GeneraDatosRegreso(UsuariosOnlineBroxel usuario, String sesion)
        {
           

            UsuarioOnlineResponse uor = new UsuarioOnlineResponse();
            uor.UserBroxel.Nombre = usuario.NombreCompleto;
            uor.UserBroxel.CodigoPostal = usuario.CP;
            uor.UserBroxel.FechaDeNacimiento = Convert.ToDateTime(usuario.FechaNacimiento);
            uor.UserBroxel.RFC = usuario.RFC;            
            uor.UserBroxel.Telefono = usuario.Telefono;
            uor.UserBroxel.Celular = usuario.Celular;
            uor.UserBroxel.Email = usuario.CorreoElectronico;
            uor.UserBroxel.Usuario = usuario.Usuario;
            uor.UserBroxel.Id = GetSecureUsuarioId(usuario.Id);
            uor.UserBroxel.Sexo = usuario.Sexo;
            uor.UserBroxel.Cuentas = ObtenerCuentas(usuario.Id);
            uor.UserBroxel.Sesion = sesion;
            uor.Success = true;
            uor.UserResponse = "Login válido";
            return uor;
        }
        /// <summary>
        /// Obtiene idUsuario seguro, con tiempo de vida
        /// </summary>
        /// <param name="idUser">idUser</param>
        /// <returns>idUser seguro, entero de 10 posiciones</returns>
        private int GetSecureUsuarioId(int idUser)
        {
            return new IdSecureComp().GetIdSecure(idUser);
        }

        private List<CuentaOnline> ObtenerCuentas(int idUsuario)
        {
            List<CuentaOnline> cuentas = new List<CuentaOnline>();
            try
            {
                // MLS Modificación para Login de usuarios sin cuenta.
                if (new VCardBL().ValidaUsuarioSinCuenta(idUsuario) > 0)
                    return null;

                var orderDict = "DUGNBEK".Select((c, i) => new { Letter = c, Order = i }).ToDictionary(o => o.Letter, o => o.Order);

                // var _broxelRdgEntities = new broxelco_rdgEntities();

                var idApp = 0;

                var creaCliente =
                    _broxelCo.CreaClienteSinTarjetaLog.FirstOrDefault(x => x.idUsuarioOnlineBroxel == idUsuario);
                if (creaCliente != null)
                    idApp = (int)creaCliente.idApp;

                if (idApp != 3)
                {
                    var query = (from m in _broxelCo.maquila
                                 join b in _broxelCo.productos_broxel on m.producto equals b.codigo
                                 join a in _broxelCo.accessos_clientes on m.id equals a.IdMaquila
                                 where a.IdUsuarioOnlineBroxel == idUsuario
                                 select new
                                 {
                                     m.num_cuenta,
                                     m.nombre_titular,
                                     m.producto,
                                     m.clave_cliente,
                                     m.nro_tarjeta,
                                     b.branding,
                                     b.Descripcion,
                                     a.Alias,
                                     a.P2P_Activo
                                 }).ToList();

                    foreach (var item in query)
                    {
                        CuentaOnline co = new CuentaOnline();
                        co = OnlineHelper.GetCuentaLogin(item.num_cuenta);
                        co.Alias = item.Alias;
                        co.P2P_Activo = (bool) item.P2P_Activo;
                        cuentas.Add(co);
                    }
                }
                else
                {
                    var query = (from m in _broxelCo.maquila
                                 join b in _broxelCo.productos_broxel on m.producto equals b.codigo
                                 join a in _broxelCo.accessos_clientes on m.id equals a.IdMaquila
                                 where a.IdUsuarioOnlineBroxel == idUsuario && a.ActivoCuenta == true
                                 select new
                                 {
                                     m.num_cuenta,
                                     m.nombre_titular,
                                     m.producto,
                                     m.clave_cliente,
                                     m.nro_tarjeta,
                                     b.branding,
                                     b.Descripcion,
                                     a.Alias,
                                     a.P2P_Activo
                                 }).ToList();

                    foreach (var item in query)
                    {
                        CuentaOnline co = new CuentaOnline();
                        co = OnlineHelper.GetCuentaLogin(item.num_cuenta);
                        co.Alias = item.Alias;
                        co.P2P_Activo = (bool) item.P2P_Activo;
                        cuentas.Add(co);
                    }
                }

                //Valida si hay cuenta Adicional a una virtual
                string prods = ConfigurationManager.AppSettings["SolicitudFisica"];

                if (!String.IsNullOrEmpty(prods))
                {
                    string[] productos = prods.Split(',');
                    foreach (var producto in productos)
                    {
                        var tarjetas = cuentas.Where(s => s.Prod == producto).ToList();
                        if (tarjetas.Any())
                        {
                            var numCuenta = tarjetas.Select(s => s.NumCuenta).Distinct().FirstOrDefault();

                            if (numCuenta != null)
                            {
                                var firstOrDefault = tarjetas.FirstOrDefault();
                                if (firstOrDefault != null)
                                {
                                    var prod = firstOrDefault.Prod;
                                    //join r in _broxelCo.maquila on p.id_de_registro equals r.DCConsecCliente.Trim()
                                    var queryAdic = (from p in _broxelCo.TarjetasFisicasAdicionales
                                        where p.NumCuenta == numCuenta && p.CodigoProducto == prod && p.IdUsuarioOnlineBroxel == idUsuario 
                                        select new
                                        {
                                            p
                                        }).FirstOrDefault();


                                    if(queryAdic != null)
                                    {
                                        foreach (var cuenta in cuentas)
                                        {
                                            if (cuenta.NumCuenta == numCuenta)
                                            {
                                                cuenta.NumTarjeta = queryAdic.p.Tarjeta;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Consulta de Tarjetas Por Usuario: Error => " + ex.Message);
            }
            return cuentas;
        }
    }

    [Serializable]
    public class UsuarioOnlineResponse : OnlineResponse
    {
        public UsuarioOnline UserBroxel = new UsuarioOnline();
        public UsuarioOnlineResponse()
        {
        }
    }
}