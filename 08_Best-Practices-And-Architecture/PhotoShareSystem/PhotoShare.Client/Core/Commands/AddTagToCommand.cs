namespace PhotoShare.Client.Core.Commands
{
    using Service;
    using System;
    using Utilities;

    public class AddTagToCommand 
    {
        private readonly TagService tagService;
        private readonly AlbumService albumService;

        public AddTagToCommand(TagService tagService, AlbumService albumService)
        {
            this.tagService = tagService;
            this.albumService = albumService;
        }

        // AddTagTo <albumName> <tag>
        public string Execute(string[] data)
        {
            string albumName = data[0];
            string tagName = TagUtilities.ValidateOrTransform(data[1]);

            if (!this.albumService.IsAlbumExisting(albumName) || 
                !this.tagService.IsTagExisting(tagName))
            {
                throw new ArgumentException("Either tag or album do not exist!");
            }

            this.albumService.AddTagTo(albumName, tagName);

            return $"Tag {tagName} added to {albumName}!";
        }
    }
}
