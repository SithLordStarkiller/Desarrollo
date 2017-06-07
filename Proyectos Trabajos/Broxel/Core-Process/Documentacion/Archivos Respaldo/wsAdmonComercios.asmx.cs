using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using wsBroxel.App_Code;
using wsBroxel.App_Code.VCBL;
using wsBroxel.wsComercios;
using System.IO;

namespace wsBroxel
{
    /// <summary>
    /// Summary description for wsAdmonComercios
    /// </summary>
    [WebService(Namespace = "wsAdmonComercios")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsAdmonComercios : System.Web.Services.WebService
    {

        #region PrivateMembers

        private readonly CASA_SRTMX_ComerciosPortType _comercios =
            new CASA_SRTMX_ComerciosPortTypeClient("CASA_SRTMX_ComerciosHttpsSoap11Endpoint"
                , "https://" + ConfigurationManager.AppSettings["CredencialHost"] + ":" + ConfigurationManager.AppSettings["CredencialPort"] + "/services/CASA_SRTMX_Comercios.CASA_SRTMX_ComerciosHttpsSoap11Endpoint"
                );

        private Autenticacion aut()
        {
            return new Autenticacion
            {
                Password = Helper.CipherPassCREA("bRoXeL_1.2.3.4"),
                Usuario = "broxel"
            };
        }

        private readonly String Entidad = "651";

        #endregion

        #region PrivateFunctions


        private String ConvierteUsuario(String usuario)
        {
            if (usuario == null)
                usuario = "000";
            if (usuario.Length < 3)
                usuario = usuario.PadLeft(3).Replace(' ', '0');
            if (usuario.Length >= 22)
                usuario = usuario.Substring(0, 22);
            usuario = usuario.Replace("@", String.Empty);
            usuario = usuario.Replace(".", String.Empty);
            return usuario;
        }

        private AltaComercioTPVResponse AgregarComercio(String nombreComercio, String codigoComercio, String producto, String usuario)
        {
            if ((!string.IsNullOrEmpty(nombreComercio) && nombreComercio.All(Char.IsLetterOrDigit)) &&
                (!string.IsNullOrEmpty(codigoComercio) && codigoComercio.All(Char.IsLetterOrDigit)) &&
                (!string.IsNullOrEmpty(producto) && producto.All(Char.IsLetterOrDigit)) &&
                (!string.IsNullOrEmpty(usuario) && usuario.All(Char.IsLetterOrDigit)))
            {

                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                var resp = _comercios.AltaComercio(new AltaComercioRequest1
                {
                    AltaComercioRequest = new AltaComercioRequest
                    {
                        Autenticacion = aut(),
                        DatosComercio = new AltaComercioRequestDatosComercio
                        {
                            Denominacion = nombreComercio,
                            Entidad = Entidad,
                            NroCom = codigoComercio,
                            Producto = producto,
                            Usuario = ConvierteUsuario(usuario)
                        }
                    }
                }).AltaComercioResponse;
                return new AltaComercioTPVResponse
                {
                    CodigoRespuesta = Convert.ToInt32(resp.Response.CodigoRespuesta),
                    Success = resp.Response.CodigoRespuesta == "00" ? 1 : 0,
                    FechaCreacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    UserResponse = resp.Response.Descripcion,
                    NumeroAutorizacion = resp.Response.TicketWS
                };
            }
            else
            {
                return null;
            }
        }

        private BajaComercioTPVResponse EliminarComercio(String numeroComercio, String producto, String usuario)
        {
            if ((!string.IsNullOrEmpty(numeroComercio) && numeroComercio.All(Char.IsLetterOrDigit)) &&
                (!string.IsNullOrEmpty(producto) && producto.All(Char.IsLetterOrDigit)) &&
                (!string.IsNullOrEmpty(usuario) && usuario.All(Char.IsLetterOrDigit)))
            {

                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                var resp = _comercios.BajaComercio(new BajaComercioRequest1
                {
                    BajaComercioRequest = new BajaComercioRequest
                    {
                        Autenticacion = aut(),
                        DatosComercio = new BajaComercioRequestDatosComercio
                        {
                            Entidad = Entidad,
                            NroCom = numeroComercio,
                            Producto = producto,
                            Usuario = ConvierteUsuario(usuario)
                        }
                    }
                }).BajaComercioResponse;
                return new BajaComercioTPVResponse
                {
                    CodigoRespuesta = Convert.ToInt32(resp.Response.CodigoRespuesta),
                    Success = resp.Response.CodigoRespuesta == "00" ? 1 : 0,
                    FechaCreacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    UserResponse = resp.Response.Descripcion,
                    NumeroAutorizacion = resp.Response.TicketWS,
                };
            }
            else
            {
                return new BajaComercioTPVResponse
                {
                    CodigoRespuesta = Convert.ToInt32(resp.Response.CodigoRespuesta),
                    Success = resp.Response.CodigoRespuesta == "00" ? 1 : 0,
                    FechaCreacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    UserResponse = resp.Response.Descripcion,
                    NumeroAutorizacion = resp.Response.TicketWS,
                };
            }
        }

        private ModificacionComercioTPVResponse ModificarComercio(String numeroComercio, String nombreComercio, String producto, String usuario)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            var resp = _comercios.ModificacionComercio(new ModificacionComercioRequest1
            {
                ModificacionComercioRequest = new ModificacionComercioRequest
                {
                    Autenticacion = aut(),
                    DatosComercio = new ModificacionComercioRequestDatosComercio
                    {
                        Entidad = Entidad,
                        NroCom = numeroComercio,
                        Producto = producto,
                        Usuario = ConvierteUsuario(usuario)
                    },
                    DatosModificacion = new ModificacionComercioRequestDatosModificacion
                    {
                        Denominacion = nombreComercio,
                        Producto = producto
                    }

                }
            }).ModificacionComercioResponse;
            return new ModificacionComercioTPVResponse
            {
                CodigoRespuesta = Convert.ToInt32(resp.Response.CodigoRespuesta),
                Success = resp.Response.CodigoRespuesta == "00" ? 1 : 0,
                FechaCreacion = DateTime.Now.ToString(),
                UserResponse = resp.Response.Descripcion,
                NumeroAutorizacion = resp.Response.TicketWS
            };
        }

        private ConsultaComercioProdResponse ConsultarComercioPorProducto(String pagina, String producto)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            var resp = _comercios.ConsultaComercioProd(new ConsultaComercioProdRequest1
            {
                ConsultaComercioProdRequest = new ConsultaComercioProdRequest
                {
                    Autenticacion = aut(),
                    DatosProducto = new ConsultaComercioProdRequestDatosProducto
                    {
                        Entidad = Entidad,
                        Producto = producto,
                    },
                    Pagina = pagina,
                    ZonaHoraria = "America/Mexico_City"
                }
            });
            return resp.ConsultaComercioProdResponse;
        }

