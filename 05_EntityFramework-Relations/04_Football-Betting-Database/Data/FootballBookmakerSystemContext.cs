namespace _04_Football_Betting_Database
{
    using Models;
    using System.Data.Entity;

    public class FootballBookmakerSystemContext : DbContext
    {
        public FootballBookmakerSystemContext()
            : base("name=FootballBookmakerSystemContext")
        {
        }

        public virtual DbSet<Bet> Bets { get; set; }

        public virtual DbSet<BetGame> BetGames { get; set; }

        public virtual DbSet<Color> Colors { get; set; }

        public virtual DbSet<Competition> Competitions { get; set; }

        public virtual DbSet<CompetitionType> CompetitionTypes { get; set; }

        public virtual DbSet<Continent> Continents { get; set; }

        public virtual DbSet<Country> Countries { get; set; }

        public virtual DbSet<Game> Games { get; set; }

        public virtual DbSet<Player> Players { get; set; }

        public virtual DbSet<PlayerStatistic> PlayerStatistics { get; set; }

        public virtual DbSet<Position> Positions { get; set; }

        public virtual DbSet<ResultPrediction> ResultPredictions { get; set; }

        public virtual DbSet<Round> Rounds { get; set; }

        public virtual DbSet<Team> Teams { get; set; }

        public virtual DbSet<Town> Towns { get; set; }

        public virtual DbSet<User> Users { get; set; }
    }
}