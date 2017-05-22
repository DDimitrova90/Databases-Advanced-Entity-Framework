namespace _04_Football_Betting_Database.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public enum Type
    {
        local,
        national,
        international
    }

    public class CompetitionType
    {
        public CompetitionType()
        {
            this.Competitions = new HashSet<Competition>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Competition> Competitions { get; set; }
    }
}
