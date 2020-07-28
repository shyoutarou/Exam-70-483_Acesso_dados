using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Visual_Connect
{
    class Program
    {
        static void Main(string[] args)
        {
            //var connectionString = @"Data Source=localhost;Initial Catalog=School;Integrated Security=True";

            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            sqlConnectionStringBuilder.DataSource = @"localhost";
            sqlConnectionStringBuilder.InitialCatalog = "School";
            sqlConnectionStringBuilder.IntegratedSecurity = true;
            string connectionString = sqlConnectionStringBuilder.ToString();

            Sample tds = new Sample();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Student", connection);
                da.Fill(tds, tds.Student.TableName);
            } // Connection is automatically closed.

            foreach (DataRow item in tds.Student.Rows)
            {
                Console.WriteLine(string.Format("ID: {0} - Name: {1}", item[0], item[1]));
            }

            // This will get the current WORKING directory (i.e. \bin\Debug)
            string workingDirectory = Environment.CurrentDirectory;
            // or: Directory.GetCurrentDirectory() gives the same result

            // This will get the current PROJECT directory
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            var path = projectDirectory + "\\Sample.xsd";
            DataSet dataSet = new DataSet();
            dataSet.ReadXmlSchema(path);
            foreach (DataTable table in dataSet.Tables)
            {
                Console.WriteLine(table.TableName);
                foreach (DataColumn column in table.Columns)
                {
                    Console.WriteLine("\t{0}: {1}", column.ColumnName,
                        column.DataType.Name);
                }
            }

            //using (var xmlStream = new StreamReader(path))
            //{
            //    dataSet.ReadXmlSchema(xmlStream);
            //    foreach (DataTable table in dataSet.Tables)
            //    {
            //        Console.WriteLine(table.TableName);
            //        foreach (DataColumn column in table.Columns)
            //        {
            //            Console.WriteLine("\t{0}: {1}", column.ColumnName,
            //                column.DataType.Name);
            //        }
            //    }
            //};

            Console.ReadKey();
        }
    }
}
