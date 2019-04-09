using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

/*
# Weak Event Pattern
 - 일반적으로 이벤트를 생성하여 이벤트 핸들러를등록하면 이벤트가 제대로 삭제되지 않는한은
   핸들러가 삭제되지 않는다. 이는 메모리 leaking이라 할 수 있다
 - .net 4.5이상 부터는 WeakEventManager를 지원하며, 이를 통하여 이벤트 해제를 해주지 않아도
   객체가 삭제되면 이벤트가 끊어지고 GC가 일어난다
 - Reference를 추가하여야하는데 기본적으로 VStudio에 포함되어 있으며 WindowsBase를 참조 추가 해주면 된다
*/

namespace Lec104_WeakEventPattern
{
    public class Button
    {
        public event EventHandler Clicked;

        public void Fire()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }
    }

    public class Window
    {
        public Window(Button button)
        {
            //button.Clicked += ButtonOnClicked;
            WeakEventManager<Button, EventArgs>.AddHandler(button, "Clicked", ButtonOnClicked);
        }

        private void ButtonOnClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Button clicked (Window handler)");
        }

        ~Window()
        {
            Console.WriteLine("Window finalized");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var button = new Button();
            var window = new Window(button);
            var windowRef = new WeakReference(window);
            button.Fire();

            Console.WriteLine($"Setting window to null");
            window = null;

            FireGC();
            Console.WriteLine($"Is the window alive after GC? {windowRef.IsAlive}");
        }

        private static void FireGC()
        {
            Console.WriteLine("Starting GC");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            Console.WriteLine("GC is done!");

        }
    }
}
