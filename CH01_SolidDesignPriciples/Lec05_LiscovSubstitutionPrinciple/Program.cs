using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec05_LiscovSubstitutionPrinciple
{
    public class Rectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public Rectangle()
        {

        }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }
        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    /*
    Liscov Substitution Principle : 상속하면  override로 지정하라??
    */
    public class Square : Rectangle
    {
        public override int Width
        {
            set { base.Width = base.Height = value; }
        }

        public override int Height
        {
            set { base.Width = base.Height = value; }
        }
    }

    class Program
    {
        static public int Area(Rectangle r) => r.Width * r.Height;
        
        static void Main(string[] args)
        {
            Rectangle rc = new Rectangle(2, 3);
            Console.WriteLine($"{rc} has area {Area(rc)}");

            Square sq = new Square();
            sq.Width = 4;
            Console.WriteLine($"{sq} has area {Area(sq)}");

            Rectangle sq2 = new Square(); // upcast가 일어나 결과가 안나온다
            sq2.Width = 4;
            Console.WriteLine($"{sq2} has area {Area(sq2)}");


            Console.ReadKey();
        }
    }
}
