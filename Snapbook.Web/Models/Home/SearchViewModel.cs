namespace Snapbook.Web.Models.Home
{
    using System.ComponentModel.DataAnnotations;

    public class SearchViewModel
    {
        public string SearchText { get; set; }
        
        [Display(Name = "USERS")]
        public bool SearchInUsers { get; set; } = true;
    
        [Display(Name = "ALBUMS")]
        public bool SearchInAlbums { get; set; } = true;

        [Display(Name = "PHOTOS")]
        public bool SearchInPhotos { get; set; } = true;

        [Display(Name = "ADS")]
        public bool SearchInAds { get; set; } = true;
        
        public SearchResultsViewModel Results { get; set; }
    }
}
