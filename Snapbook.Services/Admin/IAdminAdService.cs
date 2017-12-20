namespace Snapbook.Services.Admin
{
    using Models.Ad;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminAdService
    {
        Task<bool> Delete(int id);

        Task<IEnumerable<AdListingServiceModel>> All();

        Task<bool> Exists(int id);

        Task<bool> Edit(
            string name,
            string description,
            string imageUrl,
            string website,
            int id);

        Task<AdEditServiceModel> FindForEdit(int id);
    }
}
