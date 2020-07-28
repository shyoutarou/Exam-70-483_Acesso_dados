using System;
using System.Web.Script.Serialization;

namespace Serializacao_JavaScript
{
    class Program
    {
        private class Teacher
        {
            private int id { get; set; }
            public string name { get; set; }
            public long salary { get; set; }
        }

        static void Main(string[] args)
        {
            // Criou a instância e inicializou
            Teacher professor = new Teacher()
            {
                name = "Raimundo Nonato",
                salary = 1000,
            };

            JavaScriptSerializer dataContract = new JavaScriptSerializer();
            string serializedDataInStringFormat = dataContract.Serialize(professor);
            Console.WriteLine("A serialização JavaScript foi concluída!");

            professor = dataContract.Deserialize<Teacher>(serializedDataInStringFormat);

            Console.WriteLine(professor.name);
            Console.WriteLine(professor.salary);
            Console.WriteLine("Desserialização JavaScript concluída!");

            Console.ReadKey();
        }
    }
}
