using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1_Decorator
{
    public interface IBird
    {
        int Age { get; set; }
        string Fly();
    }

    public class Bird : IBird
    {
        public int Age { get; set; }

        public string Fly()
        {
            return (Age < 10) ? "flying" : "too old";
        }
    }

    public interface ILizard
    {
        int Age { get; set; }
        string Crawl();
    }

    public class Lizard : ILizard
    {
        public int Age { get; set; }

        public string Crawl()
        {
            return (Age > 1) ? "crawling" : "too young";
        }
    }

    public class Dragon // no need for interfaces
    {
        private Bird bird = new Bird();
        private Lizard lizard = new Lizard();

        private int age;
        public int Age
        {
            get { return age; }
            set
            {
                age = value;
                lizard.Age = age;
                bird.Age = age;
            }
        }

        public string Fly()
        {
            return bird.Fly();
        }

        public string Crawl()
        {
            return lizard.Crawl();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var d = new Dragon();
            d.Age = 10;
            Console.WriteLine(d.Fly());
            Console.WriteLine(d.Crawl());
        }
    }
}
