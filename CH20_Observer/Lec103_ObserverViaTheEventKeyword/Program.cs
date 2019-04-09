using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec103_ObserverViaTheEventKeyword
{
    public class FallsIllEventArgs
    {
        public string Address;
    }

    public class Person
    {
        public event EventHandler<FallsIllEventArgs> FallsIll;

        public void CatchCold()
        {
            FallsIll?.Invoke(this, new FallsIllEventArgs(){ Address = "123 London Road"});
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var person = new Person();
            person.FallsIll += CallDoctor;
            person.CatchCold();
            person.FallsIll -= CallDoctor;
        }

        private static void CallDoctor(object sender, FallsIllEventArgs e)
        {
            Console.WriteLine($"A doctor has been called to {e.Address}");
        }
    }
}
