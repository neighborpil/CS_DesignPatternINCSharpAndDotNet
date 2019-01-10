using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1
{
    public class Command
    {
        public enum Action
        {
            Deposit,
            Withdraw
        }
        public Action TheAction;
        public int Amount;
        public bool Success;
    }

    public class Account
    {
        public int Balance { get; set; }
        public void Process(Command c)
        {
            switch (c.TheAction)
            {
                case Command.Action.Deposit:
                    this.Balance += c.Amount;
                    c.Success = true;
                    break;
                case Command.Action.Withdraw:
                    if (Balance >= c.Amount)
                    {
                        Balance -= c.Amount;
                        c.Success = true;
                    }
                    else
                        c.Success = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}

namespace Ex1.Test
{
    [TestFixture]
    public class TestSuite
    {
        [Test]
        public void Test()
        {
            var a = new Account();
            var command = new Command { Amount = 100, TheAction = Command.Action.Deposit };
            a.Process(command);
            Assert.That(a.Balance, Is.EqualTo(100));
            Assert.IsTrue(command.Success);

            command = new Command { Amount = 50, TheAction = Command.Action.Withdraw };
            a.Process(command);
            Assert.That(a.Balance, Is.EqualTo(50));
            Assert.IsTrue(command.Success);

            command = new Command { Amount = 150, TheAction = Command.Action.Withdraw };
            a.Process(command);
            Assert.That(a.Balance, Is.EqualTo(50));
            Assert.IsFalse(command.Success);
        }
    }
}