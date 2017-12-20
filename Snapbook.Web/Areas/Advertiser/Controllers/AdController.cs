namespace Snapbook.Web.Areas.Advertiser.Controllers
{
    using Data.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Models.Ad;
    using PaulMiami.AspNetCore.Mvc.Recaptcha;
    using Services.Advertiser;
    using System.Threading.Tasks;

    public class AdController : BaseController
    {
        private readonly IAdvertiserAdService ads;
        private readonly UserManager<User> userManager;

        public AdController(
            IAdvertiserAdService ads,
            UserManager<User> userManager)
        {
            this.ads = ads;
            this.userManager = userManager;
        }

        public async Task<IActionResult> CreateAd()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var ad = await this.ads.Find(user.Id);

            if (ad != null)
            {
                return this.BadRequest();
            }

            return this.View();
        }
        
        [HttpPost]
        [ValidateRecaptcha]
        public async Task<IActionResult> CreateAd(CreateAdViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var ad = await this.ads.Find(user.Id);

            if (ad != null)
            {
                return this.BadRequest();
            }

            var success = await this.ads.CreateAd(
                model.Name,
                model.Description,
                model.AdProfilePicUrl,
                model.Website,
                user.Id);

            if (!success)
            {
                return this.BadRequest();
            }

            this.TempData.AddSuccessMessage($"Ad {model.Name} has been successfully created.");
            return this.RedirectToAction("Ad", "Users", new { Area = "", id = 0 });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var ad = await this.ads.FindForEdit(id);

            if (ad == null)
            {
                return this.RedirectToAction("NotFoundPage", "Home", new {Area=""});
            }

            if (user.Id != ad.UserId)
            {
                return this.RedirectToAction("AccessDenied", "Account", new {Area = ""});
            }

            return this.View(new EditAdViewModel
            {
                Name = ad.Name,
                Description = ad.Description,
                AdProfilePicUrl = ad.AdProfilePicUrl,
                Website = ad.Website
            });
        }

        [HttpPost]
        [ValidateRecaptcha]
        public async Task<IActionResult> Edit(int id, EditAdViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var ad = await this.ads.FindForEdit(id);
            
            if (user.Id != ad.UserId)
            {
                return this.BadRequest();
            }

            var success = await this.ads.Edit(
                model.Name,
                model.Description,
                model.AdProfilePicUrl,
                model.Website,
                user.Id,
                id);

            if (!success)
            {
                return this.BadRequest();
            }

            this.TempData.AddSuccessMessage($"Ad {model.Name} details have been successfully changed.");
            return this.RedirectToAction("Ad", "Users", new {Area = "", id = id});
        }
    }
}
