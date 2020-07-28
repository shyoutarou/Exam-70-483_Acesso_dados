using System;
using System.IO;
using System.Text;

namespace StringWriter_and_StringReader
{
    class Program
    {
        static void Main(string[] args)
        {
            StringReadWrite srw = new StringReadWrite();
        }
    }

    public class StringReadWrite
    {
        StringBuilder sb = new StringBuilder();

        public StringReadWrite()
        {
            WriteReadData_SemStringBuilder();
            WriteData();
            ReadData();

            Console.ReadKey();
        }

        public void WriteData()
        {
            // Note that we are passing the StringBuilder sb object to the StringWriter
            StringWriter sw = new StringWriter(sb);
            Console.WriteLine("Please enter your first and last name...");
            string name = Console.ReadLine();

            // Write the name to the StringBuilder sb object
            sw.WriteLine("Name: " + name);

            // Close the sw stream object
            sw.Flush();
            sw.Close();
        }

        public void ReadData()
        {
            // Note we are converting the sb object to a string and passing it to the StringReader
            StringReader sr = new StringReader(sb.ToString());

            Console.WriteLine("Reading the information...");

            // Use Peek to see if another character exists in sb
            while (sr.Peek() > -1)
            {
                // Read a line from the string and display it
                Console.WriteLine(sr.ReadLine());
            }

            Console.WriteLine(" ");
            Console.WriteLine("Thank you!");

            sr.Close();
        }


        public void WriteReadData_SemStringBuilder()
        {
            // Grava string ou caracteres
            using (StringWriter stringWriter = new StringWriter())
            {
                stringWriter.Write("Exemplo do String Writer");
                stringWriter.Write("Anexar texto");
                Console.WriteLine(stringWriter.ToString());
            }

            using (StringReader stringReader = new StringReader("Exemplo de leitor de string"))
            {
                Console.WriteLine(stringReader.ReadLine());
            }
        }
    }

}
