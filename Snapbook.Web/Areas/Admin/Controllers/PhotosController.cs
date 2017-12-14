namespace Snapbook.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Snapbook.Services.Admin;

    public class PhotosController : BaseController
    {
        private readonly IAdminPhotoService photos;

        public PhotosController(IAdminPhotoService photos)
        {
            this.photos = photos;
        }

        public async Task<IActionResult> All()
            => this.View(await this.photos.All());

        public async Task<bool> Delete(int id)
        {
            var photo = await this.photos.Exists(id);

            if (!photo)
            {
                //return this.NotFound();
                return false;
            }

            this.photos.Delete(id);

            //return this.RedirectToAction(nameof(this.All));
            return true;
        }
    }
}
