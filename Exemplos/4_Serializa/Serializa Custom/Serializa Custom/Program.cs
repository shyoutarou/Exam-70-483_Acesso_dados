using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;

namespace Serializa_Custom
{
    [Serializable]
    public class Person : ISerializable
    {
        private int _id;
        public string FirstName;
        public string LastName;
        public void SetId(int id) { _id = id; }
        public Person() { }
        public Person(SerializationInfo info, StreamingContext context)
        {
            FirstName = info.GetString("custom field 1");
            LastName = info.GetString("custom field 2");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("custom field 1", FirstName);
            info.AddValue("custom field 2", LastName);
        }

        [OnSerializing()]
        internal void OnSerializingMethod(StreamingContext context)
        {
            Console.WriteLine("OnSerializing.");
        }
        [OnSerialized()]
        internal void OnSerializedMethod(StreamingContext context)
        {
            Console.WriteLine("OnSerialized.");
        }
        [OnDeserializing()]
        internal void OnDeserializingMethod(StreamingContext context)
        {
            Console.WriteLine("OnDeserializing.");
        }
        [OnDeserialized()]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            Console.WriteLine("OnSerialized.");
        }
    }

    [Serializable]
    public class Teacher_custom : ISerializable
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Teacher_custom()
        {

        }
        protected Teacher_custom(SerializationInfo info, StreamingContext context)
        {
            this.ID = info.GetInt32("IDKey");
            this.Name = info.GetString("NameKey");
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("IDKey", 9999);
            info.AddValue("NameKey", "Novo Nome Prof");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Teacher_custom professor_cust = new Teacher_custom()
            {
                ID = 11,
                Name = "Prof Girafalles",
            };

            BinaryFormatter binaryFmt = new BinaryFormatter();

            using (var stream = new FileStream(@"xmlCustom.xml", FileMode.OpenOrCreate))
            {
                binaryFmt.Serialize(stream, professor_cust);
            }
            Console.WriteLine("A serialização Custom foi concluída!");

            using (var stream = new FileStream(@"xmlCustom.xml", FileMode.Open))
            {
                professor_cust = (Teacher_custom)binaryFmt.Deserialize(stream);
            }

            Console.WriteLine(professor_cust.ID);
            Console.WriteLine(professor_cust.Name);
            Console.WriteLine("Desserialização Custom concluída!");



            Person p = new Person { FirstName = "John", LastName = "Doe" };

            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream("data.bin", FileMode.Create))
            {
                formatter.Serialize(stream, p);
            }
            using (Stream stream = new FileStream("data.bin", FileMode.Open))
            {
                Person dp = (Person)formatter.Deserialize(stream);
            }

            Console.ReadKey();
        }
    }
}
