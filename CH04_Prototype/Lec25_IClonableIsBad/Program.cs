using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec25_IClonableIsBad
{
    public class Person
    {
        public string[] Names;
        private Address Address;

        public Person(string[] names, Address address)
        {
            this.Names = names ?? throw new ArgumentNullException(paramName: nameof(names));
            this.Address = address ?? throw new ArgumentNullException(paramName: nameof(address));
        }
    }

    public class Address
    {
        public string StreetName;
        public int HouseNumber;

    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
