namespace Snapbook.Services.User.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using Data;
    using Data.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserPhotoService : IUserPhotoService
    {
        private readonly SnapbookDbContext db;

        public UserPhotoService(SnapbookDbContext db)
        {
            this.db = db;
        }

        public void Create(
            string description, 
            string imageUrl, 
            string location, 
            string latitude, 
            string longitude, 
            string tags,
            int albumId)
        {
            var photo = new Photo
            {
                Description = description,
                PublishDate = DateTime.UtcNow,
                ImageUrl = imageUrl,
                Location = location,
                Latitude = latitude,
                Longitude = longitude,
                AlbumId = albumId
            };

            this.db.Add(photo);
            this.db.SaveChanges();

            var tagsList = tags.Trim().Split(new []{ ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var t in tagsList)
            {
                var tag = new Tag
                {
                    Content = t,
                    PhotoId = photo.Id
                };
                this.db.Add(tag);
            }

            this.db.SaveChanges();
        }
    }
}
