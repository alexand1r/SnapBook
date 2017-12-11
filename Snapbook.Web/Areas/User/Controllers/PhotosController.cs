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

    public class PhotosController : BaseController
    {
        private readonly IUserPhotoService photos;
        private readonly UserManager<User> userManager;

        public PhotosController(
            IUserPhotoService photos,
            UserManager<User> userManager)
        {
            this.photos = photos;
            this.userManager = userManager;
        }
    }
}
