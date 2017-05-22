namespace PhotoShare.Service
{
    using Data;
    using Models;
    using System.Linq;

    public class TagService
    {
        public bool IsTagExisting(string tagName)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.Tags.Any(t => t.Name == tagName);
            }
        }

        public void AddTag(string tagName)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                Tag tag = new Tag();
                tag.Name = tagName;
                context.Tags.Add(tag);
                context.SaveChanges();
            }
        }
    }
}
