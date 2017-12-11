namespace Snapbook.Web.Models.Photos
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class EditPhotoViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(PhotoDescriptionMinLength)]
        [MaxLength(PhotoDescriptionMaxLength)]
        public string Description { get; set; }

        public string Location { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string UserId { get; set; }

        public string Tags { get; set; }
    }
}
