using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Demo8
{
    //Binary serialization

    [Serializable]
    public class Animal
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var animalDb = new Dictionary<Guid,Animal>(1);
            var t = new Animal { Id = Guid.NewGuid(), Name = "Tiger" };
            animalDb.Add(t.Id, t);

            Console.WriteLine("Hit enter to serialize to binary");
            Console.ReadLine();

            //only in memory for demo
            //using(var ms = new MemoryStream())
            //{
            //    //write binary
            //    var formatter = new BinaryFormatter();
            //    formatter.Serialize(ms, animalDb);
            //    Console.WriteLine(ms.Length);
            //    //read binary
            //    ms.Seek(0, SeekOrigin.Begin);
            //    animalDb = (Dictionary<Guid,Animal>)formatter.Deserialize(ms);
            //    Console.WriteLine(animalDb.First().Value.Name);
            //}

            //Use extensions instead of memorystream
            animalDb.WriteToDisk();
            animalDb.Clear();
            animalDb = animalDb.ReadFromDisk();
            Console.ReadLine();
        }
    }
}
