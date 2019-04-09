using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lec131_LocalIversionOfControl2
{
    public static class ExtensionMethods
    {
        public struct BoolMarker<T>
        {
            public bool Result;
            public T Self;

            public enum Operation
            {
                None,
                And,
                Or
            }

            internal Operation PendingOP;

            public BoolMarker(bool result, T self, Operation pendingOp)
            {
                Result = result;
                Self = self;
                PendingOP = pendingOp;
            }

            public BoolMarker(bool result, T self) : this(result, self, Operation.None)
            {
                Result = result;
                Self = self;
            }

            public BoolMarker<T> And => new BoolMarker<T>(Result, Self, Operation.And);

            public static implicit operator bool(BoolMarker<T> marker)
            {
                return marker.Result;
            }
        }

        public static bool HasNo<TSubject, T>(this TSubject self,
            Func<TSubject, IEnumerable<T>> props)
        {
            return !props(self).Any();
        }

        public static bool HasSome<TSubject, T>(this TSubject self,
            Func<TSubject, IEnumerable<T>> props)
        {
            return props(self).Any();
        }

        public static BoolMarker<TSubject> HasNo2<TSubject, T>(this TSubject self,
            Func<TSubject, IEnumerable<T>> props)
        {
            return  new BoolMarker<TSubject>(!props(self).Any(), self);
        }

        public static BoolMarker<TSubject> HasSome2<TSubject, T>(this TSubject self,
            Func<TSubject, IEnumerable<T>> props)
        {
            return new BoolMarker<TSubject>(props(self).Any(), self);
        }

        public static BoolMarker<T> HasNo2<T, U>(this BoolMarker<T> marker,
            Func<T, IEnumerable<U>> props)
        {
            if (marker.PendingOP == BoolMarker<T>.Operation.And && !marker.Result)
                return marker;
            return new BoolMarker<T>(!props(marker.Self).Any(), marker.Self);
        }
    }



    public class Person
    {
        public List<string> Names = new List<string>();
        public List<Person> Children = new List<Person>();
    }


    class Program
    {
        static void Main(string[] args)
        {
        }

        public void Process(Person person)
        {

            if (person.Names.Count == 0) // 이름 검색시 일반적인 방법
            {
            }
            if(!person.Names.Any())
            {
            }

            if (person.HasNo(p => p.Names))
            {
            }

            if(person.HasSome(p => p.Names))
            {
            }

            if (person.HasSome2(p => p.Names).And.HasNo2(p => p.Children))
            {
            }
        }
    }
}
