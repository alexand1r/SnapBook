namespace Snapbook.Services.Models.Ad
{
    using Common.Mapping;
    using Data.Models;

    public class AdListingServiceModel : IMapFrom<Ad>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string AdProfilePicUrl { get; set; }

        public string Website { get; set; }
    }
}
