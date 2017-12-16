namespace Snapbook.Web.Areas.Advertiser.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Snapbook.Data.Models;
    using Snapbook.Services.Advertiser;
    using Snapbook.Web.Areas.Advertiser.Models.Ad;
    using Snapbook.Web.Areas.Advertiser.Models.Photos;
    using Snapbook.Web.Infrastructure.Extensions;
    using Snapbook.Web.Infrastructure.Filters;

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
                return this.NotFound();
            }

            if (user.Id != ad.UserId)
            {
                return this.RedirectToAction("AccessDenied", "Account", new { Area = "" });
            }

            return this.View();
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> AddPhoto(int adId, AddPhotoToAdViewModel model)
        {
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
