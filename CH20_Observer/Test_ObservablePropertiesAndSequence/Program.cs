using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Test_ObservablePropertiesAndSequence.Annotations;

namespace Test_ObservablePropertiesAndSequence
{
    public class Market : INotifyPropertyChanged
    {
        private float volatility;

        public float Volatility
        {
            get => volatility;
            set
            {
                if (value.Equals(volatility)) return;
                volatility = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var market = new Market();
            market.PropertyChanged += (sender, eventArgs) =>
            {
                if (eventArgs.PropertyName == "Volatility")
                {
                    float volatility = (float) ((Market) sender).Volatility;
                    Console.WriteLine($"volatility is changed to {volatility}");
                }
            };
            market.Volatility = 1.3f;

            Console.ReadKey();
        }
    }
}
