using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo5
{
    public class Person
    {
        public int Age { get; set; }
        public string Name{ get; set; }
    }
    class Program
    {
        static List<int> intList = new List<int>();
        static List<string> stringList = new List<string>();
        static List<Person> personList = new List<Person>();
        static void Main(string[] args)
        {
            for (var i = 0; i < 1000; i++)
            {
                intList.Add(i);
            }

            stringList.Add("one");
            stringList.Add("two");
            stringList.Add("three");
            stringList.Add("four");
            stringList.Add("five");

            for (var i = 0; i < stringList.Count; i++ )
            {
                personList.Add(new Person { Age = i, Name = stringList[i] });
            }

            // exempel 1 lambda - hitta range
            foreach (var num in intList.Where(p => p > 20 && p < 30))
                Console.WriteLine(num);
            //exempel 1 query- hitta range
            var nums = from i in intList where i > 20 && i < 30 select i;
            foreach (var num in nums)
                Console.WriteLine(num);
            
            // deklarera innan uttrycket
            Func<int, bool> exp1 = delegate (int x) { return x > 20 && x < 30; };
            Func<int, bool> exp2 = x => x > 20 && x < 30;
            // exempel 1 lambda - hitta average in range
            Console.WriteLine(intList.Where(exp2).Average());

            foreach (var p in personList.Where(p => p.Name.StartsWith("f") && p.Age > 2))
                Console.WriteLine(p.Age + ", " + p.Name);            

            Console.ReadLine();
        }
    }
}
