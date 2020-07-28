using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tipo_implicito
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public static class IntExtensions
    {
        public static int Multiply(this int x, int y)
        {
            return x * y;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Variáveis de tipo implícito
            int i = 42;
            Stream m = new MemoryStream();
            //string s = i + m; // This line gives a compile error

            var it = 42;
            var mt = new MemoryStream();
            //string s = i + m;

            Dictionary<string, IEnumerable<Tuple<Type, int>>> data = new Dictionary<string, IEnumerable<Tuple<Type, int>>>();
            var implicitData = new Dictionary<string, IEnumerable<Tuple<Type, int>>>();

            var whatsMyType = GetData();

            // Sintaxe de inicialização do objeto
            // Create and initialize a new object
            Person p = new Person();
            p.FirstName = "John";
            p.LastName = "Doe";

            Person p2 = new Person
            {
                FirstName = "John",
                LastName = "Doe"
            };

            var people = new List<Person>{
                            new Person { FirstName =  "João", LastName = "Corça"},
                            new Person { FirstName = "Jane", LastName = "Doe"}};

            // Expressões lambda

            Func<int, int> myDelegate = delegate (int x) { return x * 2; };
            Console.WriteLine(myDelegate(21)); // Displays 42

            Func<int, int> myDelegate2 = x => x * 2;
            Console.WriteLine(myDelegate(21)); // Displays 42

            int z = 2;
            Console.WriteLine(z.Multiply(21)); // Displays 42

            // Tipos anônimos

            var person = new
            {
                FirstName = "John",
                LastName = "Doe"
            };

            Console.WriteLine(person.GetType().Name); // Displays “<>f__AnonymousType0`2”
            Console.ReadKey();
        }

        private static object GetData()
        {
            Console.WriteLine("GetData");
            return true;
        }
    }
}
