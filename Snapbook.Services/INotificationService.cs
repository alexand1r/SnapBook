namespace Snapbook.Services
{
    using Models.Notification;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface INotificationService
    {
        Task<IEnumerable<NotificationServiceModel>> All(string userId);

        int FindUnRead(string username);

        void ChangeAllToRead(string userId);
    }
}
