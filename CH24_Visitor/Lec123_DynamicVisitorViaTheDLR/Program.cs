using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Lec123_DynamicVisitorViaTheDLR
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

    // dynamic을 사용하면 일일이 캐스티하거나 인터페이스를 설정하지 않고도 Visitor를 사용 할 수 있다
    // 하지만 성능이 매우떨어진다
    // 또 고차원의 hirareky
    public class ExpressionPrinter
    {
        public void Print(AdditionalExpression ae, StringBuilder sb)
        {
            sb.Append("(");
            Print((dynamic)ae.Left, sb); 
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
            Expression e = new AdditionalExpression(
                new DoubleExpression(1),
                new AdditionalExpression(
                    new DoubleExpression(3),
                    new DoubleExpression(4)));
            var ep = new ExpressionPrinter();
            var sb = new StringBuilder();
            ep.Print((dynamic)e, sb);
            WriteLine(sb);
            ReadKey();
        }
    }
}
