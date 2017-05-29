using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using wsBroxel.App_Code.Online;
using wsBroxel.App_Code.RenominacionBL;
using wsBroxel.App_Code.RenominacionBL.Model;
using wsBroxel.App_Code.VCBL;

namespace wsBroxel.BussinessLogic
{
    /// <summary>
    /// Operaciones de tarjetas broxel
    /// </summary>
    public class BroxelCards
    {
        /// <summary>
        /// Obtener tarjetas asociadas por clave de cliente.
        /// </summary>
        /// <param name="numCuenta"></param>
        /// <param name="claveCliente"></param>
        /// <returns></returns>
        public List<maquila> ObtenerCuentasbyClaveCliente(string numCuenta, string claveCliente)
        {
            List<maquila> cuentas = new List<maquila>();
            try
            {
                using (var broxelco = new broxelco_rdgEntities())
                {
                    var cuentasMaq = from p in broxelco.maquila
                        where p.clave_cliente == claveCliente && p.num_cuenta != numCuenta
                        select p;


                    //broxelco.vw_maquila.Where(s => s.clave_cliente == claveCliente && s.num_cuenta != numCuenta);

                    if (cuentasMaq.Any())
                    {

                        cuentas = cuentasMaq.ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("ObtenerCuentasbyClaveCliente --> " + "Error entro al catch: " + ex.Message);
            }
            return cuentas;
        }

        /// <summary>
        /// Activar cuenta.
        /// </summary>
        /// <param name="numCuenta"></param>
        /// <returns></returns>
        public bool ActivarCuentaClienteIndividual(string numCuenta)
        {
            bool resAct = false;
            try
            {
                var vcBl = new VCardBL();
                resAct = vcBl.ActivarCuentaTarjetaVirtual(numCuenta);

                if (!resAct)
                    Helper.SendMail("broxelonline@broxel.com",
                        "josesalvador.macias@broxel.com",
                        "ActivarCuenta", "Error al activar cuenta : " + numCuenta,
                        "67896789");
            }
            catch (Exception ex)
            {
                Helper.SendMail("broxelonline@broxel.com", "josesalvador.macias@broxel.com",
                    "ActivarCuenta", "Error al activar cuenta " + numCuenta + " : " + ex,
                    "67896789");
            }
            return resAct;
        }

        /// <summary>
        /// renominacion de tarjeta por id de usuario broxel.
        /// </summary>
        /// <param name="idUsuarioOnline"></param>
        /// <param name="idMaquila"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool RenominacionbyIdUsuarioOnline(int idUsuarioOnline, int idMaquila, string userName)
        {
            bool resAct = false;
            try
            {
                var broxelcoRdgEntities = new broxelco_rdgEntities();
                var myoEntities = new MYOEntities();

                var userMyo = myoEntities.Users.FirstOrDefault(s => s.UserName == userName);

                var maquila = broxelcoRdgEntities.maquila.FirstOrDefault(s => s.id == idMaquila);
                if (maquila != null)
                {
                    List<OriginacionData> originacion = new List<OriginacionData>();
                    if (userMyo != null)
                    {
                        var nombre = userMyo.Name + " " + userMyo.SecondName;
                        OriginacionData ori = new OriginacionData
                        {
                            NumCuenta = maquila.num_cuenta,
                            ApellidoMaterno = userMyo.LastNameMother.Trim(),
                            ApellidoPaterno = userMyo.LastNameFather.Trim(),
                            Calle = userMyo.Domicile,
                            Clabe = maquila.CLABE,
                            ClaveCliente = maquila.clave_cliente,
                            CodigoPostal = userMyo.PostalCode,
                            Colonia = userMyo.Colony,
                            Email = userMyo.Email,
                            EstadoCivil = "",
                            FechaNacimiento = userMyo.BirthDate.ToString("yyyy-MM-dd"),
                            Genero = userMyo.Gender,
                            Municipio = userMyo.DelTown,
                            NumExterior = userMyo.NumExt,
                            NumInterior = userMyo.NumInt,
                            NumTarjeta = maquila.nro_tarjeta.Substring(maquila.nro_tarjeta.Length - 4),
                            TelefonoMovil = userMyo.PhoneNumber,
                            Producto = maquila.producto,
                            Nombre = nombre.Trim()

                        };

                        originacion.Add(ori);
                        var respo = new RenominaBL().InsertaRenominacionConOriginacion(originacion, true);
                        if (respo == "")
                            Helper.SendMail("broxelonline@broxel.com",
                                "josesalvador.macias@broxel.com", "CreaClientes",
                                "Error al renominar " + userName, "67896789");
                        else
                            resAct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.SendMail("broxelonline@broxel.com", "josesalvador.macias@broxel.com",
                    "ActivarCuenta", "Error al renominar " + userName + " : " + ex,
                    "67896789");
            }
            return resAct;
        }

        /// <summary>
        /// buscar si la tarjeta esta asociada en accesos clientes.
        /// </summary>
        /// <param name="numCuenta"></param>
        /// <returns></returns>
        public bool EstaAsociadasAUsuario(string numCuenta)
        {
            bool resAct = false;
            try
            {
                var broxelcoRdgEntities = new broxelco_rdgEntities();

                var maquila = broxelcoRdgEntities.accessos_clientes.FirstOrDefault(s => s.cuenta == numCuenta);
                if (maquila != null)
                {
                    resAct = true;
                }
            }
            catch (Exception ex)
            {
                Helper.SendMail("broxelonline@broxel.com", "josesalvador.macias@broxel.com",
                    "ActivarCuenta", "Error al buscar la cuenta : " + numCuenta + " : " + ex,
                    "67896789");
            }
            return resAct;
        }

        ///<summary>
        /// Buscar si el usuario cuenta con mas tarjetas de cliente corporativo
        /// </summary>
        /// <param name="idUsuarioOnline"></param>
        /// <returns></returns>
        public List<maquila> ObtenerCuentasIndividuales(int idUsuarioOnline)
        {
            List<maquila> cuentas = new List<maquila>();
            try
            {
                var broxelcoRdgEntities = new broxelco_rdgEntities();

                var b2c = (from a in broxelcoRdgEntities.accessos_clientes
                    join
                    m in broxelcoRdgEntities.maquila on a.IdMaquila equals m.id
                    where
                    a.IdUsuarioOnlineBroxel == idUsuarioOnline &&
                    (m.producto.Contains("K166") || m.producto.Contains("D152"))
                    select new
                    {
                        m.num_cuenta,
                        m.clave_cliente,
                        m.producto,
                        m.cuenta_madre
                    });

                if (b2c.Any())
                {
                    foreach (var cuenta in b2c)
                    {
                        maquila m = new maquila
                        {
                            num_cuenta = cuenta.num_cuenta,
                            clave_cliente = cuenta.clave_cliente,
                            producto = cuenta.producto,
                            cuenta_madre = cuenta.cuenta_madre
                        };

                        cuentas.Add(m);
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.SendMail("broxelonline@broxel.com", "josesalvador.macias@broxel.com",
                    "ActivarCuenta",
                    "Error al buscar la cuentas cuentas de cliente individual del usuario : " + idUsuarioOnline + " : " +
                    ex,
                    "67896789");
            }
            return cuentas;
        }

        ///<summary>
        /// Buscar si el usuario cuenta con mas tarjetas de cliente corporativo
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="numCuenta"></param>
        /// <param name="producto"></param>
        /// <param name="idMaquila"></param>
        /// <param name="idUserOnline"></param>
        /// <returns></returns>
        public bool CreaClienteIndividual(string userName, string numCuenta, string producto, int idMaquila, int idUserOnline)
        {
            bool respuesta = false;
            Trace.WriteLine("Datos ingresados para la alta de cliente--> " + " usuario: " + userName + " -- numcuenta: " +
                            numCuenta + " -- producto: " + producto + " -- idmaquila: " + idMaquila);
            try
            {
                var myoEntities = new MYOEntities();
                var broxelcoRdgEntities = new broxelco_rdgEntities();
                var vCard = new VCardBL();

                var userMyo = myoEntities.Users.FirstOrDefault(s => s.UserName == userName);

                if (userMyo != null)
                {
                    var res =
                        broxelcoRdgEntities.CreaClienteSinTarjetaLog.FirstOrDefault(
                            x => x.cuenta == numCuenta);

                    if (res == null)
                    {
                        var resp = vCard.CreaClientesB2C(producto, idMaquila, userMyo.Name,
                            userMyo.SecondName,
                            userMyo.LastNameFather, userMyo.LastNameMother, userMyo.BirthDate, "",
                            userMyo.Colony,
                            userMyo.Domicile, userMyo.NumExt, userMyo.NumInt, userMyo.PhoneNumber,
                            userMyo.UserName,
                            "", userMyo.DelTown, userMyo.City, userMyo.PostalCode, numCuenta, 1,
                            idUserOnline);

                        if (resp.Success)
                        {
                            respuesta = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.SendMail("broxelonline@broxel.com", "josesalvador.macias@broxel.com",
                    "Crear cliente",
                    "Error al crear cliente individual del usuario : " + userName + " : " + ex.Message +
                    " ------ inner: " + ex.InnerException,
                    "67896789");
                respuesta = false;
            }
            return respuesta;
        }

        ///<summary>
        /// Buscar si el usuario cuenta con mas tarjetas de cliente corporativo
        /// </summary>
        /// <param name="numCuenta"></param>
        /// <param name="claveCliente"></param>
        /// <returns></returns>
        public bool CambiaClienteMaquila(string numCuenta, string claveCliente)
        {
            bool respuesta = false;
            try
            {

                var broxelcoRdgEntities = new broxelco_rdgEntities();

                var actualizaMaquila = (from p in broxelcoRdgEntities.maquila
                    where p.num_cuenta == numCuenta
                    select p).FirstOrDefault();

                if (actualizaMaquila != null)
                {
                    actualizaMaquila.clave_cliente = claveCliente;
                    broxelcoRdgEntities.SaveChanges();
                    respuesta = true;
                }
            }
            catch (Exception ex)
            {
                Helper.SendMail("broxelonline@broxel.com", "josesalvador.macias@broxel.com",
                    "ActivarCuenta", "Error a la cambiar la clave de cliente : " + numCuenta + " : " + ex,
                    "67896789");
                respuesta = false;
            }
            return respuesta;
        }


        ///<summary>
        /// Busca si tiene un registro pendiente en creaclientes 
        /// </summary>
        /// <param name="numCuenta"></param>
        /// <returns></returns>
        public bool TieneRegistroEnCreaCliente(string numCuenta)
        {
            bool respuesta = false;
            try
            {

                var broxelcoRdgEntities = new broxelco_rdgEntities();

                var creacliente = (from p in broxelcoRdgEntities.CreaClienteSinTarjetaLog
                                        where p.cuenta == numCuenta
                                        select p).FirstOrDefault();

                if (creacliente != null)
                {
                    respuesta = true;
                }
            }
            catch (Exception ex)
            {
                Helper.SendMail("broxelonline@broxel.com", "josesalvador.macias@broxel.com",
                "ActivarCuenta", "Error a la cambiar la clave de cliente : " + numCuenta + " : " + ex,
                "67896789");
                respuesta = false;
            }
            return respuesta;
        }

        /// <summary>
        /// Manda correo error.
        /// </summary>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        public void MailError(string mensaje)
        {
            try
            {
                Helper.SendMail("broxelonline@broxel.com",
                        "josesalvador.macias@broxel.com",
                        "ActivarCuenta", mensaje,
                        "67896789");
            }
            catch (Exception ex)
            {
                Helper.SendMail("broxelonline@broxel.com", "josesalvador.macias@broxel.com",
                    "ActivarCuenta", "Error al mandar mensaje family " + ex,
                    "67896789");
            }
        }
    }
}