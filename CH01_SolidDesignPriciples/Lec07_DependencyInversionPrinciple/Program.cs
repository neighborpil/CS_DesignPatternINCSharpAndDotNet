using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec07_DependencyInversionPrinciple
{
    /*
    High-level 모듈은 Low-level모듈에 의존적이면 안된다

    */
    public enum Relationship
    {
        Parent,
        Child,
        Sibling
    }

    public class Person
    {
        public string Name;
        // public DateTime DateOfBirth;;
        // 생략
    }

    public interface IRelationshipBrowser
    {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }


    // low-level
    public class Relationships
    {
        private List<(Person, Relationship, Person)> relations = new List<(Person, Relationship, Person)>();

        public void AddParentAndChild(Person parent, Person child)
        {
            relations.Add((parent, Relationship.Parent, child));
            relations.Add((child, Relationship.Child, parent));
        }

        public List<(Person, Relationship, Person)> Relations => relations; // data structure가 직접 노출이되어 바꿀 수 없다
    }

    // high-level
    public class RelationshipsHigh : IRelationshipBrowser
    {

        private List<(Person, Relationship, Person)> relations = new List<(Person, Relationship, Person)>();

        public void AddParentAndChild(Person parent, Person child)
        {
            relations.Add((parent, Relationship.Parent, child));
            relations.Add((child, Relationship.Child, parent));
        }

        public IEnumerable<Person> FindAllChildrenOf(string name) // 자료구조가 직접 노출되지 않아 자료구조를 바꿀 수 있다
        {
            //foreach (var r in relations.Where(
            //    x => x.Item1.Name == "John" &&
            //    x.Item2 == Relationship.Parent))
            //{
            //    yield return r.Item3;
            //}
            // 위와 동일
            return relations.Where(x => x.Item3.Name == name && x.Item2 == Relationship.Parent)
                            .Select(r => r.Item3);
        }

        public List<(Person, Relationship, Person)> Relations => relations;
    }

    class Research
    {
        // low-level
        public Research(Relationships relationships)
        {
            var relations = relationships.Relations;
            foreach (var r in relations.Where(
                x => x.Item1.Name == "John" &&
                x.Item2 == Relationship.Parent))
            {
                Console.WriteLine($"John has a child called {r.Item3.Name}");
            }
        }

        // high-level
        public Research(IRelationshipBrowser browser)
        {
            foreach (var p in browser.FindAllChildrenOf("John"))
            {
                Console.WriteLine($"John has a child called {p.Name}");
            }
        }

        static void Main(string[] args)
        {
            var parent = new Person { Name = "John" };
            var child1 = new Person { Name = "Chris" };
            var child2 = new Person { Name = "Mary" };

            var relationships = new Relationships();
            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);

            new Research(relationships);
        }
    }
}
