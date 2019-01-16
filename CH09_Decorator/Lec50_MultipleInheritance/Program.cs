using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Lec50_MultipleInheritance
{
    public interface IBird
    {
        int Weight { get; set; }
        void Fly();
    }

    public class Bird 
    {
        public int Weight { get; set; }

        public void Fly()
        {
            WriteLine($"Soaring in the sky with weight {Weight}");
        }
    }

    public interface ILizard
    {
        int Weight { get; set; }

        void Crawl();
    }

    public class Lizard
    {
        public int Weight { get; set; }
        public void Crawl()
        {
            WriteLine($"Crawling in the dirt with weight {Weight}");
        }
    }


    public class Dragon : IBird, ILizard
    {
        private Bird bird = new Bird();
        private Lizard lizard = new Lizard();

        private int weight;
        public int Weight
        {
            get { return weight; }
            set
            {
                weight = value;
                bird.Weight = value;
                lizard.Weight = value;
            }
        } 

        //사용 할 수 없다
        //public int IBird.Weight { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //public int ILizard.Weight { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
            d.Weight = 123; // 드래곤에만 저장되고 IBird 및 ILizard에는 저장되지 않는다 
            d.Fly();
            d.Crawl();
        }
    }
}
