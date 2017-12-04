using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using SysX509 = System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Collections;
using System.Net;
using System.Configuration;

using Org.BouncyCastle.X509;
using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1;

using GOB.SPF.ConecII.AccessData;
using GOB.SPF.ConecII.AccessData.Repositories;
using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.Library;



namespace GOB.SPF.ConecII.Business
{
    public class FirmaDigital
    {

        #region Class Variables       
        private enum estadoCertificado { Good = 0, Revoked = 1, Unknown = 2 };
        private byte[] _certByte;
               
        #endregion

        #region Properties

        public int Identificador { get; private set; }       
        public string Certificado { get; private set; }
        public string Llave { get; private set; }
        public string Password { get; private set; }
        public string Razon { get; private set; }
        public string UrlSAT { get; private set; }
        public byte[] Firma { get; private set;  }
        public string CadenaOriginal { get; set; }

        #endregion

        #region Constructors

        public FirmaDigital()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identificador"></param>
        /// <param name="idTipo"></param>
        /// <param name="certificado"></param>
        /// <param name="llave"></param>
        /// <param name="password"></param>
        /// <param name="razon"></param>
        /// <param name="urlSAT"></param>
        /// <remarks>
        /// TODO: no se necsita el tipo de documento en este componente?
        /// </remarks>
        public FirmaDigital(
            int identificador,           
            string certificado, 
            string llave, 
            string password, 
            string razon)
        {
            
            if (identificador == 0)
                throw new ConecException("Identificador inválido");
            
            if(String.IsNullOrEmpty(certificado))
                throw new ConecException("Certificado inválido");

            if (String.IsNullOrEmpty(llave))
                throw new ConecException("Llave inválida");

            UrlSAT = ConfigurationManager.AppSettings["urlFirmaSAT"] != null ? ConfigurationManager.AppSettings["urlFirmaSAT"].To<string>() : null;

            if (String.IsNullOrEmpty(UrlSAT))
                throw new ConecException("URL SAT invalida");
            
            

            Identificador = identificador;          
            Certificado = certificado;
            Llave = llave;
            Password = password;
            Razon = razon;
            _certByte = Convert.FromBase64String(certificado);



        }
        #endregion


        /// <summary>
        /// Agrega un certificado emisor
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public int AgregarCertificadoEmisor(string file)
        {
            var bytearray = Convert.FromBase64String(file);

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repository = new RepositoryFirmaDigital(uow);

                var result =  repository.AgregarCertificadoEmisor(bytearray);

                uow.SaveChanges();

                return result;
            }
               
        }

        #region Public Methods

        /// <summary>
        /// Regresa la firma digital
        /// </summary>
        public byte[] GenerarFirma(string cadenaOriginal)
        {
            if (String.IsNullOrEmpty(cadenaOriginal))
                throw new ConecException("Cadena original inválida");

            CadenaOriginal = CadenaOriginal;

            _certByte = Convert.FromBase64String(Certificado);

            byte[] keyByte = Convert.FromBase64String(Llave);

            X509Certificate2 certificado = new X509Certificate2(_certByte);

            if (certificado.NotAfter < DateTime.Now)
            {
                throw new ConecException("El certificado no se encuentra vigente, favor de ingresar otro.");
            }

            if (certificado.NotBefore > DateTime.Now)
            {
                throw new ConecException("El certificado no se encuentra vigente, favor de ingresar otro.");
            }

            var rsa = DecodePrivateKeyInfo(keyByte, Password);

            if (rsa == null)
            {
                throw new ConecException("La contraseña proporcionada no corresponde a la llave privada.");
            }

            // verficar la correspondencia entre el certificado y la clave privada asociada al certificado
            RSAParameters parametrosLlavePrivada = rsa.ExportParameters(true);
            byte[] bytModuloLlavePrivada = parametrosLlavePrivada.Modulus;

            RSACryptoServiceProvider llavePublica = certificado.PublicKey.Key as RSACryptoServiceProvider;
            RSAParameters parametrosLlavePublica = llavePublica.ExportParameters(false);
            byte[] bytModuloLlavePublica = parametrosLlavePublica.Modulus;

            if (!CompareBytearrays(bytModuloLlavePrivada, bytModuloLlavePublica))
            {
                throw new ConecException("El certificado y la llave privada no corresponden.");
            }

            int status = 0;

            var emisores = ObtenerEmisores();
            if (emisores == null)
            {
                throw new ConecException("No se encontraron los certidicaos emisores");
            }
            foreach (byte[] emisor in emisores)
            {
                status = ValidaEmisorVigencia(_certByte, emisor);
                if (status == 100)
                    break;
            }

            switch (status)
            {
                case 0:
                    throw new ConecException("Error en el servicio de validación del certificado.");
                case 1:
                    throw new ConecException("El certificado está revocado.");
                case -1:
                    throw new ConecException("El certificado no pudo ser validado.");
                case -2:
                    throw new ConecException("el emisor no corresponde");
            }
            
            Firma = ObtenerFirma(_certByte, keyByte, Password, CadenaOriginal, Razon);

            if (Firma == null)
                throw new ConecException("Ereror al obtener la firma digital");

            return Firma;

        }

