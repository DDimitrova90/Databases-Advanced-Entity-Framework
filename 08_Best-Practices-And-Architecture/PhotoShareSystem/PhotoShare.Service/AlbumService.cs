namespace PhotoShare.Service
{
    using Data;
    using Models;
    using System.Linq;

    public class AlbumService
    {
        public void AddAlbum(string albumTitle, string ownerUsername, Color color, string[] tags)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                Album album = new Album();
                album.Name = albumTitle;
                album.BackgroundColor = color;
                album.Tags = context.Tags.Where(t => tags.Contains(t.Name)).ToList();

                User owner = context.Users.SingleOrDefault(u => u.Username == ownerUsername);
                
                if (owner != null)
                {
                    AlbumRole albumRole = new AlbumRole();
                    albumRole.User = owner;
                    albumRole.Album = album;
                    albumRole.Role = Role.Owner;

                    album.AlbumRoles.Add(albumRole);
                    context.Albums.Add(album);
                    context.SaveChanges();
                }
            }
        }

        public void AddTagTo(string albumName, string tagName)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                Album album = context.Albums.SingleOrDefault(a => a.Name == albumName);
                Tag tag = context.Tags.SingleOrDefault(t => t.Name == tagName);

                album.Tags.Add(tag);
                context.SaveChanges();
            }
        }

        public bool IsAlbumExisting(string albumTitle)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.Albums.Any(a => a.Name == albumTitle);
            }
        }

        public Album GetById(int id)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.Albums.Find(id);
            }
        }

        public bool IsUserOwnerOfAlbum(string username, string albumName)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                Album album = context.Albums
                    .Include("AlbumRoles")
                    .Include("AlbumRoles.User")
                    .SingleOrDefault(a => a.Name == albumName);

                if (album == null)
                {
                    return false;
                }

                return album.AlbumRoles.Any(ar => ar.User.Username == username && ar.Role == Role.Owner);
            }
        }

        public void ShareAlbum(string username, int albumId, Role role)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                User userToAdd = context.Users.SingleOrDefault(u => u.Username == username);
                Album album = context.Albums.Find(albumId);

                if (userToAdd != null && album != null)
                {
                    AlbumRole albumRole = new AlbumRole();
                    albumRole.User = userToAdd;
                    albumRole.Album = album;
                    albumRole.Role = role;

                    context.AlbumRoles.Add(albumRole);
                    context.SaveChanges();
                }
            }
        }
    }
}
