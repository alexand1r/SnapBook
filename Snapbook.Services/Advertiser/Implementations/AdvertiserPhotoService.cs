namespace Snapbook.Services.Advertiser.Implementations
{
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class AdvertiserPhotoService : IAdvertiserPhotoService
    {
        private readonly SnapbookDbContext db;

        public AdvertiserPhotoService(SnapbookDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> Create(
            string description,
            string imageUrl,
            string location,
            string latitude,
            string longitude,
            string tags,
            int adId)
        {
            var ad = await this.db
                .Ads
                .FirstOrDefaultAsync(a => a.Id == adId);

            if (ad == null)
            {
                return false;
            }

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

            return true;
        }
    }
}
