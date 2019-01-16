using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Lec73_Command
{
    public class BankAccount
    {
        private int balance;
        private int overdraftLimit = -500;

        public void Deposit(int amount)
        {
            balance += amount;
            WriteLine($"Deposited ${amount}, balance is now {balance}");
        }
        public void Withdraw(int amount)
        {
            if(balance - amount >= overdraftLimit)
            {
                balance -= amount;
                WriteLine($"Withdrew ${amount}, balance is now {balance}");
            }
        }

        public override string ToString()
        {
            return $"{nameof(balance)}: {balance}";
        }
    }

    public interface ICommand
    {
        void Call();
    }

    public class BankAccountCommand : ICommand
    {
        private BankAccount account;
        public enum Action
        {
            Deposit, Withdraw
        }
        private Action action;
        private int amount;

        public BankAccountCommand(BankAccount account, Action action, int amount)
        {
            this.account = account ?? throw new ArgumentNullException(paramName: nameof(account));
            this.action = action;
            this.amount = amount;
        }

        public void Call()
        {
            switch (action)
            {
                case Action.Deposit:
                    account.Deposit(amount);
                    break;
                case Action.Withdraw:
                    account.Withdraw(amount);
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
            var ba = new BankAccount();
            var commands = new List<BankAccountCommand>
            {
                new BankAccountCommand(ba, BankAccountCommand.Action.Deposit, 100),
                new BankAccountCommand(ba, BankAccountCommand.Action.Withdraw, 50)
            };

            WriteLine(ba);

            foreach (var c in commands)
                c.Call();
            WriteLine(ba);

            Console.ReadKey();
        }
    }
}
