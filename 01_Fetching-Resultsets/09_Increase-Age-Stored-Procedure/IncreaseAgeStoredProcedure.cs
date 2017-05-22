namespace _09_Increase_Age_Stored_Procedure
{
    using System;
    using System.Data.SqlClient;

    public class IncreaseAgeStoredProcedure
    {
        public static void Main()
        {
            SqlConnection connection =
                new SqlConnection("Server=DESKTOP-QTOP4NO;Database=MinionsDB;Integrated Security=true");

            connection.Open();

            int minionId = int.Parse(Console.ReadLine());

            using (connection)
            {
                string execProcQuery = @"EXEC usp_GetOlder @MinionId";
                SqlCommand execProcCommand = new SqlCommand(execProcQuery, connection);
                execProcCommand.Parameters.AddWithValue("@MinionId", minionId);
                execProcCommand.ExecuteNonQuery();

                string getMinionQuery = @"SELECT Name, Age FROM Minions WHERE Id = @MinionId";
                SqlCommand getMinionCommand = new SqlCommand(getMinionQuery, connection);
                getMinionCommand.Parameters.AddWithValue("@MinionId", minionId);
                SqlDataReader reader = getMinionCommand.ExecuteReader();
                
                using (reader)
                {
                    reader.Read();
                    Console.WriteLine($"{reader["Name"]} {reader["Age"]}");
                }
            }
        }
    }
}
