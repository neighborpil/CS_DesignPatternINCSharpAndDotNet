using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Lec105_ObservablePropertiesAndSequences.Annotations;

namespace Lec105_ObservablePropertiesAndSequences
{
    /*
    # INotifyPropertyChanged
     - 이벤트를 사용하지 않을때
     - 이벤트 갯수가 많이 않을 때
     */

    public class Market : INotifyPropertyChanged
    {
        private float volatility;
        public event PropertyChangedEventHandler PropertyChanged;

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
                if (eventArgs.PropertyName == "volatility")
                {

                }
            };
        }
    }
}
