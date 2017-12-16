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

        public async Task<bool> Delete(int id)
        {
            var ad = await this.db
                .Ads
                .FirstOrDefaultAsync(a => a.Id == id);

            if (ad == null)
            {
                return false;
            }

            this.db.Ads.Remove(ad);
            this.db.SaveChanges();

            return true;
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

        public async Task<bool> Edit(
            string name,
            string description,
            string imageUrl,
            string website,
            int id)
        {
            var ad = await this.db
                .Ads
                .FirstOrDefaultAsync(a => a.Id == id);

            if (ad == null)
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
    }
}
