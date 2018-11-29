using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
# Property Proxy
 - C++같은 언어는 대입연산자(=)를 override하여 변경하는것이 가능하다
 - 하지만 C#은 불가능하며, 이를 위하여 Proxy pattern을 사용하여 한단계 거쳐야 가능하다
*/
namespace Lec63_PropertyProxy
{
    public class Property<T> where T : new()
    {
        private T value;

        public T Value
        {
            get => value;
            set
            {
                if (Equals(this.value, value))
                    return;
                Console.WriteLine($"Assigning value to {value}");
                this.value = value;
            }
        }


        public Property() : this(default(T))
        {
            //
        }

        public Property(T value)
        {
            this.value = value;
        }

        public static implicit operator T(Property<T> property)
        {
            return property.value; // int n = p_int;
        }

        public static implicit operator Property<T>(T value)
        {
            return new Property<T>(value);// Property<int> p = 123;
        }

        public bool Equals(Property<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<T>.Default.Equals(value, other.value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals((Property<T>)obj);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public static bool operator ==(Property<T> left, Property<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Property<T> left, Property<T> right)
        {
            return !Equals(left, right);
        }
    }

    public class Creature
    {
        private Property<int> agility = new Property<int>();
        public int Agility
        {
            get => agility.Value;
            set => agility.Value = value;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var c = new Creature();
            c.Agility = 10; // c.set_Agility(10) 이게 아니다
                            // c.Agility = new Property<int>(10)

            c.Agility = 10; // 2번째 저장하면 같은 값이면 저장하지 않는다
            Console.ReadKey();
        }
    }
}
