namespace Snapbook.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Snapbook.Data.Models;
    using Snapbook.Services.Models.Notification;

    public class NotificationService : INotificationService
    {
        private readonly SnapbookDbContext db;

        public NotificationService(SnapbookDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<NotificationServiceModel>> All(string userId)
        {
            var notifications = await this.db
                .Notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .OrderByDescending(n => n.PublishDate)
                .ProjectTo<NotificationServiceModel>()
                .ToListAsync();

            return notifications;
        }

        public int FindUnRead(string username)
            => this.db
                .Notifications
                .Count(n => n.User.UserName == username && !n.IsRead);

        public void ChangeAllToRead(string userId)
        {
            var notReadNotifications = this.db
                .Notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .ToList();

            foreach (var n in notReadNotifications)
            {
                n.IsRead = true;
            }

            this.db.SaveChanges();
        }
    }
}
