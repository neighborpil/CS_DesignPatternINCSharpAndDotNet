using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ImpromptuInterface;
using static System.Console;

namespace Lec100_DynamicNullObject
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

        public void Warn(string msg)
        {
        }
    }

    public class Null<TInterface> : DynamicObject where TInterface : class
    {
        public static TInterface Instance => 
            new Null<TInterface>().ActLike<TInterface>();

        public override bool TryInvokeMember(
            InvokeMemberBinder binder, object[] args, out object result)
        {
            result = Activator.CreateInstance(binder.ReturnType);
            return true;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var log = Null<ILog>.Instance;
            log.Info("asdf");
            var ba = new BankAccount(log);
            ba.Deposit(100);
            WriteLine("finished");
            ReadKey();

        }
    }
}
