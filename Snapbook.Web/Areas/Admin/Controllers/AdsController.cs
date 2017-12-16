namespace Snapbook.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Snapbook.Data.Models;
    using Snapbook.Services.Admin;
    using Snapbook.Web.Areas.Admin.Models.Ads;
    using Snapbook.Web.Infrastructure.Extensions;
    using Snapbook.Web.Infrastructure.Filters;

    public class AdsController : BaseController
    {
        private readonly IAdminAdService ads;
        
        public AdsController(
            IAdminAdService ads)
        {
            this.ads = ads;
        }

        public async Task<IActionResult> All()
            => this.View(await this.ads.All());

        public async Task<IActionResult> Edit(int id)
        {
            var ad = await this.ads.FindForEdit(id);

            if (ad == null)
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
            var success = await this.ads.Edit(
                model.Name,
                model.Description,
                model.AdProfilePicUrl,
                model.Website,
                id);

            if (!success)
            {
                return this.BadRequest();
            }

            this.TempData.AddSuccessMessage($"Ad {model.Name} details have been successfully changed.");
            return this.RedirectToAction("Ad", "Users", new { area = "", id = id });
        }

        public async Task<string> Delete(int id)
        {
            var success = await this.ads.Delete(id);

            if (!success)
            {
                return $"Ad Not Found";
            }

            return $"The ad has been succesffuly deleted.";
        }
    }
}
