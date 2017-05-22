namespace _08_Create_User
{
    using System;

    public class CreateUser
    {
        public static void Main()
        {
            UserContext context = new UserContext();

            //context.Database.Initialize(true);   // to create database

            context.Users.Add(
                new User()
                {
                    Username = "Stewart",
                    Password = "YZK93CHX2QX",
                    Email = "Nam@acnulla.co.uk",
                    RegisteredOn = new DateTime(2001, 11, 19),
                    LastTimeLoggedIn = new DateTime(2015, 02, 12),
                    Age = 18,
                    IsDeleted = false
                });

            context.Users.Add(
              new User()
              {
                  Username = "Peter",
                  Password = "XXW84BXO4ND",
                  Email = "Sed.malesuada.augue@necimperdietnec.com",
                  RegisteredOn = new DateTime(2008, 08, 26),
                  LastTimeLoggedIn = new DateTime(2014, 02, 17),
                  Age = 86,
                  IsDeleted = false
              });

            context.Users.Add(
              new User()
              {
                  Username = "Zenaida",
                  Password = "FPY71FKW7VM",
                  Email = "Ut.tincidunt@lectuspede.ca",
                  RegisteredOn = new DateTime(2007, 05, 18),
                  LastTimeLoggedIn = new DateTime(2017, 03, 12),
                  Age = 25,
                  IsDeleted = false
              });

            context.Users.Add(
              new User()
              {
                  Username = "Zorita",
                  Password = "SEQ59CWR8HT",
                  Email = "orci.lacus@sociosqu.com",
                  RegisteredOn = new DateTime(2002, 09, 03),
                  LastTimeLoggedIn = new DateTime(2014, 05, 11),
                  Age = 20,
                  IsDeleted = false
              });

            context.Users.Add(
              new User()
              {
                  Username = "Micah",
                  Password = "REM96DPR5JQ",
                  Email = "Sed@turpisegestas.com",
                  RegisteredOn = new DateTime(2009, 11, 03),
                  LastTimeLoggedIn = new DateTime(2017, 09, 10),
                  Age = 68,
                  IsDeleted = false
              });

            context.SaveChanges();
        }
    }
}
