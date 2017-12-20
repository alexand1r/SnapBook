namespace Snapbook.Web.Models.Photos
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class CreateCommentViewModel
    {
        [Required]
        [StringLength(CommentContentMaxLength, ErrorMessage = StringLengthBetweenErrorMessage, MinimumLength = CommentContentMinLength)]
        public string Content { get; set; }
    }
}
