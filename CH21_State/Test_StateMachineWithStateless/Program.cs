using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stateless;

namespace Test_StateMachineWithStateless
{
    public enum Health
    {
        NonReproductive,
        Pragnant,
        Reproductive
    }

    public enum Activity
    {
        GiveBirth,
        ReachPuberty,
        HaveAbortion,
        HaveUnprotectedSex,
        Hystorectomy // 자궁적출술
    }

    class Program
    {
        static void Main(string[] args)
        {
            var machine = new StateMachine<Health, Activity>(Health.NonReproductive);
            machine.Configure(Health.NonReproductive)
                .Permit(Activity.ReachPuberty, Health.Reproductive);
            machine.Configure(Health.Reproductive)
                .Permit(Activity.Hystorectomy, Health.NonReproductive)
                .PermitIf(Activity.HaveUnprotectedSex, Health.Pragnant, () => ParentNotWatching);
            machine.Configure(Health.Pragnant)
                .Permit(Activity.GiveBirth, Health.Reproductive)
                .Permit(Activity.HaveAbortion, Health.Reproductive);
        }

        public static bool ParentNotWatching { get; set; }
    }
}
