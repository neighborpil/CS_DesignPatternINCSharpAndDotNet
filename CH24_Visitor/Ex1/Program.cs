using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1
{
    public abstract class ExpressionVisitor
    {
        public abstract void Visit(Value e);
        public abstract void Visit(AdditionExpression ae);
        public abstract void Visit(MultiplicationExpression me);
    }

    public abstract class Expression
    {
        public abstract void Accept(ExpressionVisitor ev);
    }

    public class Value : Expression
    {
        public readonly int TheValue;

        public Value(int value)
        {
            TheValue = value;
        }

        // todo
        public override void Accept(ExpressionVisitor ev)
        {
            ev.Visit(this);
        }
    }

    public class AdditionExpression : Expression
    {
        public readonly Expression LHS, RHS;

        public AdditionExpression(Expression lhs, Expression rhs)
        {
            LHS = lhs;
            RHS = rhs;
        }

        // todo
        public override void Accept(ExpressionVisitor ev)
        {
            ev.Visit(this);
        }
    }

    public class MultiplicationExpression : Expression
    {
        public readonly Expression LHS, RHS;

        public MultiplicationExpression(Expression lhs, Expression rhs)
        {
            LHS = lhs;
            RHS = rhs;
        }

        // todo
        public override void Accept(ExpressionVisitor ev)
        {
            ev.Visit(this);
        }
    }

    public class ExpressionPrinter : ExpressionVisitor
    {
        private StringBuilder sb = new StringBuilder();

        public override void Visit(Value value)
        {
            // todo
            sb.Append(value.TheValue);
        }

        public override void Visit(AdditionExpression ae)
        {
            // todo
            sb.Append("(");
            ae.LHS.Accept(this);
            sb.Append("+");
            ae.RHS.Accept(this);
            sb.Append(")");
        }

        public override void Visit(MultiplicationExpression me)
        {
            // todo
            me.LHS.Accept(this);
            sb.Append("*");
            me.RHS.Accept(this);
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
            var simple = new AdditionExpression(
                new Value(2), 
                new MultiplicationExpression(
                    new Value(3),
                    new Value(4)));
            var ep = new ExpressionPrinter();
            ep.Visit(simple);

            Console.WriteLine(ep);
            Console.ReadKey();

        }
    }
}

namespace Ex1.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void SimpleAddtion()
        {
            var simple = new AdditionExpression(new Value(2), new Value(3));
            var ep = new ExpressionPrinter();
            ep.Visit(simple);
            Assert.That(ep.ToString(), Is.EqualTo("(2+3)"));
        }

        [Test]
        public void ProductOfAdditionAndValue()
        {
            var expr = new MultiplicationExpression(
                new AdditionExpression(new Value(2), new Value(3)),
                new Value(4)
            );
            var ep = new ExpressionPrinter();
            ep.Visit(expr);
            Assert.That(ep.ToString(), Is.EqualTo("(2+3)*4"));
        }
    }

}
