namespace Snapbook.Web.Areas.User.Controllers
{
    using Data.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Albums;
    using PaulMiami.AspNetCore.Mvc.Recaptcha;
    using Services.User;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AlbumsController : BaseController
    {
        private readonly IUserCategoryService categories;
        private readonly IUserAlbumService albums;
        private readonly UserManager<User> userManager;

        public AlbumsController(
            IUserCategoryService categories,
            IUserAlbumService albums,
            UserManager<User> userManager)
        {
            this.categories = categories;
            this.albums = albums;
            this.userManager = userManager;
        }

        public async Task<ActionResult> CreateAlbum()
        {
            var categoriess = await this.GetCategories();

            return this.View(new CreateAlbumViewModel
            {
                Categories = categoriess
            });
        }

        [HttpPost]
        [ValidateRecaptcha]
        public async Task<IActionResult> CreateAlbum(CreateAlbumViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Categories = await this.GetCategories();
                return this.View(model);
            }

            var userId = this.userManager.GetUserId(this.User);

            this.albums.Create(
                model.Title,
                model.Description,
                int.Parse(model.CategoryId),
                userId);

            this.TempData.AddSuccessMessage($"Album {model.Title} has been successfully created.");
            return this.RedirectToAction("Profile", "Users", new { area = "", username = this.User.Identity.Name });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var album = await this.albums.Find(id);

            if (album == null)
            {
                return this.RedirectToAction("NotFoundPage", "Home", new {Area=""});
            }

            if (user.Id != album.UserId)
            {
                return this.RedirectToAction("AccessDenied", "Account");
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

            var user = await this.userManager.GetUserAsync(this.User);

            var album = await this.albums.Find(id);

            if (user.Id != album.UserId)
            {
                return this.BadRequest();
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
            return this.RedirectToAction("Details", "Albums", new { Area = "", id = id });
        }

        private async Task<IEnumerable<SelectListItem>> GetCategories()
        {
            var categoriess = await this.categories.AllAsync();
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
