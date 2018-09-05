using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_AbstractFactory
{
    public interface IHotDrink
    {
        void Consume();
    }

    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("Enjoy Tea!");
        }
    }

    internal class Coffee : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("Enjoy Coffee");
        }
    }

    public interface IHotDrinkFacgory
    {
        IHotDrink Prepare(int amount);
    }

    internal class TeaFactory : IHotDrinkFacgory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Boil water and pour {amount}ml, Enjoy!");
            return new Tea();
        }
    }

    internal class CoffeeFactory : IHotDrinkFacgory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Boil water and pour {amount}ml, Enjoy!");
            return new Coffee();
        }
    }

    public class HotDrinkMachine
    {
        public enum AvailableDrink : byte
        {
            Tea,
            Coffee
        }

        private Dictionary<AvailableDrink, IHotDrinkFacgory> factories = new Dictionary<AvailableDrink, IHotDrinkFacgory>();

        public HotDrinkMachine()
        {
            foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
            {
                var factory = (IHotDrinkFacgory)Activator.CreateInstance(
                    Type.GetType($"Test_AbstractFactory.{Enum.GetName(typeof(AvailableDrink), drink)}Factory"));
                factories.Add(drink, factory);
            }
        }

        public IHotDrink MakeDrink(AvailableDrink drink, int amount)
        {
            return factories[drink].Prepare(amount);
        }
	
    }

    class Program
    {
        static void Main(string[] args)
        {
            HotDrinkMachine machine = new HotDrinkMachine();
            var drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Coffee, 10);
            drink.Consume();
            drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 5);
            drink.Consume();

        }
    }
}
