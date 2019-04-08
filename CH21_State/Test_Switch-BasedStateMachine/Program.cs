using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Switch_BasedStateMachine
{
    enum State
    {
        Locked,
        Failed,
        Unlocked
    }

    class Program
    {
        static void Main(string[] args)
        {
            string code = "1234";
            var state = State.Locked;
            var entry = new StringBuilder();

            while (true)
            {
                switch (state)
                {
                    case State.Locked:
                        entry.Append(Console.ReadKey().KeyChar);
                        string typedCode = entry.ToString();
                        if (typedCode == code)
                        {
                            state = State.Unlocked;
                            break;
                        }

                        if (!code.StartsWith(typedCode))
                            state = State.Failed;

                        break;
                    case State.Failed:
                        Console.CursorLeft = 0;
                        Console.WriteLine("Failed");
                        entry.Clear();
                        state = State.Locked;
                        break;
                    case State.Unlocked:
                        Console.CursorLeft = 0;
                        Console.WriteLine("Unlocked");
                        return;
                }
            }

        }
    }
}
