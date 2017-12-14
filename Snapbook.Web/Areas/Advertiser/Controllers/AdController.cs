namespace Snapbook.Web.Areas.Advertiser.Controllers
{
    using Data.Models;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Models.Ad;
    using Services.Advertiser;
    using System.Threading.Tasks;

    public class AdController : BaseController
    {
        private readonly IAdvertiserPhotoService photos;
        private readonly IAdvertiserAdService ads;
        private readonly UserManager<User> userManager;

        public AdController(
            IAdvertiserPhotoService photos,
            IAdvertiserAdService ads,
            UserManager<User> userManager)
        {
            this.photos = photos;
            this.ads = ads;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var ad = await this.ads.FindForEdit(id);

            if (user == null || ad == null)
            {
                return this.NotFound();
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
        [ValidateModelState]
        public async Task<IActionResult> Edit(int id, EditAdViewModel model)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user == null)
            {
                return this.NotFound();
            }

            this.ads.Edit(
                model.Name,
                model.Description,
                model.AdProfilePicUrl,
                model.Website,
                user.Id,
                id);

            return this.RedirectToAction("Ad", "Users", new {area = "", id = id});
        }

        public IActionResult AddPhoto(int adId)
            => this.View();

        [HttpPost]
        [ValidateModelState]
        public IActionResult AddPhoto(int adId, AddPhotoToAdViewModel model)
        {
            this.photos.Create(
                model.Description,
                model.ImageUrl,
                model.Location,
                model.Latitude,
                model.Longitude,
                model.Tags,
                adId);

            return this.RedirectToAction("Ad", "Users", new { area = "Advertiser" });
        }
    }
}
