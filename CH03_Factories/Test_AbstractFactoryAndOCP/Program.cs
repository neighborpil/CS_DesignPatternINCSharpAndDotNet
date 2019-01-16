using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_AbstractFactoryAndOCP
{
    public interface IHotDrink
    {
        void Consume();
    }

    public class Tea : IHotDrink
    {
        private int amount;

        public Tea(int amount)
        {
            this.amount = amount;
        }
        public void Consume()
        {
            for (int i = 0; i < amount; i++)
            {
                Console.Write("Drink Tea!");
            }
            Console.WriteLine();
        }
    }

    public class Coffee : IHotDrink
    {
        private int amount;

        public Coffee(int amount)
        {
            this.amount = amount;
        }

        public void Consume()
        {
            for (int i = 0; i < amount; i++)
            {
                Console.Write("Drink Coffee!");
            }
            Console.WriteLine();
        }
    }

    public interface IHotDrinkFactory
    {
        IHotDrink Prepare(int amount);
    }

    public class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Boil water, pour {amount}ml");
            return new Tea(amount);
        }
    }

    public class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Boil water, pour {amount}ml");
            return new Coffee(amount);
        }
    }

    public class HotDrinkMachine
    {
        List<Tuple<string, IHotDrinkFactory>> factories = new List<Tuple<string, IHotDrinkFactory>>();
        public HotDrinkMachine()
        {
            foreach (Type t in typeof(HotDrinkMachine).Assembly.GetTypes())
            {
                if(typeof(IHotDrinkFactory).IsAssignableFrom(t) && !t.IsInterface)
                {
                    factories.Add(Tuple.Create(t.Name.Replace("Factory", string.Empty), (IHotDrinkFactory)Activator.CreateInstance(t)));
                }
            }
        }

        public IHotDrink MakeDrink()
        {
            Console.WriteLine("Available Drinks: ");
            for (int index = 0; index < factories.Count; index++)
            {
                var tuple = factories[index];
                Console.WriteLine($"{index}: {tuple.Item1}");
            }

            while (true)
            {
                string s = Console.ReadLine();
                if(s != null &&
                    int.TryParse(s, out int number)
                    && number >= 0
                    && number < factories.Count)
                {
                    Console.WriteLine("Specify amounts: ");
                    s = Console.ReadLine();
                    if(!string.IsNullOrWhiteSpace(s) &&
                        int.TryParse(s, out int amount) &&
                        amount > 0)
                    {
                        return factories[number].Item2.Prepare(amount);
                    }
                }
                Console.WriteLine("Incorrect input! Try agatin");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var machine = new HotDrinkMachine();
            var drink = machine.MakeDrink();
            drink.Consume();
        }
    }
}
