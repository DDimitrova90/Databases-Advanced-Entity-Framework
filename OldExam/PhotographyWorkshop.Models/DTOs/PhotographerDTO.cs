namespace PhotographyWorkshop.Models.DTOs
{
    using System.Collections.Generic;

    public class PhotographerDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public ICollection<int> Lenses { get; set; }
    }
}
