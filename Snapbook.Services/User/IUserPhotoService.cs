namespace Snapbook.Services.User
{
    using System.Threading.Tasks;

    public interface IUserPhotoService
    {
        void Create(
            string description,
            string imageUrl,
            string location,
            string latitude,
            string longitude,
            string tags,
            int albumId);
    }
}
