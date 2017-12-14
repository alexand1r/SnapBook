namespace Snapbook.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class User : IdentityUser
    {
        [Required]
        [StringLength(UserNameMaxLength, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = UserNameMinLength)]
        public string Name { get; set; }

        [StringLength(UserBioMaxLength, ErrorMessage = "The {0} must be no more than {1} characters long.")]
        public string Bio { get; set; }

        public string ProfilePicUrl { get; set; }

        public DateTime BirthDate { get; set; }

        public Ad Ad { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public List<Album> Albums { get; set; } = new List<Album>();

        public List<UsersLikedImages> LikedPhotos { get; set; } = new List<UsersLikedImages>();

        public List<UsersSavedImages> SavedPhotos { get; set; } = new List<UsersSavedImages>();

        public List<Notification> Notifications { get; set; } = new List<Notification>();
    }
}

