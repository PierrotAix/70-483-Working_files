using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_LINQ
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
            Console.WriteLine("Avant LINQ ----------------------------------");
            foreach (var item in GetLeads())
            {
                Console.WriteLine(item.Name);
            }

            var query = from I in GetLeads()
                        where I.AddressLine1 != null &&
                        I.City != null && I.PostalCode != null &&
                        I.Region != null
                        //var query = from I in GetLeads()
                        //            where AddressLine1 != null &&
                        //            City != null && PostalCode != null &&
                        //            Region != null
                        select new MailingAddress
                        {
                           Name= I.Name ?? "our Neighbors",
                           AddressLine1= I.AddressLine1,
                           AddressLine2= I.AddressLine2,
                           City= I.City,
                           Region= I.Region,
                           PostalCode=I.PostalCode
                        };

            foreach (var item in query)
            {
                Console.WriteLine($" {item.Name}  {item.AddressLine1}");
            }

            /*
             Avant LINQ ----------------------------------
            Pierre
            Chritine

             Pierre  1 rue
             Chritine  1 rue
             our Neighbors  1 rue
             * */

        }

        private static List<MailingAddress> GetLeads()
        {
            List<MailingAddress> maListe = new List<MailingAddress>();
            MailingAddress ma1 = new MailingAddress()
            {
                Name = "Pierre",
                AddressLine1 = "1 rue",
                AddressLine2 = "Franchet",
                City = "Aix-en-Provence",
                PostalCode = "13090",
                Region = "PACA"
            };
            maListe.Add(ma1);

            MailingAddress ma2 = new MailingAddress()
            {
                Name = "Chritine",
                AddressLine1 = "1 rue",
                AddressLine2 = "Franchet",
                City = "Aix-en-Provence",
                PostalCode = "13090",
                Region = "PACA"
            };
            maListe.Add(ma2);

            MailingAddress ma3 = new MailingAddress()
            {
                Name = null, //String.Empty,
                AddressLine1 = "1 rue",
                AddressLine2 = "Franchet",
                City = "Aix-en-Provence",
                PostalCode = "13090",
                Region = "PACA"
            };
            maListe.Add(ma3);

            return maListe;
        }

        private static void Test01()
        {
            Console.WriteLine();

            var list1 = new List<int>() { 1, 2, 3, 4 };
            //var result = list1.Where(i => i > 3).ToList();
            var result = list1.Where(i => i > 3);


            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
            list1.Add(5);
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }
    }

    public class MailingAddress
    {
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }

    }
}
