using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Linq;

namespace LINQtoXML
{
    class Person
    {
        public int ID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailaddress { get; set; }
        public string phonenumber { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // This will get the current WORKING directory (i.e. \bin\Debug)
            string workingDirectory = Environment.CurrentDirectory;
            // or: Directory.GetCurrentDirectory() gives the same result

            // This will get the current PROJECT directory
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            string xmlString = System.IO.File.ReadAllText(projectDirectory + "\\XMLFile.xml");

            //LINQToXML();

            ////QUERY
            QUERYXML(xmlString, projectDirectory);

            ////CREATE
            //CREATEXML(projectDirectory);

            //UPDATE
            //UPDATEXElement(xmlString, projectDirectory);
            //UPDATEXDocument(projectDirectory);

            //UpdateFuncionalXML(xmlString);

            Console.ReadKey();
        }

        static void LINQToXML()
        {
            List<Person> persons = new List<Person>()
                {
                    new Person() { firstName = "John", lastName = "Smith", emailaddress = "john@unknown.com" },
                    new Person() { firstName = "Jane", lastName = "Doe", emailaddress = "jane@unknown.com" , phonenumber = "001122334455" },
                };

            var xmlpersons = new XElement("people",
                                            from e in persons
                                            select new XElement("person", new XElement("contactdetails", new XElement("emailaddress", e.emailaddress),
                                                                                                        e.phonenumber == null ? null : new XElement("phonenumber", e.phonenumber)),
                                                                new XAttribute("firstName", e.firstName),
                                                                new XAttribute("lastName", e.lastName)
                                                                )
                                            );
            Console.WriteLine(xmlpersons);
        }

        static void QUERYXML(string xmlString, string projectDirectory)
        {
            Stream xmlFromFile = File.Open(projectDirectory + "\\XMLFile.xml", FileMode.Open);
            StreamReader reader = new StreamReader(xmlFromFile);
            string xmlData = reader.ReadToEnd();

            Console.WriteLine("StreamReader: " + xmlData);

            XDocument doc = XDocument.Parse(xmlString);
            IEnumerable<string> personNames = from p in doc.Descendants("person")
                                              select (string)p.Attribute("firstName")
                                              + " " + (string)p.Attribute("lastName");
            foreach (string s in personNames)
            {
                Console.WriteLine(s);
            }


            IEnumerable<string> personNames2 = from p in doc.Descendants("person")
                                               where p.Descendants("phonenumber").Any()
                                               let name = (string)p.Attribute("firstName")
                                               + " " + (string)p.Attribute("lastName")
                                               orderby name
                                               select name;

            foreach (string s in personNames2)
            {
                Console.WriteLine(s);
            }

        }

        static void CREATEXML(string projectDirectory)
        {
            XElement root = new XElement("Root",
                                new List<XElement>
                                {
                        new XElement("Child1"),
                        new XElement("Child2"),
                        new XElement("Child3")
                                },
                                new XAttribute("MyAttribute", 42));
            root.Add(new XElement("Name", "Hamza Ali"));
            root.Add(new XElement("Age", "21"));
            root.Add(new XElement("Address", "Pakistan"));
            root.Add(new XAttribute("AddAttribute", 3.21));

            Console.WriteLine(root);
            //root.Save(projectDirectory + "\\test.xml");
        }

        static void UPDATEXElement(string xmlString, string projectDirectory)
        {
            XElement root2 = XElement.Parse(xmlString);
            foreach (XElement p in root2.Descendants("person"))
            {
                string name = (string)p.Attribute("firstName") + (string)p.Attribute("lastName");
                p.Add(new XAttribute("IsMale", name.Contains("John")));
                XElement contactDetails = p.Element("contactdetails");
                if (!contactDetails.Descendants("phonenumber").Any())
                {
                    contactDetails.Add(new XElement("phonenumber", "009999334455"));
                }
            }

            Console.WriteLine(root2);
            //root2.Save(projectDirectory + "\\updatetest.xml");
        }

        static void UPDATEXDocument(string projectDirectory)
        {

            var file = projectDirectory + "\\updatetest.xml";
            string xmlData = System.IO.File.ReadAllText(file);

            XDocument document = new XDocument();
            document = XDocument.Parse(xmlData);
            //this will read the Name's Node if the age is 21
            //var contactDetails = (from p in document.Descendants("person")
            //                where p.Attribute("firstName").Value == "John"
            //                select p.Descendants("contactdetails")).FirstOrDefault();

            var contactDetails = document.Descendants("person")
                    .Where(e => e.Attribute("firstName").Value == "John")
                    .Select(e => e.Descendants("contactdetails")).FirstOrDefault();

            foreach (XElement contacs in contactDetails)
            {
                if (contacs.Element("emailaddress") != null)
                {
                    var mailnode = contacs.Element("emailaddress");

                    Console.WriteLine(mailnode.GetType());
                    Console.WriteLine(mailnode.Value);

                    mailnode.Value = "john@unknown.com";
                }
            }

            Console.WriteLine(document);
            //document.Save(file);
        }

        static void UpdateFuncionalXML(string xmlString)
        {
            XElement root3 = XElement.Parse(xmlString);
            XElement newTree = new XElement("People",
                                            from p in root3.Descendants("person")
                                            let name = (string)p.Attribute("firstName") + (string)p.Attribute("lastName")
                                            let contactDetails = p.Element("contactdetails")
                                            select new XElement("person", new XAttribute("IsMale", name.Contains("John")),
                                            p.Attributes(),
                                            new XElement("contactdetails",
                                            contactDetails.Element("emailaddress"),
                                            contactDetails.Element("phonenumber") ?? new XElement("phonenumber", "112233455")
            )));

            Console.WriteLine(root3);
        }


        static void DELETEXML(string projectDirectory)
        {
            var file = projectDirectory + "\\updatetest.xml";
            string xmlData = System.IO.File.ReadAllText(file);

            XDocument document = new XDocument();
            document = XDocument.Parse(xmlData);
            document.Descendants().Where(s => s.Value == "Pakistan").Remove();
            document.Save(file);
        }
    }
}
