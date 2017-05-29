using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace wsBroxel.App_Code.Utils
{
    /// <summary>
    /// Clase para encripción AES
    /// </summary>
    public class AesEncrypter
    {
        #region Settings

        private const int Iterations = 2;
        private const int KeySize = 256;

        private const string Hash = "SHA1";
        private const string Salt = "aselrias38490a32"; // Random
        private const string Vector = "8947az34awl34kjq"; // Random

        #endregion

        /// <summary>
        /// Encripción AES
        /// </summary>
        /// <param name="value">Cadena a encriptar</param>
        /// <param name="password">Password o semilla para la encripción</param>
        /// <returns>Cadena encriptada</returns>
        public static string Encrypt(string value, string password)
        {
            return Encrypt<AesManaged>(value, password);
        }

        /// <summary>
        /// Encripción AES
        /// </summary>
        /// <param name="value">Cadena a encriptar</param>
        /// <param name="password">Password o semilla para la encripción</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>Cadena encriptada</returns>
        public static string Encrypt<T>(string value, string password)
                where T : SymmetricAlgorithm, new()
        {
            byte[] vectorBytes = Encoding.ASCII.GetBytes(Vector);
            byte[] saltBytes = Encoding.ASCII.GetBytes(Salt);
            byte[] valueBytes = Encoding.UTF8.GetBytes(value);

            byte[] encrypted;
            using (T cipher = new T())
            {
                var passwordBytes =
                    new PasswordDeriveBytes(password, saltBytes, Hash, Iterations);
                var keyBytes = passwordBytes.GetBytes(KeySize / 8);

                cipher.Mode = CipherMode.CBC;

                using (var encryptor = cipher.CreateEncryptor(keyBytes, vectorBytes))
                {
                    using (var to = new MemoryStream())
                    {
                        using (var writer = new CryptoStream(to, encryptor, CryptoStreamMode.Write))
                        {
                            writer.Write(valueBytes, 0, valueBytes.Length);
                            writer.FlushFinalBlock();
                            encrypted = to.ToArray();
                        }
                    }
                }
                cipher.Clear();
            }
            return Convert.ToBase64String(encrypted);
        }

        /// <summary>
        /// Desencripción de cadena AES
        /// </summary>
        /// <param name="value">Cadena encriptada</param>
        /// <param name="password">semilla para desencripción</param>
        /// <returns>Cadena desencriptada</returns>
        public static string Decrypt(string value, string password)
        {
            return Decrypt<AesManaged>(value, password);
        }
        /// <summary>
        /// Desencripción de cadena AES
        /// </summary>
        /// <param name="value">Cadena encriptada</param>
        /// <param name="password">semilla para desencripción</param>
        /// <returns>Cadena desencriptada</returns>
        public static string Decrypt<T>(string value, string password) where T : SymmetricAlgorithm, new()
        {
            var vectorBytes = Encoding.ASCII.GetBytes(Vector);
            var saltBytes = Encoding.ASCII.GetBytes(Salt);
            var valueBytes = Convert.FromBase64String(value);

            byte[] decrypted;
            int decryptedByteCount;

            using (T cipher = new T())
            {
                var passwordBytes = new PasswordDeriveBytes(password, saltBytes, Hash, Iterations);
                byte[] keyBytes = passwordBytes.GetBytes(KeySize / 8);

                cipher.Mode = CipherMode.CBC;

                try
                {
                    using (var decryptor = cipher.CreateDecryptor(keyBytes, vectorBytes))
                    {
                        using (var from = new MemoryStream(valueBytes))
                        {
                            using (var reader = new CryptoStream(from, decryptor, CryptoStreamMode.Read))
                            {
                                decrypted = new byte[valueBytes.Length];
                                decryptedByteCount = reader.Read(decrypted, 0, decrypted.Length);
                            }
                        }
                    }
                }
                catch
                {
                    return String.Empty;
                }

                cipher.Clear();
            }
            return Encoding.UTF8.GetString(decrypted, 0, decryptedByteCount);
        }
    }
}