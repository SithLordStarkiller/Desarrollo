using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace wsBroxel.Dispatcher
{
    public class PetrusPin
    {
        public string Key { get; private set; }
        public string Nip { get; private set; }
        public string CardNumber { get; private set; }
        private string PreparePin { get; set; }
        private string PrepareCardNumber { get; set; }
        public byte[] PinBlock { get; private set; }
        public string PinBlockEncrypted { get; private set; }

        public PetrusPin(string cardnumber, string nip)
        {
            Key = ConfigurationManager.AppSettings["PinKey"];
            Nip = nip;
            CardNumber = cardnumber;
        }
        public void GeneratePinblock()
        {
            PreparePin = string.Format("{0}{1}{2}{3}", "0", Nip.Length.ToString("X"), Nip, "".PadRight(16 - 2 - Nip.Length, 'F'));
            PrepareCardNumber = string.Format("{0}{1}", "0000", CardNumber.Right(12, 1));
            PinBlock = XOR(PreparePin.HexToByte(), PrepareCardNumber.HexToByte());
        }
        public void GeneratePinblockEncrypted()
        {
            PinBlockEncrypted = Encryption((Key).HexToByte(), PinBlock).BToHex();
        }

        public byte[] Encryption(byte[] Deskey, byte[] plainText)
        {
            SymmetricAlgorithm TdesAlg = new TripleDESCryptoServiceProvider();
            TdesAlg.Key = Deskey;
            TdesAlg.Mode = CipherMode.CBC;
            TdesAlg.Padding = PaddingMode.Zeros;
            TdesAlg.IV = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            ICryptoTransform ict = TdesAlg.CreateEncryptor(TdesAlg.Key, TdesAlg.IV);
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, ict, CryptoStreamMode.Write);
            cStream.Write(plainText, 0, plainText.Length);
            cStream.FlushFinalBlock();
            cStream.Close();
            return mStream.ToArray();
        }

        public byte[] XOR(byte[] bHEX1, byte[] bHEX2)
        {
            byte[] bHEX_OUT = new byte[bHEX1.Length];
            for (int i = 0; i < bHEX1.Length; i++)
            {
                bHEX_OUT[i] = (byte)(bHEX1[i] ^ bHEX2[i]);
            }
            return bHEX_OUT;
        }
    }
    public static class Helpers
    {
        public static byte[] HexToByte(this string hexString)
        {
            byte[] byteOUT = new byte[hexString.Length / 2];
            for (int i = 0; i < hexString.Length; i = i + 2)
            {
                byteOUT[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            }
            return byteOUT;
        }
        public static string BToHex(this byte[] Bdata)
        {
            return BitConverter.ToString(Bdata).Replace("-", "");
        }
        public static string Right(this string str, int len)
        {
            return str.Substring(str.Length - len, len);
        }
        public static string Right(this string str, int len, int skiplen)
        {
            return str.Substring(str.Length - len - skiplen, len);
        }
    }
}