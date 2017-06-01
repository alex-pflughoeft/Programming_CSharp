using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Certification.Chapter_1.Objective_1_3
{
    class Listing_1_46
    {
        /// <summary>
        /// Using the equality operator
        /// </summary>
        public static void Start()
        {
            int x = 42; int y = 1; int z = 42;

            Console.WriteLine(x == y); // Displays false 
            Console.WriteLine(x == z); // Displays true
        }
    }

    /// <summary>
    /// Boolean OR operator
    /// </summary>
    class Listing_1_47
    {
        public static void Start()
        {
            bool x = true; bool y = false;

            bool result = x || y; Console.WriteLine(result); // Displays True
        }
    }

    /// <summary>
    /// Short-circuiting the OR operator
    /// </summary>
    class Listing_1_48
    {
        public static void Start()
        {
            OrShortCircuit();
        }

        public static void OrShortCircuit()
        {
            bool x = true;
            bool result = x || GetY();
        }

        private static bool GetY()
        {
            Console.WriteLine("This method doesn’t get called");
            return true;
        }
    }

    /// <summary>
    /// Using the AND operator
    /// </summary>
    class Listing_1_49
    {
        public static void Start()
        {
            int value = 42;
            bool result = (0 < value) && (value < 100);
        }
    }

    /// <summary>
    /// Short-circuiting the AND operator
    /// </summary>
    class Listing_1_50
    {
        public static void Start()
        {
            string input = Console.ReadLine();

            bool result = (input != null) && (input.StartsWith("v"));
            // Do something with the result 
        }
    }

    /// <summary>
    /// Using the XOR operator (Exclusive OR)
    /// </summary>
    class Listing_1_51
    {
        public static void Start()
        {
            bool a = true; bool b = false;

            Console.WriteLine(a ^ a); // False 
            Console.WriteLine(a ^ b); // True 
            Console.WriteLine(b ^ b); // False
        }
    }

    /// <summary>
    /// Basic if statement
    /// </summary>
    class Listing_1_52
    {
        public static void Start()
        {
            bool b = true;
            if (b)
                Console.WriteLine("True");
        }
    }

    /// <summary>
    /// An if statement with code block
    /// </summary>
    class Listing_1_53
    {
        public static void Start()
        {
            bool b = true;
            if (b)
            {
                Console.WriteLine("Both these lines");
                Console.WriteLine("Will be executed");
            }
        }
    }

    /// <summary>
    /// Code blocks and scoping
    /// </summary>
    class Listing_1_54
    {
        public static void Start()
        {
            bool b = true;
            if (b)
            {
                int r = 42;
                b = false;
            }

            // r is not accessible 
            // b is now false
        }
    }

    /// <summary>
    /// Using an else statement
    /// </summary>
    class Listing_1_55
    {
        public static void Start()
        {
            bool b = false;

            if (b)
            {
                Console.WriteLine("True");
            }
            else
            {
                Console.WriteLine("False");
            }
        }
    }

    /// <summary>
    /// Using mutiple if/else statements
    /// </summary>
    class Listing_1_56
    {
        public static void Start()
        {
            bool b = false; bool c = true;

            if (b)
            {
                Console.WriteLine("b is true");
            }
            else if (c)
            {
                Console.WriteLine("c is true");
            }
            else
            {
                Console.WriteLine("b and c are false");
            }
        }
    }

    /// <summary>
    /// A more readable nested if statement
    /// </summary>
    class Listing_1_57
    {
        public static void Start()
        {
            bool x = true;
            bool y = false;

            // Here is a nested line for if statements
            //if (x) if (y) F(); else G();

            // When layed out nicely it translates to this
            if (x)
            {
                if (y)
                {
                    //F();
                }
                else
                {
                    //G();
                }
            }
        }
    }

    /// <summary>
    /// The null-coalescing operator
    /// </summary>
    class Listing_1_58
    {
        public static void Start()
        {
            int? x = null;
            int y = x ?? -1;
        }
    }

    /// <summary>
    /// Nesting the null-coalescing operator
    /// </summary>
    class Listing_1_59
    {
        public static void Start()
        {
            int? x = null;
            int? z = null;
            int y = x ?? 
                z ?? 
                -1;
        }
    }

    /// <summary>
    /// The conditional operator
    /// </summary>
    class Listing_1_60
    {
        public static void Start()
        {
            GetValue(true);
        }

        private static int GetValue(bool p)
        {
            if (p)
                return 1;
            else
                return 0;

            // The above could be replaced with this
            return p ? 1 : 0;
        }
    }

    /// <summary>
    /// A complex if statement
    /// </summary>
    class Listing_1_61
    {
        public static void Start()
        {
            Check('y');
        }

        static void Check(char input)
        {
            if (input =='a'         
                || input =='e'         
                || input =='i'         
                || input =='o'         
                || input =='u')
            {
                Console.WriteLine("Input is a vowel");
            }
            else
            {
                Console.WriteLine("Input is a consonant");
            }
        }
    }

    /// <summary>
    /// A switch statement
    /// </summary>
    class Listing_1_62
    {
        public static void Start()
        {
            CheckWithSwitch('y');
        }

        static void CheckWithSwitch(char input)
        {
            switch (input)
            {
                case 'a':
                case 'e':
                case 'i':
                case 'o':
                case 'u':
                    {
                        Console.WriteLine("Input is a vowel");
                        break;
                    }
                case 'y':
                    {
                        Console.WriteLine("Input is sometimes a vowel.");
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Input is a consonant");
                        break;
                    }
            }
        }
    }

    /// <summary>
    /// goto in a switch statement
    /// </summary>
    class Listing_1_63
    {
        public static void Start()
        {
            int i = 1; switch (i)
            {
                case 1:
                    {
                        Console.WriteLine("Case1");
                        goto case 2;
                    }
                case 2:
                    {
                        Console.WriteLine("Case2");
                        break;
                    }
            }

            // Displays // Case 1 // Case 2
        }
    }

    /// <summary>
    /// A basic for loop
    /// </summary>
    class Listing_1_64
    {
        public static void Start()
        {
            int[] values = { 1, 2, 3, 4, 5, 6 };
            for (int index = 0; index < values.Length; index++)
            {
                Console.Write(values[index]);
            }

            // Displays // 12345
        }
    }

    /// <summary>
    /// A for lop with multiple loop variables
    /// </summary>
    class Listing_1_65
    {
        public static void Start()
        {
            int[] values = { 1, 2, 3, 4, 5, 6 };
            for (int x = 0, y = values.Length - 1;
                ((x < values.Length) && (y >= 0));
                x++, y--)
            {
                Console.Write(values[x]);
                Console.Write(values[y]);
            }

            // Displays // 162534435261
        }
    }

    /// <summary>
    /// A for loop with a custom increment
    /// </summary>
    class Listing_1_66
    {
        public static void Start()
        {
            int[] values = { 1, 2, 3, 4, 5, 6 };
            for (int index = 0; index < values.Length; index += 2)
            {
                Console.Write(values[index]);
            }

            // Displays // 135
        }
    }

    /// <summary>
    /// A for loop with a break statement
    /// </summary>
    class Listing_1_67
    {
        public static void Start()
        {
            int[] values = { 1, 2, 3, 4, 5, 6 };

            for (int index = 0; index < values.Length; index++)
            {
                if (values[index] == 4) break;

                Console.Write(values[index]);
            }

            // Displays // 123
        }
    }

    /// <summary>
    /// A for loop with a continue statement
    /// </summary>
    class Listing_1_68
    {
        public static void Start()
        {
            int[] values = { 1, 2, 3, 4, 5, 6 };

            for (int index = 0; index < values.Length; index++)
            {
                if (values[index] == 4) continue;

                Console.Write(values[index]);
            }

            // Displays 
            // 12356
        }
    }

    /// <summary>
    /// Implementing a for loop with a while statement
    /// </summary>
    class Listing_1_69
    {
        public static void Start()
        {
            int[] values = { 1, 2, 3, 4, 5, 6 };

            {
                int index = 0;
                while (index < values.Length)
                {
                    Console.Write(values[index]); index++;
                }
            }
        }
    }

    /// <summary>
    /// do-while loop
    /// </summary>
    class Listing_1_70
    {
        public static void Start()
        {
            do
            {
                Console.WriteLine("Executed once!");
            }
            while (false);
        }
    }

    /// <summary>
    /// foreach loop
    /// </summary>
    class Listing_1_71
    {
        public static void Start()
        {
            int[] values = { 1, 2, 3, 4, 5, 6 };

            foreach (int i in values) { Console.Write(i); }

            // Displays 123456
        }
    }

    /// <summary>
    /// Changing items in a foreach
    /// </summary>
    class Listing_1_72
    {
        class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public static void Start()
        {
            CannotChangeForeachIterationVariable();
        }

        static void CannotChangeForeachIterationVariable()
        {
            var people = new List<Person> { new Person() { FirstName = "John", LastName = "Doe" }, new Person() { FirstName = "Jane", LastName = "Doe" }, };

            foreach (Person p in people)
            {
                p.LastName = "Changed"; // This is allowed         
                                        // p = new Person(); // This gives a compile error     
            }
        }
    }

    /// <summary>
    /// The compiler-generated code for a foreach loop
    /// </summary>
    class Listing_1_73
    {
        class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public static void Start()
        {
            CannotChangeForeachIterationVariable();
        }

        static void CannotChangeForeachIterationVariable()
        {
            var people = new List<Person> { new Person() { FirstName = "John", LastName = "Doe" }, new Person() { FirstName = "Jane", LastName = "Doe" }, };

            List<Person>.Enumerator e = people.GetEnumerator();

            try { Person v; while (e.MoveNext()) { v = e.Current; } }
            finally
            {
                System.IDisposable d = e as System.IDisposable; if (d != null) d.Dispose();
            }
        }
    }

    /// <summary>
    /// goto statement with a label
    /// </summary>
    class Listing_1_74
    {
        public static void Start()
        {
            int x = 3;
            if (x == 3) goto customLabel;
            x++;

            customLabel:
            Console.WriteLine(x); // Displays 3
        }
    }
}