        /// <summary>
        /// regresa informacion del certificado
        /// </summary>
        /// <param name="certificado"></param>
        /// <returns></returns>
        public Certificado ObtenerCertificado()
        {
            Certificado result = null;           
            X509CertificateParser objCP = new X509CertificateParser();
            Org.BouncyCastle.X509.X509Certificate[] certificado = new Org.BouncyCastle.X509.X509Certificate[] { objCP.ReadCertificate(_certByte) };

            if (certificado[0] != null)
            {
                result = new Certificado();
                result.Serie = certificado[0].SerialNumber.To<string>();
                result.FechaInicial = certificado[0].NotBefore;
                result.FechaFinal = certificado[0].NotAfter;
            }

            return result;


        }
        #endregion

        #region Private Methods
        

        /// <summary>
        /// Regresa la lista de posibles certificados emisores
        /// </summary>
        /// <returns>
        /// </returns>
        private  List<byte[]> ObtenerEmisores()
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var repository = new RepositoryFirmaDigital(uow);
                return repository.ObtenerEmisores();
            }

        }

        
        /// <summary>
        /// Valida certificado emisor y la vigencia
        /// </summary>
        /// <param name="cert"></param>
        /// <param name="certEmisor">base64String</param>
        /// <returns></returns>
        /// <remarks>
        /// Valida el certificado y hace una llamada al SAT para obtener la vigencia.
        /// </remarks>
        private int ValidaEmisorVigencia(byte[] cert, byte[] certEmisor)
        {
            int intRegresa = 0;

            SysX509.X509Certificate2 objCert = new SysX509.X509Certificate2(cert);
            X509CertificateParser objCP = new X509CertificateParser();

            Org.BouncyCastle.X509.X509Certificate[] certificadoValidar = new Org.BouncyCastle.X509.X509Certificate[] { objCP.ReadCertificate(objCert.RawData) };

            X509CertificateParser objCP1 = new X509CertificateParser();

            Org.BouncyCastle.X509.X509Certificate[] certificadoEmisor = new Org.BouncyCastle.X509.X509Certificate[] { objCP1.ReadCertificate(certEmisor) };

            if (certificadoValidar[0].IssuerDN.Equals(certificadoEmisor[0].SubjectDN))
            {

                estadoCertificado estadoCertificado = Consulta(certificadoValidar[0], certificadoEmisor[0]);
                switch (estadoCertificado)
                {
                    case estadoCertificado.Revoked:
                        intRegresa = 1; // "El certificado está revocado";
                        break;
                    case estadoCertificado.Unknown:
                        intRegresa = -1; // "El certificado no pudo se validado";
                        break;
                    case estadoCertificado.Good:
                        intRegresa = 100; // "El certificado está vigente";
                        break;
                    default:
                        intRegresa = 0;
                        break;
                }
            }
            else
            {
                intRegresa = -2; // el emisor no corresponde
            }
            return intRegresa;
        }

        /// <summary>
        /// Regresa el Sello Digital
        /// </summary>
        /// <param name="certFile"></param>
        /// <param name="keyFile"></param>
        /// <param name="password"></param>
        /// <param name="cadenaOriginal"></param>
        /// <param name="razon"></param>
        /// <returns></returns>
        private byte[] ObtenerFirma(byte[] certFile, byte[] keyFile, string password, string cadenaOriginal, string razon)
        {
            string strRegresa = string.Empty;
            X509Certificate2 objCert = new X509Certificate2(certFile);
            RSACryptoServiceProvider rsa = OpenKeyFile(keyFile, password);
            RSACryptoServiceProvider key = objCert.PrivateKey as RSACryptoServiceProvider;
            objCert.PrivateKey = rsa;

            return FirmaCadena(objCert, ref cadenaOriginal, razon);

        }

        /// <summary>
        /// Valida contraseña y obtiene el objeto RSACryptoServiceProvider
        /// </summary>
        /// <param name="encpkcs8"></param>
        /// <param name="pPassword"></param>
        /// <returns></returns>
        private RSACryptoServiceProvider DecodePrivateKeyInfo(byte[] encpkcs8, string pPassword)
        {
            // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
            // this byte[] includes the sequence byte and terminal encoded null 
            byte[] OIDpkcs5PBES2 = { 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x05, 0x0D };
            byte[] OIDpkcs5PBKDF2 = { 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x05, 0x0C };
            byte[] OIDdesEDE3CBC = { 0x06, 0x08, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x03, 0x07 };
            byte[] seqdes = new byte[10];
            byte[] seq = new byte[11];
            byte[] salt;
            byte[] IV;
            byte[] encryptedpkcs8;
            byte[] pkcs8;

            int saltsize, ivsize, encblobsize;
            int iterations;

            // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------
            MemoryStream mem = new MemoryStream(encpkcs8);
            int lenstream = (int)mem.Length;
            BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading
            byte bt = 0;
            ushort twobytes = 0;

            try
            {

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)
                    //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();	//advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();	//advance 2 bytes
                else
                    return null;

                twobytes = binr.ReadUInt16();	//inner sequence
                if (twobytes == 0x8130)
                    binr.ReadByte();
                else if (twobytes == 0x8230)
                    binr.ReadInt16();


                seq = binr.ReadBytes(11);		//read the Sequence OID
                if (!CompareBytearrays(seq, OIDpkcs5PBES2))	//is it a OIDpkcs5PBES2 ?
                    return null;

                twobytes = binr.ReadUInt16();	//inner sequence for pswd salt
                if (twobytes == 0x8130)
                    binr.ReadByte();
                else if (twobytes == 0x8230)
                    binr.ReadInt16();

                twobytes = binr.ReadUInt16();	//inner sequence for pswd salt
                if (twobytes == 0x8130)
                    binr.ReadByte();
                else if (twobytes == 0x8230)
                    binr.ReadInt16();

                seq = binr.ReadBytes(11);		//read the Sequence OID
                if (!CompareBytearrays(seq, OIDpkcs5PBKDF2))	//is it a OIDpkcs5PBKDF2 ?
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)
                    binr.ReadByte();
                else if (twobytes == 0x8230)
                    binr.ReadInt16();

                bt = binr.ReadByte();
                if (bt != 0x04)		//expect octet string for salt
                    return null;
                saltsize = binr.ReadByte();
                salt = binr.ReadBytes(saltsize);

                bt = binr.ReadByte();
                if (bt != 0x02) 	//expect an integer for PBKF2 interation count
                    return null;

                int itbytes = binr.ReadByte();	//PBKD2 iterations should fit in 2 bytes.
                if (itbytes == 1)
                    iterations = binr.ReadByte();
                else if (itbytes == 2)
                    iterations = 256 * binr.ReadByte() + binr.ReadByte();
                else
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)
                    binr.ReadByte();
                else if (twobytes == 0x8230)
                    binr.ReadInt16();


                seqdes = binr.ReadBytes(10);		//read the Sequence OID
                if (!CompareBytearrays(seqdes, OIDdesEDE3CBC))	//is it a OIDdes-EDE3-CBC ?
                    return null;

                bt = binr.ReadByte();
                if (bt != 0x04)		//expect octet string for IV
                    return null;
                ivsize = binr.ReadByte();	// IV byte size should fit in one byte (24 expected for 3DES)
                IV = binr.ReadBytes(ivsize);

                bt = binr.ReadByte();
                if (bt != 0x04)		// expect octet string for encrypted PKCS8 data
                    return null;


                bt = binr.ReadByte();

                if (bt == 0x81)
                    encblobsize = binr.ReadByte();	// data size in next byte
                else if (bt == 0x82)
                    encblobsize = 256 * binr.ReadByte() + binr.ReadByte();
                else
                    encblobsize = bt;		// we already have the data size


                encryptedpkcs8 = binr.ReadBytes(encblobsize);
                SecureString secpswd = new SecureString();
                foreach (char c in pPassword)
                    secpswd.AppendChar(c);

                pkcs8 = DecryptPBDK2(encryptedpkcs8, salt, IV, secpswd, iterations);
                if (pkcs8 == null)	// probably a bad pswd entered.
                    return null;

                RSACryptoServiceProvider rsa = DecodePrivateKeyInfo(pkcs8);
                return rsa;
            }

            catch (Exception)
            {
                return null;
            }

            finally { binr.Close(); }

        }
        private byte[] DecryptPBDK2(byte[] edata, byte[] salt, byte[] IV, SecureString secpswd, int iterations)
        {
            CryptoStream decrypt = null;

            IntPtr unmanagedPswd = IntPtr.Zero;
            byte[] psbytes = new byte[secpswd.Length];
            unmanagedPswd = Marshal.SecureStringToGlobalAllocAnsi(secpswd);
            Marshal.Copy(unmanagedPswd, psbytes, 0, psbytes.Length);
            Marshal.ZeroFreeGlobalAllocAnsi(unmanagedPswd);

            try
            {
                Rfc2898DeriveBytes kd = new Rfc2898DeriveBytes(psbytes, salt, iterations);
                TripleDES decAlg = TripleDES.Create();
                decAlg.Key = kd.GetBytes(24);
                decAlg.IV = IV;
                MemoryStream memstr = new MemoryStream();
                decrypt = new CryptoStream(memstr, decAlg.CreateDecryptor(), CryptoStreamMode.Write);
                decrypt.Write(edata, 0, edata.Length);
                decrypt.Flush();
                decrypt.Close();	// this is REQUIRED.
                byte[] cleartext = memstr.ToArray();
                return cleartext;
            }
            catch (Exception e)
            {
                Console.WriteLine("Problem decrypting: {0}", e.Message);
                return null;
            }
        }
        private RSACryptoServiceProvider DecodePrivateKeyInfo(byte[] pkcs8)
        {
            // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
            // this byte[] includes the sequence byte and terminal encoded null 
            byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            byte[] seq = new byte[15];
            // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------
            MemoryStream mem = new MemoryStream(pkcs8);
            int lenstream = (int)mem.Length;
            BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading
            byte bt = 0;
            ushort twobytes = 0;

            try
            {

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)	//data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();	//advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();	//advance 2 bytes
                else
                    return null;


                bt = binr.ReadByte();
                if (bt != 0x02)
                    return null;

                twobytes = binr.ReadUInt16();

                if (twobytes != 0x0001)
                    return null;

                seq = binr.ReadBytes(15);		//read the Sequence OID
                if (!CompareBytearrays(seq, SeqOID))	//make sure Sequence for OID is correct
                    return null;

                bt = binr.ReadByte();
                if (bt != 0x04)	//expect an Octet string 
                    return null;

                bt = binr.ReadByte();		//read next byte, or next 2 bytes is  0x81 or 0x82; otherwise bt is the byte count
                if (bt == 0x81)
                    binr.ReadByte();
                else
                    if (bt == 0x82)
                    binr.ReadUInt16();
                //------ at this stage, the remaining sequence should be the RSA private key

                byte[] rsaprivkey = binr.ReadBytes((int)(lenstream - mem.Position));
                RSACryptoServiceProvider rsacsp = DecodeRSAPrivateKey(rsaprivkey);
                return rsacsp;
            }

            catch (Exception)
            {
                return null;
            }

            finally { binr.Close(); }
        }
        private RSACryptoServiceProvider DecodeRSAPrivateKey(byte[] privkey)
        {
            byte[] MODULUS, E, D, P, Q, DP, DQ, IQ;

            // ---------  Set up stream to decode the asn.1 encoded RSA private key  ------
            MemoryStream mem = new MemoryStream(privkey);
            BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading
            byte bt = 0;
            ushort twobytes = 0;
            int elems = 0;
            try
            {
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)	//data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();	//advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();	//advance 2 bytes
                else
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102)	//version number
                    return null;
                bt = binr.ReadByte();
                if (bt != 0x00)
                    return null;


                //------  all private key components are Integer sequences ----
                elems = GetIntegerSize(binr);
                MODULUS = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                E = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                D = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                P = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                Q = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                DP = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                DQ = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                IQ = binr.ReadBytes(elems);

                Console.WriteLine("showing components ..");

                // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider((new CspParameters
                {
                    Flags = CspProviderFlags.UseMachineKeyStore,

                    KeyContainerName = String.Format("MyPrefix {{{0}}}", Guid.NewGuid())
                }));
                RSAParameters RSAparams = new RSAParameters();
                RSAparams.Modulus = MODULUS;
                RSAparams.Exponent = E;
                RSAparams.D = D;
                RSAparams.P = P;
                RSAparams.Q = Q;
                RSAparams.DP = DP;
                RSAparams.DQ = DQ;
                RSAparams.InverseQ = IQ;
                RSA.ImportParameters(RSAparams);
                return RSA;
            }
            catch (Exception)
            {
                return null;
            }
            finally { binr.Close(); }
        }

        /// <summary>
        /// Hace una llamdada al SAT y verifica el estatus del certificado
        /// </summary>
        /// <param name="certificadoValida"></param>
        /// <param name="certificadoEmisor"></param>
        /// <returns></returns>
        private estadoCertificado Consulta(Org.BouncyCastle.X509.X509Certificate certificadoValida, Org.BouncyCastle.X509.X509Certificate certificadoEmisor)
        {
            List<string> URLs = ConsultaAutorizaciondeAccesoUrlOcsp(certificadoValida);
            if (URLs.Count == 0)
            {
                throw new Exception("No se encontraron URL Ocsp en el certificado a validar.");
            }

            string strUrl = URLs[0];
            OcspReq peticionOCSP = GenerarPeticionOCSP(certificadoEmisor, certificadoValida.SerialNumber);

            byte[] bytRespuesta = EnviarDatos(strUrl, peticionOCSP.GetEncoded(), "application/ocsp-request", "application/ocsp-response");

            return ProcesaRespuestaOCSP(certificadoValida, certificadoEmisor, bytRespuesta);
        }
        private List<string> ConsultaAutorizaciondeAccesoUrlOcsp(Org.BouncyCastle.X509.X509Certificate certificado)
        {
            List<string> ocspUrls = new List<string>();

            ocspUrls.Add(UrlSAT);

            return ocspUrls;
        }
        private OcspReq GenerarPeticionOCSP(Org.BouncyCastle.X509.X509Certificate certificadoEmisor, BigInteger numeroSerie)
        {
            CertificateID id = new CertificateID(CertificateID.HashSha1, certificadoEmisor, numeroSerie);
            return GenerarPeticionOCSP(id);
        }
        private OcspReq GenerarPeticionOCSP(CertificateID id)
        {
            OcspReqGenerator ocspGenerarPeticion = new OcspReqGenerator();
            ocspGenerarPeticion.AddRequest(id);
            BigInteger nonce = BigInteger.ValueOf(new DateTime().Ticks);

            ArrayList oids = new ArrayList();
            Hashtable values = new Hashtable();

            oids.Add(OcspObjectIdentifiers.PkixOcsp);

            Asn1OctetString asn1 = new DerOctetString(new DerOctetString(new byte[] { 1, 3, 6, 1, 5, 5, 7, 48, 1, 1 }));
            values.Add(OcspObjectIdentifiers.PkixOcsp, new Org.BouncyCastle.Asn1.X509.X509Extension(false, asn1));
            ocspGenerarPeticion.SetRequestExtensions(new Org.BouncyCastle.Asn1.X509.X509Extensions(oids, values));
            return ocspGenerarPeticion.Generate();
        }
        private byte[] EnviarDatos(string strUrl, byte[] bytDatos, string strContentType, string strAcepta)
        {
            HttpWebRequest peticion = (HttpWebRequest)WebRequest.Create(strUrl);
            peticion.Method = "POST";
            peticion.ContentType = strContentType;
            peticion.ContentLength = bytDatos.Length;
            peticion.Accept = strAcepta;
            Stream stPeticion = peticion.GetRequestStream();
            stPeticion.Write(bytDatos, 0, bytDatos.Length);
            stPeticion.Close();
            HttpWebResponse respuesta = (HttpWebResponse)peticion.GetResponse();
            Stream stRespuesta = respuesta.GetResponseStream();
            byte[] bytRespuesta = ArregloBytes(stRespuesta);
            stRespuesta.Close();

            return bytRespuesta;
        }
        private estadoCertificado ProcesaRespuestaOCSP(Org.BouncyCastle.X509.X509Certificate certificadoValidar, Org.BouncyCastle.X509.X509Certificate certificadoEmisor, byte[] bytRespuesta)
        {
            OcspResp respuestaOcsp = new OcspResp(bytRespuesta);
            estadoCertificado certificadoEstado = estadoCertificado.Unknown;
            switch (respuestaOcsp.Status)
            {
                case OcspRespStatus.Successful:
                    BasicOcspResp or = (BasicOcspResp)respuestaOcsp.GetResponseObject();
                    if (or.Responses.Length == 1)
                    {
                        SingleResp respuesta = or.Responses[0];
                        ValidarCertificadoId(certificadoEmisor, certificadoValidar, respuesta.GetCertID());

                        Object certificadoEstatus = respuesta.GetCertStatus();
                        if (certificadoEstatus == Org.BouncyCastle.Ocsp.CertificateStatus.Good)
                        {
                            certificadoEstado = estadoCertificado.Good;
                        }
                        else
                        {
                            if (certificadoEstatus is Org.BouncyCastle.Ocsp.RevokedStatus)
                            {
                                certificadoEstado = estadoCertificado.Revoked;
                            }
                            else
                            {
                                if (certificadoEstatus is Org.BouncyCastle.Ocsp.UnknownStatus)
                                {
                                    certificadoEstado = estadoCertificado.Unknown;
                                }
                            }
                        }


                    }
                    break;
                default:
                    throw new Exception("Estado Desconocido " + respuestaOcsp.Status);

            }
            return certificadoEstado;
        }
        private void ValidarCertificadoId(Org.BouncyCastle.X509.X509Certificate certificadoEmisor, Org.BouncyCastle.X509.X509Certificate certificadoValidar, CertificateID idCertificado)
        {
            CertificateID nuevoId = new CertificateID(CertificateID.HashSha1, certificadoEmisor, certificadoValidar.SerialNumber);

            if (!nuevoId.SerialNumber.Equals(certificadoValidar.SerialNumber))
            {
                throw new Exception("Id invalido de certificado en la respuesta");
            }
            if (!Org.BouncyCastle.Utilities.Arrays.AreEqual(nuevoId.GetIssuerNameHash(), idCertificado.GetIssuerNameHash()))
            {
                throw new Exception("Cetificado del emisor invalido en la respuesta");
            }
        }
        private int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            byte lowbyte = 0x00;
            byte highbyte = 0x00;
            int count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02)		//expect integer
                return 0;
            bt = binr.ReadByte();

            if (bt == 0x81)
                count = binr.ReadByte();	// data size in next byte
            else
                if (bt == 0x82)
            {
                highbyte = binr.ReadByte(); // data size in next 2 bytes
                lowbyte = binr.ReadByte();
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = bt;     // we already have the data size
            }
            while (binr.ReadByte() == 0x00)
            {	//remove high order zeros in data
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);
            //last ReadByte wasn't a removed zero, so back up a byte
            return count;
        }
        private bool CompareBytearrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            int i = 0;
            foreach (byte c in a)
            {
                if (c != b[i])
                    return false;
                i++;
            }
            return true;
        }
        private byte[] ArregloBytes(Stream st)
        {
            byte[] bytBuffer = new byte[4096 * 8];
            MemoryStream ms = new MemoryStream();

            int intLeer = 0;
            while ((intLeer = st.Read(bytBuffer, 0, bytBuffer.Length)) > 0)
            {
                ms.Write(bytBuffer, 0, intLeer);
            }
            return ms.ToArray();
        }
        private RSACryptoServiceProvider OpenKeyFile(byte[] file, string pPassword)
        {
            if (file == null)
                return null;

            return DecodePrivateKeyInfo(file, pPassword);   //PKCS #8 encrypted

        }
        private byte[] FirmaCadena(SysX509.X509Certificate2 SignerCertificate, ref string strCadenaComplemento, string strRazon)
        {

            X509CertificateParser objCP = new X509CertificateParser();
            Org.BouncyCastle.X509.X509Certificate[] certificadoValidar = new Org.BouncyCastle.X509.X509Certificate[] { objCP.ReadCertificate(SignerCertificate.RawData) };

            string strCadenaOriginal = "||SERVICIO DE PROTECCIÓN FEDERAL|" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "|" +
                                        strRazon + "|" + strCadenaComplemento + "|" +
                                        certificadoValidar[0].SubjectDN.GetValueList()[0].ToString() + "|" + certificadoValidar[0].SubjectDN.GetValueList()[4].ToString() +
                                         "|" + SignerCertificate.SerialNumber +
                                        "|" + SignerCertificate.NotBefore + "|" + SignerCertificate.NotAfter + "||";
            strCadenaComplemento = strCadenaOriginal;
            Encoding unicode = Encoding.Unicode;
            Encoding utf8 = Encoding.UTF8;
            byte[] unicodeBytes = unicode.GetBytes(strCadenaOriginal);
            byte[] utf8Bytes = Encoding.Convert(unicode, utf8, unicodeBytes);

            RSACryptoServiceProvider rsaObjeto;
            rsaObjeto = (RSACryptoServiceProvider)SignerCertificate.PrivateKey;
            SHA1 sha1 = SHA1.Create();

            //convert the input text to array of bytes
            byte[] hashData = sha1.ComputeHash(utf8Bytes);

            RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider();

            byte[] encriptado = rsaObjeto.Encrypt(hashData, false);
            string strEncriptado = Convert.ToBase64String(encriptado);
            return encriptado;
        }
        private byte[] SignMsg(byte[] Message, X509Certificate2 SignerCertificate, bool Detached)
        {
            System.Security.Cryptography.Pkcs.ContentInfo contentInfo = new System.Security.Cryptography.Pkcs.ContentInfo(Message);

            //Instanciamos el objeto SignedCms con el contenedor
            SignedCms objSignedCms = new SignedCms(contentInfo, Detached);
            //Creamos el "firmante"
            CmsSigner objCmsSigner = new CmsSigner(SignerCertificate);
            objCmsSigner.SignedAttributes.Add(new Pkcs9SigningTime());

            // Include the following line if the top certificate in the
            // smartcard is not in the trusted list.
            objCmsSigner.IncludeOption = SysX509.X509IncludeOption.EndCertOnly;

            //  Sign the CMS/PKCS #7 message. The second argument is
            //  needed to ask for the pin.
            objSignedCms.ComputeSignature(objCmsSigner);

            //Encodeamos el mensaje CMS/PKCS #7
            return objSignedCms.Encode();
        }

        #endregion
    }
}
