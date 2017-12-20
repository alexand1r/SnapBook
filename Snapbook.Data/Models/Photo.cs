namespace Snapbook.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Photo
    {
        public int Id { get; set; }

        [Required]
        [StringLength(PhotoDescriptionMaxLength, ErrorMessage = StringLengthBetweenErrorMessage, MinimumLength = PhotoDescriptionMinLength)]
        public string Description { get; set; }

        public DateTime PublishDate { get; set; }

        public string Location { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int? AlbumId { get; set; }

        public Album Album { get; set; }

        public int? AdId { get; set; }

        public Ad Ad { get; set; }

        public List<Tag> Tags { get; set; } = new List<Tag>();

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public List<UsersLikedImages> Likers { get; set; } = new List<UsersLikedImages>();

        public List<UsersSavedImages> Savers { get; set; } = new List<UsersSavedImages>();
    }
}
