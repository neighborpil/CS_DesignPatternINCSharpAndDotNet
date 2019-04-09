using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_HandmadeStateMachine
{
    public enum State
    {
        OffHook,
        Connecting,
        Connected,
        OnHold
    }

    public enum Trigger
    {
        CallDialed,
        HungUp,
        CallConnected,
        PlaceOnHold,
        TakenOffHold,
        LeftMessage
    }

    class Program
    {
        private static Dictionary<State, List<(Trigger, State)>> rules = new Dictionary<State, List<(Trigger, State)>>();

        static void Main(string[] args)
        {
            rules[State.OffHook] = new List<(Trigger, State)>()
            {
                (Trigger.CallDialed, State.Connecting)
            };
            rules[State.Connecting] = new List<(Trigger, State)>()
            {
                (Trigger.HungUp, State.OffHook),
                (Trigger.CallConnected, State.Connected)
            };
            rules[State.Connected] = new List<(Trigger, State)>()
            {
                (Trigger.LeftMessage, State.OffHook),
                (Trigger.HungUp, State.OffHook),
                (Trigger.PlaceOnHold, State.OnHold)
            };
            rules[State.OnHold] = new List<(Trigger, State)>()
            {
                (Trigger.TakenOffHold, State.Connected),
                (Trigger.HungUp, State.OffHook)
            };

            var state = State.OffHook;
            while (true)
            {
                Console.WriteLine($"The phone is currently {state}");
                Console.WriteLine($"Select a tirgger");

                for (int i = 0; i < rules[state].Count; i++)
                {
                    var (t, _) = rules[state][i];
                    Console.WriteLine($"{i}.{t}");
                }

                int input = int.Parse(Console.ReadLine());
                var (_, s) = rules[state][input];
                state = s;
            }
        }
    }
}
