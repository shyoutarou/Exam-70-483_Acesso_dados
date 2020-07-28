using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExecuteXmlReader_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PeopleConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM People FOR XML AUTO, XMLDATA";
                System.Xml.XmlReader xmlreader = cmd.ExecuteXmlReader();

                while (xmlreader.Read())
                {
                    var xml = xmlreader.ReadOuterXml();
                    xml = xml.Replace(@"/>", @"/>" + Environment.NewLine);

                    Console.WriteLine(xml);
                }
            }
            Console.ReadKey();
        }
    }
}
