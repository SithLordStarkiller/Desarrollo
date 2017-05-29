using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations.Sql;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using wsBroxel.App_Code.SolicitudBL;
using wsBroxel.App_Code.RenominacionBL.Model;

namespace wsBroxel.App_Code.RenominacionBL
{
    /// <summary>
    /// Logica de negocio de renominación.
    /// </summary>
    public class RenominaBL
    {
        #region Metodos publicos
        /// <summary>
        /// Genera una solicitud de renominación en base a una originación
        /// </summary>
        /// <param name="originaciones">Lista de originaciones</param>
        /// <param name="ejecuta">Parametro que indica si la solicitud se ejecuta al finalizar el llenado o no</param>
        /// <returns></returns>
        public string InsertaRenominacionConOriginacion(List<OriginacionData> originaciones, bool ejecuta = true)
        {
            var folio = "";
            try
            {
                folio = InsertSolicitudRenominacion(originaciones[0].ClaveCliente, originaciones[0].Producto, true);
                if (folio == "")
                    return folio;
                var renomina =
                new Task(() => InsertaRenominacionDetalleConOriginacion(originaciones, folio, ejecuta));
                renomina.Start();
                return folio;
            }
            catch (Exception e)
            {
                Helper.SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com", "Error de renominacion service", "La renominacion fallo :  " + e, "yMQ3E3ert6", "Broxel : Renominacion Service");
            }
            return folio;
        }

        /// <summary>
        /// Genera solicitud de Reniminación.
        /// </summary>
        /// <param name="renominacion">Datos de la renominación.</param>
        /// <param name="claveCliente">Clave del usuario que realiza la renominación.</param>
        /// <param name="producto">Producto del usuario que realiza la renominación.</param>
        /// <param name="tarjeta">Tarjeta del usuario que realliza la renominación.</param>
        /// <param name="obtCuenta">Campo que identifica si los datos se obtuvieron por medio del numero de cuenta proporcionado.</param>
        /// <returns></returns>
        public RenominacionExternaResponse InsertaRenominacionExterna(RenominacionExternaData renominacion, string claveCliente, string producto, string tarjeta, bool obtCuenta)
        {
            var res = new RenominacionExternaResponse();
            try
            {
                res = ValidaDatosRenominacionExterna(renominacion);
                if (res.Respuesta == 0)
                {
                    var folio = InsertSolicitudRenominacion(claveCliente, producto, true);
                    if (string.IsNullOrEmpty(folio))
                        res = new RenominacionExternaResponse
                        {
                            Folio = string.Empty,
                            Respuesta = 1,
                            Descripcion = "Error al Insertar la Solicitud de Renominacion Externa."
                        };
                    var msg = string.Empty;
                    InsertaRenominacionExternaDetalle(renominacion, claveCliente, producto, tarjeta, folio, obtCuenta, ref msg);
                    if(!string.IsNullOrEmpty(msg))
                        return new RenominacionExternaResponse
                        {
                            Folio = folio,
                            Respuesta = 1,
                            Descripcion = msg
                        };
                    return new RenominacionExternaResponse
                    {
                        Folio = folio,
                        Respuesta = 0,
                        Descripcion = "Renominación Realizada Con Éxito."
                    };
                }
            }
            catch (Exception e)
            {
                res = new RenominacionExternaResponse
                {
                    Folio = string.Empty,
                    Respuesta = 1,
                    Descripcion = "Error al Insertar Renominación Externa."
                };
                Helper.SendMail("dispersiones@broxel.com"," mauricio.lopez@broxel.com", "Error de renominacion service", "La renominacion externa fallo :  " + e, "yMQ3E3ert6", "Broxel : Renominacion Service");
            }
            return res;
        }
        /// <summary>
        /// Inserción de detalles de renominacion asincrona
        /// </summary>
        /// <param name="originaciones">Lista de originaciones</param>
        /// <param name="folio">Folio de renominacion</param>
        /// <param name="ejecuta">Parametro que indica si la solicitud se ejecuta al finalizar el llenado o no</param>
        private void InsertaRenominacionDetalleConOriginacion(List<OriginacionData> originaciones, string folio, bool ejecuta = true)
        {
            var webService = new BroxelService();
            try
            {
                foreach (var originacion in originaciones)
                {
                    PreActualizaMaquila(originacion, originacion.NumCuenta);
                    InsertRenominacionesInternas(originacion, folio);
                }
                if (ejecuta)
                    webService.Renominar(folio, "webService");

            }
            catch (Exception e)
            {
                Helper.SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com", "Error de renominacion service", "La renominacion fallo :  " + e, "yMQ3E3ert6", "Broxel : Renominacion Service");
                throw;
            }
        }

