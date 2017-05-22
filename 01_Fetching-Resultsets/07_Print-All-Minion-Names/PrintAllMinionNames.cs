namespace _07_Print_All_Minion_Names
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;

    public class PrintAllMinionNames
    {
        public static void Main()
        {
            SqlConnection connection = 
                new SqlConnection("Server=DESKTOP-QTOP4NO;Database=MinionsDB;Integrated Security=true");

            connection.Open();

            using (connection)
            {
                string getNamesQuery = @"SELECT Name FROM Minions";
                SqlCommand getNamesCommand = new SqlCommand(getNamesQuery, connection);
                SqlDataReader reader = getNamesCommand.ExecuteReader();

                List<string> minionsNames = new List<string>();

                using (reader)
                {
                    while (reader.Read())
                    {
                        minionsNames.Add(reader[0].ToString());
                    }
                }

                while (minionsNames.Count() > 0)
                {
                    Console.WriteLine(minionsNames[0]);
                    minionsNames.RemoveAt(0);
                    Console.WriteLine(minionsNames[minionsNames.Count - 1]);
                    minionsNames.RemoveAt(minionsNames.Count - 1);
                }
            }
        }
    }
}
