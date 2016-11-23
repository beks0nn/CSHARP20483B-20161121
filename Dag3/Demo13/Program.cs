using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo13
{
    class Program
    {
        static BlockingCollection<int> blockingCollection = new BlockingCollection<int>(10);
        static void Main(string[] args)
        {
            Task.Run(async () => { 
                for(var i = 0; i < 100; i++){
                    blockingCollection.Add(i);
                    Console.WriteLine("Produced: " + i);
                    await Task.Delay(500);
                }            
            });

            Task.Run(async () => {
                foreach (var num in blockingCollection.GetConsumingEnumerable())
                {
                    await Task.Delay(15000);
                    Console.WriteLine("Consumed: " + num);
                }            
            });
            


            Console.ReadLine();
        }
    }
}
