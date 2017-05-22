namespace _04_Football_Betting_Database.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Game
    {
        public Game()
        {
            this.PlayerStatistics = new HashSet<PlayerStatistic>();
            this.BetGames = new HashSet<BetGame>();
        }

        [Key]
        public int Id { get; set; }

        public virtual Team HomeTeam { get; set; }

        public virtual Team AwayTeam { get; set; }

        public int HomeGoals { get; set; }

        public int AwayGoals { get; set; }

        public DateTime DateTime { get; set; }

        public decimal HomeTeamWinBetRate { get; set; }

        public decimal AwayTeamWinBetRate { get; set; }

        public decimal DrawGameBetRate { get; set; }

        public int RoundId { get; set; }

        public virtual Round Round { get; set; }

        public int CompetitionId { get; set; }

        public virtual Competition Competition { get; set; }

        public virtual ICollection<PlayerStatistic> PlayerStatistics { get; set; }

        public virtual ICollection<BetGame> BetGames { get; set; }
    }
}
