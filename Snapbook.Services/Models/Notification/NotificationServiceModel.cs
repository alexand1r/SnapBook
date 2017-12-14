namespace Snapbook.Services.Models.Notification
{
    using System;
    using Common.Mapping;
    using Data.Models;

    public class NotificationServiceModel : IMapFrom<Notification>
    {
        public string Sender { get; set; }

        public string SenderUrl { get; set; }

        public int PhotoId { get; set; }

        public string PhotoUrl { get; set; }

        public string Action { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
