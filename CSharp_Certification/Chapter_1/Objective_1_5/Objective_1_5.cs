using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Certification.Chapter_1.Objective_1_5
{
    /// <summary>
    /// Parsing an invalid number
    /// </summary>
    class Listing_1_88
    {
        public static void Start()
        {
            string s ="NaN";
            int i = int.Parse(s);
        }
    }

    /// <summary>
    /// Catching a FormatException
    /// </summary>
    class Listing_1_89
    {
        public static void Start()
        {
            while (true)
            {
                string s = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(s)) break;
                try
                {
                    int i = int.Parse(s);
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("{0} is not avalid number. Please try again", s);
                }
            }
        }
    }

    /// <summary>
    /// Catching different exception types
    /// </summary>
    class Listing_1_90
    {
        public static void Start()
        {
            string s = Console.ReadLine();

            try { int i = int.Parse(s); }
            catch (ArgumentNullException)
            {
                Console.WriteLine("You need to enter a value");
            }
            catch (FormatException)
            {
                Console.WriteLine("{0} is not avalid number. Please try again", s);
            }
        }
    }

    /// <summary>
    /// Using a finally block
    /// </summary>
    class Listing_1_91
    {
        public static void Start()
        {
            string s = Console.ReadLine(); try { int i = int.Parse(s); }
            catch (ArgumentNullException) { Console.WriteLine("You need to enter a value"); }
            catch (FormatException)
            {
                Console.WriteLine("{0} is not avalid number. Please try again", s);
            }
            finally
            {
                Console.WriteLine("Program complete.");
            }
        }
    }

    /// <summary>
    /// Using Environment.Failfast
    /// </summary>
    class Listing_1_92
    {
        public static void Start()
        {
            string s = Console.ReadLine();

            try
            {
                int i = int.Parse(s);
                if (i == 42) Environment.FailFast("Special number entered");
            }
            finally
            {
                Console.WriteLine("Program complete.");
            }
        }
    }

    /// <summary>
    /// Inspecting an exception
    /// </summary>
    class Listing_1_93
    {
        public static void Start()
        {

        }
    }

    /// <summary>
    /// Throwing an ArgumentNullException
    /// </summary>
    class Listing_1_94
    {
        public static void Start()
        {
            OpenAndParse("");
        }

        public static string OpenAndParse(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException("fileName", "Filename is required");

            return File.ReadAllText(fileName);
        }
    }

    /// <summary>
    /// Rethrowing an exception
    /// </summary>
    class Listing_1_95
    {
        public static void Start()
        {
            try
            {
                // Do something
                //SomeOperation();
            }
            catch (Exception logEx)
            {
                // Log the expection
                // Log(logEx);
                throw; // rethrow the original exception }
            }
        }
    }

    /// <summary>
    /// Throwing a new exception that points to the original one
    /// </summary>
    class Listing_1_96
    {
        public static void Start()
        {
            try
            {
                // Do Something
                //ProcessOrder();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while processing order", ex);
            }
        }
    }

    /// <summary>
    /// Using ExceptionDispatchInfo.Throw
    /// </summary>
    class Listing_1_97
    {
        public static void Start()
        {
            ExceptionDispatchInfo possibleException = null;

            try { string s = Console.ReadLine(); int.Parse(s); } catch (FormatException ex) { possibleException = ExceptionDispatchInfo.Capture(ex); }

            if (possibleException != null) { possibleException.Throw(); }
            // Displays // Unhandled Exception: System.FormatException:  
            // Input string was not in a correct format. 
            //   at System.Number.StringToNumber(String str, NumberStyles options,  
            //         NumberBuffer& number, NumberFormatInfo info, Boolean parseDecimal) 
            //   at System.Number.ParseInt32(String s, NumberStyles style,  
            //         NumberFormatInfo info) 
            //   at System.Int32.Parse(String s) 
            //   at ExceptionHandling.Program.Main() in c:\Users\Wouter\Documents\ 
            //      Visual Studio 2012\Projects\ExamRefProgrammingInCSharp\Chapter1\     
            //         Program.cs:line  17 
            //--- End of stack trace from previous location where exception was thrown --- 
            //   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw() 
            //   at ExceptionHandling.Program.Main() in c:\Users\Wouter\Documents\ 
            //      Visual Studio 2012\Projects\ExamRefProgrammingInCSharp\Chapter1\ 
            //         Program.cs:line 6
        }
    }

    /// <summary>
    /// Creating a custom exception
    /// </summary>
    class Listing_1_98
    {
        public static void Start()
        {

        }

        [Serializable]
        public class OrderProcessingException : Exception, ISerializable
        {
            public OrderProcessingException(int orderId)
            {
                OrderId = orderId;
                this.HelpLink = "http://www.mydomain.com/infoaboutexception";
            }
            public OrderProcessingException(int orderId, string message)
                : base(message)
            {
                OrderId = orderId;
                this.HelpLink = "http://www.mydomain.com/infoaboutexception";
            }

            public OrderProcessingException(int orderId, string message, Exception innerException)
                : base(message, innerException)
            {
                OrderId = orderId;
                this.HelpLink = "http://www.mydomain.com/infoaboutexception";
            }

            protected void EntityOperationException(SerializationInfo info, StreamingContext context)
            {
                OrderId = (int)info.GetValue("OrderId", typeof(int));
            }

            public int OrderId { get; private set; }

            public override void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                base.GetObjectData(info, context);
                info.AddValue("entityId", 1, typeof(int));
            }
        }
    }
}
