using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo12
{
    public interface IAlive
    {
        bool CanBreatheAir { get; set; }
    }

    public interface IWalk
    {
        void Walk(int howFarInKm);
    }

    public interface ISwim : IAlive
    {
        void Swim();
    }

    public class Human : IWalk, ISwim
    {
        public bool CanBreatheAir { get; set; }
     
        public void Walk(int howFar)
        {
            Console.WriteLine("Human walking "+ howFar);
        }

        public void Swim()
        {
            Console.WriteLine("Human swimming");            
        }

        public void HumanStuff()
        {

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var h = new Human();
            h.Swim();
            h.Walk(12);
            h.HumanStuff();
            Console.ReadLine();
        }
    }
}
