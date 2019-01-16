using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Builder
{
    public class HtmlElement
    {
        public string Name, Text;
        public List<HtmlElement> Elements = new List<HtmlElement>();
        private const int indentSize = 4;

        public HtmlElement()
        {

        }

        public HtmlElement(string name, string text)
        {
            this.Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
            this.Text = text ?? throw new ArgumentNullException(paramName: nameof(text));
        }

        public override string ToString()
        {
            return ToStringImple(0);
        }

        private string ToStringImple(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', indentSize * indent);
            sb.AppendLine($"{i}<{Name}>");
            if (!string.IsNullOrWhiteSpace(Text))
            {
                sb.Append(new string(' ', indentSize * (indent + 1)));
                sb.AppendLine(Text);
            }
            foreach (var e in Elements)
            {
                sb.Append(e.ToStringImple(indent + 1));
            }
            sb.AppendLine($"{i}</{Name}>");
            return sb.ToString();
        }
    }

    public class HtmlBuilder
    {
        private readonly string rootName;
        HtmlElement root = new HtmlElement();
        public HtmlBuilder(string rootName)
        {
            this.rootName = rootName;
            root.Name = rootName;
        }

        public void AddChild(string childName, string childText)
        {
            var e = new HtmlElement(childName, childText);
            root.Elements.Add(e);
        }

        public override string ToString()
        {
            return root.ToString();
        }

        public void Clear()
        {
            root = new HtmlElement { Name = rootName };
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var builder = new HtmlBuilder("ul");
            builder.AddChild("li", "Hello");
            builder.AddChild("li", "World");
            Console.WriteLine(builder.ToString());

            builder.Clear();
            builder.AddChild("li", "Hello2");
            builder.AddChild("li", "World2");
            Console.WriteLine(builder.ToString());

            builder = new HtmlBuilder("Html");
            builder.AddChild("body", "");
            Console.WriteLine(builder.ToString());

        }
    }
}
