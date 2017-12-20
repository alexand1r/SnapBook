namespace Snapbook.Web.Areas.Admin.Controllers
{
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Albums;
    using PaulMiami.AspNetCore.Mvc.Recaptcha;
    using Services.Admin;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AlbumsController : BaseController
    {
        private readonly IAdminAlbumService albums;
        private readonly IAdminCategoryService categories;

        public AlbumsController(
            IAdminAlbumService albums,
            IAdminCategoryService categories)
        {
            this.albums = albums;
            this.categories = categories;
        }

        public async Task<IActionResult> All()
            => this.View(await this.albums.All());

        public async Task<IActionResult> Edit(int id)
        {
            var album = await this.albums.Find(id);

            if (album == null)
            {
                return this.RedirectToAction("NotFoundPage", "Home", new {Area=""});
            }

            var categoriess = await this.GetCategories();

            return this.View(new EditAlbumViewModel
            {
                Categories = categoriess,
                Title = album.Title,
                Description = album.Description,
                CategoryId = album.CategoryId
            });
        }

        [HttpPost]
        [ValidateRecaptcha]
        public async Task<IActionResult> Edit(int id, EditAlbumViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Categories = await this.GetCategories();
                return this.View(model);
            }

            var success = await this.albums.Edit(
                model.Title,
                model.Description,
                int.Parse(model.CategoryId),
                id);

            if (!success)
            {
                return this.BadRequest();
            }

            this.TempData.AddSuccessMessage($"Album {model.Title} details have been successfully changed.");
            return this.RedirectToAction("Details", "Albums", new { area = "", id = id });
        }

        public async Task<string> Delete(int id)
        {
            var success = await this.albums.Delete(id);

            if (!success)
            {
                return $"Album not found.";
            }

            return $"Album has been successfully deleted.";
        }

        private async Task<IEnumerable<SelectListItem>> GetCategories()
        {
            var categoriess = await this.categories.All();
            return categoriess
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
                .ToList();
        }
    }
}
