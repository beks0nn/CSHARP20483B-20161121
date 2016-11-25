using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SemaphoreDemo
{
   
    class Program
    {
        // System-Wide semaphore that accepts 2 concurrent threads 
        static Semaphore semaphore = new Semaphore(1, 1,"SEMAPHOREDEMO");

        // Appdomain semaphore that accepts 2 concurrent threads 
        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(2, 2);
        static void Main(string[] args)
        {

#if DEBUG
            Console.WriteLine("THIS CODE IS COMPILED & EXECUTED SINCE DEBUG IS DEFINED");
#else
            Console.WriteLine("THIS CODE IS COMPILED & EXECUTED SINCE DEBUG IS NOT DEFINED");
#endif

            #region Sempahore Slim
            // Start 8 actions. All will start, but block so that only 2 (in this case) can access the semaphore area at the same time.
            var semaphoreslimTasks = new List<Task>();
            for (var i = 0; i < 8; i++)
            {
                Console.WriteLine("Starting Action " + i);
                var s = i.ToString();
                semaphoreslimTasks.Add(Task.Run(() => DoSemaphoreSlimStuff(s)));
            }

            Task.WaitAll(semaphoreslimTasks.ToArray());
            Console.WriteLine("All semaphoreslim tasks completed");
            #endregion

            #region Semaphore

            // Start 8 actions. All will start, but block so that only 2 (in this case) can access the semaphore area at the same time.
            // This semaphore is system wide so that it will block access over process... To test this start several instances of the exe and you will see that it will block between processes!
            var semaphoreTasks = new List<Task>();
            for (var i = 0; i < 8; i++)
            {
                Console.WriteLine("Starting Action " + i);
                var s = i.ToString();
                semaphoreTasks.Add(Task.Run(() => DoSemaphoreStuff(s)));
            }
            Task.WaitAll(semaphoreTasks.ToArray());
            Console.WriteLine("All semaphore tasks completed");

            #endregion

            Console.ReadLine();
        }

        [Conditional("DEBUG")]
        public static void DoSemaphoreSlimStuff(string actionNr)
        {
            Console.WriteLine("Action nr {0} is waiting in semaphoreslim land for threadid {1}", actionNr,Thread.CurrentThread.ManagedThreadId);
            semaphoreSlim.Wait();
            Console.WriteLine("Action nr {0} was granted access to semaphoreslim land for threadid {1}",actionNr, Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(5000);
            Console.WriteLine("Action nr {0} work done in semaphoreslim land, releasing threadid {1}", actionNr, Thread.CurrentThread.ManagedThreadId);
            semaphoreSlim.Release();
        }

        [Conditional("DEBUG")]
        public static void DoSemaphoreStuff(string actionNr)
        {
            Console.WriteLine("Action nr {0} is waiting in semaphore land for threadid {1}", actionNr, Thread.CurrentThread.ManagedThreadId);
            semaphore.WaitOne();
            Console.WriteLine("Action nr {0} was granted access to semaphore land for threadid {1}", actionNr, Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(5000);
            Console.WriteLine("Action nr {0} work done in semaphore land, releasing threadid {1}", actionNr, Thread.CurrentThread.ManagedThreadId);
            semaphore.Release();
        }
    }
}
