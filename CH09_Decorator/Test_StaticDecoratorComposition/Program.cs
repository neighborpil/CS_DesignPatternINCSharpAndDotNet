using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_StaticDecoratorComposition
{
    public abstract class Shape
    {
        public abstract string AsString();
    }

    public class Circle : Shape
    {
        private float radius;
        public float Radius { get => radius; set => radius = value; }

        public Circle() : this(0.0f)
        {

        }

        public Circle(float radius)
        {
            this.radius = radius;
        }


        public override string AsString() => $"A circle with radius {radius}";
    }

    public class Square : Shape
    {
        private float side;

        public float Side { get => side; set => side = value; }

        public Square() : this(0.0f)
        {

        }

        public Square(float side)
        {
            this.side = side;
        }

        public override string AsString() => $"A square with side {side}";
    }

    public class ColoredShape : Shape
    {
        private Shape shape;
        private string color;

        public ColoredShape(Shape shape, string color)
        {
            this.shape = shape ?? throw new ArgumentNullException(paramName: nameof(shape));
            this.color = color ?? throw new ArgumentNullException(paramName: nameof(color));
        }

        public override string AsString() => $"{shape.AsString()} has the color {color}";
    }

    public class TransparentShape : Shape
    {
        private readonly Shape shape;
        private float transparency;

        public TransparentShape(Shape shape, float transparency)
        {
            this.shape = shape ?? throw new ArgumentNullException(paramName: nameof(shape));
            this.transparency = transparency;
        }

        public override string AsString() => $"{shape.AsString()} has {transparency * 100}% transparency";
    }

    public class ColoredShape<T> : Shape where T : Shape, new()
    {
        private string color;
        T shape = new T();

        public ColoredShape() : this("black")
        {

        }

        public ColoredShape(string color)
        {
            this.color = color ?? throw new ArgumentNullException(paramName: nameof(color));
        }

        public override string AsString() => $"{shape.AsString()} has the color {color}";
    }

    public class TransparentShape<T> : Shape where T : Shape, new()
    {
        private float transparency;
        T shape = new T();

        public TransparentShape() : this(0.0f)
        {

        }

        public TransparentShape(float transparency)
        {
            this.transparency = transparency;
        }
        public override string AsString() => $"{shape.AsString()} has {transparency * 100}% transparency";
    }

    class Program
    {
        static void Main(string[] args)
        {
            var redSquare = new ColoredShape<Square>("red");
            Console.WriteLine(redSquare.AsString());

            var circle = new TransparentShape<ColoredShape<Circle>>(0.4f);
            Console.WriteLine(circle.AsString());
        }
    }
}
