using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_CSharp7
{
    /// <summary>
    /// https://iamtimcorey.com/top-5-new-csharp-7-features/
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //Test01(); //inline declaration

            //Test02(); //pattern matching

            //Test03(); // Powerfull switch statement

            //Test04(); //Throw in expression

            Test05(); // Tuples

            Console.ReadLine();
        }

        private static void Test05()
        {
            (string firstName, string lastName, bool itWorked) name = SplitName("Tim Corey");
            Console.WriteLine($"The fisrt name is { name.firstName } and the last name is {name.lastName} ");

            //The fisrt name is Tim and the last name is Corey

        }

        /// <summary>
        ///  you need to add the System.ValueTuple NuGet package in
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public static (string firstName, string lastName, bool itWorked) SplitName(string fullName)
        {
            string[] vals = fullName.Split(' ');

            return (vals[0], vals[1], true);
        }

        /// <summary>
        /// Love #4 Throw in expression
        /// ?? null coalescing parameter
        /// </summary>
        private static void Test04()
        {
            Employee emp1 = new Employee { FirstName = "Joe", LastName = "Smith", IsManager = false, YearsWorked = 2 };
            Employee emp2 = new Employee { FirstName = "Sandra", LastName = "Jones", IsManager = true, YearsWorked = 30 };
            List<Employee> people = new List<Employee>() { emp1, emp2 };

            Employee ceo = people.Where(x => x.IsManager).FirstOrDefault() ?? throw new Exception("There was a problem finding a manager.");
            Console.WriteLine($"The ceo is { ceo.FirstName }.");
        }

        /// <summary>
        /// Love #3 Powerfull switch statements (finally)
        /// </summary>
        private static void Test03()
        {

            Employee emp1 = new Employee { FirstName = "Joe", LastName = "Smith", IsManager = false, YearsWorked = 2 };
            Employee emp2= new Employee { FirstName = "Sandra", LastName = "Jones", IsManager = true, YearsWorked = 30 };
            Customer cust1 = new Customer { FirstName = "Tim", LastName = "Corey", TotalDollarsSpent = 145M };
            Customer cust2 = new Customer { FirstName = "Delana", LastName = "GreenBack", TotalDollarsSpent = 45678M};
            List<object> people = new List<object>() { emp1, emp2, cust1, cust2 };

            foreach (var p in people)
            {
                switch (p)
                {
                    case Employee e when (e.IsManager == false):
                        Console.WriteLine($"{e.FirstName} is a good employee.");
                        break;
                    case Employee e when (e.IsManager):
                        Console.WriteLine($"{e.FirstName} runs this company.");
                        break;
                    case Customer c when (c.TotalDollarsSpent > 1000):
                        Console.WriteLine($"{c.FirstName} is a loyal customer.");
                        break;
                    case Customer c:
                        Console.WriteLine($"{c.FirstName} needs to spend more money.");
                        break;
                    default:
                        break;
                }
            }

            /*
            Joe is a good employee.
            Sandra runs this company.
            Tim needs to spend more money.
            Delana is a loyal customer.
             * */
        }

        /// <summary>
        /// Love #2: Patern Matching
        /// </summary>
        private static void Test02()
        {
            string ageFromConsole = "21";
            int ageFromDatabase = 84;

            //object ageVal = ageFromConsole; // any type
            object ageVal = ageFromDatabase; // any type

            if (ageVal is int age || (ageVal is string ageText && int.TryParse(ageText, out age) ))
            {
                Console.WriteLine($"Your age is { age }.");
            }
            else
            {
                Console.WriteLine("We do not know your age...");
            }
        }

        private static void Test01()
        {
            Console.Write("What is your age : ");
            string ageText = Console.ReadLine();

            // old way of doing thing:
            //int age = 0;
            //bool isValidAge = int.TryParse(ageText, out age);

            // new way out variable declared inline
            bool isValidAge = int.TryParse(ageText, out int age);

            Console.WriteLine($"Your age is { age }.");
        }

        private class Employee
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public int YearsWorked { get; set; } = 0;



            public bool IsManager { get; set; } = false;
       
        }

        private class Customer
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }
            public decimal TotalDollarsSpent { get; set; }

        }
    }
}
