namespace Snapbook.Web.Areas.User.Models.Albums
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class CreateAlbumViewModel
    {
        [Required]
        [StringLength(AlbumTitleMaxLength, ErrorMessage = StringLengthBetweenErrorMessage, MinimumLength = AlbumTitleMinLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(AlbumDescriptionMaxLength, ErrorMessage = StringLengthBetweenErrorMessage, MinimumLength = AlbumDescriptionMinLength)]
        public string Description { get; set; }

        [Display(Name = "Category")]
        public string CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
