namespace Snapbook.Web.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Models.Home;
    using Models.Photos;
    using Services;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly IPhotoService photos;
        private readonly IAlbumService albums;
        private readonly IUserService users;
        private readonly IAdService ads;
        private readonly UserManager<User> userManager;

        public HomeController(
            IPhotoService photos,
            IAlbumService albums,
            IUserService users,
            IAdService ads,
            UserManager<User> userManager)
        {
            this.photos = photos;
            this.albums = albums;
            this.users = users;
            this.ads = ads;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var pics = await this.photos.All();
            var user = await this.userManager.GetUserAsync(this.User);

            return this.View(new PhotoHomeListingViewModel
            {
                Photos = pics,
                User = user
            });
        }

        public IActionResult Search(SearchViewModel model)
        {
            return this.View(new SearchViewModel
            {
                Current = "users",
                Results = new SearchResultsViewModel
                {
                    Current = "users"
                }
            });
        }

        public async Task<IActionResult> SearchResults(SearchViewModel model)
        {
            var current = model.Current;

            var results = new SearchResultsViewModel();

            results.Current = current;

            if (model.SearchInAlbums)
            {
                results.Albums = await this.albums.Find(model.SearchText);
            }

            if (model.SearchInUsers)
            {
                results.Users = await this.users.Find(model.SearchText);
            }

            if (model.SearchInPhotos)
            {
                results.Photos = await this.photos.Find(model.SearchText);
            }

            if (model.SearchInAds)
            {
                results.Ads = await this.ads.FindForListing(model.SearchText);
            }

            return this.PartialView("_SearchResults", results);
        }

        public IActionResult NotFoundPage() => this.View();

        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
