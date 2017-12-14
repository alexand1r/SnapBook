namespace Snapbook.Services.Admin.Implementations
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using Snapbook.Data;
    using Snapbook.Services.Admin.Models.Ad;

    public class AdminAdService : IAdminAdService
    {
        private readonly SnapbookDbContext db;

        public AdminAdService(SnapbookDbContext db)
        {
            this.db = db;
        }

        public void Delete(int id)
        {
            var ad = this.db.Ads.FirstOrDefault(a => a.Id == id);

            if (ad == null)
            {
                return;
            }

            this.db.Ads.Remove(ad);
            this.db.SaveChanges();
        }

        public async Task<IEnumerable<AdListingServiceModel>> All()
            => await this.db
                .Ads
                .OrderByDescending(a => a.Id)
                .ProjectTo<AdListingServiceModel>()
                .ToListAsync();

        public async Task<bool> Exists(int id)
            => await this.db
                .Ads
                .AnyAsync(a => a.Id == id);

        public void Edit(
            string name,
            string description,
            string imageUrl,
            string website,
            string userId,
            int id)
        {
            var ad = this.db.Ads.FirstOrDefault(a => a.Id == id);

            if (ad == null)
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
