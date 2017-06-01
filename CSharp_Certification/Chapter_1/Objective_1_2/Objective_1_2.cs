using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Manage multithreading
/// </summary>
namespace CSharp_Certification.Chapter_1.Objective_1_2
{
    /// <summary>
    /// Acessing shared data in a multithreaded application
    /// </summary>
    class Listing_1_35
    {
        public static void Start()
        {
            int n = 0; var up = Task.Run(() =>
            {
                for (int i = 0; i < 1000000; i++)
                    n++;
            });
            for (int i = 0; i < 1000000; i++)
                n--;

            up.Wait();
            Console.WriteLine(n);
        }
    }

    /// <summary>
    /// Using the lock keyword
    /// </summary>
    class Listing_1_36
    {
        public static void Start()
        {
            int n = 0;

            object _lock = new object();

            var up = Task.Run(() =>
            {
                for (int i = 0; i < 1000000; i++)
                    lock (_lock)
                        n++;
            });

            for (int i = 0; i < 1000000; i++)
                lock (_lock)
                    n--;

            up.Wait();
            Console.WriteLine(n);
        }
    }

    /// <summary>
    /// Creating a deadlock
    /// </summary>
    class Listing_1_37
    {
        public static void Start()
        {
            object lockA = new object();
            object lockB = new object();

            var up = Task.Run(() =>
            {
                lock (lockA)
                {
                    Thread.Sleep(1000);
                    lock (lockB)
                    {
                        Console.WriteLine("Locked A and B");
                    }
                }
            });

            lock (lockB)
            {
                lock (lockA)
                {
                    Console.WriteLine("Locked B and A");
                }
            }
            up.Wait();
        }
    }

    /// <summary>
    /// Generated code from a lock statement
    /// </summary>
    class Listing_1_38
    {
        public static void Start()
        {
            object gate = new object();
            bool __lockTaken = false;
            try
            {
                Monitor.Enter(gate, ref __lockTaken);
            }
            finally
            {
                if (__lockTaken)
                    Monitor.Exit(gate);
            }
        }
    }

    /// <summary>
    /// A potential problem with multithreaded code
    /// </summary>
    class Listing_1_39
    {
        public static void Start()
        {
            // TODO: Finish me!
        }
    }

    /// <summary>
    /// Using the Interlocked class
    /// </summary>
    class Listing_1_40
    {
        public static void Start()
        {
            int n = 0;
            var up = Task.Run(() =>
            {
                for (int i = 0; i < 1000000; i++)
                    Interlocked.Increment(ref n);
            });
            for (int i = 0; i < 1000000; i++)
                Interlocked.Decrement(ref n);

            up.Wait(); Console.WriteLine(n);
        }
    }

    /// <summary>
    /// Compare and exchange as a nonatomic operation
    /// </summary>
    class Listing_1_41
    {
        static int value = 1;

        public static void Start()
        {
            Task t1 = Task.Run(() =>
            {
                if (value == 1)
                {                 // Removing the following line will change the output                 
                    Thread.Sleep(1000);
                    value = 2;
                }
            }); Task t2 = Task.Run(() => { value = 3; }); Task.WaitAll(t1, t2); Console.WriteLine(value); // Displays 2 
        }
    }

    /// <summary>
    /// Using a CancellationToken
    /// </summary>
    class Listing_1_42
    {
        public static void Start()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(); CancellationToken token = cancellationTokenSource.Token;

            Task task = Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    Console.Write("*"); Thread.Sleep(1000);
                }
            }, token);

            Console.WriteLine("Press enter to stop the task"); Console.ReadLine(); cancellationTokenSource.Cancel();

            Console.WriteLine("Press enter to end the application"); Console.ReadLine();
        }
    }

    /// <summary>
    /// Throwing OperationCanceledException
    /// </summary>
    class Listing_1_43
    {
        public static void Start()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(); CancellationToken token = cancellationTokenSource.Token;
            Task task = Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    Console.Write("*"); Thread.Sleep(1000);
                }

                token.ThrowIfCancellationRequested();
            }, token);

            try
            {
                Console.WriteLine("Press enter to stop the task"); Console.ReadLine();

                cancellationTokenSource.Cancel(); task.Wait();
            }
            catch (AggregateException e) { Console.WriteLine(e.InnerExceptions[0].Message); }
            Console.WriteLine("Press enter to end the application"); Console.ReadLine();
        }
    }

    /// <summary>
    /// Adding a continuation for canceled tasks
    /// </summary>
    class Listing_1_44
    {
        public static void Start()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;
            Task task = Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    Console.Write("*");
                    Thread.Sleep(1000);
                }
                throw new OperationCanceledException();
            }, token).ContinueWith((t) => { t.Exception.Handle((e) => true); Console.WriteLine("You have canceled the task"); }, TaskContinuationOptions.OnlyOnCanceled);
        }
    }

    /// <summary>
    /// Setting a timeout on a task
    /// </summary>
    class Listing_1_45
    {
        public static void Start()
        {
            Task longRunning = Task.Run(() => { Thread.Sleep(10000); });

            int index = Task.WaitAny(new[] { longRunning }, 1000);

            if (index == -1) Console.WriteLine("Task timed out");
        }
    }
}
