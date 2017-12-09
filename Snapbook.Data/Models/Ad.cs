namespace Snapbook.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Ad
    {
        public int Id { get; set; }

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

        public string Website { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public List<Photo> Photos { get; set; } = new List<Photo>();
    }
}
