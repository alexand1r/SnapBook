namespace Snapbook.Web.Areas.User.Controllers
{
    using Data.Models;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Albums;
    using Services.User;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AlbumsController : BaseController
    {
        private readonly IUserPhotoService photos;
        private readonly IUserCategoryService categories;
        private readonly IUserAlbumService albums;
        private readonly UserManager<User> userManager;

        public AlbumsController(
            IUserPhotoService photos,
            IUserCategoryService categories,
            IUserAlbumService albums,
            UserManager<User> userManager)
        {
            this.photos = photos;
            this.categories = categories;
            this.albums = albums;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var album = await this.albums.Find(id);

            if (user.Id != album.UserId)
            {
                return this.NotFound();
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

        public IActionResult AddPhoto(int albumId)
            => this.View();

        [HttpPost]
        [ValidateModelState]
        public IActionResult AddPhoto(int albumId, AddPhotoToAlbumViewModel model)
        {
            this.photos.Create(
                model.Description,
                model.ImageUrl,
                model.Location,
                model.Latitude,
                model.Longitude,
                model.Tags,
                albumId);

            return this.RedirectToAction("Details", "Albums", new { area = "", id = albumId});
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
