using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Fill_DataTable
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PeopleConnection"].ConnectionString;
            DataTable tbl = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string command = "select * from Student";
                SqlDataAdapter ad = new SqlDataAdapter(command, connection);
                ad.Fill(tbl);//Now the data in DataTable (memory)
            }

            foreach (DataRow item in tbl.Rows)
            {
                Console.WriteLine(string.Format("ID: {0} - Name: {1}", item[0], item[1]));
            }



            //New Record to add in DataTable
            //DataRow newRow = tbl.NewRow();
            //newRow["StudentName"] = "Mary Moon";
            //tbl.Rows.Add(newRow);
            //DataRow newRow2 = tbl.NewRow();
            //newRow2["StudentName"] = "Bob Cuspe";
            //tbl.Rows.Add(newRow2);

            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    //Now newRow has to add in Database(Pass newRow Parameters to this insert query)
            //    string newCommand = @"Insert into Student(StudentName) Values(@StudentName)";
            //    SqlDataAdapter ad = new SqlDataAdapter(newCommand, connection);
            //    SqlCommand insertCommand = new SqlCommand(newCommand, connection);
            //    //Create the parameters
            //    insertCommand.Parameters.Add(new SqlParameter("@StudentName", SqlDbType.VarChar, 50, "StudentName"));
            //    //Associate Insert Command to DataAdapter so that it could add into Database
            //    ad.InsertCommand = insertCommand;
            //    ad.Update(tbl);
            //}


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Student", connection);
                //Create the update command
                SqlCommand update = new SqlCommand();
                update.Connection = connection;
                update.CommandType = CommandType.Text;
                update.CommandText = "UPDATE Student SET StudentName = @StudentName WHERE StudentID = @StudentID";
                //Create the parameters
                update.Parameters.Add(new SqlParameter("@StudentName", SqlDbType.VarChar, 50, "StudentName"));
                update.Parameters.Add(new SqlParameter("@StudentID", SqlDbType.Int, 0, "StudentID"));

                //Create the delete command
                SqlCommand delete = new SqlCommand();
                delete.Connection = connection;
                delete.CommandType = CommandType.Text;
                delete.CommandText = "DELETE FROM Student WHERE StudentID = @StudentID";
                //Create the parameters
                SqlParameter deleteParameter = new SqlParameter("@StudentID", SqlDbType.Int, 0, "StudentID");
                deleteParameter.SourceVersion = DataRowVersion.Original; delete.Parameters.Add(deleteParameter);
                //Associate the update and delete commands with the DataAdapter.
                da.UpdateCommand = update;
                da.DeleteCommand = delete;
                //Get the data.
                DataSet ds = new DataSet();
                da.Fill(ds, "Student");
                //Update the first row
                ds.Tables[0].Rows[0]["StudentName"] = "Ricky Martin";

                //Delete the second row.
                ds.Tables[0].Rows[1].Delete();

                //Update the database.
                da.Update(ds.Tables[0]);
            }

            Console.ReadLine();
        }
    }
}
