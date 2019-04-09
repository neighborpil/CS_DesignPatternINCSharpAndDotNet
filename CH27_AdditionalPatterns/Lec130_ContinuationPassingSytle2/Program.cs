using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lec130_ContinuationPassingSytle2
{
    public enum WorkflowResult
    {
        Success, Failure
    }

    /*
    # workflow가 길어지면 분기에서 항상 성공하는 것이 아니라 실패 할 때도 있다
      그런때 사용하는 방법
      - c# 에서는 out keyword를 허용하기 때문에, return 값은 성공실패 bool값,
        실제 값은 out으로 따로 빼두고, 성공할 때만 사용
    */
    public class QuadraticEquationSolver
    {
        // ax^2 + bx + c == 0
        public WorkflowResult Start(double a, double b, double c, out Tuple<Complex, Complex> result)
        {
            var disc = b * b - 4 * a + c;
            if (disc < 0) //항상 실패하기 때문에
            {
                result = null;
                return WorkflowResult.Failure;
            }
            //return SolveComplex(a, b, c, disc);
            else // 성공하는 경우에만 값 반환
                return SolveSimple(a, b, disc, out result);
        }

        private WorkflowResult SolveSimple(double a, double b, double disc, out Tuple<Complex, Complex> result)
        {
            var rootDisc = Math.Sqrt(disc);
            result = Tuple.Create(
                new Complex((-b + rootDisc) / (2 * a), 0),
                new Complex((-b - rootDisc) / (2 * a), 0));
            return WorkflowResult.Success;
        }

        private Tuple<Complex, Complex> SolveComplex(double a, double b, double c, double disc)
        {
            
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
            Tuple<Complex, Complex> solution;
            var flag = solver.Start(1, 10, 16, out solution);
            if (flag == WorkflowResult.Success)
            {
                // 사용
            }

        }
    }
}
