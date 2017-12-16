namespace Snapbook.Services.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Snapbook.Services.Admin.Models.Photo;

    public interface IAdminPhotoService
    {
        Task<bool> Delete(int id);

        Task<IEnumerable<PhotoListingServiceModel>> All();

        Task<bool> Exists(int id);
    }
}
