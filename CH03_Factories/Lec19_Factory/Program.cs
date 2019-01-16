using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec19_Factory
{
    public static class PointFactory
    {
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }

        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }
    }

    public class Point
    {
        private double x, y;

        public Point(double x, double y) // contructor를 숨기지 못해 오류상황 가능
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var ptCartesian = PointFactory.NewCartesianPoint(1, 2);
            Console.WriteLine(ptCartesian);
            var ptPolar = PointFactory.NewPolarPoint(1, Math.PI / 4);
            Console.WriteLine(ptPolar);
        }
    }
}
