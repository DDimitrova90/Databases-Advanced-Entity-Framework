namespace _04_Football_Betting_Database.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public enum Prediction
    {
        [Description("Home Team Win")] HomeTeamWin,
        [Description("Draw Game")] DrawGame,
        [Description("Away Team Win")] AwayTeamWin
    }

    public class ResultPrediction
    {
        public ResultPrediction()
        {
            this.BetGames = new HashSet<BetGame>();
        }

        [Key]
        public int Id { get; set; }

        public string Prediction { get; set; }

        public virtual ICollection<BetGame> BetGames { get; set; }
    }
}
