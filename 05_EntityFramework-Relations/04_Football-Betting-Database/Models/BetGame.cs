namespace _04_Football_Betting_Database.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class BetGame
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Game")]
        public int GameId { get; set; }

        public virtual Game Game { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Bet")]
        public int BetId { get; set; }

        public virtual Bet Bet { get; set; }

        public int ResultPredictionId { get; set; }

        public virtual ResultPrediction ResultPrediction { get; set; }
    }
}
