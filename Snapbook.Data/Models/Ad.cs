namespace Snapbook.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Ad
    {
        public int Id { get; set; }

        [Required]
        [StringLength(AdNameMaxLength, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = AdNameMinLength)]
        public string Name { get; set; }

        [Required]
        public string AdProfilePicUrl { get; set; }

        [Required]
        [StringLength(AdDescriptionMaxLength, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = AdDescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        [RegularExpression("www\\.[\\S]+\\.[\\S]+", ErrorMessage = "Website field should be in format 'www.example.com'")]
        public string Website { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public List<Photo> Photos { get; set; } = new List<Photo>();
    }
}
