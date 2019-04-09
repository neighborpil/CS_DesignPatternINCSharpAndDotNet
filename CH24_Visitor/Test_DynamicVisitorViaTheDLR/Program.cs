using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_DynamicVisitorViaTheDLR
{
    public abstract class Expression
    {

    }

    public class DoubleExpression : Expression
    {
        public double Value;

        public DoubleExpression(double value)
        {
            Value = value;
        }
    }

    public class AdditionalExpression : Expression
    {
        public Expression Left, Right;

        public AdditionalExpression(Expression left, Expression right)
        {
            Left = left ?? throw new ArgumentNullException(nameof(left));
            Right = right ?? throw new ArgumentNullException(nameof(right));
        }
    }

    public class ExpressionPrinter
    {
        public void Print(AdditionalExpression ae, StringBuilder sb)
        {
            sb.Append("(");
            Print((dynamic) ae.Left, sb);
            sb.Append("+");
            Print((dynamic) ae.Right, sb);
            sb.Append(")");
        }

        public void Print(DoubleExpression de, StringBuilder sb)
        {
            sb.Append(de.Value);
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
            var ep = new ExpressionPrinter();
            ep.Print((dynamic)e, sb);
            Console.WriteLine(sb.ToString());
            Console.ReadKey();
        }
    }
}
