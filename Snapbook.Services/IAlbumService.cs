namespace Snapbook.Services
{
    using Models.Album;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAlbumService
    {
        Task<AlbumDetailsServiceModel> Details(int id);

        Task<IEnumerable<AlbumListingServiceModel>> Find(string searchText);
    }
}
