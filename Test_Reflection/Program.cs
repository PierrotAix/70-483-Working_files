using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Test_Reflection
{
    class Program
    {
        /// <summary>
        /// https://www.youtube.com/watch?v=3FvT6uNMT7M&t=1s Advanced C#: 05 Reflection
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //Test01(); // de 0 à 17:04

            Test02();

            Console.ReadLine();
        }

        private static void Test02()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var types = assembly.GetTypes().Where(t => t.GetCustomAttributes<MyClassAttribute>().Count() > 0);
            foreach (var type in types)
            {
                Console.WriteLine("Type name: "+ type.Name);

                var methods = type.GetMethods().Where(m => m.GetCustomAttributes<MyMethodAttribute>().Count() > 0);
                foreach (var method in methods)
                {
                    Console.WriteLine("\tMethod name: " + method.Name);
                }
            }
        }

        private static void Test01()
        {
            var assembly = Assembly.GetExecutingAssembly();

            Console.WriteLine(assembly.FullName);

            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                Console.WriteLine("Type : " + type + " Base Type: " + type.BaseType);

                var props = type.GetProperties();
                foreach (var prop in props)
                {
                    Console.WriteLine("\tProperty Name: " + prop.Name + " PropertyType: " + prop.PropertyType);
                }

                var fields = type.GetFields();
                foreach (var field in fields)
                {
                    Console.WriteLine("\tField Name: " + field.Name);
                }

                var methods = type.GetMethods();
                foreach (var method in methods)
                {
                    Console.WriteLine("\tMethod Name:" + method.Name);
                }
            }
            Console.WriteLine("------------------------------------------------------------------------");
            var sample = new Sample { Name = "John", Age = 52 };

            var sampleType = typeof(Sample);  //var sampleType = sample.GetType();

            //var nameProperty = sampleType.GetProperty("Name");
            //Console.WriteLine("Property : " + nameProperty.GetValue(sample));

            var myMethod = sampleType.GetMethod("MyMethod");
            myMethod.Invoke(sample, null);
        }

        [MyClass]
        public class Sample
        {
            public int Age;

            public string   Name { get; set; }

            [MyMethod]
            public void MyMethod()
            {
                Console.WriteLine("Hello from MyMethod!");
            }

            public void NoAttributeMethod() { }
        }

        [AttributeUsage(AttributeTargets.Class)]
        public class MyClassAttribute : Attribute   { }

        [AttributeUsage(AttributeTargets.Method)]
        public class MyMethodAttribute : Attribute {  }
    }
}
