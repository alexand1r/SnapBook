namespace Snapbook.Services.Advertiser
{
    using Advertiser.Models;
    using System.Threading.Tasks;

    public interface IAdvertiserAdService
    {
        Task<bool> CreateAd(
            string name,
            string description,
            string imageUrl,
            string website,
            string userId);

        Task<bool> Edit(
            string name,
            string description,
            string imageUrl,
            string website,
            string userId,
            int id);

        Task<AdEditServiceModel> FindForEdit(int id);

        Task<AdDetailsServiceModel> Find(string userId);
    }
}
