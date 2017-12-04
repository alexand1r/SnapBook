namespace Snapbook.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Photo
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int Likes { get; set; }

        public DateTime PublishDate { get; set; }

        public string Location { get; set; }

        public string ImageUrl { get; set; }

        public int AdId { get; set; }

        public Ad Ad { get; set; }

        public string ProfileId { get; set; }

        public User Profile { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public List<UsersLikedImages> Likers { get; set; } = new List<UsersLikedImages>();

        public List<UsersSavedImages> Savers { get; set; } = new List<UsersSavedImages>();
    }
}
