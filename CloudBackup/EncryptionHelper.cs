using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CloudBackup
{
    class EncryptionHelper
    {
        private static readonly byte[] VIKey = Encoding.UTF8.GetBytes("@1B2c3D4e5F6g7H8");
        
        public static void Main()
        {
            
            try
            {

                //string original = "Here is some data to encrypt!";
                string key = "abc123deaoezdf77";

                //Console.WriteLine("Original:   {0}", original);

                //// Create a new instance of the TripleDESCryptoServiceProvider
                //// class.  This generates a new key and initialization 
                //// vector (IV).
                //string crypted = EncryptString(original, key);

                //Console.WriteLine("Original crypt:   {0}", crypted);

                //string roundtrip = DecryptString(crypted, key);

                //    //Display the original data and the decrypted data.
                    
                    
                //    Console.WriteLine("Round Trip: {0}", roundtrip);

                EncryptFile(key, "c:\\temp\\test.txt", "c:\\temp\\test_cipher.txt");
                DecryptFile(key, "c:\\temp\\test_cipher.txt", "c:\\temp\\test_roudtrip.txt");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }

        private static byte[] GenerateAlgotihmInputs(string password)
        {

            byte[] key;
            
            List<byte[]> result = new List<byte[]>();

            Rfc2898DeriveBytes rfcDb = new Rfc2898DeriveBytes(password, System.Text.Encoding.UTF8.GetBytes(password));

            key = rfcDb.GetBytes(16);
            

            return key;

        }

        public static string EncryptString(String cipherText, string Key)
        {
            byte[] tmpKey = GenerateAlgotihmInputs(Key);
            byte[] plainText = Encoding.UTF8.GetBytes(cipherText);
            byte[] encrypted;
            using (RijndaelManaged alg = new RijndaelManaged())
            {

                alg.Key = tmpKey;
                alg.IV = tmpKey;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = alg.CreateEncryptor(alg.Key, alg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(plainText, 0, plainText.Length);
                        csEncrypt.FlushFinalBlock();


                        // Place les données chiffrées dans un tableau d'octet
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(encrypted);


        }

       
        public static string DecryptString(String cipherText, string Key)
        {
            byte[] tmpCipherText = Convert.FromBase64String(cipherText);
            byte[] tmpKey = GenerateAlgotihmInputs(Key);
            
            using (RijndaelManaged alg = new RijndaelManaged())
            {
                alg.Key = tmpKey;
                alg.IV = tmpKey;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = alg.CreateDecryptor(alg.Key, alg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(tmpCipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        // Place les données déchiffrées dans un tableau d'octet
                        byte[] plainTextData = new byte[tmpCipherText.Length];

                        int decryptedByteCount = csDecrypt.Read(plainTextData, 0, plainTextData.Length);
                        return Encoding.UTF8.GetString(plainTextData, 0, decryptedByteCount);
                    }
                    
                }
                
            }

        }

        public static void EncryptFile(string strKey, string pathPlainTextFile, string pathCypheredTextFile)
        {


            // Place la clé de déchiffrement dans un tableau d'octets
            byte[] key = GenerateAlgotihmInputs(strKey);

            // Place le vecteur d'initialisation dans un tableau d'octets
            byte[] iv = GenerateAlgotihmInputs(strKey);

            Directory.CreateDirectory(Directory.GetParent(pathCypheredTextFile).FullName);
            FileStream fsCypheredFile = new FileStream(pathCypheredTextFile, FileMode.Create);

            RijndaelManaged rijndael = new RijndaelManaged();
            rijndael.Mode = CipherMode.CBC;
            rijndael.Key = key;
            rijndael.IV = iv;


            ICryptoTransform aesEncryptor = rijndael.CreateEncryptor();

            CryptoStream cs = new CryptoStream(fsCypheredFile, aesEncryptor, CryptoStreamMode.Write);

            FileStream fsPlainTextFile = new FileStream(pathPlainTextFile, FileMode.OpenOrCreate);

            int data;

            while ((data = fsPlainTextFile.ReadByte()) != -1)
            {
                cs.WriteByte((byte)data);
            }

            fsPlainTextFile.Close();
            cs.Close();
            fsCypheredFile.Close();
        }


        public static void DecryptFile(string strKey, string pathCypheredTextFile, string pathPlainTextFile)
        {

            // Place la clé de déchiffrement dans un tableau d'octets
            byte[] key = GenerateAlgotihmInputs(strKey);

            // Place le vecteur d'initialisation dans un tableau d'octets
            byte[] iv = GenerateAlgotihmInputs(strKey);

            // Filestream of the new file that will be decrypted.   
            Directory.CreateDirectory(Directory.GetParent(pathPlainTextFile).FullName);
            FileStream fsCrypt = new FileStream(pathPlainTextFile, FileMode.Create);

            RijndaelManaged rijndael = new RijndaelManaged();
            rijndael.Mode = CipherMode.CBC;
            rijndael.Key = key;
            rijndael.IV = iv;


            ICryptoTransform aesDecryptor = rijndael.CreateDecryptor();

            CryptoStream cs = new CryptoStream(fsCrypt, aesDecryptor, CryptoStreamMode.Write);

            // FileStream of the file that is currently encrypted.    
            FileStream fsIn = new FileStream(pathCypheredTextFile, FileMode.OpenOrCreate);

            int data;

            while ((data = fsIn.ReadByte()) != -1)
                cs.WriteByte((byte)data);
            cs.Close();
            fsIn.Close();
            fsCrypt.Close();

        }

    }
}
