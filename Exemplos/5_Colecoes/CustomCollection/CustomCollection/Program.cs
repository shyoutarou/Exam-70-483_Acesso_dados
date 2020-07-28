using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CustomCollection
{
    public class Person
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    public class PeopleCollection : List<Person>
    {
        public void RemoveByAge(int age)
        {
            for (int index = this.Count - 1; index >= 0; index--)
            {
                if (this[index].Age == age)
                {
                    this.RemoveAt(index);
                }
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Person p in this)
            {
                sb.AppendFormat("{0} {1} is {2}", p.FirstName, p.LastName, p.Age);
            }
            return sb.ToString();
        }
    }

    class PersonCollection : CollectionBase
    {
        public void Add(Person person)
        {
            List.Add(person);
        }

        public void Insert(int index, Person person)
        {
            List.Insert(index, person);
        }

        public void Remove(Person person)
        {
            List.Remove(person);
        }

        public Person this[int index]
        {
            get { return (Person)List[index]; }
            set { List[index] = value; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PeopleCollection people = new PeopleCollection {
                    new Person{ FirstName ="John", LastName = "Doe", Age = 42},
                    new Person{ FirstName = "Jane", LastName = "Doe", Age = 21 }};

            people.RemoveByAge(42);
            Console.WriteLine(people.Count); // Displays: 1

            PersonCollection persons = new PersonCollection();

            persons.Add(new Person() { ID = 1, FirstName = "John", LastName = "Smith" });
            persons.Add(new Person() { ID = 2, FirstName = "Jane", LastName = "Doe" });
            persons.Add(new Person() { ID = 3, FirstName = "Bill Jones", LastName = "Smith" });

            foreach (Person person in persons)
            {
                Console.WriteLine(person.FirstName);
            }

            Console.ReadKey();
        }
    }
}
