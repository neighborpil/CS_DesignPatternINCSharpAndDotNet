using JetBrains.dotMemoryUnit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec58_TextFormatting
{
    /*
    # JetBrains.DotMemoryUnit를 설치한다
     - 닷넷에서의 무료 메모리 유닛 테스트 API이다
    */
    public class User
    {
        string fullName;

        public User(string fullName)
        {
            this.fullName = fullName ?? throw new ArgumentNullException(paramName: nameof(fullName));
        }
    }

    [TestFixture]
    public class Demo
    {
        static void Main(string[] args)
        {

        }

        [Test]
        public void TestUser()
        {
            var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());

            var users = new List<User>();

            foreach (var firstName in firstNames)
                foreach (var lastName in lastNames)
                    users.Add(new User($"{firstName} {lastName}"));

            ForceGC();

            dotMemory.Check(memory =>
            {
                Console.WriteLine(memory.SizeInBytes);
            });
        }


        private void ForceGC()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private string RandomString()
        {
            Random rand = new Random();
            return new string(
                Enumerable.Range(0, 10)
                .Select(i => (char)('a' + rand.Next(26)))
                .ToArray());
        }
    }

}
