namespace _04_Football_Betting_Database.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Player
    {
        public Player()
        {
            this.PlayerStatistics = new HashSet<PlayerStatistic>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string SquadNumber { get; set; }

        public int TeamId { get; set; }

        public virtual Team Team { get; set; }

        public int PositionId { get; set; }

        public virtual Position Position { get; set; }

        public bool IsInjured { get; set; }

        public virtual ICollection<PlayerStatistic> PlayerStatistics { get; set; }
    }
}
