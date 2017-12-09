namespace Snapbook.Services.User.Implementations
{
    using Data;
    using Data.Models;
    using System;

    public class UserAlbumService : IUserAlbumService
    {
        private readonly SnapbookDbContext db;

        public UserAlbumService(SnapbookDbContext db)
        {
            this.db = db;
        }

        public void Create(
            string title, 
            string description, 
            int categoryId,
            string userId)
        {
            var album = new Album
            {
                Title = title,
                Description = description,
                CategoryId = categoryId,
                PublishDate = DateTime.UtcNow,
                UserId = userId
            };

            this.db.Add(album);
            this.db.SaveChanges();
        }
    }
}
