using RepositoryPatternSample.Core.DomainModel;
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
                // Example1 - Get data from our custom method defined on the IAnimalRepository interface
                var animals = unitOfWork.Animals.GetMostDangerousAnimals(3);
                foreach(var a in animals)
                {
                    Console.WriteLine(a.Name + ", " + a.Age);
                }

                // Example 3 - Add a dangerous animal with scale 8
                var newAnimal = new Animal() { Age = 3, Name = "Unknown", Dangerous = true, DangerScale = 8 };
                unitOfWork.Animals.Add(newAnimal);
                //Commit
                unitOfWork.Complete();

                // Example 3 - update all dangerous animals to dangerscale 10
                foreach (var animal in unitOfWork.Animals.Find(p => p.Dangerous == true))
                {
                    animal.DangerScale = 10;
                    unitOfWork.Animals.Update(animal);
                }
                //Commit changes
                unitOfWork.Complete();

                //Example 4 - Lista alla djur
                foreach (var a in unitOfWork.Animals.GetAll())
                {
                    Console.WriteLine(a.Id + ", " + a.Name + ", " + a.Age + ", " + a.Dangerous + ", " + a.DangerScale);
                }

                //Example 5 - Ta bort unknown
                foreach (var a in unitOfWork.Animals.Find(p => p.Name == "Unknown"))
                {
                    unitOfWork.Animals.Remove(a);
                }
                unitOfWork.Complete();

                Console.ReadLine();                
            }
        }
    }
}
