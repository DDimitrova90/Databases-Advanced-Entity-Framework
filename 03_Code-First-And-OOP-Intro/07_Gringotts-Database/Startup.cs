namespace _07_Gringotts_Database
{
    using System;

    public class Startup
    {
        public static void Main()
        {
            WizardDepositsContext context = new WizardDepositsContext();

            //context.Database.Initialize(true);  //to create the database

            context.WizardDeposits.Add(
                new WizardDeposit()
                {
                    FirstName = "Hannah",
                    LastName = "Abbott",
                    Notes = @"Hufflepuff student in Harry's year and member of Dumbledore's Army.
                            Hufflepuff student in Harry's year and member of Dumbledore's Army.",
                    Age = 71,
                    MagicWandCreator = "Antioch Peverell",
                    MagicWandSize = 19,
                    DepositGroup = "Troll Chest",
                    DepositStartDate = new DateTime(1990, 09, 27),
                    DepositAmount = 47443.03M,
                    DepositInterest = 29.28M,
                    DepositCharge = 73.00M,
                    DepositExpirationDate = new DateTime(1991, 04, 08),
                    IsDepositExpired = false
                });

            context.WizardDeposits.Add(
                new WizardDeposit()
                {
                    FirstName = "Bathsheda",
                    LastName = "Babbling",
                    Notes = @"Ancient Runes teacher at Hogwarts",
                    Age = 63,
                    MagicWandCreator = "Ollivander family",
                    MagicWandSize = 20,
                    DepositGroup = "Human Pride",
                    DepositStartDate = new DateTime(1986, 03, 27),
                    DepositAmount = 46597.40M,
                    DepositInterest = 26.64M,
                    DepositCharge = 64.00M,
                    DepositExpirationDate = new DateTime(1986, 05, 16),
                    IsDepositExpired = true
                });

            context.WizardDeposits.Add(
                new WizardDeposit()
                {
                    FirstName = "Ludo",
                    LastName = "Bagman",
                    Notes = @"Quidditch Beater for the Wimbourne Wasps and Head of the Department of Magical Games and Sports within the Ministry of Magic",
                    Age = 19,
                    MagicWandCreator = "Mykew Gregorovitch",
                    MagicWandSize = 18,
                    DepositGroup = "Venomous Tongue",
                    DepositStartDate = new DateTime(1990, 07, 04),
                    DepositAmount = 23190.94M,
                    DepositInterest = 14.92M,
                    DepositCharge = 30.00M,
                    DepositExpirationDate = new DateTime(1990, 12, 27),
                    IsDepositExpired = false
                });

            context.WizardDeposits.Add(
                new WizardDeposit()
                {
                    FirstName = "Bathilda",
                    LastName = "Bagshot",
                    Notes = @"Author of A History of Magic, great aunt of Gellert Grindelwald.",
                    Age = 52,
                    MagicWandCreator = "Jimmy Kiddell",
                    MagicWandSize = 15,
                    DepositGroup = "Blue Phoenix",
                    DepositStartDate = new DateTime(1993, 06, 07),
                    DepositAmount = 687.67M,
                    DepositInterest = 18.18M,
                    DepositCharge = 62.00M,
                    DepositExpirationDate = new DateTime(1993, 09, 10),
                    IsDepositExpired = false
                });

            WizardDeposit wiz = new WizardDeposit();
            wiz.Id = 1;
            wiz.FirstName = "ihk";
            wiz.LastName = "brbrbrb";
            wiz.Age = 13;
            wiz.MagicWandSize = 19;
            context.WizardDeposits.Add(wiz);

            context.SaveChanges();

            

            //try
            //{
            //    context.SaveChanges();
            //}
            //catch (DbEntityValidationException ex)
            //{
            //    StringBuilder sb = new StringBuilder();

            //    foreach (var failure in ex.EntityValidationErrors)
            //    {
            //        sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
            //        foreach (var error in failure.ValidationErrors)
            //        {
            //            sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
            //            sb.AppendLine();
            //        }
            //    }

            //    throw new DbEntityValidationException(
            //    "Entity Validation Failed - errors follow:\n" +
            //    sb.ToString(), ex
            //    );
            //}
        }
    }
}
