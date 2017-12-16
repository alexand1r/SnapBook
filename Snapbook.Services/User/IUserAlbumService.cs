namespace Snapbook.Services.User
{
    using Models.Album;
    using System.Threading.Tasks;

    public interface IUserAlbumService
    {
        Task<EditAlbumServiceModel> Find(int id);

        Task<bool> Edit(
            string title,
            string description,
            int categoryId,
            int albumId);

        void Create(
            string title,
            string description,
            int categoryId,
            string userId);
    }
}
