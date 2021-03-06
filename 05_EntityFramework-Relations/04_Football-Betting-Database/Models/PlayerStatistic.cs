﻿namespace _04_Football_Betting_Database.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class PlayerStatistic
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Game")]
        public int GameId { get; set; }

        public virtual Game Game { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Player")]
        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }

        public int ScoredGoals { get; set; }

        public int PlayerAssists { get; set; }

        public double PlayedMinutes { get; set; }
    }
}
