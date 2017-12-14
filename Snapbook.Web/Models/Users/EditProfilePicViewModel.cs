namespace Snapbook.Web.Models.Users
{
    using System.ComponentModel.DataAnnotations;

    public class EditProfilePicViewModel
    {
        [Required]
        [Display(Name = "Choose a new photo")]
        public string ImageUrl { get; set; }

        public string Username { get; set; }
    }
}
