namespace Snapbook.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Snapbook.Services.Models.Notification;

    public interface INotificationService
    {
        Task<IEnumerable<NotificationServiceModel>> All(string userId);

        int FindUnRead(string username);

        void ChangeAllToRead(string userId);
    }
}
