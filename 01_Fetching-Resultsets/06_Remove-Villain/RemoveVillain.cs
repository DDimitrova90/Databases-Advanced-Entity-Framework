namespace _06_Remove_Villain
{
    using System;
    using System.Data.SqlClient;
  
    public class RemoveVillain
    {
        public static void Main()
        {
            SqlConnection connection = 
                new SqlConnection("Server=DESKTOP-QTOP4NO;Database=MinionsDB;Integrated Security=true");

            connection.Open();

            int villainId = int.Parse(Console.ReadLine());

            using (connection)
            {
                string deleteServantQuery = @"DELETE FROM MinionsVillains WHERE VillainId = @VillainId";
                SqlCommand deleteServantCommand = new SqlCommand(deleteServantQuery, connection);
                deleteServantCommand.Parameters.AddWithValue("@VillainId", villainId);
                int affected = deleteServantCommand.ExecuteNonQuery();

                string checkVillainQuery = @"SELECT Name FROM Villains WHERE Id = @VillainId";
                SqlCommand checkVillainCommand = new SqlCommand(checkVillainQuery, connection);
                checkVillainCommand.Parameters.AddWithValue("@VillainId", villainId);
                var villainName = checkVillainCommand.ExecuteScalar();

                if (villainName == null)
                {
                    Console.WriteLine("No such villain was found");
                    return;
                }

                string deleteVillainQuery = @"DELETE FROM Villains WHERE Id = @VillainId";
                SqlCommand deleteVillainCommand = new SqlCommand(deleteVillainQuery, connection);
                deleteVillainCommand.Parameters.AddWithValue("@VillainId", villainId);
                deleteVillainCommand.ExecuteNonQuery();

                Console.WriteLine($"{villainName} was deleted");
                Console.WriteLine($"{affected} minions released");
            }
        }
    }
}
