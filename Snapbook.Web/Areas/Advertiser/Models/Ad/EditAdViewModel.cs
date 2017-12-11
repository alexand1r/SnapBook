namespace Snapbook.Web.Areas.Advertiser.Models.Ad
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class EditAdViewModel
    {
        [Required]
        [MinLength(AdNameMinLength)]
        [MaxLength(AdNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string AdProfilePicUrl { get; set; }

        [Required]
        [MinLength(AdNameMinLength)]
        [MaxLength(AdNameMaxLength)]
        public string Description { get; set; }

        [Required]
        public string Website { get; set; }
    }
}
