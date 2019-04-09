using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_ObserverViaTheEventKeyword
{
    public class FallsIllEventArgs
    {
        public string Address;
    }

    public class Person
    {
        public event EventHandler<FallsIllEventArgs> FallsIll;

        public void CatchACold()
        {
            FallsIll?.Invoke(this, new FallsIllEventArgs(){ Address = "Oosaki 123"});
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var person = new Person();
            person.FallsIll += CallADoctor;
            person.CatchACold();
        }

        private static void CallADoctor(object sender, FallsIllEventArgs e)
        {
            Console.WriteLine($"Doctor goes to the {e.Address}");
        }
    }
}
