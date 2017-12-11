namespace Snapbook.Services.Implementations
{
    using Advertiser.Models;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models.Ad;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AdService : IAdService
    {
        private readonly SnapbookDbContext db;

        public AdService(SnapbookDbContext db)
        {
            this.db = db;
        }

        public async Task<AdDetailsServiceModel> Find(string userId)
            => await this.db
                .Ads
                .Where(a => a.UserId == userId)
                .ProjectTo<AdDetailsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<AdDetailsServiceModel> FindById(int id)
            => await this.db
                .Ads
                .Where(a => a.Id == id)
                .ProjectTo<AdDetailsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<AdListingServiceModel>> FindForListing(string searchText)
        {
            if (searchText == null)
            {
                return new List<AdListingServiceModel>();
            }

            var ads = await this.db
                .Ads
                .OrderByDescending(p => p.Id)
                .Where(a => a.Name.ToLower().Contains(searchText.ToLower())
                    || a.Description.ToLower().Contains(searchText.ToLower())
                    || a.Website.ToLower().Contains(searchText.ToLower()))
                .ProjectTo<AdListingServiceModel>()
                .ToListAsync();

            return ads;
        }
    }
}
