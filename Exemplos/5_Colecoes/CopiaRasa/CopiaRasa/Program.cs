using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopiaRasa
{
    class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Person[] orginal = new Person[1];
            orginal[0] = new Person()
            {
                Name = "John"
            };
            Person[] clone = (Person[])orginal.Clone();
            clone[0].Name = "Mary";

            Person[] clonereplace = (Person[])orginal.Clone();
            clonereplace[0] = new Person() { Name = "Bob" };

            Console.WriteLine("Original name " + orginal[0].Name); //Original name Mary
            Console.WriteLine("Clone name " + clone[0].Name); //Clone name Mary
            Console.WriteLine("Replaced Clone name " + clonereplace[0].Name); //Replaced Clone name Bob

            Console.ReadKey();
        }
    }


}
