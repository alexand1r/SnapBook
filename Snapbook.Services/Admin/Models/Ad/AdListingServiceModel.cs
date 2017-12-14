namespace Snapbook.Services.Admin.Models.Ad
{
    using Common.Mapping;
    using Data.Models;

    public class AdListingServiceModel : IMapFrom<Ad>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
