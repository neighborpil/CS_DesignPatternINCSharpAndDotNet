using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex_Test1_1
{
    public interface IValueContainer : IEnumerable<int>
    {

    }

    public class SingleValue : IValueContainer
    {
        public int Value;

        public IEnumerator<int> GetEnumerator()
        {
            yield return Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ManyValues : List<int>, IValueContainer
    {

    }

    public static class ExtensionMethods
    {
        public static int Sum(this List<IValueContainer> containers)
        {
            int result = 0;
            foreach (var c in containers)
            {
                foreach (var i in c)
                {
                    result += i;
                }
            }
            return result;
        }
    }

    [TestFixture]
    public class UnitTestManyValues
    {
        [Test]
        public void checkSum()
        {
            Assert.That(new List<IValueContainer> { new SingleValue { Value = 3 }, new ManyValues { 4, 5 } }.Sum(), Is.EqualTo(12));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var value1 = new SingleValue { Value = 3 };
            var values1 = new ManyValues { 4, 5 };
            Console.WriteLine(value1.Sum());

            var result = values1.Sum();
            Console.WriteLine(result);

            var totalValues = new List<IValueContainer> { value1, values1 };
            Console.WriteLine(totalValues.Sum());
        }
    }
}
