using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Demo8
{
    public static class SerializerExtensions
    {
        public static void WriteToDisk<TKey, TEntity>(this Dictionary<TKey, TEntity> dict, string filename = null, string path = @"c:\io\")
        {
            path = getFilename<TEntity>(filename, path);
            using (var ms = new FileStream(path, FileMode.Create))
            {
                //write binary
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, dict);
            }
        }

        public static Dictionary<TK,T> ReadFromDisk<TK,T>(this Dictionary<TK, T> dict,  string filename = null, string path =  @"c:\io\")
        {
            path = getFilename<T>(filename, path);
            using (var ms = new FileStream(path, FileMode.Open))
            {
                //read binary
                var formatter = new BinaryFormatter();
                return (Dictionary<TK, T>)formatter.Deserialize(ms);                                
            }
        }
        private static string getFilename<T>(string filename, string path)
        {
            if (filename == null)
                path = path + typeof(T).Name.ToLower() + ".bin";
            else
                path = path + filename;
            return path;
        }
    }
}
