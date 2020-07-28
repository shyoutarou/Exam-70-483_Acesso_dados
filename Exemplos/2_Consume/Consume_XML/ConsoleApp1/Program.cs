using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            CreatingXM_XmlWriter(sb);
            Parsing_XmlReader(sb);

            Console.ReadKey();
        }

        static void Parsing_XmlReader(StringBuilder sb)
        {
            // This will get the current WORKING directory (i.e. \bin\Debug)
            string workingDirectory = Environment.CurrentDirectory;
            // or: Directory.GetCurrentDirectory() gives the same result

            // This will get the current PROJECT directory
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            string xmlString = System.IO.File.ReadAllText(projectDirectory + "\\XMLFile1.xml");

            //Se for de um StringBuilder/StringWriter
            var xml = sb.ToString();

            using (StringReader stringReader = new StringReader(xmlString))
            {
                using (XmlReader xmlReader = XmlReader.Create(stringReader,
                new XmlReaderSettings() { IgnoreWhitespace = true }))
                {
                    xmlReader.MoveToContent();
                    xmlReader.ReadStartElement("people");
                    string firstName = xmlReader.GetAttribute("firstName");
                    string lastName = xmlReader.GetAttribute("lastName");
                    Console.WriteLine("Person: {0}{1}", firstName, lastName);
                    xmlReader.ReadStartElement("person");
                    Console.WriteLine("ContactDetails");
                    xmlReader.ReadStartElement("contactdetails");
                    string emailAddress = xmlReader.ReadString();
                    Console.WriteLine("Email address: {0}", emailAddress);

                    Console.WriteLine("=======xmlReader.Read()=======");
                    while (xmlReader.Read())// read the entire xml
                    {
                        Console.WriteLine(xmlReader.Value);
                    }
                }
            }
        }

        static StringBuilder CreatingXM_XmlWriter(StringBuilder sb)
        {
            using (StringWriter stream = new StringWriter(sb))
            {
                using (XmlWriter writer = XmlWriter.Create(stream, new XmlWriterSettings() { Indent = true }))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("people");
                    writer.WriteStartElement("person");
                    writer.WriteAttributeString("firstName", "John");
                    writer.WriteAttributeString("lastName", "Doe");
                    writer.WriteStartElement("contactdetails");
                    writer.WriteElementString("EmailAddress", "john@unknown.com");
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    writer.Flush();
                }
            }

            return sb;
        }
    }
}
