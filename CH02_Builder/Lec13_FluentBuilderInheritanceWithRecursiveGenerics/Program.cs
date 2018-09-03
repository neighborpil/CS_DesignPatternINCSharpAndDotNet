using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec13_FluentBuilderInheritanceWithRecursiveGenerics
{
    public class Person
    {
        public string Name;
        public string Position;

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }
    }

    public abstract class PersonBuilder
    {
        protected Person person = new Person();
        public Person Build()
        {
            return person;
        }
    }
         
    // 
    public class PersonInfoBuilder<SELF> : PersonBuilder where SELF : PersonInfoBuilder<SELF>
    {
        protected Person person = new Person(); // 상속 사용
        public SELF Called(string name)
        {
            person.Name = name;
            return (SELF)this;
        }
    }

    public class PersonJobBuilder<SELF>: PersonInfoBuilder<PersonJobBuilder<SELF>> where SELF : PersonJobBuilder<SELF>
    {
        public SELF WorksAsA(string position)
        {
            person.Position = position;
            return (SELF)this;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var builder = new PersonJobBuilder();
            //builder.Called("dmitri")
            //       .WorksAsAs // 오류가 뜬다 왜냐하면 Called메서드는 PersonInfoBuilder를 Return하기 때문이다

        }
    }
}
