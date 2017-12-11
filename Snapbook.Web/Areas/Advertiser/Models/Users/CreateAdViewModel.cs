namespace Snapbook.Web.Areas.Advertiser.Models.Users
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class CreateAdViewModel
    {
        [Required]
        [MinLength(AdNameMinLength)]
        [MaxLength(AdNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Ad Picture")]
        public string AdProfilePicUrl { get; set; }

        [Required]
        [MinLength(AdNameMinLength)]
        [MaxLength(AdNameMaxLength)]
        public string Description { get; set; }

        [Required]
        public string Website { get; set; }
    }
}
