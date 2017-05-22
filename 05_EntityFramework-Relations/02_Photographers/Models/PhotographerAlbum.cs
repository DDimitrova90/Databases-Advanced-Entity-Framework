namespace _02_Photographers.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /*
     * Create class for mapping table PhotographerAlbums that I already have, and add to here new
     * column "Role" with enum values. Add it to Context. Change classes Album and Photographer to have 
     * ICollection<PhotographerAlbum>. And finally change Configuration: create some object from type
     * PhotographerAlbum and add them to each photographer. Create migration: Add-Migration AddRoles
     * and then: Update-Database
    */

    public enum Role
    {
        Owner = 0,
        Viewer = 1
    }

    public class PhotographerAlbum
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Photographer")]
        public int Photographer_Id { get; set; }

        public virtual Photographer Photographer { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Album")]
        public int Album_Id { get; set; }

        public virtual Album Album { get; set; }

        public Role Role { get; set; }
    }
}
