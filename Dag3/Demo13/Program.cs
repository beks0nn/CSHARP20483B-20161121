using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Demo13
{
    class Program
    {
        /// <summary>
        /// Enkel generisk collection som ger oss ett producer/consumer pattern.
        /// Notera att genom att endast tillåta en capacity på 10 så kommer Add att blockas när det finns 10 items i vår collection.
        /// När sedan en consumer tar ett item så kan man lägga till igen.
        /// 
        /// Väljer man att inte sätta en capacity så kommer man att kunna lägga till hur mycket man vill
        /// </summary>
        static BlockingCollection<int> blockingCollection = new BlockingCollection<int>(10);

        static void Main(string[] args)
        {
            //Task som lägger till 2 tal i sekunden
            Task.Run(async () =>
            {
                for (var i = 0; i < 100; i++)
                {
                    blockingCollection.Add(i);
                    Console.WriteLine("Produced: " + i);
                    await Task.Delay(500);
                }
            });

            //En task som endast konsumerar ett värde från vår collection va 15:e sekund, vilket kommer att blocka addering av items då vi har satt capacity till 10.
            //Normalt sett så vill man skriva och läsa (prduce/consume) så snabbt som möjligt, det vi gör här är bara för att förstå hur BlockingCollection<T> fungerar. 
            Task.Run(async () => {
                Console.WriteLine("Starting consumer");
                // Viktigt: När det inte finns några items att läsa kommer GetConsumingEnumerable() att blocka (vänta) på att ett item dyker upp.
                // Detta ska därför göras i en Task så att man inte blockerar ALLT!
                foreach (var num in blockingCollection.GetConsumingEnumerable())
                {
                    await Task.Delay(15000);
                    Console.WriteLine("Consumed: " + num);
                }
                // Hit kommer vi inte då blockingCollection alltid blockar på GetConsumingEnumerable()
                Console.WriteLine("Ending consumer");
            });
            


            Console.ReadLine();
        }
    }
}
