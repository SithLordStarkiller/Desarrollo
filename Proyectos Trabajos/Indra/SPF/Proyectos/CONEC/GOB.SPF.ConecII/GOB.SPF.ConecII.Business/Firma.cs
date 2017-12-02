using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.Entities.Amatzin;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.X509;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using SysX509 = System.Security.Cryptography.X509Certificates;

namespace GOB.SPF.ConecII.Business
{
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true), Guid("0A57EAB6-5C41-4F1C-9E87-6EE91EB57761")]

    public class Firma
    {
        public string CadenaFirmada { get; private set; } = string.Empty;

        string fileNameCer;
        string fileNameKey;
        string password;

        public enum EstadoCertificado { Good = 0, Revoked = 1, Unknown = 2 };

        public enum VigenciaCertificado
        {
            ErrorEnServicio = 0,
            Revocado = 1,
            NoSePudoValidar = -1,
            Vigente = 100,
            EmisorNoCorresponde = -2
        }

        public Firma(FirmaDocumento firmaDocumento)
        {
            fileNameKey = $"{firmaDocumento.DocumentoKey.Directorio}\\{firmaDocumento.DocumentoKey.Nombre}.{firmaDocumento.DocumentoKey.Extension}";
            fileNameCer = $"{firmaDocumento.DocumentoCer.Directorio}\\{firmaDocumento.DocumentoCer.Nombre}.{firmaDocumento.DocumentoCer.Extension}";
            password = firmaDocumento.Password;
        }

        public string FirmarCadena(ref string strCadenaOriginal, string strRazon)
        {
            X509Certificate2 objCert = new X509Certificate2(fileNameCer);
            RSACryptoServiceProvider rsa = OpenKeyFile(fileNameKey, password);

            objCert.PrivateKey = rsa;

            // SE OBTIENE EL SELLO DIGITAL
            byte[] bytCadenaFirmada = FirmaCadena(objCert, ref strCadenaOriginal, strRazon);

            return Convert.ToBase64String(bytCadenaFirmada);
        }

        public string FirmarDocumento(string strRuta, string strNombreArchivo, string strCadenaOriginal, string strRazon)
        {
            string strRegresa = "";
            X509Certificate2 objCert = new X509Certificate2(fileNameCer);
            RSACryptoServiceProvider rsa = OpenKeyFile(fileNameKey, password);
            RSACryptoServiceProvider key = objCert.PrivateKey as RSACryptoServiceProvider;
            objCert.PrivateKey = rsa;
            byte[] bytCadenaFirmada;

            if (!Directory.Exists(strRuta))
            {
                strRegresa = "No existe el directorio especificado";
                return strRegresa;
            }
            if (!ExisteArchivoPdf(strRuta, strNombreArchivo))
            {
                strRegresa = "No existe el archivo original";
                return strRegresa;
            }
            // SE OBTIENE EL SELLO DIGITAL
            bytCadenaFirmada = FirmaCadena(objCert, ref strCadenaOriginal, strRazon);

            string outputFileName = strRuta + strNombreArchivo + "sello.pdf";
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outputFileName, FileMode.Create));

            document.Open();

            PdfContentByte cb = writer.DirectContent;
            writer.PageEvent = new PDFFooter(strCadenaOriginal);
            string filenamePdf = strRuta + strNombreArchivo + ".pdf";
            PdfReader reader = new PdfReader(filenamePdf);
            for (int pageNumber = 1; pageNumber < reader.NumberOfPages + 1; pageNumber++)
            {
                document.SetPageSize(reader.GetPageSizeWithRotation(1));
                document.NewPage();
                if (pageNumber == 1)
                {
                    Chunk fileRef = new Chunk(" ");
                    fileRef.SetLocalDestination(filenamePdf);
                    document.Add(fileRef);
                }

                PdfImportedPage page = writer.GetImportedPage(reader, pageNumber);

                int rotation = reader.GetPageRotation(pageNumber);
                if (rotation == 90 || rotation == 270)
                {
                    cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(pageNumber).Height);
                }
                else
                {
                    cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                }
            }
            document.NewPage();
            Paragraph paragraph = new Paragraph();
            Font titleFont = new Font(iTextSharp.text.Font.FontFamily.HELVETICA
                                      , 12
                                      , iTextSharp.text.Font.NORMAL
                                      , BaseColor.BLACK
                );
            Chunk titleChunk = new Chunk("Cadena Original:   " + "\r" + strCadenaOriginal, titleFont);
            paragraph.Add(titleChunk);
            document.Add(paragraph);

            paragraph = new Paragraph();
            Font textFont = new Font(iTextSharp.text.Font.FontFamily.HELVETICA
                                     , 12
                                     , iTextSharp.text.Font.NORMAL
                                     , BaseColor.BLACK
                );

           
            Chunk textChunk = new Chunk("Sello Digital:      " + "\r" + Convert.ToBase64String(bytCadenaFirmada), textFont);

            paragraph.Add(textChunk);

            document.Add(paragraph);
            document.Close();

            writer.Close();

            reader.Close();

            SignHashed(outputFileName, strRuta + strNombreArchivo + "firmado.pdf",
                            objCert,
                            strRazon,
                            "SERVICIO DE PROTECCIÓN FEDERAL",
                            true);
            return "firmado";
        }
        
        public string ValidarArchivos()
        {
            string strRegresa = string.Empty;

            // todos los datos son requeridos
            if (fileNameCer.Length == 0)
            {
                strRegresa = "Es necesaria la ruta del archivo que contiene el certificado"; ;
                return strRegresa;
            }
            if (fileNameKey.Length == 0)
            {
                strRegresa = "Es necesaria la ruta del archivo que contiene la llave privada"; ;
                return strRegresa;
            }
            if (password.Length == 0)
            {
                strRegresa = "Es necesaria la contraseña para la llave privada"; ;
                return strRegresa;
            }
            //valida la existencia de los archivos cer y key
            if (!File.Exists(fileNameCer))
            {
                strRegresa = "No existe el archivo .CER, favor de ingresar la ruta correcta"; ;
                return strRegresa;
            }
            if (!File.Exists(fileNameKey))
            {
                strRegresa = "No existe el archivo .KEY, favor de ingresar la ruta correcta"; ;
                return strRegresa;
            }

            // verificar que el certificado este vigente

            X509Certificate2 objCertificado = new X509Certificate2(fileNameCer);
            if (objCertificado.NotAfter < DateTime.Now)
            {
                strRegresa = "El certificado no se encuentra vigente, favor de ingresar otro"; ;
                return strRegresa;
            }
            if (objCertificado.NotBefore > DateTime.Now)
            {
                strRegresa = "El certificado no se encuentra vigente, favor de ingresar otro"; ;
                return strRegresa;
            }

            // validar que la contraseña abra la llave privada
            RSACryptoServiceProvider rsa = OpenKeyFile(fileNameKey, password);
            if (rsa == null)
            {
                strRegresa = "La contraseña proporcionada no corresponde a la llave privada"; ;
                return strRegresa;
            }

            // verficar la correspondencia entre el certificado y la clave privada asociada al certificado

            RSAParameters parametrosLlavePrivada = rsa.ExportParameters(true);
            byte[] bytModuloLlavePrivada = parametrosLlavePrivada.Modulus;

            RSACryptoServiceProvider llavePublica = objCertificado.PublicKey.Key as RSACryptoServiceProvider;
            RSAParameters parametrosLlavePublica = llavePublica.ExportParameters(false);
            byte[] bytModuloLlavePublica = parametrosLlavePublica.Modulus;

            if (!CompareBytearrays(bytModuloLlavePrivada, bytModuloLlavePublica))
            {
                strRegresa = "El certificado y la llave privada NO corresponden"; ;
                return strRegresa;
            }

            return strRegresa;
        }

        public VigenciaCertificado ValidarVigenciaCertificado(string strCertificadoEmisor)
        {
            VigenciaCertificado intRegresa = VigenciaCertificado.ErrorEnServicio;
            X509Certificate2 objCert = new X509Certificate2(fileNameCer);
            X509CertificateParser objCP = new X509CertificateParser();

            Org.BouncyCastle.X509.X509Certificate[] certificadoValidar = new Org.BouncyCastle.X509.X509Certificate[] { objCP.ReadCertificate(objCert.RawData) };

            X509CertificateParser objCP1 = new X509CertificateParser();
            byte[] bytCertificadoEmisor = Convert.FromBase64String(strCertificadoEmisor);
            Org.BouncyCastle.X509.X509Certificate[] certificadoEmisor = new Org.BouncyCastle.X509.X509Certificate[] { objCP1.ReadCertificate(bytCertificadoEmisor) };

            if (certificadoValidar[0].IssuerDN.Equals(certificadoEmisor[0].SubjectDN))
            {
                EstadoCertificado estadoCertificado = Consulta(certificadoValidar[0], certificadoEmisor[0]);
                switch (estadoCertificado)
                {
                    case EstadoCertificado.Revoked:
                        intRegresa = VigenciaCertificado.Revocado; // "El certificado está revocado";
                        break;
                    case EstadoCertificado.Unknown:
                        intRegresa = VigenciaCertificado.NoSePudoValidar; // "El certificado no pudo se validado";
                        break;
                    case EstadoCertificado.Good:
                        intRegresa = VigenciaCertificado.Vigente; // "El certificado está vigente";
                        break;
                    default:
                        intRegresa =  VigenciaCertificado.ErrorEnServicio;
                        break;
                }
            }
            else
            {
                intRegresa = VigenciaCertificado.EmisorNoCorresponde; // el emisor no corresponde
            }
            return intRegresa;
        }

        public string RegresarDatosFirmante()
        {
            string strRegresa = string.Empty;
            X509Certificate2 objCert = new X509Certificate2(fileNameCer);
            X509CertificateParser objCP = new X509CertificateParser();

            Org.BouncyCastle.X509.X509Certificate[] certificadoValidar = new Org.BouncyCastle.X509.X509Certificate[] { objCP.ReadCertificate(objCert.RawData) };
            IList lisCertificado = certificadoValidar[0].SubjectDN.GetValueList();
            string strObtiene = lisCertificado[0].ToString();
            strRegresa = strObtiene;
            strObtiene = lisCertificado[4].ToString();
            strRegresa = strRegresa + "|" + strObtiene;
            strObtiene = lisCertificado[5].ToString();
            strRegresa = strRegresa + "|" + strObtiene;

            return strRegresa;
        }

        public SysX509.X509Certificate RegresarCertificado()
        {
            SysX509.X509Certificate objCertificado = new SysX509.X509Certificate(fileNameCer);
            return objCertificado;
        }

        static RSACryptoServiceProvider OpenKeyFile(string filename, string pPassword)
        {
            RSACryptoServiceProvider rsa = null;
            byte[] keyblob = GetFileBytes(filename);
            if (keyblob == null)
                return null;

            rsa = DecodePrivateKeyInfo(keyblob, pPassword);	//PKCS #8 encrypted

            return (rsa != null) ? rsa : null;
        }

        static byte[] DecryptPBDK2(byte[] edata, byte[] salt, byte[] IV, SecureString secpswd, int iterations)
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

        static RSACryptoServiceProvider DecodePrivateKeyInfo(byte[] pkcs8)
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

        static RSACryptoServiceProvider DecodeRSAPrivateKey(byte[] privkey)
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

        static RSACryptoServiceProvider DecodePrivateKeyInfo(byte[] encpkcs8, string pPassword)
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

        bool ExisteArchivoPdf(string strRuta, string strArchivo)
        {
            return File.Exists(strRuta + strArchivo + ".pdf");
        }

        static byte[] GetFileBytes(string filename)
        {
            if (!File.Exists(filename))
                return null;
            Stream stream = new FileStream(filename, FileMode.Open);
            int datalen = (int)stream.Length;
            byte[] filebytes = new byte[datalen];
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(filebytes, 0, datalen);
            stream.Close();
            return filebytes;
        }

        static bool CompareBytearrays(byte[] a, byte[] b)
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

        static int GetIntegerSize(BinaryReader binr)
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

        //private void btnKey_Click(object sender, EventArgs e)
        //{
        //    ofdKey.ShowDialog();
        //    if (ofdKey.FileName.Length > 0)
        //    {
        //        txbKey.Text = ofdKey.FileName;
        //    }
        //}

        //private void btnCer_Click(object sender, EventArgs e)
        //{
        //    ofdCer.ShowDialog();
        //    if (ofdCer.FileName.Length > 0)
        //    {
        //        txbCer.Text = ofdCer.FileName;
        //    }
        //}

        // funciones para validacion OCSP

        static EstadoCertificado Consulta(Org.BouncyCastle.X509.X509Certificate certificadoValida,
            Org.BouncyCastle.X509.X509Certificate certificadoEmisor)
        {
            List<string> URLs = ConsultaAutorizaciondeAccesoUrlOcsp(certificadoValida);
            if (URLs.Count == 0)
            {
                throw new Exception("No se encontraron URL Ocsp en el certificado a validar");
            }

            string strUrl = URLs[0];
            OcspReq peticionOCSP = GenerarPeticionOCSP(certificadoEmisor, certificadoValida.SerialNumber);

            byte[] bytRespuesta = EnviarDatos(strUrl, peticionOCSP.GetEncoded(), "application/ocsp-request", "application/ocsp-response");

            return ProcesaRespuestaOCSP(certificadoValida, certificadoEmisor, bytRespuesta);
        }

        static byte[] EnviarDatos(string strUrl, byte[] bytDatos, string strContentType, string strAcepta)
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

        static byte[] ArregloBytes(Stream st)
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

        static List<string> ConsultaAutorizaciondeAccesoUrlOcsp(Org.BouncyCastle.X509.X509Certificate certificado)
        {
            List<string> ocspUrls = new List<string>();

            ocspUrls.Add("https://cfdi.sat.gob.mx/edofiel");
            return ocspUrls;
        }

        protected static Asn1Object ConsultaValorExtension(Org.BouncyCastle.X509.X509Certificate certificado, string strOid)
        {
            if (certificado == null)
            {
                return null;
            }

            byte[] bytArreglo = certificado.GetExtensionValue(new DerObjectIdentifier(strOid)).GetOctets();

            if (bytArreglo == null)
            {
                return null;

            }
            Asn1InputStream aInput = new Asn1InputStream(bytArreglo);
            return aInput.ReadObject();

        }

        static EstadoCertificado ProcesaRespuestaOCSP(Org.BouncyCastle.X509.X509Certificate certificadoValidar, Org.BouncyCastle.X509.X509Certificate certificadoEmisor, byte[] bytRespuesta)
        {
            OcspResp respuestaOcsp = new OcspResp(bytRespuesta);
            EstadoCertificado certificadoEstado = EstadoCertificado.Unknown;
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
                            certificadoEstado = EstadoCertificado.Good;
                        }
                        else
                        {
                            if (certificadoEstatus is Org.BouncyCastle.Ocsp.RevokedStatus)
                            {
                                certificadoEstado = EstadoCertificado.Revoked;
                            }
                            else
                            {
                                if (certificadoEstatus is Org.BouncyCastle.Ocsp.UnknownStatus)
                                {
                                    certificadoEstado = EstadoCertificado.Unknown;
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

        void ValidarRespuesta(BasicOcspResp basicRespuesta, Org.BouncyCastle.X509.X509Certificate certificadoEmisor)
        {
            ValidarFirmaRespuesta(basicRespuesta, certificadoEmisor.GetPublicKey());
            ValidarAutorizacionFirma(certificadoEmisor, basicRespuesta.GetCerts()[0]);
        }

        void ValidarAutorizacionFirma(Org.BouncyCastle.X509.X509Certificate certificadoEmisor, Org.BouncyCastle.X509.X509Certificate certificadoFirma)
        {
            if (!(certificadoEmisor.IssuerDN.Equivalent(certificadoFirma.IssuerDN) && certificadoEmisor.SerialNumber.Equals(certificadoFirma.SerialNumber)))
            {
                throw new Exception("invalido OCSP");
            }
        }

        void ValidarFirmaRespuesta(BasicOcspResp basicOcsp, Org.BouncyCastle.Crypto.AsymmetricKeyParameter parametroAsimetrico)
        {
            if (!basicOcsp.Verify(parametroAsimetrico))
            {
                throw new Exception("firma inválida");
            }
        }

        static void ValidarCertificadoId(Org.BouncyCastle.X509.X509Certificate certificadoEmisor, Org.BouncyCastle.X509.X509Certificate certificadoValidar, CertificateID idCertificado)
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

        static OcspReq GenerarPeticionOCSP(Org.BouncyCastle.X509.X509Certificate certificadoEmisor, BigInteger numeroSerie)
        {
            CertificateID id = new CertificateID(CertificateID.HashSha1, certificadoEmisor, numeroSerie);
            return GenerarPeticionOCSP(id);
        }

        static OcspReq GenerarPeticionOCSP(CertificateID id)
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

        #region Funciones de firmado

        static byte[] FirmaCadena(X509Certificate2 signerCertificate, ref string strCadenaComplemento, string strRazon)
        {

            X509CertificateParser objCP = new X509CertificateParser();
            Org.BouncyCastle.X509.X509Certificate[] certificadoValidar = new Org.BouncyCastle.X509.X509Certificate[] { objCP.ReadCertificate(signerCertificate.RawData) };

            string strCadenaOriginal = "||SERVICIO DE PROTECCIÓN FEDERAL|" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "|" +
                                        strRazon + "|" + strCadenaComplemento + "|" +
                                        certificadoValidar[0].SubjectDN.GetValueList()[0].ToString() + "|" + certificadoValidar[0].SubjectDN.GetValueList()[4].ToString() +
                                         "|" + signerCertificate.SerialNumber +
                                        "|" + signerCertificate.NotBefore + "|" + signerCertificate.NotAfter + "||";
            strCadenaComplemento = strCadenaOriginal;
            Encoding unicode = Encoding.Unicode;
            Encoding utf8 = Encoding.UTF8;
            byte[] unicodeBytes = unicode.GetBytes(strCadenaOriginal);
            byte[] utf8Bytes = Encoding.Convert(unicode, utf8, unicodeBytes);

            RSACryptoServiceProvider rsaObjeto;
            rsaObjeto = (RSACryptoServiceProvider)signerCertificate.PrivateKey;
            SHA1 sha1 = SHA1.Create();

            //convert the input text to array of bytes
            byte[] hashData = sha1.ComputeHash(utf8Bytes);

            RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider();

            byte[] encriptado = rsaObjeto.Encrypt(hashData, false);
            string strEncriptado = Convert.ToBase64String(encriptado);
            return encriptado;
        }

        static byte[] SignMsg(byte[] message, X509Certificate2 signerCertificate, bool detached)
        {
            ContentInfo contentInfo = new System.Security.Cryptography.Pkcs.ContentInfo(message);

            //Instanciamos el objeto SignedCms con el contenedor
            SignedCms objSignedCms = new SignedCms(contentInfo, detached);
            //Creamos el "firmante"
            CmsSigner objCmsSigner = new CmsSigner(signerCertificate);
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

        static void SignHashed(string source, string target, SysX509.X509Certificate2 certificate,
            string reason, string location, bool addVisibleSign)
        {
            X509CertificateParser objCP = new X509CertificateParser();
            Org.BouncyCastle.X509.X509Certificate[] objChain = new Org.BouncyCastle.X509.X509Certificate[] { objCP.ReadCertificate(certificate.RawData) };

            PdfReader objReader = new PdfReader(source);
            PdfStamper objStamper = PdfStamper.CreateSignature(objReader, new FileStream(target, FileMode.Create), '\0');
            PdfSignatureAppearance objSA = objStamper.SignatureAppearance;
            Rectangle rectanguloFirma = new Rectangle(350, 50, 35, 200);
            objSA.LocationCaption = "Firmado en: ";
            objSA.ReasonCaption = "Motivo firma: ";

            if (addVisibleSign)
            {
                objSA.SetVisibleSignature(rectanguloFirma, objReader.NumberOfPages, null);
            }
            objSA.SignDate = DateTime.Now;

            objSA.Reason = reason;
            objSA.Location = location;
            objSA.Acro6Layers = true;

            PdfSignature objSignature = new PdfSignature(PdfName.ADOBE_PPKMS, PdfName.ADBE_PKCS7_SHA1);

            objSignature.Date = new PdfDate(objSA.SignDate);
            objSignature.Name = objChain[0].SubjectDN.GetValueList()[0].ToString();

            if (objSA.Reason != null)
                objSignature.Reason = objSA.Reason;
            if (objSA.Location != null)
                objSignature.Location = objSA.Location;
            objSA.CryptoDictionary = objSignature;
            int intCSize = 5000;
            Hashtable objTable = new Hashtable();
            objTable[PdfName.CONTENTS] = intCSize * 2 + 2;
            Dictionary<PdfName, int> exc = new Dictionary<PdfName, int>();
            exc[PdfName.CONTENTS] = intCSize * 2 + 2;

            objSA.PreClose(exc);


            HashAlgorithm objSHA1 = new SHA1CryptoServiceProvider();

            Stream objStream = objSA.GetRangeStream();
            int intRead = 0;
            byte[] bytBuffer = new byte[8192];
            while ((intRead = objStream.Read(bytBuffer, 0, 8192)) > 0)
            {
                objSHA1.TransformBlock(bytBuffer, 0, intRead, bytBuffer, 0);
            }
            objSHA1.TransformFinalBlock(bytBuffer, 0, 0);

            byte[] bytPK = SignMsg(objSHA1.Hash, certificate, false);
            byte[] bytOut = new byte[intCSize];

            PdfDictionary objDict = new PdfDictionary();

            Array.Copy(bytPK, 0, bytOut, 0, bytPK.Length);
            objDict.Put(PdfName.CONTENTS, new PdfString(bytOut).SetHexWriting(true));
            objSA.Close(objDict);
            objStamper.Dispose();
            objReader.Dispose();
            objStamper.Close();
            objReader.Close();
        }
        #endregion

        public class PDFFooter : PdfPageEventHelper
        {
            string strCadena = "";
            public PDFFooter(string strCadenaOriginal)
            {
                strCadena = strCadenaOriginal;
            }

            public override void OnEndPage(PdfWriter writer, Document document)
            {
                Font fontFooter = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7);
                base.OnEndPage(writer, document);
                PdfPTable tabFot = new PdfPTable(new float[] { 1F });
                PdfPCell cell;
                tabFot.TotalWidth = document.PageSize.Width;
                cell = new PdfPCell(new Phrase(strCadena, fontFooter));
                cell.Border = 0;
                tabFot.AddCell(cell);
                tabFot.WriteSelectedRows(0, -1, 0, document.Bottom, writer.DirectContent);
            }

            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                base.OnCloseDocument(writer, document);
            }
        }

    }
}
