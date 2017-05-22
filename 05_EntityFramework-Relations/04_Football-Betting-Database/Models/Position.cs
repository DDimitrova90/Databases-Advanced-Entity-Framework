namespace _04_Football_Betting_Database.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Position
    {
        public Position()
        {
            this.Players = new HashSet<Player>();
        }

        [Key]
        [StringLength(2)]
        public string Id { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}
