using System;
using System.Collections.Generic;

namespace Collections_and_Arrays
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a collection that is a list of strings
            var dogs = new List<string>();

            dogs.Add("Bulldog");
            dogs.Add("Collie");
            dogs.Add("Retriever");

            // Use foreach to iterate through the list
            foreach (var dog in dogs)
            {
                Console.WriteLine(dog + " ");
            }

            // You can also access items by index number 
            Console.WriteLine("By index number: ");
            Console.WriteLine(dogs[1]);



            // Create an array of integers
            int[] a1 = new int[] { 1, 3, 5, 7, 9 };

            foreach (int i in a1)
            {
                Console.WriteLine(i + " ");
            }

        }
    }
}
