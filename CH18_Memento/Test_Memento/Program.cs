using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace Test_Memento
{
    public class Memento
    {
        public int Balance { get; }
        public Memento(int balance)
        {
            Balance = balance;
        }
    }

    public class BankAccount
    {
        private int balance;

        public BankAccount(int balance)
        {
            this.balance = balance;
        }

        public Memento Deposit(int amount)
        {
            balance += amount;
            return new Memento(balance);
        }

        public void Restore(Memento memento)
        {
            this.balance = memento.Balance;
        }

        public override string ToString()
        {
            return $"{nameof(balance)}: {balance}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var ba = new BankAccount(100);
            var m1 = ba.Deposit(100);
            var m2 = ba.Deposit(50);

            WriteLine(ba.ToString());
            ba.Restore(m1);
            WriteLine(ba.ToString());
            ba.Restore(m2);
            WriteLine(ba.ToString());

        }
    }
}
