namespace Snapbook.Web.Areas.Advertiser.Models.Ad
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class EditAdViewModel
    {
        [Required]
        [StringLength(AdNameMaxLength, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = AdNameMinLength)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Choose a different photo")]
        public string AdProfilePicUrl { get; set; }

        [Required]
        [StringLength(AdDescriptionMaxLength, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = AdDescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        [RegularExpression("www\\.[\\S]+\\.[\\S]+", ErrorMessage = "Website field should be in format 'www.example.com'")]
        public string Website { get; set; }
    }
}
