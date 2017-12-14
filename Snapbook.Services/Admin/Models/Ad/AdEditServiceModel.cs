namespace Snapbook.Services.Admin.Models.Ad
{
    using Snapbook.Common.Mapping;
    using Snapbook.Data.Models;

    public class AdEditServiceModel : IMapFrom<Ad>
    {
        public string Name { get; set; }

        public string AdProfilePicUrl { get; set; }

        public string Description { get; set; }

        public string Website { get; set; }
    }
}
