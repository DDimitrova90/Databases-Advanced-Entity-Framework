namespace PhotoShare.Client.Core.Commands
{
    using Service;
    using System;
    using Utilities;

    public class AddTagCommand
    {
        private readonly TagService tagService;

        public AddTagCommand(TagService tagService)
        {
            this.tagService = tagService;
        }

        // AddTag <tag>
        public string Execute(string[] data)
        {
            string tagName = TagUtilities.ValidateOrTransform(data[0]);

            if (this.tagService.IsTagExisting(tagName))
            {
                throw new ArgumentException($"Tag {tagName} exists!");
            }

            this.tagService.AddTag(tagName);

            return $"Tag {tagName} was added successfully!";

            //using (PhotoShareContext context = new PhotoShareContext())
            //{
            //    context.Tags.Add(new Tag
            //    {
            //        Name = tag
            //    });

            //    context.SaveChanges();
            //}
        }
    }
}
