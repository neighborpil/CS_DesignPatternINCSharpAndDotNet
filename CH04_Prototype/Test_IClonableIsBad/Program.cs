using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_IClonableIsBad
{
    public class Person : ICloneable
    {
        public string[] Names;
        public Address Address;

        public Person(string[] names, Address address)
        {
            Names = names ?? throw new ArgumentNullException(paramName: nameof(names));
            Address = address ?? throw new ArgumentNullException(paramName: nameof(address));
        }

        public object Clone()
        {
            return new Person(Names, Address);
        }

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(",", Names)}, {nameof(Address)}: {Address}";
        }
    }

    public class Address : ICloneable
    {
        public string StreetName;
        public int HouseNumber;

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName ?? throw new ArgumentNullException(paramName: nameof(streetName));
            HouseNumber = houseNumber;
        }

        public object Clone()
        {
            return new Address(StreetName, HouseNumber);
        }

        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person( new []{ "John", "Call" }, new Address("London", 123));

            Console.WriteLine(person);

            var NewPerson = (Person)person.Clone();
            NewPerson.Address.HouseNumber = 321;
            NewPerson.Names[0] = "Jack";
            Console.WriteLine(NewPerson);

        }
    }
}
