namespace Snapbook.Services.User
{
    using Models.Category;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserCategoryService
    {
        Task<IEnumerable<CategoryServiceModel>> AllAsync();
    }
}
