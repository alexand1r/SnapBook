﻿namespace Snapbook.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(CommentContentMaxLength, ErrorMessage = StringLengthBetweenErrorMessage, MinimumLength = CommentContentMinLength)]
        public string Content { get; set; }

        public DateTime PublishDate { get; set; }
        
        public User Author { get; set; }

        public string AuthorId { get; set; }

        public int PhotoId { get; set; }

        public Photo Photo { get; set; }
    }
}
