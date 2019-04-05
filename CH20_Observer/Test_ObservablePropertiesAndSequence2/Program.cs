using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_ObservablePropertiesAndSequence2
{
    public class Market
    {
        private List<float> prices = new List<float>();

        public void AddPrice(float price)
        {
            this.prices.Add(price);
            PriceAdded?.Invoke(this, price);
        }

        public event EventHandler<float> PriceAdded;
    }

    public class Market2
    {
        public BindingList<float> Prices = new BindingList<float>();

        public void AddPrice(float price)
        {
            Prices.Add(price);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var market = new Market();
            market.PriceAdded += (sender, f) => Console.WriteLine($"I added {f} to list");
            market.AddPrice(3.3f);

            var market2 = new Market2();
            market2.Prices.ListChanged += (sender, eventArgs) =>
            {
                if (eventArgs.ListChangedType == ListChangedType.ItemAdded)
                {
                    float addedPrice = ((BindingList<float>) sender)[eventArgs.NewIndex];
                    Console.WriteLine($"I added {addedPrice} to list");
                }
            };
            market2.AddPrice(3.4f);

            Console.ReadKey();

        }
    }
}
