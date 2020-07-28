using System;
using System.IO;
using System.Xml.Serialization;

namespace Serializacao_XML
{
    [Serializable]
    [XmlRoot("Teacher")]
    public class Teacher
    {
        [XmlAttribute("ID")]
        public int ID { get; set; }
        [XmlElement("Name")]
        public string Name { get; set; }
        public long Salary { get; set; }
        [XmlIgnore]
        public string IgnoraCampo { get; set; }
        [XmlElement("Students")]
        public studentClass st { get; set; }
    }

    [Serializable]
    public class studentClass
    {
        [XmlAttribute("RollNo")]
        public int rollno { get; set; }
        [XmlElement("Marks")]
        public int marks { get; set; }
    }


    class Program
    {
        static void Main(string[] args)
        {
            // Criou a instância e inicializou
            Teacher professor = new Teacher()
            {
                ID = 1,
                Name = "Raimundo Nonato",
                Salary = 1000,
                st = new studentClass
                {
                    rollno = 1,
                    marks = 50
                }
            };

            XmlSerializer xml = new XmlSerializer(typeof(Teacher));
            using (var stream = new FileStream("Sample.xml", FileMode.Create))
            {
                xml.Serialize(stream, professor);
            }

            Console.WriteLine("A serialização XML foi concluída!");

            using (var stream = new FileStream("Sample.xml", FileMode.Open))
            {
                professor = (Teacher)xml.Deserialize(stream);
            }

            Console.WriteLine(professor.ID);
            Console.WriteLine(professor.Name);
            Console.WriteLine(professor.Salary);
            Console.WriteLine(professor.st.rollno);
            Console.WriteLine(professor.st.marks);
            Console.WriteLine("Desserialização XML concluída!");
            Console.ReadKey();
        }
    }
}
