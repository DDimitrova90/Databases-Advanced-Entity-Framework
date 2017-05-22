namespace _04_Add_Minion
{
    using System;
    using System.Data.SqlClient;

    public class AddMinion
    {
        public static void Main()
        {
            string[] minionsArgs = Console.ReadLine()
                .Split(new char[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string minionName = minionsArgs[1];
            int minionAge = int.Parse(minionsArgs[2]);
            string minionTown = minionsArgs[3];

            string[] villainArgs = Console.ReadLine()
                .Split(new char[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string villainName = villainArgs[1];
            string villainEvilness = "evil";

            CheckAndAddTown(minionTown);

            CheckAndAddVillain(villainName, villainEvilness);

            int townIndex = GetTownIndex(minionTown);

            AddMinions(minionName, minionAge, townIndex);

            int minionIndex = GetMinionIndex(minionName);
            int villainIndex = GetVillainIndex(villainName);

            SetMinionToVillain(minionIndex, villainIndex, minionName, villainName);
        }

        public static void SetMinionToVillain(int minionIndex, int villainIndex, string minionName, string villainName)
        {
            SqlConnection connection =
                new SqlConnection("Server=DESKTOP-QTOP4NO;Database=MinionsDB;Integrated Security=true");
            connection.Open();

            using (connection)
            {
                string insertQuery = 
                    @"INSERT INTO MinionsVillains VALUES (@minionId, @villainId)";
                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddRange(new[]
                {
                new SqlParameter("@minionId", minionIndex),
                new SqlParameter("@villainId", villainIndex)
                });
                int affected = insertCommand.ExecuteNonQuery();

                Console.WriteLine($"{affected} row was affected!");
                Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
            }
        }

        public static int GetVillainIndex(string villainName)
        {
            SqlConnection connection =
                new SqlConnection("Server=DESKTOP-QTOP4NO;Database=MinionsDB;Integrated Security=true");
            connection.Open();

            using (connection)
            {
                string getIndexQuery = @"SELECT Id FROM Villains WHERE Name = @villainName";
                SqlCommand getIndexCommand = new SqlCommand(getIndexQuery, connection);
                getIndexCommand.Parameters.AddWithValue("@villainName", villainName);
                int villainIndex = (int)getIndexCommand.ExecuteScalar();
                return villainIndex;
            }
        }

        public static int GetMinionIndex(string minionName)
        {
            SqlConnection connection =
                new SqlConnection("Server=DESKTOP-QTOP4NO;Database=MinionsDB;Integrated Security=true");
            connection.Open();

            using (connection)
            {
                string getIndexQuery = @"SELECT Id FROM Minions WHERE Name = @minionName";
                SqlCommand getIndexCommand = new SqlCommand(getIndexQuery, connection);
                getIndexCommand.Parameters.AddWithValue("@minionName", minionName);
                int index = (int)getIndexCommand.ExecuteScalar();
                return index;
            }
        }

        public static int GetTownIndex(string minionTown)
        {
            SqlConnection connection =
                new SqlConnection("Server=DESKTOP-QTOP4NO;Database=MinionsDB;Integrated Security=true");
            connection.Open();

            using (connection)
            {
                string getIndexQuery = @"SELECT Id FROM Towns WHERE Name = @townName";
                SqlCommand getIndexCommand = new SqlCommand(getIndexQuery, connection);
                getIndexCommand.Parameters.AddWithValue("@townName", minionTown);
                int townIndex = (int)getIndexCommand.ExecuteScalar();
                return townIndex;
            }
        }

        public static void CheckAndAddVillain(string villainName, string villainEvilness)
        {
            SqlConnection connection =
                new SqlConnection("Server=DESKTOP-QTOP4NO;Database=MinionsDB;Integrated Security=true");
            connection.Open();

            using (connection)
            {
                string checkVillainQuery = @"SELECT Name FROM Villains WHERE Name = @villainName";
                SqlCommand checkVillainCommand = new SqlCommand(checkVillainQuery, connection);
                checkVillainCommand.Parameters.AddWithValue("@villainName", villainName);
                string villainResult = (string)checkVillainCommand.ExecuteScalar();

                if (villainResult == null)
                {
                    string insertVillainQuery = @"INSERT INTO Villains VALUES (@villainName, @villainEvilness)";
                    SqlCommand insertVillainCommand = new SqlCommand(insertVillainQuery, connection);
                    SqlParameter paramName = new SqlParameter("@villainName", villainName);
                    SqlParameter paramEvilness = new SqlParameter("@villainEvilness", villainEvilness);
                    insertVillainCommand.Parameters.Add(paramName);
                    insertVillainCommand.Parameters.Add(paramEvilness);
                    int affected = insertVillainCommand.ExecuteNonQuery();

                    Console.WriteLine($"{affected} row was affected!");
                    Console.WriteLine($"Villain {villainName} was added to the database.");
                }
            }
        }

        public static void CheckAndAddTown(string minionTown)
        {
            SqlConnection connection =
                new SqlConnection("Server=DESKTOP-QTOP4NO;Database=MinionsDB;Integrated Security=true");
            connection.Open();

            using (connection)
            {
                string checkTownQuery = @"SELECT Name FROM Towns WHERE Name = @townName";
                SqlCommand checkTownCommand = new SqlCommand(checkTownQuery, connection);
                checkTownCommand.Parameters.AddWithValue("@townName", minionTown);
                string townResult = (string)checkTownCommand.ExecuteScalar();

                if (townResult == null)
                {
                    Console.Write("Please, enter country: ");
                    string country = Console.ReadLine();
                    string insertTownQuery = @"INSERT INTO Towns VALUES (@townName, @country)";
                    SqlCommand insertTownCommand = new SqlCommand(insertTownQuery, connection);
                    SqlParameter paramTown = new SqlParameter("@townName", minionTown);
                    SqlParameter paramCounty = new SqlParameter("@country", country);
                    insertTownCommand.Parameters.Add(paramTown);
                    insertTownCommand.Parameters.Add(paramCounty);
                    int affected = insertTownCommand.ExecuteNonQuery();

                    Console.WriteLine($"{affected} row was affected!");
                    Console.WriteLine($"Town {minionTown} was added to the database.");
                }
            }

        }

        public static void AddMinions(string minionName, int minionAge, int townIndex)
        {
            SqlConnection connection =
                new SqlConnection("Server=DESKTOP-QTOP4NO;Database=MinionsDB;Integrated Security=true");
            connection.Open();

            using (connection)
            {
                string insertMinionQuery = 
                    @"INSERT INTO Minions VALUES (@minionName, @minionAge, @townId)";
                SqlCommand checkMinionCommand = new SqlCommand(insertMinionQuery, connection);
                SqlParameter paramName = new SqlParameter("@minionName", minionName);
                SqlParameter paramAge = new SqlParameter("@minionAge", minionAge);
                SqlParameter paramTownId = new SqlParameter("@townId", townIndex);
                checkMinionCommand.Parameters.Add(paramName);
                checkMinionCommand.Parameters.Add(paramAge);
                checkMinionCommand.Parameters.Add(paramTownId);
                int affected = checkMinionCommand.ExecuteNonQuery();

                Console.WriteLine($"{affected} row was added to table Minions!");
            }
        }
    }
}
