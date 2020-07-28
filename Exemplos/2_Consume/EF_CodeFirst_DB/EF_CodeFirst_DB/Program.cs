using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_CodeFirst_DB
{
    class Program
    {
        static void Main(string[] args)
        {
            using (EntityModel ctx = new EntityModel())
            {
                var person = new People { FirstName = "John", LastName = "Doe" };
                ctx.People.Add(person);
                ctx.SaveChanges();

                // Display all Blogs from the database
                var query = from b in ctx.People
                            orderby b.FirstName
                            select b;

                Console.WriteLine("All People in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.FirstName);
                }
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
