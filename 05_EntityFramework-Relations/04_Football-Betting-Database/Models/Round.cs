﻿namespace _04_Football_Betting_Database.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Round
    {
        public Round()
        {
            this.Games = new HashSet<Game>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
