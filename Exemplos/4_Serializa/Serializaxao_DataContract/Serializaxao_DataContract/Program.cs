using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Serializaxao_DataContract
{
    [DataContract]
    public class Teacher
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember(Name = "Nome")]
        public string Name { get; set; }
        [IgnoreDataMember]
        public string IgnoraCampo { get; set; }

        [DataMember]
        public long Salary { get; set; }
    }

    class Program
    {
        public static Teacher SerializaDataContract(Teacher professor)
        {
            DataContractSerializer dataContract = new DataContractSerializer(typeof(Teacher));
            using (var stream = new FileStream("Sample.xml", FileMode.Create))
            {
                dataContract.WriteObject(stream, professor);
            }
            Console.WriteLine("A serialização DataContract foi concluída!");

            using (var stream = new FileStream("Sample.xml", FileMode.Open))
            {
                professor = (Teacher)dataContract.ReadObject(stream);
            }

            return professor;
        }

        public static Teacher SerializaJSONDataContract(Teacher professor)
        {
            //Serialization
            DataContractJsonSerializer dataContract = new DataContractJsonSerializer(typeof(Teacher));
            using (var stream = new FileStream("Sample.json", FileMode.Create))
            {
                dataContract.WriteObject(stream, professor);
            }
            Console.WriteLine("A serialização JSON DataContract foi concluída!");

            using (var stream = new FileStream("Sample.json", FileMode.Open))
            {
                professor = (Teacher)dataContract.ReadObject(stream);
            }

            return professor;
        }

        static void Main(string[] args)
        {
            // Criou a instância e inicializou
            Teacher professor = new Teacher()
            {
                ID = 1,
                Name = "Raimundo Nonato",
                IgnoraCampo = "kkkkk",
                Salary = 1000,
            };

            professor = SerializaDataContract(professor);

            Console.WriteLine(professor.ID);
            Console.WriteLine(professor.Name);
            Console.WriteLine(professor.Salary);
            Console.WriteLine("Desserialização DataContract concluída!");

            professor.ID = 2;
            professor.Name = "Prof Girafalles";
            professor.IgnoraCampo = "blabla";
            professor.Salary = 55000;

            professor = SerializaJSONDataContract(professor);

            Console.WriteLine(professor.ID);
            Console.WriteLine(professor.Name);
            Console.WriteLine(professor.Salary);
            Console.WriteLine("Desserialização JSON DataContract concluída!");

            Console.ReadKey();
        }
    }
}
