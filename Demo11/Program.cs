using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Demo11
{
    public class Person{

    }
    class Program
    {
        static void Main(string[] args)
        {
            var sw = Stopwatch.StartNew();
            //Console.WriteLine("Start");
            //Parallel.Invoke(() => { Thread.Sleep(3000); }, () => { });
            //Console.WriteLine("End");

            Parallel.For(1, 10, (i) => { Console.WriteLine(i); });
            sw.Stop();
            Console.WriteLine(sw.Elapsed.TotalMilliseconds);

            sw.Restart();
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
            }
                sw.Stop();
            Console.WriteLine(sw.Elapsed.TotalMilliseconds);
            //var cts = new CancellationTokenSource();

            //var _ = Task.Run(async () => {
            //    //try
            //    //{
            //        while (true)
            //        {
                        
            //            Console.WriteLine(DateTime.Now);
            //            await Task.Delay(1000);
            //        }
            //    //}
            //    //catch (TaskCanceledException tce) { Console.WriteLine("Task Cancelled"); }
            //    //catch (Exception ex)
            //    //{
            //    //    Console.WriteLine("EX " + ex.Message);
            //    //}
            //}, cts.Token);

            ////Thread.Sleep(5000);
            //cts.CancelAfter(5000);

            //Task.Run(async ()=>{
            //    Console.WriteLine("AAA");
            //    Task.Delay(4000).ContinueWith((a) => { Console.WriteLine("Delay completed"); });
            //    Console.WriteLine("BB");
            //});

            var t1 = Task.Run(async () => { await Task.Delay(1000); Console.WriteLine("t1 done"); });
            var t2 = Task.Run(async () => { await Task.Delay(2000); Console.WriteLine("t2 done"); });
            var t3 = Task.Run(async () => { await Task.Delay(3000); Console.WriteLine("t3 done"); });

            Console.WriteLine("Waiting all");
            Task.WaitAll(t1, t2, t3);
            Console.WriteLine("Waiting all done");

            Task t = new Task(() => { Console.WriteLine("My Task"); });
            t.Start();

            Task.Factory.StartNew(() => { Console.WriteLine("Started with factory"); });
            

            //Console.WriteLine(p.Result.GetType().Name);
            Console.WriteLine("Completed...");
            Console.ReadLine();
        }

        public static async Task<Person> MyService()
        {
            return MyRepository().Result;
            //await Task.Run(() =>
            //{
            //    Console.WriteLine("Going to sleep {0}", DateTime.Now);
            //    System.Threading.Thread.Sleep(3000);
            //    Console.WriteLine("Waking up {0}", DateTime.Now);
            //});
        }

        public static async Task<Person> MyRepository()
        {
            Console.WriteLine("Going to sleep {0}", DateTime.Now);
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Waking up {0}", DateTime.Now);
            return new Person();// Task.FromResult<Person>( new Person());
        }

    }
}
