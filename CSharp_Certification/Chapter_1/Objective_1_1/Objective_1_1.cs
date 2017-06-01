using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Manage program flow
/// </summary>
namespace CSharp_Certification.Chapter_1.Objective_1_1
{
    /// <summary>
    /// Creating a thread with the Thread class
    /// </summary>
    class Listing_1_1
    {
        public static void Start()
        {
            Thread t = new Thread(new ThreadStart(ThreadMethod));
            t.Start();
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("Main thread: Do some work.");
                Thread.Sleep(0);
            }
            t.Join();
        }

        public static void ThreadMethod()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Thread Proc: {0}", i);
                Thread.Sleep(0);
            }
        }
    }

    /// <summary>
    /// Using a background thread
    /// </summary>
    class Listing_1_2
    {
        public static void Start()
        {
            Thread t = new Thread(new ThreadStart(ThreadMethod));
            t.IsBackground = true;
            t.Start();
        }

        public static void ThreadMethod()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Thread Proc: {0}", i);
                Thread.Sleep(1000);
            }
        }
    }

    /// <summary>
    /// Using a ParameterizedThreadStart
    /// </summary>
    class Listing_1_3
    {
        public static void ThreadMethod(object o)
        {
            for (int i = 0; i < (int)o; i++)
            {
                Console.WriteLine("Thread Proc:{0}", i);
                Thread.Sleep(0);
            }
        }

        public static void Start()
        {
            Thread t = new Thread(new ParameterizedThreadStart(ThreadMethod));
            t.Start(5);
            t.Join();
        }
    }

    /// <summary>
    /// Stopping a thread
    /// </summary>
    class Listing_1_4
    {
        public static void Start()
        {
            bool stopped = false;

            Thread t = new Thread(new ThreadStart(() =>
            {
                while (!stopped)
                {
                    Console.WriteLine("Running...");
                    Thread.Sleep(1000);
                }
            }));

            t.Start();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            stopped = true;
            t.Join();
        }
    }

    /// <summary>
    /// Using the ThreadStaticAttribute
    /// </summary>
    class Listing_1_5
    {
        [ThreadStatic]
        public static int _field;

        public static void Start()
        {
            new Thread(() =>
            {
                for (int x = 0; x < 10; x++)
                {
                    _field++; Console.WriteLine("Thread A: {0}", _field);
                }
            }).Start();
            new Thread(() =>
            {

                for (int x = 0; x < 10; x++)
                {
                    _field++; Console.WriteLine("Thread B: {0}", _field);
                }
            }).Start(); Console.ReadKey();
        }
    }

    /// <summary>
    /// Using ThreadLocal<T>
    /// </summary>
    class Listing_1_6
    {
        public static ThreadLocal<int> _field =
            new ThreadLocal<int>(() =>
            {
                return Thread.CurrentThread.ManagedThreadId;
            });

        public static void Start()
        {
            new Thread(() =>
            {
                for (int x = 0; x < _field.Value; x++)
                {
                    Console.WriteLine("Thread A: {0}", x);
                }
            }).Start(); new Thread(() =>
            {
                for (int x = 0; x < _field.Value; x++) { Console.WriteLine("Thread B: {0}", x); }
            }).Start();

            Console.ReadKey();
        }
    }

    /// <summary>
    /// Queuing some work to the thread pool
    /// </summary>
    class Listing_1_7
    {
        public static void Start()
        {
            ThreadPool.QueueUserWorkItem((s) =>
            {
                Console.WriteLine("Working on a thread from thread pool");
            });

            Console.ReadLine();
        }
    }

    /// <summary>
    /// Starting a new Task
    /// </summary>
    class Listing_1_8
    {
        public static void Start()
        {
            Task t = Task.Run(() =>
            {
                for (int x = 0; x < 100; x++)
                {
                    Console.Write('*');
                }
            });

            t.Wait();
        }
    }

    /// <summary>
    /// Using a Task that returns a value
    /// </summary>
    class Listing_1_9
    {
        public static void Start()
        {
            Task<int> t = Task.Run(() =>
            {
                return 42;
            }); Console.WriteLine(t.Result); // Displays 42
        }
    }

    /// <summary>
    /// Adding a continuation
    /// </summary>
    class Listing_1_10
    {
        public static void Start()
        {
            Task<int> t = Task.Run(() =>
            {
                return 42;
            }).ContinueWith((i) =>
            {
                return i.Result * 2;
            });

            Console.WriteLine(t.Result); // Displays 8
        }
    }

    /// <summary>
    /// Scheduling different continuation tasks 
    /// </summary>
    class Listing_1_11
    {
        public static void Start()
        {
            Task<int> t = Task.Run(() => { return 42; });

            t.ContinueWith((i) => { Console.WriteLine("Canceled"); }, TaskContinuationOptions.OnlyOnCanceled);

            t.ContinueWith((i) => { Console.WriteLine("Faulted"); }, TaskContinuationOptions.OnlyOnFaulted);

            var completedTask = t.ContinueWith((i) => { Console.WriteLine("Completed"); }, TaskContinuationOptions.OnlyOnRanToCompletion);

            completedTask.Wait();
        }
    }

    /// <summary>
    /// Attaching child tasks to a parent task
    /// </summary>
    class Listing_1_12
    {
        public static void Start()
        {
            Task<Int32[]> parent = Task.Run(() =>
            {
                var results = new Int32[3];
                new Task(() => results[0] = 0,
                    TaskCreationOptions.AttachedToParent).Start();
                new Task(() => results[1] = 1,
                    TaskCreationOptions.AttachedToParent).Start();
                new Task(() => results[2] = 2,
                    TaskCreationOptions.AttachedToParent).Start();
                return results;
            });

            var finalTask = parent.ContinueWith(parentTask => { foreach (int i in parentTask.Result) Console.WriteLine(i); });

            finalTask.Wait();
        }
    }

    /// <summary>
    /// Using a TaskFactory
    /// </summary>
    class Listing_1_13
    {
        public static void Start()
        {
            Task<Int32[]> parent = Task.Run(() =>
            {
                var results = new Int32[3];
                TaskFactory tf = new TaskFactory(TaskCreationOptions.AttachedToParent,
                    TaskContinuationOptions.ExecuteSynchronously);

                tf.StartNew(() => results[0] = 0);
                tf.StartNew(() => results[1] = 1);
                tf.StartNew(() => results[2] = 2);
                return results;
            });

            var finalTask = parent.ContinueWith(parentTask => { foreach (int i in parentTask.Result) Console.WriteLine(i); });

            finalTask.Wait();
        }
    }

    /// <summary>
    /// Using Task.WaitAll
    /// </summary>
    class Listing_1_14
    {
        public static void Start()
        {
            Task[] tasks = new Task[3];

            tasks[0] = Task.Run(() =>
                        {
                            Thread.Sleep(1000);
                            Console.WriteLine("1");
                            return 1;
                        });
            tasks[1] = Task.Run(() =>
                        {
                            Thread.Sleep(1000);
                            Console.WriteLine("2");
                            return 2;
                        });
            tasks[2] = Task.Run(() =>
                        {
                            Thread.Sleep(1000);
                            Console.WriteLine("3");
                            return 3;
                        });
            Task.WaitAll(tasks);
        }
    }

    /// <summary>
    /// Using Task.WaitAny
    /// </summary>
    class Listing_1_15
    {
        public static void Start()
        {
            Task<int>[] tasks = new Task<int>[3];

            tasks[0] = Task.Run(() => { Thread.Sleep(2000); return 1; });
            tasks[1] = Task.Run(() => { Thread.Sleep(1000); return 2; });
            tasks[2] = Task.Run(() => { Thread.Sleep(3000); return 3; });

            while (tasks.Length > 0)
            {
                int i = Task.WaitAny(tasks);
                Task<int> completedTask = tasks[i];

                Console.WriteLine(completedTask.Result);

                var temp = tasks.ToList();
                temp.RemoveAt(i);
                tasks = temp.ToArray();
            }
        }
    }

    /// <summary>
    /// Using Parallel.For and Parallel.Foreach
    /// </summary>
    class Listing_1_16
    {
        public static void Start()
        {
            Parallel.For(0, 10, i => { Thread.Sleep(1000); });

            var numbers = Enumerable.Range(0, 10); Parallel.ForEach(numbers, i => { Thread.Sleep(1000); });
        }
    }

    /// <summary>
    /// Using Parallel.Break
    /// </summary>
    class Listing_1_17
    {
        public static void Start()
        {
            ParallelLoopResult result = Parallel.For(0, 1000, (int i, ParallelLoopState loopState) =>
            {
                if (i == 500)
                {
                    Console.WriteLine("Breaking loop"); loopState.Break();
                }
                return;
            });
        }
    }

    /// <summary>
    /// async and await
    /// </summary>
    class Listing_1_18
    {
        public static async Task<string> DownloadContent()
        {
            using (HttpClient client = new HttpClient())
            {
                string result = await client.GetStringAsync("http://www.microsoft.com");
                return result;
            }
        }

        public static void Start()
        {
            string result = DownloadContent().Result;
            Console.WriteLine(result);
        }
    }

    /// <summary>
    /// Scalability versus responsiveness
    /// </summary>
    class Listing_1_19
    {
        public Task SleepAsyncA(int millisecondsTimeout)
        {
            return Task.Run(() => Thread.Sleep(millisecondsTimeout));
        }

        public Task SleepAsyncB(int millisecondsTimeout)
        {
            TaskCompletionSource<bool> tcs = null;
            var t = new Timer(delegate { tcs.TrySetResult(true); }, null, -1, -1);
            tcs = new TaskCompletionSource<bool>(t);
            t.Change(millisecondsTimeout, -1);
            return tcs.Task;
        }

        public static void Start()
        {
            // TODO:
        }
    }

    /// <summary>
    /// Using ConfigureAwait
    /// </summary>
    class Listing_1_20
    {
        //private async void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    HttpClient httpClient = new HttpClient();

        //    string content = await httpClient.GetStringAsync("http://www.microsoft.com").ConfigureAwait(false);

        //    Output.Content = content;
        //}

        public static void Start()
        {
            // TODO:
        }
    }

    /// <summary>
    /// Continuing on a thread pool instread of the UI thread
    /// </summary>
    class Listing_1_21
    {
        //private async void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    HttpClient httpClient = new HttpClient();

        //    string content = await httpClient.GetStringAsync("http://www.microsoft.com").ConfigureAwait(false);

        //    using (FileStream sourceStream = new FileStream("temp.html", FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true))
        //    {
        //        byte[] encodedText = Encoding.Unicode.GetBytes(content);
        //        await sourceStream.WriteAsync(encodedText, 0, encodedText.Length).ConfigureAwait(false);
        //    };
        //}

        public static void Start()
        {
            // TODO:
        }
    }

    /// <summary>
    /// Using AsParallel
    /// </summary>
    class Listing_1_22
    {
        public static void Start()
        {
            var numbers = Enumerable.Range(0, 100000000);
            var parallelResult = numbers.AsParallel().Where(i => i % 2 == 0).ToArray();
        }
    }

    /// <summary>
    /// Unordered parallel query
    /// </summary>
    class Listing_1_23
    {
        public static void Start()
        {
            var numbers = Enumerable.Range(0, 10);
            var parallelResult = numbers.AsParallel().Where(i => i % 2 == 0).ToArray();
            foreach (int i in parallelResult)
                Console.WriteLine(i);
        }
    }

    /// <summary>
    /// Ordered parallel query
    /// </summary>
    class Listing_1_24
    {
        public static void Start()
        {
            var numbers = Enumerable.Range(0, 10);
            var parallelResult = numbers.AsParallel().AsOrdered().Where(i => i % 2 == 0).ToArray();

            foreach (int i in parallelResult)
                Console.WriteLine(i);
        }
    }

    /// <summary>
    /// Making a parallel query sequential
    /// </summary>
    class Listing_1_25
    {
        public static void Start()
        {
            var numbers = Enumerable.Range(0, 20);

            var parallelResult = numbers.AsParallel().AsOrdered().Where(i => i % 2 == 0).AsSequential();

            foreach (int i in parallelResult.Take(5))
                Console.WriteLine(i);
        }
    }

    /// <summary>
    /// Using ForAll
    /// </summary>
    class Listing_1_26
    {
        public static void Start()
        {
            var numbers = Enumerable.Range(0, 20);

            var parallelResult = numbers.AsParallel().Where(i => i % 2 == 0);

            parallelResult.ForAll(e => Console.WriteLine(e));
        }
    }

    /// <summary>
    /// Catching AggregateException
    /// </summary>
    class Listing_1_27
    {
        public static void Start()
        {
            var numbers = Enumerable.Range(0, 20);
            try
            {
                var parallelResult = numbers.AsParallel().Where(i => IsEven(i)); parallelResult.ForAll(e => Console.WriteLine(e));
            }
            catch (AggregateException e) { Console.WriteLine("There where {0} exceptions", e.InnerExceptions.Count); }
        }

        public static bool IsEven(int i)
        {
            if (i % 10 == 0)
                throw new ArgumentException("i");
            return i % 2 == 0;
        }
    }

    /// <summary>
    /// Using BlockingCollection<T>
    /// </summary>
    class Listing_1_28
    {
        public static void Start()
        {
            BlockingCollection<string> col = new BlockingCollection<string>();
            Task read = Task.Run(() =>
            {
                while (true)
                {
                    Console.WriteLine(col.Take());
                }
            });
            Task write = Task.Run(() =>
            {
                while (true)
                {
                    string s = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(s)) break;
                    col.Add(s);
                }
            });
            write.Wait();
        }
    }

    /// <summary>
    /// Using ConcurrentBag
    /// </summary>
    class Listing_1_29
    {
        public static void Start()
        {
            BlockingCollection<string> col = new BlockingCollection<string>();

            Task read = Task.Run(() =>
            {
                foreach (string v in col.GetConsumingEnumerable())
                    Console.WriteLine(v);
            });
        }
    }

    /// <summary>
    /// Enumerating a ConcurrentBag
    /// </summary>
    class Listing_1_30
    {
        public static void Start()
        {
            ConcurrentBag<int> bag = new ConcurrentBag<int>();

            bag.Add(42); bag.Add(21);

            int result; if (bag.TryTake(out result)) Console.WriteLine(result);

            if (bag.TryPeek(out result)) Console.WriteLine("There is a next item: {0}", result);
        }
    }

    /// <summary>
    /// Enumerating a ConcurrentBag
    /// </summary>
    class Listing_1_31
    {
        public static void Start()
        {
            ConcurrentBag<int> bag = new ConcurrentBag<int>();
            Task.Run(() =>
            {
                bag.Add(42);
                Thread.Sleep(1000);
                bag.Add(21);
            });
            Task.Run(() =>
            {
                foreach (int i in bag)
                    Console.WriteLine(i);
            }).Wait();
            // Displays 
            // 42
        }
    }

    /// <summary>
    /// Using a ConcurrentStack
    /// </summary>
    class Listing_1_32
    {
        public static void Start()
        {
            ConcurrentStack<int> stack = new ConcurrentStack<int>();

            stack.Push(42);

            int result; if (stack.TryPop(out result)) Console.WriteLine("Popped: {0}", result);

            stack.PushRange(new int[] { 1, 2, 3 });

            int[] values = new int[2]; stack.TryPopRange(values);

            foreach (int i in values) Console.WriteLine(i);

            // Popped: 42 // 3 // 2
        }
    }

    /// <summary>
    /// Using a ConcurrentQueue
    /// </summary>
    class Listing_1_33
    {
        public static void Start()
        {
            ConcurrentQueue<int> queue = new ConcurrentQueue<int>(); queue.Enqueue(42);

            int result;
            if (queue.TryDequeue(out result))
                Console.WriteLine("Dequeued: {0}", result);

            // Dequeued: 42
        }
    }

    /// <summary>
    /// Using a ConcurrentDictionary
    /// </summary>
    class Listing_1_34
    {
        public static void Start()
        {
            var dict = new ConcurrentDictionary<string, int>();

            if (dict.TryAdd("k1", 42))
            {
                Console.WriteLine("Added");
            }
            if (dict.TryUpdate("k1", 21, 42))
            {
                Console.WriteLine("42 updated to 21");
            }

            dict["k1"] = 42; // Overwrite unconditionally 

            int r1 = dict.AddOrUpdate("k1", 3, (s, i) => i * 2);
            int r2 = dict.GetOrAdd("k2", 3);
        }
    }
}
