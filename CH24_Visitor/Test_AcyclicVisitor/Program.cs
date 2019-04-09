using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_AcyclicVisitor
{
    public interface IVisitor
    {
        //
    }

    public interface IVisitor<TVisitable>
    {
        void Visit(TVisitable obj);
    }

    public class Expression
    {
        public virtual void Accept(IVisitor visitor)
        {
            if (visitor is IVisitor<Expression> typed)
                typed.Visit(this);
        }
    }

    public class DoubleExpression : Expression
    {
        public double Value;

        public DoubleExpression(double value)
        {
            Value = value;
        }

        public override void Accept(IVisitor visitor)
        {
            if (visitor is IVisitor<DoubleExpression> typed)
                typed.Visit(this);
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

        public override void Accept(IVisitor visitor)
        {
            if(visitor is IVisitor<AdditionalExpression> typed)
                typed.Visit(this);
        }
    }

    public class ExpressionPrinter : IVisitor,
        IVisitor<Expression>,
        IVisitor<DoubleExpression>,
        IVisitor<AdditionalExpression>
    {
        private StringBuilder sb = new StringBuilder();

        public void Visit(Expression obj)
        {
            //
        }

        public void Visit(DoubleExpression obj)
        {
            sb.Append(obj.Value);
        }

        public void Visit(AdditionalExpression obj)
        {
            sb.Append("(");
            obj.Left.Accept(this);
            sb.Append("+");
            obj.Right.Accept(this);
            sb.Append(")");
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var e = new AdditionalExpression(
                left: new DoubleExpression(3),
                right: new AdditionalExpression(
                    left: new DoubleExpression(4),
                    right: new DoubleExpression(8)));
            var ep = new ExpressionPrinter();
            ep.Visit(e);
            Console.WriteLine(ep);
            Console.ReadKey();
        }
    }
}