        /// <summary>
        /// Inserción de detalle de la renominación externa
        /// </summary>
        /// <param name="renominacion"> datos de la renominacion</param>
        /// <param name="claveCliente">Clave de cliente de maquila</param>
        /// <param name="producto">producto de maquila</param>
        /// <param name="tarjeta"> numero de tarjeta de maquila</param>
        /// <param name="folio">Folio de renominacion externa</param>
        /// <param name="obtCuenta"></param>
        private void InsertaRenominacionExternaDetalle(RenominacionExternaData renominacion, string claveCliente, string producto, string tarjeta, string folio, bool obtCuenta, ref string msg)
        {
            msg = string.Empty;
            var webService = new BroxelService();
            try
            {
                PreActualizaMaquilaRenExterna(renominacion, obtCuenta);
                var originacion = new OriginacionData
                {
                    NumCuenta = renominacion.NumeroCuenta,
                    NumTarjeta = tarjeta,
                    Producto = producto,
                    ClaveCliente = claveCliente,
                    Calle = renominacion.Calle,
                    NumExterior = renominacion.NumExterior,
                    NumInterior = renominacion.NumInterior,
                    Colonia = renominacion.Colonia,
                    CodigoPostal = renominacion.CodigoPostal,
                    Municipio = renominacion.Municipio,
                    TelefonoMovil = renominacion.TelefonoMovil,
                    FechaNacimiento = renominacion.FechaNacimiento.ToString("yyyy-MM-dd"),
                    Genero = renominacion.Genero,
                    Nombre = renominacion.NombreCompleto
                };
                if (!InsertRenominacionesInternas(originacion, folio))
                    msg += "Error al insertar en Renominaciones Internas. ";

                if (!webService.Renominar(folio, "webService"))
                    msg += "Error al Renominar.";
            }
            catch (Exception e)
            {
                msg += "Error al Insertar el detalle de la renominación externa.";
                Helper.SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com", "Error de renominacion service",
                    "La renominacion externa fallo :  " + e, "yMQ3E3ert6", "Broxel : Renominacion Service");
                throw;
            }
        }

        /// <summary>
        /// Valida datos de renominacion externa
        /// </summary>
        /// <param name="renominacion">Datos de la renominacion</param>
        /// <returns></returns>
        private static RenominacionExternaResponse ValidaDatosRenominacionExterna(RenominacionExternaData renominacion)
        {
            var res = new RenominacionExternaResponse {Respuesta = 0};
            var msg = string.Empty;
            if (string.IsNullOrEmpty(renominacion.TelefonoMovil) || renominacion.TelefonoMovil.Length != 10)
                msg += "Teléfono Móvil Inválido, debe ser de 10 dígitos. ";
            if(string.IsNullOrEmpty(renominacion.Genero) || (!renominacion.Genero.Equals("M") && !renominacion.Genero.Equals("F")))
                msg += "Género Inválido, M-Masculino o F-Femenino. ";
            if (string.IsNullOrEmpty(renominacion.EstadoCivil) || (!renominacion.EstadoCivil.Equals("S") && !renominacion.EstadoCivil.Equals("C")))
                msg += "Género Inválido, S-Soltero o C-Casado. ";
            if (string.IsNullOrEmpty(renominacion.TieneHijos) || (!renominacion.TieneHijos.Equals("S") && !renominacion.TieneHijos.Equals("N")))
                msg += "Género Inválido, S-Si o N-No. ";
            if (string.IsNullOrEmpty(renominacion.Rfc) || renominacion.Rfc.Length != 13 )
                msg += "RFC Inválido, debe tener 13 caracteres. ";
            if (string.IsNullOrEmpty(renominacion.Curp) || renominacion.Curp.Length != 18)
                msg += "CURP Inválido, debe tener 18 caracteres. ";
            if (!string.IsNullOrEmpty(renominacion.Nss) && renominacion.Nss.Length != 11)
                msg += "NSS Inválido, debe tener 11 dígitos, en caso de no poseer dejar el campo en blanco. ";
            if (string.IsNullOrEmpty(msg)) return res;
            return new RenominacionExternaResponse
            {
                Folio = string.Empty,
                Respuesta = 1,
                Descripcion = msg
            };
        }

