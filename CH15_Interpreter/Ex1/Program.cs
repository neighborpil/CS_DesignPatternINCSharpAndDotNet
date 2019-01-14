using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1
{
    public class Token
    {
        public enum Type
        {
            Integer, Plus, Minus, Variable
        }
        public Type MyType;
        public string Text;

        public Token(Type myType, string text)
        {
            MyType = myType;
            Text = text;
        }
        public override string ToString()
        {
            return $"`{Text}`";
        }
    }
    public class ExpressionProcessor
    {

        public Dictionary<char, int> Variables = new Dictionary<char, int>();
        public ExpressionProcessor()
        {

        }
        public void AddVariable(char letter, int value)
        {
            Variables.Add(letter, value);
        }

        public int Calculate(string expression)
        {
            // parse
            List<Token> tokens = Lex(expression);
            return Parse(tokens);
        }

        private int Parse(List<Token> tokens)
        {
            int result = int.Parse(tokens[0].Text);
            string expression = null;
            for (int i = 1; i < tokens.Count; i++)
            {
                if(i % 2 == 1) // 홀수 기호
                    expression = tokens[i].Text;
                else
                {
                    if (expression.Equals("+"))
                    {
                        int value = int.Parse(tokens[i].Text);
                        result += value;
                    }
                    else if(expression.Equals("-"))
                    {
                        int value = int.Parse(tokens[i].Text);
                        result -= value;
                    }
                }
            }
            return result;
        }

        private List<Token> Lex(string expression)
        {
            List<Token> tokens = new List<Token>();
            for (int i = 0; i < expression.Length; i++)
            {
                if(expression[i] == '+')
                    tokens.Add(new Token(Token.Type.Plus, "+"));
                else if (expression[i] == '-')
                    tokens.Add(new Token(Token.Type.Minus, "-"));
                else if(char.IsLetter(expression[i]))
                {
                    tokens.Add(new Token(Token.Type.Variable, expression[i].ToString()));
                }
                else
                {
                    var sb = new StringBuilder(expression[i].ToString());
                    for (int j = i + 1; j < expression.Length; ++j)
                    {
                        if (char.IsDigit(expression[j]))
                        {
                            sb.Append(expression[j]);
                            ++i;
                        }
                        else
                        {
                            tokens.Add(new Token(Token.Type.Integer, sb.ToString()));
                            break;
                        }
                    }
                }
            }
            return tokens;
        }


    }

    class Program
    {
        static void Main(string[] args)
        {
            string sentence = "3+3-32-1";
            ExpressionProcessor processor = new ExpressionProcessor();
            int result = processor.Calculate(sentence);
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
