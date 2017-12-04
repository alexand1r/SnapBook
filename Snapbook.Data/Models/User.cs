namespace Snapbook.Data.Models
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public string Bio { get; set; }

        public string ProfilePicUrl { get; set; }

        public List<Photo> Photos { get; set; }

        public List<UsersLikedImages> LikedPhotos { get; set; } = new List<UsersLikedImages>();

        public List<UsersSavedImages> SavedPhotos { get; set; } = new List<UsersSavedImages>();

        public List<Notification> Notifications { get; set; } = new List<Notification>();

        public List<UsersFollowers> Followers { get; set; } = new List<UsersFollowers>();

        public List<UsersFollowers> Following { get; set; } = new List<UsersFollowers>();
    }
}

