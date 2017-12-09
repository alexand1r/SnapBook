namespace Snapbook.Web.Areas.User.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.Albums;
    using Services.User;
    using Snapbook.Web.Infrastructure.Filters;

    public class AlbumsController : BaseController
    {
        private readonly IUserPhotoService photos;

        public AlbumsController(IUserPhotoService photos)
        {
            this.photos = photos;
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

            return this.RedirectToAction("Details", "Albums", new {id = albumId});
        }
    }
}
