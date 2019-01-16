using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPointExample
{
    public enum CoordinateSystem
    {
        Cartesian,
        Polar
    }
    public class Point
    {
        private double x, y;

        public Point(double a, double b, CoordinateSystem system = CoordinateSystem.Cartesian)
        {
            switch (system)
            {
                case CoordinateSystem.Cartesian:
                    this.x = a;
                    this.y = b;
                    break;
                case CoordinateSystem.Polar:
                    this.x = a * Math.Cos(b);
                    this.y = a * Math.Sin(b);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(system), system, null);
            }
            
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
