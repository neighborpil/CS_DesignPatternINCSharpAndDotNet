using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_LifeWithoutBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var words = new[] { "Hello", "World" };
            StringBuilder sb = new StringBuilder();
            sb.Append("<ul>");
            foreach (var word in words)
            {
                sb.AppendFormat("<li>{0}</li>", word);
            }
            sb.Append("</ul>");
            Console.WriteLine(sb.ToString());
        }
    }
}
