using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec14_FacetedBuilder
{
    /*
    # 하나의 object에 여러개의 Builder가 필요할 때 사용하는 법
     */
    
    public class Person
    {
        // address
        public string StreetAddress, Postcode, City;

        // employment
        public string CompanyName, Position;
        public int AnnualIncom;

        public override string ToString()
        {
            return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(Postcode)}: {Postcode}, {nameof(City)}: {City}, {nameof(CompanyName)}: {CompanyName}, {nameof(Position)}: {Position}, {nameof(AnnualIncom)}: {AnnualIncom}";
        }
    }

    public class PersonBuilder // facade
    {
        // reference object!
        protected Person person = new Person();

        public PersonJobBuilder Works => new PersonJobBuilder(person);
        public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

        public static implicit operator Person(PersonBuilder pb)
        {
            return pb.person;
        }
    }

    public class PersonAddressBuilder : PersonBuilder
    {
        public PersonAddressBuilder(Person person)
        {
            this.person = person;
        }

        public PersonAddressBuilder At(string street)
        {
            person.StreetAddress = street;
            return this;
        }

        public PersonAddressBuilder WithPostCode(string postCode)
        {
            person.Postcode = postCode;
            return this;
        }

        public PersonAddressBuilder In(string city)
        {
            person.City = city;
            return this;
        }
    }

    public class PersonJobBuilder : PersonBuilder
    {
        public PersonJobBuilder(Person person)
        {
            this.person = person;
        }

        public PersonJobBuilder At(string companyName)
        {
            person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder AsA(string positon)
        {
            person.Position = positon;
            return this;
        }

        public PersonBuilder Earning(int amount)
        {
            person.AnnualIncom = amount;
            return this;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var pb = new PersonBuilder();
            Person person = pb
                .Lives.At("123 Younho")
                      .In("Busan")
                      .WithPostCode("123")
                .Works.At("Busan")
                      .At("Engineer")
                      .Earning(123000);

            Console.WriteLine(person);
        }
    }
}
