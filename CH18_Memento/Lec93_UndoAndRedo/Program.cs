using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Lec93_UndoAndRedo
{
    public class Memento
    {
        public Memento(int balance)
        {
            Balance = balance;
        }

        public int Balance { get; }
    }

    public class BankAccount
    {
        private int balance;
        private List<Memento> changes = new List<Memento>();
        private int current; // This indicates which memento is currently on

        public BankAccount(int balance)
        {
            this.balance = balance;
            changes.Add(new Memento(balance));
        }

        public Memento Deposit(int amount)
        {
            balance += amount;
            var m = new Memento(balance);
            changes.Add(m);
            ++current;
            return m;
        }

        public Memento Restore(Memento m)
        {
            if(m != null)
            {
                balance = m.Balance;
                changes.Add(m);
                return m;
            }
            return null;
        }

        public Memento Undo()
        {
            if(current > 0)
            {
                var m = changes[--current];
                balance = m.Balance;
                return m;
            }
            return null;
        }

        public Memento Redo()
        {
            if(current +1 < changes.Count)
            {
                var m = changes[++current];
                balance = m.Balance;
                return m;
            }
            return null;
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
            ba.Deposit(50);
            ba.Deposit(25);
            WriteLine(ba);

            ba.Undo();
            WriteLine($"Undo 1: {ba}");
            ba.Undo();
            WriteLine($"Undo 2: {ba}");
            ba.Redo();
            WriteLine($"Redo 1: {ba}");

        }
    }
}
