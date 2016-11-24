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
        /// <summary>
        /// Implementeras pga IAlive som ISwim och IWalk ärver
        /// </summary>
        public bool CanBreatheAir { get; set; }
     
        /// <summary>
        /// Implementeras pga IWalk interface
        /// </summary>
        /// <param name="howFar"></param>
        public void Walk(int howFar)
        {
            Console.WriteLine("Human walking "+ howFar);
        }

        /// <summary>
        /// Implementeras pga ISwim interface
        /// </summary>
        public void Swim()
        {
            Console.WriteLine("Human swimming");            
        }

        /// <summary>
        /// Publik metod på Human, inget med interface att göra
        /// </summary>
        public void HumanStuff()
        {

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Human ger oss access till de implementerade interfacen samt de exponerade metoder/properties som finns på Human klassen
            var h = new Human();
            h.Swim();
            h.Walk(12);
            h.HumanStuff();
            Console.ReadLine();
        }
    }
}
