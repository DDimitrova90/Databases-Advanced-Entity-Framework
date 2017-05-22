namespace _03_Get_Minion_Names
{
    using System;
    using System.Data.SqlClient;
    using System.IO;

    public class GetMinionNames
    {
        public static void Main()
        {
            SqlConnection connection = 
                new SqlConnection("Server=DESKTOP-QTOP4NO;Database=MinionsDB;Integrated Security=true");

            connection.Open();

            Console.Write("Enter villain id: ");
            int villainId = int.Parse(Console.ReadLine());

            using (connection)
            {
                string query = File.ReadAllText(@"D:\SoftUni\C# DB Fundamentals\Databases Advanced-Entity Framework\Fetching-Resultsets\03_Get-Minion-Names\GetMinionNames.sql");
                SqlCommand getMinionNamesCommand = new SqlCommand(query, connection);
                getMinionNamesCommand.Parameters.AddWithValue("@VillainId", villainId);
                SqlDataReader reader = getMinionNamesCommand.ExecuteReader();

                using (reader)
                {
                    if (!reader.Read())
                    {
                        Console.WriteLine($"No villain with ID {villainId} exists in the database.");
                        return;
                    }

                    Console.WriteLine($"Villain: {reader[0]}");
                    int num = 1;

                    if (reader[1].ToString() == "0")
                    {
                        Console.WriteLine("(no minions)");
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"{num}. {reader[1]} {reader[2]}");
                    }


                    while (reader.Read())
                    {
                        num++;
                        Console.WriteLine($"{num}. {reader[1]} {reader[2]}");
                    }
                }
            }
        }
    }
}
