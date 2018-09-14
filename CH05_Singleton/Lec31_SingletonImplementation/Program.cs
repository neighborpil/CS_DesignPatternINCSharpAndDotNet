using MoreLinq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec31_SingletonImplementation
{
    public interface IDatabase
    {
        int GetPopulation(string name);
    }

    

    public class SingletonDatabase : IDatabase
    {
        private Dictionary<string, int> capitals;

        private SingletonDatabase()
        {
            Console.WriteLine("Initializing database");
            capitals = File.ReadAllLines("Capitals.txt")
                .Batch(2)
                .ToDictionary(
                list => list.ElementAt(0).Trim(),
                list => int.Parse(list.ElementAt(1))
                );
        }

        public int GetPopulation(string name)
        {
            return capitals[name];
        }

        //private static SingletonDatabase instance = new SingletonDatabase();

        // lazy instance로 만들어주면 좋다
        private static Lazy<SingletonDatabase> instance = new Lazy<SingletonDatabase>(() => new SingletonDatabase());
        public static SingletonDatabase Instance => instance.Value;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var db =  SingletonDatabase.Instance; // 한번만 로드하고 안바뀐다
            var city = "Tokyo";
            Console.WriteLine($"{city} has population {db.GetPopulation(city)}");

        }
    }
}
