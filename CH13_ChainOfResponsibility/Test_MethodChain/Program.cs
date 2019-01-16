using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_MethodChain
{
    public class Creature
    {
        public string Name;
        public int Attack, Defense;

        public Creature(string name, int attack, int defense)
        {
            Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
            Attack = attack;
            Defense = defense;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Attack)}: {Attack}, {nameof(Defense)}: {Defense}";
        }
    }

    public class CreatureModifier
    {
        protected Creature creature;
        protected CreatureModifier next; // linked list

        public CreatureModifier(Creature creature)
        {
            this.creature = creature ?? throw new ArgumentNullException(paramName: nameof(creature));
        }

        public void Add(CreatureModifier cm)
        {
            if (next != null)
                next.Add(cm);
            else
                next = cm;
        }

        public virtual void Handle() => next?.Handle();
    }

    public class NoBonusesModifier : CreatureModifier
    {
        public NoBonusesModifier(Creature creature) : base(creature)
        {
        }

        public override void Handle()
        {
            //
        }
    }

    public class DoubleAttackModifier : CreatureModifier
    {
        public DoubleAttackModifier(Creature creature) : base(creature)
        {
        }

        public override void Handle()
        {
            Console.WriteLine($"Doubling {creature.Name}'s attack");
            creature.Attack *= 2;
            base.Handle();
        }
    }

    public class IncreaseDefenseModifier : CreatureModifier
    {
        public IncreaseDefenseModifier(Creature creature) : base(creature)
        {
        }

        public override void Handle()
        {
            Console.WriteLine($"Increase {creature.Name}'s defense");
            creature.Defense = +3;
            base.Handle();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var goblin = new Creature("Goblin", 2, 2);
            Console.WriteLine(goblin);

            var root = new CreatureModifier(goblin);
            root.Add(new DoubleAttackModifier(goblin));
            root.Add(new NoBonusesModifier(goblin));
            root.Add(new IncreaseDefenseModifier(goblin));
            root.Handle();
            Console.WriteLine(goblin);
            Console.ReadKey();

        }
    }
}
