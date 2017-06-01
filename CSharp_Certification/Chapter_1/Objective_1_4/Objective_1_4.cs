using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Certification.Chapter_1.Objective_1_4
{
    /// <summary>
    /// Using a delegate
    /// </summary>
    class Listing_1_75
    {
        public delegate int Calculate(int x, int y);

        public int Add(int x, int y) { return x + y; }
        public int Multiply(int x, int y) { return x * y; }

        public void UseDelegate()
        {
            Calculate calc = Add; Console.WriteLine(calc(3, 4)); // Displays 7 

            calc = Multiply; Console.WriteLine(calc(3, 4)); // Displays 12
        }

        public static void Start()
        {
            Listing_1_75 listing = new Listing_1_75();
            listing.UseDelegate();
        }
    }

    /// <summary>
    /// A multicast delegate
    /// </summary>
    class Listing_1_76
    {
        public void MethodOne() { Console.WriteLine("Method One"); }

        public void MethodTwo() { Console.WriteLine("Method Two"); }

        public delegate void Del();

        public void Multicast()
        {
            Del d = MethodOne; d += MethodTwo;

            d();
        }
        // Displays 
        // MethodOne 
        // MethodTwo

        public static void Start()
        {
            Listing_1_76 listing = new Listing_1_76();
            listing.Multicast();
        }
    }

    /// <summary>
    /// Covariance with delegates
    /// </summary>
    class Listing_1_77
    {
        public delegate TextWriter CovarianceDel();
        public StreamWriter MethodStream() { return null; }
        public StringWriter MethodString() { return null; }

        public void Process()
        {
            CovarianceDel del;
            del = MethodStream;
            del = MethodString;
        }

        public static void Start()
        {
            Listing_1_77 listing = new Listing_1_77();
            listing.Process();
        }
    }

    /// <summary>
    /// Contravariance with delegates
    /// </summary>
    class Listing_1_78
    {
        void DoSomething(TextWriter tw) { }
        public delegate void ContravarianceDel(StreamWriter tw);

        public void Process()
        {
            ContravarianceDel del = DoSomething;
        }

        public static void Start()
        {
            Listing_1_78 listing = new Listing_1_78();
            listing.Process();
        }
    }

    /// <summary>
    /// Lambda expression to create a delegate
    /// </summary>
    class Listing_1_79
    {
        public delegate int Calculate(int x, int y);

        public void Process()
        {
            Calculate calc = (x, y) => x + y;
            Console.WriteLine(calc(3, 4));// Displays 7 
            calc = (x, y) => x * y;
            Console.WriteLine(calc(3, 4)); // Displays 12
        }

        public static void Start()
        {
            Listing_1_79 listing = new Listing_1_79();
            listing.Process();
        }
    }

    /// <summary>
    /// Creating a lambda expression with multiple statements
    /// </summary>
    class Listing_1_80
    {
        public delegate int Calculate(int x, int y);

        public void Process()
        {
            Calculate calc = (x, y) => 
            {
                Console.WriteLine("Adding numbers");
                return x + y;
            };

            int result = calc(3, 4);
            // Displays
            // Adding numbers
        }

        public static void Start()
        {
            Listing_1_80 listing = new Listing_1_80();
            listing.Process();
        }
    }

    /// <summary>
    /// Using the Action delegate
    /// </summary>
    class Listing_1_81
    {
        public static void Start()
        {
            Action<int, int> calc = (x, y) => 
            {
                Console.WriteLine(x + y);
            };

            calc(3, 4); // Displays 7
        }
    }

    /// <summary>
    /// Using an Action to expose an event
    /// </summary>
    class Listing_1_82
    {
        public class Pub
        {
            public Action OnChange { get; set; }

            public void Raise() { if (OnChange != null) { OnChange(); } }
        }

        public static void Start()
        {
            Pub p = new Pub();
            p.OnChange += () => Console.WriteLine("Event raised to method 1");
            p.OnChange += () => Console.WriteLine("Event raised to method 2");
            p.Raise();
        }
    }

    /// <summary>
    /// Using the event keyword
    /// </summary>
    class Listing_1_83
    {
        public class Pub
        {
            public event Action OnChange = delegate { };

            public void Raise() { OnChange(); }
        }

        public static void Start()
        {
            // TODO:
        }
    }

    /// <summary>
    /// Custom event arguments
    /// </summary>
    class Listing_1_84
    {
        public class MyArgs : EventArgs
        {
            public MyArgs(int value) { Value = value; }

            public int Value { get; set; }
        }

        public class Pub
        {
            public event EventHandler<MyArgs> OnChange = delegate { };

            public void Raise() { OnChange(this, new MyArgs(42)); }
        }

        public static void Start()
        {
            Pub p = new Pub();

            p.OnChange += (sender, e) => Console.WriteLine("Event raised: {0}", e.Value);

            p.Raise();
        }
    }

    /// <summary>
    /// Custom event accessor
    /// </summary>
    class Listing_1_85
    {
        public class MyArgs : EventArgs
        {
            public MyArgs(int value) { Value = value; }

            public int Value { get; set; }
        }

        public class Pub
        {
            private event EventHandler<MyArgs> onChange = delegate { };
            public event EventHandler<MyArgs> OnChange
            {
                add
                {
                    lock (onChange)
                    {
                        onChange += value;
                    }
                }
                remove
                {
                    lock 
                        (onChange)
                    {
                        onChange -= value;
                    }
                }
            }

            public void Raise() { onChange(this, new MyArgs(42)); }
        }

        public static void Start()
        {
            // TODO:
        }
    }

    /// <summary>
    /// Exception when raising an event
    /// </summary>
    class Listing_1_86
    {
        public class Pub
        {
            public event EventHandler OnChange = delegate { };
            public void Raise() { OnChange(this, EventArgs.Empty); }
        }

        public static void Start()
        {
            Pub p = new Pub();

            p.OnChange += (sender, e) => Console.WriteLine("Subscriber 1 called");

            p.OnChange += (sender, e) => { throw new Exception(); };

            p.OnChange += (sender, e) => Console.WriteLine("Subscriber 3 called");
            p.Raise();
        }
    }

    /// <summary>
    /// Manually raising events with exception handling
    /// </summary>
    class Listing_1_87
    {
        public class Pub
        {
            public event EventHandler OnChange = delegate { }; public void Raise()
            {
                var exceptions = new List<Exception>();

                foreach (Delegate handler in OnChange.GetInvocationList())
                {
                    try
                    {
                        handler.DynamicInvoke(this, EventArgs.Empty);
                    }
                    catch (Exception ex)
                    {
                        exceptions.Add(ex);
                    }
                }

                if (exceptions.Any()) { throw new AggregateException(exceptions); }
            }
        }

        public static void Start()
        {
            Pub p = new Pub();

            p.OnChange += (sender, e) => Console.WriteLine("Subscriber 1 called");

            p.OnChange += (sender, e) => { throw new Exception(); };

            p.OnChange += (sender, e) => Console.WriteLine("Subscriber 3 called");

            try { p.Raise(); } catch (AggregateException ex) { Console.WriteLine(ex.InnerExceptions.Count); }
        }
    }
}
