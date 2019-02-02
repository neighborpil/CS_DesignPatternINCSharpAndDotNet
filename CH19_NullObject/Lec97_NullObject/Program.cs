using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using JetBrains.Annotations;
using static System.Console;
namespace Lec97_NullObject
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

        public BankAccount(/*[CanBeNull]*/ ILog log)
        {
            this.log = log;
        }

        public void Deposit(int amount)
        {
            balance += amount;
            log/*?*/.Info($"Deposited {amount}, balance is now {balance}");
        }
    }

    public class NullLog : ILog
    {
        public void Info(string msg)
        {
        }

        // 만약 반환값이 있다면 default 값을 반환한다
        //public int Info(string msg)
        //{
        //  return default(int);
        //}

        public void Warn(string msg)
        {
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //var log = new ConsoleLog();

            //var ba = new BankAccount(null); // null일 경우 오류가 난다
            //ba.Deposit(100);

            var cb = new ContainerBuilder();
            //cb.RegisterType<BankAccount>();
            //cb.RegisterInstance((ILog) null); // null을 등록 할 수 없다
            //cb.Register(ctx => new BankAccount(null)); // 등록 가능
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
