﻿namespace Snapbook.Web.Models.Photos
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class EditPhotoViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(PhotoDescriptionMaxLength, ErrorMessage = StringLengthBetweenErrorMessage, MinimumLength = PhotoDescriptionMinLength)]
        public string Description { get; set; }

        public string Location { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string Tags { get; set; }
    }
}
