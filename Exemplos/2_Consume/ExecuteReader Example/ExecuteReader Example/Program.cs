using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ExecuteReader_Example
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
                cmd.CommandText = "SELECT * FROM People";

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            var middlename = AddFieldValue(dr, "MiddleName");

                            Console.WriteLine(string.Format("MiddleName: {0}", middlename));
                            Console.WriteLine(string.Format("Id: {0}", dr[0]));
                            Console.WriteLine(string.Format("First Name: {0} , Last Name: {1}", dr["FirstName"], dr["LastName"]));
                        }
                    }
                }
            }

            Console.ReadKey();

        }

        private static string AddFieldValue(SqlDataReader row, string fieldName)
        {
            if (!DBNull.Value.Equals(row[fieldName]))
                return (string)row[fieldName] + " ";
            else if (Convert.IsDBNull(row[fieldName]))
                return "NA";
            else
                return String.Empty;
        }
    }
}
