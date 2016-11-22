using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo7
{
    class Program
    {
        static void Main(string[] args)
        {
            // skriva till fil 1 -            // jobbig med text
            using(var fs = new FileStream(@"c:\io\myfile1.txt", FileMode.Append, FileAccess.Write))
            {
                Console.WriteLine("Enter text to write to file");
                var input = Console.ReadLine();
                var b = System.Text.Encoding.UTF8.GetBytes(input);
                fs.Write(b,0,b.Length);                
            }
            // skriva till fil 2 - enkel med text
            using (var fs = new StreamWriter(@"c:\io\myfile2.txt", true, Encoding.UTF8))
            {
                Console.WriteLine("Enter text to write to file");
                var input = Console.ReadLine();
                fs.Write(input);            
            }
            Console.WriteLine("Hit enter to read files");
            Console.ReadLine();

            // jobbig med text
            using(var sr = new FileStream(@"c:\io\myfile1.txt", FileMode.Open, FileAccess.Read)){
                
                var buff = new byte[1024];
                sr.Read(buff, 0, buff.Length);
                Console.WriteLine(Encoding.UTF8.GetString(buff,0, buff.Length));
            }

            // enkel, men ingen append
            File.WriteAllText(@"c:\io\myfile1.txt", "this is content", Encoding.UTF8);
            // väldigt enkel med text
            var content = File.ReadAllText(@"c:\io\myfile1.txt", Encoding.UTF8);
            Console.WriteLine(content);
            Console.ReadLine();
        }
    }
}
