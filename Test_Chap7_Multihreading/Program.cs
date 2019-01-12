using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test_Chap7_Multihreading
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test01(); // from the book red page 272

            /*   from UTUBE: https://www.youtube.com/watch?v=RhLLxew4-TY&t=797s ---------------- */
            // Exam 70-483: Programming with C# - Objective 1.1

            //Test02(); // Using the ParametereizedThreadStart

            //Test03(); // Stopping a thread 

            //Test04(); // Using the ThreadStaticAttribute

            //Test05(); //Queuing some work to the thread pool

            //Test06(); // Starting a new Task with a lambda expression

            //Test07(); // Starting a new Task without a lambda expression

            //Test08(); // Using a Task that returns a value

            //Test09(); // Adding a continuation

            //Test10(); // Adding a continuation

            //Test11(); // Using Task.WaitAll

            //Test12(); //Using Task.WaitAny

            //Test13(); // Using Parallel.For and Parrallel.Foreach

            //Test14(); // async and await

            //Exam 70-483: Programming with C# - Objective 1.2 Manage multithreading https://www.youtube.com/watch?v=e-5o3ZBWg9Q

            //Test15(); // Listing 1-35 Accessing shared data in a multithreaded application with lock

            //Test16(); //Creating a deadlock

            //Test17(); // Using the Interlocked class

            //Test18(); // Using the CancellationToken

            Test19(); // Setting a timeout on a task


            if (Debugger.IsAttached)
            {
                Console.Write("Press any key to continue . . .");
                Console.ReadKey(true);
            }
            //-----------------------------------------------------------------------
        }

        /// <summary>
        /// Listing 1-45 Setting a timeout on a task
        /// </summary>
        private static void Test19()
        {
            Task longRunning = Task.Run(() =>
               {
                   Thread.Sleep(5000);
               });

            int index = Task.WaitAny(new Task[] { longRunning }, 1000);
            if (index == -1)
            {
                Console.WriteLine("Task time out");
            }

        }
        //-----------------------------------------------------------------------

        /// <summary>
        /// Listing 1-42 Using a CancellationToken
        /// </summary>
        private static void Test18()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;

            Task myTask = Task.Run(() =>
           {
               while (!token.IsCancellationRequested)
               {
                   Console.Write("*");
                   Thread.Sleep(1000);
               }

               token.ThrowIfCancellationRequested();
           },token);


            try
            {
                Console.WriteLine("Press enter to stop task");
                Console.ReadLine();
                cancellationTokenSource.Cancel();
                myTask.Wait();
            }
            catch(AggregateException e)
            {
                Console.WriteLine(e.InnerExceptions[0].Message);
            }


            Console.WriteLine("Press enter to end the program");
            Console.ReadLine();
        }
        //-----------------------------------------------------------------------


        /// <summary>
        /// Listing 1-40  Using the Interlocked clas
        /// </summary>
        private static void Test17()
        {
            int n = 0;

            Task myTask = Task.Run( () =>
                {
                    for (int i = 0; i < 1000000; i++)
                    {
                        //n++;
                        Interlocked.Increment(ref n);
                    }
                } );

            for (int i = 0; i < 1000000; i++)
            {
                //n--;
                Interlocked.Decrement(ref n);
            }
            myTask.Wait();
            Console.WriteLine(n); //0 OK
        }
        //-----------------------------------------------------------------------

        /// <summary>
        /// Listing 1-37 Creating a deadlock
        /// </summary>
        private static void Test16()
        {
            object lockA = new object();
            object lockB = new object();

            Task myTask = Task.Run(() =>
            {
                
                lock (lockA)
                {
                    Thread.Sleep(5000);
                    lock (lockB)
                    {
                        Console.WriteLine("Locked A and B");
                    }
                }
            });

            lock(lockB)
            {
                Thread.Sleep(1000);
                lock (lockA) //stuck here because lock is already locked
                {
                    Console.WriteLine("Locked B and A");
                }
            }

            myTask.Wait();

        }
        //-----------------------------------------------------------------------


        /// <summary>
        /// Listing 1-35 Accessing shared data in a multithreaded application
        /// </summary>
        private static void Test15()
        {
            int n = 0;

            object _lock = new object();

            Task myTask = Task.Run(() =>
            {
                for (int i = 0; i < 1000000; i++)
                {
                    lock (_lock)
                    {
                        n++; //non atomic operation // cannot be interrupted // n = n + 1; //Read + Write
                    }                    
                }
            });

            for (int i = 0; i < 1000000; i++)
            {
                lock (_lock)
                {
                    n--; //non atomic operation
                }               
            }

            myTask.Wait();
            Console.WriteLine(n); //0 OK with lock
        }
        //-----------------------------------------------------------------------


        public static async Task<string> DownLoadContent()
        {
            using (HttpClient client = new HttpClient() )
            {
                string result = await client.GetStringAsync("http://www.microsoft.com");
                return result;
            } 
        }

        /// <summary>
        /// Listing 1-18 async and await
        /// </summary>
        private static void Test14()
        {
            string result = DownLoadContent().Result;
            Console.WriteLine(result);
        }
        //-----------------------------------------------------------------------
         
        /// <summary>
        /// Listing 1-16 Using Parallel.For and Parrallel.Foreach
        /// </summary>
        private static void Test13()
        {
            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine(i);
            //    Thread.Sleep(1000);
            //}

            //paralle version:
            //Parallel.For(0, 10, (i) =>
            //{
            //    Console.WriteLine(i);
            //    Thread.Sleep(1000);
            //});

            int[] myArray = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            //foreach (int x in myArray)
            //{
            //    Console.WriteLine(x);
            //    Thread.Sleep(1000);
            //}
            Parallel.ForEach(myArray, (i) =>
             {
                 Console.WriteLine(i);
                 Thread.Sleep(1000);
             });

            
        }
        //-----------------------------------------------------------------------


        /// <summary>
        /// Listing 1-15 Using Task.WaitAny
        /// </summary>
        private static void Test12()
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
            /*
            2
            1
            3
            Press any key to continue . . . 
             * */
        }

        //-----------------------------------------------------------------------

        /// <summary>
        /// Listing 1-14 Using Task.WaitAll
        /// </summary>
        private static void Test11()
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
                Thread.Sleep(2000);
                Console.WriteLine("2");
                return 1;
            });

            tasks[2] = Task.Run(() =>
            {
                Thread.Sleep(3000);
                Console.WriteLine("3");
                return 1;
            });

            Task.WaitAll(tasks);
            /*
             1
            2
            3
            Press any key to continue . . . 
             * */
        }

        //-----------------------------------------------------------------------

        /// <summary>
        /// Listing 1-11 Adding a continuation
        /// </summary>
        private static void Test10()
        {
            Task<int> t = Task.Run(() =>
            {
                //throw new Exception(); // on crée une exception)
                return 42;
            });

            t.ContinueWith((i) =>
            {
                Console.WriteLine("Faulted");
            },TaskContinuationOptions.OnlyOnFaulted);

            t.ContinueWith((i) =>
            {
                Console.WriteLine("Completed");
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            Console.WriteLine(t.Result);
        }

        //-----------------------------------------------------------------------

        /// <summary>
        /// Linstin 1-10 Adding a continuation
        /// </summary>
        private static void Test09()
        {

            Task<int> t = Task.Run(() =>
            {
                return 42;
            }).ContinueWith( (i) =>
            {
                return i.Result * 2;
            }); //84

            t = t.ContinueWith((i) =>
            {
                return i.Result * 2;
            }); //168

            Console.WriteLine(t.Result);
            //            84
            //Press any key to continue . . .
        }

        //-----------------------------------------------------------------------

        /// <summary>
        /// Listing 1-9  Using a Task that returns a value
        /// </summary>
        private static void Test08()
        {
            Task<int> t = Task.Run(() =>
            {
                return 42;
            });
            Console.WriteLine(t.Result);
            // 42
            //Press any key to continue . . .
        }

        //-----------------------------------------------------------------------

        public static void ThreadMethod()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.Write('*');
            }
        }

        /// <summary>
        /// Listinng 1-8 Starting a new Task without a lambda expression
        /// </summary>
        private static void Test07()
        {
            Task t = Task.Run(action: ThreadMethod);
            t.Wait();
            /*
             ********************************************************************************
              ********************Press any key to continue . . . 
             * */
        }
        //-----------------------------------------------------------------------


        /// <summary>
        /// Listinng 1-8 Starting a new Task
        /// </summary>
        private static void Test06()
        {
            Task t = Task.Run(() =>
           {
               for (int i = 0; i < 100; i++)
               {
                   Console.Write('*');
               }
           });
            t.Wait();
            /*
             ********************************************************************************
              ********************Press any key to continue . . . 
             * */
        }

        //-----------------------------------------------------------------------


        /// <summary>
        /// Listing 1-7 Queuing some work to the thread pool
        /// </summary>
        private static void Test05()
        {
            ThreadPool.QueueUserWorkItem((s) =>
            {
                Console.WriteLine("Working on a thread from the threadpool");
            });
            Console.ReadLine();
        }

        //-----------------------------------------------------------------------
        [ThreadStatic]
        private static int _field;

        /// <summary>
        /// Listing 1-5 Using the ThreadStaticAttribute
        /// </summary>
        private static void Test04()
        {
            Thread t1 = new Thread(new ThreadStart(
                () =>
                {
                    for (int i = 0; i < 10; i++)
                    {
                        _field++;
                        Console.WriteLine("Thread A: {0}", _field);
                    }
                }   ));
            t1.Start();

            Thread t2 = new Thread(new ThreadStart(
                () =>
                {
                    for (int i = 0; i < 10; i++)
                    {
                        _field++;
                        Console.WriteLine("Thread B: {0}", _field);
                    }
                }));
            t2.Start();

            /*
             Thread A: 1
            Thread A: 2
            Thread A: 3
            Thread A: 4
            Thread A: 5
            Thread A: 6
            Thread A: 7
            Thread A: 8
            Thread A: 9
            Thread A: 10
            Thread B: 1
            Thread B: 2
            Thread B: 3
            Thread B: 4
            Thread B: 5
            Thread B: 6
            Thread B: 7
            Thread B: 8
            Thread B: 9
            Thread B: 10
            Press any key to continue . . .
             * */
        }
        //-----------------------------------------------------------------------

        /// <summary>
        /// Listing 1-4 Stopping a thread 
        /// </summary>
        private static void Test03()
        {
            bool stopped = false;

            Thread t = new Thread(new ThreadStart(
                () =>
                {
                    while(!stopped)
                    {
                        Console.WriteLine("Running");
                        Thread.Sleep(1000);
                    }
                    Console.WriteLine("Thread is about to close");
                }));
            t.Start();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            stopped = true;

            t.Join();

            /*
             Press any key to exit
            Running
            Running
            Running
            Running
            Running
            Running
            Running
            Running
            Running
             Press any key to continue . . .
             * */
        }
        //-----------------------------------------------------------------------

        /// <summary>
        /// Using the ParametereizedThreadStart
        /// </summary>
        private static void Test02()
        {
            Thread t = new Thread(new ParameterizedThreadStart(ThreadMethod));
            t.Start(15);
            t.Join();

            /*
             ThreadProc : 0
            ThreadProc : 1
            ThreadProc : 2
            ThreadProc : 3
            ThreadProc : 4
            ThreadProc : 5
            ThreadProc : 6
            ThreadProc : 7
            ThreadProc : 8
            ThreadProc : 9
            ThreadProc : 10
            ThreadProc : 11
            ThreadProc : 12
            ThreadProc : 13
            ThreadProc : 14
            Press any key to continue . . . 
             * */
        }

        public static void ThreadMethod(object o)
        {
            for (int i = 0; i < (int)o; i++)
            {
                Console.WriteLine("ThreadProc : {0}", i);
                Thread.Sleep(0);
            }         
        }

        /// <summary>
        /// -------------------------------------------------------
        /// </summary>
        private static void Test01()
        {
            // We are using Stopwatch to time the code
            Stopwatch sw = Stopwatch.StartNew();
            // Here we call different methods
            // for different ways of running our application.
            if (false)
            {
                RunSequencial();
                /*
                 The result is 20
                We're done in 17350ms!
                Press any key to continue . . . 
                 * */
            }
            else
            {
                RunWithThread();
                /*
                 The result is 20
                We're done in 12428ms!
                Press any key to continue . . .* 
                 * */
            }



            // Print the time it took to run the application.
            Console.WriteLine("We're done in {0}ms!", sw.ElapsedMilliseconds);

        }

        private static void RunWithThread()
        {
            double result = 0d;
            // Create the thread to read from I/O
            var thread = new Thread(() => result = ReadDataFromIO());
            // Start the thread
            thread.Start();
            // Save the result of the calculation into another variable
            double result2 = DoIntensiveCalculations();
            // Wait for the thread to finish
            thread.Join();
            // Calculate the end result
            result += result2;
            // Print the result
            Console.WriteLine("The result is {0}", result);


        }

        private static void RunSequencial()
        {
            double result = 0d;
            // Call the function to read data from I/O
            result += ReadDataFromIO();
            // Add the result of the second calculation
            result += DoIntensiveCalculations();
            // Print the result
            Console.WriteLine("The result is {0}", result);
        }

        static double DoIntensiveCalculations()
        {
            // We are simulating intensive calculations
            // by doing nonsens divisions
            double result = 100000000d;
            var maxValue = Int32.MaxValue;
            for (int i = 1; i < maxValue; i++)
            {
                result /= i;
            }
            return result + 10d;
        }

        static double ReadDataFromIO()
        {
            // We are simulating an I/O by putting the current thread to sleep.
            Thread.Sleep(5000);
            return 10d;
        }
    }
}
