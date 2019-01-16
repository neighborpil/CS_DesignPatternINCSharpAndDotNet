using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_MultipleInheritance
{
    public interface IBird
    {
        int Weight { get; set; }
        void Fly();
    }

    public class Bird : IBird
    {
        public int Weight { get; set; }

        public void Fly()
        {
            Console.WriteLine($"Fly with weight {Weight}");
        }
    }

    public interface ILizard
    {
        int Weight { get; set; }
        void Crawl();
    }

    public class Lizard : ILizard
    {
        public int Weight { get; set; }

        public void Crawl()
        {
            Console.WriteLine($"Crawl with weight {Weight}");
        }
    }

    public class Dragon : ILizard, IBird
    {
        Bird bird = new Bird();
        Lizard lizard = new Lizard();

        private int weight;
        public int Weight
        {
            get
            {
                return weight;
            }
            set
            {
                weight = value;
                bird.Weight = value;
                lizard.Weight = value;
            }
        }

        public void Crawl()
        {
            lizard.Crawl();
        }

        public void Fly()
        {
            bird.Fly();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var d = new Dragon();
            d.Weight = 1323;
            d.Crawl();
            d.Fly();
        }
    }
}
