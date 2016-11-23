using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Demo11
{    
    class Program
    {        
        static void Main(string[] args)
        {
            //Olika sätt att starta en Task
            StartingTasksExample().Wait();

            // Task.Delay med Token är det som stoppas loopen!!!
            CancellationTokenExample().Wait();
            
            WaitAnyExample();

            //Notera skillnaden mellan WaitAll och Wait individuellt
            WaitAllVsWaitIndividualExample().Wait();

            // Kör actions parallellt
            
            ParallelExample(16);
            // Actual Cores
            ParallelExample(Environment.ProcessorCount / 2);
            // Sekventiellt
            ParallelExample(1);
            // Obegränsat!
            ParallelExample(-1);

            Console.WriteLine("Completed...");
            Console.ReadLine();
        }

        public static void ParallelExample(int parallel)
        {
            Console.WriteLine("***ParallelExample With Parallel "+parallel+"***");
            var myTasks = new List<Action>(1000);
            for(var i = 0; i < 1000; i++)
            {
                var s = "Task " + i;
                myTasks.Add(() => 
                {
                    var sw = Stopwatch.StartNew();

                    var r = 0;
                    for (var x = 0; x < 1000000; x++)
                    {
                        r += x;
                    }
                    sw.Stop();
                    //Console.WriteLine("Task " + s + " with parallell " + parallel + " took " + sw.Elapsed.TotalMilliseconds);
                });
            }
            var swTot = Stopwatch.StartNew();
            var options = new ParallelOptions { MaxDegreeOfParallelism = parallel };
            Parallel.Invoke(myTasks.ToArray());
            Console.WriteLine("All tasks with parallell " + parallel + " took " + swTot.Elapsed.TotalMilliseconds);
        }

        public static async Task StartingTasksExample()
        {
            Console.WriteLine("***StartingTasksExample***");
            await Task.Run(()=> { Console.WriteLine("Task started with Task.Run"); });

            await Task.Factory.StartNew(() => { Console.WriteLine("Task started with Task.Factory.StartNew"); });

            new Task(()=> { Console.WriteLine("Task started with Task.Run"); }).Start();
        }

        public static async Task CancellationTokenExample()
        {
            Console.WriteLine("***CancellationTokenExample***");
            // Token will be cancelled after 5 sec
            var cts = new CancellationTokenSource(5000);
            //Start a new task and wait for it. Will be cancelled after 5 sec
            await Task.Run(async () =>
            {
                try
                {
                    while (true)
                    {

                        Console.WriteLine(DateTime.Now);
                        //if (cts.IsCancellationRequested)
                        //    break;
                        await Task.Delay(1000, cts.Token);
                    }
                }
                catch (TaskCanceledException tce) { Console.WriteLine("Task Cancelled"); }
                catch (Exception ex)
                {
                    Console.WriteLine("EX " + ex.Message);
                }
            }, cts.Token);
        }

        /// <summary>
        /// Will wait for all tasks to complete
        /// </summary>
        public static void WaitAnyExample()
        {
            Console.WriteLine("***WaitAnyExample***");
            var t1 = Task.Run(async () => { await Task.Delay(1000); Console.WriteLine("t1 done"); });
            var t2 = Task.Run(async () => { await Task.Delay(2000); Console.WriteLine("t2 done"); });
            var t3 = Task.Run(async () => { await Task.Delay(3000); Console.WriteLine("t3 done"); });

            Console.WriteLine("Waiting any");
            Task.WaitAny(t1, t2, t3);
            Console.WriteLine("Waiting any done");
        }

        /// <summary>
        /// Will only wait until any of the tasks in the array is completed
        /// </summary>
        public static async Task WaitAllVsWaitIndividualExample()
        {
            Console.WriteLine("***WaitAllExample***");

            Console.WriteLine("Waiting all");
            var sw = Stopwatch.StartNew();

            var t1 = Task.Run(async () => { await Task.Delay(1000); Console.WriteLine("t1 done"); });
            var t2 = Task.Run(async () => { await Task.Delay(2000); Console.WriteLine("t2 done"); });
            var t3 = Task.Run(async () => { await Task.Delay(3000); Console.WriteLine("t3 done"); });


            Task.WaitAll(t1, t2, t3);
            sw.Stop();
            Console.WriteLine("Waiting all done in {0}", sw.Elapsed.TotalMilliseconds);

            Console.WriteLine("Waiting individually");
            sw.Restart();
            await Task.Run(async () => { await Task.Delay(1000); Console.WriteLine("t1 done"); });
            await Task.Run(async () => { await Task.Delay(2000); Console.WriteLine("t2 done"); });
            await Task.Run(async () => { await Task.Delay(3000); Console.WriteLine("t3 done"); });
            
            sw.Stop();
            Console.WriteLine("Waiting individually done in {0}", sw.Elapsed.TotalMilliseconds);
        }
        
    }
}
