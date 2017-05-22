namespace TeamBuilder.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Validation;

    public class User
    {
        public User()
        {
            this.ParticipateTeams = new HashSet<Team>();
            this.CreatedTeams = new HashSet<Team>();
            this.CreatedEvents = new HashSet<Event>();
            this.Invitations = new HashSet<Invitation>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 3)]
        [Index(IsUnique = true)]
        public string Username { get; set; }

        [Required]
        [Password(6, 30, ContainsLowercase = true, ContainsDigit = true, ContainsUppercase = true, 
            ErrorMessage = "Invalid password!")]
        public string Password { get; set; }

        [MaxLength(25)]
        public string FirstName { get; set; }

        [MaxLength(25)]
        public string LastName { get; set; }

        public int? Age { get; set; }

        public Gender? Gender { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Team> ParticipateTeams { get; set; }

        public virtual ICollection<Team> CreatedTeams { get; set; }

        public virtual ICollection<Event> CreatedEvents { get; set; }

        public virtual ICollection<Invitation> Invitations { get; set; }
    }
}
