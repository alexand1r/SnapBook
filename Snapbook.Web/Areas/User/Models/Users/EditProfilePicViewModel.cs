namespace Snapbook.Web.Areas.User.Models.Users
{
    using System.ComponentModel.DataAnnotations;

    public class EditProfilePicViewModel
    {
        [Required]
        [Display(Name = "Choose a new picture")]
        public string ImageUrl { get; set; }

        public string Username { get; set; }
    }
}
