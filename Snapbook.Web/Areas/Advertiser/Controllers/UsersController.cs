namespace Snapbook.Web.Areas.Advertiser.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Users;
    using Services.Advertiser;
    using System.Threading.Tasks;
    using Snapbook.Web.Infrastructure.Filters;

    public class UsersController : BaseController
    {
        private readonly IAdvertiserUserService users;
        private readonly IAdvertiserAdService ads;
        private readonly UserManager<User> userManager;

        public UsersController(
            IAdvertiserUserService users,
            IAdvertiserAdService ads,
            UserManager<User> userManager)
        {
            this.users = users;
            this.ads = ads;
            this.userManager = userManager;
        }

        public IActionResult CreateAd()
            => this.View();

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> CreateAd(CreateAdViewModel model)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            this.users.CreateAd(
                model.Name,
                model.Description,
                model.AdProfilePicUrl,
                model.Website,
                user.Id);

            return this.RedirectToAction("Ad", "Users");
        }
    }
}
