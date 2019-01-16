using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec41_Bridge
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
            Console.WriteLine($"Drawing pixels for circle with radius {radius}");
        }
    }

    public abstract class Shape
    {
        protected IRenderer renderer;

        protected Shape(IRenderer renderer)
        {
            this.renderer = renderer ?? throw new ArgumentNullException(paramName: nameof(renderer));
        }

        public abstract void Draw();
        public abstract void Resize(float factor);
    }

    public class Circle : Shape
    {
        private float radius;

        public Circle(IRenderer renderer,  float radius) : base(renderer)
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
            /*
            #bridge pattern
             - 실제 실행하는 부분과 계산하는 부분을 분리
             - interface를 통하여 실제 표시하는 부분을 분리
             - 여러개의 운영체제 또는 시스템에서 실행할 때
             - 계산하는 부분은 하나의 클래스에서 만들어 놓고,
               보여주거나 디바이스에 따라 곳만 인터페이스를 상속하여 만듬
            */

            //IRenderer renderer = new RasterRenderer();
            //var renderer = new VectorRenderer();
            //var circle = new Circle(renderer, 5);
            //circle.Draw();
            //circle.Resize(2);
            //circle.Draw();

            // Dependancy injection
            var cb = new ContainerBuilder();
            cb.RegisterType<VectorRenderer>().As<IRenderer>().SingleInstance();
            cb.Register((c, p) => new Circle(c.Resolve<IRenderer>(), p.Positional<float>(0)));
            using (var c = cb.Build())
            {
                var circle = c.Resolve<Circle>(new PositionalParameter(0, 5.0F));
                circle.Draw();
                circle.Resize(2.0f);
                circle.Draw();
            }


        }
    }
}
