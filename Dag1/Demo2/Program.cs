using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo2
{
    // måste ärvas för att kunna skapa instans (abstrakt)
    public abstract class Animal
    {
        public bool Carnivore { get; set; }

        public Animal()
        {
            Console.WriteLine("CTOR - Animal");
        }
    }

    // får inte ärva pga sealed
    public sealed class Cat : Animal
    {
        public Cat()
            : base()
        {
            Console.WriteLine("CTOR - Cat");
        }
        //public new bool Carnivore { get; set; }
    }

    // gömd utanför assemblyn
    internal class Lion : Animal
    {

    }

    class Program
    {
        static void Main(string[] args)
        {
            var a = new Lion() { Carnivore = true };
            //a.Carnivore = true;

            Console.ReadLine();
        }
    }
}
