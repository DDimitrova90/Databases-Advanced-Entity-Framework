namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using Service;
    using System;
    using System.Linq;
    using Utilities;

    public class CreateAlbumCommand
    {
        private readonly UserService userService;
        private readonly TagService tagService;
        private readonly AlbumService albumService;

        public CreateAlbumCommand(UserService userService, TagService tagService, AlbumService albumService)
        {
            this.userService = userService;
            this.tagService = tagService;
            this.albumService = albumService;
        }

        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public string Execute(string[] data)
        {
            string username = data[0];
            string albumTitle = data[1];
            string backgroundColor = data[2];
            string[] tags = data.Skip(3).Select(t => TagUtilities.ValidateOrTransform(t)).ToArray();

            if (!this.userService.IsExistingByUsername(username))
            {
                throw new ArgumentException($"User {username} not found!");
            }

            Color color;
            bool isColorValid = Enum.TryParse(backgroundColor, out color);

            if (!isColorValid)
            {
                throw new ArgumentException($"Color {color} not found!");
            }

            if (tags.Any(t => !this.tagService.IsTagExisting(t)))
            {
                throw new ArgumentException("Invalid tags!");
            }

            // TODO: check album title
            if (this.albumService.IsAlbumExisting(albumTitle))
            {
                throw new ArgumentException($"Album {albumTitle} exists!");
            }

            // TODO: add to database
            this.albumService.AddAlbum(albumTitle, username, color, tags);

            return $"Album {albumTitle} successfully created!";
        }
    }
}
