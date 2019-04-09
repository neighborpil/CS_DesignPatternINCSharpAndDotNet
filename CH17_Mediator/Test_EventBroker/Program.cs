using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Test_EventBroker
{
    public class Actor
    {
        public EventBroker broker;

        public Actor(EventBroker broker)
        {
            this.broker = broker ?? throw new ArgumentNullException(paramName: nameof(broker));
        }
    }

    public class FootballPlayer : Actor
    {
        public string Name { get; set; }
        public int GoalScored { get; set; } = 0;
        public void Scored()
        {
            GoalScored++;
            broker.Publish(new PlayerScoredEvent { Name = Name, GoalsScored = GoalScored });
        }
        public void AssaultReferee()
        {
            broker.Publish(new PlayerSentOffEvent { Name = Name, Reason = "voilence" });
        }

        public FootballPlayer(EventBroker broker, string name) : base(broker)
        {
            Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
            broker.OfType<PlayerScoredEvent>()
                .Where(ps => !ps.Name.Equals(name))
                .Subscribe(ps => WriteLine($"{name}: Nicely done, {ps.Name}! It's your {ps.GoalsScored} goal."));
            broker.OfType<PlayerSentOffEvent>()
                .Where(ps => !ps.Name.Equals(name))
                .Subscribe(ps => WriteLine($"{name}: See you in the lockers, {ps.Name}"));
        }
    }

    public class FootballCoach : Actor
    {
        public FootballCoach(EventBroker broker) : base(broker)
        {
            broker.OfType<PlayerScoredEvent>()
                .Subscribe(ps => WriteLine($"Coach: Well done, {ps.Name}"));
            broker.OfType<PlayerSentOffEvent>()
                .Subscribe(ps => WriteLine($"Coach: How could you, {ps.Name}"));
        }
    }

    public class PlayerEvent
    {
        public string Name { get; set; }
    }

    public class PlayerScoredEvent : PlayerEvent
    {
        public int GoalsScored { get; set; }
    }

    public class PlayerSentOffEvent : PlayerEvent
    {
        public string Reason { get; set; }
    }

    public class EventBroker : IObservable<PlayerEvent>
    {
        Subject<PlayerEvent> subscriptions = new Subject<PlayerEvent>();

        public IDisposable Subscribe(IObserver<PlayerEvent> observer)
        {
            return subscriptions.Subscribe(observer);
        }
        public void Publish(PlayerEvent pe)
        {
            subscriptions.OnNext(pe);
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<EventBroker>().SingleInstance();
            cb.RegisterType<FootballCoach>();
            cb.Register((c, p) => new FootballPlayer(
                c.Resolve<EventBroker>(),
                p.Named<string>("name")));
            using(var c = cb.Build())
            {
                var coach = c.Resolve<FootballCoach>();
                var player1 = c.Resolve<FootballPlayer>(new NamedParameter("name", "John"));
                var player2 = c.Resolve<FootballPlayer>(new NamedParameter("name", "Chris"));

                player1.Scored();
                player1.Scored();
                player1.Scored();
                player1.AssaultReferee();
                player2.Scored();

                 ReadKey();
            }
        }
    }
}
