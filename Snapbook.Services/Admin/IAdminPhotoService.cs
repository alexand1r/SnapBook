namespace Snapbook.Services.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Snapbook.Services.Admin.Models.Photo;

    public interface IAdminPhotoService
    {
        void Delete(int id);

        Task<IEnumerable<PhotoListingServiceModel>> All();

        Task<bool> Exists(int id);
    }
}
