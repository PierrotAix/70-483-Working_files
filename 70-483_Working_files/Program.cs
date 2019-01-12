using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;

namespace SerialisationJSON
{
    class Program
    {
        /// <summary>
            // ecrire les donnees JSON dans le flux
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            Console.WriteLine("Début du test");

            // creation d'instance
            Person p = new Person();
            p.name = "Pierre"; // c

            p.age = 52;

            // serialsation
            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Person)); // DataContractJsonSerializer(typeof(Person));

            // ecrire les donnees JSON dans le flux
            ser.WriteObject(stream1, p);

            // Lire les donnees JSON depuis le flux
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            Console.WriteLine("JSON form of Person object : ");
            Console.WriteLine(sr.ReadToEnd());

            // Deserialise une instance de Type Person depuis JSON
            stream1.Position = 0;
            Person p2 = (Person)ser.ReadObject(stream1);
            Console.WriteLine($"Deserialized back, got name = {p2.name} and age={p2.age}");

            /*
            string json = "{ "age":52,"name":"Pierre"}";
            // Cree un nouvel objet depuis le DataContractJsonSerializer
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            */


            Console.WriteLine("Fin du test");
            Console.ReadKey();

        }
    }
}
