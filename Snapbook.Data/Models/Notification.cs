﻿namespace Snapbook.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Notification
    {
        public int Id { get; set; }

        [Required]
        public string Message { get; set; }

        public bool IsRead { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
