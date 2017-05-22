namespace _08_Increase_Minions_Age
{
    using System;
    using System.Data.SqlClient;
    using System.Linq;

    public class IncreaseMinionsAge
    {
        public static void Main()
        {
            SqlConnection connection =
                new SqlConnection("Server=DESKTOP-QTOP4NO;Database=MinionsDB;Integrated Security=true");

            int[] minionsIds = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            connection.Open();

            using (connection)
            {
                string selectMinionsQuery = @"SELECT * FROM Minions";
                SqlCommand selectMinionsCommand = new SqlCommand(selectMinionsQuery, connection);
                SqlDataReader reader = selectMinionsCommand.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        if (minionsIds.Contains((int)reader["Id"]))
                        {
                            string[] name = reader["Name"].ToString().Split(' ');
                            string minionName = string.Empty;

                            for (int i = 0; i < name.Length; i++)
                            {
                                minionName += name[i].Substring(0, 1).ToUpper() + name[i].Substring(1) + " ";
                            }
                            
                            Console.WriteLine($"{minionName}{(int)reader["Age"] + 1}");
                        }
                        else
                        {
                            Console.WriteLine($"{reader["Name"]} {reader["Age"]}");
                        }
                    }
                }
            }
        }
    }
}
