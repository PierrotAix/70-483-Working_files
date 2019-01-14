using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Enum
{
    /// <summary>
    /// Question 145 Objective: Implement Data Access Subobjective: Store data and retrieve data from collections.
    /// </summary>
    class Program
    {

        public enum Planet
        {
            Mercury,
            Venus,
            Earth,
            Mars,
            Jupiter,
            Saturn,
            Uranus,
            Neptune,
            PlutoDwarf
        }
        public class SolarSystem
        {
            public static IEnumerable<Planet> PlanetByDistanceFromSun
            {
                get
                {
                    yield return Planet.Mercury;
                    yield return Planet.Venus;
                    yield return Planet.Earth;
                    yield return Planet.Mars;
                    yield return Planet.Jupiter;
                    yield return Planet.Saturn;
                    yield return Planet.Uranus;
                    yield return Planet.Neptune;
                    yield return Planet.PlutoDwarf;
                }
            }
        }

        static void Main(string[] args)
        {

            foreach (Planet p in SolarSystem.PlanetByDistanceFromSun)
            {
                Console.WriteLine(p);
            }

            Console.ReadLine();
        }
    }
}
