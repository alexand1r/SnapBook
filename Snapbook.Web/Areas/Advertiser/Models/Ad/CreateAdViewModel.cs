namespace Snapbook.Web.Areas.Advertiser.Models.Ad
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class CreateAdViewModel
    {
        [Required]
        [StringLength(AdNameMaxLength, ErrorMessage = StringLengthBetweenErrorMessage, MinimumLength = AdNameMinLength)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Choose a photo")]
        public string AdProfilePicUrl { get; set; }

        [Required]
        [StringLength(AdDescriptionMaxLength, ErrorMessage = StringLengthBetweenErrorMessage, MinimumLength = AdDescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        [RegularExpression(AdWebsiteRegex, ErrorMessage = WebsiteFormatErrorMessage)]
        public string Website { get; set; }
    }
}
