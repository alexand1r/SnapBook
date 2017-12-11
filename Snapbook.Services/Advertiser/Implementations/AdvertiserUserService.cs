namespace Snapbook.Services.Advertiser.Implementations
{
    using Data.Models;
    using Snapbook.Data;

    public class AdvertiserUserService : IAdvertiserUserService
    {
        private readonly SnapbookDbContext db;

        public AdvertiserUserService(SnapbookDbContext db)
        {
            this.db = db;
        }

        public void CreateAd(
            string name, 
            string description, 
            string imageUrl, 
            string website,
            string userId)
        {
            var ad = new Ad
            {
                Name = name,
                AdProfilePicUrl = imageUrl,
                Description = description,
                Website = website,
                UserId = userId
            };

            this.db.Add(ad);
            this.db.SaveChanges();
        }
    }
}
