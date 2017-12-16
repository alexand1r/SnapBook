namespace Snapbook.Web.Areas.User.Models.Photos
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class AddPhotoToAlbumViewModel
    {
        [Required]
        [StringLength(PhotoDescriptionMaxLength, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = PhotoDescriptionMinLength)]
        public string Description { get; set; }

        public string Location { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        [Required]
        [Display(Name = "Choose a photo")]
        public string ImageUrl { get; set; }

        public string Tags { get; set; }
    }
}
