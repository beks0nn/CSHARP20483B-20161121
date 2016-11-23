using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    public static class MyBlockingCollection<T>
    {
        // TODO: declare blocking collection av typen T
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

            MyBlockingCollection<int>.Push(123);
            MyBlockingCollection<string>.Push("ABC");
            
            Console.ReadLine();
        }
    }
}
