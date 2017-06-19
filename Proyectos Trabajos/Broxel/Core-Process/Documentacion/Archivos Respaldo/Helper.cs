using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Web.Services;
using System.Xml.Serialization;
using MySql.Data.MySqlClient;
using wsBroxel.App_Code;
using wsBroxel.App_Code.AsignacionLineaBL;
using wsBroxel.App_Code.CargosBL;
using wsBroxel.App_Code.SolicitudBL;
using wsBroxel.wsSMS;
using wsBroxel.wsTimbrado;
using wsBroxel.Dispatcher;

namespace wsBroxel
{
    public static class Helper
    {
        public static object tablaDispersion = new object();
        const string key = "ALT*0123+";

        #region CipherDecipher

        private static byte[] Base64(string type, byte[] data)
        {
            var pem = Encoding.ASCII.GetString(data);
            var header = string.Format("-----BEGIN {0}-----", type);
            var footer = string.Format("-----END {0}-----", type);
            var start = pem.IndexOf(header, StringComparison.Ordinal) + header.Length;
            var end = pem.IndexOf(footer, start, StringComparison.Ordinal);
            var base64 = pem.Substring(start, (end - start));
            return Convert.FromBase64String(base64);
        }

        private static X509Certificate2 LoadPEMFile(string file)
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

        private static string LoadOpenSSLPrivateKey(string filename)
        {
            if (!File.Exists(filename))
                return null;
            var sr = File.OpenText(filename);
            var pemstr = sr.ReadToEnd().Trim();
            return pemstr;
        }

        public static string CipherNIP(string nip)
        {
            var cert = new X509Certificate2(LoadPEMFile(AppDomain.CurrentDomain.RelativeSearchPath + "/pines.pem"));
            var rsa = (RSACryptoServiceProvider)cert.PublicKey.Key;
            return Convert.ToBase64String(rsa.Encrypt(Encoding.UTF8.GetBytes(nip), false));
        }

        public static string CipherPassCREA(string stringToCipher)
        {
            var rsa = DecodeX509PublicKey(LoadPublicKey(AppDomain.CurrentDomain.RelativeSearchPath + "/clave_rsa_sol_publ.key"));

            string pass = Convert.ToBase64String(rsa.Encrypt(Encoding.ASCII.GetBytes(stringToCipher), false));
            return pass;
            //  return (Convert.ToBase64String(rsa.Encrypt(Encoding.ASCII.GetBytes(stringToCipher), false)));
        }

        public static string DechiperCREA(string stringToDecipher)
        {
            var rsa = DecodeRSAPrivateKey(DecodeOpenSSLPrivateKey(LoadOpenSSLPrivateKey(AppDomain.CurrentDomain.RelativeSearchPath + "/clave_rsa_rta_priv.key")));
            return (Encoding.UTF8.GetString(rsa.Decrypt(Convert.FromBase64String(stringToDecipher), false)));
        }

        private static RSACryptoServiceProvider DecodeX509PublicKey(byte[] x509Key)
        {
            // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
            byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            var seq = new byte[15];
            // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------
            var mem = new MemoryStream(x509Key);
            var binr = new BinaryReader(mem); //wrap Memory Stream with BinaryReader for easy reading
            byte bt = 0;
            ushort twobytes = 0;

            try
            {
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte(); //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16(); //advance 2 bytes
                else
                    return null;

                seq = binr.ReadBytes(15); //read the Sequence OID
                if (!CompareBytearrays(seq, SeqOID)) //make sure Sequence for OID is correct
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8103) //data read as little endian order (actual data order for Bit String is 03 81)
                    binr.ReadByte(); //advance 1 byte
                else if (twobytes == 0x8203)
                    binr.ReadInt16(); //advance 2 bytes
                else
                    return null;

                bt = binr.ReadByte();
                if (bt != 0x00) //expect null byte next
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte(); //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16(); //advance 2 bytes
                else
                    return null;

                twobytes = binr.ReadUInt16();
                byte lowbyte = 0x00;
                byte highbyte = 0x00;

                if (twobytes == 0x8102) //data read as little endian order (actual data order for Integer is 02 81)
                    lowbyte = binr.ReadByte(); // read next bytes which is bytes in modulus
                else if (twobytes == 0x8202)
                {
                    highbyte = binr.ReadByte(); //advance 2 bytes
                    lowbyte = binr.ReadByte();
                }
                else
                    return null;
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
                var RSA = new RSACryptoServiceProvider();
                var RSAKeyInfo = new RSAParameters();
                RSAKeyInfo.Modulus = modulus;
                RSAKeyInfo.Exponent = exponent;
                RSA.ImportParameters(RSAKeyInfo);
                return RSA;
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
        private static RSACryptoServiceProvider DecodeRSAPrivateKey(byte[] privkey)
        {
            byte[] MODULUS, E, D, P, Q, DP, DQ, IQ;

            // ---------  Set up stream to decode the asn.1 encoded RSA private key  ------
            var mem = new MemoryStream(privkey);
            var binr = new BinaryReader(mem);
            //wrap Memory Stream with BinaryReader for easy reading
            byte bt = 0;
            ushort twobytes = 0;
            var elems = 0;
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

                // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                var RSA = new RSACryptoServiceProvider();
                var RSAparams = new RSAParameters();
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

        private static int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            byte lowbyte = 0x00;
            byte highbyte = 0x00;
            var count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02)		//expect integer
                return 0;
            bt = binr.ReadByte();

            if (bt == 0x81)
                count = binr.ReadByte();	// data size in next byte
            else
                if (bt == 0x82)
                {
                    highbyte = binr.ReadByte();	// data size in next 2 bytes
                    lowbyte = binr.ReadByte();
                    byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                    count = BitConverter.ToInt32(modint, 0);
                }
                else
                {
                    count = bt;		// we already have the data size
                }
            while (binr.ReadByte() == 0x00)
            {	//remove high order zeros in data
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);		//last ReadByte wasn't a removed zero, so back up a byte
            return count;
        }

