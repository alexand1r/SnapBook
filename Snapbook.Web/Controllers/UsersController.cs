namespace Snapbook.Web.Controllers
{
    using Data.Models;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Users;
    using Snapbook.Services;
    using System.Threading.Tasks;
    using PaulMiami.AspNetCore.Mvc.Recaptcha;
    using Snapbook.Web.Infrastructure.Extensions;

    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserService users;
        private readonly IAdService ads;
        private readonly INotificationService notifications;
        private readonly UserManager<User> userManager;

        public UsersController(
            IUserService users,
            IAdService ads,
            INotificationService notifications,
            UserManager<User> userManager)
        {
            this.users = users;
            this.ads = ads;
            this.notifications = notifications;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Profile(string username)
        {
            var user = await this.userManager.FindByNameAsync(username);

            if (user == null)
            {
                return this.NotFound();
            }

            var profile = await this.users.ProfileAsync(user.Id);

            return this.View(profile);
        }

        public async Task<IActionResult> ChangeProfilePic(string username)
        {
            var user = await this.userManager.FindByNameAsync(username);

            if (user == null)
            {
                return this.NotFound();
            }

            return this.View(new EditProfilePicViewModel
            {
                ImageUrl = user.ProfilePicUrl,
                Username = username
            });
        }

        [HttpPost]
        [ValidateRecaptcha]
        public async Task<IActionResult> ChangeProfilePic(string username, EditProfilePicViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var success = await this.users.EditProfilePic(username, model.ImageUrl);

            if (!success)
            {
                return this.BadRequest();
            }

            this.TempData.AddSuccessMessage($"The profile photo has been successfully changed.");
            return this.RedirectToAction(nameof(this.Profile), "Users", new { Area = "", username = username });
        }

        public async Task<IActionResult> Notifications(string username)
        {
            var user = await this.userManager.FindByNameAsync(username);

            if (user == null)
            {
                return this.NotFound();
            }

            var allNotifications = await this.notifications.All(user.Id);

            this.notifications.ChangeAllToRead(user.Id);

            return this.View(allNotifications);
        }

        public async Task<IActionResult> Ad(int id)
        {
            if (id != 0)
            {
                var adById = await this.ads.FindById(id);

                if (adById == null)
                {
                    return this.NotFound();
                }

                return this.View(new AdDetailsViewModel
                {
                    Ad = adById
                });
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var ad = await this.ads.Find(user?.Id);
            
            if (ad == null)
            {
                return this.RedirectToAction("CreateAd", "Ad", new {Area="Advertiser"});
            }

            return this.View(new AdDetailsViewModel
            {
                Ad = ad
            });
        }
    }
}
