namespace Snapbook.Web.Areas.User.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Users;
    using Services.User;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsersController : BaseController
    {
        private readonly IUserCategoryService categories;
        private readonly IUserAlbumService albums;
        private readonly UserManager<User> userManager;

        public UsersController(
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

            return this.RedirectToAction("Index", "Home", new { area = "" });
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
