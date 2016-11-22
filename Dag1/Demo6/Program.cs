using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo6
{
    public class Person
    {
        public string Name{ get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Person class extension
            var p = new Person();
            p.Name = "AA";
            p.DoCoolStuff("asasdasd");
            Console.WriteLine(p.Name);

            //Ienumerable extension
            var l = new List<int>();
            l.ListExtension<int,string>("asdasd");

            Console.ReadLine();
        }
    }

    public static class MyExtensions
    {
        /// <summary>
        /// Person extension genom "this" som key före första parametern
        /// </summary>
        /// <param name="p"></param>
        /// <param name="s"></param>
        public static void DoCoolStuff(this Person p, string s)
        {
            p.Name = s;
        }

        /// <summary>
        /// Generisk extension
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TK"></typeparam>
        /// <param name="list"></param>
        /// <param name="v"></param>
        public static void ListExtension<T, TK>(this IEnumerable<T> list, TK v)
        {
        }
    }
}
