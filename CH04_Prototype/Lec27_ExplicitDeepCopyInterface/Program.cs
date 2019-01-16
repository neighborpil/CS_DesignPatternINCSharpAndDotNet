using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec27_ExplicitDeepCopyInterface
{
    public interface IPrototype<T>
    {
        T DeepCopy();
    }

    public class Person : IPrototype<Person>
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

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }

        public Person DeepCopy()
        {
            return new Person(Names, Address.DeepCopy());
        }
    }

    public class Address : IPrototype<Address>
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

        public Address DeepCopy()
        {
            return new Address(StreetName, HouseNumber);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var john = new Person(new[] { "John", "Smith" }, new Address("London Road", 123));

            //var jane = new Person(john);
            var jane = john.DeepCopy();
            jane.Address.HouseNumber = 321;

            Console.WriteLine(john);
            Console.WriteLine(jane);
        }
    }
}
