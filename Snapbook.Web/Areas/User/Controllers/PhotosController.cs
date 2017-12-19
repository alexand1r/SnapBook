namespace Snapbook.Web.Areas.User.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Data.Models;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.User;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore.Internal;
    using PaulMiami.AspNetCore.Mvc.Recaptcha;
    using Snapbook.Web.Areas.User.Models.Photos;
    using Snapbook.Web.Infrastructure.Extensions;

    public class PhotosController : BaseController
    {
        private readonly IUserPhotoService photos;
        private readonly IUserAlbumService albums;
        private readonly UserManager<User> userManager;

        public PhotosController(
            IUserPhotoService photos,
            IUserAlbumService albums,
            UserManager<User> userManager)
        {
            this.photos = photos;
            this.albums = albums;
            this.userManager = userManager;
        }

        public async Task<IActionResult> AddPhoto(int albumId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var album = await this.albums.Find(albumId);

            if (album == null)
            {
                return this.NotFound();
            }

            if (user.Id != album.UserId)
            {
                return this.RedirectToAction("AccessDenied", "Account", new { Area = "" });
            }

            return this.View();
        }

        [HttpPost]
        [ValidateRecaptcha]
        public async Task<IActionResult> AddPhoto(int albumId, AddPhotoToAlbumViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var album = await this.albums.Find(albumId);

            var user = await this.userManager.GetUserAsync(this.User);

            if (album.UserId != user.Id)
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
                albumId);

            if (!success)
            {
                return this.BadRequest();
            }

            this.TempData.AddSuccessMessage($"The photo has been successfully added.");
            return this.RedirectToAction("Details", "Albums", new { area = "", id = albumId });
        }
    }
}
