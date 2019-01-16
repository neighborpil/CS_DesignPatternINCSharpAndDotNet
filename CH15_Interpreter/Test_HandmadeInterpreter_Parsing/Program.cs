using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Test_HandmadeInterpreter_Parsing
{
    public interface IElement
    {
        int Value { get; }
    }

    public class Integer : IElement
    {
        public Integer(int value)
        {
            Value = value;
        }

        public int Value { get; }
    }

    public class BinaryOperation : IElement
    {
        public enum Type
        {
            Addition, Subtraction
        }
        public Type myType;
        public IElement Left, Right;
        public int Value
        {
            get
            {
                switch (myType)
                {
                    case Type.Addition:
                        return Left.Value + Right.Value;
                    case Type.Subtraction:
                        return Left.Value - Right.Value;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }

    public class Token
    {
        public enum Type
        {
            Integer, Plus, Minus, Lparen, Rparen
        }
        public Type myType;
        public string Text;

        public Token(Type myType, string text)
        {
            this.myType = myType;
            Text = text ?? throw new ArgumentNullException(paramName: nameof(text));
        }

        public override string ToString()
        {
            return $"`{Text}`";
        }
    }

    class Program
    {
        public static List<Token> Lex(string input)
        {
            var result = new List<Token>();
            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case '+':
                        result.Add(new Token(Token.Type.Plus, "+"));
                        break;
                    case '-':
                        result.Add(new Token(Token.Type.Minus, "-"));
                        break;
                    case '(':
                        result.Add(new Token(Token.Type.Lparen, "("));
                        break;
                    case ')':
                        result.Add(new Token(Token.Type.Rparen, ")"));
                        break;
                    default:
                        var sb = new StringBuilder(input[i].ToString());
                        for (int j = i+1; j < input.Length; ++j)
                        {
                            if (char.IsDigit(input[j]))
                            {
                                sb.Append(input[j]);
                                ++i;
                            }
                            else
                            {
                                result.Add(new Token(Token.Type.Integer, sb.ToString()));
                                break;
                            }
                        }
                        break;
                }
            }
            return result;
        }

        public static IElement Parse(IReadOnlyList<Token> tokens)
        {
            var result = new BinaryOperation();
            bool haveLHS = false;
            for (int i = 0; i < tokens.Count; i++)
            {
                var token = tokens[i];
                switch (token.myType)
                {
                    case Token.Type.Integer:
                        var integer = new Integer(int.Parse(token.Text));
                        if (!haveLHS)
                        {
                            result.Left = integer;
                            haveLHS = true;
                        }
                        else
                            result.Right = integer;
                        break;
                    case Token.Type.Plus:
                        result.myType = BinaryOperation.Type.Addition;
                        break;
                    case Token.Type.Minus:
                        result.myType = BinaryOperation.Type.Subtraction;
                        break;
                    case Token.Type.Lparen:
                        int j = i;
                        for (; i < tokens.Count; ++j)
                            if (tokens[j].myType == Token.Type.Rparen)
                                break;
                        // process subexpression without opening (
                        var subexpression = tokens.Skip(i + 1).Take(j - i - 1).ToList();
                        var element = Parse(subexpression);
                        if (!haveLHS)
                        {
                            result.Left = element;
                            haveLHS = true;
                        }
                        else
                            result.Right = element;
                        i = j;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return result;
        }

        static void Main(string[] args)
        {
            string input = "(3+14)-(5-3)";
            var tokens = Lex(input);
            WriteLine(string.Join(", ", tokens));

            var parsed = Parse(tokens);
            WriteLine($"{input} = {parsed.Value}");
            ReadKey();
        }
    }
}
