namespace _01_Initial_Setup
{
    using System;
    using System.Data.SqlClient;
    using System.IO;

    public class InitialSetup
    {
        public static void Main()
        {
            SqlConnection connection = new SqlConnection("Server=DESKTOP-QTOP4NO;Integrated Security=true");

            connection.Open();

            string sqlCreateDB = "CREATE DATABASE MinionsDB";
            SqlCommand createDBCommand = new SqlCommand(sqlCreateDB, connection);

            string query = File.ReadAllText(@"D:\SoftUni\C# DB Fundamentals\Databases Advanced-Entity Framework\Fetching-Resultsets\01_Initial-Setup\InitialSetup.sql");
            SqlCommand createTablesCommand = new SqlCommand(query, connection);

            using (connection)
            {
                createDBCommand.ExecuteNonQuery();
                Console.WriteLine(createTablesCommand.ExecuteNonQuery());
            }            
        }
    }
}
