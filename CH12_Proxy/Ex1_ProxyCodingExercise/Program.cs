using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1_ProxyCodingExercise
{
    public class Person
    {
        public int Age { get; set; }
        public string Drink()
        {
            return "dringking";
        }
        public string Drive()
        {
            return "driving";
        }
        public string DrinkAndDrive()
        {
            return "driving while drunk";
        }
    }

    public class ResponsiblePerson 
    {
        private readonly Person person;

        public ResponsiblePerson(Person person)
        {
            this.person = person;
        }

        public int Age
        {
            get => person.Age;
            set => person.Age = value;
        }

        public string Drink()
        {
            if (person.Age < 18)
                return "too young";
            else
                return person.Drink();
        }
        public string Drive()
        {
            if (person.Age < 16)
                return "too young";
            else
                return person.Drive();
        }
        public string DrinkAndDrive()
        {
            return "dead";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person();
            ResponsiblePerson responsiblePerson = new ResponsiblePerson(person);
            responsiblePerson.Age = 17;
            Console.WriteLine(responsiblePerson.Drive());
            Console.WriteLine(responsiblePerson.Drink());
            Console.WriteLine(responsiblePerson.DrinkAndDrive());
            Console.ReadKey();
        }
    }
}
