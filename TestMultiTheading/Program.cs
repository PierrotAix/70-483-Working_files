using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestMultiTheading
{
    class Program
    {
        /// <summary>
        /// Selon le livre C# page 405
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //Test00_PasAsynchrone();

            //Test01_Asynchrone();

            //Test02_AsyncAwaitAsync();

            //Test03_AsyncAwaitIteration();

            //Test04_LINQAggregate();

            //Test05_Dictonary();

            Test06_LINQ_query();


            Console.ReadKey();
        }


        private static void Test06_LINQ_query()
        {
            var query = from p in ProductList.GetProducts() select p.Price;
            // The code above uses a LINQ expression to project the Price property from
            // a collection of Product instances. The resulting query is a IEnumarable<string> 
            // instance of product names.

            foreach (var item in query)
            {
                Console.WriteLine(item);
            }
        }

        private static void Test05_Dictonary()
        {
            var dictionary = new Dictionary<int, string>()
            {
                {1, "Sales" },
                {2, "Markeling" },
                {3, "Finance" }
            };

            foreach (var item in dictionary)
            {
                Console.WriteLine("Pour la clef : " + item.Key + " la valeur est : " + item.Value);
            }
            /*
             Pour la clef : 1 la valeur est : Sales
                Pour la clef : 2 la valeur est : Markeling
                Pour la clef : 3 la valeur est : Finance
             * */
        }

        private static void Test04_LINQAggregate()
        {
            int[] ints = {4, 8, 8, 3, 9, 0 , 8, 2 };

            //Compte le nombre de nombres pairs du tableau en utilisant une initialisation à 0
            int numEven = ints.Aggregate(0,
                (total, next) => next %2 == 0 ? total + 1 : total
                );

            Console.WriteLine("Le nombre de chiffres pairs du tableau est {0}", numEven);
        }

        private static void Test03_AsyncAwaitIteration()
        {
            Console.WriteLine("Lancement d'une fonction longue asynchrone dans une itération");
            Iteration();

            for (int i = 1; i < 20; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(1000);
            }

            /*
             * 
            Lancement d'une fonction longue asynchrone dans une itération
            1
            2
            3
            4
            5
            6
            Resultat 1 : -24,4605663988507
            7
            8
            9
            10
            11
            12
            Resultat 2 : -24,4605663988507
            13
            14
            15
            16
            17
            18
            19
             * 
             */

        }

        private static async void Iteration()
        {
            for (int i = 0; i < 2; i++)
            {
                double resultat = await TimeConsumingFunctionAsync();
                Console.WriteLine("Resultat " + ( i + 1) + " : " + resultat);
            }
        }

        private static async Task Test02_AsyncAwaitAsync()
        {
            double resultat = await TimeConsumingFunctionAsync();

            Console.WriteLine("Résultat :" + resultat.ToString());
        }

        private static void Test01_Asynchrone()
        {
            Console.WriteLine("Lancement d'une fonction longue asynchrone");

            int i = 0;

            Task<double> task = TimeConsumingFunctionAsync();

            TaskAwaiter<double> awaiter = task.GetAwaiter();
            awaiter.OnCompleted(
                () =>
                {
                    i = 20;
                    double resultat2 = awaiter.GetResult();
                    Console.Write("Résultat: ");
                    Console.WriteLine(resultat2);
                }
                );

            for ( i = 1; i < 15; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(1000);
            }
            /*
             Lancement d'une fonction longue asynchrone
            1
            2
            3
            4
            5
            6
            Résultat: -24,4605663988507
             * */
        }

        private static void Test00_PasAsynchrone()
        {
            Console.WriteLine("Lancement d'une fonction longue");

            DateTime debut = DateTime.Now;

            double resultat = TimeConsumingFunction();

            DateTime fin = DateTime.Now;

            Console.WriteLine("Résultat :" + resultat.ToString());

            Console.WriteLine("Temps d'éxecution en secondes :" +
                TimeSpan.FromTicks(fin.Ticks - debut.Ticks).TotalSeconds);
        }

        private static double TimeConsumingFunction()
        {
            double x = 1;
            for (int i = 1; i < 100000000; i++)
            {
                x += Math.Tan(x) / i;
            }
            return x;
        }

        public static Task<double> TimeConsumingFunctionAsync()
        {
            return Task.Run(() => TimeConsumingFunction());
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public double Price { get; set; }
    }

    public class ProductList : List<Product>
    {
        public static ProductList GetProducts()
        {
            //List<Product> products = new List<Product>();
            //products.Add( new Product { ID = "1", Name = "Riz", Price = 100 } );
            //products.Add(new Product { ID = "2", Name = "Pates", Price = 50 });
            //return (ProductList)products;
            ProductList products = new ProductList();
            products.Add(new Product { ID = "1", Name = "Riz", Price = 100 });
            products.Add(new Product { ID = "2", Name = "Pates", Price = 50 });
            return products;
        }
    }




}
