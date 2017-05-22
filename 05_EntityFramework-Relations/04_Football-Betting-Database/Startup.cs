namespace _04_Football_Betting_Database
{
    public class Startup
    {
        public static void Main()
        {
            FootballBookmakerSystemContext context = new FootballBookmakerSystemContext();

            context.Database.Initialize(true);
        }
    }
}
