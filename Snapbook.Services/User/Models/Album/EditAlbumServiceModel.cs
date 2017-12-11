namespace Snapbook.Services.User.Models.Album
{
    using Common.Mapping;
    using Data.Models;

    public class EditAlbumServiceModel : IMapFrom<Album>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string CategoryId { get; set; }

        public string UserId { get; set; }
    }
}
