using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Test_CopyThroughSerialization
{
    public static class ExtensionMethods
    {
        public static T DeepCopy<T>(this T self)
        {
            using(var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, self);
                stream.Seek(0, SeekOrigin.Begin);
                object copy = formatter.Deserialize(stream);
                return (T)copy;
            }
        }

        public static T DeepCopyXml<T>(this T self)
        {
            using(var stream = new MemoryStream())
            {
                var s = new XmlSerializer(typeof(T));
                s.Serialize(stream, self);
                stream.Position = 0;
                return (T)s.Deserialize(stream);
            }
        }
    }

    public class Person
    {
        public string[] Names;
        public Address Address;

        public Person()
        {

        }

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
    }

    public class Address
    {
        public string StreetName;
        public int HouseNumber;

        public Address()
        {

        }

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
    }

    class Program
    {
        static void Main(string[] args)
        {
            var john = new Person(new []{ "John", "Kim" }, new Address("Korea", 123));
            Console.WriteLine(john);

            var jane = john.DeepCopyXml();
            jane.Names[0] = "Jane";
            jane.Address.HouseNumber = 321;

            Console.WriteLine(jane);
        }
    }
}
