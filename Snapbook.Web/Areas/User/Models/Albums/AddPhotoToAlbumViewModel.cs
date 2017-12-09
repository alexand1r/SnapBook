namespace Snapbook.Web.Areas.User.Models.Albums
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class AddPhotoToAlbumViewModel
    {
        [Required]
        [MinLength(PhotoDescriptionMinLength)]
        [MaxLength(PhotoDescriptionMaxLength)]
        public string Description { get; set; }

        public string Location { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        [Required]
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

        public string Tags { get; set; }
    }
}
