using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Test_LINQ_to_XML
{
    class Program
    {
        static void Main(string[] args)
        {
            XDocument xDoc = XDocument.Load("monFichier.xml");

            var result = xDoc.Descendants("Movie");

            var movies = from m in xDoc.Descendants("Movie")
                         where m.Attribute("ReleaseDate").Value == ""
                         select m;

            movies.ToList().ForEach(m => m.Attribute("ReleaseDate").Value = "2013"); //ne marche pas

            foreach (var movie in movies)
            {
                Console.WriteLine(movie.Descendants("Title"));
            }

            Console.WriteLine("Tapez unr touche pour finir");
            Console.ReadLine();
        }
    }
}
