namespace Snapbook.Data.Models
{
    using System;

    public class Notification
    {
        public int Id { get; set; }

        public string Action { get; set; }

        public bool IsRead { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public DateTime PublishDate { get; set; }

        public int PhotoId { get; set; }

        public string PhotoUrl { get; set; }

        public string Sender { get; set; }

        public string SenderUrl { get; set; }
    }
}
