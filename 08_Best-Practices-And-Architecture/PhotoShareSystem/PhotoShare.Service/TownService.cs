namespace PhotoShare.Service
{
    using Data;
    using Models;
    using System.Linq;

    public class TownService
    {
        public void AddTown(string name, string countryName)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                Town town = new Town();
                town.Name = name;
                town.Country = countryName;

                context.Towns.Add(town);
                context.SaveChanges();
            }
        }

        public bool IsTownExisting(string townName)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.Towns.Any(t => t.Name == townName);
            }
        }

        public Town GetByTownName(string townName)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.Towns.SingleOrDefault(t => t.Name == townName);
            }
        }
    }
}
