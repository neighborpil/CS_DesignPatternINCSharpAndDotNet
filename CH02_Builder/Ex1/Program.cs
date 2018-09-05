using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1
{
    public class CodeBuilder
    {
        StringBuilder sb = new StringBuilder();
        int indent = 2;

        public CodeBuilder(string className)
        {
            sb.AppendLine($"public class {className}");
            sb.AppendLine($"{{");
        }

        public CodeBuilder AddField(string name, string type)
        {
            for (int i = 0; i < indent; i++)
            {
                sb.AppendFormat("{0}", ' ');
            }
            sb.AppendLine($"public {type} {name};");
            return this;
        }

        public override string ToString()
        {
            sb.AppendLine($"}}");

            return sb.ToString();
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
            Console.WriteLine(cb);
        }
    }
}
