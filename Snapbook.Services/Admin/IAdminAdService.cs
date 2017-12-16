namespace Snapbook.Services.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Snapbook.Services.Admin.Models.Ad;

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
