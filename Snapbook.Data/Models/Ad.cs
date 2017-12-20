namespace Snapbook.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Ad
    {
        public int Id { get; set; }

        [Required]
        [StringLength(AdNameMaxLength, ErrorMessage = StringLengthBetweenErrorMessage, MinimumLength = AdNameMinLength)]
        public string Name { get; set; }

        [Required]
        public string AdProfilePicUrl { get; set; }

        [Required]
        [StringLength(AdDescriptionMaxLength, ErrorMessage = StringLengthBetweenErrorMessage, MinimumLength = AdDescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        [RegularExpression(AdWebsiteRegex, ErrorMessage = WebsiteFormatErrorMessage)]
        public string Website { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public List<Photo> Photos { get; set; } = new List<Photo>();
    }
}
