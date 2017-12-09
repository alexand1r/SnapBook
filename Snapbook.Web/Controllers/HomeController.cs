namespace Snapbook.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Snapbook.Data.Models;
    using Snapbook.Web.Models.Photos;

    public class HomeController : Controller
    {
        private readonly IPhotoService photos;
        private readonly UserManager<User> userManager;

        public HomeController(
            IPhotoService photos,
            UserManager<User> userManager)
        {
            this.photos = photos;
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
      
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
