using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo3
{
    public class Person
    {
        public int Age { get; protected set; }
        public string Name { get; set; }

        public void SetAge(int age)
        {
            this.Age = age;
        }

        //tostring is virtual on object, so we can override
        public sealed override string ToString()
        {
            return string.Format("name: {0}, age: {1}", this.Name, this.Age);
        }

        //kan köra override i ärvande klasser
        public virtual string Says()
        {
            return "Hello";
        }
    }
    public class Customer : Person
    {
        public Customer()
        {
            this.Age = 34;
        }
        public override string Says()
        {
            return "To expensive";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Person();
            p.SetAge(34);
            Console.WriteLine(p.ToString());
            Console.WriteLine(p.Says());

            var c = new Customer();
            Console.WriteLine(c.Says());
            Console.ReadLine();
        }
    }
}