        private ConsultaComercioNroResponse ConsultarComercioPorNumero(String numeroComercio)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            var resp = _comercios.ConsultaComercioNro(new ConsultaComercioNroRequest1
            {
                ConsultaComercioNroRequest = new ConsultaComercioNroRequest
                {
                    Autenticacion = aut(),
                    DatosComercio = new ConsultaComercioNroRequestDatosComercio
                    {
                        Entidad = Entidad,
                        NroCom = numeroComercio
                    },
                    ZonaHoraria = "America/Mexico_City"
                }
            });
            return resp.ConsultaComercioNroResponse;
        }

        #endregion

        #region PublicAndExposedMethods
        /// <summary>
        /// Creacion de un cliente de tipo comercio
        /// </summary>
        /// <param name="idComercio"></param>
        /// <param name="email"></param>
        /// <param name="celular"></param>
        /// <param name="clabe"></param>
        /// <returns></returns>
        [WebMethod]
        public string CrearClienteComercio(int idComercio, string email, string celular, string clabe)
        {
            return new VCardBL().CrearClienteComercio(idComercio, email, celular, clabe);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nombreComercio"></param>
        /// <param name="numeroComercio"></param>
        /// <param name="producto"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [WebMethod]
        public AltaComercioTPVResponse AltaComercio(String nombreComercio, String numeroComercio, String producto, String usuario)
        {
            return AgregarComercio(nombreComercio, numeroComercio, producto, usuario);
        }

        [WebMethod]
        public BajaComercioTPVResponse BajaComercio(String numeroComercio, String producto, String usuario)
        {
            return EliminarComercio(numeroComercio, producto, usuario);
        }

        [WebMethod]
        public ModificacionComercioTPVResponse ModificaComercio(String numeroComercio, String nombreComercio, String producto, String usuario)
        {
            return ModificarComercio(numeroComercio, nombreComercio, producto, usuario);
        }

        [WebMethod]
        public ConsultaComercioProdResponse ConsultaComerciosPorProducto(String numeroPagina, String producto)
        {
            return ConsultarComercioPorProducto(numeroPagina, producto);
        }

        [WebMethod]
        public ConsultaComercioNroResponse ConsultaDeComercioPorNumero(String numeroComercio)
        {
            return ConsultarComercioPorNumero(numeroComercio);
        }

        private bool WriteCSV(int totalPaginas, String producto)
        {
            //String[] whatever = { "4540563" };
            //var flista = whatever.ToList();
            
            string path = @"c:\temp\Aldo.csv";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("Numero, Denominacion, FechaAlta, Producto");
                    System.Diagnostics.Debug.WriteLine("Numero, Denominacion, FechaAlta, Producto");
                }
            }

            using (StreamWriter sw = File.AppendText(path))
            {
                for (int i = 1; i <= totalPaginas; i++)
                {
                    var respuesta = ConsultarComercioPorProducto(i + "", producto);
                    foreach (var comercio in respuesta.Comercios)
                    {
                        sw.WriteLine(comercio.NroCom + "," + comercio.Denominacion.Replace(',', ' ') + "," + comercio.FechaAlta + "," + producto);
                        System.Diagnostics.Debug.WriteLine(comercio.NroCom + "," + comercio.Denominacion + "," + comercio.FechaAlta + "," + producto);
                    }
                }
            }

            return true;
        }

         [WebMethod]
        public bool BatchModify()
        {
            String[] items = {"5167374", "5179171", "5182266", "5197918", "5215512", "5229828", "5244058", "5279260", "5376967", "5386024", "5389390", "5436852", "5456587", "5487723", "5576608", "5674213", "5682786", "6055669", "6157374", "6157382", "6277230", "6304901", "6304919", "6332035", "6332043", "6332050", "6360507", "7292035", "7292036", "7302880", "9247743"};

            String[] denom = { "DE DIOS REVILLAGIGEDO", "VYR MIGUEL ANGEL", "DE DIOS SAN MATEO", "VYR SANTA FE", "EXPRESS MILPA ALTA", "DE DIOS XOCHIMILCO EMB", "FARM DIOS SUC METROPOL", "FARM DIOS SUC GUSTVO B", "FARM VY MAGDALENA", "FARM DIOS CHALCO", "DE DIOS ROMERO RUBIO", "DE DIOS COACALCO", "DE DIOS UNIV", "DE DIOS ARAGON", "FARM DE COLORES", "DIOSMARINA NACIONAL", "FARM DE DIOS BARRANCA", "DE DIOS FLORENCIA", "DIOS ROJO GOMEZ", "FARM DIOS IZAZAGA", "FARM HMG", "INP INST DE PEDIATRA", "DIOS ARBOLEDAS", "DIOS CASTORENA", "DIOS LA CANADA", "DIOS LA RAZA", "DIOS TONALA", "VYR PLAZA ARAGON", "DE DIOS ROSARIO", "DE DIOS DIV DL NORTE", "FARM CLIN LONDRES" };

            for (int i = 0; i < items.Length; i++)
            {
                var respuesta = this.ModificarComercio(items[i], denom[i], "K153", "albertoortiz");
                System.Diagnostics.Debug.WriteLine(respuesta);
            }
            return true;
        }

        [WebMethod]
        public bool BatchUpload()
        {
            String[] array = { "1973403", "5203443", "1973171", "1987262", "1972561", "1972983", "5814363" };

            foreach (var item in array)
            {
                var respuesta = AgregarComercio("FARMACIAS BENAVIDES", item, "K153", "albertoortiz");
                System.Diagnostics.Debug.WriteLine(item + " - " + respuesta.CodigoRespuesta);
            }

            return true;
        }

        [WebMethod]
        public bool BatchErase()
        {
            String[] array = { "0717215", "0943464", "1008058", "1008066", "1008068", "1083872", "1296144", "1296409", "1779305", "1793470", "1793769", "1825678", "1840479", "1858190", "1899996", "1956937", "1979301", "2159838", "2186112", "2297075", "2338457", "3357720", "3357936", "3412368", "4010682", "4551321", "4876868", "5218052", "5218060", "5299581", "5508502", "5608203", "7072146", "7073560", "7148746", "7246665", "7247006", "7259021", "7312898", "7372113", "7426474", "7431782", "7456538", "7483652", "7489162", "8086712", "8943508", "9864232", "9907601", "9963190"};
            foreach (var item in array)
            {
                var respuesta = EliminarComercio(item, "K151", "raulhernandez");
                System.Diagnostics.Debug.WriteLine(item + " - " + respuesta.CodigoRespuesta);
            }
            return true;
        }






        [WebMethod]
        public bool BatchSave()
        {
            String[] productos = { "B151", "B651", "D151", "D651", "E651", "G651", "K150", "K151", "K152", "K153", "K154", "K155", "K156", "K157", "K158", "K159", "K160", "K161", "K651", "N651", "U151", "U152", "U153", "U651", "Y001"};

            foreach (String producto in productos)
            {
                System.Diagnostics.Debug.WriteLine("*****************" + producto + "********************");

                var respuesta = ConsultarComercioPorProducto(0 +"", producto);
                if (!respuesta.Response.CodigoRespuesta.Equals("00"))
                {
                    System.Diagnostics.Debug.WriteLine("Sin productos, saltando");
                    continue;
                }
                else { 
                    WriteCSV(Int32.Parse(respuesta.TotalPaginas), producto); 
                }
               

            }
            return true;
        }

        [WebMethod]
        public bool ExistenCSV() 
        {
            string path = @"c:\temp\Existentes.csv";

            String[] comercios = { };

            
            if(!File.Exists(path)) 
            {
                using(StreamWriter sw = File.CreateText(path)) 
                {
                    sw.WriteLine("Número, Existe");
                    System.Diagnostics.Debug.WriteLine("====Header====");
                    System.Diagnostics.Debug.WriteLine("Numero, Existe");
                }
            }

            using(StreamWriter sw = File.CreateText(path)) 
            {
                foreach(var comercio in comercios) 
                {
                    var respuesta = ConsultarComercioPorNumero(comercio);
                    sw.WriteLine(comercio + "," + respuesta.Response.CodigoRespuesta);
                    System.Diagnostics.Debug.WriteLine(comercio + ", " + respuesta.Response.CodigoRespuesta );
                }
            }

            return true;
           


        }
        [WebMethod]
        public bool BatchConsulta()
        {
            String[] whatever = { "6554554", "9453994", "7318868", "6378376", "4076089", "4422465", "9453937", "6718043", "9800863", "6507669", "4171807", "7444729", "7991367", "9453986", "9917006", "6529036", "4258257", "4490454", "6554505", "7908056", "9952078", "6532741", "4288361", "4452546", "9454075", "6631931", "7779051", "6631907", "4322178", "4469169", "9454034", "9454026", "7798812", "6654545", "4371944", "4447124", "9453903", "9453929", "0691675", "6718035", "4398848", "4540555", "9453895", "9510413", "6263842", "6774392", "4398657", "4540563" };

            string path = @"c:\temp\Aldo.csv";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("Numero,Denominacion,FechaAlta,Producto,Usuario");
                    System.Diagnostics.Debug.WriteLine("=====Header======");
                    System.Diagnostics.Debug.WriteLine("Numero, Denominacion, FechaAlta, Producto, Usuario");
                }

            }
            using (StreamWriter sw = File.CreateText(path))
            {
                foreach (var item in whatever)
                {
                    var respuesta = this.ConsultarComercioPorNumero(item);
                    if (respuesta.Response.CodigoRespuesta != "00")
                    {
                        sw.WriteLine(item + "," + "error" + "," + "" + "," + respuesta.Response.CodigoRespuesta + "," + "");
                        System.Diagnostics.Debug.WriteLine(item + "," + "error" + "," + "" + "," + "" + "," + "");
                    }
                    else
                    {
                        sw.WriteLine(item + "," + respuesta.Comercios.First().Denominacion +
                            "," + respuesta.Comercios.First().FechaAlta + "," + respuesta.Comercios.First().Producto
                            + "," + respuesta.Comercios.First().Usuario);
                        System.Diagnostics.Debug.WriteLine(item + "," + respuesta.Comercios.First().Denominacion +
                            "," + respuesta.Comercios.First().FechaAlta + "," + respuesta.Comercios.First().Producto
                            + "," + respuesta.Comercios.First().Usuario);
                    }
                }
            }
            return true;
        }
        

        #endregion
    }

}


