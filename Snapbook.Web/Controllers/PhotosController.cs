namespace Snapbook.Web.Controllers
{
    using Data.Models;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Photos;
    using Services;
    using System.Linq;
    using System.Threading.Tasks;
    using PaulMiami.AspNetCore.Mvc.Recaptcha;
    using Snapbook.Web.Infrastructure.Extensions;

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

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var photo = await this.photos.FindForEdit(id);

            if (photo == null)
            {
                return this.NotFound();
            }

            if (user.Id != photo.AdUserId 
                && user.Id != photo.AlbumUserId 
                && !this.User.IsInRole(WebConstants.AdministratorRole))
            {
                return this.RedirectToAction("AccessDenied", "Account");
            }

            return this.View(new EditPhotoViewModel
            {
                Id = id,
                Description = photo.Description,
                Location = photo.Location,
                Longitude = photo.Longitude,
                Latitude = photo.Latitude,
                Tags = string.Join(" ", photo.Tags.Select(t => t.Content).ToList()),
            });
        }

        [Authorize]
        [HttpPost]
        [ValidateRecaptcha]
        public async Task<IActionResult> Edit(int id, EditPhotoViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var photo = await this.photos.FindForEdit(id);

            if (photo == null)
            {
                return this.BadRequest();
            }

            if (user.Id != photo.AdUserId
                && user.Id != photo.AlbumUserId
                && !this.User.IsInRole(WebConstants.AdministratorRole))
            {
                return this.BadRequest();
            }

            var success = await this.photos.Edit(
                id,
                model.Description,
                model.Location,
                model.Latitude,
                model.Longitude,
                model.Tags);

            if (!success)
            {
                return this.BadRequest();
            }

            this.TempData.AddSuccessMessage($"The photo details have been successfully changed.");
            return this.RedirectToAction("Details", "Photos", new { area = "", id = id });
        }

        public async Task<IActionResult> Details(int id)
        {
            var photo = await this.photos.Details(id);

            if (photo == null)
            {
                return this.NotFound();
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var canSave = false;
            var canLike = false;

            if (user != null)
            {
                canLike = await this.photos.CanLike(user.Id, id);
                canSave = await this.photos.CanSave(user.Id, id);
            }

            var relatedPhotos = await this.photos.RelatedPhotos(id);

            return this.View(new PhotoDetailsViewModel
            {
                Photo = photo,
                RelatedPhotos = relatedPhotos,
                CanLike = canLike,
                CanSave = canSave,
                User = user
            });
        }
        
        [Notification]
        public async Task<IActionResult> Comment(int photoId, CreateCommentViewModel model)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            
            var result = await this.photos.Comment(
                photoId,
                model.Content,
                user?.Id);

            return this.PartialView("_Comments", result);
        }

        [Authorize]
        [Notification]
        public async Task<IActionResult> Like(int photoId)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var result = await this.photos
                .Like(user.Id, photoId);

            return this.PartialView("_Likes", result);
        }

        [Authorize]
        [Notification]
        public async Task<IActionResult> Unlike(int photoId)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var result = await this.photos
                .Unlike(user.Id, photoId);

            return this.PartialView("_Likes", result);
        }

        [Authorize]
        public async Task<string> Save(int photoId)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var hasSaved = await this.photos
                .Save(user.Id, photoId);

            if (!hasSaved)
            {
                return $"Error: Photo not saved.";
            }

            return $"The photo has been successufully saved.";
        }

        [Authorize]
        public async Task<string> Unsave(int photoId)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var hasUnsaved = await this.photos
                .Unsave(user.Id, photoId);

            if (!hasUnsaved)
            {
                return $"Error: Photo is still saved.";
            }

            return $"The photo has been successfully unsaved.";
        }
    }
}
