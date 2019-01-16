using MoreLinq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Lec37_VectorRasterDemo
{
    public class Point
    {
        public int X, Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class Line
    {
        public Point Start, End;

        public Line(Point start, Point end)
        {
            Start = start ?? throw new ArgumentNullException(paramName: nameof(start));
            End = end ?? throw new ArgumentNullException(paramName: nameof(end));
        }
    }

    public class VectorObject : Collection<Line>
    {
        
    }

    public class VectorRectangle : VectorObject
    {
        public VectorRectangle(int x, int y, int width, int height)
        {
            //4개의 라인 추가
            Add(new Line(new Point(x, y), new Point(x + width, y)));
            Add(new Line(new Point(x + width, y), new Point(x + width, y + height)));
            Add(new Line(new Point(x, y), new Point(x, y + height)));
            Add(new Line(new Point(x, y + height), new Point(x + width, y + height)));
        }
    }

    public class LineToPointAdapter : Collection<Point>
    {
        private static int count;

        public LineToPointAdapter(Line line)
        {
            WriteLine($"{++count}: Generating points for line [{line.Start.X},{line.Start.Y}]-[{line.End.X},{line.End.Y}]");

            int left = Math.Min(line.Start.X, line.End.X);
            int right = Math.Max(line.Start.X, line.End.X);
            int top = Math.Min(line.Start.Y, line.End.Y);
            int bottom = Math.Max(line.Start.Y, line.End.Y);
            int dx = right - left;
            //int dy = line.End.Y - line.Start.Y;
            int dy = top - bottom;

            if(dx == 0) // 세로선
            {
                for (int y = top; y <= bottom; y++)
                {
                    Add(new Point(left, y));
                }
            }
            else if(dy == 0) // 가로선
            {
                for (int x = left; x <= right; ++x)
                {
                    Add(new Point(x, top)); // top, bottom은 같기 때문에 어느것 써도 된다
                }
            }
        }
    }

    class Program
    {
        private static readonly List<VectorObject> vectorObjects = new List<VectorObject>
        {
            new VectorRectangle(1, 1, 10, 10),
            new VectorRectangle(3, 3, 6, 6)
        };

        public static void DrawPoint(Point p)
        {
            Write($"({p.X}.{p.Y})");
        }

        static void Main(string[] args)
        {
            Draw();
        }

        private static void Draw()
        {
            foreach (var vo in vectorObjects)
            {
                foreach (var line in vo)
                {
                    var adapter = new LineToPointAdapter(line);
                    adapter.ForEach(DrawPoint);
                    Console.WriteLine();
                }
            }
        }
    }
}
