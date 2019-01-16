using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_ImplicitOperatorTest
{
    class Digit
    {
        public double val;

        public Digit(double d)
        {
            val = d;
        }

        public static implicit operator double(Digit d)
        {
            return d.val;
        }

        public static implicit operator Digit(double d)
        {
            return new Digit(d);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Digit dig = new Digit(7);
            double num = dig;
            Digit dig2 = 12;
            Console.WriteLine($"num = {num}, num2 = {dig2.val}");
            Console.ReadLine();
        }
    }
}
