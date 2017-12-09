namespace Snapbook.Services.Models.Tag
{
    using Common.Mapping;
    using Data.Models;

    public class TagListingServiceModel : IMapFrom<Tag>
    {
        public string Content { get; set; }
    }
}
