using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex_Factory01
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}";
        }
    }

    internal class PersonFactory
    {
        private static int id;

        Person newPerson = new Person();

        public Person CreatePerson(string name)
        {
            newPerson.Id = PersonFactory.id++;
            newPerson.Name = name;
            return newPerson;
        }
    }

    class Program 
    {
        static void Main(string[] args)
        {
            Person newPerson = new PersonFactory()
                               .CreatePerson("Jack");
            Console.WriteLine(newPerson);

            Person person2 = new PersonFactory()
                .CreatePerson("Jill"); 
            Console.WriteLine(person2);
        }
    }
}
