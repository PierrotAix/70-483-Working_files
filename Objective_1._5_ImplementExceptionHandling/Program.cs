using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objective_1._5_ImplementExceptionHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test01();

            Test02();

            Console.ReadKey();
        }

        private static void Test02()
        {
            

            try
            {
                string s = OpenAndParse(null);
            }
            catch (ArgumentNullException e)
            {

                Console.WriteLine("Arg null exception" + e.Message);
            }
        }

        private static string OpenAndParse(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                  throw new ArgumentNullException("filename", "Le nom de fichier est exigé");
            }
            return File.ReadAllText(fileName);
        }

        private static void Test01()
        {
            string s = null; // "55555555555555555555555555"; // null; // "Hello";
            try
            {
                int i = int.Parse(s);
            }
            catch (ArgumentNullException ee)
            {
                Console.WriteLine("Null Exception: " + ee.StackTrace);
            }
            catch (FormatException e)
            {
                Console.WriteLine("Invalid value Exception: " + e.Message);
                //throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Autre Exception : " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Dans le finally bloc");
            }

            Console.WriteLine("End");
        }
    }
}
