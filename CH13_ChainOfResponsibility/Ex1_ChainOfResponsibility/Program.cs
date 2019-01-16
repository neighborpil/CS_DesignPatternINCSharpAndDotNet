using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1_ChainOfResponsibility
{
    public enum Statistic
    {
        Attack,
        Defense
    }

    public class StatQuery
    {
        public Statistic Statistic;
        public int Result;
    }

    public abstract class Creature
    {
        protected Game game;
        protected readonly int baseAttack;
        protected readonly int baseDefense;

        protected Creature(Game game, int baseAttack, int baseDefense)
        {
            this.game = game ?? throw new ArgumentNullException(paramName: nameof(game));
            this.baseAttack = baseAttack;
            this.baseDefense = baseDefense;
        }

        public virtual int Attack { get; set; }
        public virtual int Defense { get; set; }
        public abstract void Query(object source, StatQuery sq);
    }

    public class Goblin : Creature
    {
        public override void Query(object source, StatQuery sq)
        {
            if(ReferenceEquals(source, this))
            {
                switch (sq.Statistic)
                {
                    case Statistic.Attack:
                        sq.Result += baseAttack;
                        break;
                    case Statistic.Defense:
                        sq.Result += baseDefense;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                if (sq.Statistic == Statistic.Defense)
                    sq.Result++;
            }
        }

        public override int Defense
        {
            get
            {
                var q = new StatQuery { Statistic = Statistic.Defense };
                foreach (var c in game.Creatures)
                    c.Query(this, q);
                return q.Result;
            }
        }

        public override int Attack
        {
            get
            {
                var q = new StatQuery { Statistic = Statistic.Attack };
                foreach (var c in game.Creatures)
                    c.Query(this, q);
                return q.Result;
            }
        }

        public Goblin(Game game) : base(game, 1, 1)
        {
            //
        }

        protected Goblin(Game game, int baseAttack, int baseDefense): base(game, baseAttack, baseDefense)
        {
            //
        }
    }

    public class GoblinKing : Goblin
    {
        public GoblinKing(Game game) : base(game, 3, 3)
        {
            //
        }

        public override void Query(object source, StatQuery sq)
        {
            if (!ReferenceEquals(source, this) && sq.Statistic == Statistic.Attack)
                sq.Result++; // every goblin gets +1 attack;
            else
                base.Query(source, sq);
        }
    }

    public class Game
    {
        public IList<Creature> Creatures = new List<Creature>();
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    
}

namespace Ex1_ChainOfResponsibility.Test
{
    [TestFixture]
    public class TestSuite
    {
        [Test]
        public void ManyGoblinsTest()
        {
            var game = new Game();
            var goblin = new Goblin(game);
            game.Creatures.Add(goblin);

            Assert.That(goblin.Attack, Is.EqualTo(1));
            Assert.That(goblin.Defense, Is.EqualTo(1));

            var goblin2 = new Goblin(game);
            game.Creatures.Add(goblin2);
            Assert.That(goblin2.Attack, Is.EqualTo(1));
            Assert.That(goblin2.Defense, Is.EqualTo(2));

            var goblin3 = new GoblinKing(game);
            game.Creatures.Add(goblin3);

            Assert.That(goblin3.Attack, Is.EqualTo(3));
            Assert.That(goblin3.Defense, Is.EqualTo(5));

            Assert.That(goblin.Attack, Is.EqualTo(2));
            Assert.That(goblin.Defense, Is.EqualTo(3));
        }
    }
}