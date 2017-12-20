namespace Snapbook.Services.Admin
{
    using Models.Photo;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminPhotoService
    {
        Task<bool> Delete(int id);

        Task<IEnumerable<PhotoListingServiceModel>> All();

        Task<bool> Exists(int id);
    }
}
