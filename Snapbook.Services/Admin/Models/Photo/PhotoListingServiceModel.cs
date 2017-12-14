namespace Snapbook.Services.Admin.Models.Photo
{
    using Common.Mapping;
    using Data.Models;

    public class PhotoListingServiceModel : IMapFrom<Photo>
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }
    }
}
