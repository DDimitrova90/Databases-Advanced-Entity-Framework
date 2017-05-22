namespace TeamBuilder.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Team
    {
        public Team()
        {
            this.Members = new HashSet<User>();
            this.Events = new HashSet<Event>();
            this.Invitations = new HashSet<Invitation>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [MaxLength(32)]
        public string Description { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string Acronym { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("Creator")]
        public int CreatorId { get; set; }

        public virtual User Creator { get; set; }

        public virtual ICollection<User> Members { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<Invitation> Invitations { get; set; }
    }
}
