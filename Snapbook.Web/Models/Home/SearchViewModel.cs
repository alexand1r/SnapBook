namespace Snapbook.Web.Models.Home
{
    public class SearchViewModel
    {
        public string SearchText { get; set; }
        
        public bool SearchInUsers { get; set; } = true;
    
        public bool SearchInAlbums { get; set; } = true;

        public bool SearchInPhotos { get; set; } = true;

        public bool SearchInAds { get; set; } = true;
        
        public SearchResultsViewModel Results { get; set; }
    }
}
