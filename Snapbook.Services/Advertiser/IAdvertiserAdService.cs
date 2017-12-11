namespace Snapbook.Services.Advertiser
{
    using Advertiser.Models;
    using System.Threading.Tasks;

    public interface IAdvertiserAdService
    {
        void Edit(
            string name,
            string description,
            string imageUrl,
            string website,
            string userId,
            int id);

        Task<AdEditServiceModel> FindForEdit(int id);
    }
}
