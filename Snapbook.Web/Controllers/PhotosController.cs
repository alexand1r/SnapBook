namespace Snapbook.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Snapbook.Data.Models;
    using Snapbook.Web.Models.Photos;

    public class PhotosController : Controller
    {
        private readonly IPhotoService photos;
        private readonly UserManager<User> userManager;

        public PhotosController(
            IPhotoService photos,
            UserManager<User> userManager)
        {
            this.photos = photos;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var canLike = await this.photos.CanLike(user.Id, id);
            var photo = await this.photos.Details(id);

            return this.View(new PhotoDetailsViewModel
            {
                Photo = photo,
                CanLike = canLike,
                User = user
            });
        }
        
        public async Task<IActionResult> Comment(int photoId, CreateCommentViewModel model)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var result = await this.photos.Comment(
                photoId,
                model.Content,
                user.Id);
            
            return this.PartialView("_Comments", result);
        }

        public async Task<IActionResult> Like(int photoId)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var result = await this.photos
                .Like(user.Id, photoId);

            return this.PartialView("_Likes", result);
        }

        public async Task<IActionResult> Unlike(int photoId)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var result = await this.photos
                .Unlike(user.Id, photoId);

            return this.PartialView("_Likes", result);
        }
    }
}
