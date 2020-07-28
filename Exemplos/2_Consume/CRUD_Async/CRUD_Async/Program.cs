using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

namespace CRUD_Async
{
    class Program
    {
        static void Main(string[] args)
        {
            //SelectDataFromTable().Wait();
            //SelectMultipleResultSets().Wait();
            //UpdateRows().Wait();
            //InsertRowWithParameterizedQuery().Wait();
            InsertRowTransactionScope().Wait();
            Console.ReadKey();
        }


        public static async Task SelectDataFromTable()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PeopleConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM People", connection);
                await connection.OpenAsync();
                SqlDataReader dataReader = await command.ExecuteReaderAsync();
                while (await dataReader.ReadAsync())
                {
                    string formatStringWithMiddleName = "Person({0}) is named {1}{2}{3}";
                    string formatStringWithoutMiddleName = "Person({0}) is named {1}{3}";
                    if ((dataReader["middlename"] == null))
                    {
                        Console.WriteLine(formatStringWithoutMiddleName, dataReader["id"],
                        dataReader["firstname"], dataReader["lastname"]);
                    }
                    else
                    {
                        Console.WriteLine(formatStringWithMiddleName, dataReader["id"],
                        dataReader["firstname"], dataReader["middlename"], dataReader["lastname"]);
                    }
                }
                dataReader.Close();
            }
        }

        public static async Task SelectMultipleResultSets()
        {
            string connectionString = ConfigurationManager.
            ConnectionStrings["PeopleConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(@"SELECT * FROM People;
    SELECT TOP 1 * FROM People ORDER BY LastName", connection);

                await connection.OpenAsync();
                SqlDataReader dataReader = await command.ExecuteReaderAsync();
                await ReadQueryResults(dataReader);
                await dataReader.NextResultAsync(); // Move to the next result set
                await ReadQueryResults(dataReader);
                dataReader.Close();
            }
        }

        private static async Task ReadQueryResults(SqlDataReader dataReader)
        {
            while (await dataReader.ReadAsync())
            {
                string formatStringWithMiddleName = @"Person({0}) is named {1}{2}{3}";
                string formatStringWithoutMiddleName = "Person({0}) is named {1}{3}";
                if ((dataReader["middlename"] == null))
                {
                    Console.WriteLine(formatStringWithoutMiddleName, dataReader["id"],
                    dataReader["firstname"], dataReader["lastname"]);
                }
                else
                {
                    Console.WriteLine(formatStringWithMiddleName, dataReader["id"],
                    dataReader["firstname"], dataReader["middlename"], dataReader["lastname"]);
                }
            }
        }

        public static async Task UpdateRows()
        {
            string connectionString = ConfigurationManager.
            ConnectionStrings["PeopleConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command1 = new SqlCommand("INSERT INTO People VALUES('Diane', 'Birch', null)", connection);
                SqlCommand commanddel = new SqlCommand("INSERT INTO People VALUES('John', 'Birch', ''); DELETE FROM People; --'", connection);


                SqlCommand command = new SqlCommand("UPDATE People SET FirstName ='John' WHERE id = 3", connection);
                await connection.OpenAsync();
                int numberOfUpdatedRows = await command.ExecuteNonQueryAsync();
                Console.WriteLine("Updated {0} rows", numberOfUpdatedRows);
            }
        }

        public static async Task InsertRowWithParameterizedQuery()
        {
            string connectionString = ConfigurationManager.
            ConnectionStrings["PeopleConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command =
                new SqlCommand(@"INSERT INTO People([FirstName], [LastName], [MiddleName]) 
                VALUES(@firstName, @lastName, @middleName)", connection);
                await connection.OpenAsync();
                command.Parameters.AddWithValue("@firstName", "John");
                command.Parameters.AddWithValue("@lastName", "Doe");
                command.Parameters.AddWithValue("@middleName", "Little");
                int numberOfInsertedRows = await command.ExecuteNonQueryAsync();
                Console.WriteLine("Inserted {0} rows", numberOfInsertedRows);
            }
        }

        public static async Task InsertRowTransactionScope()
        {
            string connectionString = ConfigurationManager.
            ConnectionStrings["PeopleConnection"].ConnectionString;

            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command1 = new SqlCommand(@"INSERT INTO People([FirstName], [LastName], [MiddleName])
                                        VALUES('Chuck', 'Lil', 'Norris')", connection);
                    SqlCommand command2 = new SqlCommand(@"INSERT INTO People([FirstName], [LastName], [MiddleName])
                                        VALUES('Ernesto', 'Tiharu', 'Jr.')", connection);
                    await connection.OpenAsync();
                    command1.ExecuteNonQuery();
                    command2.ExecuteNonQuery();
                }
                transactionScope.Complete();
            }
        }
    }
}
