namespace _04_Football_Betting_Database.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Team
    {
        public Team()
        {
            this.Players = new HashSet<Player>();       
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Logo { get; set; }

        [StringLength(3)]
        public string Initials { get; set; }

        public virtual Color PrimaryKitColor { get; set; }

        public virtual Color SecondaryKitColor { get; set; }

        public int TownId { get; set; }

        public virtual Town Town { get; set; }

        public decimal Budget { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}
