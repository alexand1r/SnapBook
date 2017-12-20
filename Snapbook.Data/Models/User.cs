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
        [StringLength(UserNameMaxLength, ErrorMessage = StringLengthBetweenErrorMessage, MinimumLength = UserNameMinLength)]
        public string Name { get; set; }

        [StringLength(UserBioMaxLength, ErrorMessage = StringLengthMaxLengthErrorMessage)]
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

