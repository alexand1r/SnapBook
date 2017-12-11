namespace Snapbook.Services.User.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.Album;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserAlbumService : IUserAlbumService
    {
        private readonly SnapbookDbContext db;

        public UserAlbumService(SnapbookDbContext db)
        {
            this.db = db;
        }

        public async Task<EditAlbumServiceModel> Find(int id)
            => await this.db
                .Albums
                .Where(a => a.Id == id)
                .ProjectTo<EditAlbumServiceModel>()
                .FirstOrDefaultAsync();

        public void Edit(string title, string description, int categoryId, int albumId)
        {
            var album = this.db.Albums.FirstOrDefault(a => a.Id == albumId);

            if (album == null)
            {
                return;
            }

            album.Title = title;
            album.Description = description;
            album.CategoryId = categoryId;

            this.db.SaveChanges();
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
