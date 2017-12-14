namespace Snapbook.Web.Models.Photos
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class CreateCommentViewModel
    {
        [Required]
        [StringLength(CommentContentMaxLength, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = CommentContentMinLength)]
        public string Content { get; set; }
    }
}
