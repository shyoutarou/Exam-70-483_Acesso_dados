using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ExecuteNonQuery_Example
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
                cmd.CommandText = "INSERT INTO People (FirstName, LastName) " + "VALUES ('Joe', 'Smith')";
                int result = cmd.ExecuteNonQuery();

                if (result > 0) Console.WriteLine("Data is Inserted");
                else Console.WriteLine("Error while inserting");




                SqlCommand cmd_sp = new SqlCommand();
                cmd_sp.Connection = connection;
                cmd_sp.CommandType = CommandType.StoredProcedure;
                cmd_sp.CommandText = "PersonInsert";
                cmd_sp.Parameters.Add(new SqlParameter("@FirstName", "Sasha"));
                cmd_sp.Parameters.Add(new SqlParameter("@LastName", "Gray"));
                int result_sp = cmd_sp.ExecuteNonQuery();

            }
            Console.ReadKey();
        }
    }
}
