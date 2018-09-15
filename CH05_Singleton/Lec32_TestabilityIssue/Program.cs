using MoreLinq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec32_TestabilityIssue
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
            capitals = File.ReadAllLines(
                Path.Combine(
                    new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName, "Capitals.txt"))
                .Batch(2)
                .ToDictionary(
                    list => list.ElementAt(0).Trim(),
                    list => int.Parse(list.ElementAt(1))
                );
            //capitals = File.ReadAllLines(
            //    Path.Combine(
            //        new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName, "Capitals.txt"))
            //    .Batch(2)
            //    .ToDictionary(
            //        list => list.ElementAt(0).Trim(),
            //        list => int.Parse(list.ElementAt(1))
            //    );
        }

        public static Lazy<SingletonDatabase> instance =
            new Lazy<SingletonDatabase>(() => new SingletonDatabase());
        public static SingletonDatabase Instance = instance.Value;

        public int GetPopulation(string name)
        {
            return capitals[name];
        }
    }

    public class SingletonRecordFinder
    {
        public int GetTotalPopulation(IEnumerable<string> names)
        {
            // singleton을 사용하면 하드코딩해야 하므로, 다른 것으로 치환하지 못한다.
            // 보통 테스트 할 때는 모든 데이터베이스를 테스트 하지 못한다.
            // 다른 것으로 치환해서 하는데 singleton은 하드코딩해야 하므로
            // 간단하게 치환하지 못한다
            // 그래서 Singleton의 테스트 이슈가 있다.
            int result = 0;
            foreach (var name in names)
                result += SingletonDatabase.Instance.GetPopulation(name);
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
        public void SingletonTotalPopulationTest()
        {
            var rf = new SingletonRecordFinder();
            var names = new[] { "Seoul", "Tokyo" };
            int tp = rf.GetTotalPopulation(names);
            Assert.That(tp, Is.EqualTo(17500000 + 33200000));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var db = SingletonDatabase.Instance;
            var city = "Seoul";
            Console.WriteLine($"{city}: {db.GetPopulation(city)}");
        }
    }
}
