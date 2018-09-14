using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex_Prototype
{
    public interface IPrototype<T>
    {
        T DeepCopy();
    }

    public class Point
    {
        public int X, Y;

        public override string ToString()
        {
            return $"X : {X}, Y : {Y}";
        }
    }

    public class Line
    {
        public Point Start, End;

        public override string ToString()
        {
            return $"{nameof(Start)}: {Start}, {nameof(End)}: {End}";
        }

        public Line DeepCopy()
        {
            return new Line() { Start = new Point() { X = this.Start.X, Y = this.Start.Y },
                                End = new Point() { X = this.End.X, Y = this.End.Y } };
        }
    }
     
    class Program
    {
        static void Main(string[] args)
        {
            var line = new Line() { Start = new Point() { X = 0, Y = 0 }, End = new Point() { X = 3, Y = 3 } };
            Console.WriteLine(line);
            var newLine = line.DeepCopy();
            newLine.End.X = 5;
            newLine.End.Y = 5;

            Console.WriteLine(newLine);
        }
    }
}
