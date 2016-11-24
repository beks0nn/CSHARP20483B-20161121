using RepositoryPatternSample.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternSample
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var unitOfWork = new UnitOfWork(new ZooContext()))
            {
                // Example1
                var animals = unitOfWork.Animals.GetMostDangerousAnimals(3);
                foreach(var a in animals)
                {
                    Console.WriteLine(a.Name + ", " + a.Age);
                }
                Console.ReadLine();                
            }
        }
    }
}
