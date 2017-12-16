namespace Snapbook.Services.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Snapbook.Services.Admin.Models.Category;

    public interface IAdminCategoryService
    {
        void Create(string name);

        Task<bool> Edit(int id, string name);

        Task<bool> Delete(int id);

        Task<bool> ExistsAsync(string name);

        Task<CategoryListingServiceModel> Find(int id);

        Task<IEnumerable<CategoryListingServiceModel>> All();
    }
}
