using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Certification.Chapter_2.Objective_2_1
{
    /// <summary>
    /// Using the FlagAttribute for an enum
    /// </summary>
    class Listing_2_1
    {
        [Flags]
        enum Days
        {
            None = 0x0,
            Sunday = 0x1,
            Monday = 0x2,
            Tuesday = 0x4,
            Wednesday = 0x8,
            Thursday = 0x10,
            Friday = 0x20,
            Saturday = 0x40
        }

        Days readingDays = Days.Monday | Days.Saturday;
    }

    /// <summary>
    /// Creating a custom struct
    /// </summary>
    class Listing_2_2
    {
        public struct Point
        {
            public int x, y;

            public Point(int p1, int p2)
            {
                x = p1;
                y = p2;
            }
        }
    }

    /// <summary>
    /// Calling a method
    /// </summary>
    class Listing_2_3
    {
        public static void Start()
        {
            System.Console.WriteLine("I’m calling a method!");
        }
    }

    /// <summary>
    /// Creating a method
    /// </summary>
    class Listing_2_4
    {
        class Calculator
        {
            public int Add(int x, int y)
            {
                return x + y;
            }
        }

        public static void Start()
        {
            Calculator calc = new Calculator();

            calc.Add(3, 4);
        }
    }

    class Listing_2_5
    {
        class Customer
        {
            public string FirstName { get; set; }
        }
    }


}
