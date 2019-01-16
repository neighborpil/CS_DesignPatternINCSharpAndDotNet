using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec17_PointExample
{
    // Factory Pattern을 사용하는 이유는 생성자가 불분명할 때가 많기 때문이다

    public enum CoordinateSystem
    {
        Cartesian,
        Porlar
    }

    public class Point
    {
        private double x, y;

        /// <summary>
        /// 생성자에서 직교좌표계인지, 포인트좌표계인지 구별할 수 없기 때문에 주석에 달아줘야하고 enum을 만들어 구별해야한다
        /// 매우 불편
        /// Initializes a point from EITHER cartesian or porlar
        /// </summary>
        /// <param name="a">x if cartesian, rho if polar</param>
        /// <param name="b"></param>
        /// <param name="system"></param>
        public Point(double a, double b, CoordinateSystem system = CoordinateSystem.Cartesian)
        {
            switch (system)
            {
                case CoordinateSystem.Cartesian:
                    this.x = a;
                    this.y = b;
                    break;
                case CoordinateSystem.Porlar:
                    this.x = a * Math.Cos(b);
                    this.y = a * Math.Sin(b);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(system), system, null);
            }
        }

        // 구별할 수 없다, 이런 방식으로 하면 안된다
        //public Point(double rho, double theta)
        //{

        //}
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
