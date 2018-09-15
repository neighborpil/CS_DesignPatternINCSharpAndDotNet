using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex_Singleton
{
    public class SingletonTester
    {
        public static bool IsSingleton(Func<object> func)
        {
            var obj1 = func();
            var obj2 = func();
            return ReferenceEquals(obj1, obj2);
        }
    }

    [TestFixture]
    public class UnitTests
    {
        [Test]
        public void ReferenceTest()
        {
            var obj = new object();
            Assert.IsTrue(SingletonTester.IsSingleton(() => obj));
            Assert.IsFalse(SingletonTester.IsSingleton(() => new object()));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}
