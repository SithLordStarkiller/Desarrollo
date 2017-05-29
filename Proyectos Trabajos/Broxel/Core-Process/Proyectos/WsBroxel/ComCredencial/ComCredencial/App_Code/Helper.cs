using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ComCredencial
{
    public static class Helper
    {
        #region CipherDecipher

        private static byte[] Base64(string type, byte[] data)
        {
            var pem = Encoding.ASCII.GetString(data);
            var header = String.Format("-----BEGIN {0}-----", type);
            var footer = String.Format("-----END {0}-----", type);
            var start = pem.IndexOf(header, StringComparison.Ordinal) + header.Length;
            var end = pem.IndexOf(footer, start, StringComparison.Ordinal);
            var base64 = pem.Substring(start, (end - start));
            return Convert.FromBase64String(base64);
        }

        private static X509Certificate2 LoadPemFile(string file)
        {
            X509Certificate2 x509 = null;
            using (var fs = File.OpenRead(file))
            {
                var data = new byte[fs.Length];
                fs.Read(data, 0, data.Length);
                if (data[0] != 0x30)
                {
                    // maybe it's ASCII PEM base64 encoded ? 
                    data = Base64("CERTIFICATE", data);
                }
                if (data != null)
                    x509 = new X509Certificate2(data);
            }
            return x509;
        }

        private static byte[] LoadPublicKey(string file)
        {
            byte[] datos = { };
            using (var fs = File.OpenRead(file))
            {
                var data = new byte[fs.Length];
                fs.Read(data, 0, data.Length);
                if (data[0] != 0x30)
                {
                    // maybe it's ASCII PEM base64 encoded ? 
                    data = Base64("PUBLIC KEY", data);
                    datos = data;
                }
            }
            return datos;
        }

        private static string LoadOpenSslPrivateKey(String filename)
        {
            if (!File.Exists(filename))
                return null;
            var sr = File.OpenText(filename);
            var pemstr = sr.ReadToEnd().Trim();
            return pemstr;
        }

        public static string CipherNip(string nip)
        {
            var cert = new X509Certificate2(LoadPemFile(AppDomain.CurrentDomain.RelativeSearchPath + "/pines.pem"));
            var rsa = (RSACryptoServiceProvider)cert.PublicKey.Key;
            return Convert.ToBase64String(rsa.Encrypt(Encoding.UTF8.GetBytes(nip), false));
        }

        public static string CipherPassCrea(string stringToCipher)
        {
            var rsa = DecodeX509PublicKey(LoadPublicKey(AppDomain.CurrentDomain.RelativeSearchPath + "/clave_rsa_sol_publ.key"));
            return (Convert.ToBase64String(rsa.Encrypt(Encoding.ASCII.GetBytes(stringToCipher), false)));
        }

        public static String DechiperCrea(string stringToDecipher)
        {
            var rsa = DecodeRsaPrivateKey(DecodeOpenSslPrivateKey(LoadOpenSslPrivateKey(AppDomain.CurrentDomain.RelativeSearchPath + "/clave_rsa_rta_priv.key")));
            return (Encoding.UTF8.GetString(rsa.Decrypt(Convert.FromBase64String(stringToDecipher), false)));
        }

        private static RSACryptoServiceProvider DecodeX509PublicKey(byte[] x509Key)
        {
            // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
            byte[] seqOid = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------
            var mem = new MemoryStream(x509Key);
            var binr = new BinaryReader(mem); //wrap Memory Stream with BinaryReader for easy reading

            try
            {
                ushort twobytes = binr.ReadUInt16();
                switch (twobytes)
                {
                    case 0x8130:
                        binr.ReadByte(); //advance 1 byte
                        break;
                    case 0x8230:
                        binr.ReadInt16(); //advance 2 bytes
                        break;
                    default:
                        return null;
                }

                var seq = binr.ReadBytes(15);
                if (!CompareBytearrays(seq, seqOid)) //make sure Sequence for OID is correct
                    return null;

                twobytes = binr.ReadUInt16();
                switch (twobytes)
                {
                    case 0x8103:
                        binr.ReadByte(); //advance 1 byte
                        break;
                    case 0x8203:
                        binr.ReadInt16(); //advance 2 bytes
                        break;
                    default:
                        return null;
                }

                var bt = binr.ReadByte();
                if (bt != 0x00) //expect null byte next
                    return null;

                twobytes = binr.ReadUInt16();
                switch (twobytes)
                {
                    case 0x8130:
                        binr.ReadByte(); //advance 1 byte
                        break;
                    case 0x8230:
                        binr.ReadInt16(); //advance 2 bytes
                        break;
                    default:
                        return null;
                }

                twobytes = binr.ReadUInt16();
                byte lowbyte;
                byte highbyte = 0x00;

                switch (twobytes)
                {
                    case 0x8102:
                        lowbyte = binr.ReadByte(); // read next bytes which is bytes in modulus
                        break;
                    case 0x8202:
                        highbyte = binr.ReadByte(); //advance 2 bytes
                        lowbyte = binr.ReadByte();
                        break;
                    default:
                        return null;
                }
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                //reverse byte order since asn.1 key uses big endian order
                var modsize = BitConverter.ToInt32(modint, 0);

                var firstbyte = binr.ReadByte();
                binr.BaseStream.Seek(-1, SeekOrigin.Current);

                if (firstbyte == 0x00)
                {
                    //if first byte (highest order) of modulus is zero, don't include it
                    binr.ReadByte(); //skip this null byte
                    modsize -= 1; //reduce modulus buffer size by 1
                }

                var modulus = binr.ReadBytes(modsize); //read the modulus bytes

                if (binr.ReadByte() != 0x02) //expect an Integer for the exponent data
                    return null;
                var expbytes = (int)binr.ReadByte();
                // should only need one byte for actual exponent data (for all useful values)
                var exponent = binr.ReadBytes(expbytes);

                // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                var rsa = new RSACryptoServiceProvider();
                var rsaKeyInfo = new RSAParameters {Modulus = modulus, Exponent = exponent};
                rsa.ImportParameters(rsaKeyInfo);
                return rsa;
            }
            catch (Exception)
            {
                return null;
            }

            finally
            {
                binr.Close();
            }
        }

        //------- Parses binary ans.1 RSA private key; returns RSACryptoServiceProvider  ---
        private static RSACryptoServiceProvider DecodeRsaPrivateKey(byte[] privkey)
        {
            // ---------  Set up stream to decode the asn.1 encoded RSA private key  ------
            var mem = new MemoryStream(privkey);
            var binr = new BinaryReader(mem);
            //wrap Memory Stream with BinaryReader for easy reading
            try
            {
                var twobytes = binr.ReadUInt16();
                switch (twobytes)
                {
                    case 0x8130:
                        binr.ReadByte();	//advance 1 byte
                        break;
                    case 0x8230:
                        binr.ReadInt16();	//advance 2 bytes
                        break;
                    default:
                        return null;
                }

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102)	//version number
                    return null;
                var bt = binr.ReadByte();
                if (bt != 0x00)
                    return null;


                //------  all private key components are Integer sequences ----
                var elems = GetIntegerSize(binr);
                var modulus = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                var e = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                var d = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                var p = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                var q = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                var dp = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                var dq = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                var iq = binr.ReadBytes(elems);

                // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                var rsa = new RSACryptoServiceProvider();
                var rsAparams = new RSAParameters
                {
                    Modulus = modulus,
                    Exponent = e,
                    D = d,
                    P = p,
                    Q = q,
                    DP = dp,
                    DQ = dq,
                    InverseQ = iq
                };
                rsa.ImportParameters(rsAparams);
                return rsa;
            }
            catch (Exception)
            {
                return null;
            }
            finally { binr.Close(); }
        }

        private static int GetIntegerSize(BinaryReader binr)
        {
            int count;
            var bt = binr.ReadByte();
            if (bt != 0x02)		//expect integer
                return 0;
            bt = binr.ReadByte();

            switch (bt)
            {
                case 0x81:
                    count = binr.ReadByte();	// data size in next byte
                    break;
                case 0x82:
                {
                    var highbyte = binr.ReadByte();
                    var lowbyte = binr.ReadByte();
                    byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                    count = BitConverter.ToInt32(modint, 0);
                }
                    break;
                default:
                    count = bt;		// we already have the data size
                    break;
            }
            while (binr.ReadByte() == 0x00)
            {	//remove high order zeros in data
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);		//last ReadByte wasn't a removed zero, so back up a byte
            return count;
        }

        //-----  Get the binary RSA PRIVATE key, decrypting if necessary ----
        private static byte[] DecodeOpenSslPrivateKey(String instr)
        {
            const String pemprivheader = "-----BEGIN RSA PRIVATE KEY-----";
            const String pemprivfooter = "-----END RSA PRIVATE KEY-----";
            var pemstr = instr.Trim();
            byte[] binkey = {};
            if (!pemstr.StartsWith(pemprivheader) || !pemstr.EndsWith(pemprivfooter))
                return null;

            var sb = new StringBuilder(pemstr);
            sb.Replace(pemprivheader, "");  //remove headers/footers, if present
            sb.Replace(pemprivfooter, "");

            var pvkstr = sb.ToString().Trim();	//get string after removing leading/trailing whitespace

            try
            {        // if there are no PEM encryption info lines, this is an UNencrypted PEM private key
                binkey = Convert.FromBase64String(pvkstr);
                return binkey;
            }
            catch (FormatException)
            {		//if can't b64 decode, it must be an encrypted private key
                //Console.WriteLine("Not an unencrypted OpenSSL PEM private key");  
            }

            var str = new StringReader(pvkstr);
            //-------- read PEM encryption info. lines and extract salt -----
            var line = str.ReadLine();
            if (line != null && !line.StartsWith("Proc-Type: 4,ENCRYPTED"))
                return null;
            var saltline = str.ReadLine();
            if (saltline != null && !saltline.StartsWith("DEK-Info: DES-EDE3-CBC,"))
                return null;
            if (saltline != null)
            {
                var saltstr = saltline.Substring(saltline.IndexOf(",", StringComparison.Ordinal) + 1).Trim();
                var salt = new byte[saltstr.Length / 2];
                for (var i = 0; i < salt.Length; i++)
                    salt[i] = Convert.ToByte(saltstr.Substring(i * 2, 2), 16);
                if (str.ReadLine() != "")
                    return null;

                //------ remaining b64 data is encrypted RSA key ----
                var encryptedstr = str.ReadToEnd();

                try
                {	//should have b64 encrypted RSA key now
                    if (encryptedstr != null) binkey = Convert.FromBase64String(encryptedstr);
                }
                catch (FormatException)
                {  // bad b64 data.
                    return null;
                }

                //------ Get the 3DES 24 byte key using PDK used by OpenSSL ----

                var despswd = new SecureString();
                despswd.AppendChar('c');
                despswd.AppendChar('r');
                despswd.AppendChar('e');
                despswd.AppendChar('d');
                despswd.AppendChar('e');
                despswd.AppendChar('n');
                despswd.AppendChar('c');
                despswd.AppendChar('i');
                despswd.AppendChar('a');
                despswd.AppendChar('l');

                var deskey = GetOpenSsl3Deskey(salt, despswd, 1, 2);    // count=1 (for OpenSSL implementation); 2 iterations to get at least 24 bytes
                if (deskey == null)
                    return null;

                //------ Decrypt the encrypted 3des-encrypted RSA private key ------
                var rsakey = DecryptKey(binkey, deskey, salt);	//OpenSSL uses salt value in PEM header also as 3DES IV
                if (rsakey != null)
                    return rsakey;	//we have a decrypted RSA private key
            }
            Console.WriteLine("Failed to decrypt RSA private key; probably wrong password.");
            return null;
        }

        // ----- Decrypt the 3DES encrypted RSA private key ----------
        private static byte[] DecryptKey(byte[] cipherData, byte[] desKey, byte[] iv)
        {
            var memst = new MemoryStream();
            var alg = TripleDES.Create();
            alg.Key = desKey;
            alg.IV = iv;
            try
            {
                var cs = new CryptoStream(memst, alg.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(cipherData, 0, cipherData.Length);
                //cs.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                return null;
            }
            var decryptedData = memst.ToArray();
            return decryptedData;
        }

        //-----   OpenSSL PBKD uses only one hash cycle (count); miter is number of iterations required to build sufficient bytes ---
        private static byte[] GetOpenSsl3Deskey(byte[] salt, SecureString secpswd, int count, int miter)
        {
            const int hashlength = 16; //MD5 bytes
            var keymaterial = new byte[hashlength * miter];     //to store contatenated Mi hashed results


            var psbytes = new byte[secpswd.Length];
            var unmanagedPswd = Marshal.SecureStringToGlobalAllocAnsi(secpswd);
            Marshal.Copy(unmanagedPswd, psbytes, 0, psbytes.Length);
            Marshal.ZeroFreeGlobalAllocAnsi(unmanagedPswd);

            //UTF8Encoding utf8 = new UTF8Encoding();
            //byte[] psbytes = utf8.GetBytes(pswd);

            // --- contatenate salt and pswd bytes into fixed data array ---
            var data00 = new byte[psbytes.Length + salt.Length];
            Array.Copy(psbytes, data00, psbytes.Length);		//copy the pswd bytes
            Array.Copy(salt, 0, data00, psbytes.Length, salt.Length);	//concatenate the salt bytes

            // ---- do multi-hashing and contatenate results  D1, D2 ...  into keymaterial bytes ----
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = null;
            var hashtarget = new byte[hashlength + data00.Length];   //fixed length initial hashtarget

            for (var j = 0; j < miter; j++)
            {
                // ----  Now hash consecutively for count times ------
                if (j == 0)
                    result = data00;   	//initialize 
                else
                {
                    if (result != null)
                    {
                        Array.Copy(result, hashtarget, result.Length);
                        Array.Copy(data00, 0, hashtarget, result.Length, data00.Length);
                    }
                    result = hashtarget;
                    //Console.WriteLine("Updated new initial hash target:") ;
                    //showBytes(result) ;
                }

                for (var i = 0; i < count; i++)
                    result = md5.ComputeHash(result);
                Array.Copy(result, 0, keymaterial, j * hashlength, result.Length);  //contatenate to keymaterial
            }
            //showBytes("Final key material", keymaterial);
            var deskey = new byte[24];
            Array.Copy(keymaterial, deskey, deskey.Length);

            Array.Clear(psbytes, 0, psbytes.Length);
            Array.Clear(data00, 0, data00.Length);
            if (result != null) Array.Clear(result, 0, result.Length);
            Array.Clear(hashtarget, 0, hashtarget.Length);
            Array.Clear(keymaterial, 0, keymaterial.Length);

            return deskey;
        }

        private static bool CompareBytearrays(ICollection<byte> a, IList<byte> b)
        {
            if (a.Count != b.Count)
                return false;
            var i = 0;
            foreach (var c in a)
            {
                if (c != b[i])
                    return false;
                i++;
            }
            return true;
        }

        #endregion

        #region broxelProcessing

        public static String GetCuentaFromTarjeta(string tarjeta)
        {
            var dbHelper = new broxelco_rdgEntities();
            try
            {
                var qry =
                    @"select `m`.`id`, `m`.`nro-corr` as `nro_corr`, `m`.`nro-tarjeta` as `nro_tarjeta`, `m`.`nombre_titular`, `m`.`num_cuenta`, `m`.`domicilio`, `m`.`piso`, `m`.`localidad`, `m`.`codigo_postal`, `m`.`provincia`, `m`.`nombre_tarjethabiente`, `m`.`limite_compras`, `m`.`limite_credito`, `m`.`imp_adelantos`, `m`.`grupo_cuenta`, `m`.`producto`, `m`.`import`, `m`.`maquila`, `m`.`saldo_restante`, `m`.`total_movimientos`, `m`.`fecha_ultimo_movimiento`, `m`.`disponible`, `m`.`fecha_disponible`, `m`.`fecha_ultima_modificacion`, `m`.`cliente_bx`, `m`.`cuenta_madre`, `m`.`programa`, `m`.`clave_cliente`, `m`.`4ta_linea` as `cuarta_linea`, `m`.`usuario_web`, `m`.`email` from maquila m join registri_broxel r on m.num_cuenta=r.NRucO WHERE left(m.`nro-tarjeta`,6)='" +
                    tarjeta.Substring(0, 6) + @"' and right(r.`folio_de_registro`,6)='" + tarjeta.Substring(6, 6) +
                    @"' and right(m.`nro-tarjeta`,4)='" + tarjeta.Substring(12, 4) + @"'and r.tipo='00' order by r.id limit 1";
                var maq = dbHelper.Database.SqlQuery<vw_maquila>(qry).ToList();
                return maq.Count == 1 ? maq[0].num_cuenta : String.Empty;
            }
            catch (Exception e)
            {
                return String.Empty;
            }
        }

        #endregion

        #region Mailing

        public static void SendMail(String from, String to, String subject, String body, String fromPassword, String fromName = "")
        {
            try
            {
                if (fromName == String.Empty)
                    fromName = from;
                var message = new MailMessage(from, to, subject, body)
                {
                    From = new MailAddress(from, fromName),
                    IsBodyHtml = true,
                };

                var emailClient = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(from, fromPassword),
                };
                emailClient.Send(message);
            }
            catch (Exception e)
            {
                var DoNothing = true;
            }
        }

        #endregion

        internal static string TruncaTarjeta(string numTarjeta)
        {
            return numTarjeta.Substring(0, 6) + "** ****" + numTarjeta.Substring(12);
        }
    }
}
