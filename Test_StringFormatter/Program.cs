using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_StringFormatter
{
    class Program
    {
        /// <summary>
        /// Question 92 Objective: Create and Use Types Subobjective: Manipulate string
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.SetPrincipalPolicy(System.Security.Principal.PrincipalPolicy.WindowsPrincipal);

            var num = new PhoneNumber { Digits = "8115550100" };
            var formattedString = String.Format(new PhoneNumberFormatter(),"{0}", num);

            Console.WriteLine($"Le numéro inital {num.Digits} a éte formaté en {formattedString}");

            Console.ReadLine();
            //Le numéro inital 8115550100 a éte formaté en (811)555-0100

        }

        public class PhoneNumber
        {
            private string _digits;

            public string Digits
            {
                get { return this._digits; }
                set
                {
                    if (value.Length != 10) throw new ArgumentException();
                    this._digits = value;
                }
            }
        }

        public class PhoneNumberFormatter : ICustomFormatter, IFormatProvider
        {


            public object GetFormat(Type formatType)
            {
                if (formatType == typeof(ICustomFormatter))
                    return this;
                else
                    return null;
            }

            public string Format(string format, object arg, IFormatProvider formatProvider)
            {
                if (arg is PhoneNumber)
                {
                    var num = (PhoneNumber)arg;
                    return "(" + num.Digits.Substring(0, 3) + ")" + num.Digits.Substring(3, 3) +
                        "-" + num.Digits.Substring(6, 4);
                }
                else if (string.IsNullOrEmpty(format)) return arg.ToString();
                else return String.Format("{0;" + format + "}", arg);
            }
        }
    }
}
