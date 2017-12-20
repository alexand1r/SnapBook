namespace Snapbook.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Album
    {
        public int Id { get; set; }

        [Required]
        [StringLength(AlbumTitleMaxLength, ErrorMessage = StringLengthBetweenErrorMessage, MinimumLength = AlbumTitleMinLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(AlbumDescriptionMaxLength, ErrorMessage = StringLengthBetweenErrorMessage, MinimumLength = AlbumDescriptionMinLength)]
        public string Description { get; set; }

        public DateTime PublishDate { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public List<Photo> Photos { get; set; } = new List<Photo>();
    }
}
