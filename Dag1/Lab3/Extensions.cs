using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    [Serializable]
    public class Animal
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Animal(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
        }
    }

    public static class SerializerExtensions
    {
        public static void SaveToDisk<TK, T>(this Dictionary<TK, T> dict, string filename = null, string path = @"c:\io\")
        {
            path = getFilename<T>(filename, path);
            
            using (var ms = new FileStream(path, FileMode.Create))
            {
                //write binary
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, dict);
            }
        }
        public static Dictionary<TK, T> ReadFromDisk<TK, T>(this Dictionary<TK, T> dict, string filename = null, string path = @"c:\io\")
        {
            path = getFilename<T>(filename, path);
            if (!File.Exists(path))
                return new Dictionary<TK, T>();
            using (var ms = new FileStream(path, FileMode.Open))
            {
                //read binary
                var formatter = new BinaryFormatter();
                var animalDb = (Dictionary<TK, T>)formatter.Deserialize(ms);
                return animalDb;
            }
        }
        private static string getFilename<T>(string filename, string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (filename == null)
                path = path + typeof(T).Name.ToLower() + ".bin";
            else
                path = path + filename;
            return path;
        }
    }
}
