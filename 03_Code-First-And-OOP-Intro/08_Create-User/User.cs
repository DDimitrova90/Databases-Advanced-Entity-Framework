namespace _08_Create_User
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 4)]
        public string Username { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6)]
        [RegularExpression(@"[a-zA-Z\d!@#$%^&*,_+<>?]+")]
        public string Password { get; set; }

        [Required]
        [RegularExpression(@"([A-Za-z0-9][A-Za-z0-9.-_]+[A-Za-z0-9])@([A-Za-z]+.)([A-Za-z]+.)*")]
        public string Email { get; set; }

        [Range(0, 1000000)]
        public byte ProfilePicture { get; set; }

        public DateTime RegisteredOn { get; set; }

        public DateTime LastTimeLoggedIn { get; set; }

        [Range(1, 120)]
        public int Age { get; set; }

        public bool IsDeleted { get; set; }
    }
}
