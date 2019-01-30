using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using static  System.Console;
namespace Ex1
{
    public class Participant
    {
        public string Name { get; set; }
        public int Value { get; set; }
        private Mediator mediator;

        public Participant(Mediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(paramName: nameof(mediator));
            mediator.Join(this);
        }

        public Participant(string name, Mediator mediator) : this(mediator)
        {
            Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
        }

        public void Say(int value)
        {
            mediator.Broadcast(Name, value);
            WriteLine($"{Name} says {value}");
        }

        public override string ToString()
        {
            return $"{Name}: {Value}";
        }
    }

    public class Mediator
    {
        List<Participant> participants = new List<Participant>();

        public void Join(Participant participant)
        {
            this.participants.Add(participant);
        }

        public void Broadcast(string name, int value)
        {
            participants.ForEach(p =>
            {
                if (!p.Name.Equals(name))
                    p.Value += value;
            });
        }

        public void Saying()
        {
            participants.ForEach(p => WriteLine(p));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Mediator m = new Mediator();
            Participant p1 = new Participant("john", m);
            Participant p2 = new Participant("chris", m);
            m.Saying();
            p1.Say(3);
            m.Saying();
            p2.Say(1);
            m.Saying();
            ReadKey();
        }
    }
}

namespace Ex1.Test
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestSuite()
        {
            Mediator m = new Mediator();
            Participant p1 = new Participant("john", m);
            Participant p2 = new Participant("chris", m);
            Assert.That(p1.Value, Is.EqualTo(0));
            Assert.That(p2.Value, Is.EqualTo(0));
            p1.Say(3);
            Assert.That(p1.Value, Is.EqualTo(0));
            Assert.That(p2.Value, Is.EqualTo(3));
            p2.Say(1);
            Assert.That(p1.Value, Is.EqualTo(1));
            Assert.That(p2.Value, Is.EqualTo(3));
        }
    }

}