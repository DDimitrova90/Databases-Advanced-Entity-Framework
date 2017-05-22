namespace _02_Get_Villains_Names
{
    using System;
    using System.Data.SqlClient;
    using System.IO;

    public class GetVillainsNames
    {
        public static void Main()
        {
            SqlConnection connection = 
                new SqlConnection("Server=DESKTOP-QTOP4NO;Database=MinionsDB;Integrated Security=true");

            connection.Open();

            using (connection)
            {
                string query = File.ReadAllText(@"D:\SoftUni\C# DB Fundamentals\Databases Advanced-Entity Framework\Fetching-Resultsets\02_Get-Villains-Names\Get-Villains-Names.sql");
                SqlCommand getVillainsNamesCommand = new SqlCommand(query, connection);

                SqlDataReader reader = getVillainsNamesCommand.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader[0] + " " + reader[1]);
                    }
                }
            }
        }
    }
}
