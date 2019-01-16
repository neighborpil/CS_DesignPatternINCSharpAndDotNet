using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec26_CopyConstructor
{
    public class Person// : ICloneable // deep copy를 명시하는 내용이 없다. 
    {
        public string[] Names;
        public Address Address;

        public Person(string[] names, Address address)
        {
            this.Names = names ?? throw new ArgumentNullException(paramName: nameof(names));
            this.Address = address ?? throw new ArgumentNullException(paramName: nameof(address));
        }

        public Person(Person other)
        {
            Names = other.Names;
            Address = new Address(other.Address);
        }

        //deep copy
        //public object Clone()
        //{
        //    return new Person(Names, (Address)Address.Clone());
        //}

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }
    }

    public class Address// : ICloneable
    {
        public string StreetName;
        public int HouseNumber;

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName ?? throw new ArgumentNullException(paramName: nameof(streetName));
            HouseNumber = houseNumber;
        }

        public Address(Address other)
        {
            StreetName = other.StreetName;
            HouseNumber = other.HouseNumber;
        }

        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }

        //public object Clone()
        //{
        //    return new Address(StreetName, HouseNumber);
        //}
    }

    class Program
    {
        static void Main(string[] args)
        {
            var john = new Person(new[] { "John", "Smith" }, new Address("London Road", 123));

            //var jane = john;
            //jane.Names[0] = "Jane";

            // 결과는 IClonable과 같다. 하지만 새로운 생성자를 생성하는 방법은 C#에서는 흔하지 않아 별로 좋지 않다
            var jane = new Person(john); 
            jane.Address.HouseNumber = 321; 

            Console.WriteLine(john);
            Console.WriteLine(jane);




        }
    }

}
