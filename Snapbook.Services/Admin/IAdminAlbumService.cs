namespace Snapbook.Services.Admin
{
    using Admin.Models.Album;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminAlbumService
    {
        Task<bool> Delete(int id);

        Task<IEnumerable<AlbumListingServiceModel>> All();

        Task<bool> Exists(int id);

        Task<EditAlbumServiceModel> Find(int id);

        Task<bool> Edit(string title, string description, int categoryId, int albumId);
    }
}
