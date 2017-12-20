namespace Snapbook.Web.Areas.Advertiser.Models.Photos
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class AddPhotoToAdViewModel
    {
        [Required]
        [StringLength(PhotoDescriptionMaxLength, ErrorMessage = StringLengthBetweenErrorMessage, MinimumLength = PhotoDescriptionMinLength)]
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
