using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec20_InnerFactory
{
    

    public class Point
    {
        private double x, y;

        private Point(double x, double y) // internal로 바꾸면 외부에서는 사용못하지만 그래도 문제가 생긴다
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }

        public static Point Origin => new Point(0, 0); // =>기호는 속성으로 만든다, Point.Orgin을 요청할 때마다 새로운 인스턴스를 만든다 X
        public static Point Origin2 = new Point(0, 0); // =>  Point.Origin을 요청하면 한번만 만든다. better
        

        #region 간결하다 베스트

        public static class Factory
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

        #endregion

        #region 이것도 한 방법이다, 가끔은 Factory를 인스턴스화 하고 싶을 때가 있고 그런때에 사용

        //public static PointFactory Factory => new PointFactory();

        //public class PointFactory
        //{
        //    public Point NewCartesianPoint(double x, double y)
        //    {
        //        return new Point(x, y);
        //    }

        //    public Point NewPolarPoint(double rho, double theta)
        //    {
        //        return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        //    }
        //} 

        #endregion
    }

    class Program
    {
        static void Main(string[] args)
        {
            var ptCartesian = Point.Factory.NewCartesianPoint(1, 2);
            Console.WriteLine(ptCartesian);
            var ptPolar = Point.Factory.NewPolarPoint(1, Math.PI / 4);
            Console.WriteLine(ptPolar);

            var point1 = Point.Origin;
            Console.WriteLine(point1);
            var point2 = Point.Origin2;
            Console.WriteLine(point2);
        }
    }
}
