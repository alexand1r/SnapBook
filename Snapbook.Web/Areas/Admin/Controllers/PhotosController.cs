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

        public async Task<string> Delete(int id)
        {
            var success = await this.photos.Delete(id);

            if (!success)
            {
                return $"Photo Not Found.";
            }

            return $"The photo has been successfully deleted.";
        }
    }
}