        //-----  Get the binary RSA PRIVATE key, decrypting if necessary ----
        private static byte[] DecodeOpenSSLPrivateKey(string instr)
        {
            const String pemprivheader = "-----BEGIN RSA PRIVATE KEY-----";
            const String pemprivfooter = "-----END RSA PRIVATE KEY-----";
            var pemstr = instr.Trim();
            byte[] binkey;
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
            if (!str.ReadLine().StartsWith("Proc-Type: 4,ENCRYPTED"))
                return null;
            var saltline = str.ReadLine();
            if (!saltline.StartsWith("DEK-Info: DES-EDE3-CBC,"))
                return null;
            var saltstr = saltline.Substring(saltline.IndexOf(",") + 1).Trim();
            var salt = new byte[saltstr.Length / 2];
            for (var i = 0; i < salt.Length; i++)
                salt[i] = Convert.ToByte(saltstr.Substring(i * 2, 2), 16);
            if (str.ReadLine() != "")
                return null;

            //------ remaining b64 data is encrypted RSA key ----
            var encryptedstr = str.ReadToEnd();

            try
            {	//should have b64 encrypted RSA key now
                binkey = Convert.FromBase64String(encryptedstr);
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

            var deskey = GetOpenSSL3deskey(salt, despswd, 1, 2);    // count=1 (for OpenSSL implementation); 2 iterations to get at least 24 bytes
            if (deskey == null)
                return null;

            //------ Decrypt the encrypted 3des-encrypted RSA private key ------
            var rsakey = DecryptKey(binkey, deskey, salt);	//OpenSSL uses salt value in PEM header also as 3DES IV
            if (rsakey != null)
                return rsakey;	//we have a decrypted RSA private key
            else
            {
                Console.WriteLine("Failed to decrypt RSA private key; probably wrong password.");
                return null;
            }
        }

        // ----- Decrypt the 3DES encrypted RSA private key ----------
        private static byte[] DecryptKey(byte[] cipherData, byte[] desKey, byte[] IV)
        {
            var memst = new MemoryStream();
            var alg = TripleDES.Create();
            alg.Key = desKey;
            alg.IV = IV;
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
        private static byte[] GetOpenSSL3deskey(byte[] salt, SecureString secpswd, int count, int miter)
        {
            var unmanagedPswd = IntPtr.Zero;
            const int HASHLENGTH = 16; //MD5 bytes
            var keymaterial = new byte[HASHLENGTH * miter];     //to store contatenated Mi hashed results


            var psbytes = new byte[secpswd.Length];
            unmanagedPswd = Marshal.SecureStringToGlobalAllocAnsi(secpswd);
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
            var hashtarget = new byte[HASHLENGTH + data00.Length];   //fixed length initial hashtarget

            for (var j = 0; j < miter; j++)
            {
                // ----  Now hash consecutively for count times ------
                if (j == 0)
                    result = data00;   	//initialize 
                else
                {
                    Array.Copy(result, hashtarget, result.Length);
                    Array.Copy(data00, 0, hashtarget, result.Length, data00.Length);
                    result = hashtarget;
                    //Console.WriteLine("Updated new initial hash target:") ;
                    //showBytes(result) ;
                }

                for (var i = 0; i < count; i++)
                    result = md5.ComputeHash(result);
                Array.Copy(result, 0, keymaterial, j * HASHLENGTH, result.Length);  //contatenate to keymaterial
            }
            //showBytes("Final key material", keymaterial);
            var deskey = new byte[24];
            Array.Copy(keymaterial, deskey, deskey.Length);

            Array.Clear(psbytes, 0, psbytes.Length);
            Array.Clear(data00, 0, data00.Length);
            Array.Clear(result, 0, result.Length);
            Array.Clear(hashtarget, 0, hashtarget.Length);
            Array.Clear(keymaterial, 0, keymaterial.Length);

            return deskey;
        }

        private static bool CompareBytearrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
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

        public static string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        #endregion

        #region broxelProcessing

        public static string GetPetrusStatusCodeByCredentialStatusCode(string estado)
        {
            string returnValue = "1";
            switch (estado)
            {
                case "2N": returnValue = "3";
                    break;
                case "01": returnValue = "1";
                    break;
                case "03": returnValue = "12";
                    break;
                case "21": returnValue = "7";
                    break;
                case "23": returnValue = "6";
                    break;
                case "28": returnValue = "8";
                    break;
                case "2T": returnValue = "3";
                    break;
                case "2R": returnValue = "1";
                    break;
                case "2P":  returnValue = "11";
                    break;
                default:
                    break;
            }
            return returnValue;
        }
        public static int GetStatusTypeByStatusCode(string estado)
        {
            int returnValue = 1;
            switch (estado)
            {
                case "2N": returnValue = 3;
                    break;
                case "01": returnValue = 1;
                    break;
                case "03": returnValue = 4;
                    break;
                case "21": returnValue = 3;
                    break;
                case "23": returnValue = 3;
                    break;
                case "28": returnValue = 3;
                    break;
                case "2T": returnValue = 3;
                    break;
                case "2R": returnValue = 1;
                    break;
                case "2P":  returnValue = 3;
                    break;
                default:
                    break;
            }
            return returnValue;
        }

		public static int GetProcesadorFromCuenta(string cuenta)
		{
			int returnValue = 1;
			broxelco_rdgEntities dbHelper = new broxelco_rdgEntities();
			String qry = @"select Concat(left(r.`numero_tc`,6),right(rb.folio_de_registro,6),right(r.`numero_tc`,4)) as NumeroTarjeta, m.nombre_titular as NombreTarjetaHabiente, r.id as Id, replace(right(rb.fecha_de_registro,5),'-','') as FechaExpira, Concat(right(right(left(rb.transacciones,5),3),1),left(right(left(rb.transacciones,5),3),1), right(left(right(left(rb.transacciones,5),3),2),1)) as CVC, m.procesador as Procesador, m.num_cuenta NumCuenta from registro_tc r join registri_broxel rb on r.id = rb.id_de_registro join maquila m on r.numero_de_cuenta = m.num_cuenta where rb.tipo='00' and m.num_cuenta = '" + cuenta + @"'";
			var maq = dbHelper.Database.SqlQuery<MaquilaResumen>(qry).ToList();
			if (maq.Count == 1)
			{
				returnValue = maq[0].Procesador;
			}
			return returnValue;
		}

		public static int GetProcesadorFromTarjeta(string tarjea)
		{
			int returnValue = 1;
			broxelco_rdgEntities dbHelper = new broxelco_rdgEntities();
			String qry = @"select num_cuenta, procesador from maquila join registri_broxel r on `maquila`.`num_cuenta`=r.NRucO WHERE left(`maquila`.`nro-tarjeta`,6)='" + tarjea.Substring(0, 6) + @"' and right(r.`folio_de_registro`,6)='" + tarjea.Substring(6, 6) + @"' and right(`maquila`.`nro-tarjeta`,4)='" + tarjea.Substring(12, 4) + @"' order by r.id desc limit 1";
			var maq = dbHelper.Database.SqlQuery<CuentaEntity>(qry).ToList();
			if (maq.Count == 1)
			{
				returnValue = maq[0].procesador ?? 1;
			}
			return returnValue;
		}

		/// <summary>
		/// Get Process Engine by card number
		/// </summary>
		/// <param name="Tarjeta"></param>
		/// <returns></returns>
		public static DispatcherCuenta GetEngineByCard(string Tarjeta)
        {
            DispatcherCuenta cuenta = new DispatcherCuenta();
            broxelco_rdgEntities dbHelper = new broxelco_rdgEntities();

            String qry = @"select m.num_cuenta, m.procesador from maquila m join registro_tc tc on m.num_cuenta = tc.numero_de_cuenta join registri_broxel r on tc.numero_de_cuenta=r.NRucO WHERE left(tc.`numero_tc`,6)='" + Tarjeta.Substring(0, 6) + @"' and right(r.`folio_de_registro`,6)='" + Tarjeta.Substring(6, 6) + @"' and right(tc.`numero_tc`,4)='" + Tarjeta.Substring(12, 4) + @"' order by r.id desc limit 1";
            var maq = dbHelper.Database.SqlQuery<CuentaEntity>(qry).ToList();
            if (maq.Count == 1)
            {
                cuenta.NumeroCuenta = maq[0].num_cuenta;
                cuenta.Procesador = maq[0].procesador ?? 1;
            }
            return cuenta;
        }

        public static Tarjeta GetTarjetaFromCuenta(string NumCuenta)
        {
            Boolean flag;
            do
            {
                flag = false;
                try
                {
                    var dbHelper = new broxelco_rdgEntities();
                    Tarjeta t = null;
                    String qry = @"select Concat(left(r.`numero_tc`,6),right(rb.folio_de_registro,6),right(r.`numero_tc`,4)) as NumeroTarjeta, m.nombre_titular as NombreTarjetaHabiente, r.id as Id, replace(right(rb.fecha_de_registro,5),'-','') as FechaExpira, Concat(right(right(left(rb.transacciones,5),3),1),left(right(left(rb.transacciones,5),3),1), right(left(right(left(rb.transacciones,5),3),2),1)) as CVC, m.procesador as Procesador, m.num_cuenta NumCuenta from registro_tc r join registri_broxel rb on r.id = rb.id_de_registro join maquila m on r.numero_de_cuenta = m.num_cuenta where rb.tipo='00' and m.num_cuenta = '" + NumCuenta + @"'";
                    var maq = dbHelper.Database.SqlQuery<MaquilaResumen>(qry).ToList();

                    if (maq.Count > 1)
                    {
                        var max = maq.Max(x => x.Id);
                        maq = maq.Where(x => x.Id == max).ToList();
                    }
                    if (maq.Count == 1)
                    {
                        t = new Tarjeta(maq[0].NombreTarjetaHabiente, maq[0].NumeroTarjeta, maq[0].FechaExpira, maq[0].CVC, NumCuenta, maq[0].Procesador);
                    }
                    return t;
                }
                catch (System.Data.EntityCommandExecutionException e)
                {
                    flag = true;
                }
                catch (MySqlException e)
                {
                    flag = true;
                }
                catch (Exception e)
                {
                    SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com ", "Fallo armando cuenta " + NumCuenta, "Fallo armar en cuenta " + NumCuenta + " en " + GetLocalIp() + ": " + e, "yMQ3E3ert6");
                    return null;
                }
            } while (flag);
            return null;
        }

        public static Tarjeta GetTarjetaFromTarjeta(string tarjeta)
        {
            Boolean flag;
            do
            {
                flag = false;
                try
                {
                    var dbHelper = new broxelco_rdgEntities();
                    Tarjeta t = null;
                    String qry = @"select Concat(left(r.`numero_tc`,6),right(rb.folio_de_registro,6),right(r.`numero_tc`,4)) as NumeroTarjeta, m.nombre_titular as NombreTarjetaHabiente, r.id as Id, replace(right(rb.fecha_de_registro,5),'-','') as FechaExpira, Concat(right(right(left(rb.transacciones,5),3),1),left(right(left(rb.transacciones,5),3),1), right(left(right(left(rb.transacciones,5),3),2),1)) as CVC, m.procesador as Procesador, m.num_cuenta NumCuenta " +
                        "from registro_tc r join registri_broxel rb on r.id = rb.id_de_registro join maquila m on r.numero_de_cuenta = m.num_cuenta " +
                        "where left(r.numero_tc,6)='"+ tarjeta.Substring(0,6) +"' " +
                        "and right(rb.`folio_de_registro`,6)='"+ tarjeta.Substring(6,6) +"' " +
                        "and right(r.numero_tc,4)= '"+tarjeta.Substring(12,4) +"' " +
                        "order by r.id limit 1";

                    var maq = dbHelper.Database.SqlQuery<MaquilaResumen>(qry).ToList();

                    if (maq.Count > 1)
                    {
                        var max = maq.Max(x => x.Id);
                        maq = maq.Where(x => x.Id == max).ToList();
                    }
                    if (maq.Count == 1)
                    {
                        t = new Tarjeta(maq[0].NombreTarjetaHabiente, maq[0].NumeroTarjeta, maq[0].FechaExpira, maq[0].CVC, maq[0].NumCuenta, maq[0].Procesador);
                    }
                    return t;
                }
                catch (System.Data.EntityCommandExecutionException e)
                {
                    flag = true;
                }
                catch (MySqlException e)
                {
                    flag = true;
                }
                catch (Exception e)
                {
                    SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com ", "Fallo armando cuenta " + tarjeta, "Fallo armar en cuenta " + tarjeta + " en " + GetLocalIp() + ": " + e, "yMQ3E3ert6");
                    return null;
                }
            } while (flag);
            return null;
        }


        public static Tarjeta GetTarjetaFromCuentaAdicional(string NumCuenta)
        {
            Boolean flag;
            do
            {
                flag = false;
                try
                {
                    var dbHelper = new broxelco_rdgEntities();
                    Tarjeta t = null;
                    String qry = @"select Concat(left(r.`numero_tc`,6),right(rb.folio_de_registro,6),right(r.`numero_tc`,4)) as NumeroTarjeta, m.nombre_titular as NombreTarjetaHabiente, r.id as Id, replace(right(rb.fecha_de_registro,5),'-','') as FechaExpira, Concat(right(right(left(rb.transacciones,5),3),1),left(right(left(rb.transacciones,5),3),1), right(left(right(left(rb.transacciones,5),3),2),1)) as CVC from registro_tc r join registri_broxel rb on r.id = rb.id_de_registro join maquila m on r.numero_de_cuenta = m.num_cuenta where rb.tipo='01' and m.num_cuenta = '" + NumCuenta + @"'";
                    var maq = dbHelper.Database.SqlQuery<MaquilaResumen>(qry).ToList();

                    if (maq.Count > 1)
                    {
                        var max = maq.Max(x => x.Id);
                        maq = maq.Where(x => x.Id == max).ToList();
                    }
                    if (maq.Count == 1)
                    {
                        t = new Tarjeta(maq[0].NombreTarjetaHabiente, maq[0].NumeroTarjeta, maq[0].FechaExpira, maq[0].CVC, NumCuenta);
                    }
                    return t;
                }
                catch (System.Data.EntityCommandExecutionException e)
                {
                    flag = true;
                }
                catch (MySqlException e)
                {
                    flag = true;
                }
                catch (Exception e)
                {
                    SendMail("dispersiones@broxel.com", "josesalvador.macias@broxel.com ", "Fallo armando cuenta " + NumCuenta, "Fallo armar en cuenta " + NumCuenta + " en " + GetLocalIp() + ": " + e, "yMQ3E3ert6");
                    return null;
                }
            } while (flag);
            return null;
        }

        public static Tarjeta GetTarjetaFromCuentaYTerm(string numCuenta, string tarjetaEnmascarada)
        {
            var dbHelper = new broxelco_rdgEntities();
            Tarjeta t = null;
            var rtc = (from rt in dbHelper.registro_tc
                       join rb in dbHelper.vw_registri_broxel on rt.id equals rb.id_de_registro
                       where rt.numero_de_cuenta == numCuenta && rt.tipo == "01" && rt.numero_tc == tarjetaEnmascarada
                       select new
                       {
                           NumeroTarjeta =
                               rt.numero_tc.Substring(0, 6) + rb.folio_de_registro.Substring(5) +
                               rt.numero_tc.Substring(13),
                           NombreTarjetaHabiente = rt.nombre,
                           Id = rt.id,
                           FechaExpira = rb.fecha_de_registro.Substring(4).Replace("-", ""),
                           CVC = rb.transacciones.Substring(4, 1) + rb.transacciones.Substring(2, 1) + rb.transacciones.Substring(3, 1)
                       }
                      ).OrderByDescending(x => x.Id).ToList();
            if (rtc.Count > 1)
            {
                var max = rtc.Max(x => x.Id);
                rtc = rtc.Where(x => x.Id == max).ToList();
            }
            if (rtc.Count == 1)
            {
                t = new Tarjeta(rtc[0].NombreTarjetaHabiente, rtc[0].NumeroTarjeta, rtc[0].FechaExpira, rtc[0].CVC, numCuenta);
            }
            return t;
        }

        public static string GetCuentaFromTarjeta(string Tarjeta)
        {
            broxelco_rdgEntities dbHelper = new broxelco_rdgEntities();
            Tarjeta t = null;
            try
            {
                String qry =
                    @"select `m`.`id`, `m`.`nro-corr` as `nro_corr`, `m`.`nro-tarjeta` as `nro_tarjeta`, `m`.`nombre_titular`, `m`.`num_cuenta`, `m`.`domicilio`, `m`.`piso`, `m`.`localidad`, `m`.`codigo_postal`, `m`.`provincia`, `m`.`nombre_tarjethabiente`, `m`.`limite_compras`, `m`.`limite_credito`, `m`.`imp_adelantos`, `m`.`grupo_cuenta`, `m`.`producto`, `m`.`import`, `m`.`maquila`, `m`.`saldo_restante`, `m`.`total_movimientos`, `m`.`fecha_ultimo_movimiento`, `m`.`disponible`, `m`.`fecha_disponible`, `m`.`fecha_ultima_modificacion`, `m`.`cliente_bx`, `m`.`cuenta_madre`, `m`.`programa`, `m`.`clave_cliente`, `m`.`4ta_linea` as `cuarta_linea`, `m`.`usuario_web`, `m`.`email` from maquila m join registri_broxel r on m.num_cuenta=r.NRucO WHERE left(m.`nro-tarjeta`,6)='" +
                    Tarjeta.Substring(0, 6) + @"' and right(r.`folio_de_registro`,6)='" + Tarjeta.Substring(6, 6) +
                    @"' and right(m.`nro-tarjeta`,4)='" + Tarjeta.Substring(12, 4) + @"'and r.tipo='00' order by r.id limit 1";
                var maq = dbHelper.Database.SqlQuery<vw_maquila>(qry).ToList();
                if (maq.Count == 1)
                    return maq[0].num_cuenta;
                return String.Empty;
            }
            catch (Exception e)
            {
                return String.Empty;
            }
        }
		
		public static string GetCuentaFromTarjetaAdicional(string Tarjeta)
        {
            broxelco_rdgEntities dbHelper = new broxelco_rdgEntities();
            string respuesta = String.Empty;
            try
            {
                String qry =
                    @"select tc.* from registro_tc tc join registri_broxel r on tc.numero_de_cuenta=r.NRucO WHERE left(tc.`numero_tc`,6)='" +
                    Tarjeta.Substring(0, 6) + @"' and right(r.`folio_de_registro`,6)='" + Tarjeta.Substring(6, 6) +
                    @"' and right(tc.`numero_tc`,4)='" + Tarjeta.Substring(12, 4) + @"'and r.tipo='01' order by r.id limit 1";
                var regTc = dbHelper.Database.SqlQuery<registro_tc>(qry).ToList();
                if (regTc.Count == 1)
                    respuesta = regTc[0].numero_de_cuenta;

            }
            catch (Exception e)
            {
                respuesta = String.Empty;
            }
            return respuesta;
        }

        internal static void CorrerPago(string folio)
        {
            var dbHelper = new broxelco_rdgEntities();
            Tarjeta t;
            int POS = 0;
            var webService = new BroxelService();
            var count = 0;

            var dispersionSolicitud = dbHelper.pagosSolicitudes.Where(x => x.folio == folio && x.estado == "Validando").ToList();
            if (dispersionSolicitud.Count == 1 && dispersionSolicitud[0].estado == "Validando")
            {
                if (!VerificaLineaCreditoActualPago(dispersionSolicitud[0]))
                {
                    dispersionSolicitud[0].estado = "PENDIENTE";
                    dbHelper.SaveChanges();

                    string html = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"> <html xmlns=\"http://www.w3.org/1999/xhtml\"> <head> <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" /> <meta name=\"viewport\" content=\"initial-scale=1.0\"/> <meta name=\"format-detection\" content=\"telephone=no\"/> <title>Broxel Proceso de Asignación</title> <style type=\"text/css\"> /* Resets: see reset.css for details */ .ReadMsgBody { width: 100%; background-color: #ffffff;} .ExternalClass {width: 100%; background-color: #ffffff;} .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {line-height:100%;} html{width: 100%; } body {-webkit-text-size-adjust:none; -ms-text-size-adjust:none; } body {margin:0; padding:0;} table {border-spacing:0;} img{display:block !important;} table td {border-collapse:collapse;} .yshortcuts a {border-bottom: none !important;} /* main color = #f05e5e other color = #b91925 background color = #ececec */ img{height:auto !important;} @media only screen and (max-width: 640px){ body{ width:auto!important; } table[class=\"container\"]{ width: 100%!important; padding-left: 20px!important; padding-right: 20px!important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; } img[class=\"small-image-100-percent\"]{ width:100% !important; height:auto !important; } table[class=\"full-width\"]{ width:100% !important; } table[class=\"full-width-text\"]{ width:100% !important; background-color:#b91925; padding-left:20px !important; padding-right:20px !important; } table[class=\"full-width-text2\"]{ width:100% !important; background-color:#f3f3f3; padding-left:20px !important; padding-right:20px !important; } table[class=\"col-2-3img\"]{ width:50% !important; margin-right: 20px !important; } table[class=\"col-2-3img-last\"]{ width:50% !important; } table[class=\"col-2\"]{ width:47% !important; margin-right:20px !important; } table[class=\"col-2-last\"]{ width:47% !important; } table[class=\"col-3\"]{ width:29% !important; margin-right:20px !important; } table[class=\"col-3-last\"]{ width:29% !important; } table[class=\"row-2\"]{ width:50% !important; } td[class=\"text-center\"]{ text-align: center !important; } /* start clear and remove*/ table[class=\"remove\"]{ display:none !important; } td[class=\"remove\"]{ display:none !important; } /* end clear and remove*/ table[class=\"fix-box\"]{ padding-left:20px !important; padding-right:20px !important; } td[class=\"fix-box\"]{ padding-left:20px !important; padding-right:20px !important; } td[class=\"font-resize\"]{ font-size: 18px !important; line-height: 22px !important; } } @media only screen and (max-width: 479px){ body{ font-size:10px !important; } table[class=\"container2\"]{ width: 100%!important; float:none !important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } img[class=\"small-image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } table[class=\"full-width\"]{ width:100% !important; } table[class=\"full-width-text\"]{ width:100% !important; background-color:#b91925; padding-left:20px !important; padding-right:20px !important; } table[class=\"full-width-text2\"]{ width:100% !important; background-color:#f3f3f3; padding-left:20px !important; padding-right:20px !important; } table[class=\"col-2\"]{ width:100% !important; margin-right:0px !important; } table[class=\"col-2-last\"]{ width:100% !important; } table[class=\"col-3\"]{ width:100% !important; margin-right:0px !important; } table[class=\"col-3-last\"]{ width:100% !important; } table[class=\"row-2\"]{ width:100% !important; } table[id=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } td[id=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } td[class=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } /*start text center*/ td[class=\"text-center\"]{ text-align: center !important; } div[class=\"text-center\"]{ text-align: center !important; } /*end text center*/ /* start clear and remove */ table[id=\"clear-padding\"]{ padding:0 !important; } td[id=\"clear-padding\"]{ padding:0 !important; } td[class=\"clear-padding\"]{ padding:0 !important; } table[class=\"remove-479\"]{ display:none !important; } td[class=\"remove-479\"]{ display:none !important; } table[class=\"clear-align\"]{ float:none !important; } /* end clear and remove */ table[class=\"width-small\"]{ width:100% !important; } table[class=\"fix-box\"]{ padding-left:0px !important; padding-right:0px !important; } td[class=\"fix-box\"]{ padding-left:0px !important; padding-right:0px !important; } td[class=\"font-resize\"]{ font-size: 14px !important; } } @media only screen and (max-width: 320px){ table[class=\"width-small\"]{ width:125px !important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } } </style> </head> <body style=\"font-size:12px;\"> <table id=\"mainStructure\" style=\"background-color:#cccccc;\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- START TAB TOP --> <tr> <td valign=\"top\"> <table style=\"background-color: rgb(204, 204, 204);\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td height=\"6\" valign=\"top\"> <table class=\"full-width\" style=\"height:6px;\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> </tr> <!-- start space height --> <tr> <td height=\"5\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END TAB TOP --> <!--START TOP NAVIGATION ​LAYOUT--> <tr> <td valign=\"top\"> <table style=\"background-color: rgb(204, 204, 204);\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <!-- START TAB TOP --> <tr> <td class=\"fix-box\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td height=\"6\" valign=\"top\"> <table class=\"full-width\" style=\"height:6px;\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td class=\"full-width\" style=\"background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <table class=\"clear-align\" style=\"height: 6px; background-color: rgb(0, 194, 231);\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"282\"> <tbody><tr> <td height=\"6\" valign=\"top\"></td> </tr> </tbody></table> </td> <td class=\"remove-479\" style=\"width: 100%; background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <table class=\"clear-align\" style=\"height: 6px; background-color: rgb(0, 194, 231);\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td height=\"6\" valign=\"top\"></td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END TAB TOP --> <!-- START CONTAINER NAVIGATION --> <tr> <td class=\"fix-box\" align=\"center\" valign=\"top\"> <!-- start top navigation container --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start top navigaton --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <table class=\"container2\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <a style=\"text-decoration: none;\" href=\"#\"><img src=\"https://online.broxel.com/Images/Comunicados/img/logo2.png\" style=\"max-width: 310px; background-color: rgb(30, 194, 229);\" alt=\"Logo\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"293\"></a> </td> </tr> </tbody></table> <!--start content nav --> <table class=\"container2\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td class=\"remove-479\" height=\"13\" valign=\"top\"></td> </tr> <!--start call us --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 13px; line-height: 18px; color: #555555; font-weight:bold; text-align: center; font-family:Arail,Tahoma, Helvetica, Arial, sans-serif;\"><div align=\"left\"><span style=\"color: rgb(30, 194, 229);\">Estimad@</span><br/>[CLIENTE]</div></td> </tr> </tbody></table> </td> <!--start space width --> <!--start space width --> </tr> <!--end call us --> <tr> <td height=\"14\" valign=\"top\"></td> </tr> </tbody></table> <!--end content nav --> </td> </tr> </tbody></table> <!-- end top navigaton --> </td> </tr> </tbody></table> <!-- end top navigation container --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- END CONTAINER NAVIGATION --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!--END TOP NAVIGATION ​LAYOUT--> <!-- START LAYOUT 1 --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <!-- start layout-1 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-1 container width 560px --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <!-- start text content --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 18px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#555555; font-weight:bold; text-align:center;\"><div align=\"left\"><span style=\"color: rgb(0, 194, 231); font-weight:bold;\"> Falta de fondos para asignación de línea </span></div></td> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 13px; line-height: 22px; font-family:Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:left; \"><p>La solicitud de asignación de línea con folio<strong><a style=\"text-decoration: none; color: #555555; font-weight: bold;\"> [FOLIO] </a></strong> ha sido rechazada por falta de fondos. Favor de realizar el deposito a la cuenta CLABE que le asignó su ejecutivo de cuenta. </p><p> </tr> <tr> <td valign=\"top\"> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr></tr> <tr> <td valign=\"top\"> <!-- start button --> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"background-color: rgb(0, 194, 231); border-radius: 3px 3px 3px 3px; background-clip: padding-box; font-size: 13px; font-family: Arial,Tahoma,Helvetica,sans-serif; text-align: center; color: rgb(255, 255, 255); font-weight: normal; padding-left: 18px; padding-right: 18px;\" align=\"center\" height=\"32\" valign=\"middle\" width=\"auto\"> <span style=\"color: #ffffff; font-weight: normal;\"> <a href=\"https://corporate.broxel.com/login.php\" style=\"text-decoration: none; color: #ffffff; font-weight: normal;\">Broxel Corporate Access </a> </span> </td> </tr> </tbody></table> <!-- end button --> </td> </tr> <tr></tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <td valign=\"top\"> </td> </tr> <tr> <td style=\"font-size: 13px; line-height: 22px; font-family:Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:left; \"><p><a style=\"text-decoration: none; color: #555555; font-weight: bold;\"><br><br>¿Dudas? </a>comuníquese con su ejecutivo de cuenta Broxel al <a style=\"text-decoration: none; color: #00C2E7; font-weight: bold;\"> teléfono 4433-0303. </a><br/><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">Gracias por utilizar nuestra plataforma Broxel Corporate Access.</a></td> </tr> <tr> <td align=\"center\" valign=\"top\" width=\"auto\"> </td> </tr> </tbody></table> </td> </tr> <!-- end text content --> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> <!-- end layout-1 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-1 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT 1 --> <!-- START LAYOUT Servicio al Cliente --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td valign=\"top\"> <!-- start layout-6 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-6 container width 560px --> <table class=\"full-width\" style=\"background-color: #ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <!-- start image content --> <tbody><tr> <td valign=\"top\" width=\"100%\"> <!-- start content left --> <table class=\"full-width\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td align=\"left\" valign=\"top\"> <a style=\"text-decoration: none;\" href=\"#\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-tel.png\" alt=\"tel\" width=\"127\" height=\"127\" hspace=\"0\" vspace=\"0\" border=\"0\" style=\"display:block; max-width:125px; \"> </a> </td> <!-- space width --> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"20\"> <tbody><tr> <td valign=\"top\"></td> </tr> </tbody></table> </td> <!-- space width --> <!-- start text content --> <td valign=\"top\"> <table class=\"width-small\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start text content --> <tbody><tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 18px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#555555; font-weight:bold; text-align:left;\"> <span style=\"color: #555555; font-weight: bold;\"> <a href=\"#\" style=\"text-decoration: none; color: #555555; font-weight: bold;\">Servicio al Cliente</a> </span> </td> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 13px; line-height: 22px; font-family:Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:left; \"> Para Broxel lo más importante es ofrecerle un servicio oportuno y de calidad. Si tiene cualquier duda sobre alguno de estos servicios, solo llame al <strong><a style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">01800-Broxel-1</a></strong> (01800-276935-1) y un ejecutivo con gusto le ayudará.</td> </tr> </tbody></table> </td> </tr> <!-- end text content --> </tbody></table> </td> <!-- end text content --> </tr> </tbody></table> </td> </tr> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> <!-- end content left --> </td> </tr> <!-- end image content --> </tbody></table> <!-- end layout-6 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-6 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT Servicio al Cliente --> <!-- START LAYOUT Aviso Privacidad --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <!-- start layout-1 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-1 container width 560px --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"509\"> <!--start space height --> <tbody><tr> <td width=\"509\" height=\"16\"></td> </tr> <!--end space height --> <!-- start text content --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody> <!--start space height --> <tr> <td height=\"0\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 11px; line-height: 16px; font-family: Arial,Tahoma, Helvetica, sans-serif; color: #a3a2a2; font-weight: normal; text-align: center;\"><p><a href=\"http://www.broxel.com/index.php/aviso-privacidad-sapi\" style=\"text-decoration: none; color: #555555; font-weight: bold;\"><span style=\"color: rgb(0, 194, 231); font-weight: bold;\">Aviso de Privacidad</span></a></p> <p> Sus datos se encuentran protegidos a través de nuestro Aviso de Privacidad.<br>Consulte nuestro Aviso de Privacidad en: <a href=\"http://www.broxel.com/index.php/aviso-privacidad-sapi\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">www.broxel.com</a>.</p> <p> Este correo electrónico está destinado a residentes de México y fue enviado desde Broxel.<br/> Si ha recibido este correo de una dirección diferente, significa que fue reenviado.</p> <p>Las comunicaciones vía correo electrónico de Broxel se basan en el manejo de cookies<br/> y/o Web Beacons.</p> <p>Al recibir este correo y consultar el contenido del mismo, usted consiente que Broxel recabe y tenga acceso a sus datos personales.</p> <p>Para más información favor de consultar <strong><a href=\"http://www.broxel.com/index.php/terminos-condiciones\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">Términos y Condiciones.</a></strong></p></td> </tr> <tr> <td align=\"center\" valign=\"top\" width=\"auto\"> </td> </tr> </tbody></table> </td> </tr> <!-- end text content --> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> <!-- end layout-1 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-1 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT 67 --> <!-- START LAYOUT 7--> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <!-- start layout-7 container --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td valign=\"top\"> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <tr> <td valign=\"top\"> <!-- start logo footer and address --> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td valign=\"top\"> <!--start icon socail navigation --> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td align=\"left\" valign=\"top\"> <table class=\"container\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://www.facebook.com/BroxelSF\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-facebook.jpg\" alt=\"icon-facebook\" style=\"max-width:33px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> <td style=\"padding-left:5px; \" class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://twitter.com/BroxelSF\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-twitter.jpg\" alt=\"icon-twitter\" style=\"max-width:30px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> <td style=\"padding-left:5px; \" class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://plus.google.com/u/0/116529217974857845752/posts\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-googleplus.jpg\" alt=\"icon-googleplus\" style=\"max-width:30px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> </tr> </tbody></table> </td> </tr> </tbody></table> <!--end icon socail navigation --> </td> </tr> </tbody></table> <!-- end logo footer and address --> </td> </tr> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> <!-- end layout-7 container --> </td> </tr> <!-- END LAYOUT-7--> <!-- START FOOTER COPY RIGHT --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <!-- start layout-7 container --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <tbody><tr> <!-- start unsubscribe content --> <td valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 13px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:center;\" class=\"text-center\" align=\"center\"> Síguanos por facebook, twitter y google +<br /> Visitenos en <a href=\"http://broxel.com\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">www.broxel.com</a> para conocer más de nuestros productos.</td> </tr> </tbody></table> </td> <!-- end unsubscribe content --> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <!-- start COPY RIGHT content --> <td style=\"font-size: 11px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:center;\" valign=\"top\">© 2014 BROXEL | Servicios Financieros. Todos los derechos Reservados.</td> <!-- end COPY RIGHT content --> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END FOOTER COPY RIGHT --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> </table></body> </html>";
                    html = html.Replace("[CLIENTE]", dispersionSolicitud[0].cliente);
                    html = html.Replace("[FOLIO]", dispersionSolicitud[0].folio);
                    SendMail("dispersiones@broxel.com",
                        ConfigurationManager.AppSettings["emailsAvisosDispersiones"] + ", " +
                        dispersionSolicitud[0].usuarioEjecucion + ", " + dispersionSolicitud[0].usuarioAprobacion + ", " +
                        dispersionSolicitud[0].usuarioCreacion,
                        "Solicitud de pagos rechazada - cliente :" + dispersionSolicitud[0].claveCliente,
                        html, "yMQ3E3ert6");
                    return;
                }
                dispersionSolicitud[0].estado = "Ejecutando";
                dbHelper.SaveChanges();
                var dispersionesa =
                    dbHelper.pagosInternos.Where(
                        x => x.idSolicitud == folio && (((x.codigoRespuesta != "-1" || x.codigoRespuesta == null) && x.pago > 0.0))).OrderBy(x => x.id);
                var dispersiones = dispersionesa.ToList();
                foreach (var dispersion in dispersiones)
                {
                    count++;
                    if (dispersiones.Count(x => x.cuenta == dispersion.cuenta) != 1)
                    {
                        dispersion.codigoRespuesta = "989";
                        continue;
                    }
                    try
                    {
                        t = GetTarjetaFromCuenta(dispersion.cuenta);
                        if (t != null)
                        {
                            try
                            {
                                if (dispersion.pago > 0 && dispersion.codigoRespuesta != "-1")
                                {
                                    var res = webService.EfectuarPago(Convert.ToDecimal(dispersion.pago), t.NombreTarjeta, t.NumeroTarjeta, t.Cvc2, t.FechaExpira, 2, 12, dispersion.cuenta);
                                    dispersion.codigoAutorizacion = res.NumeroAutorizacion;
                                    dispersion.idmov = res.IdMovimiento.ToString(CultureInfo.InvariantCulture);
                                    dispersion.codigoRespuesta = res.CodigoRespuesta.ToString(CultureInfo.InvariantCulture);
                                    dispersion.antes = (float?)res.SaldoAntes;
                                    dispersion.despues = (float?)res.SaldoDespues;
                                    POS++;

                                    //Para intentar bloquear la cuenta si es que esta fue  bloqueada
                                    if (dispersion.codigoRespuesta == "-1" && (dispersion.bloquearCuenta ?? false))
                                        BloqueaCuentasRedDePagos(dispersion.cuenta, folio);
                                }
                            }
                            catch (Exception ex)
                            {
                                dispersion.codigoRespuesta = "990";
                            }
                        }
                        else
                            dispersion.codigoRespuesta = "993";
                        if (count >= 35)
                        {
                            dbHelper.SaveChanges();
                            count = 0;
                        }
                    }
                    catch (Exception e)
                    {
                        //TODO: Guardar en bitacora
                        SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com ", "Problema pago " + folio, "Fallo el pago folio " + folio + " en cuenta " + dispersion.cuenta + "  " + e, "yMQ3E3ert6");
                    }
                }
                dbHelper.SaveChanges();
                ActualizaYMandaMailPago(dispersionSolicitud.ElementAt(0), "PAGO", folio);
            }
        }

        internal static void CorrerDispersion(string folio, string tipo)
        {
            var dbHelper = new broxelco_rdgEntities();
            Tarjeta t;
            int POS = 0, ATM = 0;
            var webService = new BroxelService();
            var count = 0;
            bool comision;

            var dispersionSolicitud = dbHelper.dispersionesSolicitudes.Where(x => x.folio == folio && x.estado == "Validando").ToList();
            if (dispersionSolicitud.Count == 1 && dispersionSolicitud[0].estado == "Validando")
            {
                if (!VerificaLineaCreditoActual(dispersionSolicitud[0], out comision))
                {
                    dispersionSolicitud[0].estado = "PENDIENTE";
                    dbHelper.SaveChanges();
                    if (!comision)
                        SendMail("dispersiones@broxel.com",
                            "javier.lopez@broxel.com, juan.pastrana@broxel.com,  gabriela.cardenas@broxel.com, mauricio.lopez@broxel.com, rodrigo.diazdeleon@broxel.com, jesus.valdiviezo@broxel.com, karla.celis@broxel.com, ericka.escobedo@broxel.com , monica.jimenez@broxel.com , berenice.sanchez@broxel.com",
                            "Solicitud de asignación de línea rechazada - cliente :" +
                            dispersionSolicitud[0].claveCliente,
                            "El cliente " + dispersionSolicitud[0].cliente + " con clave " +
                            dispersionSolicitud[0].claveCliente + " no tiene comision de dispersion para el producto " +
                            dispersionSolicitud[0].producto + " favor de verificarlo.", "yMQ3E3ert6");
                    else
                    {
                        string html = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"> <html xmlns=\"http://www.w3.org/1999/xhtml\"> <head> <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" /> <meta name=\"viewport\" content=\"initial-scale=1.0\"/> <meta name=\"format-detection\" content=\"telephone=no\"/> <title>Broxel Proceso de Asignación</title> <style type=\"text/css\"> /* Resets: see reset.css for details */ .ReadMsgBody { width: 100%; background-color: #ffffff;} .ExternalClass {width: 100%; background-color: #ffffff;} .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {line-height:100%;} html{width: 100%; } body {-webkit-text-size-adjust:none; -ms-text-size-adjust:none; } body {margin:0; padding:0;} table {border-spacing:0;} img{display:block !important;} table td {border-collapse:collapse;} .yshortcuts a {border-bottom: none !important;} /* main color = #f05e5e other color = #b91925 background color = #ececec */ img{height:auto !important;} @media only screen and (max-width: 640px){ body{ width:auto!important; } table[class=\"container\"]{ width: 100%!important; padding-left: 20px!important; padding-right: 20px!important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; } img[class=\"small-image-100-percent\"]{ width:100% !important; height:auto !important; } table[class=\"full-width\"]{ width:100% !important; } table[class=\"full-width-text\"]{ width:100% !important; background-color:#b91925; padding-left:20px !important; padding-right:20px !important; } table[class=\"full-width-text2\"]{ width:100% !important; background-color:#f3f3f3; padding-left:20px !important; padding-right:20px !important; } table[class=\"col-2-3img\"]{ width:50% !important; margin-right: 20px !important; } table[class=\"col-2-3img-last\"]{ width:50% !important; } table[class=\"col-2\"]{ width:47% !important; margin-right:20px !important; } table[class=\"col-2-last\"]{ width:47% !important; } table[class=\"col-3\"]{ width:29% !important; margin-right:20px !important; } table[class=\"col-3-last\"]{ width:29% !important; } table[class=\"row-2\"]{ width:50% !important; } td[class=\"text-center\"]{ text-align: center !important; } /* start clear and remove*/ table[class=\"remove\"]{ display:none !important; } td[class=\"remove\"]{ display:none !important; } /* end clear and remove*/ table[class=\"fix-box\"]{ padding-left:20px !important; padding-right:20px !important; } td[class=\"fix-box\"]{ padding-left:20px !important; padding-right:20px !important; } td[class=\"font-resize\"]{ font-size: 18px !important; line-height: 22px !important; } } @media only screen and (max-width: 479px){ body{ font-size:10px !important; } table[class=\"container2\"]{ width: 100%!important; float:none !important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } img[class=\"small-image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } table[class=\"full-width\"]{ width:100% !important; } table[class=\"full-width-text\"]{ width:100% !important; background-color:#b91925; padding-left:20px !important; padding-right:20px !important; } table[class=\"full-width-text2\"]{ width:100% !important; background-color:#f3f3f3; padding-left:20px !important; padding-right:20px !important; } table[class=\"col-2\"]{ width:100% !important; margin-right:0px !important; } table[class=\"col-2-last\"]{ width:100% !important; } table[class=\"col-3\"]{ width:100% !important; margin-right:0px !important; } table[class=\"col-3-last\"]{ width:100% !important; } table[class=\"row-2\"]{ width:100% !important; } table[id=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } td[id=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } td[class=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } /*start text center*/ td[class=\"text-center\"]{ text-align: center !important; } div[class=\"text-center\"]{ text-align: center !important; } /*end text center*/ /* start clear and remove */ table[id=\"clear-padding\"]{ padding:0 !important; } td[id=\"clear-padding\"]{ padding:0 !important; } td[class=\"clear-padding\"]{ padding:0 !important; } table[class=\"remove-479\"]{ display:none !important; } td[class=\"remove-479\"]{ display:none !important; } table[class=\"clear-align\"]{ float:none !important; } /* end clear and remove */ table[class=\"width-small\"]{ width:100% !important; } table[class=\"fix-box\"]{ padding-left:0px !important; padding-right:0px !important; } td[class=\"fix-box\"]{ padding-left:0px !important; padding-right:0px !important; } td[class=\"font-resize\"]{ font-size: 14px !important; } } @media only screen and (max-width: 320px){ table[class=\"width-small\"]{ width:125px !important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } } </style> </head> <body style=\"font-size:12px;\"> <table id=\"mainStructure\" style=\"background-color:#cccccc;\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- START TAB TOP --> <tr> <td valign=\"top\"> <table style=\"background-color: rgb(204, 204, 204);\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td height=\"6\" valign=\"top\"> <table class=\"full-width\" style=\"height:6px;\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> </tr> <!-- start space height --> <tr> <td height=\"5\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END TAB TOP --> <!--START TOP NAVIGATION ​LAYOUT--> <tr> <td valign=\"top\"> <table style=\"background-color: rgb(204, 204, 204);\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <!-- START TAB TOP --> <tr> <td class=\"fix-box\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td height=\"6\" valign=\"top\"> <table class=\"full-width\" style=\"height:6px;\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td class=\"full-width\" style=\"background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <table class=\"clear-align\" style=\"height: 6px; background-color: rgb(0, 194, 231);\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"282\"> <tbody><tr> <td height=\"6\" valign=\"top\"></td> </tr> </tbody></table> </td> <td class=\"remove-479\" style=\"width: 100%; background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <table class=\"clear-align\" style=\"height: 6px; background-color: rgb(0, 194, 231);\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td height=\"6\" valign=\"top\"></td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END TAB TOP --> <!-- START CONTAINER NAVIGATION --> <tr> <td class=\"fix-box\" align=\"center\" valign=\"top\"> <!-- start top navigation container --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start top navigaton --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <table class=\"container2\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <a style=\"text-decoration: none;\" href=\"#\"><img src=\"https://online.broxel.com/Images/Comunicados/img/logo2.png\" style=\"max-width: 310px; background-color: rgb(30, 194, 229);\" alt=\"Logo\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"293\"></a> </td> </tr> </tbody></table> <!--start content nav --> <table class=\"container2\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td class=\"remove-479\" height=\"13\" valign=\"top\"></td> </tr> <!--start call us --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 13px; line-height: 18px; color: #555555; font-weight:bold; text-align: center; font-family:Arail,Tahoma, Helvetica, Arial, sans-serif;\"><div align=\"left\"><span style=\"color: rgb(30, 194, 229);\">Estimad@</span><br/>[CLIENTE]</div></td> </tr> </tbody></table> </td> <!--start space width --> <!--start space width --> </tr> <!--end call us --> <tr> <td height=\"14\" valign=\"top\"></td> </tr> </tbody></table> <!--end content nav --> </td> </tr> </tbody></table> <!-- end top navigaton --> </td> </tr> </tbody></table> <!-- end top navigation container --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- END CONTAINER NAVIGATION --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!--END TOP NAVIGATION ​LAYOUT--> <!-- START LAYOUT 1 --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <!-- start layout-1 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-1 container width 560px --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <!-- start text content --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 18px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#555555; font-weight:bold; text-align:center;\"><div align=\"left\"><span style=\"color: rgb(0, 194, 231); font-weight:bold;\"> Falta de fondos para asignación de línea </span></div></td> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 13px; line-height: 22px; font-family:Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:left; \"><p>La solicitud de asignación de línea con folio<strong><a style=\"text-decoration: none; color: #555555; font-weight: bold;\"> [FOLIO] </a></strong> ha sido rechazada por falta de fondos. Favor de realizar el deposito a la cuenta CLABE que le asignó su ejecutivo de cuenta. </p><p> </tr> <tr> <td valign=\"top\"> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr></tr> <tr> <td valign=\"top\"> <!-- start button --> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"background-color: rgb(0, 194, 231); border-radius: 3px 3px 3px 3px; background-clip: padding-box; font-size: 13px; font-family: Arial,Tahoma,Helvetica,sans-serif; text-align: center; color: rgb(255, 255, 255); font-weight: normal; padding-left: 18px; padding-right: 18px;\" align=\"center\" height=\"32\" valign=\"middle\" width=\"auto\"> <span style=\"color: #ffffff; font-weight: normal;\"> <a href=\"https://corporate.broxel.com/login.php\" style=\"text-decoration: none; color: #ffffff; font-weight: normal;\">Broxel Corporate Access </a> </span> </td> </tr> </tbody></table> <!-- end button --> </td> </tr> <tr></tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <td valign=\"top\"> </td> </tr> <tr> <td style=\"font-size: 13px; line-height: 22px; font-family:Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:left; \"><p><a style=\"text-decoration: none; color: #555555; font-weight: bold;\"><br><br>¿Dudas? </a>comuníquese con su ejecutivo de cuenta Broxel al <a style=\"text-decoration: none; color: #00C2E7; font-weight: bold;\"> teléfono 4433-0303. </a><br/><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">Gracias por utilizar nuestra plataforma Broxel Corporate Access.</a></td> </tr> <tr> <td align=\"center\" valign=\"top\" width=\"auto\"> </td> </tr> </tbody></table> </td> </tr> <!-- end text content --> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> <!-- end layout-1 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-1 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT 1 --> <!-- START LAYOUT Servicio al Cliente --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td valign=\"top\"> <!-- start layout-6 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-6 container width 560px --> <table class=\"full-width\" style=\"background-color: #ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <!-- start image content --> <tbody><tr> <td valign=\"top\" width=\"100%\"> <!-- start content left --> <table class=\"full-width\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td align=\"left\" valign=\"top\"> <a style=\"text-decoration: none;\" href=\"#\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-tel.png\" alt=\"tel\" width=\"127\" height=\"127\" hspace=\"0\" vspace=\"0\" border=\"0\" style=\"display:block; max-width:125px; \"> </a> </td> <!-- space width --> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"20\"> <tbody><tr> <td valign=\"top\"></td> </tr> </tbody></table> </td> <!-- space width --> <!-- start text content --> <td valign=\"top\"> <table class=\"width-small\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start text content --> <tbody><tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 18px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#555555; font-weight:bold; text-align:left;\"> <span style=\"color: #555555; font-weight: bold;\"> <a href=\"#\" style=\"text-decoration: none; color: #555555; font-weight: bold;\">Servicio al Cliente</a> </span> </td> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 13px; line-height: 22px; font-family:Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:left; \"> Para Broxel lo más importante es ofrecerle un servicio oportuno y de calidad. Si tiene cualquier duda sobre alguno de estos servicios, solo llame al <strong><a style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">01800-Broxel-1</a></strong> (01800-276935-1) y un ejecutivo con gusto le ayudará.</td> </tr> </tbody></table> </td> </tr> <!-- end text content --> </tbody></table> </td> <!-- end text content --> </tr> </tbody></table> </td> </tr> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> <!-- end content left --> </td> </tr> <!-- end image content --> </tbody></table> <!-- end layout-6 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-6 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT Servicio al Cliente --> <!-- START LAYOUT Aviso Privacidad --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <!-- start layout-1 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-1 container width 560px --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"509\"> <!--start space height --> <tbody><tr> <td width=\"509\" height=\"16\"></td> </tr> <!--end space height --> <!-- start text content --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody> <!--start space height --> <tr> <td height=\"0\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 11px; line-height: 16px; font-family: Arial,Tahoma, Helvetica, sans-serif; color: #a3a2a2; font-weight: normal; text-align: center;\"><p><a href=\"http://www.broxel.com/index.php/aviso-privacidad-sapi\" style=\"text-decoration: none; color: #555555; font-weight: bold;\"><span style=\"color: rgb(0, 194, 231); font-weight: bold;\">Aviso de Privacidad</span></a></p> <p> Sus datos se encuentran protegidos a través de nuestro Aviso de Privacidad.<br>Consulte nuestro Aviso de Privacidad en: <a href=\"http://www.broxel.com/index.php/aviso-privacidad-sapi\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">www.broxel.com</a>.</p> <p> Este correo electrónico está destinado a residentes de México y fue enviado desde Broxel.<br/> Si ha recibido este correo de una dirección diferente, significa que fue reenviado.</p> <p>Las comunicaciones vía correo electrónico de Broxel se basan en el manejo de cookies<br/> y/o Web Beacons.</p> <p>Al recibir este correo y consultar el contenido del mismo, usted consiente que Broxel recabe y tenga acceso a sus datos personales.</p> <p>Para más información favor de consultar <strong><a href=\"http://www.broxel.com/index.php/terminos-condiciones\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">Términos y Condiciones.</a></strong></p></td> </tr> <tr> <td align=\"center\" valign=\"top\" width=\"auto\"> </td> </tr> </tbody></table> </td> </tr> <!-- end text content --> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> <!-- end layout-1 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-1 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT 67 --> <!-- START LAYOUT 7--> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <!-- start layout-7 container --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td valign=\"top\"> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <tr> <td valign=\"top\"> <!-- start logo footer and address --> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td valign=\"top\"> <!--start icon socail navigation --> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td align=\"left\" valign=\"top\"> <table class=\"container\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://www.facebook.com/BroxelSF\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-facebook.jpg\" alt=\"icon-facebook\" style=\"max-width:33px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> <td style=\"padding-left:5px; \" class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://twitter.com/BroxelSF\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-twitter.jpg\" alt=\"icon-twitter\" style=\"max-width:30px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> <td style=\"padding-left:5px; \" class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://plus.google.com/u/0/116529217974857845752/posts\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-googleplus.jpg\" alt=\"icon-googleplus\" style=\"max-width:30px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> </tr> </tbody></table> </td> </tr> </tbody></table> <!--end icon socail navigation --> </td> </tr> </tbody></table> <!-- end logo footer and address --> </td> </tr> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> <!-- end layout-7 container --> </td> </tr> <!-- END LAYOUT-7--> <!-- START FOOTER COPY RIGHT --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <!-- start layout-7 container --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <tbody><tr> <!-- start unsubscribe content --> <td valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 13px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:center;\" class=\"text-center\" align=\"center\"> Síguanos por facebook, twitter y google +<br /> Visitenos en <a href=\"http://broxel.com\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">www.broxel.com</a> para conocer más de nuestros productos.</td> </tr> </tbody></table> </td> <!-- end unsubscribe content --> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <!-- start COPY RIGHT content --> <td style=\"font-size: 11px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:center;\" valign=\"top\">© 2014 BROXEL | Servicios Financieros. Todos los derechos Reservados.</td> <!-- end COPY RIGHT content --> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END FOOTER COPY RIGHT --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> </table></body> </html>";
                        html = html.Replace("[CLIENTE]", dispersionSolicitud[0].cliente);
                        html = html.Replace("[FOLIO]", dispersionSolicitud[0].folio);
                        SendMail("dispersiones@broxel.com",
                            ConfigurationManager.AppSettings["emailsAvisosDispersiones"] +
                            (dispersionSolicitud[0].usuarioEjecucion.Contains("@") ? (", " + dispersionSolicitud[0].usuarioEjecucion) : "") +
                            (dispersionSolicitud[0].usuarioAprobacion.Contains("@") ? (", " + dispersionSolicitud[0].usuarioAprobacion) : "") +
                            (dispersionSolicitud[0].usuarioCreacion.Contains("@") ? (", " + dispersionSolicitud[0].usuarioCreacion) : ""),
                            "Solicitud de asignación de línea rechazada - cliente :" + dispersionSolicitud[0].claveCliente,
                            html, "yMQ3E3ert6");
                    }
                    return;
                }
                dispersionSolicitud[0].estado = "Ejecutando";
                dbHelper.SaveChanges();
                var dispersiones = dbHelper.dispersionesInternas.Where(x => x.idSolicitud == folio && ((x.codigoRespuestaPOS != "-1" && x.incrementoPOS > 0.0) || (x.codigoRespuestaATM != "-1" && x.incrementoATM > 0.0))).OrderBy(x => x.id).ToList();
                foreach (var dispersion in dispersiones)
                {
                    //Mejoravit
                    if (dispersion.codigoRespuestaPOS == "983")
                        continue;
                    count++;
                    if (dispersiones.Count(x => x.cuenta == dispersion.cuenta) != 1)
                    {
                        dispersion.codigoRespuestaPOS = "989";
                        dispersion.codigoRespuestaATM = "989";
                        continue;
                    }
                    try
                    {
                        t = GetTarjetaFromCuenta(dispersion.cuenta);
                        if (t != null)
                        {
                            try
                            {
                                if (dispersion.incrementoATM > 0.0 && dispersion.codigoRespuestaATM != "-1")
                                {
                                    if (tipo == "INCREMENTO")
                                    {
                                        var res = webService.ActualizarLimiteATM(t.NombreTarjeta, t.NumeroTarjeta, t.FechaExpira, t.Cvc2, Convert.ToDecimal(dispersion.incrementoATM), 2, dispersion.cuenta,t.Procesador);
                                        dispersion.codigoAutorizacionATM = res.NumeroAutorizacion;
                                        dispersion.idmovATM = res.IdMovimiento.ToString(CultureInfo.InvariantCulture);
                                        dispersion.codigoRespuestaATM = res.CodigoRespuesta.ToString(CultureInfo.InvariantCulture);
                                        dispersion.antesATM = (float?)res.SaldoAntes;
                                        dispersion.despuesATM = (float?)res.SaldoDespues;
                                        ATM++;
                                    }
                                    else
                                    {
                                        var res = webService.NuevoLimiteATM(t.NombreTarjeta, t.NumeroTarjeta, t.FechaExpira, t.Cvc2, Convert.ToDecimal(dispersion.incrementoATM), 2, dispersion.cuenta);
                                        dispersion.codigoAutorizacionATM = res.NumeroAutorizacion;
                                        dispersion.idmovATM = res.IdMovimiento.ToString(CultureInfo.InvariantCulture);
                                        dispersion.codigoRespuestaATM =
                                            res.CodigoRespuesta.ToString(CultureInfo.InvariantCulture);
                                        dispersion.antesATM = (float?)res.SaldoAntes;
                                        dispersion.despuesATM = (float?)res.SaldoDespues;
                                        ATM++;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                dispersion.codigoRespuestaATM = "989";
                            }
                            finally
                            {
                                try
                                {
                                    if (dispersion.incrementoPOS > 0 && dispersion.codigoRespuestaPOS != "-1")
                                    {
                                        if (tipo == "INCREMENTO")
                                        {
                                            var res = webService.ActualizarLimiteCredito(t.NombreTarjeta, t.NumeroTarjeta, t.FechaExpira, t.Cvc2, Convert.ToDecimal(dispersion.incrementoPOS), 2, dispersion.cuenta, t.Procesador);
                                            dispersion.codigoAutorizacionPOS = res.NumeroAutorizacion;
                                            dispersion.idmovPOS = res.IdMovimiento.ToString(CultureInfo.InvariantCulture);
                                            dispersion.codigoRespuestaPOS = res.CodigoRespuesta.ToString(CultureInfo.InvariantCulture);
                                            dispersion.antesPOS = (float?)res.SaldoAntes;
                                            dispersion.despuesPOS = (float?)res.SaldoDespues;
                                            POS++;


                                            //Para intentar bloquear la cuenta si es que esta fue  bloqueada
                                            if (dispersion.codigoRespuestaPOS == "-1" && dispersion.bloquearCuenta)
                                                BloqueaCuentasRedDePagos(dispersion.cuenta, folio);
                                        }
                                        else
                                        {
                                            var res = webService.NuevoLimiteCredito(t.NombreTarjeta, t.NumeroTarjeta, t.FechaExpira, t.Cvc2, Convert.ToDecimal(dispersion.incrementoPOS), 2, dispersion.cuenta);
                                            dispersion.codigoAutorizacionPOS = res.NumeroAutorizacion;
                                            dispersion.idmovPOS = res.IdMovimiento.ToString(CultureInfo.InvariantCulture);
                                            dispersion.codigoRespuestaPOS = res.CodigoRespuesta.ToString(CultureInfo.InvariantCulture);
                                            dispersion.antesPOS = (float?)res.SaldoAntes;
                                            dispersion.despuesPOS = (float?)res.SaldoDespues;
                                            POS++;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    dispersion.codigoRespuestaPOS = "990";
                                }
                            }
                        }
                        else
                            dispersion.codigoRespuestaATM = dispersion.codigoRespuestaPOS = "993";
                        if (count >= 35)
                        {
                            dbHelper.SaveChanges();
                            count = 0;
                        }
                    }
                    catch (Exception e)
                    {
                        //TODO: Guardar en bitacora
                        SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com ", "Problema Disp " + folio, "Fallo la dispersion folio " + folio + " en cuenta " + dispersion.cuenta + "  " + e, "yMQ3E3ert6");
                    }
                }
                try
                {
                    dbHelper.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }
                
                ActualizaYMandaMail(folio, tipo);
            }
        }

        internal static void CorrerCargo(string folio, string tipo)
        {
            var broxelSqlHelper = new BroxelSQLEntities();
            var mysqlHelper = new broxelco_rdgEntities();
            Tarjeta t;
            int Cargos = 0;
            var webService = new BroxelService();
            var count = 0;

            var cargoSolicitud = broxelSqlHelper.CargosSolicitudes.Where(x => x.Folio == folio && x.Estado == "Validando").ToList();
            if (cargoSolicitud.Count == 1 && cargoSolicitud[0].Estado == "Validando")
            {
                var sqlPaymentsHelper = new BroxelEntities();
                int idComercio = cargoSolicitud[0].IdComercio;
                int idCargoSolicitud = cargoSolicitud[0].Id;
                int usuarioCargo = 1;
                var comercio = sqlPaymentsHelper.Comercio.FirstOrDefault(x => x.idComercio == idComercio);

                //if (usuariosEnComercio.Count < 0)
                //{
                //    cargoSolicitud[0].Estado = "PENDIENTE";
                //    broxelSqlHelper.SaveChanges();

                //    string html = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"> <html xmlns=\"http://www.w3.org/1999/xhtml\"> <head> <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" /> <meta name=\"viewport\" content=\"initial-scale=1.0\"/> <meta name=\"format-detection\" content=\"telephone=no\"/> <title>Broxel Proceso de Asignación</title> <style type=\"text/css\"> /* Resets: see reset.css for details */ .ReadMsgBody { width: 100%; background-color: #ffffff;} .ExternalClass {width: 100%; background-color: #ffffff;} .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {line-height:100%;} html{width: 100%; } body {-webkit-text-size-adjust:none; -ms-text-size-adjust:none; } body {margin:0; padding:0;} table {border-spacing:0;} img{display:block !important;} table td {border-collapse:collapse;} .yshortcuts a {border-bottom: none !important;} /* main color = #f05e5e other color = #b91925 background color = #ececec */ img{height:auto !important;} @media only screen and (max-width: 640px){ body{ width:auto!important; } table[class=\"container\"]{ width: 100%!important; padding-left: 20px!important; padding-right: 20px!important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; } img[class=\"small-image-100-percent\"]{ width:100% !important; height:auto !important; } table[class=\"full-width\"]{ width:100% !important; } table[class=\"full-width-text\"]{ width:100% !important; background-color:#b91925; padding-left:20px !important; padding-right:20px !important; } table[class=\"full-width-text2\"]{ width:100% !important; background-color:#f3f3f3; padding-left:20px !important; padding-right:20px !important; } table[class=\"col-2-3img\"]{ width:50% !important; margin-right: 20px !important; } table[class=\"col-2-3img-last\"]{ width:50% !important; } table[class=\"col-2\"]{ width:47% !important; margin-right:20px !important; } table[class=\"col-2-last\"]{ width:47% !important; } table[class=\"col-3\"]{ width:29% !important; margin-right:20px !important; } table[class=\"col-3-last\"]{ width:29% !important; } table[class=\"row-2\"]{ width:50% !important; } td[class=\"text-center\"]{ text-align: center !important; } /* start clear and remove*/ table[class=\"remove\"]{ display:none !important; } td[class=\"remove\"]{ display:none !important; } /* end clear and remove*/ table[class=\"fix-box\"]{ padding-left:20px !important; padding-right:20px !important; } td[class=\"fix-box\"]{ padding-left:20px !important; padding-right:20px !important; } td[class=\"font-resize\"]{ font-size: 18px !important; line-height: 22px !important; } } @media only screen and (max-width: 479px){ body{ font-size:10px !important; } table[class=\"container2\"]{ width: 100%!important; float:none !important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } img[class=\"small-image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } table[class=\"full-width\"]{ width:100% !important; } table[class=\"full-width-text\"]{ width:100% !important; background-color:#b91925; padding-left:20px !important; padding-right:20px !important; } table[class=\"full-width-text2\"]{ width:100% !important; background-color:#f3f3f3; padding-left:20px !important; padding-right:20px !important; } table[class=\"col-2\"]{ width:100% !important; margin-right:0px !important; } table[class=\"col-2-last\"]{ width:100% !important; } table[class=\"col-3\"]{ width:100% !important; margin-right:0px !important; } table[class=\"col-3-last\"]{ width:100% !important; } table[class=\"row-2\"]{ width:100% !important; } table[id=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } td[id=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } td[class=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } /*start text center*/ td[class=\"text-center\"]{ text-align: center !important; } div[class=\"text-center\"]{ text-align: center !important; } /*end text center*/ /* start clear and remove */ table[id=\"clear-padding\"]{ padding:0 !important; } td[id=\"clear-padding\"]{ padding:0 !important; } td[class=\"clear-padding\"]{ padding:0 !important; } table[class=\"remove-479\"]{ display:none !important; } td[class=\"remove-479\"]{ display:none !important; } table[class=\"clear-align\"]{ float:none !important; } /* end clear and remove */ table[class=\"width-small\"]{ width:100% !important; } table[class=\"fix-box\"]{ padding-left:0px !important; padding-right:0px !important; } td[class=\"fix-box\"]{ padding-left:0px !important; padding-right:0px !important; } td[class=\"font-resize\"]{ font-size: 14px !important; } } @media only screen and (max-width: 320px){ table[class=\"width-small\"]{ width:125px !important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } } </style> </head> <body style=\"font-size:12px;\"> <table id=\"mainStructure\" style=\"background-color:#cccccc;\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- START TAB TOP --> <tr> <td valign=\"top\"> <table style=\"background-color: rgb(204, 204, 204);\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td height=\"6\" valign=\"top\"> <table class=\"full-width\" style=\"height:6px;\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> </tr> <!-- start space height --> <tr> <td height=\"5\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END TAB TOP --> <!--START TOP NAVIGATION ​LAYOUT--> <tr> <td valign=\"top\"> <table style=\"background-color: rgb(204, 204, 204);\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <!-- START TAB TOP --> <tr> <td class=\"fix-box\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td height=\"6\" valign=\"top\"> <table class=\"full-width\" style=\"height:6px;\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td class=\"full-width\" style=\"background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <table class=\"clear-align\" style=\"height: 6px; background-color: rgb(0, 194, 231);\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"282\"> <tbody><tr> <td height=\"6\" valign=\"top\"></td> </tr> </tbody></table> </td> <td class=\"remove-479\" style=\"width: 100%; background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <table class=\"clear-align\" style=\"height: 6px; background-color: rgb(0, 194, 231);\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td height=\"6\" valign=\"top\"></td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END TAB TOP --> <!-- START CONTAINER NAVIGATION --> <tr> <td class=\"fix-box\" align=\"center\" valign=\"top\"> <!-- start top navigation container --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start top navigaton --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <table class=\"container2\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <a style=\"text-decoration: none;\" href=\"#\"><img src=\"https://online.broxel.com/Images/Comunicados/img/logo2.png\" style=\"max-width: 310px; background-color: rgb(30, 194, 229);\" alt=\"Logo\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"293\"></a> </td> </tr> </tbody></table> <!--start content nav --> <table class=\"container2\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td class=\"remove-479\" height=\"13\" valign=\"top\"></td> </tr> <!--start call us --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 13px; line-height: 18px; color: #555555; font-weight:bold; text-align: center; font-family:Arail,Tahoma, Helvetica, Arial, sans-serif;\"><div align=\"left\"><span style=\"color: rgb(30, 194, 229);\">Estimad@</span><br/>[CLIENTE]</div></td> </tr> </tbody></table> </td> <!--start space width --> <!--start space width --> </tr> <!--end call us --> <tr> <td height=\"14\" valign=\"top\"></td> </tr> </tbody></table> <!--end content nav --> </td> </tr> </tbody></table> <!-- end top navigaton --> </td> </tr> </tbody></table> <!-- end top navigation container --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- END CONTAINER NAVIGATION --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!--END TOP NAVIGATION ​LAYOUT--> <!-- START LAYOUT 1 --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <!-- start layout-1 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-1 container width 560px --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <!-- start text content --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 18px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#555555; font-weight:bold; text-align:center;\"><div align=\"left\"><span style=\"color: rgb(0, 194, 231); font-weight:bold;\"> Falta de fondos para asignación de línea </span></div></td> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 13px; line-height: 22px; font-family:Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:left; \"><p>La solicitud de asignación de línea con folio<strong><a style=\"text-decoration: none; color: #555555; font-weight: bold;\"> [FOLIO] </a></strong> ha sido rechazada por falta de fondos. Favor de realizar el deposito a la cuenta CLABE que le asignó su ejecutivo de cuenta. </p><p> </tr> <tr> <td valign=\"top\"> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr></tr> <tr> <td valign=\"top\"> <!-- start button --> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"background-color: rgb(0, 194, 231); border-radius: 3px 3px 3px 3px; background-clip: padding-box; font-size: 13px; font-family: Arial,Tahoma,Helvetica,sans-serif; text-align: center; color: rgb(255, 255, 255); font-weight: normal; padding-left: 18px; padding-right: 18px;\" align=\"center\" height=\"32\" valign=\"middle\" width=\"auto\"> <span style=\"color: #ffffff; font-weight: normal;\"> <a href=\"https://corporate.broxel.com/login.php\" style=\"text-decoration: none; color: #ffffff; font-weight: normal;\">Broxel Corporate Access </a> </span> </td> </tr> </tbody></table> <!-- end button --> </td> </tr> <tr></tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <td valign=\"top\"> </td> </tr> <tr> <td style=\"font-size: 13px; line-height: 22px; font-family:Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:left; \"><p><a style=\"text-decoration: none; color: #555555; font-weight: bold;\"><br><br>¿Dudas? </a>comuníquese con su ejecutivo de cuenta Broxel al <a style=\"text-decoration: none; color: #00C2E7; font-weight: bold;\"> teléfono 4433-0303. </a><br/><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">Gracias por utilizar nuestra plataforma Broxel Corporate Access.</a></td> </tr> <tr> <td align=\"center\" valign=\"top\" width=\"auto\"> </td> </tr> </tbody></table> </td> </tr> <!-- end text content --> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> <!-- end layout-1 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-1 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT 1 --> <!-- START LAYOUT Servicio al Cliente --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td valign=\"top\"> <!-- start layout-6 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-6 container width 560px --> <table class=\"full-width\" style=\"background-color: #ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <!-- start image content --> <tbody><tr> <td valign=\"top\" width=\"100%\"> <!-- start content left --> <table class=\"full-width\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td align=\"left\" valign=\"top\"> <a style=\"text-decoration: none;\" href=\"#\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-tel.png\" alt=\"tel\" width=\"127\" height=\"127\" hspace=\"0\" vspace=\"0\" border=\"0\" style=\"display:block; max-width:125px; \"> </a> </td> <!-- space width --> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"20\"> <tbody><tr> <td valign=\"top\"></td> </tr> </tbody></table> </td> <!-- space width --> <!-- start text content --> <td valign=\"top\"> <table class=\"width-small\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start text content --> <tbody><tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 18px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#555555; font-weight:bold; text-align:left;\"> <span style=\"color: #555555; font-weight: bold;\"> <a href=\"#\" style=\"text-decoration: none; color: #555555; font-weight: bold;\">Servicio al Cliente</a> </span> </td> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 13px; line-height: 22px; font-family:Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:left; \"> Para Broxel lo más importante es ofrecerle un servicio oportuno y de calidad. Si tiene cualquier duda sobre alguno de estos servicios, solo llame al <strong><a style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">01800-Broxel-1</a></strong> (01800-276935-1) y un ejecutivo con gusto le ayudará.</td> </tr> </tbody></table> </td> </tr> <!-- end text content --> </tbody></table> </td> <!-- end text content --> </tr> </tbody></table> </td> </tr> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> <!-- end content left --> </td> </tr> <!-- end image content --> </tbody></table> <!-- end layout-6 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-6 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT Servicio al Cliente --> <!-- START LAYOUT Aviso Privacidad --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <!-- start layout-1 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-1 container width 560px --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"509\"> <!--start space height --> <tbody><tr> <td width=\"509\" height=\"16\"></td> </tr> <!--end space height --> <!-- start text content --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody> <!--start space height --> <tr> <td height=\"0\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 11px; line-height: 16px; font-family: Arial,Tahoma, Helvetica, sans-serif; color: #a3a2a2; font-weight: normal; text-align: center;\"><p><a href=\"http://www.broxel.com/index.php/aviso-privacidad-sapi\" style=\"text-decoration: none; color: #555555; font-weight: bold;\"><span style=\"color: rgb(0, 194, 231); font-weight: bold;\">Aviso de Privacidad</span></a></p> <p> Sus datos se encuentran protegidos a través de nuestro Aviso de Privacidad.<br>Consulte nuestro Aviso de Privacidad en: <a href=\"http://www.broxel.com/index.php/aviso-privacidad-sapi\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">www.broxel.com</a>.</p> <p> Este correo electrónico está destinado a residentes de México y fue enviado desde Broxel.<br/> Si ha recibido este correo de una dirección diferente, significa que fue reenviado.</p> <p>Las comunicaciones vía correo electrónico de Broxel se basan en el manejo de cookies<br/> y/o Web Beacons.</p> <p>Al recibir este correo y consultar el contenido del mismo, usted consiente que Broxel recabe y tenga acceso a sus datos personales.</p> <p>Para más información favor de consultar <strong><a href=\"http://www.broxel.com/index.php/terminos-condiciones\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">Términos y Condiciones.</a></strong></p></td> </tr> <tr> <td align=\"center\" valign=\"top\" width=\"auto\"> </td> </tr> </tbody></table> </td> </tr> <!-- end text content --> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> <!-- end layout-1 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-1 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT 67 --> <!-- START LAYOUT 7--> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <!-- start layout-7 container --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td valign=\"top\"> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <tr> <td valign=\"top\"> <!-- start logo footer and address --> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td valign=\"top\"> <!--start icon socail navigation --> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td align=\"left\" valign=\"top\"> <table class=\"container\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://www.facebook.com/BroxelSF\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-facebook.jpg\" alt=\"icon-facebook\" style=\"max-width:33px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> <td style=\"padding-left:5px; \" class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://twitter.com/BroxelSF\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-twitter.jpg\" alt=\"icon-twitter\" style=\"max-width:30px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> <td style=\"padding-left:5px; \" class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://plus.google.com/u/0/116529217974857845752/posts\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-googleplus.jpg\" alt=\"icon-googleplus\" style=\"max-width:30px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> </tr> </tbody></table> </td> </tr> </tbody></table> <!--end icon socail navigation --> </td> </tr> </tbody></table> <!-- end logo footer and address --> </td> </tr> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> <!-- end layout-7 container --> </td> </tr> <!-- END LAYOUT-7--> <!-- START FOOTER COPY RIGHT --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <!-- start layout-7 container --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <tbody><tr> <!-- start unsubscribe content --> <td valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 13px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:center;\" class=\"text-center\" align=\"center\"> Síguanos por facebook, twitter y google +<br /> Visitenos en <a href=\"http://broxel.com\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">www.broxel.com</a> para conocer más de nuestros productos.</td> </tr> </tbody></table> </td> <!-- end unsubscribe content --> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <!-- start COPY RIGHT content --> <td style=\"font-size: 11px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:center;\" valign=\"top\">© 2014 BROXEL | Servicios Financieros. Todos los derechos Reservados.</td> <!-- end COPY RIGHT content --> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END FOOTER COPY RIGHT --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> </table></body> </html>";
                //    html = html.Replace("[CLIENTE]", cargoSolicitud[0].ClaveCliente);
                //    html = html.Replace("[FOLIO]", cargoSolicitud[0].Folio);
                //    SendMail("dispersiones@broxel.com",
                //        ConfigurationManager.AppSettings["emailsAvisosCargos"] +
                //        (cargoSolicitud[0].UsuarioEjecucion.Contains("@") ? (", " + cargoSolicitud[0].UsuarioEjecucion) : "") +
                //        (cargoSolicitud[0].UsuarioAprobacion.Contains("@") ? (", " + cargoSolicitud[0].UsuarioAprobacion) : "") +
                //        (cargoSolicitud[0].UsuarioCreacion.Contains("@") ? (", " + cargoSolicitud[0].UsuarioCreacion) : ""),
                //        "Solicitud de asignación de línea rechazada - cliente :" + cargoSolicitud[0].ClaveCliente,
                //        html, "yMQ3E3ert6");
                //    return;
                //}
                var cuentasCobrables = mysqlHelper.CuentasCobrables.ToList();


                cargoSolicitud[0].Estado = "Ejecutando";
                broxelSqlHelper.SaveChanges();
                var cargoss =
                    broxelSqlHelper.CargosDetalle.Where(
                        x => x.IdSolicitud == idCargoSolicitud && ((x.CodigoRespuesta != -1 || x.CodigoRespuesta == null) && x.Importe > 0.0m)) // && x.Id==40)
                        .OrderBy(x => x.Id);
                var cargos = cargoss.ToList();
                foreach (var cargo in cargos)
                {
                    //Thread.Sleep(1000);
                    count++;
                    if (!cargoSolicitud[0].CuentasMultiples)
                        if (cargos.Count(x => x.Cuenta == cargo.Cuenta) != 1)
                        {
                            cargo.CodigoRespuesta = 989;
                            continue;
                        }
                    try
                    {
                        var cuenta = cuentasCobrables.FirstOrDefault(x => x.Uno == cargo.Cuenta);
                        if (cuenta != null)
                        {
                            t = new Tarjeta
                            {
                                Cuenta = cargo.Cuenta,
                                NumeroTarjeta = "522339000" + cuenta.Dos,
                                FechaExpira = cuenta.Tres,
                                Cvc2 = new Random().Next(0, 999).ToString("D3")
                            };
                            webService.ActivacionDeCuenta(cargo.Cuenta, "cargosaldo@cuentafalsa.com");
                        }
                        else
                        {
                            t = GetTarjetaFromCuenta(cargo.Cuenta);
                        }
                        if (t != null)
                        {
                            try
                            {
                                if (cargo.Importe > 0 && cargo.CodigoRespuesta != -1)
                                {
                                    //TODO  MLS Revivir en base al campo Cargo por remanente y si el saldo es menor actualizar el importe del cargo al saldo
                                    if (cargoSolicitud[0].CargoPorRemanente)
                                    {
                                        cargo.SaldoAntes = webService.GetSaldosPorCuenta(cargo.Cuenta).Saldos.DisponibleCompras;
                                        if (cargo.Importe > cargo.SaldoAntes)
                                            cargo.Importe = (decimal)cargo.SaldoAntes;
                                    }
                                    //
                                    var res = webService.SetCargoConCuenta(cargo.Importe, t.NombreTarjeta, t.NumeroTarjeta,
                                        t.Cvc2, t.FechaExpira, folio + "-" + cargo.Id.ToString(),
                                        comercio.Comercio1, usuarioCargo, idComercio, cargo.Cuenta);
                                    cargo.NumeroAutorizacion = res.NumeroAutorizacion;
                                    cargo.FechaCargo = DateTime.Now;
                                    cargo.IdMovimiento = res.IdMovimiento;
                                    cargo.CodigoRespuesta = res.CodigoRespuesta;
                                    //cargo.SaldoDespues = webService.GetSaldosPorCuenta(cargo.Cuenta).Saldos.DisponibleCompras;
                                }
                            }
                            catch (Exception ex)
                            {
                                cargo.CodigoRespuesta = 990;
                            }
                        }

                        else
                            cargo.CodigoRespuesta = 993;
                        if (count >= 35)
                        {
                            broxelSqlHelper.SaveChanges();
                            count = 0;
                        }
                    }
                    catch (Exception e)
                    {
                        //TODO: Guardar en bitacora
                        SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com", "Problema Disp " + folio, "Fallo el cargo folio " + folio + " en cuenta " + cargo.Cuenta + "  " + e, "yMQ3E3ert6");
                    }
                }
                broxelSqlHelper.SaveChanges();
                ActualizaYMandaMailCargo(cargoSolicitud[0]);
            }
        }

        private static void ActualizaYMandaMailCargo(CargosSolicitudes pCargoSolicitud)
        {
            try
            {
                var broxelSqlHelper = new BroxelSQLEntities();
                var mysqlHelper = new broxelco_rdgEntities();

                //decimal antesPOS = 0, despuesPOS = 0, posSolicitado = 0;
                decimal posSolicitado = 0;
                int correctasPOS = 0;
                var errores = new List<ErroresDispersion>();

                var cargoSolicitud = broxelSqlHelper.CargosSolicitudes.FirstOrDefault(x => x.Id == pCargoSolicitud.Id);

                var cargosDetalles = broxelSqlHelper.CargosDetalle.Where(x => x.IdSolicitud == cargoSolicitud.Id && x.Importe > 0).ToList();
                if (cargosDetalles.Count >= 1)
                {
                    //antesPOS = (decimal)(cargosDetalles.Sum(x => x.SaldoAntes));
                    //despuesPOS = (decimal)cargosDetalles.Sum(x => x.SaldoDespues);
                    correctasPOS = cargosDetalles.Count(x => x.CodigoRespuesta == -1);
                    posSolicitado = (decimal)(cargosDetalles.Sum(x => x.Importe));
                }

                if (cargoSolicitud != null)
                {
                    cargoSolicitud.ImporteTotal = cargosDetalles.Where(x => x.CodigoRespuesta == -1).Sum(x => x.Importe);
                    if (correctasPOS == cargosDetalles.Count(x => x.Importe != 0))
                        cargoSolicitud.Estado = "COMPLETA";
                    else
                    {
                        cargoSolicitud.Estado = "CON ERRORES";
                        var codigosRespuesta = mysqlHelper.CodigosRespuesta.ToList();  // este si es mysql
                        var codigos = cargosDetalles.Select(x => x.CodigoRespuesta).Distinct();
                        codigos = codigos.Where(x => x != -1).ToList();
                        foreach (var codig in codigos)
                        {
                            var codigo = Convert.ToInt32(codig);
                            var firstOrDefault = codigosRespuesta.FirstOrDefault(x => x.Id == codigo);
                            if (firstOrDefault != null)
                            {
                                var e = new ErroresDispersion
                                {
                                    CodigoRespuesta = codigo,
                                    DescripcionCodigoResp = firstOrDefault.Descripcion,
                                    CausaComunCodigoResp = firstOrDefault.CausaComun
                                };
                                var cuentasConError = cargosDetalles.Where(x => x.CodigoRespuesta == codig).ToList();
                                foreach (var cuentaConError in cuentasConError)
                                {
                                    e.CuentasConError.Add(cuentaConError.Cuenta);
                                }
                                errores.Add(e);
                            }
                        }
                    }
                    broxelSqlHelper.SaveChanges();

                    ThreadPool.QueueUserWorkItem(delegate
                    {
                        new CargoBL().ActualizaControlCuentasDetalle(pCargoSolicitud.Folio);
                    });

                    //GeneraComisionYCargoCargos(pagoSolicitud, despuesPOS - antesPOS);
                    EnviarMailCargos(cargoSolicitud.EmailNotificacion, cargoSolicitud.Folio, Convert.ToDecimal(cargoSolicitud.ImporteTotal), cargoSolicitud.Estado, cargosDetalles.Count, cargoSolicitud.ClaveCliente, errores, posSolicitado);
                }
            }
            catch (Exception e)
            {
                SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com ", "Problema Cargo " + pCargoSolicitud.Folio, "Fallo el pago en ActualizaYMandaMailCargo con folio" + pCargoSolicitud.Folio + "  " + e, "yMQ3E3ert6");
            }
        }

        private static void EnviarMailCargos(string emailNotificar, string folio, decimal totalCargo, string estado, int cuantas, string claveCliente, List<ErroresDispersion> erroresCargos, decimal posSolicitado)
        {
            var mysqlHelper = new broxelco_rdgEntities();
            var broxelSqlHelper = new BroxelSQLEntities();
            int cuentasConError = 0;
            var From = "dispersiones@broxel.com";
            var nombreCliente = mysqlHelper.clientesBroxel.Where(x => x.claveCliente == claveCliente).ToList().First().razonSocial;
            var Subject = String.Format("Solicitud de cargo masivo terminada - cliente : {0}", claveCliente);
            var sb = new StringBuilder();
            var htmlIni =
                "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"> <html xmlns=\"http://www.w3.org/1999/xhtml\"> <head> <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" /> <meta name=\"viewport\" content=\"initial-scale=1.0\"/> <meta name=\"format-detection\" content=\"telephone=no\"/> <title>Broxel Proceso de Asignación</title> <style type=\"text/css\"> /* Resets: see reset.css for details */ .ReadMsgBody { width: 100%; background-color: #ffffff;} .ExternalClass {width: 100%; background-color: #ffffff;} .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {line-height:100%;} html{width: 100%; } body {-webkit-text-size-adjust:none; -ms-text-size-adjust:none; } body {margin:0; padding:0;} table {border-spacing:0;} img{display:block !important;} table td {border-collapse:collapse;} .yshortcuts a {border-bottom: none !important;} /* main color = #f05e5e other color = #b91925 background color = #ececec */ img{height:auto !important;} @media only screen and (max-width: 640px){ body{ width:auto!important; } table[class=\"container\"]{ width: 100%!important; padding-left: 20px!important; padding-right: 20px!important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; } img[class=\"small-image-100-percent\"]{ width:100% !important; height:auto !important; } table[class=\"full-width\"]{ width:100% !important; } table[class=\"full-width-text\"]{ width:100% !important; background-color:#b91925; padding-left:20px !important; padding-right:20px !important; } table[class=\"full-width-text2\"]{ width:100% !important; background-color:#f3f3f3; padding-left:20px !important; padding-right:20px !important; } table[class=\"col-2-3img\"]{ width:50% !important; margin-right: 20px !important; } table[class=\"col-2-3img-last\"]{ width:50% !important; } table[class=\"col-2\"]{ width:47% !important; margin-right:20px !important; } table[class=\"col-2-last\"]{ width:47% !important; } table[class=\"col-3\"]{ width:29% !important; margin-right:20px !important; } table[class=\"col-3-last\"]{ width:29% !important; } table[class=\"row-2\"]{ width:50% !important; } td[class=\"text-center\"]{ text-align: center !important; } /* start clear and remove*/ table[class=\"remove\"]{ display:none !important; } td[class=\"remove\"]{ display:none !important; } /* end clear and remove*/ table[class=\"fix-box\"]{ padding-left:20px !important; padding-right:20px !important; } td[class=\"fix-box\"]{ padding-left:20px !important; padding-right:20px !important; } td[class=\"font-resize\"]{ font-size: 18px !important; line-height: 22px !important; } } @media only screen and (max-width: 479px){ body{ font-size:10px !important; } table[class=\"container2\"]{ width: 100%!important; float:none !important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } img[class=\"small-image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } table[class=\"full-width\"]{ width:100% !important; } table[class=\"full-width-text\"]{ width:100% !important; background-color:#b91925; padding-left:20px !important; padding-right:20px !important; } table[class=\"full-width-text2\"]{ width:100% !important; background-color:#f3f3f3; padding-left:20px !important; padding-right:20px !important; } table[class=\"col-2\"]{ width:100% !important; margin-right:0px !important; } table[class=\"col-2-last\"]{ width:100% !important; } table[class=\"col-3\"]{ width:100% !important; margin-right:0px !important; } table[class=\"col-3-last\"]{ width:100% !important; } table[class=\"row-2\"]{ width:100% !important; } table[id=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } td[id=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } td[class=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } /*start text center*/ td[class=\"text-center\"]{ text-align: center !important; } div[class=\"text-center\"]{ text-align: center !important; } /*end text center*/ /* start clear and remove */ table[id=\"clear-padding\"]{ padding:0 !important; } td[id=\"clear-padding\"]{ padding:0 !important; } td[class=\"clear-padding\"]{ padding:0 !important; } table[class=\"remove-479\"]{ display:none !important; } td[class=\"remove-479\"]{ display:none !important; } table[class=\"clear-align\"]{ float:none !important; } /* end clear and remove */ table[class=\"width-small\"]{ width:100% !important; } table[class=\"fix-box\"]{ padding-left:0px !important; padding-right:0px !important; } td[class=\"fix-box\"]{ padding-left:0px !important; padding-right:0px !important; } td[class=\"font-resize\"]{ font-size: 14px !important; } } @media only screen and (max-width: 320px){ table[class=\"width-small\"]{ width:125px !important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } } </style> </head> <body style=\"font-size:12px;\"> <table id=\"mainStructure\" style=\"background-color:#cccccc;\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- START TAB TOP --> <tr> <td valign=\"top\"> <table style=\"background-color: rgb(204, 204, 204);\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td height=\"6\" valign=\"top\"> <table class=\"full-width\" style=\"height:6px;\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> </tr> <!-- start space height --> <tr> <td height=\"5\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END TAB TOP --> <!--START TOP NAVIGATION ​LAYOUT--> <tr> <td valign=\"top\"> <table style=\"background-color: rgb(204, 204, 204);\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <!-- START TAB TOP --> <tr> <td class=\"fix-box\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td height=\"6\" valign=\"top\"> <table class=\"full-width\" style=\"height:6px;\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td class=\"full-width\" style=\"background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <table class=\"clear-align\" style=\"height: 6px; background-color: rgb(0, 194, 231);\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"282\"> <tbody><tr> <td height=\"6\" valign=\"top\"></td> </tr> </tbody></table> </td> <td class=\"remove-479\" style=\"width: 100%; background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <table class=\"clear-align\" style=\"height: 6px; background-color: rgb(0, 194, 231);\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td height=\"6\" valign=\"top\"></td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END TAB TOP --> <!-- START CONTAINER NAVIGATION --> <tr> <td class=\"fix-box\" align=\"center\" valign=\"top\"> <!-- start top navigation container --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start top navigaton --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <table class=\"container2\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <a style=\"text-decoration: none;\" href=\"#\"><img src=\"https://online.broxel.com/Images/Comunicados/img/logo2.png\" style=\"max-width: 310px; background-color: rgb(30, 194, 229);\" alt=\"Logo\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"293\"></a> </td> </tr> </tbody></table> <!--start content nav --> <table class=\"container2\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td class=\"remove-479\" height=\"13\" valign=\"top\"></td> </tr> <!--start call us --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 13px; line-height: 18px; color: #555555; font-weight:bold; text-align: center; font-family:Arail,Tahoma, Helvetica, Arial, sans-serif;\"><div align=\"left\"><span style=\"color: rgb(30, 194, 229);\">Estimad@</span><br/>[CLIENTE]</div></td> </tr> </tbody></table> </td> <!--start space width --> <!--start space width --> </tr> <!--end call us --> <tr> <td height=\"14\" valign=\"top\"></td> </tr> </tbody></table> <!--end content nav --> </td> </tr> </tbody></table> <!-- end top navigaton --> </td> </tr> </tbody></table> <!-- end top navigation container --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- END CONTAINER NAVIGATION --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!--END TOP NAVIGATION ​LAYOUT--> <!-- START LAYOUT 1 --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <!-- start layout-1 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-1 container width 560px --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <!-- start text content --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 18px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#555555; font-weight:bold; text-align:center;\"><div align=\"left\"><span style=\"color: rgb(0, 194, 231); font-weight:bold;\"> Proceso de cargos masivos terminado </span></div></td> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 13px; line-height: 22px; font-family:Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:left; \"><p>Le informamos que su solicitud de cargo masivo en línea, con el número de folio <strong><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">[FOLIO]</a></strong><br/> arrojó los siguientes resultados:</p></td> </tr> <tr> <td valign=\"top\"> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <!--start Tabla Cuentas --> <tr> <td height=\"15\"><table width=\"420\" Table Border=\"1\" Bordercolor=#ffffff cellspacing=\"2\" cellpadding=\"2\"> <tr> <td width=\"370\" align=\"left\" bgcolor=\"#f2f2f2\" scope=\"row\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\"><p style=\"margin: 0; padding-left: 8px;\">Total de cuentas solicitadas:</a></td> <td width=\"100\" align=\"center\" bgcolor=\"#e6e6e6\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\">[SOLICITADAS]</a></td> </tr> <tr> <th align=\"left\" bgcolor=\"#f2f2f2\" scope=\"row\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\"><p style=\"margin: 0; padding-left: 8px;\">Total de cuentas procesadas con cargo :</a></th> <td align=\"center\" bgcolor=\"#e6e6e6\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\">[BIEN]</a></td> </tr> </table></td> </tr> <!--end Tabla Cuentas --> <tr></tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <!--start Tabla Monto y Total --> <tr> <td height=\"15\"><table width=\"420\" Table Border=\"1\" Bordercolor=#ffffff cellspacing=\"2\" cellpadding=\"2\"><tr> <th align=\"left\" bgcolor=\"#f2f2f2\" scope=\"row\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\"><p style=\"margin: 0; padding-left: 8px;\">Total de cargo aplicado</a></th> <td align=\"center\" bgcolor=\"#e6e6e6\"><a style=\"font-size: 13px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\">[POSREAL]</a></td> </tr> <!--end space height --> <tr> <td valign=\"top\"> </td> </tr> <tr> <td style=\"font-size: 13px; line-height: 22px; font-family:Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:left; \"><p><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">";
            htmlIni = htmlIni.Replace("[CLIENTE]", nombreCliente);
            htmlIni = htmlIni.Replace("[FOLIO]", folio);
            htmlIni = htmlIni.Replace("[SOLICITADAS]", cuantas.ToString());
            htmlIni = htmlIni.Replace("[POSREAL]", PrintCurrency(totalCargo));

            sb.Append(htmlIni);
            if (estado != "COMPLETA")
            {
                var listaDescripcionError = mysqlHelper.CodigosRespuesta.ToList();

                foreach (var error in erroresCargos)
                {
                    var descripcionError = listaDescripcionError.FirstOrDefault(x => x.Id == error.CodigoRespuesta).Descripcion;
                    sb.AppendFormat("<a style=\"text-decoration: none; color:#a3a2a2; font-weight: normal;\"><br><br>Las siguientes </a></strong><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">{0}</a></strong><a style=\"text-decoration: none; color:#a3a2a2; font-weight: normal;\"> cuentas no pudieron ser procesadas por código </a><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">{1}.</a></strong></strong><br/>", error.CuentasConError.Count, error.CodigoRespuesta);
                    foreach (var cuenta in error.CuentasConError)
                    {
                        sb.AppendFormat("<br/>{0}", cuenta);
                        cuentasConError++;
                    }
                }
            }
            sb = sb.Replace("[BIEN]", (cuantas - cuentasConError).ToString());
            sb.Append("<br><br>¿Dudas? </a>comuníquese con su ejecutivo de cuenta Broxel al <a style=\"text-decoration: none; color: #00C2E7; font-weight: bold;\"> teléfono 4433-0303. </a><br/><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">Gracias por utilizar nuestra plataforma Broxel Corporate Access.</a></td> </tr> <tr> <td align=\"center\" valign=\"top\" width=\"auto\"> </td> </tr> </tbody></table> </td> </tr> <!-- end text content --> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> <!-- end layout-1 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-1 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT 1 --> <!-- START LAYOUT Servicio al Cliente --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td valign=\"top\"> <!-- start layout-6 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-6 container width 560px --> <table class=\"full-width\" style=\"background-color: #ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <!-- start image content --> <tbody><tr> <td valign=\"top\" width=\"100%\"> <!-- start content left --> <table class=\"full-width\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td align=\"left\" valign=\"top\"> <a style=\"text-decoration: none;\" href=\"#\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-tel.png\" alt=\"tel\" width=\"127\" height=\"127\" hspace=\"0\" vspace=\"0\" border=\"0\" style=\"display:block; max-width:125px; \"> </a> </td> <!-- space width --> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"20\"> <tbody><tr> <td valign=\"top\"></td> </tr> </tbody></table> </td> <!-- space width --> <!-- start text content --> <td valign=\"top\"> <table class=\"width-small\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start text content --> <tbody><tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 18px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#555555; font-weight:bold; text-align:left;\"> <span style=\"color: #555555; font-weight: bold;\"> <a href=\"#\" style=\"text-decoration: none; color: #555555; font-weight: bold;\">Servicio al Cliente</a> </span> </td> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 13px; line-height: 22px; font-family:Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:left; \"> Para Broxel lo más importante es ofrecerle un servicio oportuno y de calidad. Si tiene cualquier duda sobre alguno de estos servicios, solo llame al <strong><a style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">01800-Broxel-1</a></strong> (01800-276935-1) y un ejecutivo con gusto le ayudará.</td> </tr> </tbody></table> </td> </tr> <!-- end text content --> </tbody></table> </td> <!-- end text content --> </tr> </tbody></table> </td> </tr> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> <!-- end content left --> </td> </tr> <!-- end image content --> </tbody></table> <!-- end layout-6 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-6 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT Servicio al Cliente --> <!-- START LAYOUT Aviso Privacidad --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <!-- start layout-1 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-1 container width 560px --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"509\"> <!--start space height --> <tbody><tr> <td width=\"509\" height=\"16\"></td> </tr> <!--end space height --> <!-- start text content --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody> <!--start space height --> <tr> <td height=\"0\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 11px; line-height: 16px; font-family: Arial,Tahoma, Helvetica, sans-serif; color: #a3a2a2; font-weight: normal; text-align: center;\"><p><a href=\"http://www.broxel.com/index.php/aviso-privacidad-sapi\" style=\"text-decoration: none; color: #555555; font-weight: bold;\"><span style=\"color: rgb(0, 194, 231); font-weight: bold;\">Aviso de Privacidad</span></a></p> <p> Sus datos se encuentran protegidos a través de nuestro Aviso de Privacidad.<br>Consulte nuestro Aviso de Privacidad en: <a href=\"http://www.broxel.com/index.php/aviso-privacidad-sapi\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">www.broxel.com</a>.</p> <p> Este correo electrónico está destinado a residentes de México y fue enviado desde Broxel.<br/> Si ha recibido este correo de una dirección diferente, significa que fue reenviado.</p> <p>Las comunicaciones vía correo electrónico de Broxel se basan en el manejo de cookies<br/> y/o Web Beacons.</p> <p>Al recibir este correo y consultar el contenido del mismo, usted consiente que Broxel recabe y tenga acceso a sus datos personales.</p> <p>Para más información favor de consultar <strong><a href=\"http://www.broxel.com/index.php/terminos-condiciones\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">Términos y Condiciones.</a></strong></p></td> </tr> <tr> <td align=\"center\" valign=\"top\" width=\"auto\"> </td> </tr> </tbody></table> </td> </tr> <!-- end text content --> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> <!-- end layout-1 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-1 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT 67 --> <!-- START LAYOUT 7--> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <!-- start layout-7 container --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td valign=\"top\"> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <tr> <td valign=\"top\"> <!-- start logo footer and address --> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td valign=\"top\"> <!--start icon socail navigation --> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td align=\"left\" valign=\"top\"> <table class=\"container\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://www.facebook.com/BroxelSF\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-facebook.jpg\" alt=\"icon-facebook\" style=\"max-width:33px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> <td style=\"padding-left:5px; \" class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://twitter.com/BroxelSF\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-twitter.jpg\" alt=\"icon-twitter\" style=\"max-width:30px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> <td style=\"padding-left:5px; \" class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://plus.google.com/u/0/116529217974857845752/posts\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-googleplus.jpg\" alt=\"icon-googleplus\" style=\"max-width:30px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> </tr> </tbody></table> </td> </tr> </tbody></table> <!--end icon socail navigation --> </td> </tr> </tbody></table> <!-- end logo footer and address --> </td> </tr> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> <!-- end layout-7 container --> </td> </tr> <!-- END LAYOUT-7--> <!-- START FOOTER COPY RIGHT --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <!-- start layout-7 container --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <tbody><tr> <!-- start unsubscribe content --> <td valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 13px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:center;\" class=\"text-center\" align=\"center\"> Síguanos por facebook, twitter y google +<br /> Visitenos en <a href=\"http://broxel.com\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">www.broxel.com</a> para conocer más de nuestros productos.</td> </tr> </tbody></table> </td> <!-- end unsubscribe content --> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <!-- start COPY RIGHT content --> <td style=\"font-size: 11px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:center;\" valign=\"top\">© 2015 BROXEL | Servicios Financieros. Todos los derechos Reservados.</td> <!-- end COPY RIGHT content --> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END FOOTER COPY RIGHT --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> </table></body> </html>");
            SendMail(From, ConfigurationManager.AppSettings["emailsAvisosCargos"] + " , " + emailNotificar, Subject, sb.ToString(), "yMQ3E3ert6", "Broxel : Asignacion de Lineas");

        }

        // Inicio MLS Cambio en congeladora, no estaba en la version productiva

        internal static void CorrerRenominacion(string folio)
        {
            var dbHelper = new broxelco_rdgEntities();
            Tarjeta t;
            int POS = 0;
            var webService = new BroxelService();
            var count = 0;
            List<maquila> cuentas = new List<maquila>();
            var mySql = new MySqlDataAccess();

            var renominacionSolicitud = dbHelper.RenominacionSolicitudes.Where(x => x.Folio == folio && x.Estado == "Validando").ToList();
            if (renominacionSolicitud.Count == 1 && renominacionSolicitud[0].Estado == "Validando")
            {
                renominacionSolicitud[0].Estado = "Ejecutando";
                dbHelper.SaveChanges();
                // Inicio MLS pre renominación
                var i = 0;
                while (true)
                {
                    var msg = "";
                    if (mySql.SpPreRenominacion(renominacionSolicitud[0].Id, ref msg))
                        break;
                    i++;
                    if (i > 2)
                        throw new Exception("No fue posible ejecutar el proceso previo a la renominación: " + msg);
                }
                // Fin MLS pre renominación
                var renominaciones =
                    dbHelper.RenominacionesInternas.Where(
                        x => x.FolioSolicitud == folio && (x.CodigoRespuesta != "-1" || x.CodigoRespuesta == null))
                        .OrderBy(x => x.Id)
                        .ToList();
                foreach (var renominacion in renominaciones)
                {
                    count++;
                    try
                    {
                        t = GetTarjetaFromCuenta(renominacion.Cuenta);
                        if (t != null)
                        {
                            try
                            {
                                if (renominacion.CodigoRespuesta != "-1")
                                {
                                    //var res = webService.TransferenciaDeCuentas(renominacion.Cuenta, Convert.ToDecimal(renominacion.Monto), t.NumeroTarjeta, renomincacionSolicitud[0].UsuarioCreacion, "Concentradora");
                                    var res = webService.NominacionTarjeta(renominacion.Cuenta, renominacion.Colonia,
                                        renominacion.NombreCalle, renominacion.Tarjeta, renominacion.CodigoPostal,
                                        renominacion.NombreMunicipio, renominacion.NumeroCalle,
                                        renominacion.TipoDireccion, renominacion.CodigoEstado,
                                        renominacion.CodigoMunicipio, renominacion.NumeroDeDocumento,
                                        renominacion.TipoDocumento, renominacion.GrupoLiquidacion, renominacion.Telefono,
                                        Convert.ToDateTime(renominacion.FechaNacimiento), renominacion.EstadoCivil, renominacion.Hijos,
                                        renominacion.Ocupacion, renominacion.Sexo, renominacion.DenominacionTarjeta,
                                        renominacion.NombreCompletoTitular);
                                    renominacion.CodigoAutorizacion = res.NumeroAutorizacion;
                                    renominacion.CodigoRespuesta = res.CodigoRespuesta.ToString(CultureInfo.InvariantCulture);
                                    renominacion.IdRenominacion = res.IdRenominacion;
                                    //var maq = dbHelper.maquila.FirstOrDefault(x => x.num_cuenta == renominacion.Cuenta);
                                    //maq.nombre_titular = renominacion.NombreCompletoTitular;
                                    //maq.nombre_tarjethabiente = renominacion.DenominacionTarjeta;
                                    //cuentas.Add(maq);
                                }
                            }
                            catch (Exception ex)
                            {
                                renominacion.CodigoRespuesta = "990";
                                SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com ", "Problema renominacion" + folio, "Fallo renominacion folio " + folio + " en Id " + renominacion.Id + "  " + ex, "yMQ3E3ert6");
                            }
                        }
                        else
                            renominacion.CodigoRespuesta = "993";
                        if (count >= 35)
                        {
                            dbHelper.SaveChanges();
                            count = 0;
                            //cuentas = new List<maquila>();
                        }
                    }
                    catch (Exception e)
                    {
                        //TODO: Guardar en bitacora
                        SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com ", "Problema renominacion" + folio, "Fallo renominacion folio " + folio + " en Id " + renominacion.Id + "  " + e, "yMQ3E3ert6");
                    }
                }
                try
                {
                    dbHelper.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    StringBuilder exception = new StringBuilder();
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        exception.AppendFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            exception.AppendFormat("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    Helper.SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com ", "Error en  CorrerRenominacion ", "Error " + exception.ToString(),
                          "yMQ3E3ert6", "Errores ");

                }
                // Inicio MLS post renominación
                var msgPr = "";
                if (!mySql.SpPostRenominacion(renominacionSolicitud[0].Id, ref msgPr))
                    SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com ", "Problema renominacion" + folio, "Fallo porst renominacion folio " + folio + " : " + msgPr, "yMQ3E3ert6");
                // Fin MLS post renominación

                ActualizaYMandaMailRenominacion(renominacionSolicitud.ElementAt(0), folio);
            }
        }

        private static void ActualizaYMandaMailRenominacion(RenominacionSolicitudes renominacionSolicitud, string folio)
        {
            try
            {
                broxelco_rdgEntities _broxelcoRdgEntities = new broxelco_rdgEntities();
                var dbHelper = new broxelco_rdgEntities();
                var errores = new List<ErroresDispersion>();

                var renominacionSolicitudes = dbHelper.RenominacionesInternas.Where(x => x.FolioSolicitud == folio).ToList();

                var solicitudRenominacion = dbHelper.RenominacionSolicitudes.FirstOrDefault(x => x.Folio == folio);
                if (solicitudRenominacion != null)
                {
                    if (renominacionSolicitudes.Count() == renominacionSolicitudes.Count(x => x.IdRenominacion != 0))
                        solicitudRenominacion.Estado = "COMPLETA";
                    else
                    {
                        solicitudRenominacion.Estado = "CON ERRORES";
                        var codigos = renominacionSolicitudes.Select(x => x.CodigoRespuesta).Distinct();
                        codigos = codigos.Where(x => x != "-1").ToList();
                        foreach (var codig in codigos)
                        {
                            var e = new ErroresDispersion
                            {
                                CodigoRespuesta = Convert.ToInt32(codig),
                                DescripcionCodigoResp = "",
                                CausaComunCodigoResp = ""
                            };
                            var cuentasConError = renominacionSolicitudes.Where(x => x.CodigoRespuesta == codig).ToList();
                            foreach (var cuentaConError in cuentasConError)
                            {
                                e.CuentasConError.Add(cuentaConError.Cuenta);
                            }
                            errores.Add(e);
                        }
                    }
                    dbHelper.SaveChanges();
                    EnviarMailRenominacion(solicitudRenominacion.Email, folio, "RENOMINACION",
                       solicitudRenominacion.Estado, renominacionSolicitudes.Count, solicitudRenominacion.ClaveCliente, errores);
                }
            }
            catch (Exception e)
            {
                SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com ", "Problema Disp " + folio, "Fallo la dispersion en ActualizaYMandaMail con folio" + folio + "  " + e, "yMQ3E3ert6");
            }
        }

        private static void EnviarMailRenominacion(string emailNotificar, string folio, string tipo, string estado, int cuantas, string cliente, List<ErroresDispersion> erroresDispersion)
        {
            var dbHelper = new broxelco_rdgEntities();
            int cuentasConError = 0;
            var From = "dispersiones@broxel.com";
            var nombreCliente = dbHelper.clientesBroxel.Where(x => x.claveCliente == cliente).ToList().First().razonSocial;
            var Subject = String.Format("Solicitud de renominacion terminada - cliente : {0}", cliente);
            var sb = new StringBuilder();
            var htmlIni =
                "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"> <html xmlns=\"http://www.w3.org/1999/xhtml\"> <head> <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" /> <meta name=\"viewport\" content=\"initial-scale=1.0\"/> <meta name=\"format-detection\" content=\"telephone=no\"/> <title>Broxel Proceso de Renominacion </title> <style type=\"text/css\"> /* Resets: see reset.css for details */ .ReadMsgBody { width: 100%; background-color: #ffffff;} .ExternalClass {width: 100%; background-color: #ffffff;} .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {line-height:100%;} html{width: 100%; } body {-webkit-text-size-adjust:none; -ms-text-size-adjust:none; } body {margin:0; padding:0;} table {border-spacing:0;} img{display:block !important;} table td {border-collapse:collapse;} .yshortcuts a {border-bottom: none !important;} /* main color = #f05e5e other color = #b91925 background color = #ececec */ img{height:auto !important;} @media only screen and (max-width: 640px){ body{ width:auto!important; } table[class=\"container\"]{ width: 100%!important; padding-left: 20px!important; padding-right: 20px!important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; } img[class=\"small-image-100-percent\"]{ width:100% !important; height:auto !important; } table[class=\"full-width\"]{ width:100% !important; } table[class=\"full-width-text\"]{ width:100% !important; background-color:#b91925; padding-left:20px !important; padding-right:20px !important; } table[class=\"full-width-text2\"]{ width:100% !important; background-color:#f3f3f3; padding-left:20px !important; padding-right:20px !important; } table[class=\"col-2-3img\"]{ width:50% !important; margin-right: 20px !important; } table[class=\"col-2-3img-last\"]{ width:50% !important; } table[class=\"col-2\"]{ width:47% !important; margin-right:20px !important; } table[class=\"col-2-last\"]{ width:47% !important; } table[class=\"col-3\"]{ width:29% !important; margin-right:20px !important; } table[class=\"col-3-last\"]{ width:29% !important; } table[class=\"row-2\"]{ width:50% !important; } td[class=\"text-center\"]{ text-align: center !important; } /* start clear and remove*/ table[class=\"remove\"]{ display:none !important; } td[class=\"remove\"]{ display:none !important; } /* end clear and remove*/ table[class=\"fix-box\"]{ padding-left:20px !important; padding-right:20px !important; } td[class=\"fix-box\"]{ padding-left:20px !important; padding-right:20px !important; } td[class=\"font-resize\"]{ font-size: 18px !important; line-height: 22px !important; } } @media only screen and (max-width: 479px){ body{ font-size:10px !important; } table[class=\"container2\"]{ width: 100%!important; float:none !important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } img[class=\"small-image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } table[class=\"full-width\"]{ width:100% !important; } table[class=\"full-width-text\"]{ width:100% !important; background-color:#b91925; padding-left:20px !important; padding-right:20px !important; } table[class=\"full-width-text2\"]{ width:100% !important; background-color:#f3f3f3; padding-left:20px !important; padding-right:20px !important; } table[class=\"col-2\"]{ width:100% !important; margin-right:0px !important; } table[class=\"col-2-last\"]{ width:100% !important; } table[class=\"col-3\"]{ width:100% !important; margin-right:0px !important; } table[class=\"col-3-last\"]{ width:100% !important; } table[class=\"row-2\"]{ width:100% !important; } table[id=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } td[id=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } td[class=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } /*start text center*/ td[class=\"text-center\"]{ text-align: center !important; } div[class=\"text-center\"]{ text-align: center !important; } /*end text center*/ /* start clear and remove */ table[id=\"clear-padding\"]{ padding:0 !important; } td[id=\"clear-padding\"]{ padding:0 !important; } td[class=\"clear-padding\"]{ padding:0 !important; } table[class=\"remove-479\"]{ display:none !important; } td[class=\"remove-479\"]{ display:none !important; } table[class=\"clear-align\"]{ float:none !important; } /* end clear and remove */ table[class=\"width-small\"]{ width:100% !important; } table[class=\"fix-box\"]{ padding-left:0px !important; padding-right:0px !important; } td[class=\"fix-box\"]{ padding-left:0px !important; padding-right:0px !important; } td[class=\"font-resize\"]{ font-size: 14px !important; } } @media only screen and (max-width: 320px){ table[class=\"width-small\"]{ width:125px !important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } } </style> </head> <body style=\"font-size:12px;\"> <table id=\"mainStructure\" style=\"background-color:#cccccc;\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- START TAB TOP --> <tr> <td valign=\"top\"> <table style=\"background-color: rgb(204, 204, 204);\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td height=\"6\" valign=\"top\"> <table class=\"full-width\" style=\"height:6px;\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> </tr> <!-- start space height --> <tr> <td height=\"5\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END TAB TOP --> <!--START TOP NAVIGATION ​LAYOUT--> <tr> <td valign=\"top\"> <table style=\"background-color: rgb(204, 204, 204);\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <!-- START TAB TOP --> <tr> <td class=\"fix-box\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td height=\"6\" valign=\"top\"> <table class=\"full-width\" style=\"height:6px;\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td class=\"full-width\" style=\"background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <table class=\"clear-align\" style=\"height: 6px; background-color: rgb(0, 194, 231);\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"282\"> <tbody><tr> <td height=\"6\" valign=\"top\"></td> </tr> </tbody></table> </td> <td class=\"remove-479\" style=\"width: 100%; background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <table class=\"clear-align\" style=\"height: 6px; background-color: rgb(0, 194, 231);\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td height=\"6\" valign=\"top\"></td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END TAB TOP --> <!-- START CONTAINER NAVIGATION --> <tr> <td class=\"fix-box\" align=\"center\" valign=\"top\"> <!-- start top navigation container --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start top navigaton --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <table class=\"container2\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <a style=\"text-decoration: none;\" href=\"#\"><img src=\"https://online.broxel.com/Images/Comunicados/img/logo2.png\" style=\"max-width: 310px; background-color: rgb(30, 194, 229);\" alt=\"Logo\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"293\"></a> </td> </tr> </tbody></table> <!--start content nav --> <table class=\"container2\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td class=\"remove-479\" height=\"13\" valign=\"top\"></td> </tr> <!--start call us --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 13px; line-height: 18px; color: #555555; font-weight:bold; text-align: center; font-family:Arail,Tahoma, Helvetica, Arial, sans-serif;\"><div align=\"left\"><span style=\"color: rgb(30, 194, 229);\">Estimad@</span><br/>[CLIENTE]</div></td> </tr> </tbody></table> </td> <!--start space width --> <!--start space width --> </tr> <!--end call us --> <tr> <td height=\"14\" valign=\"top\"></td> </tr> </tbody></table> <!--end content nav --> </td> </tr> </tbody></table> <!-- end top navigaton --> </td> </tr> </tbody></table> <!-- end top navigation container --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- END CONTAINER NAVIGATION --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!--END TOP NAVIGATION ​LAYOUT--> <!-- START LAYOUT 1 --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <!-- start layout-1 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-1 container width 560px --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <!-- start text content --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 18px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#555555; font-weight:bold; text-align:center;\"><div align=\"left\"><span style=\"color: rgb(0, 194, 231); font-weight:bold;\"> Proceso de renominacion terminado </span></div></td> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 13px; line-height: 22px; font-family:Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:left; \"><p>Le informamos que su solicitud de renominacion de cuentas, con el número de folio <strong><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">[FOLIO]</a></strong><br/> arrojó los siguientes resultados:</p></td> </tr> <tr> <td valign=\"top\"> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <!--start Tabla Cuentas --> <tr> <td height=\"15\"><table width=\"420\" Table Border=\"1\" Bordercolor=#ffffff cellspacing=\"2\" cellpadding=\"2\"> <tr> <td width=\"370\" align=\"left\" bgcolor=\"#f2f2f2\" scope=\"row\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\"><p style=\"margin: 0; padding-left: 8px;\">Total de cuentas solicitadas:</a></td> <td width=\"100\" align=\"center\" bgcolor=\"#e6e6e6\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\">[SOLICITADAS]</a></td> </tr> <tr> <th align=\"left\" bgcolor=\"#f2f2f2\" scope=\"row\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\"><p style=\"margin: 0; padding-left: 8px;\">Total de cuentas procesadas con renominación :</a></th> <td align=\"center\" bgcolor=\"#e6e6e6\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\">[BIEN]</a></td> </tr> </table></td> </tr> <!--end Tabla Cuentas --> <tr></tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <!--start Tabla Monto y Total --> <!--end Tabla Monto y Total --> <tr></tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <td valign=\"top\"> </td> </tr> <tr> <td style=\"font-size: 13px; line-height: 22px; font-family:Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:left; \"><p><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">";
            htmlIni = htmlIni.Replace("[CLIENTE]", nombreCliente);
            htmlIni = htmlIni.Replace("[FOLIO]", folio);
            htmlIni = htmlIni.Replace("[SOLICITADAS]", cuantas.ToString());

            sb.Append(htmlIni);
            if (estado != "COMPLETA")
            {
                var listaDescripcionError = dbHelper.CodigosRespuesta.ToList();

                foreach (var error in erroresDispersion)
                {
                    var descripcionError = listaDescripcionError.FirstOrDefault(x => x.Id == error.CodigoRespuesta).Descripcion;

                    sb.AppendFormat("<a style=\"text-decoration: none; color:#a3a2a2; font-weight: normal;\">Las siguientes </a></strong><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">{0}</a></strong><a style=\"text-decoration: none; color:#a3a2a2; font-weight: normal;\"> cuentas no pudieron ser procesadas por código </a><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">{1}.</a></strong></strong><br/>", error.CuentasConError.Count, descripcionError);
                    foreach (var cuenta in error.CuentasConError)
                    {
                        sb.AppendFormat("<br/>{0}", cuenta);
                        cuentasConError++;
                    }
                }
            }
            sb = sb.Replace("[BIEN]", (cuantas - cuentasConError).ToString());
            sb.Append("<br><br>¿Dudas? </a>comuníquese con su ejecutivo de cuenta Broxel al <a style=\"text-decoration: none; color: #00C2E7; font-weight: bold;\"> teléfono 4433-0303. </a><br/><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">Gracias por utilizar nuestra plataforma Broxel.</a></td> </tr> <tr> <td align=\"center\" valign=\"top\" width=\"auto\"> </td> </tr> </tbody></table> </td> </tr> <!-- end text content --> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> <!-- end layout-1 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-1 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT 1 --> <!-- START LAYOUT Servicio al Cliente --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td valign=\"top\"> <!-- start layout-6 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-6 container width 560px --> <table class=\"full-width\" style=\"background-color: #ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <!-- start image content --> <tbody><tr> <td valign=\"top\" width=\"100%\"> <!-- start content left --> <table class=\"full-width\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td align=\"left\" valign=\"top\"> <a style=\"text-decoration: none;\" href=\"#\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-tel.png\" alt=\"tel\" width=\"127\" height=\"127\" hspace=\"0\" vspace=\"0\" border=\"0\" style=\"display:block; max-width:125px; \"> </a> </td> <!-- space width --> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"20\"> <tbody><tr> <td valign=\"top\"></td> </tr> </tbody></table> </td> <!-- space width --> <!-- start text content --> <td valign=\"top\"> <table class=\"width-small\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start text content --> <tbody><tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 18px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#555555; font-weight:bold; text-align:left;\"> <span style=\"color: #555555; font-weight: bold;\"> <a href=\"#\" style=\"text-decoration: none; color: #555555; font-weight: bold;\">Servicio al Cliente</a> </span> </td> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 13px; line-height: 22px; font-family:Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:left; \"> Para Broxel lo más importante es ofrecerle un servicio oportuno y de calidad. Si tiene cualquier duda sobre alguno de estos servicios, solo llame al <strong><a style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">01800-Broxel-1</a></strong> (01800-276935-1) y un ejecutivo con gusto le ayudará.</td> </tr> </tbody></table> </td> </tr> <!-- end text content --> </tbody></table> </td> <!-- end text content --> </tr> </tbody></table> </td> </tr> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> <!-- end content left --> </td> </tr> <!-- end image content --> </tbody></table> <!-- end layout-6 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-6 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT Servicio al Cliente --> <!-- START LAYOUT Aviso Privacidad --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <!-- start layout-1 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-1 container width 560px --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"509\"> <!--start space height --> <tbody><tr> <td width=\"509\" height=\"16\"></td> </tr> <!--end space height --> <!-- start text content --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody> <!--start space height --> <tr> <td height=\"0\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 11px; line-height: 16px; font-family: Arial,Tahoma, Helvetica, sans-serif; color: #a3a2a2; font-weight: normal; text-align: center;\"><p><a href=\"http://www.broxel.com/index.php/aviso-privacidad-sapi\" style=\"text-decoration: none; color: #555555; font-weight: bold;\"><span style=\"color: rgb(0, 194, 231); font-weight: bold;\">Aviso de Privacidad</span></a></p> <p> Sus datos se encuentran protegidos a través de nuestro Aviso de Privacidad.<br>Consulte nuestro Aviso de Privacidad en: <a href=\"http://www.broxel.com/index.php/aviso-privacidad-sapi\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">www.broxel.com</a>.</p> <p> Este correo electrónico está destinado a residentes de México y fue enviado desde Broxel.<br/> Si ha recibido este correo de una dirección diferente, significa que fue reenviado.</p> <p>Las comunicaciones vía correo electrónico de Broxel se basan en el manejo de cookies<br/> y/o Web Beacons.</p> <p>Al recibir este correo y consultar el contenido del mismo, usted consiente que Broxel recabe y tenga acceso a sus datos personales.</p> <p>Para más información favor de consultar <strong><a href=\"http://www.broxel.com/index.php/terminos-condiciones\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">Términos y Condiciones.</a></strong></p></td> </tr> <tr> <td align=\"center\" valign=\"top\" width=\"auto\"> </td> </tr> </tbody></table> </td> </tr> <!-- end text content --> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> <!-- end layout-1 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-1 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT 67 --> <!-- START LAYOUT 7--> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <!-- start layout-7 container --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td valign=\"top\"> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <tr> <td valign=\"top\"> <!-- start logo footer and address --> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td valign=\"top\"> <!--start icon socail navigation --> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td align=\"left\" valign=\"top\"> <table class=\"container\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://www.facebook.com/BroxelSF\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-facebook.jpg\" alt=\"icon-facebook\" style=\"max-width:33px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> <td style=\"padding-left:5px; \" class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://twitter.com/BroxelSF\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-twitter.jpg\" alt=\"icon-twitter\" style=\"max-width:30px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> <td style=\"padding-left:5px; \" class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://plus.google.com/u/0/116529217974857845752/posts\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-googleplus.jpg\" alt=\"icon-googleplus\" style=\"max-width:30px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> </tr> </tbody></table> </td> </tr> </tbody></table> <!--end icon socail navigation --> </td> </tr> </tbody></table> <!-- end logo footer and address --> </td> </tr> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> <!-- end layout-7 container --> </td> </tr> <!-- END LAYOUT-7--> <!-- START FOOTER COPY RIGHT --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <!-- start layout-7 container --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <tbody><tr> <!-- start unsubscribe content --> <td valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 13px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:center;\" class=\"text-center\" align=\"center\"> Síguanos por facebook, twitter y google +<br /> Visitenos en <a href=\"http://broxel.com\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">www.broxel.com</a> para conocer más de nuestros productos.</td> </tr> </tbody></table> </td> <!-- end unsubscribe content --> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <!-- start COPY RIGHT content --> <td style=\"font-size: 11px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:center;\" valign=\"top\">© 2014 BROXEL | Servicios Financieros. Todos los derechos Reservados.</td> <!-- end COPY RIGHT content --> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END FOOTER COPY RIGHT --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> </table></body> </html>");
            SendMail(From, ConfigurationManager.AppSettings["emailsAvisosDispersiones"] + emailNotificar != String.Empty ? ", " + emailNotificar : "", Subject, sb.ToString(), "yMQ3E3ert6", "Broxel :  Renominacion de cuentas");
            //SendMail(From, "aldogarcia@broxel.com" + emailNotificar!=String.Empty?", "+emailNotificar:"", Subject, sb.ToString(), "yMQ3E3ert6",  "Broxel :  Transferencia entre cuentas");
        }

        internal static void CorrerDevolucion(string folio)
        {
            var dbHelper = new broxelco_rdgEntities();
            Tarjeta t;
            int POS = 0, ATM = 0;
            var webService = new BroxelService();
            var count = 0;
            bool comision;

            var devolucionSolicitud = dbHelper.devolucionesSolicitudes.Where(x => x.folio == folio && x.estado == "Validando").ToList();
            if (devolucionSolicitud.Count == 1 && devolucionSolicitud[0].estado == "Validando")
            {
                devolucionSolicitud[0].estado = "Ejecutando";
                dbHelper.SaveChanges();
                var devoluciones = dbHelper.devolucionesInternas.Where(x => x.idSolicitud == folio && (x.codigoRespuesta != "-1")).OrderBy(x => x.id).ToList();
                foreach (var devolucion in devoluciones)
                {
                    count++;
                    if (devoluciones.Count(x => x.cuenta == devolucion.cuenta) != 1)
                    {
                        devolucion.codigoRespuesta = "989";
                        continue;
                    }
                    try
                    {
                        t = GetTarjetaFromCuenta(devolucion.cuenta);
                        if (t != null)
                        {
                            try
                            {
                                if (devolucion.codigoRespuesta != "-1")
                                {
                                    var saldoAntes = Convert.ToDecimal(webService.GetSaldosPorCuenta(devolucion.cuenta, "", "CorrerDevolucionAntes").Saldos.DisponibleCompras);
                                    var res = webService.SetCargo(saldoAntes, t.NombreTarjeta, t.NumeroTarjeta, t.Cvc2,
                                         t.FechaExpira, "", "", 2, 1828);
                                    var saldoDespues = Convert.ToDecimal(webService.GetSaldosPorCuenta(devolucion.cuenta, "", "CorrerDevolucionDespues").Saldos.DisponibleCompras);

                                    devolucion.codigoAutorizacion = res.NumeroAutorizacion;
                                    devolucion.idmov = res.IdMovimiento.ToString(CultureInfo.InvariantCulture);
                                    devolucion.codigoRespuesta = res.CodigoRespuesta.ToString(CultureInfo.InvariantCulture);
                                    devolucion.antes = (float?)saldoAntes;
                                    devolucion.despues = (float?)saldoDespues;
                                    devolucion.monto = (float?)saldoAntes;
                                    POS++;
                                }
                            }
                            catch (Exception ex)
                            {
                                devolucion.codigoRespuesta = "990";
                            }
                        }
                        else
                            devolucion.codigoRespuesta = "993";
                        if (count >= 35)
                        {
                            dbHelper.SaveChanges();
                            count = 0;
                        }
                    }
                    catch (Exception e)
                    {
                        //TODO: Guardar en bitacora
                        SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com ", "Problema devolucion " + folio, "Fallo la devolucion folio " + folio + " en cuenta " + devolucion.cuenta + "  " + e, "yMQ3E3ert6");
                    }
                }
                dbHelper.SaveChanges();
                ActualizaYMandaMailDevolucion(folio);
            }
        }

        internal static void CorrerReversa(string folio)
        {
            var dbHelper = new broxelco_rdgEntities();
            Tarjeta t;
            var webService = new BroxelService();
            var count = 0;

            var dispersionSolicitud = dbHelper.dispersionesSolicitudes.Where(x => x.folio == folio && x.estado == "Reversando").ToList();
            if (dispersionSolicitud.Count == 1 && dispersionSolicitud[0].estado == "Reversando")
            {
                var dispersiones = dbHelper.dispersionesInternas.Where(x => x.idSolicitud == folio && ((x.codigoRespuestaPOS == "-1" && x.codRespAnulPOS != "-1") || (x.codigoRespuestaATM == "-1" && x.codRespAnulATM != "-1"))).OrderBy(x => x.id).ToList();
                foreach (var dispersion in dispersiones)
                {
                    count++;
                    try
                    {
                        t = GetTarjetaFromCuenta(dispersion.cuenta);
                        if (t != null)
                        {
                            //Reversamos POS
                            if (dispersion.codigoAutorizacionPOS == "-1")
                            {
                                dispersion.codRespAnulPOS = "-1";
                                dispersion.codAutAnulPOS = "-1";
                                dispersion.idAnulPOS = "0";
                            }
                            else if (dispersion.codigoAutorizacionPOS.Trim() != "" && dispersion.codRespAnulPOS != "-1")
                            {
                                var res = webService.ReversoLimiteForzado(t.NombreTarjeta, t.NumeroTarjeta, t.FechaExpira, t.Cvc2, Convert.ToInt32(dispersion.idmovPOS), 2);
                                dispersion.antesAnulPOS = Convert.ToDouble(res.SaldoAntes);
                                dispersion.codAutAnulPOS = res.NumeroAutorizacion;
                                dispersion.idAnulPOS = res.IdMovimiento.ToString(CultureInfo.InvariantCulture);
                                dispersion.codRespAnulPOS = res.CodigoRespuesta.ToString(CultureInfo.InvariantCulture);
                                dispersion.despuesAnulPOS = Convert.ToDouble(res.SaldoDespues);
                            }
                            //Reversamos ATM
                            if (dispersion.codigoAutorizacionATM == "-1")
                            {
                                dispersion.codRespAnulATM = "-1";
                                dispersion.codAutAnulATM = "-1";
                                dispersion.idAnulATM = "0";
                            }
                            else if (dispersion.codigoAutorizacionATM.Trim() != "" && dispersion.codRespAnulATM != "-1")
                            {
                                var res = webService.ReversoLimiteForzado(t.NombreTarjeta, t.NumeroTarjeta, t.FechaExpira, t.Cvc2, Convert.ToInt32(dispersion.idmovATM), 2);
                                dispersion.antesAnulATM = Convert.ToDouble(res.SaldoAntes);
                                dispersion.codAutAnulATM = res.NumeroAutorizacion;
                                dispersion.idAnulATM = res.IdMovimiento.ToString(CultureInfo.InvariantCulture);
                                dispersion.codRespAnulATM = res.CodigoRespuesta.ToString(CultureInfo.InvariantCulture);
                                dispersion.despuesAnulATM = Convert.ToDouble(res.SaldoDespues);
                            }
                        }
                        else
                            dispersion.codAutAnulPOS = dispersion.codRespAnulATM = "993";
                        if (count >= 35)
                        {
                            dbHelper.SaveChanges();
                            count = 0;
                        }
                    }
                    catch (Exception e)
                    {
                        //TODO: Guardar en bitacora
                        SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com ", "Problema Disp " + folio, "Fallo CorrerReverso dispersion folio " + folio + " en cuenta " + dispersion.cuenta + "  " + e, "yMQ3E3ert6");
                    }
                }
                dbHelper.SaveChanges();
                ActualizaReversoYMandaMail(folio);
            }
        }

        private static bool VerificaLineaCreditoActual(dispersionesSolicitudes dispersionSolicitud, out bool comision)
        {
            var dbHelper = new broxelco_rdgEntities();
            comision = true;
            Double montoADispersar = 0, porcentaje = 0, monto = 0;
            var cuentasCount = 0;
            if (dispersionSolicitud.tipo.ToUpper() == "INCREMENTO")
            {
                montoADispersar = dbHelper.dispersionesInternas.Where(x => x.idSolicitud == dispersionSolicitud.folio && x.codigoRespuestaPOS != "983").Sum(x => x.incrementoPOS ?? 0);
            }
            else
            {
                montoADispersar = dbHelper.dispersionesInternas.Where(x => x.idSolicitud == dispersionSolicitud.folio).Sum(x => x.incrementoPOS ?? 0);
                /*
                var fMax = dbHelper.disponibles.Max(x => x.Fecha);
                var disponibles = (from d in dbHelper.dispersionesInternas
                                   join d2 in dbHelper.disponibles on d.cuenta equals d2.NumCuenta
                                   where d.idSolicitud == dispersionSolicitud.folio && d2.Fecha == fMax
                                   select new { d, d2 }).ToList();
                montoADispersar += disponibles.Sum(d => Convert.ToDouble(d.d.incrementoPOS) - Convert.ToDouble(d.d2.SaldoDisponible));
                 */
            }
            var clienteComision = dbHelper.ClientesComisiones.FirstOrDefault(x => x.ClaveCliente == dispersionSolicitud.claveCliente && x.Producto == dispersionSolicitud.producto && x.CodigoComision == "CO0001");
            if (clienteComision == null)
            {
                comision = false;
                return comision;
            }

            if (!String.IsNullOrEmpty(clienteComision.Porcentaje))
                porcentaje = Convert.ToDouble(clienteComision.Porcentaje) / 100;
            if (!String.IsNullOrEmpty(clienteComision.Monto))
            {
                cuentasCount = (from r in dbHelper.dispersionesInternas where r.idSolicitud == dispersionSolicitud.folio select r.cuenta).Distinct().Count();
                monto = Convert.ToDouble(clienteComision.Monto) * cuentasCount;
            }
            var detalleCliente = dbHelper.DetalleClientesBroxel.FirstOrDefault(x => x.ClaveCliente == dispersionSolicitud.claveCliente && x.Producto == dispersionSolicitud.producto);
            if (detalleCliente.TopeMaximo == 1)
            {
                return detalleCliente.Dispersado + detalleCliente.CargosB1010 + montoADispersar <= detalleCliente.LineaCreditoOriginal;
            }

            if (!String.IsNullOrEmpty(clienteComision.Porcentaje))
                return detalleCliente.LineaCreditoActual >= montoADispersar + Math.Truncate((montoADispersar * (porcentaje) + (montoADispersar * (porcentaje) * .16)) * 100) / 100;
            else if (!String.IsNullOrEmpty(clienteComision.Monto))
            {
                return detalleCliente.LineaCreditoActual >= montoADispersar + (monto * 1.16);
            }
            else
            {
                return false;
            }
        }

        private static bool VerificaLineaCreditoActualPago(pagosSolicitudes pagosSolicitud)
        {
            Double porcentaje = 0, monto = 0;
            var dbHelper = new broxelco_rdgEntities();
            var clienteComision = dbHelper.ClientesComisiones.FirstOrDefault(x => x.ClaveCliente == pagosSolicitud.claveCliente && x.Producto == pagosSolicitud.producto && x.CodigoComision == "CO0020");
            var detalleCliente = dbHelper.DetalleClientesBroxel.FirstOrDefault(x => x.ClaveCliente == pagosSolicitud.claveCliente && x.Producto == pagosSolicitud.producto);

            Double montoADispersar = dbHelper.pagosInternos.Where(x => x.idSolicitud == pagosSolicitud.folio).Sum(x => x.pago ?? 0);

            if (clienteComision == null)
            {
                monto = 0;
                porcentaje = 0;
            }
            else
            {
                if (!String.IsNullOrEmpty(clienteComision.Porcentaje))
                    porcentaje = Convert.ToDouble(clienteComision.Porcentaje) / 100;
                if (!String.IsNullOrEmpty(clienteComision.Monto))
                {
                    var cuentasCount = (from r in dbHelper.dispersionesInternas where r.idSolicitud == pagosSolicitud.folio select r.cuenta).Distinct().Count();
                    monto = Convert.ToInt32(clienteComision.Monto) * cuentasCount;
                }
            }

            if (detalleCliente.TopeMaximo == 1)
                return detalleCliente.Dispersado + detalleCliente.CargosB1010 + montoADispersar <= detalleCliente.LineaCreditoOriginal;
            if (clienteComision == null)
                return detalleCliente.LineaCreditoActual >= montoADispersar;
            if (!String.IsNullOrEmpty(clienteComision.Porcentaje))
                return detalleCliente.LineaCreditoActual >= montoADispersar + Math.Truncate((montoADispersar * (porcentaje) + (montoADispersar * (porcentaje) * .16)) * 100) / 100;
            if (!String.IsNullOrEmpty(clienteComision.Monto))
                return detalleCliente.LineaCreditoActual >= montoADispersar + (monto * 1.16);
            return false;
        }

        private static void ActualizaYMandaMail(string folio, string tipo)
        {
            try
            {
                var EsMejoravit = new AsignaLineaBL().ValidaDispersionMejoravit(folio);
                var dbHelper = new broxelco_rdgEntities();
                decimal antesPOS = 0, despuesPOS = 0, antesATM = 0, despuesATM = 0, posSolicitado = 0, atmSolicitado = 0;
                int correctasPOS = 0, correctasATM = 0;
                var errores = new List<ErroresDispersion>();

                var dispersionesInternas =
                    dbHelper.dispersionesInternas.Where(x => x.idSolicitud == folio && (x.incrementoPOS > 0 || x.incrementoATM > 0)).ToList();
                if (dispersionesInternas.Count >= 1)
                {
                    antesPOS = (decimal)(dispersionesInternas.Sum(x => (x.antesPOS == null) ? 0 : x.antesPOS));
                    despuesPOS = (decimal)(dispersionesInternas.Sum(x => (x.despuesPOS == null) ? 0 : x.despuesPOS));
                    correctasPOS = dispersionesInternas.Count(x => x.codigoRespuestaPOS == "-1");
                    posSolicitado = (decimal)(dispersionesInternas.Sum(x => (x.incrementoPOS == null ? 0 : x.incrementoPOS)));
                    antesATM = (decimal)(dispersionesInternas.Sum(x => (x.antesATM == null) ? 0 : x.antesATM));
                    despuesATM = (decimal)(dispersionesInternas.Sum(x => (x.despuesATM == null) ? 0 : x.despuesATM));
                    correctasATM = dispersionesInternas.Count(x => x.codigoRespuestaATM == "-1");
                    atmSolicitado = (decimal)(dispersionesInternas.Sum(x => (x.incrementoATM == null ? 0 : x.incrementoATM)));
                }
                var solicitudDispersion = dbHelper.dispersionesSolicitudes.FirstOrDefault(x => x.folio == folio);
                if (solicitudDispersion != null)
                {
                    solicitudDispersion.montoPrincipal = (double)(despuesPOS - antesPOS);
                    if (correctasPOS == dispersionesInternas.Count(x => x.incrementoPOS != 0) &&
                        correctasATM == dispersionesInternas.Count(x => x.incrementoATM != 0))
                        solicitudDispersion.estado = "COMPLETA";
                    else
                    {
                        //solicitudDispersion.estado = EsMejoravit?"CON RECHAZOS":"CON ERRORES";
                        solicitudDispersion.estado = "CON ERRORES";
                        var codigosRespuesta = dbHelper.CodigosRespuesta.ToList();
                        var codigos = dispersionesInternas.Select(x => x.codigoRespuestaPOS).Distinct();
                        codigos = codigos.Where(x => x != "-1").ToList();
                        foreach (var codig in codigos)
                        {
                            if (string.IsNullOrEmpty(codig))
                                continue;
                            var codigo = Convert.ToInt32(codig);
                            var codigoDB = codigosRespuesta.FirstOrDefault(x => x.Id == codigo);
                            var e = new ErroresDispersion
                            {
                                CodigoRespuesta = codigo,
                                DescripcionCodigoResp = codigoDB != null ? codigoDB.Descripcion : "",
                                CausaComunCodigoResp = codigoDB != null ? codigoDB.CausaComun : ""
                            };
                            string codig1 = codig;
                            var cuentasConError = dispersionesInternas.Where(x => x.codigoRespuestaPOS == codig1).ToList();
                            foreach (var cuentaConError in cuentasConError)
                            {
                                e.CuentasConError.Add(cuentaConError.cuenta);
                            }
                            errores.Add(e);
                        }
                    }
                    dbHelper.SaveChanges();
                    GeneraComisionYCargoDispersion(solicitudDispersion, despuesPOS - antesPOS);
                    EnviarMailDispersion(solicitudDispersion.email, folio, tipo, despuesPOS - antesPOS,
                        despuesATM - antesATM, solicitudDispersion.estado, dispersionesInternas.Count,
                        solicitudDispersion.claveCliente, errores, posSolicitado, atmSolicitado);
                    EnviarSMSDispersion(solicitudDispersion);
                }
            }
            catch (Exception e)
            {
                SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com ", "Problema Disp " + folio, "Fallo la dispersion en ActualizaYMandaMail con folio" + folio + "  " + e, "yMQ3E3ert6");
            }
        }

        private static void ActualizaYMandaMailPago(pagosSolicitudes pagoSolicitud, string tipo, string folio)
        {
            try
            {
                var dbHelper = new broxelco_rdgEntities();
                decimal antesPOS = 0, despuesPOS = 0, posSolicitado = 0;
                int correctasPOS = 0, correctasATM = 0;
                var errores = new List<ErroresDispersion>();

                var pagosInternos = dbHelper.pagosInternos.Where(x => x.idSolicitud == folio && x.pago > 0).ToList();
                if (pagosInternos.Count >= 1)
                {
                    antesPOS = (decimal)(pagosInternos.Sum(x => x.antes));
                    despuesPOS = (decimal)pagosInternos.Sum(x => x.despues);
                    correctasPOS = pagosInternos.Count(x => x.codigoRespuesta == "-1");
                    posSolicitado = (decimal)(pagosInternos.Sum(x => x.pago));
                }
                var solicitudPago = dbHelper.pagosSolicitudes.FirstOrDefault(x => x.folio == folio);
                if (solicitudPago != null)
                {
                    solicitudPago.montoPrincipal = (double)(despuesPOS - antesPOS);
                    if (correctasPOS == pagosInternos.Count(x => x.pago != 0))
                        solicitudPago.estado = "COMPLETA";
                    else
                    {
                        solicitudPago.estado = "CON ERRORES";
                        var codigosRespuesta = dbHelper.CodigosRespuesta.ToList();
                        var codigos = pagosInternos.Select(x => x.codigoRespuesta).Distinct();
                        codigos = codigos.Where(x => x != "-1").ToList();
                        foreach (var codig in codigos)
                        {
                            var codigo = Convert.ToInt32(codig);
                            var e = new ErroresDispersion
                            {
                                CodigoRespuesta = codigo,
                                DescripcionCodigoResp = codigosRespuesta.FirstOrDefault(x => x.Id == codigo).Descripcion,
                                CausaComunCodigoResp = codigosRespuesta.FirstOrDefault(x => x.Id == codigo).CausaComun
                            };
                            var cuentasConError = pagosInternos.Where(x => x.codigoRespuesta == codig).ToList();
                            foreach (var cuentaConError in cuentasConError)
                            {
                                e.CuentasConError.Add(cuentaConError.cuenta);
                            }
                            errores.Add(e);
                        }
                    }
                    dbHelper.SaveChanges();
                    GeneraComisionYCargoPago(pagoSolicitud, despuesPOS - antesPOS);
                    EnviarMailPago(pagoSolicitud.email, folio, tipo, despuesPOS - antesPOS, solicitudPago.estado, pagosInternos.Count,
                        solicitudPago.claveCliente, errores, posSolicitado);
                }
            }
            catch (Exception e)
            {
                SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com ", "Problema Disp " + folio, "Fallo el pago en ActualizaYMandaMailPago con folio" + folio + "  " + e, "yMQ3E3ert6");
            }
        }

        private static void ActualizaYMandaMailDevolucion(string folio)
        {
            try
            {
                broxelco_rdgEntities _broxelcoRdgEntities = new broxelco_rdgEntities();
                var dbHelper = new broxelco_rdgEntities();
                decimal antesPOS = 0, despuesPOS = 0, posSolicitado = 0;
                int correctasPOS = 0, correctasATM = 0;
                var errores = new List<ErroresDispersion>();

                var devolucionesInternas =
                    dbHelper.devolucionesInternas.Where(x => x.idSolicitud == folio).ToList();
                if (devolucionesInternas.Count >= 1)
                {
                    antesPOS = (decimal)(devolucionesInternas.Sum(x => (x.antes == null) ? 0 : x.antes));
                    despuesPOS = (decimal)(devolucionesInternas.Sum(x => (x.despues == null) ? 0 : x.despues));
                    correctasPOS = devolucionesInternas.Count(x => x.codigoRespuesta == "-1");
                    posSolicitado = (decimal)(devolucionesInternas.Sum(x => x.monto));
                }
                var solicitudDevolucion = dbHelper.devolucionesSolicitudes.FirstOrDefault(x => x.folio == folio);
                if (solicitudDevolucion != null)
                {
                    solicitudDevolucion.montoPrincipal = (double)(despuesPOS - antesPOS);
                    if (correctasPOS == devolucionesInternas.Count(x => x.monto != 0))
                        solicitudDevolucion.estado = "COMPLETA";
                    else
                    {
                        solicitudDevolucion.estado = "CON ERRORES";
                        var codigosRespuesta = dbHelper.CodigosRespuesta.ToList();
                        var codigos = devolucionesInternas.Select(x => x.codigoRespuesta).Distinct();
                        codigos = codigos.Where(x => x != "-1").ToList();
                        foreach (var codig in codigos)
                        {
                            var codigo = Convert.ToInt32(codig);
                            var e = new ErroresDispersion
                            {
                                CodigoRespuesta = codigo,
                                DescripcionCodigoResp = codigosRespuesta.FirstOrDefault(x => x.Id == codigo).Descripcion,
                                CausaComunCodigoResp = codigosRespuesta.FirstOrDefault(x => x.Id == codigo).CausaComun
                            };
                            var cuentasConError = devolucionesInternas.Where(x => x.codigoRespuesta == codig).ToList();
                            foreach (var cuentaConError in cuentasConError)
                            {
                                e.CuentasConError.Add(cuentaConError.cuenta);
                            }
                            errores.Add(e);
                        }
                    }
                    dbHelper.SaveChanges();
                    DetalleClientesBroxel detalleClientesBroxel = _broxelcoRdgEntities.DetalleClientesBroxel.FirstOrDefault(x => x.ClaveCliente == solicitudDevolucion.claveCliente && x.Producto == solicitudDevolucion.producto);
                    ActualizaLineaCreditoCliente(detalleClientesBroxel.CLABE, 3, (despuesPOS - antesPOS) * (-1));
                    EnviarMailDevolucion(solicitudDevolucion.email, folio, "DEVOLUCION", (despuesPOS - antesPOS) * (-1),
                       solicitudDevolucion.estado, devolucionesInternas.Count, solicitudDevolucion.claveCliente, errores);
                }
            }
            catch (Exception e)
            {
                SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com ", "Problema Disp " + folio, "Fallo la dispersion en ActualizaYMandaMail con folio" + folio + "  " + e, "yMQ3E3ert6");
            }
        }

        private static void ActualizaReversoYMandaMail(string folio)
        {
            try
            {
                var dbHelper = new broxelco_rdgEntities();
                decimal antesPOS = 0, despuesPOS = 0, antesATM = 0, despuesATM = 0, posSolicitado = 0, atmSolicitado = 0;
                int correctasPOS = 0, correctasATM = 0;
                var errores = new List<ErroresDispersion>();

                var dispersionesInternas =
                    dbHelper.dispersionesInternas.Where(x => x.idSolicitud == folio && x.incrementoPOS > 0).ToList();
                if (dispersionesInternas.Count >= 1)
                {
                    antesPOS = (decimal)(dispersionesInternas.Sum(x => x.antesAnulPOS));
                    despuesPOS = (decimal)dispersionesInternas.Sum(x => x.despuesAnulPOS);
                    correctasPOS = dispersionesInternas.Count(x => x.codRespAnulPOS == "-1");
                    posSolicitado = (decimal)(dispersionesInternas.Sum(x => x.incrementoPOS));
                    antesATM = (decimal)(dispersionesInternas.Sum(x => x.antesAnulATM));
                    despuesATM = (decimal)dispersionesInternas.Sum(x => x.despuesAnulATM);
                    correctasATM = dispersionesInternas.Count(x => x.codigoRespuestaATM == "-1");
                    atmSolicitado = (decimal)(dispersionesInternas.Sum(x => x.incrementoATM));
                }
                var solicitudDispersion = dbHelper.dispersionesSolicitudes.FirstOrDefault(x => x.folio == folio);
                if (solicitudDispersion != null)
                {
                    solicitudDispersion.montoPrincipalAnul = (double)(despuesPOS - antesPOS);
                    if (correctasPOS == dispersionesInternas.Count(x => x.codRespAnulPOS == "-1" && x.codigoRespuestaPOS == "-1") && correctasATM == dispersionesInternas.Count(x => x.codRespAnulATM == "-1" && x.codigoRespuestaATM == "-1"))
                        solicitudDispersion.estado = "Reversado";
                    else
                    {
                        solicitudDispersion.estado = "Reverso Incompleto";
                        var codigosRespuesta = dbHelper.CodigosRespuesta.ToList();
                        var codigos = dispersionesInternas.Select(x => x.codRespAnulPOS).Distinct();
                        codigos = codigos.Where(x => x != "-1").ToList();
                        foreach (var codig in codigos)
                        {
                            var codigo = Convert.ToInt32(codig);
                            var e = new ErroresDispersion
                            {
                                CodigoRespuesta = codigo,
                                DescripcionCodigoResp = codigosRespuesta.FirstOrDefault(x => x.Id == codigo).Descripcion,
                                CausaComunCodigoResp = codigosRespuesta.FirstOrDefault(x => x.Id == codigo).CausaComun
                            };
                            var cuentasConError = dispersionesInternas.Where(x => x.codRespAnulPOS == codig).ToList();
                            foreach (var cuentaConError in cuentasConError)
                            {
                                e.CuentasConError.Add(cuentaConError.cuenta);
                            }
                            errores.Add(e);
                        }
                    }
                    dbHelper.SaveChanges();
                    GeneraComisionYReversoDispersion(solicitudDispersion, despuesPOS - antesPOS);
                    EnviarMailReversoDispersion(solicitudDispersion.email, folio, despuesPOS - antesPOS,
                        despuesATM - antesATM, solicitudDispersion.estado, dispersionesInternas.Count,
                        solicitudDispersion.claveCliente, errores, posSolicitado, atmSolicitado);
                }
            }
            catch (Exception e)
            {
                SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com ", "Problema Disp " + folio, "Fallo la dispersion en ActualizaYMandaMail con folio" + folio + "  " + e, "yMQ3E3ert6");
            }
        }

        private static void EnviarSMSDispersion(dispersionesSolicitudes solicitudDispersion)
        {
            try
            {
                broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
                var dispersionWE = broxelcoRdgEntities.dispersionesWE.FirstOrDefault(x => x.idSolicitud == solicitudDispersion.folio);
                if (dispersionWE != null)
                {
                    if (solicitudDispersion.estado.ToUpper() == "COMPLETA")
                    {
                        var wsSMSClient = new ServicioSMSClient();
                        wsSMSClient.EnviarSMS(
                            new SMS
                            {
                                Mensaje = "Asignacion de linea finalizada",
                                Telefono = dispersionWE.CelularNotifica
                            }, new Credenciales
                            {
                                Username = "BROXEL",
                                Password = "9W8230AP",
                                Host = "http://api.quiubas.com:9501"
                            });
                    }
                }
            }
            catch(Exception e)
            {
                SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com", "Problema al enviar el SMS de dispersión.", "Fallo en el envio del SMS de dispersión, Error: " + e, "yMQ3E3ert6");
            }
        }

        private static void GeneraComisionYCargoDispersion(dispersionesSolicitudes solicitudDispersion, decimal montoDispersado)
        {
            broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
            String DescComision = String.Empty;
            var DetalleClienteComision = broxelcoRdgEntities.DetalleClientesBroxel.FirstOrDefault(x => x.ClaveCliente == solicitudDispersion.claveCliente && x.Producto == solicitudDispersion.producto);
            var DescClienteComision = broxelcoRdgEntities.ClientesComisiones.FirstOrDefault(x => x.CodigoComision == "CO0001" && x.ClaveCliente == solicitudDispersion.claveCliente && x.Producto == solicitudDispersion.producto);
            var ComisionB1010 = broxelcoRdgEntities.ComisionesB1010.FirstOrDefault(x => x.CodigoComision == "CO0001");
            DescComision = DescClienteComision.Concepto == null ? ComisionB1010.Descripcion : DescClienteComision.Descripcion;
            Decimal Monto = 0;
            Decimal IVA = 0;
            var clienteComision = DescClienteComision;
            if (clienteComision != null)
            {
                if (!String.IsNullOrEmpty(clienteComision.Porcentaje))
                {
                    Decimal porcentaje = Convert.ToDecimal(clienteComision.Porcentaje);
                    try
                    {
                        // Calcula porcentaje, monto dispersado e IVA
                        porcentaje = porcentaje / 100;
                        Monto = Math.Truncate(montoDispersado * porcentaje * 100) / 100;
                        IVA = Math.Truncate(Monto * .16M * 100) / 100;
                        // Agrega comision CargosB1010 y Dispersado
                        var lineaCredito =
                            broxelcoRdgEntities.DetalleClientesBroxel.FirstOrDefault(x => x.ClaveCliente == solicitudDispersion.claveCliente && x.Producto == solicitudDispersion.producto);
                        lineaCredito.CargosB1010 += (double)(Monto + IVA);
                        try
                        {
                            broxelcoRdgEntities.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com ", "Fallo en GeneraComisionYCargoDispersion ", "Problema actualizando DB " + solicitudDispersion.id + "  dispersado,cargosb1010 en DetalleClientesBroxel monto:" + montoDispersado + " <br><br> Falta CargarB1010 " + Monto + IVA + " <br><br> " + e, "yMQ3E3ert6");
                        }
                        MovimientosClientes mc = new MovimientosClientes
                        {
                            Cantidad = 1,
                            Cliente = solicitudDispersion.claveCliente,
                            Monto = (double)Monto,
                            CodigoComision = "CO0001",
                            Descripcion = DescComision,
                            FechaHora = DateTime.Now,
                            IdComision = Convert.ToInt32(ComisionB1010.Id),
                            IdDetalleCliente = Convert.ToInt32(lineaCredito.Id),
                            Producto = solicitudDispersion.producto,
                            IVA = (double)IVA,
                            SubTotal = (double)Monto,
                            Total = (double)(Monto + IVA),
                            UsuarioCreacion = "WebService",
                            FolioDispersion = solicitudDispersion.folio
                        };
                        broxelcoRdgEntities.MovimientosClientes.Add(mc);
                        broxelcoRdgEntities.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        //TODO: Guardar en bitacora
                        SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com ", "Fallo en GeneraComisionYCargoDispersion ", "Problema actualizando DB " + solicitudDispersion.id + "  dispersado,cargosb1010 en DetalleClientesBroxel monto:" + montoDispersado + " <br><br> Falta generar movimiento <br><br>" + e, "yMQ3E3ert6");
                    }
                }
                else if (!String.IsNullOrEmpty(clienteComision.Monto))
                {
                    try
                    {
                        // Calcula comision por cuentas
                        var cuentasCount = (from r in broxelcoRdgEntities.dispersionesInternas where r.idSolicitud == solicitudDispersion.folio && r.codigoRespuestaPOS == "-1" select r.cuenta).Count();
                        var monto = Convert.ToDecimal(clienteComision.Monto);
                        monto *= cuentasCount;
                        Monto = Math.Truncate(monto);
                        IVA = Math.Round(monto * .16M, 2);
                        // Agrega comision CargosB1010 y Dispersado
                        var lineaCredito = broxelcoRdgEntities.DetalleClientesBroxel.FirstOrDefault(x => x.ClaveCliente == solicitudDispersion.claveCliente && x.Producto == solicitudDispersion.producto);
                        lineaCredito.CargosB1010 += (double)(Monto + IVA);
                        try
                        {
                            broxelcoRdgEntities.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com ", "Fallo actualizando cargosb1010 ", "Problema actualizando DB " + solicitudDispersion.id + "  dispersado,cargosb1010 en DetalleClientesBroxel monto:" + montoDispersado + " <br><br> Falta CargarB1010 " + Monto + IVA + " <br><br> " + e, "yMQ3E3ert6");
                        }
                        // Agrega Movimiento
                        MovimientosClientes mc = new MovimientosClientes
                        {
                            Cantidad = cuentasCount,
                            Cliente = solicitudDispersion.claveCliente,
                            Monto = Convert.ToDouble(clienteComision.Monto),
                            CodigoComision = "CO0001",
                            Descripcion = DescComision,
                            FechaHora = DateTime.Now,
                            IdComision = Convert.ToInt32(ComisionB1010.Id),
                            IdDetalleCliente = Convert.ToInt32(lineaCredito.Id),
                            Producto = solicitudDispersion.producto,
                            IVA = (double)IVA,
                            SubTotal = (double)Monto,
                            Total = (double)(Monto + IVA),
                            UsuarioCreacion = "WebService",
                            FolioDispersion = solicitudDispersion.folio
                        };
                        broxelcoRdgEntities.MovimientosClientes.Add(mc);
                        broxelcoRdgEntities.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        //TODO: Guardar en bitacora
                        SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com ", "Fallo en GeneraComisionYCargoDispersion ", "Problema actualizando DB " + solicitudDispersion.id + "  dispersado,cargosb1010 en DetalleClientesBroxel monto:" + montoDispersado + " <br><br> Falta generar movimiento <br><br>" + e, "yMQ3E3ert6");
                    }

                }
            }
            ActualizaLineaCreditoCliente(DetalleClienteComision.CLABE, 2, montoDispersado);
        }

        private static void GeneraComisionYCargoPago(pagosSolicitudes solicitudPago, decimal montoDispersado)
        {
            broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
            String DescComision = String.Empty;

            var DetalleClientesBroxel = broxelcoRdgEntities.DetalleClientesBroxel.FirstOrDefault(x => x.ClaveCliente == solicitudPago.claveCliente && x.Producto == solicitudPago.producto);
            //var DescClienteComision = broxelcoRdgEntities.ClientesComisiones.FirstOrDefault(x => x.CodigoComision == "CO0020");
            //var ComisionB1010 = broxelcoRdgEntities.ComisionesB1010.FirstOrDefault(x => x.CodigoComision == "CO0020");
            //if (DescClienteComision.Concepto == null)
            //    DescComision = ComisionB1010.Descripcion;
            //else
            //    DescComision = DescClienteComision.Descripcion;

            Decimal Monto = 0;
            Decimal IVA = 0;
            var clienteComision = broxelcoRdgEntities.ClientesComisiones.FirstOrDefault(x => x.ClaveCliente == solicitudPago.claveCliente && x.Producto == solicitudPago.producto && x.CodigoComision == "CO0020");
            if (clienteComision == null)
            {
                clienteComision = new ClientesComisiones();
                clienteComision.Porcentaje = "0";
                clienteComision.Monto = "0";
            }
            if (!String.IsNullOrEmpty(clienteComision.Porcentaje))
            {
                Decimal porcentaje = Convert.ToDecimal(clienteComision.Porcentaje);
                try
                {
                    // Calcula porcentaje, monto dispersado e IVA
                    porcentaje = porcentaje / 100;
                    Monto = Math.Truncate(montoDispersado * porcentaje * 100) / 100;
                    IVA = Math.Truncate(Monto * .16M * 100) / 100;
                    // Agrega comision CargosB1010 y Dispersado
                    var lineaCredito =
                        broxelcoRdgEntities.DetalleClientesBroxel.FirstOrDefault(x => x.ClaveCliente == solicitudPago.claveCliente && x.Producto == solicitudPago.producto);
                    lineaCredito.CargosB1010 += (double)(Monto + IVA);
                    try
                    {
                        broxelcoRdgEntities.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com ", "Fallo en GeneraComisionYCargoPAGO ", "Problema actualizando DB " + solicitudPago.id + "  pagado,cargosb1010 en DetalleClientesBroxel monto:" + montoDispersado + " <br><br> Falta CargarB1010 " + Monto + IVA + " <br><br> " + e, "yMQ3E3ert6");
                    }
                    // Agrega Movimiento
                    MovimientosClientes mc = new MovimientosClientes
                    {
                        Cantidad = 1,
                        Cliente = solicitudPago.claveCliente,
                        Monto = (double)Monto,
                        CodigoComision = "CO0020",
                        Descripcion = DescComision,
                        FechaHora = DateTime.Now,
                        IdComision = 20,
                        IdDetalleCliente = Convert.ToInt32(lineaCredito.Id),
                        Producto = solicitudPago.producto,
                        IVA = (double)IVA,
                        SubTotal = (double)Monto,
                        Total = (double)(Monto + IVA),
                        UsuarioCreacion = "WebService",
                        FolioDispersion = solicitudPago.folio
                    };
                    broxelcoRdgEntities.MovimientosClientes.Add(mc);
                    broxelcoRdgEntities.SaveChanges();
                }
                catch (Exception e)
                {
                    //TODO: Guardar en bitacora
                    SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com ", "Fallo en GeneraComisionYCargoPAGO ", "Problema actualizando DB en pago " + solicitudPago.id + "  ,cargosb1010 en DetalleClientesBroxel monto:" + montoDispersado + " <br><br> Falta generar movimiento por pago <br><br>" + e, "yMQ3E3ert6");
                }
            }
            else if (!String.IsNullOrEmpty(clienteComision.Monto))
            {
                try
                {
                    // Calcula comision por cuentas
                    var cuentasCount = (from r in broxelcoRdgEntities.dispersionesInternas where r.idSolicitud == solicitudPago.folio && r.codigoRespuestaPOS == "-1" select r.cuenta).Count();
                    var monto = Convert.ToDecimal(clienteComision.Monto);
                    monto *= cuentasCount;
                    Monto = Math.Truncate(monto);
                    IVA = Math.Round(monto * .16M, 2);
                    // Agrega comision CargosB1010 y Dispersado
                    var lineaCredito = broxelcoRdgEntities.DetalleClientesBroxel.FirstOrDefault(x => x.ClaveCliente == solicitudPago.claveCliente && x.Producto == solicitudPago.producto);
                    lineaCredito.CargosB1010 += (double)(Monto + IVA);
                    try
                    {
                        broxelcoRdgEntities.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com ", "Fallo actualizando cargosb1010 por PAGO ", "Problema actualizando DB en PAGO " + solicitudPago.id + "  ,cargosb1010 en DetalleClientesBroxel monto:" + montoDispersado + " <br><br> Falta CargarB1010 " + Monto + IVA + " <br><br> " + e, "yMQ3E3ert6");
                    }
                    // Agrega Movimiento
                    MovimientosClientes mc = new MovimientosClientes
                    {
                        Cantidad = cuentasCount,
                        Cliente = solicitudPago.claveCliente,
                        Monto = Convert.ToDouble(clienteComision.Monto),
                        CodigoComision = "CO0020",
                        Descripcion = DescComision,
                        FechaHora = DateTime.Now,
                        IdComision = 20,
                        IdDetalleCliente = Convert.ToInt32(lineaCredito.Id),
                        Producto = solicitudPago.producto,
                        IVA = (double)IVA,
                        SubTotal = (double)Monto,
                        Total = (double)(Monto + IVA),
                        UsuarioCreacion = "WebService",
                        FolioDispersion = solicitudPago.folio
                    };
                    broxelcoRdgEntities.MovimientosClientes.Add(mc);
                    broxelcoRdgEntities.SaveChanges();
                }
                catch (Exception e)
                {
                    //TODO: Guardar en bitacora
                    SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com ", "Fallo en GeneraComisionYCargoDispersion ", "Problema actualizando DB " + solicitudPago.id + "  dispersado,cargosb1010 en DetalleClientesBroxel monto:" + montoDispersado + " <br><br> Falta generar movimiento <br><br>" + e, "yMQ3E3ert6");
                }
            }
            ActualizaLineaCreditoCliente(DetalleClientesBroxel.CLABE, 4, montoDispersado);
        }

        private static void GeneraComisionYReversoDispersion(dispersionesSolicitudes solicitudDispersion, decimal montoDispersado)
        {
            broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
            String DescComision = String.Empty;
            var DetalleClienteComision = broxelcoRdgEntities.DetalleClientesBroxel.FirstOrDefault(x => x.ClaveCliente == solicitudDispersion.claveCliente && x.Producto == solicitudDispersion.producto);
            var DescClienteComision = broxelcoRdgEntities.ClientesComisiones.FirstOrDefault(x => x.CodigoComision == "CO0001");
            var ComisionB1010 = broxelcoRdgEntities.ComisionesB1010.FirstOrDefault(x => x.CodigoComision == "CO0001");
            if (DescClienteComision.Concepto == null)
                DescComision = ComisionB1010.Descripcion;
            else
                DescComision = DescClienteComision.Descripcion;
            Decimal Monto = 0;
            Decimal IVA = 0;
            var clienteComision = broxelcoRdgEntities.ClientesComisiones.FirstOrDefault(x => x.ClaveCliente == solicitudDispersion.claveCliente && x.Producto == solicitudDispersion.producto && x.CodigoComision == "CO0001");
            if (clienteComision != null)
            {
                if (!String.IsNullOrEmpty(clienteComision.Porcentaje))
                {
                    Decimal porcentaje = Convert.ToDecimal(clienteComision.Porcentaje);
                    try
                    {
                        // Calcula porcentaje, monto dispersado e IVA
                        porcentaje = porcentaje / 100;
                        Monto = Math.Truncate(montoDispersado * porcentaje * 100) / 100;
                        IVA = Math.Truncate(Monto * .16M * 100) / 100;
                        // Agrega comision CargosB1010 y Dispersado
                        var lineaCredito = broxelcoRdgEntities.DetalleClientesBroxel.FirstOrDefault(x => x.ClaveCliente == solicitudDispersion.claveCliente && x.Producto == solicitudDispersion.producto);
                        lineaCredito.CargosB1010 += (double)(Monto + IVA);
                        try
                        {
                            broxelcoRdgEntities.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com ", "Fallo en GeneraComisionYReversoCargo ", "Problema actualizando DB " + solicitudDispersion.id + "  dispersado,cargosb1010 en DetalleClientesBroxel monto:" + montoDispersado + " <br><br> Falta CargarB1010 " + Monto + IVA + " <br><br> " + e, "yMQ3E3ert6");
                        }
                        // Agrega Movimiento
                        MovimientosClientes mc = new MovimientosClientes
                        {
                            Cantidad = 1,
                            Cliente = solicitudDispersion.claveCliente,
                            Monto = (double)Monto,
                            CodigoComision = "CO0001",
                            Descripcion = "Reverso " + DescComision,
                            FechaHora = DateTime.Now,
                            IdComision = Convert.ToInt32(ComisionB1010.Id),
                            IdDetalleCliente = Convert.ToInt32(lineaCredito.Id),
                            Producto = solicitudDispersion.producto,
                            IVA = (double)IVA,
                            SubTotal = (double)Monto,
                            Total = (double)(Monto + IVA),
                            UsuarioCreacion = "WebService",
                            FolioDispersion = solicitudDispersion.folio
                        };
                        broxelcoRdgEntities.MovimientosClientes.Add(mc);
                        broxelcoRdgEntities.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        //TODO: Guardar en bitacora
                        SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com ", "Fallo en GeneraComisionYCargoDispersion ", "Problema actualizando DB " + solicitudDispersion.id + "  dispersado,cargosb1010 en DetalleClientesBroxel monto:" + montoDispersado + " <br><br> Falta generar movimiento <br><br>" + e, "yMQ3E3ert6");
                    }
                }
                else if (!String.IsNullOrEmpty(clienteComision.Monto))
                {
                    try
                    {
                        // Calcula comision por cuentas
                        var cuentasCount = (from r in broxelcoRdgEntities.dispersionesInternas where r.idSolicitud == solicitudDispersion.folio && r.codigoRespuestaPOS == "-1" select r.cuenta).Count();
                        var monto = Convert.ToDecimal(clienteComision.Monto);
                        monto *= cuentasCount;
                        Monto = Math.Truncate(monto);
                        IVA = Math.Round(monto * .16M, 2);
                        // Agrega comision CargosB1010 y Dispersado
                        var lineaCredito = broxelcoRdgEntities.DetalleClientesBroxel.FirstOrDefault(x => x.ClaveCliente == solicitudDispersion.claveCliente && x.Producto == solicitudDispersion.producto);
                        lineaCredito.CargosB1010 += (double)(Monto + IVA);
                        try
                        {
                            broxelcoRdgEntities.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com ", "Fallo actualizando cargosb1010 reverso ", "Problema actualizando DB " + solicitudDispersion.id + "  dispersado,cargosb1010 en DetalleClientesBroxel monto:" + montoDispersado + " <br><br> Falta CargarB1010 " + Monto + IVA + " <br><br> " + e, "yMQ3E3ert6");
                        }
                        // Agrega Movimiento
                        MovimientosClientes mc = new MovimientosClientes
                        {
                            Cantidad = cuentasCount,
                            Cliente = solicitudDispersion.claveCliente,
                            Monto = Convert.ToDouble(clienteComision.Monto),
                            CodigoComision = "CO0001",
                            Descripcion = "Reverso " + DescComision,
                            FechaHora = DateTime.Now,
                            IdComision = Convert.ToInt32(ComisionB1010.Id),
                            IdDetalleCliente = Convert.ToInt32(lineaCredito.Id),
                            Producto = solicitudDispersion.producto,
                            IVA = (double)IVA,
                            SubTotal = (double)Monto,
                            Total = (double)(Monto + IVA),
                            UsuarioCreacion = "WebService",
                            FolioDispersion = solicitudDispersion.folio
                        };
                        broxelcoRdgEntities.MovimientosClientes.Add(mc);
                        broxelcoRdgEntities.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        //TODO: Guardar en bitacora
                        SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com ", "Fallo en GeneraComisionYReversoDispersion ", "Problema actualizando DB " + solicitudDispersion.id + "  dispersado,cargosb1010 en DetalleClientesBroxel monto:" + montoDispersado + " <br><br> Falta generar movimiento <br><br>" + e, "yMQ3E3ert6");
                    }

                }
            }
            ActualizaLineaCreditoCliente(DetalleClienteComision.CLABE, 2, montoDispersado);
        }

        internal static int ActualizaLineaCreditoCliente(string CLABE, int tipo, decimal cantidad)
        {
            try
            {
                lock (tablaDispersion)
                {
                    var dbHelper = new broxelco_rdgEntities();
                    var cliente = dbHelper.DetalleClientesBroxel.FirstOrDefault(x => x.CLABE == CLABE);
                    if (cliente != null)
                    {
                        String callSp = "call spAfectaB1010('[{Campo}]','" + cantidad + "','" + cliente.ClaveCliente + "','" + cliente.Producto + "','WebService','[{Motivo}]')";
                        switch (tipo)
                        {
                            case 1: // deposito
                                callSp = callSp.Replace("[{Campo}]", "Abonos").Replace("[{Motivo}]", "RecepcionTransferencia");
                                dbHelper.Database.ExecuteSqlCommand(callSp);
                                break;
                            case 2: // dispersion
                                callSp = callSp.Replace("[{Campo}]", "Dispersado").Replace("[{Motivo}]", "Dispersion");
                                dbHelper.Database.ExecuteSqlCommand(callSp);
                                break;
                            case 3:
                                callSp = callSp.Replace("[{Campo}]", "DevolucionesDeCuentas").Replace("[{Motivo}]", "Devolucion");
                                dbHelper.Database.ExecuteSqlCommand(callSp);
                                break;
                            case 4:
                                callSp = callSp.Replace("[{Campo}]", "PagosCredito").Replace("[{Motivo}]", "PagoPorWs");
                                dbHelper.Database.ExecuteSqlCommand(callSp);
                                break;
                            //MLS Ajustes por transsferencia SPEI a cuenta especifica
                            case 5:
                                callSp = callSp.Replace("[{Campo}]", "Abonos").Replace("[{Motivo}]", "RecepcionTransferenciawsSPEI");
                                dbHelper.Database.ExecuteSqlCommand(callSp);
                                break;
                        }
                        return 1;
                    }
                }
            }
            catch (Exception e)
            {
                SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com ", "Fallo en ActualizaLineaCreditoCliente ", "Problema actualizando DB CLABE : " + CLABE + "  Tipo : " + tipo + " cantidad : " + cantidad + "<br><br> " + e, "yMQ3E3ert6");
                return 0;
            }
            return 0;
        }

        #endregion

        #region Mailing

        public static void EnviarMailDispersion(string emailNotificar, string folio, string tipo, decimal totalPOS, decimal totalATM, string estado, int cuantas, string cliente, List<ErroresDispersion> erroresDispersion, decimal posSolicitado, decimal atmSolicitado)
        {
            var dbHelper = new broxelco_rdgEntities();
            int cuentasConError = 0;
            var From = "dispersiones@broxel.com";
            var nombreCliente = dbHelper.clientesBroxel.Where(x => x.claveCliente == cliente).ToList().First().razonSocial;
            var Subject = String.Format("Solicitud de asignación de línea terminada - cliente : {0}", cliente);
            var sb = new StringBuilder();
            var htmlIni =
                "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"> <html xmlns=\"http://www.w3.org/1999/xhtml\"> <head> <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" /> <meta name=\"viewport\" content=\"initial-scale=1.0\"/> <meta name=\"format-detection\" content=\"telephone=no\"/> <title>Broxel Proceso de Asignación</title> <style type=\"text/css\"> /* Resets: see reset.css for details */ .ReadMsgBody { width: 100%; background-color: #ffffff;} .ExternalClass {width: 100%; background-color: #ffffff;} .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {line-height:100%;} html{width: 100%; } body {-webkit-text-size-adjust:none; -ms-text-size-adjust:none; } body {margin:0; padding:0;} table {border-spacing:0;} img{display:block !important;} table td {border-collapse:collapse;} .yshortcuts a {border-bottom: none !important;} /* main color = #f05e5e other color = #b91925 background color = #ececec */ img{height:auto !important;} @media only screen and (max-width: 640px){ body{ width:auto!important; } table[class=\"container\"]{ width: 100%!important; padding-left: 20px!important; padding-right: 20px!important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; } img[class=\"small-image-100-percent\"]{ width:100% !important; height:auto !important; } table[class=\"full-width\"]{ width:100% !important; } table[class=\"full-width-text\"]{ width:100% !important; background-color:#b91925; padding-left:20px !important; padding-right:20px !important; } table[class=\"full-width-text2\"]{ width:100% !important; background-color:#f3f3f3; padding-left:20px !important; padding-right:20px !important; } table[class=\"col-2-3img\"]{ width:50% !important; margin-right: 20px !important; } table[class=\"col-2-3img-last\"]{ width:50% !important; } table[class=\"col-2\"]{ width:47% !important; margin-right:20px !important; } table[class=\"col-2-last\"]{ width:47% !important; } table[class=\"col-3\"]{ width:29% !important; margin-right:20px !important; } table[class=\"col-3-last\"]{ width:29% !important; } table[class=\"row-2\"]{ width:50% !important; } td[class=\"text-center\"]{ text-align: center !important; } /* start clear and remove*/ table[class=\"remove\"]{ display:none !important; } td[class=\"remove\"]{ display:none !important; } /* end clear and remove*/ table[class=\"fix-box\"]{ padding-left:20px !important; padding-right:20px !important; } td[class=\"fix-box\"]{ padding-left:20px !important; padding-right:20px !important; } td[class=\"font-resize\"]{ font-size: 18px !important; line-height: 22px !important; } } @media only screen and (max-width: 479px){ body{ font-size:10px !important; } table[class=\"container2\"]{ width: 100%!important; float:none !important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } img[class=\"small-image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } table[class=\"full-width\"]{ width:100% !important; } table[class=\"full-width-text\"]{ width:100% !important; background-color:#b91925; padding-left:20px !important; padding-right:20px !important; } table[class=\"full-width-text2\"]{ width:100% !important; background-color:#f3f3f3; padding-left:20px !important; padding-right:20px !important; } table[class=\"col-2\"]{ width:100% !important; margin-right:0px !important; } table[class=\"col-2-last\"]{ width:100% !important; } table[class=\"col-3\"]{ width:100% !important; margin-right:0px !important; } table[class=\"col-3-last\"]{ width:100% !important; } table[class=\"row-2\"]{ width:100% !important; } table[id=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } td[id=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } td[class=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } /*start text center*/ td[class=\"text-center\"]{ text-align: center !important; } div[class=\"text-center\"]{ text-align: center !important; } /*end text center*/ /* start clear and remove */ table[id=\"clear-padding\"]{ padding:0 !important; } td[id=\"clear-padding\"]{ padding:0 !important; } td[class=\"clear-padding\"]{ padding:0 !important; } table[class=\"remove-479\"]{ display:none !important; } td[class=\"remove-479\"]{ display:none !important; } table[class=\"clear-align\"]{ float:none !important; } /* end clear and remove */ table[class=\"width-small\"]{ width:100% !important; } table[class=\"fix-box\"]{ padding-left:0px !important; padding-right:0px !important; } td[class=\"fix-box\"]{ padding-left:0px !important; padding-right:0px !important; } td[class=\"font-resize\"]{ font-size: 14px !important; } } @media only screen and (max-width: 320px){ table[class=\"width-small\"]{ width:125px !important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } } </style> </head> <body style=\"font-size:12px;\"> <table id=\"mainStructure\" style=\"background-color:#cccccc;\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- START TAB TOP --> <tr> <td valign=\"top\"> <table style=\"background-color: rgb(204, 204, 204);\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td height=\"6\" valign=\"top\"> <table class=\"full-width\" style=\"height:6px;\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> </tr> <!-- start space height --> <tr> <td height=\"5\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END TAB TOP --> <!--START TOP NAVIGATION ​LAYOUT--> <tr> <td valign=\"top\"> <table style=\"background-color: rgb(204, 204, 204);\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <!-- START TAB TOP --> <tr> <td class=\"fix-box\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td height=\"6\" valign=\"top\"> <table class=\"full-width\" style=\"height:6px;\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td class=\"full-width\" style=\"background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <table class=\"clear-align\" style=\"height: 6px; background-color: rgb(0, 194, 231);\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"282\"> <tbody><tr> <td height=\"6\" valign=\"top\"></td> </tr> </tbody></table> </td> <td class=\"remove-479\" style=\"width: 100%; background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <table class=\"clear-align\" style=\"height: 6px; background-color: rgb(0, 194, 231);\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td height=\"6\" valign=\"top\"></td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END TAB TOP --> <!-- START CONTAINER NAVIGATION --> <tr> <td class=\"fix-box\" align=\"center\" valign=\"top\"> <!-- start top navigation container --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start top navigaton --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <table class=\"container2\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <a style=\"text-decoration: none;\" href=\"#\"><img src=\"https://online.broxel.com/Images/Comunicados/img/logo2.png\" style=\"max-width: 310px; background-color: rgb(30, 194, 229);\" alt=\"Logo\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"293\"></a> </td> </tr> </tbody></table> <!--start content nav --> <table class=\"container2\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td class=\"remove-479\" height=\"13\" valign=\"top\"></td> </tr> <!--start call us --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 13px; line-height: 18px; color: #555555; font-weight:bold; text-align: center; font-family:Arail,Tahoma, Helvetica, Arial, sans-serif;\"><div align=\"left\"><span style=\"color: rgb(30, 194, 229);\">Estimad@</span><br/>[CLIENTE]</div></td> </tr> </tbody></table> </td> <!--start space width --> <!--start space width --> </tr> <!--end call us --> <tr> <td height=\"14\" valign=\"top\"></td> </tr> </tbody></table> <!--end content nav --> </td> </tr> </tbody></table> <!-- end top navigaton --> </td> </tr> </tbody></table> <!-- end top navigation container --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- END CONTAINER NAVIGATION --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!--END TOP NAVIGATION ​LAYOUT--> <!-- START LAYOUT 1 --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <!-- start layout-1 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-1 container width 560px --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <!-- start text content --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 18px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#555555; font-weight:bold; text-align:center;\"><div align=\"left\"><span style=\"color: rgb(0, 194, 231); font-weight:bold;\"> Proceso de asignación de línea terminado </span></div></td> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 13px; line-height: 22px; font-family:Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:left; \"><p>Le informamos que su solicitud de asignación de línea, con el número de folio <strong><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">[FOLIO]</a></strong><br/> arrojó los siguientes resultados:</p></td> </tr> <tr> <td valign=\"top\"> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <!--start Tabla Cuentas --> <tr> <td height=\"15\"><table width=\"420\" Table Border=\"1\" Bordercolor=#ffffff cellspacing=\"2\" cellpadding=\"2\"> <tr> <td width=\"370\" align=\"left\" bgcolor=\"#f2f2f2\" scope=\"row\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\"><p style=\"margin: 0; padding-left: 8px;\">Total de cuentas solicitadas:</a></td> <td width=\"100\" align=\"center\" bgcolor=\"#e6e6e6\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\">[SOLICITADAS]</a></td> </tr> <tr> <th align=\"left\" bgcolor=\"#f2f2f2\" scope=\"row\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\"><p style=\"margin: 0; padding-left: 8px;\">Total de cuentas procesadas con asignación de línea :</a></th> <td align=\"center\" bgcolor=\"#e6e6e6\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\">[BIEN]</a></td> </tr> </table></td> </tr> <!--end Tabla Cuentas --> <tr></tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <!--start Tabla Monto y Total --> <tr> <td height=\"15\"><table width=\"420\" Table Border=\"1\" Bordercolor=#ffffff cellspacing=\"2\" cellpadding=\"2\"> <tr> <th width=\"370\" align=\"left\" bgcolor=\"#f2f2f2\" scope=\"row\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\"><p style=\"margin: 0; padding-left: 8px;\">Monto POS Solicitado</a></th> <td width=\"100\" align=\"center\" bgcolor=\"#e6e6e6\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\">[POSSOLICITADO]</a></td> </tr> <tr> <th align=\"left\" bgcolor=\"#f2f2f2\" scope=\"row\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\"><p style=\"margin: 0; padding-left: 8px;\"> Monto ATM Solicitado</a></th> <td align=\"center\" bgcolor=\"#e6e6e6\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\">[ATMSOLICITADO]</a></td> </tr> <tr> <th align=\"left\" bgcolor=\"#f2f2f2\" scope=\"row\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\"><p style=\"margin: 0; padding-left: 8px;\">Total POS Asignado</a></th> <td align=\"center\" bgcolor=\"#e6e6e6\"><a style=\"font-size: 13px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\">[POSREAL]</a></td> </tr> <tr> <th align=\"left\" bgcolor=\"#f2f2f2\" scope=\"row\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\"><p style=\"margin: 0; padding-left: 8px;\">Total ATM Asignado</a></th> <td align=\"center\" bgcolor=\"#e6e6e6\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\">[ATMREAL]</a></td> </tr> </table> </td> </tr> <!--end Tabla Monto y Total --> <tr></tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <td valign=\"top\"> </td> </tr> <tr> <td style=\"font-size: 13px; line-height: 22px; font-family:Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:left; \"><p><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">";
            htmlIni = htmlIni.Replace("[CLIENTE]", nombreCliente);
            htmlIni = htmlIni.Replace("[FOLIO]", folio);
            htmlIni = htmlIni.Replace("[SOLICITADAS]", cuantas.ToString());
            htmlIni = htmlIni.Replace("[POSSOLICITADO]", PrintCurrency(posSolicitado));
            htmlIni = htmlIni.Replace("[ATMSOLICITADO]", PrintCurrency(atmSolicitado));
            htmlIni = htmlIni.Replace("[POSREAL]", PrintCurrency(totalPOS));
            htmlIni = htmlIni.Replace("[ATMREAL]", PrintCurrency(totalATM));

            sb.Append(htmlIni);
            if (estado != "COMPLETA")
            {
                var listaDescripcionError = dbHelper.CodigosRespuesta.ToList();

                foreach (var error in erroresDispersion)
                {
                    var descripcionError = listaDescripcionError.FirstOrDefault(x => x.Id == error.CodigoRespuesta).Descripcion;

                    sb.AppendFormat("<a style=\"text-decoration: none; color:#a3a2a2; font-weight: normal;\">Las siguientes </a></strong><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">{0}</a></strong><a style=\"text-decoration: none; color:#a3a2a2; font-weight: normal;\"> cuentas no pudieron ser procesadas: </a><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">{1}.</a></strong></strong><br/>", error.CuentasConError.Count, descripcionError);
                    foreach (var cuenta in error.CuentasConError)
                    {
                        sb.AppendFormat("<br/>{0}", cuenta);
                        cuentasConError++;
                    }
                }
            }
            sb = sb.Replace("[BIEN]", (cuantas - cuentasConError).ToString());
            sb.Append("<br><br>¿Dudas? </a>comuníquese con su ejecutivo de cuenta Broxel al <a style=\"text-decoration: none; color: #00C2E7; font-weight: bold;\"> teléfono 4433-0303. </a><br/><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">Gracias por utilizar nuestra plataforma Broxel Corporate Access.</a></td> </tr> <tr> <td align=\"center\" valign=\"top\" width=\"auto\"> </td> </tr> </tbody></table> </td> </tr> <!-- end text content --> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> <!-- end layout-1 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-1 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT 1 --> <!-- START LAYOUT Servicio al Cliente --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td valign=\"top\"> <!-- start layout-6 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-6 container width 560px --> <table class=\"full-width\" style=\"background-color: #ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <!-- start image content --> <tbody><tr> <td valign=\"top\" width=\"100%\"> <!-- start content left --> <table class=\"full-width\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td align=\"left\" valign=\"top\"> <a style=\"text-decoration: none;\" href=\"#\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-tel.png\" alt=\"tel\" width=\"127\" height=\"127\" hspace=\"0\" vspace=\"0\" border=\"0\" style=\"display:block; max-width:125px; \"> </a> </td> <!-- space width --> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"20\"> <tbody><tr> <td valign=\"top\"></td> </tr> </tbody></table> </td> <!-- space width --> <!-- start text content --> <td valign=\"top\"> <table class=\"width-small\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start text content --> <tbody><tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 18px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#555555; font-weight:bold; text-align:left;\"> <span style=\"color: #555555; font-weight: bold;\"> <a href=\"#\" style=\"text-decoration: none; color: #555555; font-weight: bold;\">Servicio al Cliente</a> </span> </td> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 13px; line-height: 22px; font-family:Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:left; \"> Para Broxel lo más importante es ofrecerle un servicio oportuno y de calidad. Si tiene cualquier duda sobre alguno de estos servicios, solo llame al <strong><a style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">01800-Broxel-1</a></strong> (01800-276935-1) y un ejecutivo con gusto le ayudará.</td> </tr> </tbody></table> </td> </tr> <!-- end text content --> </tbody></table> </td> <!-- end text content --> </tr> </tbody></table> </td> </tr> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> <!-- end content left --> </td> </tr> <!-- end image content --> </tbody></table> <!-- end layout-6 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-6 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT Servicio al Cliente --> <!-- START LAYOUT Aviso Privacidad --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <!-- start layout-1 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-1 container width 560px --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"509\"> <!--start space height --> <tbody><tr> <td width=\"509\" height=\"16\"></td> </tr> <!--end space height --> <!-- start text content --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody> <!--start space height --> <tr> <td height=\"0\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 11px; line-height: 16px; font-family: Arial,Tahoma, Helvetica, sans-serif; color: #a3a2a2; font-weight: normal; text-align: center;\"><p><a href=\"http://www.broxel.com/index.php/aviso-privacidad-sapi\" style=\"text-decoration: none; color: #555555; font-weight: bold;\"><span style=\"color: rgb(0, 194, 231); font-weight: bold;\">Aviso de Privacidad</span></a></p> <p> Sus datos se encuentran protegidos a través de nuestro Aviso de Privacidad.<br>Consulte nuestro Aviso de Privacidad en: <a href=\"http://www.broxel.com/index.php/aviso-privacidad-sapi\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">www.broxel.com</a>.</p> <p> Este correo electrónico está destinado a residentes de México y fue enviado desde Broxel.<br/> Si ha recibido este correo de una dirección diferente, significa que fue reenviado.</p> <p>Las comunicaciones vía correo electrónico de Broxel se basan en el manejo de cookies<br/> y/o Web Beacons.</p> <p>Al recibir este correo y consultar el contenido del mismo, usted consiente que Broxel recabe y tenga acceso a sus datos personales.</p> <p>Para más información favor de consultar <strong><a href=\"http://www.broxel.com/index.php/terminos-condiciones\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">Términos y Condiciones.</a></strong></p></td> </tr> <tr> <td align=\"center\" valign=\"top\" width=\"auto\"> </td> </tr> </tbody></table> </td> </tr> <!-- end text content --> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> <!-- end layout-1 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-1 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT 67 --> <!-- START LAYOUT 7--> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <!-- start layout-7 container --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td valign=\"top\"> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <tr> <td valign=\"top\"> <!-- start logo footer and address --> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td valign=\"top\"> <!--start icon socail navigation --> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td align=\"left\" valign=\"top\"> <table class=\"container\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://www.facebook.com/BroxelSF\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-facebook.jpg\" alt=\"icon-facebook\" style=\"max-width:33px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> <td style=\"padding-left:5px; \" class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://twitter.com/BroxelSF\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-twitter.jpg\" alt=\"icon-twitter\" style=\"max-width:30px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> <td style=\"padding-left:5px; \" class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://plus.google.com/u/0/116529217974857845752/posts\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-googleplus.jpg\" alt=\"icon-googleplus\" style=\"max-width:30px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> </tr> </tbody></table> </td> </tr> </tbody></table> <!--end icon socail navigation --> </td> </tr> </tbody></table> <!-- end logo footer and address --> </td> </tr> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> <!-- end layout-7 container --> </td> </tr> <!-- END LAYOUT-7--> <!-- START FOOTER COPY RIGHT --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <!-- start layout-7 container --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <tbody><tr> <!-- start unsubscribe content --> <td valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 13px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:center;\" class=\"text-center\" align=\"center\"> Síguanos por facebook, twitter y google +<br /> Visitenos en <a href=\"http://broxel.com\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">www.broxel.com</a> para conocer más de nuestros productos.</td> </tr> </tbody></table> </td> <!-- end unsubscribe content --> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <!-- start COPY RIGHT content --> <td style=\"font-size: 11px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:center;\" valign=\"top\">© 2014 BROXEL | Servicios Financieros. Todos los derechos Reservados.</td> <!-- end COPY RIGHT content --> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END FOOTER COPY RIGHT --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> </table></body> </html>");
            SendMail(From, ConfigurationManager.AppSettings["emailsAvisosDispersiones"] + " , " + emailNotificar, Subject, sb.ToString(), "yMQ3E3ert6", "Broxel : Asignacion de Lineas");
            //SendMail(From, "aldo.garcia@broxel.com" + " , " + emailNotificar, Subject, sb.ToString(), "yMQ3E3ert6", "Broxel : Asignacion de Lineas");
        }

        private static void EnviarMailDevolucion(string emailNotificar, string folio, string tipo, decimal totalPOS, string estado, int cuantas, string cliente, List<ErroresDispersion> erroresDispersion)
        {
            var dbHelper = new broxelco_rdgEntities();
            int cuentasConError = 0;
            var From = "dispersiones@broxel.com";
            var nombreCliente = dbHelper.clientesBroxel.Where(x => x.claveCliente == cliente).ToList().First().razonSocial;
            var Subject = String.Format("Solicitud de devolucion de línea terminada - cliente : {0}", cliente);
            var sb = new StringBuilder();
            var htmlIni =
                "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"> <html xmlns=\"http://www.w3.org/1999/xhtml\"> <head> <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" /> <meta name=\"viewport\" content=\"initial-scale=1.0\"/> <meta name=\"format-detection\" content=\"telephone=no\"/> <title>Broxel Proceso de Asignación</title> <style type=\"text/css\"> /* Resets: see reset.css for details */ .ReadMsgBody { width: 100%; background-color: #ffffff;} .ExternalClass {width: 100%; background-color: #ffffff;} .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {line-height:100%;} html{width: 100%; } body {-webkit-text-size-adjust:none; -ms-text-size-adjust:none; } body {margin:0; padding:0;} table {border-spacing:0;} img{display:block !important;} table td {border-collapse:collapse;} .yshortcuts a {border-bottom: none !important;} /* main color = #f05e5e other color = #b91925 background color = #ececec */ img{height:auto !important;} @media only screen and (max-width: 640px){ body{ width:auto!important; } table[class=\"container\"]{ width: 100%!important; padding-left: 20px!important; padding-right: 20px!important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; } img[class=\"small-image-100-percent\"]{ width:100% !important; height:auto !important; } table[class=\"full-width\"]{ width:100% !important; } table[class=\"full-width-text\"]{ width:100% !important; background-color:#b91925; padding-left:20px !important; padding-right:20px !important; } table[class=\"full-width-text2\"]{ width:100% !important; background-color:#f3f3f3; padding-left:20px !important; padding-right:20px !important; } table[class=\"col-2-3img\"]{ width:50% !important; margin-right: 20px !important; } table[class=\"col-2-3img-last\"]{ width:50% !important; } table[class=\"col-2\"]{ width:47% !important; margin-right:20px !important; } table[class=\"col-2-last\"]{ width:47% !important; } table[class=\"col-3\"]{ width:29% !important; margin-right:20px !important; } table[class=\"col-3-last\"]{ width:29% !important; } table[class=\"row-2\"]{ width:50% !important; } td[class=\"text-center\"]{ text-align: center !important; } /* start clear and remove*/ table[class=\"remove\"]{ display:none !important; } td[class=\"remove\"]{ display:none !important; } /* end clear and remove*/ table[class=\"fix-box\"]{ padding-left:20px !important; padding-right:20px !important; } td[class=\"fix-box\"]{ padding-left:20px !important; padding-right:20px !important; } td[class=\"font-resize\"]{ font-size: 18px !important; line-height: 22px !important; } } @media only screen and (max-width: 479px){ body{ font-size:10px !important; } table[class=\"container2\"]{ width: 100%!important; float:none !important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } img[class=\"small-image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } table[class=\"full-width\"]{ width:100% !important; } table[class=\"full-width-text\"]{ width:100% !important; background-color:#b91925; padding-left:20px !important; padding-right:20px !important; } table[class=\"full-width-text2\"]{ width:100% !important; background-color:#f3f3f3; padding-left:20px !important; padding-right:20px !important; } table[class=\"col-2\"]{ width:100% !important; margin-right:0px !important; } table[class=\"col-2-last\"]{ width:100% !important; } table[class=\"col-3\"]{ width:100% !important; margin-right:0px !important; } table[class=\"col-3-last\"]{ width:100% !important; } table[class=\"row-2\"]{ width:100% !important; } table[id=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } td[id=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } td[class=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } /*start text center*/ td[class=\"text-center\"]{ text-align: center !important; } div[class=\"text-center\"]{ text-align: center !important; } /*end text center*/ /* start clear and remove */ table[id=\"clear-padding\"]{ padding:0 !important; } td[id=\"clear-padding\"]{ padding:0 !important; } td[class=\"clear-padding\"]{ padding:0 !important; } table[class=\"remove-479\"]{ display:none !important; } td[class=\"remove-479\"]{ display:none !important; } table[class=\"clear-align\"]{ float:none !important; } /* end clear and remove */ table[class=\"width-small\"]{ width:100% !important; } table[class=\"fix-box\"]{ padding-left:0px !important; padding-right:0px !important; } td[class=\"fix-box\"]{ padding-left:0px !important; padding-right:0px !important; } td[class=\"font-resize\"]{ font-size: 14px !important; } } @media only screen and (max-width: 320px){ table[class=\"width-small\"]{ width:125px !important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } } </style> </head> <body style=\"font-size:12px;\"> <table id=\"mainStructure\" style=\"background-color:#cccccc;\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- START TAB TOP --> <tr> <td valign=\"top\"> <table style=\"background-color: rgb(204, 204, 204);\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td height=\"6\" valign=\"top\"> <table class=\"full-width\" style=\"height:6px;\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> </tr> <!-- start space height --> <tr> <td height=\"5\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END TAB TOP --> <!--START TOP NAVIGATION ​LAYOUT--> <tr> <td valign=\"top\"> <table style=\"background-color: rgb(204, 204, 204);\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <!-- START TAB TOP --> <tr> <td class=\"fix-box\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td height=\"6\" valign=\"top\"> <table class=\"full-width\" style=\"height:6px;\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td class=\"full-width\" style=\"background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <table class=\"clear-align\" style=\"height: 6px; background-color: rgb(0, 194, 231);\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"282\"> <tbody><tr> <td height=\"6\" valign=\"top\"></td> </tr> </tbody></table> </td> <td class=\"remove-479\" style=\"width: 100%; background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <table class=\"clear-align\" style=\"height: 6px; background-color: rgb(0, 194, 231);\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td height=\"6\" valign=\"top\"></td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END TAB TOP --> <!-- START CONTAINER NAVIGATION --> <tr> <td class=\"fix-box\" align=\"center\" valign=\"top\"> <!-- start top navigation container --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start top navigaton --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <table class=\"container2\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <a style=\"text-decoration: none;\" href=\"#\"><img src=\"https://online.broxel.com/Images/Comunicados/img/logo2.png\" style=\"max-width: 310px; background-color: rgb(30, 194, 229);\" alt=\"Logo\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"293\"></a> </td> </tr> </tbody></table> <!--start content nav --> <table class=\"container2\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td class=\"remove-479\" height=\"13\" valign=\"top\"></td> </tr> <!--start call us --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 13px; line-height: 18px; color: #555555; font-weight:bold; text-align: center; font-family:Arail,Tahoma, Helvetica, Arial, sans-serif;\"><div align=\"left\"><span style=\"color: rgb(30, 194, 229);\">Estimad@</span><br/>[CLIENTE]</div></td> </tr> </tbody></table> </td> <!--start space width --> <!--start space width --> </tr> <!--end call us --> <tr> <td height=\"14\" valign=\"top\"></td> </tr> </tbody></table> <!--end content nav --> </td> </tr> </tbody></table> <!-- end top navigaton --> </td> </tr> </tbody></table> <!-- end top navigation container --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- END CONTAINER NAVIGATION --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!--END TOP NAVIGATION ​LAYOUT--> <!-- START LAYOUT 1 --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <!-- start layout-1 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-1 container width 560px --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <!-- start text content --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 18px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#555555; font-weight:bold; text-align:center;\"><div align=\"left\"><span style=\"color: rgb(0, 194, 231); font-weight:bold;\"> Proceso de devoluciones de línea terminado </span></div></td> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 13px; line-height: 22px; font-family:Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:left; \"><p>Le informamos que su solicitud de devoluciones de línea, con el número de folio <strong><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">[FOLIO]</a></strong><br/> arrojó los siguientes resultados:</p></td> </tr> <tr> <td valign=\"top\"> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <!--start Tabla Cuentas --> <tr> <td height=\"15\"><table width=\"420\" Table Border=\"1\" Bordercolor=#ffffff cellspacing=\"2\" cellpadding=\"2\"> <tr> <td width=\"370\" align=\"left\" bgcolor=\"#f2f2f2\" scope=\"row\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\"><p style=\"margin: 0; padding-left: 8px;\">Total de cuentas solicitadas:</a></td> <td width=\"100\" align=\"center\" bgcolor=\"#e6e6e6\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\">[SOLICITADAS]</a></td> </tr> <tr> <th align=\"left\" bgcolor=\"#f2f2f2\" scope=\"row\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\"><p style=\"margin: 0; padding-left: 8px;\">Total de cuentas procesadas con devolución de línea :</a></th> <td align=\"center\" bgcolor=\"#e6e6e6\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\">[BIEN]</a></td> </tr> </table></td> </tr> <!--end Tabla Cuentas --> <tr></tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <!--start Tabla Monto y Total --> <tr> <td height=\"15\"><table width=\"420\" Table Border=\"1\" Bordercolor=#ffffff cellspacing=\"2\" cellpadding=\"2\"><tr> <th align=\"left\" bgcolor=\"#f2f2f2\" scope=\"row\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\"><p style=\"margin: 0; padding-left: 8px;\">Total devolución aplicado</a></th> <td align=\"center\" bgcolor=\"#e6e6e6\"><a style=\"font-size: 13px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\">[POSREAL]</a></td> </tr> <!--end space height --> <tr> <td valign=\"top\"> </td> </tr> <tr> <td style=\"font-size: 13px; line-height: 22px; font-family:Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:left; \"><p><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">";
            htmlIni = htmlIni.Replace("[CLIENTE]", nombreCliente);
            htmlIni = htmlIni.Replace("[FOLIO]", folio);
            htmlIni = htmlIni.Replace("[SOLICITADAS]", cuantas.ToString());
            htmlIni = htmlIni.Replace("[POSREAL]", PrintCurrency(totalPOS));

            sb.Append(htmlIni);
            if (estado != "COMPLETA")
            {
                var listaDescripcionError = dbHelper.CodigosRespuesta.ToList();

                foreach (var error in erroresDispersion)
                {
                    var descripcionError = listaDescripcionError.FirstOrDefault(x => x.Id == error.CodigoRespuesta).Descripcion;

                    sb.AppendFormat("<a style=\"text-decoration: none; color:#a3a2a2; font-weight: normal;\">Las siguientes </a></strong><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">{0}</a></strong><a style=\"text-decoration: none; color:#a3a2a2; font-weight: normal;\"> cuentas no pudieron ser procesadas: </a><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">{1}.</a></strong></strong><br/>", error.CuentasConError.Count, descripcionError);
                    foreach (var cuenta in error.CuentasConError)
                    {
                        sb.AppendFormat("<br/>{0}", cuenta);
                        cuentasConError++;
                    }
                }
            }
            sb = sb.Replace("[BIEN]", (cuantas - cuentasConError).ToString());
            sb.Append("<br><br>¿Dudas? </a>comuníquese con su ejecutivo de cuenta Broxel al <a style=\"text-decoration: none; color: #00C2E7; font-weight: bold;\"> teléfono 4433-0303. </a><br/><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">Gracias por utilizar nuestra plataforma Broxel Corporate Access.</a></td> </tr> <tr> <td align=\"center\" valign=\"top\" width=\"auto\"> </td> </tr> </tbody></table> </td> </tr> <!-- end text content --> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> <!-- end layout-1 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-1 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT 1 --> <!-- START LAYOUT Servicio al Cliente --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td valign=\"top\"> <!-- start layout-6 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-6 container width 560px --> <table class=\"full-width\" style=\"background-color: #ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <!-- start image content --> <tbody><tr> <td valign=\"top\" width=\"100%\"> <!-- start content left --> <table class=\"full-width\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td align=\"left\" valign=\"top\"> <a style=\"text-decoration: none;\" href=\"#\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-tel.png\" alt=\"tel\" width=\"127\" height=\"127\" hspace=\"0\" vspace=\"0\" border=\"0\" style=\"display:block; max-width:125px; \"> </a> </td> <!-- space width --> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"20\"> <tbody><tr> <td valign=\"top\"></td> </tr> </tbody></table> </td> <!-- space width --> <!-- start text content --> <td valign=\"top\"> <table class=\"width-small\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start text content --> <tbody><tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 18px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#555555; font-weight:bold; text-align:left;\"> <span style=\"color: #555555; font-weight: bold;\"> <a href=\"#\" style=\"text-decoration: none; color: #555555; font-weight: bold;\">Servicio al Cliente</a> </span> </td> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 13px; line-height: 22px; font-family:Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:left; \"> Para Broxel lo más importante es ofrecerle un servicio oportuno y de calidad. Si tiene cualquier duda sobre alguno de estos servicios, solo llame al <strong><a style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">01800-Broxel-1</a></strong> (01800-276935-1) y un ejecutivo con gusto le ayudará.</td> </tr> </tbody></table> </td> </tr> <!-- end text content --> </tbody></table> </td> <!-- end text content --> </tr> </tbody></table> </td> </tr> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> <!-- end content left --> </td> </tr> <!-- end image content --> </tbody></table> <!-- end layout-6 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-6 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT Servicio al Cliente --> <!-- START LAYOUT Aviso Privacidad --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <!-- start layout-1 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-1 container width 560px --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"509\"> <!--start space height --> <tbody><tr> <td width=\"509\" height=\"16\"></td> </tr> <!--end space height --> <!-- start text content --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody> <!--start space height --> <tr> <td height=\"0\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 11px; line-height: 16px; font-family: Arial,Tahoma, Helvetica, sans-serif; color: #a3a2a2; font-weight: normal; text-align: center;\"><p><a href=\"http://www.broxel.com/index.php/aviso-privacidad-sapi\" style=\"text-decoration: none; color: #555555; font-weight: bold;\"><span style=\"color: rgb(0, 194, 231); font-weight: bold;\">Aviso de Privacidad</span></a></p> <p> Sus datos se encuentran protegidos a través de nuestro Aviso de Privacidad.<br>Consulte nuestro Aviso de Privacidad en: <a href=\"http://www.broxel.com/index.php/aviso-privacidad-sapi\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">www.broxel.com</a>.</p> <p> Este correo electrónico está destinado a residentes de México y fue enviado desde Broxel.<br/> Si ha recibido este correo de una dirección diferente, significa que fue reenviado.</p> <p>Las comunicaciones vía correo electrónico de Broxel se basan en el manejo de cookies<br/> y/o Web Beacons.</p> <p>Al recibir este correo y consultar el contenido del mismo, usted consiente que Broxel recabe y tenga acceso a sus datos personales.</p> <p>Para más información favor de consultar <strong><a href=\"http://www.broxel.com/index.php/terminos-condiciones\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">Términos y Condiciones.</a></strong></p></td> </tr> <tr> <td align=\"center\" valign=\"top\" width=\"auto\"> </td> </tr> </tbody></table> </td> </tr> <!-- end text content --> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> <!-- end layout-1 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-1 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT 67 --> <!-- START LAYOUT 7--> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <!-- start layout-7 container --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td valign=\"top\"> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <tr> <td valign=\"top\"> <!-- start logo footer and address --> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td valign=\"top\"> <!--start icon socail navigation --> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td align=\"left\" valign=\"top\"> <table class=\"container\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://www.facebook.com/BroxelSF\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-facebook.jpg\" alt=\"icon-facebook\" style=\"max-width:33px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> <td style=\"padding-left:5px; \" class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://twitter.com/BroxelSF\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-twitter.jpg\" alt=\"icon-twitter\" style=\"max-width:30px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> <td style=\"padding-left:5px; \" class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://plus.google.com/u/0/116529217974857845752/posts\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-googleplus.jpg\" alt=\"icon-googleplus\" style=\"max-width:30px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> </tr> </tbody></table> </td> </tr> </tbody></table> <!--end icon socail navigation --> </td> </tr> </tbody></table> <!-- end logo footer and address --> </td> </tr> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> <!-- end layout-7 container --> </td> </tr> <!-- END LAYOUT-7--> <!-- START FOOTER COPY RIGHT --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <!-- start layout-7 container --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <tbody><tr> <!-- start unsubscribe content --> <td valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 13px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:center;\" class=\"text-center\" align=\"center\"> Síguanos por facebook, twitter y google +<br /> Visitenos en <a href=\"http://broxel.com\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">www.broxel.com</a> para conocer más de nuestros productos.</td> </tr> </tbody></table> </td> <!-- end unsubscribe content --> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <!-- start COPY RIGHT content --> <td style=\"font-size: 11px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:center;\" valign=\"top\">© 2014 BROXEL | Servicios Financieros. Todos los derechos Reservados.</td> <!-- end COPY RIGHT content --> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END FOOTER COPY RIGHT --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> </table></body> </html>");
            SendMail(From, ConfigurationManager.AppSettings["emailsAvisosDispersiones"] + " , " + emailNotificar, Subject, sb.ToString(), "yMQ3E3ert6", "Broxel : Asignacion de Lineas");
            //SendMail(From, "aldo.garcia@broxel.com" + " , " + emailNotificar, Subject, sb.ToString(), "yMQ3E3ert6", "Broxel : Asignacion de Lineas");
        }

        private static void EnviarMailPago(string emailNotificar, string folio, string tipo, decimal totalPOS, string estado, int cuantas, string cliente, List<ErroresDispersion> erroresDispersion, decimal posSolicitado)
        {
            var dbHelper = new broxelco_rdgEntities();
            int cuentasConError = 0;
            var From = "dispersiones@broxel.com";
            var nombreCliente = dbHelper.clientesBroxel.Where(x => x.claveCliente == cliente).ToList().First().razonSocial;
            var Subject = String.Format("Solicitud de pago terminada - cliente : {0}", cliente);
            var sb = new StringBuilder();
            var htmlIni =
                "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"> <html xmlns=\"http://www.w3.org/1999/xhtml\"> <head> <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" /> <meta name=\"viewport\" content=\"initial-scale=1.0\"/> <meta name=\"format-detection\" content=\"telephone=no\"/> <title>Broxel Proceso de Aplicaci&oacute;n de pago </title> <style type=\"text/css\"> /* Resets: see reset.css for details */ .ReadMsgBody { width: 100%; background-color: #ffffff;} .ExternalClass {width: 100%; background-color: #ffffff;} .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {line-height:100%;} html{width: 100%; } body {-webkit-text-size-adjust:none; -ms-text-size-adjust:none; } body {margin:0; padding:0;} table {border-spacing:0;} img{display:block !important;} table td {border-collapse:collapse;} .yshortcuts a {border-bottom: none !important;} /* main color = #f05e5e other color = #b91925 background color = #ececec */ img{height:auto !important;} @media only screen and (max-width: 640px){ body{ width:auto!important; } table[class=\"container\"]{ width: 100%!important; padding-left: 20px!important; padding-right: 20px!important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; } img[class=\"small-image-100-percent\"]{ width:100% !important; height:auto !important; } table[class=\"full-width\"]{ width:100% !important; } table[class=\"full-width-text\"]{ width:100% !important; background-color:#b91925; padding-left:20px !important; padding-right:20px !important; } table[class=\"full-width-text2\"]{ width:100% !important; background-color:#f3f3f3; padding-left:20px !important; padding-right:20px !important; } table[class=\"col-2-3img\"]{ width:50% !important; margin-right: 20px !important; } table[class=\"col-2-3img-last\"]{ width:50% !important; } table[class=\"col-2\"]{ width:47% !important; margin-right:20px !important; } table[class=\"col-2-last\"]{ width:47% !important; } table[class=\"col-3\"]{ width:29% !important; margin-right:20px !important; } table[class=\"col-3-last\"]{ width:29% !important; } table[class=\"row-2\"]{ width:50% !important; } td[class=\"text-center\"]{ text-align: center !important; } /* start clear and remove*/ table[class=\"remove\"]{ display:none !important; } td[class=\"remove\"]{ display:none !important; } /* end clear and remove*/ table[class=\"fix-box\"]{ padding-left:20px !important; padding-right:20px !important; } td[class=\"fix-box\"]{ padding-left:20px !important; padding-right:20px !important; } td[class=\"font-resize\"]{ font-size: 18px !important; line-height: 22px !important; } } @media only screen and (max-width: 479px){ body{ font-size:10px !important; } table[class=\"container2\"]{ width: 100%!important; float:none !important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } img[class=\"small-image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } table[class=\"full-width\"]{ width:100% !important; } table[class=\"full-width-text\"]{ width:100% !important; background-color:#b91925; padding-left:20px !important; padding-right:20px !important; } table[class=\"full-width-text2\"]{ width:100% !important; background-color:#f3f3f3; padding-left:20px !important; padding-right:20px !important; } table[class=\"col-2\"]{ width:100% !important; margin-right:0px !important; } table[class=\"col-2-last\"]{ width:100% !important; } table[class=\"col-3\"]{ width:100% !important; margin-right:0px !important; } table[class=\"col-3-last\"]{ width:100% !important; } table[class=\"row-2\"]{ width:100% !important; } table[id=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } td[id=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } td[class=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } /*start text center*/ td[class=\"text-center\"]{ text-align: center !important; } div[class=\"text-center\"]{ text-align: center !important; } /*end text center*/ /* start clear and remove */ table[id=\"clear-padding\"]{ padding:0 !important; } td[id=\"clear-padding\"]{ padding:0 !important; } td[class=\"clear-padding\"]{ padding:0 !important; } table[class=\"remove-479\"]{ display:none !important; } td[class=\"remove-479\"]{ display:none !important; } table[class=\"clear-align\"]{ float:none !important; } /* end clear and remove */ table[class=\"width-small\"]{ width:100% !important; } table[class=\"fix-box\"]{ padding-left:0px !important; padding-right:0px !important; } td[class=\"fix-box\"]{ padding-left:0px !important; padding-right:0px !important; } td[class=\"font-resize\"]{ font-size: 14px !important; } } @media only screen and (max-width: 320px){ table[class=\"width-small\"]{ width:125px !important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } } </style> </head> <body style=\"font-size:12px;\"> <table id=\"mainStructure\" style=\"background-color:#cccccc;\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- START TAB TOP --> <tr> <td valign=\"top\"> <table style=\"background-color: rgb(204, 204, 204);\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td height=\"6\" valign=\"top\"> <table class=\"full-width\" style=\"height:6px;\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> </tr> <!-- start space height --> <tr> <td height=\"5\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END TAB TOP --> <!--START TOP NAVIGATION ​LAYOUT--> <tr> <td valign=\"top\"> <table style=\"background-color: rgb(204, 204, 204);\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <!-- START TAB TOP --> <tr> <td class=\"fix-box\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td height=\"6\" valign=\"top\"> <table class=\"full-width\" style=\"height:6px;\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td class=\"full-width\" style=\"background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <table class=\"clear-align\" style=\"height: 6px; background-color: rgb(0, 194, 231);\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"282\"> <tbody><tr> <td height=\"6\" valign=\"top\"></td> </tr> </tbody></table> </td> <td class=\"remove-479\" style=\"width: 100%; background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <table class=\"clear-align\" style=\"height: 6px; background-color: rgb(0, 194, 231);\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td height=\"6\" valign=\"top\"></td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END TAB TOP --> <!-- START CONTAINER NAVIGATION --> <tr> <td class=\"fix-box\" align=\"center\" valign=\"top\"> <!-- start top navigation container --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start top navigaton --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <table class=\"container2\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <a style=\"text-decoration: none;\" href=\"#\"><img src=\"https://online.broxel.com/Images/Comunicados/img/logo2.png\" style=\"max-width: 310px; background-color: rgb(30, 194, 229);\" alt=\"Logo\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"293\"></a> </td> </tr> </tbody></table> <!--start content nav --> <table class=\"container2\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td class=\"remove-479\" height=\"13\" valign=\"top\"></td> </tr> <!--start call us --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 13px; line-height: 18px; color: #555555; font-weight:bold; text-align: center; font-family:Arail,Tahoma, Helvetica, Arial, sans-serif;\"><div align=\"left\"><span style=\"color: rgb(30, 194, 229);\">Estimad@</span><br/>[CLIENTE]</div></td> </tr> </tbody></table> </td> <!--start space width --> <!--start space width --> </tr> <!--end call us --> <tr> <td height=\"14\" valign=\"top\"></td> </tr> </tbody></table> <!--end content nav --> </td> </tr> </tbody></table> <!-- end top navigaton --> </td> </tr> </tbody></table> <!-- end top navigation container --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- END CONTAINER NAVIGATION --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!--END TOP NAVIGATION ​LAYOUT--> <!-- START LAYOUT 1 --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <!-- start layout-1 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-1 container width 560px --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <!-- start text content --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 18px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#555555; font-weight:bold; text-align:center;\"><div align=\"left\"><span style=\"color: rgb(0, 194, 231); font-weight:bold;\"> Proceso de aplicaci&oacute;n de pago terminado </span></div></td> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 13px; line-height: 22px; font-family:Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:left; \"><p>Le informamos que su solicitud de pago, con el número de folio <strong><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">[FOLIO]</a></strong><br/> arrojó los siguientes resultados:</p></td> </tr> <tr> <td valign=\"top\"> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <!--start Tabla Cuentas --> <tr> <td height=\"15\"><table width=\"420\" Table Border=\"1\" Bordercolor=#ffffff cellspacing=\"2\" cellpadding=\"2\"> <tr> <td width=\"370\" align=\"left\" bgcolor=\"#f2f2f2\" scope=\"row\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\"><p style=\"margin: 0; padding-left: 8px;\">Total de cuentas solicitadas:</a></td> <td width=\"100\" align=\"center\" bgcolor=\"#e6e6e6\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\">[SOLICITADAS]</a></td> </tr> <tr> <th align=\"left\" bgcolor=\"#f2f2f2\" scope=\"row\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\"><p style=\"margin: 0; padding-left: 8px;\">Total de cuentas procesadas con asignación de línea :</a></th> <td align=\"center\" bgcolor=\"#e6e6e6\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\">[BIEN]</a></td> </tr> </table></td> </tr> <!--end Tabla Cuentas --> <tr></tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <!--start Tabla Monto y Total --> <tr> <td height=\"15\"><table width=\"420\" Table Border=\"1\" Bordercolor=#ffffff cellspacing=\"2\" cellpadding=\"2\"> <tr> <th width=\"370\" align=\"left\" bgcolor=\"#f2f2f2\" scope=\"row\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\"><p style=\"margin: 0; padding-left: 8px;\">Monto para Pago Solicitado</a></th> <td width=\"100\" align=\"center\" bgcolor=\"#e6e6e6\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\">[POSSOLICITADO]</a></td> </tr> <tr> <th align=\"left\" bgcolor=\"#f2f2f2\" scope=\"row\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\"><p style=\"margin: 0; padding-left: 8px;\">Total Pago Aplicado</a></th> <td align=\"center\" bgcolor=\"#e6e6e6\"><a style=\"font-size: 13px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\">[POSREAL]</a></td> </tr> </table> </td> </tr> <!--end Tabla Monto y Total --> <tr></tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <td valign=\"top\"> </td> </tr> <tr> <td style=\"font-size: 13px; line-height: 22px; font-family:Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:left; \"><p><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">";
            htmlIni = htmlIni.Replace("[CLIENTE]", nombreCliente);
            htmlIni = htmlIni.Replace("[FOLIO]", folio);
            htmlIni = htmlIni.Replace("[SOLICITADAS]", cuantas.ToString());
            htmlIni = htmlIni.Replace("[POSSOLICITADO]", PrintCurrency(posSolicitado));
            htmlIni = htmlIni.Replace("[POSREAL]", PrintCurrency(totalPOS));

            sb.Append(htmlIni);
            if (estado != "COMPLETA")
            {
                var listaDescripcionError = dbHelper.CodigosRespuesta.ToList();

                foreach (var error in erroresDispersion)
                {
                    var descripcionError = listaDescripcionError.FirstOrDefault(x => x.Id == error.CodigoRespuesta).Descripcion;

                    sb.AppendFormat("<a style=\"text-decoration: none; color:#a3a2a2; font-weight: normal;\">Las siguientes </a></strong><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">{0}</a></strong><a style=\"text-decoration: none; color:#a3a2a2; font-weight: normal;\"> cuentas no pudieron ser procesadas: </a><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">{1}.</a></strong></strong><br/>", error.CuentasConError.Count, descripcionError);
                    foreach (var cuenta in error.CuentasConError)
                    {
                        sb.AppendFormat("<br/>{0}", cuenta);
                        cuentasConError++;
                    }
                }
            }
            sb = sb.Replace("[BIEN]", (cuantas - cuentasConError).ToString());
            sb.Append("<br><br>¿Dudas? </a>comuníquese con su ejecutivo de cuenta Broxel al <a style=\"text-decoration: none; color: #00C2E7; font-weight: bold;\"> teléfono 4433-0303. </a><br/><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">Gracias por utilizar nuestra plataforma Broxel Corporate Access.</a></td> </tr> <tr> <td align=\"center\" valign=\"top\" width=\"auto\"> </td> </tr> </tbody></table> </td> </tr> <!-- end text content --> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> <!-- end layout-1 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-1 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT 1 --> <!-- START LAYOUT Servicio al Cliente --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td valign=\"top\"> <!-- start layout-6 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-6 container width 560px --> <table class=\"full-width\" style=\"background-color: #ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <!-- start image content --> <tbody><tr> <td valign=\"top\" width=\"100%\"> <!-- start content left --> <table class=\"full-width\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td align=\"left\" valign=\"top\"> <a style=\"text-decoration: none;\" href=\"#\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-tel.png\" alt=\"tel\" width=\"127\" height=\"127\" hspace=\"0\" vspace=\"0\" border=\"0\" style=\"display:block; max-width:125px; \"> </a> </td> <!-- space width --> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"20\"> <tbody><tr> <td valign=\"top\"></td> </tr> </tbody></table> </td> <!-- space width --> <!-- start text content --> <td valign=\"top\"> <table class=\"width-small\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start text content --> <tbody><tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 18px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#555555; font-weight:bold; text-align:left;\"> <span style=\"color: #555555; font-weight: bold;\"> <a href=\"#\" style=\"text-decoration: none; color: #555555; font-weight: bold;\">Servicio al Cliente</a> </span> </td> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 13px; line-height: 22px; font-family:Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:left; \"> Para Broxel lo más importante es ofrecerle un servicio oportuno y de calidad. Si tiene cualquier duda sobre alguno de estos servicios, solo llame al <strong><a style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">01800-Broxel-1</a></strong> (01800-276935-1) y un ejecutivo con gusto le ayudará.</td> </tr> </tbody></table> </td> </tr> <!-- end text content --> </tbody></table> </td> <!-- end text content --> </tr> </tbody></table> </td> </tr> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> <!-- end content left --> </td> </tr> <!-- end image content --> </tbody></table> <!-- end layout-6 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-6 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT Servicio al Cliente --> <!-- START LAYOUT Aviso Privacidad --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <!-- start layout-1 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-1 container width 560px --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"509\"> <!--start space height --> <tbody><tr> <td width=\"509\" height=\"16\"></td> </tr> <!--end space height --> <!-- start text content --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody> <!--start space height --> <tr> <td height=\"0\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 11px; line-height: 16px; font-family: Arial,Tahoma, Helvetica, sans-serif; color: #a3a2a2; font-weight: normal; text-align: center;\"><p><a href=\"http://www.broxel.com/index.php/aviso-privacidad-sapi\" style=\"text-decoration: none; color: #555555; font-weight: bold;\"><span style=\"color: rgb(0, 194, 231); font-weight: bold;\">Aviso de Privacidad</span></a></p> <p> Sus datos se encuentran protegidos a través de nuestro Aviso de Privacidad.<br>Consulte nuestro Aviso de Privacidad en: <a href=\"http://www.broxel.com/index.php/aviso-privacidad-sapi\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">www.broxel.com</a>.</p> <p> Este correo electrónico está destinado a residentes de México y fue enviado desde Broxel.<br/> Si ha recibido este correo de una dirección diferente, significa que fue reenviado.</p> <p>Las comunicaciones vía correo electrónico de Broxel se basan en el manejo de cookies<br/> y/o Web Beacons.</p> <p>Al recibir este correo y consultar el contenido del mismo, usted consiente que Broxel recabe y tenga acceso a sus datos personales.</p> <p>Para más información favor de consultar <strong><a href=\"http://www.broxel.com/index.php/terminos-condiciones\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">Términos y Condiciones.</a></strong></p></td> </tr> <tr> <td align=\"center\" valign=\"top\" width=\"auto\"> </td> </tr> </tbody></table> </td> </tr> <!-- end text content --> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> <!-- end layout-1 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-1 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT 67 --> <!-- START LAYOUT 7--> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <!-- start layout-7 container --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td valign=\"top\"> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <tr> <td valign=\"top\"> <!-- start logo footer and address --> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td valign=\"top\"> <!--start icon socail navigation --> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td align=\"left\" valign=\"top\"> <table class=\"container\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://www.facebook.com/BroxelSF\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-facebook.jpg\" alt=\"icon-facebook\" style=\"max-width:33px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> <td style=\"padding-left:5px; \" class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://twitter.com/BroxelSF\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-twitter.jpg\" alt=\"icon-twitter\" style=\"max-width:30px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> <td style=\"padding-left:5px; \" class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://plus.google.com/u/0/116529217974857845752/posts\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-googleplus.jpg\" alt=\"icon-googleplus\" style=\"max-width:30px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> </tr> </tbody></table> </td> </tr> </tbody></table> <!--end icon socail navigation --> </td> </tr> </tbody></table> <!-- end logo footer and address --> </td> </tr> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> <!-- end layout-7 container --> </td> </tr> <!-- END LAYOUT-7--> <!-- START FOOTER COPY RIGHT --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <!-- start layout-7 container --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <tbody><tr> <!-- start unsubscribe content --> <td valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 13px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:center;\" class=\"text-center\" align=\"center\"> Síguanos por facebook, twitter y google +<br /> Visitenos en <a href=\"http://broxel.com\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">www.broxel.com</a> para conocer más de nuestros productos.</td> </tr> </tbody></table> </td> <!-- end unsubscribe content --> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <!-- start COPY RIGHT content --> <td style=\"font-size: 11px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:center;\" valign=\"top\">© 2014 BROXEL | Servicios Financieros. Todos los derechos Reservados.</td> <!-- end COPY RIGHT content --> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END FOOTER COPY RIGHT --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> </table></body> </html>");
            SendMail(From, ConfigurationManager.AppSettings["emailsAvisosDispersiones"] + emailNotificar != String.Empty ? ", " + emailNotificar : "", Subject, sb.ToString(), "yMQ3E3ert6", "Broxel :  Aplicación de pago");
            //SendMail(From, "aldogarcia@broxel.com" + emailNotificar!=String.Empty?", "+emailNotificar:"", Subject, sb.ToString(), "yMQ3E3ert6", "Broxel : Aplicación de pago");
        }

        public static void SendMail(string from, string to, string subject, string body, string fromPassword, string fromName = "")
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

                SmtpClient emailClient = null;

                if (from.Contains("@broxel.com"))
                {
                    emailClient = new SmtpClient
                    {
                        Host = "10.211.64.74",
                        Port = 252,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = true,
                    };
                }
                else
                {
                    emailClient = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        //Host = "mamadas.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(from, fromPassword),
                    };
                }

                emailClient.Send(message);
            }
            catch (Exception e)
            {
                Trace.WriteLine(DateTime.Now.ToString("O") + " Error al enviar correo a " + to + ", asunto: " + subject + " body: " + body + " error: " + e);
                try
                {
                    using (var ctx = new BroxelSQLEntities())
                    {
                        var emailErr = new logCorreoDispersion
                        {
                            body = body,
                            error = e.Message + " " + (e.InnerException != null ? e.InnerException.Message : ""),
                            fechaRegistro = DateTime.Now,
                            from = from,
                            fromName = fromName,
                            fromPassword = fromPassword,
                            status = 0,
                            subject = subject,
                            to = to
                        };
                        ctx.logCorreoDispersion.Add(emailErr);
                        ctx.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    Trace.WriteLine(DateTime.Now.ToString("O") + " Error al enviar persistir correo " + e);
                }
            }
        }

        public static void SendMailMyCard(string from, string to, string cc, string subject, string body, string fromPassword, string fromName = "", string[] attachment = null)
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

                if (!String.IsNullOrEmpty(cc))
                    message.CC.Add(cc);

                if (attachment != null)
                {
                    foreach (var s in attachment)
                    {
                        message.Attachments.Add(new Attachment(s));
                    }
                }

                SmtpClient emailClient = null;
                emailClient = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(from, fromPassword),
                };
                emailClient.Send(message);
                message.Dispose();
            }
            catch (Exception e)
            {
                Trace.WriteLine(DateTime.Now.ToString("O") + " Error al enviar correo a " + to +", cc: " + cc + ", asunto: " + subject + " body: " + body + " error: " + e);
                try
                {
                    using (var ctx = new BroxelSQLEntities())
                    {
                        var emailErr = new logCorreoDispersion
                        {
                            body = body,
                            error = e.Message + " " + (e.InnerException != null ? e.InnerException.Message : ""),
                            fechaRegistro = DateTime.Now,
                            from = from,
                            fromName = fromName,
                            fromPassword = fromPassword,
                            status = 0,
                            subject = subject,
                            to = to
                        };
                        ctx.logCorreoDispersion.Add(emailErr);
                        ctx.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    Trace.WriteLine(DateTime.Now.ToString("O") + " Error al enviar persistir correo " + e);
                }
            }
        }

        private static void EnviarMailTransferencia(string emailNotificar, string folio, string tipo, float totalPOS, string estado, int cuantas, string cliente, List<ErroresDispersion> erroresDispersion, float posSolicitado)
        {
            var dbHelper = new broxelco_rdgEntities();
            var cuentasConError = 0;
            const string From = "dispersiones@broxel.com";
            var nombreCliente = dbHelper.clientesBroxel.Where(x => x.claveCliente == cliente).ToList().First().razonSocial;
            var Subject = string.Format("Solicitud de transferencia terminada - cliente : {0}", cliente);
            var sb = new StringBuilder();
            var htmlIni =
                "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"> <html xmlns=\"http://www.w3.org/1999/xhtml\"> <head> <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" /> <meta name=\"viewport\" content=\"initial-scale=1.0\"/> <meta name=\"format-detection\" content=\"telephone=no\"/> <title>Broxel Proceso de Transferencia </title> <style type=\"text/css\"> /* Resets: see reset.css for details */ .ReadMsgBody { width: 100%; background-color: #ffffff;} .ExternalClass {width: 100%; background-color: #ffffff;} .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {line-height:100%;} html{width: 100%; } body {-webkit-text-size-adjust:none; -ms-text-size-adjust:none; } body {margin:0; padding:0;} table {border-spacing:0;} img{display:block !important;} table td {border-collapse:collapse;} .yshortcuts a {border-bottom: none !important;} /* main color = #f05e5e other color = #b91925 background color = #ececec */ img{height:auto !important;} @media only screen and (max-width: 640px){ body{ width:auto!important; } table[class=\"container\"]{ width: 100%!important; padding-left: 20px!important; padding-right: 20px!important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; } img[class=\"small-image-100-percent\"]{ width:100% !important; height:auto !important; } table[class=\"full-width\"]{ width:100% !important; } table[class=\"full-width-text\"]{ width:100% !important; background-color:#b91925; padding-left:20px !important; padding-right:20px !important; } table[class=\"full-width-text2\"]{ width:100% !important; background-color:#f3f3f3; padding-left:20px !important; padding-right:20px !important; } table[class=\"col-2-3img\"]{ width:50% !important; margin-right: 20px !important; } table[class=\"col-2-3img-last\"]{ width:50% !important; } table[class=\"col-2\"]{ width:47% !important; margin-right:20px !important; } table[class=\"col-2-last\"]{ width:47% !important; } table[class=\"col-3\"]{ width:29% !important; margin-right:20px !important; } table[class=\"col-3-last\"]{ width:29% !important; } table[class=\"row-2\"]{ width:50% !important; } td[class=\"text-center\"]{ text-align: center !important; } /* start clear and remove*/ table[class=\"remove\"]{ display:none !important; } td[class=\"remove\"]{ display:none !important; } /* end clear and remove*/ table[class=\"fix-box\"]{ padding-left:20px !important; padding-right:20px !important; } td[class=\"fix-box\"]{ padding-left:20px !important; padding-right:20px !important; } td[class=\"font-resize\"]{ font-size: 18px !important; line-height: 22px !important; } } @media only screen and (max-width: 479px){ body{ font-size:10px !important; } table[class=\"container2\"]{ width: 100%!important; float:none !important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } img[class=\"small-image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } table[class=\"full-width\"]{ width:100% !important; } table[class=\"full-width-text\"]{ width:100% !important; background-color:#b91925; padding-left:20px !important; padding-right:20px !important; } table[class=\"full-width-text2\"]{ width:100% !important; background-color:#f3f3f3; padding-left:20px !important; padding-right:20px !important; } table[class=\"col-2\"]{ width:100% !important; margin-right:0px !important; } table[class=\"col-2-last\"]{ width:100% !important; } table[class=\"col-3\"]{ width:100% !important; margin-right:0px !important; } table[class=\"col-3-last\"]{ width:100% !important; } table[class=\"row-2\"]{ width:100% !important; } table[id=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } td[id=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } td[class=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } /*start text center*/ td[class=\"text-center\"]{ text-align: center !important; } div[class=\"text-center\"]{ text-align: center !important; } /*end text center*/ /* start clear and remove */ table[id=\"clear-padding\"]{ padding:0 !important; } td[id=\"clear-padding\"]{ padding:0 !important; } td[class=\"clear-padding\"]{ padding:0 !important; } table[class=\"remove-479\"]{ display:none !important; } td[class=\"remove-479\"]{ display:none !important; } table[class=\"clear-align\"]{ float:none !important; } /* end clear and remove */ table[class=\"width-small\"]{ width:100% !important; } table[class=\"fix-box\"]{ padding-left:0px !important; padding-right:0px !important; } td[class=\"fix-box\"]{ padding-left:0px !important; padding-right:0px !important; } td[class=\"font-resize\"]{ font-size: 14px !important; } } @media only screen and (max-width: 320px){ table[class=\"width-small\"]{ width:125px !important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } } </style> </head> <body style=\"font-size:12px;\"> <table id=\"mainStructure\" style=\"background-color:#cccccc;\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- START TAB TOP --> <tr> <td valign=\"top\"> <table style=\"background-color: rgb(204, 204, 204);\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td height=\"6\" valign=\"top\"> <table class=\"full-width\" style=\"height:6px;\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> </tr> <!-- start space height --> <tr> <td height=\"5\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END TAB TOP --> <!--START TOP NAVIGATION ​LAYOUT--> <tr> <td valign=\"top\"> <table style=\"background-color: rgb(204, 204, 204);\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <!-- START TAB TOP --> <tr> <td class=\"fix-box\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td height=\"6\" valign=\"top\"> <table class=\"full-width\" style=\"height:6px;\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td class=\"full-width\" style=\"background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <table class=\"clear-align\" style=\"height: 6px; background-color: rgb(0, 194, 231);\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"282\"> <tbody><tr> <td height=\"6\" valign=\"top\"></td> </tr> </tbody></table> </td> <td class=\"remove-479\" style=\"width: 100%; background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <table class=\"clear-align\" style=\"height: 6px; background-color: rgb(0, 194, 231);\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td height=\"6\" valign=\"top\"></td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END TAB TOP --> <!-- START CONTAINER NAVIGATION --> <tr> <td class=\"fix-box\" align=\"center\" valign=\"top\"> <!-- start top navigation container --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start top navigaton --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <table class=\"container2\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <a style=\"text-decoration: none;\" href=\"#\"><img src=\"https://online.broxel.com/Images/Comunicados/img/logo2.png\" style=\"max-width: 310px; background-color: rgb(30, 194, 229);\" alt=\"Logo\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"293\"></a> </td> </tr> </tbody></table> <!--start content nav --> <table class=\"container2\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td class=\"remove-479\" height=\"13\" valign=\"top\"></td> </tr> <!--start call us --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 13px; line-height: 18px; color: #555555; font-weight:bold; text-align: center; font-family:Arail,Tahoma, Helvetica, Arial, sans-serif;\"><div align=\"left\"><span style=\"color: rgb(30, 194, 229);\">Estimad@</span><br/>[CLIENTE]</div></td> </tr> </tbody></table> </td> <!--start space width --> <!--start space width --> </tr> <!--end call us --> <tr> <td height=\"14\" valign=\"top\"></td> </tr> </tbody></table> <!--end content nav --> </td> </tr> </tbody></table> <!-- end top navigaton --> </td> </tr> </tbody></table> <!-- end top navigation container --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- END CONTAINER NAVIGATION --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!--END TOP NAVIGATION ​LAYOUT--> <!-- START LAYOUT 1 --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <!-- start layout-1 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-1 container width 560px --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <!-- start text content --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 18px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#555555; font-weight:bold; text-align:center;\"><div align=\"left\"><span style=\"color: rgb(0, 194, 231); font-weight:bold;\"> Proceso de transferencia terminado </span></div></td> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 13px; line-height: 22px; font-family:Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:left; \"><p>Le informamos que su solicitud de transferencia entre cuentas, con el número de folio <strong><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">[FOLIO]</a></strong><br/> arrojó los siguientes resultados:</p></td> </tr> <tr> <td valign=\"top\"> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <!--start Tabla Cuentas --> <tr> <td height=\"15\"><table width=\"420\" Table Border=\"1\" Bordercolor=#ffffff cellspacing=\"2\" cellpadding=\"2\"> <tr> <td width=\"370\" align=\"left\" bgcolor=\"#f2f2f2\" scope=\"row\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\"><p style=\"margin: 0; padding-left: 8px;\">Total de cuentas solicitadas:</a></td> <td width=\"100\" align=\"center\" bgcolor=\"#e6e6e6\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\">[SOLICITADAS]</a></td> </tr> <tr> <th align=\"left\" bgcolor=\"#f2f2f2\" scope=\"row\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\"><p style=\"margin: 0; padding-left: 8px;\">Total de cuentas procesadas con transferencia :</a></th> <td align=\"center\" bgcolor=\"#e6e6e6\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\">[BIEN]</a></td> </tr> </table></td> </tr> <!--end Tabla Cuentas --> <tr></tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <!--start Tabla Monto y Total --> <tr> <td height=\"15\"><table width=\"420\" Table Border=\"1\" Bordercolor=#ffffff cellspacing=\"2\" cellpadding=\"2\"> <tr> <th width=\"370\" align=\"left\" bgcolor=\"#f2f2f2\" scope=\"row\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\"><p style=\"margin: 0; padding-left: 8px;\">Monto de Transferencia Solicitado</a></th> <td width=\"100\" align=\"center\" bgcolor=\"#e6e6e6\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\">[POSSOLICITADO]</a></td> </tr> <tr> <th align=\"left\" bgcolor=\"#f2f2f2\" scope=\"row\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\"><p style=\"margin: 0; padding-left: 8px;\">Total Transferencia Aplicado</a></th> <td align=\"center\" bgcolor=\"#e6e6e6\"><a style=\"font-size: 13px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\">[POSREAL]</a></td> </tr> </table> </td> </tr> <!--end Tabla Monto y Total --> <tr></tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <td valign=\"top\"> </td> </tr> <tr> <td style=\"font-size: 13px; line-height: 22px; font-family:Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:left; \"><p><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">";
            htmlIni = htmlIni.Replace("[CLIENTE]", nombreCliente);
            htmlIni = htmlIni.Replace("[FOLIO]", folio);
            htmlIni = htmlIni.Replace("[SOLICITADAS]", cuantas.ToString());
            htmlIni = htmlIni.Replace("[POSSOLICITADO]", PrintCurrency(posSolicitado));
            htmlIni = htmlIni.Replace("[POSREAL]", PrintCurrency(totalPOS));

            sb.Append(htmlIni);
            if (estado != "COMPLETA")
            {
                var listaDescripcionError = dbHelper.CodigosRespuesta.ToList();

                foreach (var error in erroresDispersion)
                {
                    var descripcionError = listaDescripcionError.FirstOrDefault(x => x.Id == error.CodigoRespuesta).Descripcion;

                    sb.AppendFormat("<a style=\"text-decoration: none; color:#a3a2a2; font-weight: normal;\">Las siguientes </a></strong><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">{0}</a></strong><a style=\"text-decoration: none; color:#a3a2a2; font-weight: normal;\"> cuentas no pudieron ser procesadas: </a><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">{1}.</a></strong></strong><br/>", error.CuentasConError.Count, descripcionError);
                    foreach (var cuenta in error.CuentasConError)
                    {
                        sb.AppendFormat("<br/>{0}", cuenta);
                        cuentasConError++;
                    }
                }
            }
            sb = sb.Replace("[BIEN]", (cuantas - cuentasConError).ToString());
            sb.Append("<br><br>¿Dudas? </a>comuníquese con su ejecutivo de cuenta Broxel al <a style=\"text-decoration: none; color: #00C2E7; font-weight: bold;\"> teléfono 4433-0303. </a><br/><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">Gracias por utilizar nuestra plataforma Broxel Corporate Access.</a></td> </tr> <tr> <td align=\"center\" valign=\"top\" width=\"auto\"> </td> </tr> </tbody></table> </td> </tr> <!-- end text content --> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> <!-- end layout-1 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-1 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT 1 --> <!-- START LAYOUT Servicio al Cliente --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td valign=\"top\"> <!-- start layout-6 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-6 container width 560px --> <table class=\"full-width\" style=\"background-color: #ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <!-- start image content --> <tbody><tr> <td valign=\"top\" width=\"100%\"> <!-- start content left --> <table class=\"full-width\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td align=\"left\" valign=\"top\"> <a style=\"text-decoration: none;\" href=\"#\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-tel.png\" alt=\"tel\" width=\"127\" height=\"127\" hspace=\"0\" vspace=\"0\" border=\"0\" style=\"display:block; max-width:125px; \"> </a> </td> <!-- space width --> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"20\"> <tbody><tr> <td valign=\"top\"></td> </tr> </tbody></table> </td> <!-- space width --> <!-- start text content --> <td valign=\"top\"> <table class=\"width-small\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start text content --> <tbody><tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 18px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#555555; font-weight:bold; text-align:left;\"> <span style=\"color: #555555; font-weight: bold;\"> <a href=\"#\" style=\"text-decoration: none; color: #555555; font-weight: bold;\">Servicio al Cliente</a> </span> </td> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 13px; line-height: 22px; font-family:Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:left; \"> Para Broxel lo más importante es ofrecerle un servicio oportuno y de calidad. Si tiene cualquier duda sobre alguno de estos servicios, solo llame al <strong><a style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">01800-Broxel-1</a></strong> (01800-276935-1) y un ejecutivo con gusto le ayudará.</td> </tr> </tbody></table> </td> </tr> <!-- end text content --> </tbody></table> </td> <!-- end text content --> </tr> </tbody></table> </td> </tr> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> <!-- end content left --> </td> </tr> <!-- end image content --> </tbody></table> <!-- end layout-6 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-6 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT Servicio al Cliente --> <!-- START LAYOUT Aviso Privacidad --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <!-- start layout-1 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-1 container width 560px --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"509\"> <!--start space height --> <tbody><tr> <td width=\"509\" height=\"16\"></td> </tr> <!--end space height --> <!-- start text content --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody> <!--start space height --> <tr> <td height=\"0\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 11px; line-height: 16px; font-family: Arial,Tahoma, Helvetica, sans-serif; color: #a3a2a2; font-weight: normal; text-align: center;\"><p><a href=\"http://www.broxel.com/index.php/aviso-privacidad-sapi\" style=\"text-decoration: none; color: #555555; font-weight: bold;\"><span style=\"color: rgb(0, 194, 231); font-weight: bold;\">Aviso de Privacidad</span></a></p> <p> Sus datos se encuentran protegidos a través de nuestro Aviso de Privacidad.<br>Consulte nuestro Aviso de Privacidad en: <a href=\"http://www.broxel.com/index.php/aviso-privacidad-sapi\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">www.broxel.com</a>.</p> <p> Este correo electrónico está destinado a residentes de México y fue enviado desde Broxel.<br/> Si ha recibido este correo de una dirección diferente, significa que fue reenviado.</p> <p>Las comunicaciones vía correo electrónico de Broxel se basan en el manejo de cookies<br/> y/o Web Beacons.</p> <p>Al recibir este correo y consultar el contenido del mismo, usted consiente que Broxel recabe y tenga acceso a sus datos personales.</p> <p>Para más información favor de consultar <strong><a href=\"http://www.broxel.com/index.php/terminos-condiciones\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">Términos y Condiciones.</a></strong></p></td> </tr> <tr> <td align=\"center\" valign=\"top\" width=\"auto\"> </td> </tr> </tbody></table> </td> </tr> <!-- end text content --> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> <!-- end layout-1 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-1 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT 67 --> <!-- START LAYOUT 7--> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <!-- start layout-7 container --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td valign=\"top\"> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <tr> <td valign=\"top\"> <!-- start logo footer and address --> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td valign=\"top\"> <!--start icon socail navigation --> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td align=\"left\" valign=\"top\"> <table class=\"container\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://www.facebook.com/BroxelSF\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-facebook.jpg\" alt=\"icon-facebook\" style=\"max-width:33px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> <td style=\"padding-left:5px; \" class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://twitter.com/BroxelSF\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-twitter.jpg\" alt=\"icon-twitter\" style=\"max-width:30px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> <td style=\"padding-left:5px; \" class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://plus.google.com/u/0/116529217974857845752/posts\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-googleplus.jpg\" alt=\"icon-googleplus\" style=\"max-width:30px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> </tr> </tbody></table> </td> </tr> </tbody></table> <!--end icon socail navigation --> </td> </tr> </tbody></table> <!-- end logo footer and address --> </td> </tr> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> <!-- end layout-7 container --> </td> </tr> <!-- END LAYOUT-7--> <!-- START FOOTER COPY RIGHT --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <!-- start layout-7 container --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <tbody><tr> <!-- start unsubscribe content --> <td valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 13px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:center;\" class=\"text-center\" align=\"center\"> Síguanos por facebook, twitter y google +<br /> Visitenos en <a href=\"http://broxel.com\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">www.broxel.com</a> para conocer más de nuestros productos.</td> </tr> </tbody></table> </td> <!-- end unsubscribe content --> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <!-- start COPY RIGHT content --> <td style=\"font-size: 11px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:center;\" valign=\"top\">© 2014 BROXEL | Servicios Financieros. Todos los derechos Reservados.</td> <!-- end COPY RIGHT content --> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END FOOTER COPY RIGHT --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> </table></body> </html>");
            SendMail(From, ConfigurationManager.AppSettings["emailsAvisosDispersiones"] + emailNotificar != String.Empty ? ", " + emailNotificar : "", Subject, sb.ToString(), "yMQ3E3ert6", "Broxel :  Transferencia entre cuentas");
            //SendMail(From, "aldogarcia@broxel.com" + emailNotificar!=String.Empty?", "+emailNotificar:"", Subject, sb.ToString(), "yMQ3E3ert6",  "Broxel :  Transferencia entre cuentas");
        }

        internal static void NotificaEstadosDeCuenta(string grupoDeLiquidacion, DateTime fechaUltimaLiquidacion)
        {

            var db = new broxelco_rdgEntities();
            var enviarCorreos = (from m in db.maquila
                                 join r in db.registro_de_cuenta on m.num_cuenta equals r.numero_de_cuenta
                                 join a in db.accessos_clientes on m.id equals a.IdMaquila
                                 select new
                                 {
                                     terminacion = m.nro_tarjeta,
                                     fecha = r.fecha_ultima_liquidacion,
                                     cuenta = m.num_cuenta,
                                     mail = a.Email,
                                     nombre = m.nombre_titular,
                                     grupo = r.grupo_de_liquidacion
                                 }).Where(x => x.fecha == fechaUltimaLiquidacion && x.grupo == grupoDeLiquidacion && x.mail != "" && x.mail != null).ToList();

            //var db = new broxelco_rdgEntities();
            //var enviarCorreos = (from r in db.registro_de_cuenta
            //                     join m in db.maquila on r.numero_de_cuenta equals m.num_cuenta
            //select new { terminacion = m.nro_tarjeta, fecha = r.fecha_ultima_liquidacion, cuenta = m.num_cuenta, mail = m.email, nombre = m.nombre_titular, grupo = r.grupo_de_liquidacion }).Where(x => x.fecha == fechaUltimaLiquidacion && x.grupo == grupoDeLiquidacion && x.mail != "" && x.mail != null).ToList();
            foreach (var correo in enviarCorreos)
            {
                var html = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"> <html xmlns=\"http://www.w3.org/1999/xhtml\"> <head> <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" /> <meta name=\"viewport\" content=\"initial-scale=1.0\"/> <meta name=\"format-detection\" content=\"telephone=no\"/> <title>Broxel Online</title> <style type=\"text/css\"> /* Resets: see reset.css for details */ .ReadMsgBody { width: 100%; background-color: #ffffff;} .ExternalClass {width: 100%; background-color: #ffffff;} .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {line-height:100%;} html{width: 100%; } body {-webkit-text-size-adjust:none; -ms-text-size-adjust:none; } body {margin:0; padding:0;} table {border-spacing:0;} img{display:block !important;} table td {border-collapse:collapse;} .yshortcuts a {border-bottom: none !important;} /* main color = #f05e5e other color = #b91925 background color = #ececec */ img{height:auto !important;} @media only screen and (max-width: 640px){ body{ width:auto!important; } table[class=\"container\"]{ width: 100%!important; padding-left: 20px!important; padding-right: 20px!important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; } img[class=\"small-image-100-percent\"]{ width:100% !important; height:auto !important; } table[class=\"full-width\"]{ width:100% !important; } table[class=\"full-width-text\"]{ width:100% !important; background-color:#b91925; padding-left:20px !important; padding-right:20px !important; } table[class=\"full-width-text2\"]{ width:100% !important; background-color:#f3f3f3; padding-left:20px !important; padding-right:20px !important; } table[class=\"col-2-3img\"]{ width:50% !important; margin-right: 20px !important; } table[class=\"col-2-3img-last\"]{ width:50% !important; } table[class=\"col-2\"]{ width:47% !important; margin-right:20px !important; } table[class=\"col-2-last\"]{ width:47% !important; } table[class=\"col-3\"]{ width:29% !important; margin-right:20px !important; } table[class=\"col-3-last\"]{ width:29% !important; } table[class=\"row-2\"]{ width:50% !important; } td[class=\"text-center\"]{ text-align: center !important; } /* start clear and remove*/ table[class=\"remove\"]{ display:none !important; } td[class=\"remove\"]{ display:none !important; } /* end clear and remove*/ table[class=\"fix-box\"]{ padding-left:20px !important; padding-right:20px !important; } td[class=\"fix-box\"]{ padding-left:20px !important; padding-right:20px !important; } td[class=\"font-resize\"]{ font-size: 18px !important; line-height: 22px !important; } } @media only screen and (max-width: 479px){ body{ font-size:10px !important; } table[class=\"container2\"]{ width: 100%!important; float:none !important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } img[class=\"small-image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } table[class=\"full-width\"]{ width:100% !important; } table[class=\"full-width-text\"]{ width:100% !important; background-color:#b91925; padding-left:20px !important; padding-right:20px !important; } table[class=\"full-width-text2\"]{ width:100% !important; background-color:#f3f3f3; padding-left:20px !important; padding-right:20px !important; } table[class=\"col-2\"]{ width:100% !important; margin-right:0px !important; } table[class=\"col-2-last\"]{ width:100% !important; } table[class=\"col-3\"]{ width:100% !important; margin-right:0px !important; } table[class=\"col-3-last\"]{ width:100% !important; } table[class=\"row-2\"]{ width:100% !important; } table[id=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } td[id=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } td[class=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } /*start text center*/ td[class=\"text-center\"]{ text-align: center !important; } div[class=\"text-center\"]{ text-align: center !important; } /*end text center*/ /* start clear and remove */ table[id=\"clear-padding\"]{ padding:0 !important; } td[id=\"clear-padding\"]{ padding:0 !important; } td[class=\"clear-padding\"]{ padding:0 !important; } table[class=\"remove-479\"]{ display:none !important; } td[class=\"remove-479\"]{ display:none !important; } table[class=\"clear-align\"]{ float:none !important; } /* end clear and remove */ table[class=\"width-small\"]{ width:100% !important; } table[class=\"fix-box\"]{ padding-left:0px !important; padding-right:0px !important; } td[class=\"fix-box\"]{ padding-left:0px !important; padding-right:0px !important; } td[class=\"font-resize\"]{ font-size: 14px !important; } } @media only screen and (max-width: 320px){ table[class=\"width-small\"]{ width:125px !important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } } </style> </head> <body style=\"font-size:12px;\"> <table id=\"mainStructure\" style=\"background-color:#cccccc;\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- START TAB TOP --> <tr> <td valign=\"top\"> <table style=\"background-color: rgb(204, 204, 204);\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td height=\"6\" valign=\"top\"> <table class=\"full-width\" style=\"height:6px;\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td style=\"font-size: 11px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:center;\"> <span style=\"color: #626262; font-weight: normal;\"> Si no puedes ver este mensaje correctamente <span style=\"color: rgb(30, 194, 229); font-weight: normal;\"><a href=\"http://broxel.com/comunicados/edocuenta/bxedocta.html\" style=\"text-decoration: none; color: #1598B4; font-weight: bold;\">Da click aquí</a></span> </span> </td> </tr> <!-- start space height --> <tr> <td height=\"5\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END TAB TOP --> <!--START TOP NAVIGATION ​LAYOUT--> <tr> <td valign=\"top\"> <table style=\"background-color: rgb(204, 204, 204);\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <!-- START TAB TOP --> <tr> <td class=\"fix-box\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td height=\"6\" valign=\"top\"> <table class=\"full-width\" style=\"height:6px;\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td class=\"full-width\" style=\"background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <table class=\"clear-align\" style=\"height: 6px; background-color: rgb(0, 194, 231);\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"282\"> <tbody><tr> <td height=\"6\" valign=\"top\"></td> </tr> </tbody></table> </td> <td class=\"remove-479\" style=\"width: 100%; background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <table class=\"clear-align\" style=\"height: 6px; background-color: rgb(0, 194, 231);\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td height=\"6\" valign=\"top\"></td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END TAB TOP --> <!-- START CONTAINER NAVIGATION --> <tr> <td class=\"fix-box\" align=\"center\" valign=\"top\"> <!-- start top navigation container --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start top navigaton --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <table class=\"container2\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <a style=\"text-decoration: none;\" href=\"http://online.broxel.com/Login.aspx\"><img src=\"http://online.broxel.com/Images/Comunicados/img/logo1.png\" style=\"max-width: 310px; background-color: rgb(30, 194, 229);\" alt=\"Logo\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"310\"></a> </td> <td class=\"remove-479\" valign=\"mindle\">&nbsp;</td> </tr> </tbody></table> <!--start content nav --> <table class=\"container2\" align=\"right\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td class=\"remove-479\" height=\"12\" valign=\"top\"></td> </tr> <tr> <td height=\"20\" valign=\"top\"></td> </tr> <!--start call us --> <tr> <td align=\"center\" valign=\"mindle\"> <table class=\"clear-align\" align=\"right\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody> <tr> <td style=\"font-size: 13px; line-height: 18px; color: #555555; font-weight:bold; text-align: center; font-family:Arail,Tahoma, Helvetica, Arial, sans-serif;\"> <span style=\"color: rgb(30, 194, 229);\">Estimad@</span>[USUARIO]</td> </tr> </tbody></table> </td> <!--start space width --> <td class=\"remove-479\" align=\"center\" valign=\"top\"> <table style=\"height:5px;\" align=\"right\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"20\"> <tbody><tr> <td valign=\"top\"> </td> </tr> </tbody></table> </td> <!--start space width --> </tr> <!--end call us --> <tr> <td height=\"20\" valign=\"top\"></td> </tr> <tr> <td class=\"remove-479\" height=\"11\" valign=\"top\"></td> </tr> </tbody></table> <!--end content nav --> </td> </tr> </tbody></table> <!-- end top navigaton --> </td> </tr> </tbody></table> <!-- end top navigation container --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"http://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- END CONTAINER NAVIGATION --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!--END TOP NAVIGATION ​LAYOUT--> <!--START IMAGE HEADER LAYOUT--> <!--END IMAGE HEADER LAYOUT--> <!-- START LAYOUT 1 --> <!-- END LAYOUT 1 --> <!-- START LAYOUT-5 --> <!-- END LAYOUT-5 --> <!-- START LAYOUT-5 --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <!-- start layout-5 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-5 container width 560px --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <!-- start image content --> <tr> <td valign=\"top\" width=\"100%\"> <!-- start content right --> <table width=\"210\" border=\"0\" align=\"right\" cellpadding=\"0\" cellspacing=\"0\" class=\"full-width\"> <tbody><tr> <td valign=\"top\"> <table align=\"right\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td align=\"right\" valign=\"top\"> <a style=\"text-decoration: none;\" href=\"http://online.broxel.com/Login.aspx\"> <img src=\"http://online.broxel.com/Images/Comunicados/img/Img-zoom3.png\" alt=\"img/Img-zoom3.png\" width=\"210\" height=\"335\" hspace=\"0\" vspace=\"0\" border=\"0\" class=\"image-100-percent\" style=\"display:block; max-width:210px; \"> </a> </td> </tr> </tbody></table> </td> </tr> <!--start space height --> <tr> <td height=\"0\"></td> </tr> <!--end space height --> </tbody></table> <!-- end content right --> <!-- start space width --> <table class=\"remove\" style=\"font-size: 0;line-height: 0;border-collapse: collapse;\" align=\"right\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"1\"> <tbody><tr> <td style=\"font-size: 0;line-height: 0;border-collapse: collapse;\" height=\"2\" width=\"0\"> <p style=\"padding-left: 20px;\">&nbsp;</p> </td> </tr> </tbody></table> <!-- end space width --> <!-- start content left --> <table class=\"full-width\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"300\"> <!-- start text content --> <tbody><tr> <td valign=\"top\"> <table width=\"330\" border=\"0\" align=\"left\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 18px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#555555; font-weight:bold; text-align:left;\"> <span style=\"color: #555555; font-weight: bold;\"> <a href=\"http://online.broxel.com\" style=\"text-decoration: none; color: #555555; font-weight: bold;\"><span style=\"color: rgb(0, 194, 231); font-weight: bold;\">El último Estado de Cuenta de su Tarjeta Broxel [TERMINACION] ya está disponible en Broxel Online</span></a> </span> </td> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 13px; line-height: 22px; font-family:Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:left; \"><p>Si no se ha dado de alta o no cuenta con su usuario y contraseña, solo ingrese a nuestra página <strong><a href=\"http://online.broxel.com\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">online.broxel.com</a></strong>, de click en la sección de <strong><a href=\"http://online.broxel.com/CreaTuCuenta.aspx\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">Nuevo Usuario, Ingresa aquí</a></strong> y llene los datos que ahí le solicitan.</p> <p>Por este medio hacemos de su conocimiento que cualquier modificación a su tarjeta, servicios o comisiones, le será notificada por correo electrónico y podrá tener acceso a dicha información a través de nuestros servicios online.</p> <p><a style=\"text-decoration: none; color: #00C2E7; font-weight: bold;\">Gracias por formar parte de nuestra comunidad de Servicios Online.</a><p></td> </tr> <tr> <td valign=\"top\"> <!-- start button --> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"background-color: rgb(0, 194, 231); border-radius: 3px 3px 3px 3px; background-clip: padding-box; font-size: 13px; font-family: Arial,Tahoma,Helvetica,sans-serif; text-align: center; color: rgb(255, 255, 255); font-weight: normal; padding-left: 18px; padding-right: 18px;\" align=\"center\" height=\"32\" valign=\"middle\" width=\"auto\"> <span style=\"color: #ffffff; font-weight: normal;\"> <a href=\"http://online.broxel.com\" style=\"text-decoration: none; color: #ffffff; font-weight: normal;\">Vea su Estado de Cuenta</a> </span> </td> </tr> </tbody></table> <!-- end button --> </td> </tr> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> </td> </tr> <!-- end text content --> </tbody></table> <!-- end content left --> </td> </tr> <!-- end image content --> </tbody></table> <!-- end layout-5 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-5 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"http://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT-5 --> <!-- START LAYOUT 6 --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td valign=\"top\"> <!-- start layout-6 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-6 container width 560px --> <table class=\"full-width\" style=\"background-color: #ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <!-- start image content --> <tbody><tr> <td valign=\"top\" width=\"100%\"> <!-- start content left --> <table class=\"full-width\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td align=\"left\" valign=\"top\"> <a style=\"text-decoration: none;\" href=\"#\"> <img src=\"http://online.broxel.com/Images/Comunicados/img/cc.png\" alt=\"face\" style=\"display:block; max-width:125px; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"125\"> </a> </td> <!-- space width --> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"20\"> <tbody><tr> <td valign=\"top\"></td> </tr> </tbody></table> </td> <!-- space width --> <!-- start text content --> <td valign=\"top\"> <table class=\"width-small\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start text content --> <tbody><tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 18px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#555555; font-weight:bold; text-align:left;\"> <span style=\"color: #555555; font-weight: bold;\"> <a href=\"#\" style=\"text-decoration: none; color: #555555; font-weight: bold;\">Servicio al Cliente</a> </span> </td> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 13px; line-height: 22px; font-family:Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:left; \"> Para Broxel lo más importante es ofrecerle un servicio oportuno y de calidad. Si tiene cualquier duda sobre alguno de estos servicios, solo llame al <strong><a style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">01800-Broxel-1</a></strong> (01800-276935-1) y le resolveremos cualquier duda.</p></td> </tr> </tbody></table> </td> </tr> <!-- end text content --> </tbody></table> </td> <!-- end text content --> </tr> </tbody></table> </td> </tr> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> <!-- end content left --> </td> </tr> <!-- end image content --> </tbody></table> <!-- end layout-6 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-6 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"http://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT 6 --> <!-- START LAYOUT 67 --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <!-- start layout-1 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-1 container width 560px --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <!--start space height --> <tbody><tr> <td height=\"16\"></td> </tr> <!--end space height --> <!-- start text content --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody> <!--start space height --> <tr> <td height=\"0\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 11px; line-height: 16px; font-family: Arial,Tahoma, Helvetica, sans-serif; color: #a3a2a2; font-weight: normal; text-align: center;\"><p><a href=\"http://www.broxel.com/index.php/aviso-privacidad-sapi\" style=\"text-decoration: none; color: #555555; font-weight: bold;\"><span style=\"color: rgb(0, 194, 231); font-weight: bold;\">Aviso de Privacidad</span></a></p> <p> Sus datos se encuentran protegidos a través de nuestro Aviso de Privacidad.<br>Consulte nuestro Aviso de Privacidad en: <a href=\"http://www.broxel.com/index.php/aviso-privacidad-sapi\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">www.broxel.com</a>.</p> <p> Este correo electrónico está destinado a residentes de México y fue enviado desde Broxel.<br/> Si ha recibido este correo de una dirección diferente, significa que fue reenviado.</p> <p>Las comunicaciones vía correo electrónico de Broxel se basan en el manejo de cookies y/o Web Beacons.</p> <p>Al recibir este correo y consultar el contenido del mismo, usted consiente que Broxel recabe y tenga <br/> acceso a sus datos personales.</p> <p>Para más información favor de consultar <strong><a href=\"http://www.broxel.com/index.php/terminos-condiciones\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">Términos y Condiciones.</a></strong></p></td> </tr> <tr> <td align=\"center\" valign=\"top\" width=\"auto\"> </td> </tr> </tbody></table> </td> </tr> <!-- end text content --> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> <!-- end layout-1 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-1 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"http://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT 67 --> <!-- START LAYOUT 7--> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <!-- start layout-7 container --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td valign=\"top\"> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <tr> <td valign=\"top\"> <!-- start logo footer and address --> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td valign=\"top\"> <!--start icon socail navigation --> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td align=\"left\" valign=\"top\"> <table class=\"container\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://www.facebook.com/BroxelSF\"> <img src=\"http://online.broxel.com/Images/Comunicados/img/icon-facebook.jpg\" alt=\"icon-facebook\" style=\"max-width:33px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> <td style=\"padding-left:5px; \" class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://twitter.com/BroxelSF\"> <img src=\"http://online.broxel.com/Images/Comunicados/img/icon-twitter.jpg\" alt=\"icon-twitter\" style=\"max-width:30px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> <td style=\"padding-left:5px; \" class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://plus.google.com/u/0/116529217974857845752/posts\"> <img src=\"http://online.broxel.com/Images/Comunicados/img/icon-googleplus.jpg\" alt=\"icon-googleplus\" style=\"max-width:30px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> </tr> </tbody></table> </td> </tr> </tbody></table> <!--end icon socail navigation --> </td> </tr> </tbody></table> <!-- end logo footer and address --> </td> </tr> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"http://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> <!-- end layout-7 container --> </td> </tr> <!-- END LAYOUT-7--> <!-- START FOOTER COPY RIGHT --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <!-- start layout-7 container --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <tbody><tr> <!-- start unsubscribe content --> <td valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 13px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:center;\" class=\"text-center\" align=\"center\"> Síganos en facebook, twitter y google +<br /> Visitenos en <a href=\"http://broxel.com\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">www.broxel.com</a> para conocer más de nuestros productos.</td> </tr> </tbody></table> </td> <!-- end unsubscribe content --> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <!-- start COPY RIGHT content --> <td style=\"font-size: 11px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:center;\" valign=\"top\">© 2014 BROXEL | Servicios Financieros. Todos los derechos Reservados.</td> <!-- end COPY RIGHT content --> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END FOOTER COPY RIGHT --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> </table></body> </html>";
                html = html.Replace("[USUARIO]", " " + correo.nombre + " ");
                html = html.Replace("[TERMINACION]", " " + correo.terminacion + " ");
                SendMail("noreply@broxel.com", correo.mail, "Estado de cuenta Broxel Online", html, "Broxel.2018​", "Broxel Online");
            }
            SendMail("broxelonline@broxel.com", "rodrigo.diazdeleon@broxel.com, mauricio.lopez@broxel.com", "Envio de correos terminado", "Se terminaron de mandar los correos de estados de cuenta para el grupo " + grupoDeLiquidacion + " para el corte de  : " + fechaUltimaLiquidacion.ToString("D"), "67896789", "Broxel Online");
        }

        #endregion

        #region OnlineBroxel

        #region CifradoWeb

        public static string Cifrar(string texto)
        {
            var Arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(texto);
            var hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();
            var tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            var cTransform = tdes.CreateEncryptor();
            var ArrayResultado = cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length);
            tdes.Clear();
            return Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);
        }

        public static string ToBase64(string toConvert)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(toConvert));
        }

        public static string DesCifrar(string textoEncriptado)
        {
            byte[] keyArray;
            var Array_a_Descifrar = Convert.FromBase64String(textoEncriptado);
            var hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();
            var tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            var cTransform = tdes.CreateDecryptor();
            var resultArray = cTransform.TransformFinalBlock(Array_a_Descifrar, 0, Array_a_Descifrar.Length);
            tdes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        #endregion

        #region Timbrado

        internal static void TimbrarEdosCuenta(string grupoDeLiquidacion, DateTime fechaUltimaLiquidacion)
        {
            var broxelCo = new broxelco_rdgEntities();
            var wsTimbrado = new EfacturaServiceClient();

            var edosCuenta = (from e in broxelCo.vw_EdoCuenta
                              join m in broxelCo.maquila on e.numero_de_cuenta equals m.num_cuenta
                              where
                                  m.RFC != String.Empty && e.timbrado != "1" && e.fecha_ultima_liquidacion == fechaUltimaLiquidacion &&
                                  e.grupo_de_liquidacion == grupoDeLiquidacion
                              select new { e, m }).ToList();

            var estados = (from e in broxelCo.estadosCredencial select e).ToList();
            var NumCuenta = "";

            foreach (var edoCuenta in edosCuenta)
            {
                var accessoCliente = broxelCo.accessos_clientes.FirstOrDefault(x => x.IdMaquila == edoCuenta.m.id);
                NumCuenta = edoCuenta.e.numero_de_cuenta;
                var movs =
                broxelCo.ind_movimientos.Where(
                    x => x.NroRuc == NumCuenta && ((DateTime)(x.Fliqusu)) == fechaUltimaLiquidacion)
                    .OrderBy(x => x.Origen)
                    .ThenBy(x => x.NroAut)
                    .ThenByDescending(x => x.ImpTotalLiqusu)
                    .ToList();
                var traslados = new List<traslado>();
                var timbre = String.Empty;

                var regCuenta =
                    broxelCo.vw_EdoCuenta.FirstOrDefault(
                        x => x.fecha_ultima_liquidacion == fechaUltimaLiquidacion && x.numero_de_cuenta == NumCuenta);

                var conceptosMovimientos = (from movimiento in movs
                                            where movimiento.DenMov.Contains("ATM") || movimiento.DenMov.Contains("RENOVACION") || movimiento.DenMov.Contains("ANUALIDAD") || movimiento.DenMov.Contains("COMISION POR DISPOSICION")
                                            select new Concepto
                                            {
                                                Nombre = movimiento.DenMov,
                                                ValorUnitario = Convert.ToDouble(movimiento.ImpTotalLiqusu)
                                            }).ToList();

                var maq = edoCuenta.m;
                if (regCuenta.importe_intereses_punit > 0)
                    conceptosMovimientos.Add(new Concepto
                    {
                        Nombre = "Intereses Moratorios",
                        ValorUnitario = Convert.ToDouble(regCuenta.importe_intereses_punit)
                    });
                if (regCuenta.importe_intereses_finan > 0)
                    conceptosMovimientos.Add(new Concepto
                    {
                        Nombre = "Intereses Ordinarios",
                        ValorUnitario = Convert.ToDouble(regCuenta.importe_intereses_finan)
                    });
                var conc =
                    conceptosMovimientos.GroupBy(x => new { x.Nombre, x.ValorUnitario })
                        .Select(p => new { p.Key.Nombre, p.Key.ValorUnitario, Cuantos = p.Count() }).ToList();

                var conceptos = conc.Select(concepto => new concepto
                {
                    cantidad = concepto.Cuantos.ToString(),
                    descripcion = concepto.Nombre,
                    importe = (concepto.Cuantos * concepto.ValorUnitario).ToString(),
                    noIdentificacion = "",
                    unidad = "SERVICIO",
                    valorUnitario = concepto.ValorUnitario.ToString(),
                }).ToList();

                traslados.Add(new traslado
                {
                    importe = regCuenta.importe_retencion_iva.ToString(),

                    impuesto = "IVA",
                    tasa = "16.00",
                });
                var comprobante = new comprobanteCFDIv32
                {
                    claveCFDI = "FAC",
                    conceptos = conceptos.ToArray(),
                    condicionesDePago = "",
                    descuento = "",
                    fechaFolioFiscalOrig = "",
                    folio = "",
                    folioFiscalOrig = "",
                    formaDePago = "Pago en una sola exhibición",
                    impuesto = new impuesto
                    {
                        //retenciones = retenciones.ToArray(),
                        totalImpuestosRetenidos = "0",
                        traslados = traslados.ToArray(),
                        totalImpuestosTrasladados = regCuenta.importe_retencion_iva.ToString()
                    },
                    lugarExpedicion = "MEXICO D.F.",
                    metodoDePago = "No identificado",
                    moneda = "MXN",
                    numCtaPago = "",
                    etiquetaPersonalizada = new etiquetaPersonalizada
                    {
                        nombre = "",
                        valor = "",
                    },
                    receptor = new receptor
                    {
                        calle = maq.Calle, //
                        codigoPostal = maq.codigo_postal, //
                        colonia = maq.Colonia, //
                        emailContacto = accessoCliente.Email, // El email del contacto o del cliente. A este email se enviará el CFDI en caso de que así se requiera.
                        envioPDF = "0", //"Cero no Envia PDF  al emailcontacto por correo electronico. Uno si Envia PDF  al emailcontacto por correo electronico."
                        envioXML = "0", //"Cero no Envia  XML  al emailcontacto por correo electronico. Uno si Envia  XML  al emailcontacto por correo electronico."
                        estado =
                            (from e in estados where e.LetraEstado == regCuenta.codigo_provincia select e.Estado)
                                .SingleOrDefault(),
                        localidad = maq.localidad, //
                        municipio = maq.provincia, //
                        noExterior = maq.NumExterior, //
                        noInterior = maq.NumInterior, //
                        nombre = maq.NombreCompleto, //
                        //La información que se proporcione sobre el receptor será actualizada al instante siempre que el cliente mencionado ya exista en la base de datos, 
                        //en caso contrario será dado de alta como nuevo cliente. Esto a nivel interno de su cuenta en FEL.
                        pais = "México",
                        rfc = maq.RFC
                    },
                    regimenFiscal = new regimenFiscal
                    {
                        regimen = "REGIMEN GENERAL DE LEY DE PERSONAS MORALES"
                    },
                    serie = "",
                    serieFolioFiscalOrig = "",
                    tipoDeCambio = "",
                    subTotal = (conceptos.Sum(x => Convert.ToDouble(x.importe))).ToString(),
                    total = (regCuenta.importe_movimientos_no_finan - regCuenta.importe_retencion_iva).ToString(),
                };
                var resp = wsTimbrado.emitirCFDIv32(comprobante, new credenciales
                {
                    password = "123456",
                    rfc = "MAPJ660524JC1",
                    usuario = "ppm@praxis.com.mx",
                });
                if (resp.StartsWith("|0,ComprobanteXML,"))
                {
                    var lgrc =
                        broxelCo.log_registro_de_cuenta.FirstOrDefault(
                            x =>
                                x.numero_de_cuenta == NumCuenta &&
                                x.fecha_ultima_liquidacion == fechaUltimaLiquidacion);
                    timbre = resp.Remove(0, 18);
                    timbre = timbre.Remove(timbre.Length - 1);
                    lgrc.timbrado = "1";
                    lgrc.timbre = timbre;
                    broxelCo.SaveChanges();
                }
            }
        }

        #endregion

        #endregion

        private static void EnviarMailReversoDispersion(string emailNotificar, string folio, decimal totalPOS, decimal totalATM, string estado, int cuantas, string cliente, List<ErroresDispersion> erroresDispersion, decimal posSolicitado, decimal atmSolicitado)
        {
            var dbHelper = new broxelco_rdgEntities();
            int cuentasConError = 0;
            var From = "dispersiones@broxel.com";
            var nombreCliente = dbHelper.clientesBroxel.Where(x => x.claveCliente == cliente).ToList().First().razonSocial;
            var Subject = String.Format("Solicitud de reverso terminada - cliente : {0}", cliente);
            var sb = new StringBuilder();
            var htmlIni =
                "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"> <html xmlns=\"http://www.w3.org/1999/xhtml\"> <head> <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" /> <meta name=\"viewport\" content=\"initial-scale=1.0\"/> <meta name=\"format-detection\" content=\"telephone=no\"/> <title>Broxel Proceso de Reverso de Asignación</title> <style type=\"text/css\"> /* Resets: see reset.css for details */ .ReadMsgBody { width: 100%; background-color: #ffffff;} .ExternalClass {width: 100%; background-color: #ffffff;} .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {line-height:100%;} html{width: 100%; } body {-webkit-text-size-adjust:none; -ms-text-size-adjust:none; } body {margin:0; padding:0;} table {border-spacing:0;} img{display:block !important;} table td {border-collapse:collapse;} .yshortcuts a {border-bottom: none !important;} /* main color = #f05e5e other color = #b91925 background color = #ececec */ img{height:auto !important;} @media only screen and (max-width: 640px){ body{ width:auto!important; } table[class=\"container\"]{ width: 100%!important; padding-left: 20px!important; padding-right: 20px!important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; } img[class=\"small-image-100-percent\"]{ width:100% !important; height:auto !important; } table[class=\"full-width\"]{ width:100% !important; } table[class=\"full-width-text\"]{ width:100% !important; background-color:#b91925; padding-left:20px !important; padding-right:20px !important; } table[class=\"full-width-text2\"]{ width:100% !important; background-color:#f3f3f3; padding-left:20px !important; padding-right:20px !important; } table[class=\"col-2-3img\"]{ width:50% !important; margin-right: 20px !important; } table[class=\"col-2-3img-last\"]{ width:50% !important; } table[class=\"col-2\"]{ width:47% !important; margin-right:20px !important; } table[class=\"col-2-last\"]{ width:47% !important; } table[class=\"col-3\"]{ width:29% !important; margin-right:20px !important; } table[class=\"col-3-last\"]{ width:29% !important; } table[class=\"row-2\"]{ width:50% !important; } td[class=\"text-center\"]{ text-align: center !important; } /* start clear and remove*/ table[class=\"remove\"]{ display:none !important; } td[class=\"remove\"]{ display:none !important; } /* end clear and remove*/ table[class=\"fix-box\"]{ padding-left:20px !important; padding-right:20px !important; } td[class=\"fix-box\"]{ padding-left:20px !important; padding-right:20px !important; } td[class=\"font-resize\"]{ font-size: 18px !important; line-height: 22px !important; } } @media only screen and (max-width: 479px){ body{ font-size:10px !important; } table[class=\"container2\"]{ width: 100%!important; float:none !important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } img[class=\"small-image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } table[class=\"full-width\"]{ width:100% !important; } table[class=\"full-width-text\"]{ width:100% !important; background-color:#b91925; padding-left:20px !important; padding-right:20px !important; } table[class=\"full-width-text2\"]{ width:100% !important; background-color:#f3f3f3; padding-left:20px !important; padding-right:20px !important; } table[class=\"col-2\"]{ width:100% !important; margin-right:0px !important; } table[class=\"col-2-last\"]{ width:100% !important; } table[class=\"col-3\"]{ width:100% !important; margin-right:0px !important; } table[class=\"col-3-last\"]{ width:100% !important; } table[class=\"row-2\"]{ width:100% !important; } table[id=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } td[id=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } td[class=\"col-underline\"]{ float: none !important; width: 100% !important; border-bottom: 1px solid #eee; } /*start text center*/ td[class=\"text-center\"]{ text-align: center !important; } div[class=\"text-center\"]{ text-align: center !important; } /*end text center*/ /* start clear and remove */ table[id=\"clear-padding\"]{ padding:0 !important; } td[id=\"clear-padding\"]{ padding:0 !important; } td[class=\"clear-padding\"]{ padding:0 !important; } table[class=\"remove-479\"]{ display:none !important; } td[class=\"remove-479\"]{ display:none !important; } table[class=\"clear-align\"]{ float:none !important; } /* end clear and remove */ table[class=\"width-small\"]{ width:100% !important; } table[class=\"fix-box\"]{ padding-left:0px !important; padding-right:0px !important; } td[class=\"fix-box\"]{ padding-left:0px !important; padding-right:0px !important; } td[class=\"font-resize\"]{ font-size: 14px !important; } } @media only screen and (max-width: 320px){ table[class=\"width-small\"]{ width:125px !important; } img[class=\"image-100-percent\"]{ width:100% !important; height:auto !important; max-width:100% !important; min-width:124px !important; } } </style> </head> <body style=\"font-size:12px;\"> <table id=\"mainStructure\" style=\"background-color:#cccccc;\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- START TAB TOP --> <tr> <td valign=\"top\"> <table style=\"background-color: rgb(204, 204, 204);\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td height=\"6\" valign=\"top\"> <table class=\"full-width\" style=\"height:6px;\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> </tr> <!-- start space height --> <tr> <td height=\"5\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END TAB TOP --> <!--START TOP NAVIGATION ​LAYOUT--> <tr> <td valign=\"top\"> <table style=\"background-color: rgb(204, 204, 204);\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <!-- START TAB TOP --> <tr> <td class=\"fix-box\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td height=\"6\" valign=\"top\"> <table class=\"full-width\" style=\"height:6px;\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td class=\"full-width\" style=\"background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <table class=\"clear-align\" style=\"height: 6px; background-color: rgb(0, 194, 231);\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"282\"> <tbody><tr> <td height=\"6\" valign=\"top\"></td> </tr> </tbody></table> </td> <td class=\"remove-479\" style=\"width: 100%; background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <table class=\"clear-align\" style=\"height: 6px; background-color: rgb(0, 194, 231);\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td height=\"6\" valign=\"top\"></td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END TAB TOP --> <!-- START CONTAINER NAVIGATION --> <tr> <td class=\"fix-box\" align=\"center\" valign=\"top\"> <!-- start top navigation container --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start top navigaton --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <table class=\"container2\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"background-color: rgb(0, 194, 231);\" align=\"center\" valign=\"top\"> <a style=\"text-decoration: none;\" href=\"#\"><img src=\"https://online.broxel.com/Images/Comunicados/img/logo2.png\" style=\"max-width: 310px; background-color: rgb(30, 194, 229);\" alt=\"Logo\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"293\"></a> </td> </tr> </tbody></table> <!--start content nav --> <table class=\"container2\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td class=\"remove-479\" height=\"13\" valign=\"top\"></td> </tr> <!--start call us --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 13px; line-height: 18px; color: #555555; font-weight:bold; text-align: center; font-family:Arail,Tahoma, Helvetica, Arial, sans-serif;\"><div align=\"left\"><span style=\"color: rgb(30, 194, 229);\">Estimad@</span><br/>[CLIENTE]</div></td> </tr> </tbody></table> </td> <!--start space width --> <!--start space width --> </tr> <!--end call us --> <tr> <td height=\"14\" valign=\"top\"></td> </tr> </tbody></table> <!--end content nav --> </td> </tr> </tbody></table> <!-- end top navigaton --> </td> </tr> </tbody></table> <!-- end top navigation container --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- END CONTAINER NAVIGATION --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!--END TOP NAVIGATION ​LAYOUT--> <!-- START LAYOUT 1 --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <!-- start layout-1 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-1 container width 560px --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <!-- start text content --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 18px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#555555; font-weight:bold; text-align:center;\"><div align=\"left\"><span style=\"color: rgb(0, 194, 231); font-weight:bold;\"> Proceso de asignación de línea terminado </span></div></td> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 13px; line-height: 22px; font-family:Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:left; \"><p>Le informamos que su solicitud de reverso de asignación de línea, con el número de folio <strong><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">[FOLIO]</a></strong><br/> arrojó los siguientes resultados:</p></td> </tr> <tr> <td valign=\"top\"> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <!--start Tabla Cuentas --> <tr> <td height=\"15\"><table width=\"420\" Table Border=\"1\" Bordercolor=#ffffff cellspacing=\"2\" cellpadding=\"2\"> <tr> <td width=\"370\" align=\"left\" bgcolor=\"#f2f2f2\" scope=\"row\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\"><p style=\"margin: 0; padding-left: 8px;\">Total de cuentas con reverso solicitadas:</a></td> <td width=\"100\" align=\"center\" bgcolor=\"#e6e6e6\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\">[SOLICITADAS]</a></td> </tr> <tr> <th align=\"left\" bgcolor=\"#f2f2f2\" scope=\"row\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\"><p style=\"margin: 0; padding-left: 8px;\">Total de cuentas reversadas :</a></th> <td align=\"center\" bgcolor=\"#e6e6e6\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\">[BIEN]</a></td> </tr> </table></td> </tr> <!--end Tabla Cuentas --> <tr></tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <!--start Tabla Monto y Total --> <tr> <td height=\"15\"><table width=\"420\" Table Border=\"1\" Bordercolor=#ffffff cellspacing=\"2\" cellpadding=\"2\"> <tr> <th width=\"370\" align=\"left\" bgcolor=\"#f2f2f2\" scope=\"row\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\"><p style=\"margin: 0; padding-left: 8px;\">Reverso POS Solicitado</a></th> <td width=\"100\" align=\"center\" bgcolor=\"#e6e6e6\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\">[POSSOLICITADO]</a></td> </tr> <tr> <th align=\"left\" bgcolor=\"#f2f2f2\" scope=\"row\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\"><p style=\"margin: 0; padding-left: 8px;\"> Reverso ATM Solicitado</a></th> <td align=\"center\" bgcolor=\"#e6e6e6\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\">[ATMSOLICITADO]</a></td> </tr> <tr> <th align=\"left\" bgcolor=\"#f2f2f2\" scope=\"row\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\"><p style=\"margin: 0; padding-left: 8px;\">Total POS Reversado</a></th> <td align=\"center\" bgcolor=\"#e6e6e6\"><a style=\"font-size: 13px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\">[POSREAL]</a></td> </tr> <tr> <th align=\"left\" bgcolor=\"#f2f2f2\" scope=\"row\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\"><p style=\"margin: 0; padding-left: 8px;\">Total ATM Reversado</a></th> <td align=\"center\" bgcolor=\"#e6e6e6\"><a style=\"font-size: 13px; line-height: 26px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#000000; font-weight:bold;\">[ATMREAL]</a></td> </tr> </table> </td> </tr> <!--end Tabla Monto y Total --> <tr></tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <td valign=\"top\"> </td> </tr> <tr> <td style=\"font-size: 13px; line-height: 22px; font-family:Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:left; \"><p><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">";
            htmlIni = htmlIni.Replace("[CLIENTE]", nombreCliente);
            htmlIni = htmlIni.Replace("[FOLIO]", folio);
            htmlIni = htmlIni.Replace("[SOLICITADAS]", cuantas.ToString());
            htmlIni = htmlIni.Replace("[POSSOLICITADO]", PrintCurrency(posSolicitado));
            htmlIni = htmlIni.Replace("[ATMSOLICITADO]", PrintCurrency(atmSolicitado));
            htmlIni = htmlIni.Replace("[POSREAL]", PrintCurrency(totalPOS));
            htmlIni = htmlIni.Replace("[ATMREAL]", PrintCurrency(totalATM));

            sb.Append(htmlIni);
            if (estado != "COMPLETA")
            {
                var listaDescripcionError = dbHelper.CodigosRespuesta.ToList();

                foreach (var error in erroresDispersion)
                {
                    var descripcionError = listaDescripcionError.FirstOrDefault(x => x.Id == error.CodigoRespuesta).Descripcion;

                    sb.AppendFormat("<a style=\"text-decoration: none; color:#a3a2a2; font-weight: normal;\">Las siguientes </a></strong><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">{0}</a></strong><a style=\"text-decoration: none; color:#a3a2a2; font-weight: normal;\"> cuentas no pudieron ser procesadas por código </a><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">{1}.</a></strong></strong><br/>", error.CuentasConError.Count, descripcionError);
                    foreach (var cuenta in error.CuentasConError)
                    {
                        sb.AppendFormat("<br/>{0}", cuenta);
                        cuentasConError++;
                    }
                }
            }

            sb = sb.Replace("[BIEN]", (cuantas - cuentasConError).ToString());
            sb.Append("<br><br>¿Dudas? </a>comuníquese con su ejecutivo de cuenta Broxel al <a style=\"text-decoration: none; color: #00C2E7; font-weight: bold;\"> teléfono 4433-0303. </a><br/><a style=\"text-decoration: none; color: #555555; font-weight: bold;\">Gracias por utilizar nuestra plataforma Broxel Corporate Access.</a></td> </tr> <tr> <td align=\"center\" valign=\"top\" width=\"auto\"> </td> </tr> </tbody></table> </td> </tr> <!-- end text content --> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> <!-- end layout-1 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-1 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT 1 --> <!-- START LAYOUT Servicio al Cliente --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td valign=\"top\"> <!-- start layout-6 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-6 container width 560px --> <table class=\"full-width\" style=\"background-color: #ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <!-- start image content --> <tbody><tr> <td valign=\"top\" width=\"100%\"> <!-- start content left --> <table class=\"full-width\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td align=\"left\" valign=\"top\"> <a style=\"text-decoration: none;\" href=\"#\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-tel.png\" alt=\"tel\" width=\"127\" height=\"127\" hspace=\"0\" vspace=\"0\" border=\"0\" style=\"display:block; max-width:125px; \"> </a> </td> <!-- space width --> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"20\"> <tbody><tr> <td valign=\"top\"></td> </tr> </tbody></table> </td> <!-- space width --> <!-- start text content --> <td valign=\"top\"> <table class=\"width-small\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start text content --> <tbody><tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 18px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#555555; font-weight:bold; text-align:left;\"> <span style=\"color: #555555; font-weight: bold;\"> <a href=\"#\" style=\"text-decoration: none; color: #555555; font-weight: bold;\">Servicio al Cliente</a> </span> </td> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 13px; line-height: 22px; font-family:Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:left; \"> Para Broxel lo más importante es ofrecerle un servicio oportuno y de calidad. Si tiene cualquier duda sobre alguno de estos servicios, solo llame al <strong><a style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">01800-Broxel-1</a></strong> (01800-276935-1) y un ejecutivo con gusto le ayudará.</td> </tr> </tbody></table> </td> </tr> <!-- end text content --> </tbody></table> </td> <!-- end text content --> </tr> </tbody></table> </td> </tr> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> <!-- end content left --> </td> </tr> <!-- end image content --> </tbody></table> <!-- end layout-6 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-6 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT Servicio al Cliente --> <!-- START LAYOUT Aviso Privacidad --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <!-- start layout-1 container width 600px --> <table class=\"container\" style=\"background-color: #ffffff; \" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <!-- start layout-1 container width 560px --> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"509\"> <!--start space height --> <tbody><tr> <td width=\"509\" height=\"16\"></td> </tr> <!--end space height --> <!-- start text content --> <tr> <td valign=\"top\"> <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody> <!--start space height --> <tr> <td height=\"0\"></td> </tr> <!--end space height --> <tr> <td style=\"font-size: 11px; line-height: 16px; font-family: Arial,Tahoma, Helvetica, sans-serif; color: #a3a2a2; font-weight: normal; text-align: center;\"><p><a href=\"http://www.broxel.com/index.php/aviso-privacidad-sapi\" style=\"text-decoration: none; color: #555555; font-weight: bold;\"><span style=\"color: rgb(0, 194, 231); font-weight: bold;\">Aviso de Privacidad</span></a></p> <p> Sus datos se encuentran protegidos a través de nuestro Aviso de Privacidad.<br>Consulte nuestro Aviso de Privacidad en: <a href=\"http://www.broxel.com/index.php/aviso-privacidad-sapi\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">www.broxel.com</a>.</p> <p> Este correo electrónico está destinado a residentes de México y fue enviado desde Broxel.<br/> Si ha recibido este correo de una dirección diferente, significa que fue reenviado.</p> <p>Las comunicaciones vía correo electrónico de Broxel se basan en el manejo de cookies<br/> y/o Web Beacons.</p> <p>Al recibir este correo y consultar el contenido del mismo, usted consiente que Broxel recabe y tenga acceso a sus datos personales.</p> <p>Para más información favor de consultar <strong><a href=\"http://www.broxel.com/index.php/terminos-condiciones\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">Términos y Condiciones.</a></strong></p></td> </tr> <tr> <td align=\"center\" valign=\"top\" width=\"auto\"> </td> </tr> </tbody></table> </td> </tr> <!-- end text content --> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> <!-- end layout-1 container width 560px --> </td> </tr> </tbody></table> <!-- end layout-1 container width 600px --> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> <!-- END LAYOUT 67 --> <!-- START LAYOUT 7--> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <!-- start layout-7 container --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td valign=\"top\"> <table class=\"full-width\" style=\"background-color:#ffffff;\" align=\"center\" bgcolor=\"#ffffff\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <!--start space height --> <tbody><tr> <td height=\"20\"></td> </tr> <!--end space height --> <tr> <td valign=\"top\"> <!-- start logo footer and address --> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tbody><tr> <td valign=\"top\"> <!--start icon socail navigation --> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td align=\"left\" valign=\"top\"> <table class=\"container\" align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://www.facebook.com/BroxelSF\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-facebook.jpg\" alt=\"icon-facebook\" style=\"max-width:33px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> <td style=\"padding-left:5px; \" class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://twitter.com/BroxelSF\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-twitter.jpg\" alt=\"icon-twitter\" style=\"max-width:30px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> <td style=\"padding-left:5px; \" class=\"clear-padding\" align=\"center\" height=\"30\" valign=\"middle\"> <a style=\"text-decoration: none;\" href=\"https://plus.google.com/u/0/116529217974857845752/posts\"> <img src=\"https://online.broxel.com/Images/Comunicados/img/icon-googleplus.jpg\" alt=\"icon-googleplus\" style=\"max-width:30px;\" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"30\"> </a> </td> </tr> </tbody></table> </td> </tr> </tbody></table> <!--end icon socail navigation --> </td> </tr> </tbody></table> <!-- end logo footer and address --> </td> </tr> <!--start space height --> <tr> <td height=\"20\"></td> </tr> <!--end space height --> </tbody></table> </td> </tr> <!-- start shadow --> <tr> <td align=\"center\" valign=\"top\" width=\"100%\"> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td align=\"center\" valign=\"top\"> <img class=\"image-100-percent\" src=\"https://online.broxel.com/Images/Comunicados/img/shadow.png\" alt=\"shadow\" style=\"max-width:596px display:block; \" border=\"0\" hspace=\"0\" vspace=\"0\" width=\"596\"> </td> </tr> </tbody></table> </td> </tr> <!-- end shadow --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> <!-- end layout-7 container --> </td> </tr> <!-- END LAYOUT-7--> <!-- START FOOTER COPY RIGHT --> <tr> <td class=\"fix-box\" style=\"background-color: rgb(204, 204, 204);\" align=\"center\" valign=\"top\"> <!-- start layout-7 container --> <table class=\"full-width\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <!-- start space height --> <tbody><tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> <tr> <td align=\"center\" valign=\"top\"> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\"> <tbody><tr> <td valign=\"top\"> <table class=\"container\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"560\"> <tbody><tr> <!-- start unsubscribe content --> <td valign=\"top\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody><tr> <td style=\"font-size: 13px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:center;\" class=\"text-center\" align=\"center\"> Síguanos por facebook, twitter y google +<br /> Visitenos en <a href=\"http://broxel.com\" style=\"text-decoration: none; color: rgb(0, 194, 231); font-weight: bold;\">www.broxel.com</a> para conocer más de nuestros productos.</td> </tr> </tbody></table> </td> <!-- end unsubscribe content --> </tr> <!--start space height --> <tr> <td height=\"15\"></td> </tr> <!--end space height --> <tr> <!-- start COPY RIGHT content --> <td style=\"font-size: 11px; line-height: 22px; font-family: Arial,Tahoma, Helvetica, sans-serif; color:#a3a2a2; font-weight:normal; text-align:center;\" valign=\"top\">© 2014 BROXEL | Servicios Financieros. Todos los derechos Reservados.</td> <!-- end COPY RIGHT content --> </tr> </tbody></table> </td> </tr> </tbody></table> </td> </tr> <!-- END FOOTER COPY RIGHT --> <!-- start space height --> <tr> <td height=\"10\" valign=\"top\"></td> </tr> <!-- end space height --> </tbody></table> </td> </tr> </table></body> </html>");
            SendMail(From, ConfigurationManager.AppSettings["emailsAvisosDispersiones"] + " , " + emailNotificar, Subject, sb.ToString(), "yMQ3E3ert6", "Broxel : Asignacion de Lineas");
        }

        internal static string TruncaTarjeta(string NumTarjeta)
        {
            return NumTarjeta.Substring(0, 6) + "** ****" + NumTarjeta.Substring(12);
        }

        internal static void CorrerTransferencia(string folio)
        {
            var dbHelper = new broxelco_rdgEntities();
            Tarjeta t;
            int POS = 0;
            var webService = new BroxelService();
            var count = 0;
            var esTransSaldoTotal = false;
            Decimal montoPorcentaje = 0;

            var dispersionSolicitud = dbHelper.TransferenciasSolicitud.Where(x => x.Folio == folio && x.Estado == "Validando").ToList();
            if (dispersionSolicitud.Count == 1 && dispersionSolicitud[0].Estado == "Validando")
            {
                dispersionSolicitud[0].Estado = "Ejecutando";
                dbHelper.SaveChanges();
                //JAVG //20150918 //Cambio solicitado por AGM: Verificar si se realiza la transferencia del saldo total del tipo OneToOne
                esTransSaldoTotal = dispersionSolicitud[0].TransfSaldoTotal;
                //JAVG //20150918 //Se quita la validación de Monto Mayor a 0
                var dispersionesa =
                    dbHelper.TransferenciasDetalle.Where(
                        x => x.FolioSolicitud == folio && (((x.CodigoRespuesta != "-1" || x.CodigoRespuesta == null)))).OrderBy(x => x.Id);
                var dispersiones = dispersionesa.ToList();

                if (dispersiones.Any() && dispersionSolicitud[0].Tipo == "ConcentradoraACuentas")
                {
                    Decimal porcentaje = dispersiones[0].PorcentajeSaldoTotal ?? 0;
                    if (porcentaje != 0)
                    {
                        montoPorcentaje = webService.GetSaldosPorCuenta(dispersiones[0].CuentaOrigen, "", "CorrerTransferencia").Saldos.DisponibleCompras;
                        montoPorcentaje *= (porcentaje / 100);
                    }
   
                }

                foreach (var dispersion in dispersiones)
                {
                    count++;
                    if (dispersionSolicitud[0].Tipo == "ACuentaConcentradora")
                    {
                        if (dispersiones.Count(x => x.CuentaOrigen == dispersion.CuentaOrigen) != 1)
                        {
                            dispersion.CodigoRespuesta = "989";
                            continue;
                        }
                    }
                    else if (dispersionSolicitud[0].Tipo == "ConcentradoraACuentas")
                    {
                        if (dispersiones.Count(x => x.CuentaDestino == dispersion.CuentaDestino) != 1)
                        {
                            dispersion.CodigoRespuesta = "989";
                            continue;
                        }
                    }
                    else if (dispersionSolicitud[0].Tipo == "OneToOne")
                    {
                    }
                    else
                    {
                        dispersion.CodigoRespuesta = "989";
                        continue;
                    }
                    try
                    {
                        t = GetTarjetaFromCuenta(dispersion.CuentaDestino);
                        if (t != null)
                        {
                            try
                            {
                                if (dispersion.CodigoRespuesta != "-1")
                                {
                                    Decimal monto;
                                    if (esTransSaldoTotal)
                                    {
                                        //JAVG //20150918 //Se obtiene el saldo total de la cuenta
                                        monto =
                                            webService.GetSaldosPorCuenta(dispersion.CuentaOrigen, "", "CorrerTransferencia")
                                                .Saldos.DisponibleCompras;
                                    }
                                    else
                                    {

                                        monto = dispersion.Monto > 0 ? Convert.ToDecimal(dispersion.Monto) : 0;
                                    }
                                    //JAVG //20150918 //Se cambia el parámetro de monto

                                    Decimal porcentaje = 0;

                                    if (montoPorcentaje > 0)
                                    {
                                        monto = montoPorcentaje;
                                    }
                                      

                                    if (dispersionSolicitud[0].Tipo == "ACuentaConcentradora")
                                    {
                                        porcentaje = dispersion.PorcentajeSaldoTotal ?? 0;
                                        if (porcentaje != 0)
                                        {
                                            monto = webService.GetSaldosPorCuenta(dispersion.CuentaOrigen, "", "CorrerTransferencia").Saldos.DisponibleCompras;
                                            monto *= (porcentaje / 100);
                                        }
                                    }

                                    var res = webService.TransferenciaDeCuentas(dispersion.CuentaOrigen, monto, t.NumeroTarjeta, dispersionSolicitud[0].UsuarioCreacion, dispersionSolicitud[0].Tipo.ToUpper(),porcentaje);

                                    dispersion.Monto = monto;
                                    dispersion.CodigoAutorizacion = res.NumeroAutorizacion;
                                    dispersion.IdMovimiento = res.IdMovimiento.ToString(CultureInfo.InvariantCulture);
                                    dispersion.CodigoRespuesta = res.CodigoRespuesta.ToString(CultureInfo.InvariantCulture);
                                    dispersion.SaldoOrigenAntes = res.SaldoOrigenAntes;
                                    dispersion.SaldoDestinoAntes = res.SaldoDestinoAntes;
                                    dispersion.SaldoOrigenDespues = res.SaldoOrigenDespues;
                                    dispersion.SaldoDestinoDespues = res.SaldoDestinoDespues;
                                    dispersion.MontoComision = res.Comision.MontoComision;
                                    dispersion.TipoComision = res.Comision.TipoComision;
                                    dispersion.TipoConceptoComision = res.Comision.TipoConceptoComision;
                                    POS++;
                                }
                            }
                            catch (Exception ex)
                            {
                                dispersion.CodigoRespuesta = "990";
                            }
                        }
                        else
                            dispersion.CodigoRespuesta = "993";
                        if (count >= 35)
                        {
                            dbHelper.SaveChanges();
                            count = 0;
                        }
                    }
                    catch (Exception e)
                    {
                        //TODO: Guardar en bitacora
                        SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com", "Problema transferencia " + folio, "Fallo transferencia folio " + folio + " en Id " + dispersion.Id + "  " + e, "yMQ3E3ert6");
                    }
                }
                try
                {
                    dbHelper.SaveChanges();
                }
                catch (Exception e)
                {
                    SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com", "Problema transferencia " + folio, "Fallo transferencia folio " + folio + "  " + e, "yMQ3E3ert6");
                }
                
                ActualizaYMandaMailTransferencia(dispersionSolicitud.ElementAt(0), "TRANSFERENCIA", folio);
            }
        }

        private static void ActualizaYMandaMailTransferencia(TransferenciasSolicitud transferenciasSolicitud, string tipo, string folio)
        {
            try
            {
                var dbHelper = new broxelco_rdgEntities();
                float antesPOS = 0, despuesPOS = 0, posSolicitado = 0;
                int correctasPOS = 0;
                var errores = new List<ErroresDispersion>();

                var transferenciasDetalle = dbHelper.TransferenciasDetalle.Where(x => x.FolioSolicitud == folio && x.Monto > 0).ToList();
                if (transferenciasDetalle.Count >= 1)
                {
                    antesPOS = (float)(transferenciasDetalle.Sum(x => x.SaldoDestinoAntes));
                    despuesPOS = (float)transferenciasDetalle.Sum(x => x.SaldoDestinoDespues);
                    correctasPOS = transferenciasDetalle.Count(x => x.CodigoRespuesta == "-1");
                    posSolicitado = (float)(transferenciasDetalle.Sum(x => x.Monto));
                }
                var transferenciaSolicitud = dbHelper.TransferenciasSolicitud.FirstOrDefault(x => x.Folio == folio);
                if (transferenciaSolicitud != null)
                {
                    transferenciaSolicitud.MontoTotal = Convert.ToDecimal(despuesPOS - antesPOS);
                    if (correctasPOS == transferenciasDetalle.Count(x => x.Monto != 0))
                        transferenciaSolicitud.Estado = "COMPLETA";
                    else
                    {
                        transferenciaSolicitud.Estado = "CON ERRORES";
                        var codigosRespuesta = dbHelper.CodigosRespuesta.ToList();
                        var codigos = transferenciasDetalle.Select(x => x.CodigoRespuesta).Distinct();
                        codigos = codigos.Where(x => x != "-1").ToList();
                        foreach (var codig in codigos)
                        {
                            var codigo = Convert.ToInt32(codig);
                            var e = new ErroresDispersion
                            {
                                CodigoRespuesta = codigo,
                                DescripcionCodigoResp = codigosRespuesta.FirstOrDefault(x => x.Id == codigo).Descripcion,
                                CausaComunCodigoResp = codigosRespuesta.FirstOrDefault(x => x.Id == codigo).CausaComun
                            };
                            var cuentasConError = transferenciasDetalle.Where(x => x.CodigoRespuesta == codig).ToList();
                            foreach (var cuentaConError in cuentasConError)
                            {
                                e.CuentasConError.Add(cuentaConError.CuentaDestino);
                            }
                            errores.Add(e);
                        }
                    }
                    dbHelper.SaveChanges();
                    EnviarMailTransferencia(transferenciaSolicitud.EmailNotificacion, folio, tipo, despuesPOS - antesPOS, transferenciaSolicitud.Estado, transferenciasDetalle.Count, transferenciaSolicitud.ClaveCliente, errores, posSolicitado);
                }
            }
            catch (Exception e)
            {
                SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com ", "Problema Transfer " + folio, "Fallo la transfer en ActualizaYMandaMailTransfer con folio" + folio + "  " + e, "yMQ3E3ert6");
            }
        }

        private static string PrintCurrency(float? value)
        {
            try
            {
                return (Convert.ToDecimal(value).ToString("C", CultureInfo.CurrentCulture)).Insert(1, " ");
            }
            catch (Exception)
            {
                return "ERR";
            }
        }

        private static string PrintCurrency(decimal value)
        {
            try
            {
                return (Convert.ToDecimal(value).ToString("C", CultureInfo.CurrentCulture)).Insert(1, " ");
            }
            catch (Exception)
            {
                return "ERR";
            }
        }

        private static string GetLocalIp()
        {
            try
            {
                using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    socket.Connect("10.0.2.4", 65530);
                    var endPoint = socket.LocalEndPoint as IPEndPoint;
                    if (endPoint == null)
                        return "";
                    var localIp = endPoint.Address.ToString();
                    return localIp;
                }
            }
            catch (Exception e)
            {
                return "Unkown";
            }
        }

        private static void BloqueaCuentasRedDePagos(string cuenta, string folio)
        {
            try
            {
                var proxy = new BroxelService();
                var resultBloqueo = proxy.BloqueoDeCuenta(cuenta, "WSBroxel");
                if (resultBloqueo.Success.Equals(0))
                {
                    SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com ", "Problema Reverso bloqueo cuenta, folio " + folio, "Se realizo dispersion,pero esta cuenta fue desbloqueada y no se logro volver a bloquear la cuenta", "yMQ3E3ert6");
                }
            }
            catch (Exception e)
            {
                SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com ", "Problema Reverso bloqueo cuenta, folio " + folio, "Se realizo dispersion,pero esta cuenta fue desbloqueada y no se logro volver a bloquear la cuenta " + e, "yMQ3E3ert6");
            }
        }

        
    }
}