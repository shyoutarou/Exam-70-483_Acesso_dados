using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Serializacao_Binaria
{
    [Serializable]
    public class Teacher
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Salary;
        [NonSerialized]
        private int PrivateField;

        public void SetPrivate(int num) { PrivateField = num; }
    }


    [Serializable]
    class Person
    {
        private int _id;
        public string FirstName;
        public string LastName;
        public void SetId(int id) { _id = id; }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Teacher_Serialization();
            Person_Serialization();

            Console.ReadKey();
        }

        static void Teacher_Serialization()
        {
            // Criou a instância e inicializou
            Teacher professor = new Teacher()
            {
                ID = 1,
                Name = "Raimundo Nonato",
                Salary = 1000
            };

            professor.SetPrivate(666);

            // Serializador binário
            BinaryFormatter formatador = new BinaryFormatter();

            formatador.AssemblyFormat =
            System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Full;

            //Sample.bin(Arquivo binário é criado) para armazenar dados serializados binários
            using (FileStream file = new FileStream("Sample.bin", FileMode.Create))
            {
                // esta função serializa o "professor" (Objeto) em "arquivo" (Arquivo)
                formatador.Serialize(file, professor);
            }

            Console.WriteLine("A serialização binária foi concluída com êxito!");
            // Desserialização binária
            using (FileStream file = new FileStream("Sample.bin", FileMode.Open))
            {
                professor = (Teacher)formatador.Deserialize(file);
            }

            Console.WriteLine("Desserialização binária concluída com êxito!");
        }

        static void Person_Serialization()
        {
            Person person = new Person();
            person.SetId(1);
            person.FirstName = "Joe";
            person.LastName = "Smith";
            IFormatter formatter = new BinaryFormatter();

            using (FileStream file = new FileStream("Person.bin", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(file, person);
            }
        }
    }
}
