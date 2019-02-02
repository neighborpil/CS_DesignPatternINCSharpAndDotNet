using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Ex1_Answer
{
    public class Token
    {
        public int Value = 0;

        public Token(int value)
        {
            Value = value;
        }
    }

    public class Memento
    {
        public List<Token> Tokens = new List<Token>();
    }

    public class TokenMachine
    {
        public List<Token> Tokens = new List<Token>();

        public Memento AddToken(int value)
        {
            return AddToken(new Token(value));
        }

        public Memento AddToken(Token token)
        {
            Tokens.Add(token);
            var m = new Memento();
            m.Tokens = Tokens.Select(t => new Token(t.Value)).ToList();
            return m;
        }

        public void Revert(Memento m)
        {
            Tokens = m.Tokens.Select(mm => new Token(mm.Value)).ToList();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}

namespace Ex1_Answer.Test
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void SingleTokenTest()
        {       
            var tm = new TokenMachine();
            var m = tm.AddToken(123);
            tm.AddToken(456);
            tm.Revert(m);
            Assert.That(tm.Tokens.Count, Is.EqualTo(1));
            Assert.That(tm.Tokens[0].Value, Is.EqualTo(123));
        }
    }
}