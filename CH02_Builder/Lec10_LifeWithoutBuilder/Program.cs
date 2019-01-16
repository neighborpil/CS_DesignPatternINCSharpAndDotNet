using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace Lec10_LifeWithoutBuilder
{
    // 생성자에서 많은 값들을 받아 클래스를 생성할 때 사용
    class Program
    {
        static void Main(string[] args)
        {
            var hello = "hello";
            var sb = new StringBuilder();
            sb.Append("<p>");
            sb.Append(hello);
            sb.Append("<p>");
            Console.WriteLine(sb);


            var words = new[] { "Helo", "world" };
            sb.Clear();
            sb.Append("<ul>");
            foreach (var word in words)
            {
                sb.AppendFormat("<li>{0}</li>", word);
            }
            sb.Append("/<ul>");
            Console.WriteLine(sb);

        }
    }
}
