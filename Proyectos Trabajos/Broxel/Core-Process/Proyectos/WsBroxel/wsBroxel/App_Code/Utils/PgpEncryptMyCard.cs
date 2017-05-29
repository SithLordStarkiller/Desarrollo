using System;
using System.IO;
using System.Linq;
using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Security;

namespace wsBroxel.App_Code.Utils
{
    public class PgpEncryptMyCard
    {
        private PgpEncryptionKeys m_encryptionKeys;
        private const int BufferSize = 0x10000;

        /// <summary>
        /// Instantiate a new PgpEncryptMyCard class with initialized PgpEncryptMyCardionKeys.
        /// </summary>
        /// <param name="encryptionKeys"></param>
        /// <exception cref="ArgumentNullException">encryptionKeys is null</exception>
        public PgpEncryptMyCard(PgpEncryptionKeys encryptionKeys)
        {
            if (encryptionKeys == null)
            {
                throw new ArgumentNullException("encryptionKeys", "encryptionKeys is null.");
            }
            m_encryptionKeys = encryptionKeys;
        }

        /// <summary>
        /// Encrypt and sign the file pointed to by unencryptedFileInfo and
        /// write the encrypted content to outputStream.
        /// </summary>
        /// <param name="outputStream">The stream that will contain the
        /// encrypted data when this method returns.</param>
        /// <param name="fileName">FileInfo of the file to encrypt</param>
        public void Encrypt(Stream outputStream, FileInfo unencryptedFileInfo)
        {
            if (outputStream == null)
            {
                throw new ArgumentNullException("outputStream", "outputStream is null.");
            }
            if (unencryptedFileInfo == null)
            {
                throw new ArgumentNullException("unencryptedFileInfo", "unencryptedFileInfo is null.");
            }
            if (!File.Exists(unencryptedFileInfo.FullName))
            {
                throw new ArgumentException("File to encrypt not found.");
            }
            using (Stream encryptedOut = ChainEncryptedOut(outputStream))
            {
                using (Stream literalOut = ChainLiteralOut(encryptedOut, unencryptedFileInfo))
                using (FileStream inputFile = unencryptedFileInfo.OpenRead())
                {
                    WriteOutput(literalOut, inputFile);
                }
            }
        }

        private static void WriteOutput(Stream literalOut,
            FileStream inputFile)
        {
            int length = 0;
            byte[] buf = new byte[BufferSize];
            while ((length = inputFile.Read(buf, 0, buf.Length)) > 0)
            {
                literalOut.Write(buf, 0, length);
            }
        }

        private Stream ChainEncryptedOut(Stream outputStream)
        {
            PgpEncryptedDataGenerator encryptedDataGenerator;
            encryptedDataGenerator =
                new PgpEncryptedDataGenerator(SymmetricKeyAlgorithmTag.TripleDes,
                    new SecureRandom());
            encryptedDataGenerator.AddMethod(m_encryptionKeys.PublicKey);
            return encryptedDataGenerator.Open(outputStream, new byte[BufferSize]);
        }

        private static Stream ChainLiteralOut(Stream encryptedOut, FileInfo file)
        {
            PgpLiteralDataGenerator pgpLiteralDataGenerator = new PgpLiteralDataGenerator();
            return pgpLiteralDataGenerator.Open(encryptedOut, PgpLiteralData.Binary, file);
        }
    }
    public class PgpEncryptionKeys
    {
        public PgpPublicKey PublicKey { get; private set; }
        public PgpPrivateKey PrivateKey { get; private set; }
        public PgpSecretKey SecretKey { get; private set; }
        /// <summary>
        /// Initializes a new instance of the EncryptionKeys class.
        /// Two keys are required to encrypt and sign data. Your private key and the recipients public key.
        /// The data is encrypted with the recipients public key and signed with your private key.
        /// </summary>
        /// <param name="publicKeyPath">The key used to encrypt the data</param>
        /// <param name="privateKeyPath">The key used to sign the data.</param>
        /// <param name="passPhrase">The (your) password required to access the private key</param>
        /// <exception cref="ArgumentException">Public key not found. Private key not found. Missing password</exception>
        public PgpEncryptionKeys(string publicKeyPath, string privateKeyPath, string passPhrase)
        {
            if (!File.Exists(publicKeyPath))
                throw new ArgumentException("Public key file not found", "publicKeyPath");
            //if (!File.Exists(privateKeyPath))
            //    throw new ArgumentException("Private key file not found", "privateKeyPath");
            //if (String.IsNullOrEmpty(passPhrase))
            //    throw new ArgumentException("passPhrase is null or empty.", "passPhrase");
            PublicKey = ReadPublicKey(publicKeyPath);
            //SecretKey = ReadSecretKey(privateKeyPath);
            //PrivateKey = ReadPrivateKey(passPhrase);
        }
        #region Secret Key
        private PgpSecretKey ReadSecretKey(string privateKeyPath)
        {
            using (Stream keyIn = File.OpenRead(privateKeyPath))
            using (Stream inputStream = PgpUtilities.GetDecoderStream(keyIn))
            {
                PgpSecretKeyRingBundle secretKeyRingBundle = new PgpSecretKeyRingBundle(inputStream);
                PgpSecretKey foundKey = GetFirstSecretKey(secretKeyRingBundle);
                if (foundKey != null)
                    return foundKey;
            }
            throw new ArgumentException("Can't find signing key in key ring.");
        }
        /// <summary>
        /// Return the first key we can use to encrypt.
        /// Note: A file can contain multiple keys (stored in "key rings") 
        /// </summary>
        private PgpSecretKey GetFirstSecretKey(PgpSecretKeyRingBundle secretKeyRingBundle)
        {
            foreach (PgpSecretKeyRing kRing in secretKeyRingBundle.GetKeyRings())
            {
                PgpSecretKey key = kRing.GetSecretKeys()
                .Cast<PgpSecretKey>()
                .Where(k => k.IsSigningKey)
                .FirstOrDefault();
                if (key != null)
                    return key;
            }
            return null;
        }
        #endregion
        #region Public Key
        private PgpPublicKey ReadPublicKey(string publicKeyPath)
        {
            using (Stream keyIn = File.OpenRead(publicKeyPath))
            using (Stream inputStream = PgpUtilities.GetDecoderStream(keyIn))
            {
                PgpPublicKeyRingBundle publicKeyRingBundle = new PgpPublicKeyRingBundle(inputStream);
                PgpPublicKey foundKey = GetFirstPublicKey(publicKeyRingBundle);
                if (foundKey != null)
                    return foundKey;
            }
            throw new ArgumentException("No encryption key found in public key ring.");
        }
        private PgpPublicKey GetFirstPublicKey(PgpPublicKeyRingBundle publicKeyRingBundle)
        {
            foreach (PgpPublicKeyRing kRing in publicKeyRingBundle.GetKeyRings())
            {
                PgpPublicKey key = kRing.GetPublicKeys()
                .Cast<PgpPublicKey>()
                .Where(k => k.IsEncryptionKey)
                .FirstOrDefault();
                if (key != null)
                    return key;
            }
            return null;
        }
        #endregion
        #region Private Key
        private PgpPrivateKey ReadPrivateKey(string passPhrase)
        {
            PgpPrivateKey privateKey = SecretKey.ExtractPrivateKey(passPhrase.ToCharArray());
            if (privateKey != null)
                return privateKey;
            throw new ArgumentException("No private key found in secret key.");
        }
        #endregion
    }
}