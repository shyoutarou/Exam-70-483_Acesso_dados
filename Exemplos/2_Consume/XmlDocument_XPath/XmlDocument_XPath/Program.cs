using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;

namespace XmlDocument_XPath
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlDocument doc = new XmlDocument();

            // This will get the current WORKING directory (i.e. \bin\Debug)
            string workingDirectory = Environment.CurrentDirectory;
            // or: Directory.GetCurrentDirectory() gives the same result

            // This will get the current PROJECT directory
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            string xmlString = System.IO.File.ReadAllText(projectDirectory + "\\XMLFile1.xml");

            Console.WriteLine("=====sReader====");

            //to read xml string as a stream
            StringReader sReader = new StringReader(xmlString);
            doc.Load(sReader);
            foreach (XmlNode item in doc.DocumentElement)
            {
                Console.WriteLine(item.InnerText);
            }

            Console.WriteLine("=====sReader====");

            doc.LoadXml(xmlString);
            XmlNodeList nodes = doc.GetElementsByTagName("person");
            // Output the names of the people in the document
            foreach (XmlNode node in nodes)
            {
                string firstName = node.Attributes["firstName"].Value;
                string lastName = node.Attributes["lastName"].Value;
                Console.WriteLine("Name: {0}{1}", firstName, lastName);
            }
            // Start creating a new node
            XmlNode newNode = doc.CreateNode(XmlNodeType.Element, "person", "");
            XmlAttribute firstNameAttribute = doc.CreateAttribute("firstName");
            firstNameAttribute.Value = "Foo";
            XmlAttribute lastNameAttribute = doc.CreateAttribute("lastName");
            lastNameAttribute.Value = "Bar";
            newNode.Attributes.Append(firstNameAttribute);
            newNode.Attributes.Append(lastNameAttribute);
            doc.DocumentElement.AppendChild(newNode);
            Console.WriteLine("Modified xml...");
            doc.Save(Console.Out);




            //XmlDocument doc = new XmlDocument();
            //doc.LoadXml(xml); // Can be found in Listing 4-43
            XPathNavigator nav = doc.CreateNavigator();
            string query = "//people/person[@firstName='Jane']";

            XPathNodeIterator nodes_xpath = nav.Select("/people/person");
            nodes_xpath.MoveNext();
            XPathNavigator nodesNavigator = nodes_xpath.Current;
            XPathNodeIterator nodesText = nodesNavigator.SelectDescendants(XPathNodeType.Text, false);

            while (nodesText.MoveNext())
                Console.WriteLine(nodesText.Current.Value);

            XPathNodeIterator iterator = nav.Select(query);
            Console.WriteLine(iterator.Count); // Displays 1
            while (iterator.MoveNext())
            {
                string firstName = iterator.Current.GetAttribute("firstName", "");
                string lastName = iterator.Current.GetAttribute("lastName", "");
                Console.WriteLine("Name: {0}{1}", firstName, lastName);
            }


            Console.ReadKey();
        }
    }
}
