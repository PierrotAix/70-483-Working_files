using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Encrypting_Files
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"D:\BITBUCKET\c-sharp\C Sharp (Exam 70-483) Online Video Tutorials\Programming in Microsoft C# - Exam 70-483 - Working Files\Chapter 10\Encrypting Files\Encrypting Files\Data\";
            string fileName = "ml.txt";
            string pathfileName = path + fileName;
            string outputpathfileName = path + "output.txt";
            string decryptedtpathfileName = path + "decrypted.txt";

            /*
            ReadFile(pathfileName);

          Console.WriteLine("Press enter to encrypt the input file :" + pathfileName + " into " + outputpathfileName);
          Console.ReadLine();

          //EncryptFile (@"C:\MLFiles\ml.txt");
            EncryptFile(pathfileName, outputpathfileName);


            ReadFile(outputpathfileName);

          Console.WriteLine("Press enter to decrypt the file into: " + decryptedtpathfileName);
          Console.Read();

            //File.Decrypt(@"C:\MLFiles\ml.txt");
            //File.Decrypt(pathfileName);
            DecryptFile(outputpathfileName, decryptedtpathfileName);

            ReadFile(decryptedtpathfileName);
            */

            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("Enter a word to Encrypt");
            string wordToEncrypt = Console.ReadLine();
            Console.WriteLine("Vous avez saisi : " + wordToEncrypt);

            string wordEncrypted = Encrypt(wordToEncrypt);

            Console.WriteLine(@"The encrypted word is: " + wordEncrypted);

            Console.WriteLine("---------------------------------------------------------------------------");
            string decryptedWord = Decrypt(wordEncrypted);
            Console.WriteLine(@"The Decrypted word is: " + decryptedWord);


            Console.WriteLine("Press enter to exit");
            Console.ReadKey();

            /*
            ---------------------------------------------------------------------------
            Enter a word to Encrypt
            toto
            Vous avez saisi : toto
            The encrypted word is: td1KpjeM0jEV1HZX1tOLiw==
            ---------------------------------------------------------------------------
            The Decrypted word is: toto
            Press enter to exit
             
             * */

        }

        public static void ReadFile(string pathfileName)
        {
            int counter = 0;
            string line;

            // Read the file and display it line by line.
            //System.IO.StreamReader file = new System.IO.StreamReader(@"C:\MLFiles\ml.txt");
            System.IO.StreamReader file = new System.IO.StreamReader(pathfileName);

            while ((line = file.ReadLine()) != null)
                Console.WriteLine(line);
            counter++;

            file.Close();

        }


        public static void EncryptFile(string x)
        {
            File.Encrypt(x);
        }

        static readonly string PasswordHash = "P@@Sw0rd";
        static readonly string SaltKey = "S@LT&KEY";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";

        /// <summary>
        /// 91 - Encrypting and Decrypting Data Streams.mp4 02:14
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memorySTream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memorySTream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memorySTream.ToArray();
                    cryptoStream.Close();
                }
                memorySTream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }

        /// <summary>
        /// 91 - Encrypting and Decrypting Data Streams.mp4 02:14
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <returns></returns>
        public static string Decrypt(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };


            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }

        ///<summary>
        /// Steve Lydford - 12/05/2008.
        ///
        /// Encrypts a file using Rijndael algorithm.
        ///</summary>
        ///<param name="inputFile"></param>
        ///<param name="outputFile"></param>
        public static void EncryptFile(string inputFile, string outputFile)
        {

            try
            {
                string password = @"myKey123"; // Your Key Here
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                string cryptFile = outputFile;
                FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateEncryptor(key, key),
                    CryptoStreamMode.Write);

                FileStream fsIn = new FileStream(inputFile, FileMode.Open);

                int data;
                while ((data = fsIn.ReadByte()) != -1)
                    cs.WriteByte((byte)data);


                fsIn.Close();
                cs.Close();
                fsCrypt.Close();
            }
            catch
            {
                Console.WriteLine("Encryption failed!", "Error");
            }
        }

        public static void DecryptFile(string x)
        {
            File.Decrypt(x);
        }

        ///<summary>
        /// Steve Lydford - 12/05/2008.
        ///
        /// Decrypts a file using Rijndael algorithm.
        ///</summary>
        ///<param name="inputFile"></param>
        ///<param name="outputFile"></param>
        public static void DecryptFile(string inputFile, string outputFile)
        {

            {
                string password = @"myKey123"; // Your Key Here

                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateDecryptor(key, key),
                    CryptoStreamMode.Read);

                FileStream fsOut = new FileStream(outputFile, FileMode.Create);

                int data;
                while ((data = cs.ReadByte()) != -1)
                    fsOut.WriteByte((byte)data);

                fsOut.Close();
                cs.Close();
                fsCrypt.Close();

            }
        }
    }
}
