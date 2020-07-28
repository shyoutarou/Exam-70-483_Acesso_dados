using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionGenerics
{
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }


    class Program
    {
        static void Main(string[] args)
        {
            //List_Generic();
            //Dictionary_Generic();
            HashSet_Generic();
            //Queud_Generic();
            //Stack_Generic();

            Console.ReadKey();
        }

        private static void List_Generic()
        {
            List<Person> people = new List<Person>();
            //Add Person in list
            people.Add(new Person { Name = "Ali", Age = 22 });
            people.Add(new Person { Name = "Sundus", Age = 21 });
            people.Add(new Person { Name = "Hogi", Age = 12 });
            //Get total number of person in list
            Console.WriteLine("Total person are: {0}", people.Count);
            //Iterate over each person
            foreach (var person in people)
            {
                Console.WriteLine("Name: {0} - Age: {1}", person.Name, person.Age);
            }
            //Instantiate and populate list of int with values
            List<int> marks = new List<int> { 10, 25, 15, 23 };
            //Remove '25' from the list
            marks.Remove(25);
            //Get each element by its index
            Console.Write("Marks: ");
            for (int i = 0; i < marks.Count; i++)
            {
                Console.Write(marks[i] + " ");
            }
        }

        private static void Dictionary_Generic()
        {
            //Initialize Dictionary (int for roll# and assign it to student)
            Dictionary<int, Person> people = new Dictionary<int, Person>();
            //Adding student against their roll#
            people.Add(53, new Person { Name = "Ali Asad", Age = 22 });
            people.Add(11, new Person { Name = "Sundus Naveed", Age = 21 });
            people.Add(10, new Person { Name = "Hogi", Age = 12 });
            //Display Name against key
            Console.WriteLine("Roll# 11 is: {0}", people[11].Name);

            people[0] = new Person { Age = 4, Name = "Name4" };

            Person result;
            if (!people.TryGetValue(5, out result))
            {
                Console.WriteLine("No person with a key of 5 can be found");
            }

            //ContainsKey can be use to test key before inserting
            if (!people.ContainsKey(13))
            {
                people.Add(13, new Person { Name = "Lakhtey", Age = 21 });
            }
            // When you use foreach to enumerate elements of dictionary,
            // the elements are retrieved as KeyValuePairPair object.
            //KeyValuePair<TKey, TValue> is the pair of key & value for dictionary
            foreach (KeyValuePair<int, Person> student in people)
            {
                Console.WriteLine("Roll#: {0} - Name: {1} - Age: {2}",
                student.Key, student.Value.Name, student.Value.Age);
            }
            //Get All values stored in Dictionary
            var allValues = people.Values;
            foreach (var student in allValues)
            {
                Console.WriteLine("Name: {0} - Age: {1}",
                student.Name, student.Age);
            }
        }

        private static void HashSet_Generic()
        {
            HashSet<int> oddSet = new HashSet<int>();
            HashSet<int> evenSet = new HashSet<int>();
            for (int x = 1; x <= 10; x++)
            {
                if (x % 2 == 0)
                    evenSet.Add(x);
                else
                    oddSet.Add(x);
            }
            DisplaySet(oddSet);
            DisplaySet(evenSet);
            oddSet.UnionWith(evenSet);
            DisplaySet(oddSet);
        }

        private static void DisplaySet(HashSet<int> set)
        {
            Console.Write("{");
            foreach (int i in set)
            {
                Console.Write("{0}", i);
            }
            Console.WriteLine("}");
        }

        private static void Queud_Generic()
        {
            Queue<string> days = new Queue<string>();
            //Add(Enque) string object in days
            days.Enqueue("Mon");
            days.Enqueue("Tue");
            days.Enqueue("Wed");
            days.Enqueue("Thu");
            days.Enqueue("Fri");
            days.Enqueue("Sat");
            days.Enqueue("Sun");
            // Displays the properties and values of the Queue.
            Console.WriteLine("Total elements in queue<string> are {0}",
            days.Count);
            //Remove and return first element of the queue<string>
            Console.WriteLine("{0}", days.Dequeue());
            //return first element of queue without removing it from queue
            //return 'Tue'
            Console.WriteLine("{0}", days.Peek());
            //Iterate over each element of queue
            foreach (var item in days)
            {
                Console.WriteLine(item);
            }
        }

        private static void Stack_Generic()
        {
            Stack<string> history = new Stack<string>();
            //Insert browser history in stack<string>
            history.Push("google.com");
            history.Push("facebook.com/imaliasad");
            history.Push("twitter.com/imaliasad");
            history.Push("youtube.com");
            // Displays the properties and values of the Stack<string>.
            Console.WriteLine("Total elements in stack<string> are {0}",
            history.Count);
            //Remove and return top element of the Stack<string>
            Console.WriteLine("{0}", history.Pop());
            //return top element of Stack<string> without removing it from Stack
            //return 'twitter.com/imaliasad'
            Console.WriteLine("{0}", history.Peek());
            //Iterate over each element of Stack<string>
            foreach (var item in history)
            {
                Console.WriteLine(item);
            }
        }


    }
}
