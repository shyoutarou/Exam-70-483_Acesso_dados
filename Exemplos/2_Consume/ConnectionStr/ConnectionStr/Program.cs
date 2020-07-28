using System.Configuration;
using System.Data.SqlClient;

namespace ConnectionStr
{

    class Program
    {
        static void Main(string[] args)
        {
            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            sqlConnectionStringBuilder.DataSource = @"(localdb)\v11.0";
            sqlConnectionStringBuilder.InitialCatalog = "ProgrammingInCSharp";


            //Obtém ou define um valor booliano que indica se a ID de Usuário e a Senha 
            //são especificadas na conexão(quando false) ou se as atuais credenciais da 
            //conta do Windows são usadas para autenticação(quando true).
            //sqlConnectionStringBuilder.IntegratedSecurity = true;
            string connectionString = sqlConnectionStringBuilder.ToString();
            connectionString = ConfigurationManager.ConnectionStrings["ProgrammingInCSharpConnection"].ConnectionString;
            connectionString = @"Persist Security Info = False; Integrated Security = true; Initial Catalog = Northwind; server = (local)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Execute operations against the database
            } // Connection is automatically closed.
        }

    }
}
