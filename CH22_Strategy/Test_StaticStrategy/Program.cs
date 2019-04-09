using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_StaticStrategy
{
    public enum OutputFormat
    {
        Markdown,
        Html
    }

    public interface IListStrategy
    {
        void Start(StringBuilder sb);
        void AppendList(StringBuilder sb, string item);
        void End(StringBuilder sb);
    }

    public class HtmlListStrategy : IListStrategy
    {
        public void Start(StringBuilder sb)
        {
            sb.AppendLine("<ul>");
        }

        public void AppendList(StringBuilder sb, string item)
        {
            sb.AppendLine($"    <li>{item}</li>");
        }

        public void End(StringBuilder sb)
        {
            sb.AppendLine("</ul>");
        }
    }

    public class MarkdownListStrategy : IListStrategy
    {
        public void Start(StringBuilder sb)
        {
        }

        public void AppendList(StringBuilder sb, string item)
        {
            sb.AppendLine($" * {item}");
        }

        public void End(StringBuilder sb)
        {
        }

        
    }

    public class TextProcessor<LS> where LS : IListStrategy, new()
    {
        private readonly StringBuilder sb = new StringBuilder();
        private readonly IListStrategy listStrategy = new LS();

        public void AppendItems(IEnumerable<string> items)
        {
            listStrategy.Start(sb);
            foreach (var item in items)
                listStrategy.AppendList(sb, item);
            listStrategy.End(sb);
        }

        public void Clear()
        {
            sb.Clear();
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
            var processor = new TextProcessor<HtmlListStrategy>();
            processor.AppendItems(new[] {"asdf", "fds", "fsae"});
            Console.WriteLine(processor.ToString());

            var processor2 = new TextProcessor<MarkdownListStrategy>();
            processor2.AppendItems(new[] { "asdf", "fds", "fsae" });
            Console.WriteLine(processor2.ToString());

            Console.ReadKey();
        }
    }
}
