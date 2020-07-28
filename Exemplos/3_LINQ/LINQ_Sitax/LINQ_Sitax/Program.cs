using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_Sitax
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] fruits = new string[]
                            {
                "Apple","Mango","Strawberry","Date",
                "Banana","Avocado","Cherry","Grape",
                "Guava","Melon","Orange","Tomato"
                            };

            int fruitsLength = fruits.Where(p => p.StartsWith("A")).Count();

            Console.WriteLine(fruitsLength); //2

            int result = (from p in fruits
                          where p.StartsWith("A")
                          select p).Count();

            Console.WriteLine(result); //2

            Console.ReadKey();

        }
    }
}
