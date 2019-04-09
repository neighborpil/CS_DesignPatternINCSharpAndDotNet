using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Test_Reflection_BasedPrinting
{
    public abstract class Expression
    {

    }

    public class DoubleExpression : Expression
    {
        public readonly double Value;

        public DoubleExpression(double value)
        {
            Value = value;
        }
    }

    public class AdditionalExpression : Expression
    {
        public readonly Expression Left, Right;

        public AdditionalExpression(Expression left, Expression right)
        {
            Left = left ?? throw new ArgumentNullException(nameof(left));
            Right = right ?? throw new ArgumentNullException(nameof(right));
        }
    }

    public static class ExpressionPrinter
    {
        public static void Print(Expression e, StringBuilder sb)
        {
            if (e is DoubleExpression)
            {
                var de = (DoubleExpression) e;
                sb.Append(de.Value);
            }
            else if (e is AdditionalExpression)
            {
                var ae = (AdditionalExpression) e;
                sb.Append("(");
                Print(ae.Left, sb);
                sb.Append("+");
                Print(ae.Right, sb);
                sb.Append(")");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var e = new AdditionalExpression(
                new DoubleExpression(3),
                new AdditionalExpression(
                    new DoubleExpression(4),
                    new DoubleExpression(5)));
            var sb = new StringBuilder();
            ExpressionPrinter.Print(e, sb);
            WriteLine(sb);
            ReadKey();

        }
    }
}
