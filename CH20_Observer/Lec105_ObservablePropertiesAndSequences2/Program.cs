using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
# 일반적인 리스트를 사용하여 제작하여도 된다
 - 하지만 BindingList를 사용하면 따로 이벤트를 등록 할 필요가 없다

*/

namespace Lec105_ObservablePropertiesAndSequences2
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

    public class MarketWithBindingList // observable
    {
        public BindingList<float> Prices = new BindingList<float>();

        public void AddPrice(float price)
        {
            this.Prices.Add(price);
        }
    }

    class Program // observer
    {
        static void Main(string[] args)
        {
            // 기존 방법
            var market = new Market();
            market.PriceAdded += (sender, f) =>
            {
                Console.WriteLine($"We got a price of {f}");
            };
            market.AddPrice(123);

            // BindingList를 사용한 방법
            var marketWithBindingList = new MarketWithBindingList();
            marketWithBindingList.Prices.ListChanged += (sender, eventArgs) =>
            {
                if (eventArgs.ListChangedType == ListChangedType.ItemAdded)
                {
                    float price = ((BindingList<float>) sender)[eventArgs.NewIndex];
                    Console.WriteLine($"Binding list got a price of {price}");
                }
            };
            marketWithBindingList.AddPrice(123);

            Console.ReadKey();

        }
    }
}
