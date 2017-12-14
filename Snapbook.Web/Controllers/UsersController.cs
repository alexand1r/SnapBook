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
        [ValidateModelState]
        public IActionResult ChangeProfilePic(string username, EditProfilePicViewModel model)
        {
            this.users.EditProfilePic(username, model.ImageUrl);

            return this.RedirectToAction(nameof(this.Profile), "Users", new { area = "", username = username });
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
            var userId = user?.Id;

            var ad = await this.ads.Find(userId);
            
            if (ad == null)
            {
                return this.RedirectToAction("CreateAd", "Users", new {Area="Advertiser"});
            }

            return this.View(new AdDetailsViewModel
            {
                Ad = ad
            });
        }
    }
}
