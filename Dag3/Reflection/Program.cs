using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    [AttributeUsage(AttributeTargets.All)]
    public class Comment : Attribute
    {
        public string Value { get; set; }
        public Comment(string v)
        {
            this.Value = v;
        }
    }

    [Comment("This is our cat...")]
    public class Cat
    {
        public int Age { get; set; }
        public string Name { get; set; }

        public string Says()
        {
            return "Mjau";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            #region Reflection
            // Get info - no  instance needed
            var animal = typeof(Cat);
            foreach (var p in animal.GetProperties())
            {
                Console.WriteLine(p.Name + ", " + p.PropertyType.Name);
            }

            // set info
            var c = new Cat();
            c.Age = 23;

            PropertyInfo prop = typeof(Cat).GetProperty("Age");//, BindingFlags.Public | BindingFlags.Instance);
            if (null != prop && prop.CanWrite)
            {
                prop.SetValue(c, 45, null);
            }
            Console.WriteLine(c.Age);

            // call method
            MethodInfo mi = typeof(Cat).GetMethod("Says");
            Console.WriteLine(mi.Invoke(c, null));

            #endregion

            #region Custom Attribute

            //Find attribute with reflection
            var comment = (Comment)animal.GetCustomAttributes(typeof(Comment), false).FirstOrDefault();
            if (comment != null)
                Console.WriteLine("Comment for Cat = " + comment.Value);

            //Find attribute with extension method (cleaner and easier to use and re-use)
            comment = AttributeHelper.GetCustomAttribute<Cat, Comment>();
            if (comment != null)
                Console.WriteLine("Comment for Cat = " + comment.Value);
            #endregion

            Console.ReadLine();
        }
    }

    public static class AttributeHelper
    {
        public static TAttribute GetCustomAttribute<TType, TAttribute>()
        {
            if (typeof(TType).GetCustomAttributes(typeof(TAttribute), false).Any())
            {
                return (TAttribute)typeof(TType).GetCustomAttributes(typeof(TAttribute), false).First();
            }
            return default(TAttribute);
        }
    }
}
