using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec121_Reflection_BasedPrinting
{
    public abstract class Expression
    {
    }

    public class DoubleExpression : Expression
    {
        public readonly double Value;

        public DoubleExpression(double value)
        {
            this.Value = value;
        }
    }

    public class AdditionalExpression : Expression
    {
        public readonly Expression Left, Right;

        public AdditionalExpression(Expression left, Expression right)
        {
            this.Left = left ?? throw new ArgumentNullException(nameof(left));
            this.Right = right ?? throw new ArgumentNullException(nameof(right));
        }
    }

    // 이 방식을 사용하면 SRP를 지키지 못한다
    // 만약 새로운 Expression이 생성되면 또 else if를 추가해야 한다
    public static class ExpressionPrinter
    {
        public static void Print(Expression e, StringBuilder sb)
        {
            if (e is DoubleExpression de)
            {
                sb.Append(de.Value);
            }
            else if (e is AdditionalExpression ae)
            {
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
                new DoubleExpression(2),
                new AdditionalExpression(
                    new DoubleExpression(3),
                    new DoubleExpression(4)));
            var sb = new StringBuilder();
            ExpressionPrinter.Print(e, sb);
            Console.WriteLine(sb);
            Console.ReadKey();
        }
    }
}
