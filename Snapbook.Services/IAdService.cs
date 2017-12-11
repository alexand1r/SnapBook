namespace Snapbook.Services
{
    using Advertiser.Models;
    using Models.Ad;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdService
    {
        Task<AdDetailsServiceModel> Find(string userId);

        Task<AdDetailsServiceModel> FindById(int id);

        Task<IEnumerable<AdListingServiceModel>> FindForListing(string searchText);
    }
}
