namespace Snapbook.Services.Advertiser.Implementations
{
    using System;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;
    using Snapbook.Data.Models;

    public class AdvertiserPhotoService : IAdvertiserPhotoService
    {
        private readonly SnapbookDbContext db;

        public AdvertiserPhotoService(SnapbookDbContext db)
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
            int adId)
        {
            var photo = new Photo
            {
                Description = description,
                PublishDate = DateTime.UtcNow,
                ImageUrl = imageUrl,
                Location = location,
                Latitude = latitude,
                Longitude = longitude,
                AdId = adId
            };

            this.db.Add(photo);
            this.db.SaveChanges();

            var tagsList = tags.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
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
