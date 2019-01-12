using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBinaryReader
{
    class Program
    {
        const string fileName = "Strings.dat";

        static void Main(string[] args)
        {
            Console.WriteLine("Début du test");

            /*
            if (File.Exists(fileName))
            {

                using (BinaryReader reader = new BinaryReader(File.Open(fileName,FileMode.Open))) // new BinaryReader(File.Open(fileName, FileMode.Open)))
                {
                    //for (int i = 0; i < 5; i++)
                    //{
                        //char[] characters = reader.ReadChars(i);
                        //string data = new String(characters);

                        //byte[] bytes = reader.ReadBytes(i);
                        //string data = Encoding.ASCII.GetString(bytes);

                        //byte[] bytes = new byte[i];
                        //reader.Read(bytes, 0, 1);
                        //string data = Encoding.ASCII.GetString(bytes);

                        string data = reader.ReadString();


                        Console.WriteLine("data: " + data);
                    //}
                }

            }
            */

            
            //WriteDefaultValues();
            DisplayValues();
            
            /*
            Début du test
            Aspect ratio set to: 1,25
            Temp directory is: c:\Temp
            Auto save time set to: 10
            Show status bar: True
            fin du test
            */


            Console.WriteLine("fin du test");
            Console.ReadKey();
        }

        private static void DisplayValues()
        {
            //float aspectRatio;
            string tempDirectory;
            //int autoSaveTime;
            //bool showStatusBar;

            if (File.Exists(fileName))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
                {
                    for (int i = 0; i < 4; i++)
                    {                    
                        //aspectRatio = reader.ReadSingle();
                        tempDirectory = reader.ReadString();
                        //autoSaveTime = reader.ReadInt32();
                        //showStatusBar = reader.ReadBoolean();

                        Console.WriteLine("Temp directory is: " + tempDirectory);
                    }
                }

                //Console.WriteLine("Aspect ratio set to: " + aspectRatio);
                
                //Console.WriteLine("Auto save time set to: " + autoSaveTime);
                //Console.WriteLine("Show status bar: " + showStatusBar);
            }
        }

        private static void WriteDefaultValues()
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create)))
            {
                //writer.Write(1.250F);
                writer.Write(@"c:\Temp");
                //writer.Write(10);
                //writer.Write(true);
            }
        }
    }
}
