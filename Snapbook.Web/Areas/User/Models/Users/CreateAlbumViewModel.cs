namespace Snapbook.Web.Areas.User.Models.Users
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class CreateAlbumViewModel
    {
        [Required]
        [StringLength(AlbumTitleMaxLength, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = AlbumTitleMinLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(AlbumDescriptionMaxLength, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = AlbumDescriptionMinLength)]
        public string Description { get; set; }

        [Display(Name = "Category")]
        public string CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
