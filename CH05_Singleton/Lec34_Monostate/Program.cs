using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec34_Monostate
{
    public class CEO
    {
        private static string name;
        private static int age;

        public int Age { get => age; set => age = value; }
        public string Name { get => name; set => name = value; }

        public override string ToString()
        {
            return $"{nameof(name)}: {name}, {nameof(age)}: {age}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var ceo = new CEO();
            ceo.Name = "Adam";
            ceo.Age = 55;

            var ceo2 = new CEO();
            Console.WriteLine(ceo2);
        }
    }
}
