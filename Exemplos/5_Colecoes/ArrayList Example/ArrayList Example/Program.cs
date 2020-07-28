using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayList_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayList_Code();
            Hashtable_Code();
            SortedList_Code();
            Queud_Code();
            Stack_Code();

            Console.ReadKey();
        }

        static void ArrayList_Code()
        {
            ArrayList arraylist = new ArrayList();
            //add objects in arraylist
            arraylist.Add(22);
            arraylist.Add("Ali");
            arraylist.Add(true);
            //Iterate over each index of arraylist
            for (int i = 0; i < arraylist.Count; i++)
            {
                Console.WriteLine(arraylist[i]);
            }
            arraylist.Remove(22);
            foreach (var item in arraylist)
            {
                Console.WriteLine(item);
            }
        }

        static void Hashtable_Code()
        {
            Hashtable owner = new Hashtable();
            //There are no keys but value can be duplicate
            owner.Add("Bill", "Microsoft");
            owner.Add("Paul", "Microsoft");
            owner.Add("Steve", "Apple");
            owner.Add("Mark", "Facebook");

            //Display value against key
            Console.WriteLine("Bill is the owner of {0}", owner["Bill"]);
            //ContainsKey can be use to test key before inserting
            if (!owner.ContainsKey("Trump"))
            {
                owner.Add("Trump", "The Trump Organization");
            }
            // When you use foreach to enumerate hash table elements,
            // the elements are retrieved as KeyValuePair objects.
            //DictionaryEntry is the pair of key & value
            foreach (DictionaryEntry item in owner)
            {
                Console.WriteLine("{0} is owner of {1}", item.Key, item.Value);
            }

            var allValues = owner.Values;
            foreach (var item in allValues)
            {
                Console.WriteLine("Company: {0}", item);
            }
        }

        static void SortedList_Code()
        {
            SortedList mySortedList = new SortedList();
            mySortedList.Add(3, "three");
            mySortedList.Add(2, "second");
            mySortedList.Add(1, "first");

            foreach (DictionaryEntry item in mySortedList)
            {
                Console.WriteLine(item.Value);
            }
        }

        static void Queud_Code()
        {

            Queue days = new Queue();
            //Add(Enque) objects in queus
            days.Enqueue("Mon");
            days.Enqueue("Tue");
            days.Enqueue("Wed");
            days.Enqueue("Thu");
            days.Enqueue("Fri");
            days.Enqueue("Sat");
            days.Enqueue("Sun");
            // Displays the properties and values of the Queue.
            Console.WriteLine("Total elements in queue are {0}", days.Count);
            //Remove and return first element of the queue
            Console.WriteLine("{0}", days.Dequeue());
            //return first element of queue without removing it from queue
            Console.WriteLine("{0}", days.Peek());//return 'Tue'
            //Iterate over each element of queue
            Console.WriteLine();
        }

        static void Stack_Code()
        {
            Stack history = new Stack();
            //Insert browser history in stack
            history.Push("google.com");
            history.Push("facebook.com/imaliasad");
            history.Push("twitter.com/imaliasad");
            history.Push("youtube.com");
            // Displays the properties and values of the Stack.
            Console.WriteLine("Total elements in stack are {0}", history.Count);
            //Remove and return top element of the Stack
            Console.WriteLine("{0}", history.Pop());
            //return top element of Stack without removing it from Stack
            //return 'twitter.com/imaliasad'
            Console.WriteLine("{0}", history.Peek());
            //Iterate over each element of Stack
            foreach (var item in history)
            {
                Console.WriteLine(item);
            }
        }

    }
}
