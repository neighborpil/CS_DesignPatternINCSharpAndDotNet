using MoreLinq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_TestabilityIssue
{
    public interface IDatabase
    {
        int GetPopulation(string name);
    }

    public class SingletonDatabase : IDatabase
    {
        Dictionary<string, int> capitals;
        private static int instanceCount;
        public static int Count => instanceCount;

        public SingletonDatabase()
        {
            instanceCount++;
            capitals = File.ReadAllLines(Path.Combine(
                new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName, "Capitals.txt"))
                .Batch(2)
                .ToDictionary(
                    list => list.ElementAt(0),
                    list => int.Parse(list.ElementAt(1))
                );
        }

        private static Lazy<SingletonDatabase> instance = new Lazy<SingletonDatabase>(() => new SingletonDatabase());
        public static SingletonDatabase Instance => instance.Value;

        public int GetPopulation(string name)
        {
            return capitals[name];
        }
    }

    public class SingletonRecordFinder
    {
        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = 0;
            foreach (var name in names)
            {
                result += SingletonDatabase.Instance.GetPopulation(name);
            }
            return result;
        }
    }

    [TestFixture]
    public class SingletonTests
    {
        [Test]
        public void IsSingletonTest()
        {
            var db = SingletonDatabase.Instance;
            var db2 = SingletonDatabase.Instance;

            Assert.That(db, Is.SameAs(db2));
            Assert.That(SingletonDatabase.Count, Is.EqualTo(1));
        }

        [Test]
        public void SingletonToTotalPopulationTest()
        {
            var rf = new SingletonRecordFinder();
            var names = new[] { "Tokyo", "Seoul" };
            var tp = rf.GetTotalPopulation(names);
            Assert.That(tp, Is.EqualTo(17500000 + 33200000));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
