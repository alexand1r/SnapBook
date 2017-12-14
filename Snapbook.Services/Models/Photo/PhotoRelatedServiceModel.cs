namespace Snapbook.Services.Models.Photo
{
    using Common.Mapping;
    using Data.Models;

    public class PhotoRelatedServiceModel : IMapFrom<Photo>
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }
    }
}
