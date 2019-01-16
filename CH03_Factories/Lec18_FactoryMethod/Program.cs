using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec18_FactoryMethod
{
    /*
    # Factory Pattern에는 두가지 종류가 있다
     1. Proper factories
     2. Abstract factory
     */

    public class Point
    {
        private double x, y;

        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        // 하지만 이 방법으로는 single responsibility priciple을 어긴다

        // factory method
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }

        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
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
            var point = Point.NewPolarPoint(1.0, Math.PI / 2);
            Console.WriteLine(point);
        }
    }
}
