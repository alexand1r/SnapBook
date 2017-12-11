namespace Snapbook.Services.Advertiser
{
    using System.Threading.Tasks;

    public interface IAdvertiserPhotoService
    {
       void Create(
            string description,
            string imageUrl,
            string location,
            string latitude,
            string longitude,
            string tags,
            int adId);
    }
}
