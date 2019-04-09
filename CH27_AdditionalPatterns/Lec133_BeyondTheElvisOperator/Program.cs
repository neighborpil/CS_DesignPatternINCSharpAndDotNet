using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lec133_BeyondTheElvisOperator
{
    public static class MayBe
    {
        #region Reference type
        public static TResult With<TInput, TResult>(this TInput o,
            Func<TInput, TResult> evaluator)
            where TResult : class
            where TInput : class
        {
            if (o == null)
                return null;
            return evaluator(o);
        }

        public static TInput If<TInput>(this TInput o,
            Func<TInput, bool> evaulator)
            where TInput : class
        {
            if (o == null)
                return null;
            return evaulator(o) ? o : null;
        }

        public static TInput Do<TInput>(this TInput o, Action<TInput> action)
            where TInput : class
        {
            if (o == null)
                return null;
            action(o);
            return o;
        }

        public static TResult Return<TInput, TResult>(this TInput o,
            Func<TInput, TResult> evaluator, TResult failureValue)
            where TInput : class
        {
            if (o == null)
                return failureValue;
            return evaluator(o);
        } 
        #endregion

        // value type
        public static TResult WithValue<TInput, TResult>(this TInput o,
            Func<TInput, TResult> evaluator)
            where TInput : struct 
        {
            return evaluator(o);
        }
    }

    public class Person
    {
        public Address Address { get; set; }
    }

    public class Address
    {
        public string PostCode { get; set; }
    }

    public class MaybeMonadDemo
    {
        public void MyMethod(Person p)
        {
            string postcode = "UNKNOWN";

            // cs6 이전 방식
            if (p != null && p.Address != null && p.Address.PostCode != null)
                postcode = p.Address.PostCode;

            // nullcheck 내장
            postcode = p?.Address?.PostCode;

            // 메소드 섞이면 복잡한건 마찬가지
            if (p != null)
            {
                if (HasMedicalRecord(p) && p.Address != null)
                {
                    CheckAddress(p.Address);
                    if (p.Address.PostCode != null)
                        postcode = p.Address.PostCode.ToString();
                    else
                        postcode = "UNKNOWN";
                }
            }

            // expension methods로 null check 이렇게 변환 가능
            postcode = p.With(x => x.Address).With(x => x.PostCode);

            // monad? 이방식으로 가독성 있게 null 체크 및 메소드 확인 및 사용 가능
            postcode = p
                .If(HasMedicalRecord)
                .With(x => x.Address)
                .Do(CheckAddress)
                .Return(x => x.PostCode, "UNKNOWN");
        }

        private void CheckAddress(Address pAddress)
        {
            

        }

        private bool HasMedicalRecord(Person person)
        {
            throw new NotImplementedException();
        }

        static void Main(string[] args)
        {
        }
    }
}
