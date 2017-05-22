namespace _05_Change_Town_Names_Casing
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class ChangeTownNamesCasing
    {
        public static void Main()
        {
            SqlConnection connection = 
                new SqlConnection("Server=DESKTOP-QTOP4NO;Database=MinionsDB;Integrated Security=true");

            string country = Console.ReadLine();

            connection.Open();

            using (connection)
            {
                string updateTownsQuery = @"UPDATE Towns SET Name = UPPER(Name) WHERE Country = @Country";
                SqlCommand updateTownsCommand = new SqlCommand(updateTownsQuery, connection);
                updateTownsCommand.Parameters.AddWithValue("@Country", country);
                int affected = updateTownsCommand.ExecuteNonQuery();

                if (affected == 0)
                {
                    Console.WriteLine("No town names were affected.");
                    return;
                }

                Console.WriteLine($"{affected} town names were affected.");

                string selectTownsQuery = @"SELECT Name FROM Towns WHERE Country = @Country";
                SqlCommand selectTownsCommand = new SqlCommand(selectTownsQuery, connection);
                selectTownsCommand.Parameters.AddWithValue("@Country", country);
                SqlDataReader reader = selectTownsCommand.ExecuteReader();

                List<string> result = new List<string>();

                using (reader)
                {
                    while (reader.Read())
                    {
                        result.Add(reader["Name"].ToString());
                    }

                    Console.WriteLine("[" + string.Join(", ", result) + "]");
                }                 
            }
        }
    }
}
