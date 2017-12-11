namespace Snapbook.Services.Advertiser.Implementations
{
    using Advertiser.Models;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    public class AdvertiserAdService : IAdvertiserAdService
    {
        private readonly SnapbookDbContext db;

        public AdvertiserAdService(SnapbookDbContext db)
        {
            this.db = db;
        }

        public void Edit(
            string name, 
            string description, 
            string imageUrl, 
            string website, 
            string userId, 
            int id)
        {
            var ad = this.db.Ads.FirstOrDefault(a => a.Id == id);

            if (ad == null || ad.UserId != userId)
            {
                return;
            }

            ad.Name = name;
            ad.Description = description;
            ad.AdProfilePicUrl = imageUrl;
            ad.Website = website;

            this.db.SaveChanges();
        }

        public async Task<AdEditServiceModel> FindForEdit(int id)
            => await this.db
                .Ads
                .Where(a => a.Id == id)
                .ProjectTo<AdEditServiceModel>()
                .FirstOrDefaultAsync();
    }
}
