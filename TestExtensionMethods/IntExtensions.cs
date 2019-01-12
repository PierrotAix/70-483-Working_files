using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestExtensionMethods
{
    public static class IntExtensions
    {
        public static int Half(this int source)
        {
            return source / 2;
        }

        public static int AddFive(this int source)
        {
            return source + 5;
        }
    }
}
