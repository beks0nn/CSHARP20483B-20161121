using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    /// <summary>
    /// Genom att wrappa en blockingcollection kan vi nyttja producer/consumer 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class MyBlockingCollection<T>
    {
        private static BlockingCollection<T> _blockingCollection;

        static MyBlockingCollection()
        {
            Console.WriteLine("Creating new STATIC GENERIC type of " + typeof(T).Name);
            _blockingCollection = new BlockingCollection<T>();

            Task.Run(() =>
            {
                foreach (var t in _blockingCollection.GetConsumingEnumerable())
                {
                    try
                    {
                        Console.WriteLine(t);
                    }
                    catch { }
                }
            });
        }
        
        // definera en metod som kan lägga till objekt av T till vår blocking collection
        public static void Push(T item)
        {            
            _blockingCollection.Add(item);
        }        
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Statiska generiska klasser kommer att få nya instanser vid första användandet av en ny signatur MyBlockingCollection<T>

            //Här skapas en instans av den statiska klassen eftersom signaturen med <int> används första gången.
            MyBlockingCollection<int>.Push(123);
            //Här återanvänds den statiska instansen skapad ovan eftersom <int> redan använts.
            MyBlockingCollection<int>.Push(456);

            //Här skapas en instans av den statiska klassen eftersom signaturen med <string> används första gången.
            MyBlockingCollection<string>.Push("ABC");
            //Här återanvänds den statiska instansen skapad ovan eftersom <string> redan använts.
            MyBlockingCollection<string>.Push("DEF");

            Console.ReadLine();
        }
    }
}
