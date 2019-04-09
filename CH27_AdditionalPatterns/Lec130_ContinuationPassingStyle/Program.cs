using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lec130_ContinuationPassingStyle
{
    public class QuadraticEquationSolver
    {
        // ax^2 + bx + c == 0
        public Tuple<Complex, Complex> Start(double a, double b, double c)
        {
            var disc = b * b - 4 * a * c;
            if(disc < 0) // rootDisc가 0보다 작으면 NaN, 그래서 방식이 2개로 나눠짐
                return SolveComplex(a, b, c, disc);
            else
                return SolveSimple(a, b, c, disc);
        }

        private Tuple<Complex, Complex> SolveSimple(double a, double b, double c, double disc)
        {
            var rootDisc = Math.Sqrt(disc);
            return Tuple.Create(
                new Complex((-b + rootDisc) / (2 * a), 0),
                new Complex((-b - rootDisc) / (2 * a), 0));
        }

        private Tuple<Complex, Complex> SolveComplex(double a, double b, double c, double disc)
        {
            // 2차 방정식의 해 : x = -b +- (√b^2-4ac) / 2a, rootDisc = (√b^2-4ac)
            var rootDisc = Complex.Sqrt(new Complex(disc, 0));
            return Tuple.Create(
                (-b + rootDisc) / (2 * a),
                (-b - rootDisc) / (2 * a));
        }
    }
        

    class ContinuationPassingStyleProgram
    {
        static void Main(string[] args)
        {
            var solver = new QuadraticEquationSolver();
            var solutions = solver.Start(1, 10, 16);

        }
    }
}
