namespace Snapbook.Web.Areas.Advertiser.Controllers
{
    using Data.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Photos;
    using PaulMiami.AspNetCore.Mvc.Recaptcha;
    using Services.Advertiser;
    using System.Threading.Tasks;

    public class PhotosController : BaseController
    {
        private readonly IAdvertiserPhotoService photos;
        private readonly IAdvertiserAdService ads;
        private readonly UserManager<User> userManager;

        public PhotosController(
            IAdvertiserPhotoService photos,
            IAdvertiserAdService ads,
            UserManager<User> userManager)
        {
            this.photos = photos;
            this.ads = ads;
            this.userManager = userManager;
        }

        public async Task<IActionResult> AddPhoto(int adId)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var ad = await this.ads.FindForEdit(adId);

            if (ad == null)
            {
                return this.RedirectToAction("NotFoundPage", "Home", new {Area=""});
            }

            if (user.Id != ad.UserId)
            {
                return this.RedirectToAction("AccessDenied", "Account", new { Area = "" });
            }

            return this.View();
        }

        [HttpPost]
        [ValidateRecaptcha]
        public async Task<IActionResult> AddPhoto(int adId, AddPhotoToAdViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var ad = await this.ads.FindForEdit(adId);

            if (user.Id != ad.UserId)
            {
                return this.BadRequest();
            }

            var success = await this.photos.Create(
                model.Description,
                model.ImageUrl,
                model.Location,
                model.Latitude,
                model.Longitude,
                model.Tags,
                adId);

            if (!success)
            {
                return this.BadRequest();
            }

            this.TempData.AddSuccessMessage($"The photo has been successfully added.");
            return this.RedirectToAction("Ad", "Users", new { Area = "", id = adId });
        }
    }
}
