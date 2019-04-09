using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Test_ClassicVisitor_DoubleDipatch_
{
    public interface IExpressionVisitor
    {
        void Visit(DoubleExpression de);
        void Visit(AdditionalExpression ae);
    }

    public abstract class Expression
    {
        public abstract void Accept(IExpressionVisitor visitor);
    }

    public class DoubleExpression : Expression
    {
        public double Value;

        public DoubleExpression(double value)
        {
            Value = value;
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
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

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class ExpressionPrinter : IExpressionVisitor
    {
        StringBuilder sb = new StringBuilder();

        public void Visit(DoubleExpression de)
        {
            sb.Append(de.Value);
        }

        public void Visit(AdditionalExpression ae)
        {
            sb.Append("(");
            ae.Left.Accept(this);
            sb.Append("+");
            ae.Right.Accept(this);
            sb.Append(")");
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }

    public class ExpressionCalculator : IExpressionVisitor
    {
        public double Result;
        public void Visit(DoubleExpression de)
        {
            Result = de.Value;
        }

        public void Visit(AdditionalExpression ae)
        {
            ae.Left.Accept(this);
            var a = Result;
            ae.Right.Accept(this);
            var b = Result;
            Result = a + b;
        }

        public override string ToString()
        {
            return Result.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var e = new AdditionalExpression(
                new DoubleExpression(1),
                new AdditionalExpression(
                    new DoubleExpression(3),
                    new DoubleExpression(4)));
            var printer = new ExpressionPrinter();
            printer.Visit(e);
            WriteLine(printer);

            var calculator = new ExpressionCalculator();
            calculator.Visit(e);
            WriteLine($"{printer} = {calculator}");
            ReadKey();
        }
    }
}
