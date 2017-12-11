namespace Snapbook.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
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
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.PublishDate)
                .ProjectTo<NotificationServiceModel>()
                .ToListAsync();

            return notifications;
        }
    }
}
