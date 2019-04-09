using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1
{
    public class CombinationLock
    {
        private int[] combination;
        private int[] enteredDigits;
        private int position;

        public CombinationLock(int[] combination)
        {
            this.combination = combination;
            this.enteredDigits = new int[combination.Length];
            this.position = 0;
            this.Status = "LOCKED";
        }

        // you need to be changing this on user input
        public string Status;

        public void EnterDigit(int digit)
        {
            enteredDigits[position++] = digit;
            if (position == combination.Length)
            {
                bool matched = true;
                for (var i = 0; i < position; i++)
                {
                    if (combination[i] != enteredDigits[i])
                    {
                        matched = false;
                        break;
                    }
                }

                if (matched)
                    Status = "OPEN";
                else
                    Status = "ERROR";
            }
            else
            {
                bool matched = true;
                for (var i = 0; i < position; i++)
                {
                    if (combination[i] != enteredDigits[i])
                    {
                        matched = false;
                        break;
                    }
                }

                if (matched)
                    Status = string.Join("", enteredDigits.Take(position));
                else
                    Status = "ERROR";
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var c1 = new CombinationLock(new[] {1, 2, 3, 4, 5});
            c1.EnterDigit(1);
            c1.EnterDigit(2);
            c1.EnterDigit(3);
            c1.EnterDigit(4);
            c1.EnterDigit(5);

        }
    }
}

namespace Ex1.Test
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void opend()
        {
            var c1 = new CombinationLock(new []{1, 2, 3, 4, 5});
            Assert.That(c1.Status, Is.EqualTo("LOCKED"));
            c1.EnterDigit(1);
            Assert.That(c1.Status, Is.EqualTo("1"));
            c1.EnterDigit(2);
            Assert.That(c1.Status, Is.EqualTo("12"));
            c1.EnterDigit(3);
            Assert.That(c1.Status, Is.EqualTo("123"));
            c1.EnterDigit(4);
            Assert.That(c1.Status, Is.EqualTo("1234"));
            c1.EnterDigit(5);
            Assert.That(c1.Status, Is.EqualTo("OPEN"));

        }

        [Test]
        public void error()
        {
            var c1 = new CombinationLock(new[] { 1, 2, 3, 4, 5 });
            Assert.That(c1.Status, Is.EqualTo("LOCKED"));
            c1.EnterDigit(1);
            Assert.That(c1.Status, Is.EqualTo("1"));
            c1.EnterDigit(1);
            Assert.That(c1.Status, Is.EqualTo("ERROR"));
        }
    }
}
