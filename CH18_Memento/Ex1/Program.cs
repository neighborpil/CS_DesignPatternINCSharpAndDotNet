using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace Ex1
{
    public class Token
    {
        public int Value = 0;

        public Token(int value)
        {
            this.Value = value;
        }
    }

    public class Memento
    {
        private Token token;
        public Token Token { get; }
        public Memento(Token token)
        {
            this.token = token ?? throw new ArgumentNullException(paramName:nameof(token));
            Token = token;
        }

    }

    public class TokenMachine
    {
        private int sum;
        public List<Token> Tokens = new List<Token>();
        

        public Memento AddToken(int value)
        {
            sum += value;
            Token t = new Token(value);
            Tokens.Add(t);
            return new Memento(t);
        }

        public Memento AddToken(Token token)
        {
            sum += token.Value;
            Tokens.Add(token);
            return new Memento(token);
        }

        public void Revert(Memento m)
        {
            sum -= m.Token.Value;
            Tokens.Add(m.Token);
        }

        public override string ToString()
        {
            return $"{sum}";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            TokenMachine tm = new TokenMachine();
            Memento m1 =  tm.AddToken(100);
            WriteLine(tm);
            Memento m2 = tm.AddToken(new Token(200));
            WriteLine(tm);
            tm.Revert(m2);
            WriteLine(tm);
            ReadKey();
        }
    }
}
