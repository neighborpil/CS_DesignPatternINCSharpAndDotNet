using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleEventBroker;

namespace Lec132_DIContainerAndEventBrokerIntegration
{
    public class FootballPlayer
    {
        [Publishes("score")]
        public event EventHandler PlayerScored;

        public string Name { get; set; }

        public void Score()
        {
            var ps = PlayerScored;
            ps?.Invoke(this, new EventArgs());
        }
    }

    public class FootballCoach
    {
        [SubscribesTo("score")]
        public void PlayerScored(object sender, EventArgs args)
        {
            var p = sender as FootballPlayer;
            Console.WriteLine($"Well done, {p.Name}");
        }
    }

    // todo
    // 이해할수가 없다..

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
