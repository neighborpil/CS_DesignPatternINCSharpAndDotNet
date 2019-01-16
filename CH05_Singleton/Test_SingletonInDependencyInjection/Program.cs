using Autofac;
using MoreLinq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_SingletonInDependencyInjection
{
    public interface IDatabase
    {
        int GetPopulation(string name);
    }

    public class SingletonDatabase : IDatabase
    {
        Dictionary<string, int> capitals;

        public SingletonDatabase()
        {
            capitals = File.ReadAllLines(
                Path.Combine(
                    new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName, "Capitals.txt"))
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

        public static Lazy<SingletonDatabase> instance =
            new Lazy<SingletonDatabase>(() => new SingletonDatabase());
        public static SingletonDatabase Instance = instance.Value;
    }

    public class SingletonRecordFinder
    {
        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = 0;
            foreach (var name in names)
                result += SingletonDatabase.Instance.GetPopulation(name);
            return result;
        }
    }

    public class ConfigurableRecordFinder
    {
        private IDatabase database;

        public ConfigurableRecordFinder(IDatabase database)
        {
            this.database = database ?? throw new ArgumentNullException(paramName: nameof(database));
        }

        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = 0;
            foreach (var name in names)
                result += database.GetPopulation(name);
            return result;
        }
    }

    public class DummyDatabase : IDatabase
    {
        public int GetPopulation(string name)
        {
            return new Dictionary<string, int>()
            {
                ["alpha"] = 1,
                ["beta"] = 2,
                ["gamma"] = 3
            }[name];
        }
    }

    public class OrdinaryDatabase : IDatabase
    {
        Dictionary<string, int> capitals;
        public OrdinaryDatabase()
        {
            Console.WriteLine("Initialize Database");
            capitals = File.ReadAllLines(Path.Combine(
                new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName, "Capitals.txt"))
                .Batch(2)
                .ToDictionary(
                    list => list.ElementAt(0).Trim(),
                    list => int.Parse(list.ElementAt(1))
                );
            foreach (var kvp in capitals)
            {
                Console.WriteLine(kvp.Key + ", " + kvp.Value);
            }
        }

        public int GetPopulation(string name)
        {
            return capitals[name];
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
        }

        [Test]
        public void SingletonTotalPopulationTest()
        {
            var rf = new SingletonRecordFinder();
            var names = new[] { "Tokyo", "Seoul" };
            var tp = rf.GetTotalPopulation(names);
            Assert.That(tp, Is.EqualTo(17500000 + 33200000));
        }

        [Test]
        public void ConfigurablePopulationTest()
        {
            var rf = new ConfigurableRecordFinder(new DummyDatabase());
            var values = new[] { "alpha", "beta" };
            var tp = rf.GetTotalPopulation(values);
            Assert.That(tp, Is.EqualTo(1 + 2));

        }

        [Test]
        public void DIPopulationTest()
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<OrdinaryDatabase>()
                .As<IDatabase>()
                .SingleInstance();
            cb.RegisterType<ConfigurableRecordFinder>();
            using(var c = cb.Build())
            {
                var rf = c.Resolve<ConfigurableRecordFinder>();
            }

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            OrdinaryDatabase od = new OrdinaryDatabase();

        }
    }
}
