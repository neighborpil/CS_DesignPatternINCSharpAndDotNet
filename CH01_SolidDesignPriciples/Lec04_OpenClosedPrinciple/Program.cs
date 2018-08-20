using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Lec04_OpenClosedPrinciple
{
    public enum Color
    {
        Red, Green, Blue
    }

    public enum Size
    {
        Small, Medium, Large, Huge
    }

    public class Produce
    {
        public string  Name;
        public Color Color;
        public Size Size;

        public Produce(string name, Color color, Size size)
        {
            if(name == null)
            {
                throw new ArgumentNullException(paramName: nameof(name));
            }
            Name = name;
            Color = color;
            Size = size;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
