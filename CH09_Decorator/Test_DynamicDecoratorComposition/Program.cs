using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_DynamicDecoratorComposition
{
    public interface IShape
    {
        string AsString();
    }

    public class Circle : IShape
    {
        private float radius;

        public Circle(float radius)
        {
            this.radius = radius;
        }

        public void Resize(float factor)
        {
            radius *= factor;
        }

        public string AsString() => $"A circle with radius {radius}";
    }

    public class Square : IShape
    {
        private float side;

        public Square(float side)
        {
            this.side = side;
        }

        public string AsString() => $"A square with side {side}";
    }

    public class ColoredShape : IShape
    {
        private IShape shape;
        private string color;

        public ColoredShape(IShape shape, string color)
        {
            this.shape = shape ?? throw new ArgumentNullException(paramName: nameof(shape));
            this.color = color ?? throw new ArgumentNullException(paramName: nameof(color));
        }

        public string AsString() => $"{shape.AsString()} has the color {color}";
    }

    public class TransparentShape : IShape
    {
        private IShape shape;
        private float transparency;

        public TransparentShape(IShape shape, float transparency)
        {
            this.shape = shape ?? throw new ArgumentNullException(paramName: nameof(shape));
            this.transparency = transparency;
        }

        public string AsString()
        {
            return $"{shape.AsString()} has {transparency * 100 }% transparency";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var shape = new Square(5.0f);
            Console.WriteLine(shape.AsString());

            var coloredShape = new ColoredShape(shape, "red");
            Console.WriteLine(coloredShape);

            var transparentColoredShape = new TransparentShape(coloredShape, 0.3f);
            Console.WriteLine(transparentColoredShape.AsString());
        }
    }
}
