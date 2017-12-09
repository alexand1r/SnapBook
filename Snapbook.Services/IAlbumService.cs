namespace Snapbook.Services
{
    using Models.Album;
    using System.Threading.Tasks;

    public interface IAlbumService
    {
        Task<AlbumDetailsServiceModel> Details(int id);
    }
}
