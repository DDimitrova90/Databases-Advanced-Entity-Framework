namespace _07_Gringotts_Database
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class WizardDeposit
    {
        [Key]
        [Range(1, 32767)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(60)]
        public string LastName { get; set; }

        [StringLength(1000)]
        public string Notes { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Age must be non negative number!")]
        public int? Age { get; set; }

        [StringLength(100)]
        public string MagicWandCreator { get; set; }

        [Range(1, 32767)]
        public int? MagicWandSize { get; set; }

        [StringLength(20)]
        public string DepositGroup { get; set; }

        public DateTime? DepositStartDate { get; set; }

        public decimal? DepositAmount { get; set; }

        public decimal? DepositInterest { get; set; }

        public decimal? DepositCharge { get; set; }

        public DateTime? DepositExpirationDate { get; set; }

        public bool? IsDepositExpired { get; set; }
    }
}
