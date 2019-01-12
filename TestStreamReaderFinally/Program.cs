using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStreamReaderFinally
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader file = new StreamReader("data.txt");
            try
            {
                file.ReadLine();
            }
            catch (System.IO.IOException e)
            {

                throw new FileLoadException();
            }
            finally
            {
                file.Close();
            }
        }
    }
}
