using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BinaryWriter_and_BinaryReader
{
    class Program
    {
        static void Main(string[] args)
        {
            //Write Data Types values as Binary Values in Sample.dat file
            FileStream file = File.Create("Sample.dat");
            BinaryWriter binaryWriter = new BinaryWriter(file);
            binaryWriter.Write("String Value");
            binaryWriter.Write('A');
            binaryWriter.Write(true);
            binaryWriter.Close();
            //Read Binary values as respective data type's values from Sample.dat
            FileStream fileToOpen = File.Open("Sample.dat", FileMode.Open);
            BinaryReader binaryReader = new BinaryReader(fileToOpen);
            Console.WriteLine(binaryReader.ReadString());
            Console.WriteLine(binaryReader.ReadChar());
            Console.WriteLine(binaryReader.ReadBoolean());


            Console.WriteLine("=========WebRequest============");

            WebRequest request = WebRequest.Create("http://www.apress.com");

            using (WebResponse response = request.GetResponse())
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string result = reader.ReadToEnd();
                Console.WriteLine(result);
            }

            Console.ReadKey();
        }
    }
}
