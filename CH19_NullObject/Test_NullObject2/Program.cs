using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Test_NullObject2
{
    interface ILog
    {
        void Info(string msg);
        void Warn(string msg);
    }

    class ConsoleLog : ILog
    {
        public void Info(string msg)
        {
            WriteLine(msg);
        }

        public void Warn(string msg)
        {
            WriteLine($"WARNING!!{msg}");
        }
    }

    class BankAccount
    {
        private ILog log;
        private int balance;

        public BankAccount(ILog log)
        {
            this.log = log;
        }

        public void Deposit(int amount)
        {
            balance += amount;
            log.Info($"Deposited {amount}, balance is now {balance}");
        }
    }

    public class NullLog : ILog
    {
        public void Info(string msg)
        {
            
        }

        public void Warn(string msg)
        {
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<BankAccount>();
            cb.RegisterType<NullLog>().As<ILog>();

            using (var c = cb.Build())
            {
                var ba = c.Resolve<BankAccount>();
                ba.Deposit(100);
            }


        }
    }
}
