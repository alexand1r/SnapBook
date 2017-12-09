namespace Snapbook.Services.User
{
    public interface IUserAlbumService
    {
        void Create(
            string title,
            string description,
            int categoryId,
            string userId);
    }
}