        /// <summary>
        /// Inserta datos de renominacion
        /// </summary>
        /// <param name="idUser">id de cliente</param>
        /// <param name="idApp">id de tipo de app</param>
        /// <param name="producto">Producto</param>
        /// <param name="tarjeta">Cuatro ultimos digitos de la tarjeta</param>
        public void InsertaRenominacionVCard(int idUser, int idApp, string producto, string tarjeta)
        {
            try
            {
                var webService = new BroxelService();
                
                using (var ctx = new broxelco_rdgEntities())
                {
                    var creaClienteSinTarjetaLog =
                        ctx.CreaClienteSinTarjetaLog.FirstOrDefault(
                            x => x.idUsuarioOnlineBroxel == idUser && x.idApp == idApp);
                    if (creaClienteSinTarjetaLog == null)
                        return;
                    var fechaNac = "";
                    if (creaClienteSinTarjetaLog.fechaNacimiento != null)
                        fechaNac = ((DateTime)creaClienteSinTarjetaLog.fechaNacimiento).ToString("yyyy-MM-dd");

                    var originacion = new OriginacionData
                    {
                        NumCuenta = creaClienteSinTarjetaLog.cuenta,
                        Producto = producto,
                        Calle = creaClienteSinTarjetaLog.calle,
                        NumExterior = creaClienteSinTarjetaLog.numeroExt,
                        NumInterior = creaClienteSinTarjetaLog.numeroInt,
                        Colonia = creaClienteSinTarjetaLog.colonia,
                        CodigoPostal = creaClienteSinTarjetaLog.codigoPostal,
                        Municipio = creaClienteSinTarjetaLog.delegacionMunicipio,
                        TelefonoMovil = creaClienteSinTarjetaLog.celular,
                        FechaNacimiento = fechaNac,
                        Genero = creaClienteSinTarjetaLog.sexo == "H" ? "M" : "F",
                        Nombre = creaClienteSinTarjetaLog.pNombre + " " + creaClienteSinTarjetaLog.sNombre,
                        ApellidoPaterno = creaClienteSinTarjetaLog.aPaterno,
                        ApellidoMaterno = creaClienteSinTarjetaLog.aMaterno,
                        NumTarjeta = tarjeta
                    };
                    if (!PreActualizaMaquila(originacion, creaClienteSinTarjetaLog.cuenta))
                        return;
                    var folio = InsertSolicitudRenominacion(creaClienteSinTarjetaLog.claveCliente, producto, true);
                    if (folio == "")
                        return;
                    if (!InsertRenominacionesInternas(originacion, folio))
                        return;
                    creaClienteSinTarjetaLog.folioRenominacion = folio;
                    ctx.Entry(creaClienteSinTarjetaLog).State = EntityState.Modified;
                    ctx.SaveChanges();
                    webService.Renominar(folio, "webService");
                }
            }
            catch (Exception e)
            {
                Helper.SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com", "Error de renominacion service", "La renominacion fallo :  " + e, "yMQ3E3ert6", "Broxel : Renominacion Service");
            }
        }

