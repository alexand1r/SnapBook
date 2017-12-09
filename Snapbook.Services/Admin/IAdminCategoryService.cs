namespace Snapbook.Services.Admin
{
    using System.Threading.Tasks;

    public interface IAdminCategoryService
    {
        void Create(string name);

        Task<bool> ExistsAsync(string name);
    }
}
