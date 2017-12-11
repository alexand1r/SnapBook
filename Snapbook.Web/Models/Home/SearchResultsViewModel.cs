namespace Snapbook.Web.Models.Home
{
    using Services.Models.Ad;
    using Services.Models.Album;
    using Services.Models.Photo;
    using Services.Models.User;
    using System.Collections.Generic;

    public class SearchResultsViewModel
    {
        public IEnumerable<UserListingServiceModel> Users { get; set; } = new List<UserListingServiceModel>();

        public IEnumerable<AlbumListingServiceModel> Albums { get; set; } = new List<AlbumListingServiceModel>();

        public IEnumerable<PhotoListingServiceModel> Photos { get; set; } = new List<PhotoListingServiceModel>();

        public IEnumerable<AdListingServiceModel> Ads { get; set; } = new List<AdListingServiceModel>();
    }
}
