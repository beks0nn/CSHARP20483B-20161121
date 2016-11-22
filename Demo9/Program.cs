using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo9
{
    class Program
    {
        // delegate like action (void)
        delegate void Print(string s);
        //delegate like Func (returns a value)
        delegate int Sum(int x, int y);
        //generisk? bra eller dåligt?
        delegate T Foo<T>(T obj);
        static void Main(string[] args)
        {
            Print printDelegate = new Print(MyPrint);
            printDelegate += (s) => { Console.WriteLine("Action: " + s); };
            printDelegate += delegate (string s) { Console.WriteLine("DelegateAction: " + s); };
            printDelegate += MyPrint;
            // kör alla delegater med multicast
            printDelegate("hello");

            Sum math = new Sum(MySum);
            math += (x, y) => { return x + y; };
            math += delegate (int x, int y) { return x + y; };

            Console.WriteLine(math(4, 5));

            Foo<int> genericDelegate = new Foo<int>(Bar);
            Console.WriteLine(genericDelegate(5));

            Console.ReadLine();
        }
        static int Bar(int v)
        {
            return v * v;
        }
        static int MySum(int x, int y)
        {
            return x + y;
        }
        static void MyPrint(string s)
        {
            Console.WriteLine("MyPrint: " + s);
        }
    }
}
