using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo4
{
    public class Person
    {
        public Guid Id { get; protected set; }
        public string Name { get; set; }
        public Person(string name)
        {
            this.Name = name;
            this.Id = Guid.NewGuid();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            #region list
            var list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (var i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }
            foreach (var i in list)
            {
                Console.WriteLine(i);
            }
            #endregion

            // dictionary - vi visar att allt är samma referens... Ändrar vi namn så ändras alla "instanser"
            var dict = new Dictionary<Guid, Person>();
            //var cd = new ConcurrentDictionary<int, int>()
            var p1 = new Person("Uffe");
            dict.Add(p1.Id, p1);
            Person p2 = null;
            if (dict.TryGetValue(p1.Id, out p2))
            {
                Console.WriteLine(p2.Name);
                p2.Name = "APA";
                Console.WriteLine(p1.Name);
                Console.WriteLine(dict[p1.Id].Name);
            }

            Console.WriteLine(dict[p1.Id].Name);
            Console.ReadLine();
        }
    }
}
