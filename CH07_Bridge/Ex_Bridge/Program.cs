using NUnit.Framework;
using System;

namespace Ex_Bridge
{
    public interface IRenderer
    {
        string Name { get; set; }
    }

    public abstract class Shape
    {
        public string Name { get; set; }
        protected IRenderer renderer;

        protected Shape(IRenderer renderer)
        {
            this.renderer = renderer ?? throw new ArgumentNullException(paramName: nameof(renderer));
        }
    }

    public class Triangle : Shape
    {
        public Triangle(IRenderer renderer) : base(renderer)
        {
            Name = "Triangle";
        }

        public override string ToString()
        {
            renderer.Name = Name;
            return renderer.ToString();
        }
    }

    public class Square : Shape
    {
        public Square(IRenderer renderer) : base(renderer)
        {
            Name = "Square";
        }

        public override string ToString()
        {
            renderer.Name = Name;
            return renderer.ToString();
        }
    }

    public class VectorRenderer : IRenderer
    {
        public string Name { get; set; }
        public override string ToString()
        {
            return $"Drawing {Name} as lines";
        }
    }

    public class RasterRenderer : IRenderer
    {
        public string Name { get; set; }
        public override string ToString()
        {
            return $"Drawing {Name} as pixels";
        }
    }

    // imagine VectorTriangle and RasterTriangle are here too

    [TestFixture]
    public class TestSuite
    {
        [Test]
        public void Test()
        {
            Triangle triangle = new Triangle(new RasterRenderer());
            Assert.That(triangle.ToString(), Is.EqualTo($"Drawing Triangle as pixels"));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IRenderer renderer = new VectorRenderer();
            Triangle triangle = new Triangle(renderer);
            IRenderer renderer2 = new RasterRenderer();
            Console.WriteLine(new Triangle(renderer2).ToString()); 
        }
    }
}
