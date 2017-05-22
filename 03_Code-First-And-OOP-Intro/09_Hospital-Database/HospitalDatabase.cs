namespace _09_Hospital_Database
{
    using System;
    using Models;

    public class HospitalDatabase
    {
        public static void Main()
        {
            HospitalContext context = new HospitalContext();

            //context.Database.Initialize(true);

            context.Medicaments.Add(new Medicament() { Name = "Analgin" });
            context.Medicaments.Add(new Medicament() { Name = "Paracetamol" });
            context.Medicaments.Add(new Medicament() { Name = "Famotidin" });
            context.Medicaments.Add(new Medicament() { Name = "Piramem" });
            context.Medicaments.Add(new Medicament() { Name = "Vitamin B" });

            context.Diagnoses.Add(new Diagnose() { Name = "Grip", Comments = "Ptichi grip" });
            context.Diagnoses.Add(new Diagnose() { Name = "Flebit", Comments = "Nishto mu nqma" });
            context.Diagnoses.Add(new Diagnose() { Name = "Gastrit", Comments = "Nujna e dieta" });
            context.Diagnoses.Add(new Diagnose() { Name = "Migrena", Comments = "Na nervna pochva" });
            context.Diagnoses.Add(new Diagnose() { Name = "Obshta otpadnalost", Comments = "Mahmurluk" });

            context.Visitations.Add(new Visitation() { Date = new DateTime(2017, 03, 06), Comments = "Vsichko mina dobre" });
            context.Visitations.Add(new Visitation() { Date = new DateTime(2017, 03, 11) });
            context.Visitations.Add(new Visitation() { Date = new DateTime(2017, 02, 22), Comments = "Vsichko e ok" });
            context.Visitations.Add(new Visitation() { Date = new DateTime(2017, 03, 09) });
            context.Visitations.Add(new Visitation() { Date = new DateTime(2017, 03, 05), Comments = "Vsichko mina dobre" });

            context.Patients.Add(new Patient() { FirstName = "Genka", LastName = "Shikerova", Address = "Mladost-3", Email = "genka.shokerova@gmail.com", BirthDate = "25-12-1988", HasInsurance = true });
            context.Patients.Add(new Patient() { FirstName = "Tanio", LastName = "Tanev", Address = "Mladost-1", Email = "tanio.tanev@gmail.com", BirthDate = "13-10-1990", HasInsurance = false });
            context.Patients.Add(new Patient() { FirstName = "Penka", LastName = "Jeleva", Address = "Mladost-2", Email = "penka.jeleva@gmail.com", BirthDate = "03-05-1975", HasInsurance = true });
            context.Patients.Add(new Patient() { FirstName = "Stefka", LastName = "Stefanova", Address = "Strelbishte", Email = "stefka.stefanova@gmail.com", BirthDate = "07-02-1963", HasInsurance = false });
            context.Patients.Add(new Patient() { FirstName = "Faraon", LastName = "Faraonov", Address = "Slatina", Email = "faraon.faraonov@gmail.com", BirthDate = "03-06-1980", HasInsurance = true });

            context.Doctors.Add(new Doctor() { Name = "Hristo Gabarov", Stecialty = "Ortoped"});

            context.SaveChanges();
        }
    }
}
