using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec44_GeometricShapes
{
    class Program
    {
        public class GraphicObject
        {
            public virtual string Name { get; set; } = "Group";
            public string Color;

            private Lazy<List<GraphicObject>> children = new Lazy<List<GraphicObject>>();
            public List<GraphicObject> Children => children.Value;

            private void Print(StringBuilder sb, int depth)
            {
                sb.Append(new string('*', depth))
                    .Append(string.IsNullOrWhiteSpace(Color) ? string.Empty : $"{Color} ")
                    .AppendLine(Name);
                foreach (var item in collection)
                {

                }
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                Print(sb, 0);
                return sb.ToString();
            }
        }

        public class Circle : GraphicObject
        {
            public override string Name => "Circle";
        }

        public class Square : GraphicObject
        {
            public override string Name => "Square";
        }

        static void Main(string[] args)
        {
        }
    }
}
