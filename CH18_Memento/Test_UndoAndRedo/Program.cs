using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_UndoAndRedo
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
        private List<Memento> changes = new List<Memento>();
        private int current;

        public BankAccount(int balance)
        {
            this.balance = balance;
            changes.Add(new Memento(balance));
        }

        public Memento Deposit(int amount)
        {
            this.balance += amount;
            var m = new Memento(balance);
            changes.Add(m);
            ++current;
            return m;
        }

        public void Restore(Memento m) 
        {
            this.balance = m.Balance;
            changes.Add(m);
            current = changes.Count - 1;
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

        public Memento Redo() // 텍스트 예제보고 했는데 강좌와는 다르다
        {
            if(current + 1< changes.Count)
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
            Console.WriteLine(ba);

            ba.Undo(); 
            Console.WriteLine($"Undo: {ba}");
            ba.Undo();
            Console.WriteLine($"Undo: {ba}");
            ba.Redo();
            Console.WriteLine($"Redo: {ba}");
        }
    }
}