        #endregion
        #region Metodos Privados
        /// <summary>
        /// Preactualiza la maquila para que el proceso de Renominación funcione sin problemas
        /// </summary>
        /// <param name="originacion">Datos de originacion</param>
        /// <param name="numCuenta">Numero de cuenta</param>
        /// <returns></returns>
        private bool PreActualizaMaquila(OriginacionData originacion, string numCuenta)
        {
            bool res;
            try
            {
                using (var ctx = new broxelco_rdgEntities())
                {
                    var maquilas = ctx.maquila.Where(x => x.num_cuenta == numCuenta).ToList();
                    foreach (var maquila in maquilas)
                    {
                        maquila.nombre_titular = originacion.Nombre + " " + originacion.ApellidoPaterno + " " + originacion.ApellidoMaterno;
                        maquila.nombre_tarjethabiente = originacion.Nombre + " " + originacion.ApellidoPaterno + " " + originacion.ApellidoMaterno;
                        maquila.domicilio = originacion.Calle;
                        maquila.NumeroCalle = originacion.NumExterior;
                        maquila.localidad = originacion.Municipio;
                        maquila.Colonia = originacion.Colonia;
                        maquila.codigo_postal = originacion.CodigoPostal;
                        maquila.Telefono = originacion.TelefonoMovil;
                        maquila.Calle = originacion.Calle;
                        maquila.ColoniaFiscal = originacion.Colonia;
                        maquila.LocalidadFiscal = originacion.Municipio;
                        maquila.MunicipioFiscal = originacion.Municipio;
                        maquila.NumExterior = originacion.NumExterior;
                        maquila.NumInterior = originacion.NumInterior;
                        maquila.FechaDeNacimiento =  String.IsNullOrEmpty(originacion.FechaNacimiento) ? ObtenerFechaNacimientoPorRFC(originacion.Rfc) :  DateTime.Parse(originacion.FechaNacimiento);
                        if (originacion.Genero != null)
                            maquila.Sexo = originacion.Genero;
                        if (originacion.EstadoCivil != null)
                            maquila.EstadoCivil = originacion.EstadoCivil;
                        ctx.Entry(maquila).State = EntityState.Modified;
                        ctx.SaveChanges();
                    }
                    res = true;
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine("RenominaBL:PreActualizaMaquila. Error al actualizar la maquila de la cuenta " + numCuenta +": " + e);
                res = false;
            }
            return res;
        }

        /// <summary>
        /// Preactualiza la maquila para que el proceso de Renominación externa funcione sin problemas
        /// </summary>
        /// <param name="renominacion">Datos de la renominación</param>
        /// <param name="obtCuenta">Numero de cuenta</param>
        /// <returns></returns>
        private static void PreActualizaMaquilaRenExterna(RenominacionExternaData renominacion, bool obtCuenta)
        {
            try
            {
                using (var ctx = new broxelco_rdgEntities())
                {
                    var maquilas = ctx.maquila.Where(x => x.num_cuenta == renominacion.NumeroCuenta).ToList();
                    foreach (var maquila in maquilas)
                    {
                        maquila.nombre_titular = renominacion.NombreCompleto;
                        maquila.nombre_tarjethabiente = renominacion.NombreCompleto;
                        maquila.domicilio = renominacion.Calle;
                        maquila.NumeroCalle = renominacion.NumExterior;
                        maquila.localidad = renominacion.Municipio;
                        maquila.Colonia = renominacion.Colonia;
                        maquila.codigo_postal = renominacion.CodigoPostal;
                        maquila.Telefono = renominacion.TelefonoMovil;
                        maquila.Ocupacion = "EMPLEADO";
                        maquila.grupo_cuenta = "009";
                        if(obtCuenta) maquila.C4ta_linea = renominacion.CampoUnivoco;
                        maquila.email = renominacion.Email;
                        maquila.CodigoPostalFiscal = renominacion.CodigoPostal;
                        maquila.Calle = renominacion.Calle;
                        maquila.ColoniaFiscal = renominacion.Colonia;
                        maquila.LocalidadFiscal = renominacion.Municipio;
                        maquila.MunicipioFiscal = renominacion.Municipio;
                        maquila.NumExterior = renominacion.NumExterior;
                        maquila.NumInterior = renominacion.NumInterior;
                        maquila.NombreCompleto = renominacion.NombreCompleto;
                        maquila.Pais = "MX";
                        maquila.RFC = renominacion.Rfc;
                        maquila.CURP = renominacion.Curp;
                        maquila.FechaDeNacimiento = renominacion.FechaNacimiento;
                        if (!string.IsNullOrEmpty((renominacion.Nss)))
                            maquila.IMSS = renominacion.Nss;
                        if (!string.IsNullOrEmpty(renominacion.Genero))
                            maquila.Sexo = renominacion.Genero;
                        if (!string.IsNullOrEmpty(renominacion.EstadoCivil))
                            maquila.EstadoCivil = renominacion.EstadoCivil;
                        if (!string.IsNullOrEmpty(renominacion.TieneHijos))
                            maquila.Hijos = renominacion.TieneHijos;
                        ctx.Entry(maquila).State = EntityState.Modified;
                        ctx.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine("RenominaBL:PreActualizaMaquila. Error al actualizar la maquila de la cuenta " + renominacion.NumeroCuenta + ": " + e);
            }
        }

        /// <summary>
        /// Inserta Solicitud de renominacion
        /// </summary>
        /// <param name="claveCliente">Clave de Cliente</param>
        /// <param name="producto">Producto</param>
        /// <param name="emailDefault">Indica si la solicitud de renominación se hace a nombre de asignacion de lineas o el correo del cliente.</param>
        /// <returns></returns>
        private string InsertSolicitudRenominacion(string claveCliente, string producto, bool emailDefault)
        {
            return new MySqlDataAccess().InsertSolicitudRenominacion(claveCliente, producto, emailDefault);
        }
        
        /// <summary>
        /// Inserta en la tabla renominaciones internas
        /// </summary>
        /// <param name="originacion">Informacion de la renominacion</param>
        /// <param name="folio">folio de la solicitud de renominacion</param>
        /// <returns></returns>
        private bool InsertRenominacionesInternas(OriginacionData originacion, string folio)
        {
            var res = false;
            try
            {
                // Obtenemos los codigos de ciudad y estado para credencial en base al codigo postal
                // Por defecto, se toman los de campeche.
                var codigoEstado = "H";
                var codigoCiudad = "H01";
                try
                {
                    using (var ctx = new Broxel_MejoravitEntities())
                    {
                        var cpData = ctx.CPSepomex.FirstOrDefault(c => c.d_codigo == originacion.CodigoPostal);
                        if (cpData != null)
                        {
                            codigoCiudad = cpData.ciudadCredencial;
                            codigoEstado = cpData.estadoCredencial;
                        }
                    }
                }
                catch (Exception e)
                {
                    Trace.WriteLine("Error al obtener los datos de credencial a partir del codigo postal " + originacion.CodigoPostal + ": " + e);
                }

                // Obtener fecha de nacimiento a partir de RFC 
                if (string.IsNullOrEmpty(originacion.FechaNacimiento))
                {
                    var fechaNac = DateTime.Now;
                    if (Regex.IsMatch(originacion.Rfc, @"^([A-Z\s]{4})\d{6}([A-Z\w]{3})$"))
                    {
                        // Fisicas
                        try
                        {
                            fechaNac = DateTime.ParseExact(originacion.Rfc.Substring(4, 6), "yyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                        }
                        catch (Exception e)
                        {
                            Trace.WriteLine("Error al obtener fecha de nacimiento a partir del RFC: " + e);
                        }
                    }
                    if (Regex.IsMatch(originacion.Rfc, @"^([A-Z\s]{3})\d{6}([A-Z\w]{3})$"))
                    {
                        // Morales
                        try
                        {
                            fechaNac = DateTime.ParseExact(originacion.Rfc.Substring(3, 6), "yyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                        }
                        catch (Exception e)
                        {
                            Trace.WriteLine("Error al obtener fecha de nacimiento a partir del RFC: " + e);
                        }

                    }
                    originacion.FechaNacimiento = fechaNac.ToString("yyyy-MM-dd");
                }

                var nombreCompleto = originacion.Nombre + " " + originacion.ApellidoPaterno + " " +
                                     originacion.ApellidoMaterno;
                nombreCompleto = (nombreCompleto.Length > 35 ? nombreCompleto.Substring(0, 35) : nombreCompleto).Trim();

                var tarjeta = originacion.NumTarjeta.Substring(originacion.NumTarjeta.Length - 4, 4);

                res = new MySqlDataAccess().InsertRenominacionInterna(folio,
                    originacion.NumCuenta, tarjeta,
                    originacion.Producto, originacion.ClaveCliente,
                    originacion.Calle.Length > 30 ? originacion.Calle.Substring(0, 30) : originacion.Calle,
                    originacion.NumExterior + " " + originacion.NumInterior,
                    originacion.Colonia.Length > 15 ? originacion.Colonia.Substring(0, 15) : originacion.Colonia,
                    originacion.CodigoPostal,
                    originacion.Municipio.Length > 25 ? originacion.Municipio.Substring(0, 25) : originacion.Municipio,
                    "CASA", codigoEstado,
                    codigoCiudad,
                    "",
                    "IFE",
                    originacion.TelefonoMovil,
                    originacion.FechaNacimiento,
                    "S", "N",
                    originacion.Genero,
                    "",
                    nombreCompleto,
                    nombreCompleto);
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Error al insertar renominaciones Internas: " + ex);
                res = false;
            }
            return res;
        }


        /// <summary>
        /// Obtiene la fecha de nacimiento a partir del rfc
        /// </summary>
        /// <param name="rfc">Informacion de la renominacion</param>
        /// <returns>DateTime</returns>
        private DateTime ObtenerFechaNacimientoPorRFC(string rfc)
        {
            DateTime fechaNac = DateTime.Now;
            if (Regex.IsMatch(rfc, @"^([A-Z\s]{4})\d{6}([A-Z\w]{3})$"))
            {
                // Fisicas
                try
                {
                    fechaNac = DateTime.ParseExact(rfc.Substring(4, 6), "yyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                }
                catch (Exception e)
                {
                    Trace.WriteLine("Error al obtener fecha de nacimiento a partir del RFC: " + e);
                }
            }
            if (Regex.IsMatch(rfc, @"^([A-Z\s]{3})\d{6}([A-Z\w]{3})$"))
            {
                // Morales
                try
                {
                    fechaNac = DateTime.ParseExact(rfc.Substring(3, 6), "yyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                }
                catch (Exception e)
                {
                    Trace.WriteLine("Error al obtener fecha de nacimiento a partir del RFC: " + e);
                }

            }
            return fechaNac;
        }



        #endregion
    }
}