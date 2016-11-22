using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public abstract class Vehicle
    {
        public string RegNr { get; protected set; }
        public string Type { get; protected set; }
        public virtual string DisplayInfo()
        {
            return "No info available, override the method";
        }
        public Vehicle(){}

        public Vehicle(string regnr, string type="unknown")
        {
            this.RegNr = regnr;
            this.Type = type;
        }
    }
    public class Car : Vehicle
    {
        public int Tires { get; set; }
        public Car(string regnr, int tires):base(regnr)
        {
            this.Tires = tires;
        }
        public override string DisplayInfo()
        {
            return string.Format("My car has regnr {0} and {1} tires",this.RegNr, this.Tires);
        }
    }

    public class Plane : Vehicle
    {
        public Plane(string regnr)
            : base(regnr, "PLANE")
        {

        }       
    }

    class Program
    {


        static void Main(string[] args)
        {
            var volvo = new Car("ABC123", 4);
            Console.WriteLine(volvo.DisplayInfo());

            var saab = new Plane("XYZ123");
            Console.WriteLine(saab.DisplayInfo());

            Console.ReadLine();
        }
    }
}
