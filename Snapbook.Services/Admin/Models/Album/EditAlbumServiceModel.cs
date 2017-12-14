namespace Snapbook.Services.Admin.Models.Album
{
    using Snapbook.Common.Mapping;
    using Snapbook.Data.Models;

    public class EditAlbumServiceModel : IMapFrom<Album>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string CategoryId { get; set; }

        public string UserId { get; set; }
    }
}
