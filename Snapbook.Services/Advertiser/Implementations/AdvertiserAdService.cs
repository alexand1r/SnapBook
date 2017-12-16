namespace Snapbook.Services.Advertiser.Implementations
{
    using Advertiser.Models;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore.Query.Internal;
    using Snapbook.Data.Models;

    public class AdvertiserAdService : IAdvertiserAdService
    {
        private readonly SnapbookDbContext db;

        public AdvertiserAdService(SnapbookDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> CreateAd(
            string name,
            string description,
            string imageUrl,
            string website,
            string userId)
        {
            var user = await this.db
                .Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return false;
            }

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

            return true;
        }

        public async Task<bool> Edit(
            string name, 
            string description, 
            string imageUrl, 
            string website, 
            string userId, 
            int id)
        {
            var ad = await this.db
                .Ads
                .FirstOrDefaultAsync(a => a.Id == id);

            if (ad == null || ad.UserId != userId)
            {
                return false;
            }

            ad.Name = name;
            ad.Description = description;
            ad.AdProfilePicUrl = imageUrl;
            ad.Website = website;

            this.db.SaveChanges();

            return true;
        }

        public async Task<AdEditServiceModel> FindForEdit(int id)
            => await this.db
                .Ads
                .Where(a => a.Id == id)
                .ProjectTo<AdEditServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<AdDetailsServiceModel> Find(string userId)
            => await this.db
                .Ads
                .Where(a => a.UserId == userId)
                .ProjectTo<AdDetailsServiceModel>()
                .FirstOrDefaultAsync();
    }
}
