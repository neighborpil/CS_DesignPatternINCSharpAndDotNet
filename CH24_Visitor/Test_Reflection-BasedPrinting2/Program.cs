using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Test_Reflection_BasedPrinting2
{
    using DictType = Dictionary<Type, Action<Expression, StringBuilder>>;

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
        private static DictType actions = new DictType
        {
            [typeof(DoubleExpression)] = (e, sb) =>
            {
                var de = (DoubleExpression) e;
                sb.Append(de.Value);
            },
            [typeof(AdditionalExpression)] = (e, sb) =>
            {
                var ae = (AdditionalExpression) e;
                sb.Append("(");
                Print(ae.Left, sb);
                sb.Append("+");
                Print(ae.Right, sb);
                sb.Append(")");
            }
        };

        public static void Print(Expression e, StringBuilder sb)
        {
            actions[e.GetType()](e, sb);
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
                    new DoubleExpression(7)));
            var sb = new StringBuilder();
            ExpressionPrinter.Print(e, sb);
            WriteLine(sb);
            ReadKey();

        }
    }
}
