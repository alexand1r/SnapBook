namespace Snapbook.Web.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Snapbook.Data.Models;
    using Snapbook.Services.Admin;
    using Snapbook.Web.Areas.Admin.Models.Albums;

    public class AlbumsController : BaseController
    {
        private readonly IAdminAlbumService albums;
        private readonly IAdminCategoryService categories;
        private readonly UserManager<User> userManager;

        public AlbumsController(
            IAdminAlbumService albums,
            IAdminCategoryService categories,
            UserManager<User> userManager)
        {
            this.albums = albums;
            this.categories = categories;
            this.userManager = userManager;
        }

        public async Task<IActionResult> All()
            => this.View(await this.albums.All());

        public async Task<IActionResult> Edit(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var album = await this.albums.Find(id);
            
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
        public async Task<IActionResult> Edit(int id, EditAlbumViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Categories = await this.GetCategories();
                return this.View(model);
            }

            this.albums.Edit(
                model.Title,
                model.Description,
                int.Parse(model.CategoryId),
                id);

            return this.RedirectToAction("Details", "Albums", new { area = "", id = id });
        }

        public async Task<bool> Delete(int id)
        {
            var album = await this.albums.Exists(id);

            if (!album)
            {
                //return this.NotFound();
                return false;
            }

            this.albums.Delete(id);

            //return this.RedirectToAction(nameof(this.All));
            return true;
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
