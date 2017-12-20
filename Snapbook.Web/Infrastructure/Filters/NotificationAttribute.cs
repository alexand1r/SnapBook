namespace Snapbook.Web.Infrastructure.Filters
{
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;

    public class NotificationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.ModelState.IsValid)
            {
                var date = DateTime.UtcNow;

                var username = context.HttpContext.User?.Identity?.Name ?? "Anonymous";

                var action = context.RouteData.Values["action"].ToString();
                var message = $"has {action}{(action.EndsWith('e') ? "d" : "ed")} your photo.";
                
                var photoId = int.Parse(context.HttpContext.Request.QueryString.ToString().ToLower().Replace("?photoid=", ""));

                var db = context.HttpContext.RequestServices.GetService<SnapbookDbContext>();

                var user = db.Users.FirstOrDefault(u => u.UserName == username);
                var userPicUrl = user?.ProfilePicUrl;

                var photo = db.Photos.FirstOrDefault(p => p.Id == photoId);
                var photoPicUrl = photo.ImageUrl;

                var albumId = photo.AlbumId;
                var adId = photo.AdId;

                var authorId = "";

                if (albumId == null)
                {
                    var ad = db.Ads.FirstOrDefault(a => a.Id == adId);
                    authorId = ad.UserId;
                }
                else
                {
                    var album = db.Albums.FirstOrDefault(a => a.Id == albumId);
                    authorId = album.UserId;
                }

                if (authorId == user?.Id)
                {
                    return;
                }

                var notification = new Notification
                {
                    Sender = username,
                    SenderUrl = userPicUrl,
                    Action = message,
                    PhotoId = photoId,
                    PhotoUrl = photoPicUrl,
                    PublishDate = date,
                    UserId = authorId
                };

                db.Notifications.Add(notification);
                db.SaveChanges();
            }
        }

    }
}
