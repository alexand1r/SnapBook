namespace Snapbook.Web.Areas.User.Models.Users
{
    using Data;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class CreateAlbumViewModel
    {
        [Required]
        [MinLength(AlbumTitleMinLength)]
        [MaxLength(AlbumTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(AlbumDescriptionMinLength)]
        [MaxLength(AlbumDescriptionMaxLength)]
        public string Description { get; set; }

        [Display(Name = "Category")]
        public string CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
