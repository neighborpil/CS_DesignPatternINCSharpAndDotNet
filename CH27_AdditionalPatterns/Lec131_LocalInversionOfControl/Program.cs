using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec131_LocalInversionOfControl
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// collection의 Add 메소드를 가독성 높게 변경
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="colls"></param>
        /// <returns></returns>
        public static T AddTo<T>(this T self, params ICollection<T>[] colls)
        {
            foreach(var coll in colls)
                coll.Add(self);
            return self;
        }

        /// <summary>
        /// Contains 가독성 높게
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool IsOneOf<T>(this T self, params T[] values)
        {
            return values.Contains(self);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            
        }

        public void AddingNumber()
        {
            var list = new List<int>();
            var list2 = new List<int>();
            list.Add(24);
            //24.AddTo(list).AddTo(list2);
            24.AddTo(list, list2);
        }

        public void ProcessCommand(string opcode)
        {
            if (opcode == "AND" || opcode == "OR" || opcode == "XOR")
            {
            }

            if (new[] {"AND", "OR", "XOR"}.Contains(opcode)) // 좀 더 낫지만 여전히 ugly
            {
            }

            if ("AND OR XOR".Split(' ').Contains(opcode))
            {
            }

            if (opcode.IsOneOf("AND", "OR", "XOR")) // 훨씬 가독성 높다
            {
            }
        }
    }
}
