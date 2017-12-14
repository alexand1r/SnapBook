namespace Snapbook.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Snapbook.Data.Models;
    using Snapbook.Services.Admin;
    using Snapbook.Web.Areas.Admin.Models.Ads;
    using Snapbook.Web.Infrastructure.Filters;

    public class AdsController : BaseController
    {
        private readonly IAdminAdService ads;
        private readonly UserManager<User> userManager;
        
        public AdsController(
            IAdminAdService ads,
            UserManager<User> userManager)
        {
            this.ads = ads;
            this.userManager = userManager;
        }

        public async Task<IActionResult> All()
            => this.View(await this.ads.All());

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

            return this.RedirectToAction("Ad", "Users", new { area = "", id = id });
        }

        public async Task<bool> Delete(int id)
        {
            var ad = await this.ads.Exists(id);

            if (!ad)
            {
                //return this.NotFound();
                return false;
            }

            this.ads.Delete(id);

            //return this.RedirectToAction(nameof(this.All));
            return true;
        }
    }
}
