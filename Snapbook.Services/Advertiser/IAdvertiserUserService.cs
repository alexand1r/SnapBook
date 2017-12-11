namespace Snapbook.Services.Advertiser
{
    public interface IAdvertiserUserService
    {
        void CreateAd(
            string name,
            string description,
            string imageUrl,
            string website,
            string userId);
    }
}
