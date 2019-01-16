using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Bridge
{
    public interface IRenderer
    {
        void RenderCircle(float radius);
    }

    public class VectorRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Drawing a circle of radius {radius}");
        }
    }

    public class RasterRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Drawing pixels for a circle of radius {radius}");

        }
    }

    public abstract class Shape
    {
        public IRenderer renderer;

        public Shape(IRenderer renderer)
        {
            this.renderer = renderer ?? throw new ArgumentNullException(paramName: nameof(renderer));
        }

        public abstract void Draw();
        public abstract void Resize(float factor);
    }

    public class Circle : Shape
    {
        private float radius;

        public Circle(IRenderer renderer, float radius) : base(renderer)
        {
            this.radius = radius;
        }

        public override void Draw()
        {
            renderer.RenderCircle(radius);
        }

        public override void Resize(float factor)
        {
            radius *= factor;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //IRenderer renderer = new VectorRenderer();
            //IRenderer renderer = new RasterRenderer();
            //var circle = new Circle(renderer, 3.0F);

            //circle.Draw();
            //circle.Resize(2);
            //circle.Draw();

            var cb = new ContainerBuilder();
            cb.RegisterType<RasterRenderer>().As<IRenderer>().SingleInstance();
            cb.Register((c, p) => new Circle(c.Resolve<IRenderer>(), p.Positional<float>(0)));

            using(var c = cb.Build())
            {
                var circle = c.Resolve<Circle>(new PositionalParameter(0, 3.0F));
                circle.Draw();
                circle.Resize(2);
                circle.Draw();
            }
        }
    }
}
